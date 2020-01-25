namespace EyeSoft
{
    using System;
    using System.CodeDom;
    using System.Globalization;
    using System.Text;

    public class TypeName
	{
		private static readonly string[][] keywords = new string[][] {
			null,           // 1 character
            new string[] {  // 2 characters
                "as",
				"do",
				"if",
				"in",
				"is",
			},
			new string[] {  // 3 characters
                "for",
				"int",
				"new",
				"out",
				"ref",
				"try",
			},
			new string[] {  // 4 characters
                "base",
				"bool",
				"byte",
				"case",
				"char",
				"else",
				"enum",
				"goto",
				"lock",
				"long",
				"null",
				"this",
				"true",
				"uint",
				"void",
			},
			new string[] {  // 5 characters
                "break",
				"catch",
				"class",
				"const",
				"event",
				"false",
				"fixed",
				"float",
				"sbyte",
				"short",
				"throw",
				"ulong",
				"using",
				"while",
			},
			new string[] {  // 6 characters
                "double",
				"extern",
				"object",
				"params",
				"public",
				"return",
				"sealed",
				"sizeof",
				"static",
				"string",
				"struct",
				"switch",
				"typeof",
				"unsafe",
				"ushort",
			},
			new string[] {  // 7 characters
                "checked",
				"decimal",
				"default",
				"finally",
				"foreach",
				"private",
				"virtual",
			},
			new string[] {  // 8 characters
                "abstract",
				"continue",
				"delegate",
				"explicit",
				"implicit",
				"internal",
				"operator",
				"override",
				"readonly",
				"volatile",
			},
			new string[] {  // 9 characters
                "__arglist",
				"__makeref",
				"__reftype",
				"interface",
				"namespace",
				"protected",
				"unchecked",
			},
			new string[] {  // 10 characters
                "__refvalue",
				"stackalloc",
			},
		};

		public string GetTypeOutput(CodeTypeReference typeRef)
		{
			string s = String.Empty;

			CodeTypeReference baseTypeRef = typeRef;
			while (baseTypeRef.ArrayElementType != null)
			{
				baseTypeRef = baseTypeRef.ArrayElementType;
			}
			s += GetBaseTypeOutput(baseTypeRef);

			while (typeRef != null && typeRef.ArrayRank > 0)
			{
				char[] results = new char[typeRef.ArrayRank + 1];
				results[0] = '[';
				results[typeRef.ArrayRank] = ']';
				for (int i = 1; i < typeRef.ArrayRank; i++)
				{
					results[i] = ',';
				}
				s += new string(results);
				typeRef = typeRef.ArrayElementType;
			}

			return s;
		}

		private void GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments, int start, int length, StringBuilder sb)
		{
			sb.Append('<');
			bool first = true;
			for (int i = start; i < start + length; i++)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					sb.Append(", ");
				}

				// it's possible that we call GetTypeArgumentsOutput with an empty typeArguments collection.  This is the case
				// for open types, so we want to just output the brackets and commas. 
				if (i < typeArguments.Count)
					sb.Append(GetTypeOutput(typeArguments[i]));
			}
			sb.Append('>');
		}

		private string GetBaseTypeOutput(CodeTypeReference typeRef)
		{
			string s = typeRef.BaseType;
			if (s.Length == 0)
			{
				s = "void";
				return s;
			}

			string lowerCaseString = s.ToLower(CultureInfo.InvariantCulture).Trim();

			switch (lowerCaseString)
			{
				case "system.int16":
					s = "short";
					break;
				case "system.int32":
					s = "int";
					break;
				case "system.int64":
					s = "long";
					break;
				case "system.string":
					s = "string";
					break;
				case "system.object":
					s = "object";
					break;
				case "system.boolean":
					s = "bool";
					break;
				case "system.void":
					s = "void";
					break;
				case "system.char":
					s = "char";
					break;
				case "system.byte":
					s = "byte";
					break;
				case "system.uint16":
					s = "ushort";
					break;
				case "system.uint32":
					s = "uint";
					break;
				case "system.uint64":
					s = "ulong";
					break;
				case "system.sbyte":
					s = "sbyte";
					break;
				case "system.single":
					s = "float";
					break;
				case "system.double":
					s = "double";
					break;
				case "system.decimal":
					s = "decimal";
					break;
				default:
					// replace + with . for nested classes.
					//
					StringBuilder sb = new StringBuilder(s.Length + 10);
					if ((typeRef.Options & CodeTypeReferenceOptions.GlobalReference) != 0)
					{
						sb.Append("global::");
					}

					string baseType = typeRef.BaseType;

					int lastIndex = 0;
					int currentTypeArgStart = 0;
					for (int i = 0; i < baseType.Length; i++)
					{
						switch (baseType[i])
						{
							case '+':
							case '.':
								sb.Append(CreateEscapedIdentifier(baseType.Substring(lastIndex, i - lastIndex)));
								sb.Append('.');
								i++;
								lastIndex = i;
								break;

							case '`':
								sb.Append(CreateEscapedIdentifier(baseType.Substring(lastIndex, i - lastIndex)));
								i++;    // skip the '
								int numTypeArgs = 0;
								while (i < baseType.Length && baseType[i] >= '0' && baseType[i] <= '9')
								{
									numTypeArgs = numTypeArgs * 10 + (baseType[i] - '0');
									i++;
								}

								GetTypeArgumentsOutput(typeRef.TypeArguments, currentTypeArgStart, numTypeArgs, sb);
								currentTypeArgStart += numTypeArgs;

								// Arity can be in the middle of a nested type name, so we might have a . or + after it. 
								// Skip it if so. 
								if (i < baseType.Length && (baseType[i] == '+' || baseType[i] == '.'))
								{
									sb.Append('.');
									i++;
								}

								lastIndex = i;
								break;
						}
					}

					if (lastIndex < baseType.Length)
						sb.Append(CreateEscapedIdentifier(baseType.Substring(lastIndex)));

					return sb.ToString();
			}
			return s;
		}

		public string CreateEscapedIdentifier(string name)
		{
			// Any identifier started with two consecutive underscores are 
			// reserved by CSharp.
			if (IsKeyword(name) || IsPrefixTwoUnderscore(name))
			{
				return "@" + name;
			}
			return name;
		}

		private static bool IsPrefixTwoUnderscore(string value)
		{
			if (value.Length < 3)
			{
				return false;
			}
			else
			{
				return ((value[0] == '_') && (value[1] == '_') && (value[2] != '_'));
			}
		}

		private static bool IsKeyword(string value)
		{
			return FixedStringLookup.Contains(keywords, value, false);
		}
	}
}