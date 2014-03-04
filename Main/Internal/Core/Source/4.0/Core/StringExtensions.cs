namespace EyeSoft
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	public static class StringExtensions
	{
		public static bool ContainsUpperCase(this string value)
		{
			return value.ContainsAny(char.IsUpper);
		}

		public static bool ContainsDigits(this string value)
		{
			return value.ContainsAny(char.IsDigit);
		}

		public static bool ContainsSpaces(this string value)
		{
			return value.ContainsAny(char.IsWhiteSpace);
		}

		public static bool ContainsAny(this string value, Func<char, bool> func)
		{
			return value != null && value.Any(func);
		}

		public static bool AllAreLowerCase(this string value)
		{
			return value.AllAre(char.IsLower);
		}

		public static bool AllAreLetter(this string value)
		{
			return value.AllAre(char.IsLetter);
		}

		public static bool AllAreLetterOrDigit(this string value)
		{
			return value.AllAre(char.IsLetterOrDigit);
		}

		public static bool AllAre(this string value, Func<char, bool> func)
		{
			return value != null && value.All(func);
		}

		public static string SplittedToTitleCase(this string value, char separator = ' ')
		{
			return
				value.Split(separator)
					.Select(text => text.ToLower().ToTitleCase())
					.Aggregate((current, next) => string.Concat(current, separator, next));
		}

		public static string TrimStart(this string text, string trimText)
		{
			return text.TrimStart(trimText.ToCharArray());
		}

		public static string TrimEnd(this string text, string trimText)
		{
			return text.TrimEnd(trimText.ToCharArray());
		}

		public static string Trim(this string text, string trimText)
		{
			return text.Trim(trimText.ToCharArray());
		}

		public static TokenDictionary Token(this string value, int tokenNumber, int lenght)
		{
			return new TokenDictionary(value, tokenNumber, lenght);
		}

		public static bool ContainsInvariant(this string source, string value)
		{
			return Contains(source, value, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool Contains(this string source, string value, StringComparison comparison)
		{
			return source.IndexOf(value, comparison) >= 0;
		}

		public static bool IgnoreCaseEquals(this string value, string compare)
		{
			return
				value == null ? Equals(null, compare) :
					value.Equals(compare, StringComparison.InvariantCultureIgnoreCase);
		}

		public static string Concatenate(this string value, params string[] values)
		{
			var list = new List<string> { value };
			list.AddRange(values);
			return string.Concat(list.ToArray());
		}

		public static T ToEnum<T>(this string value)
		{
			var enumType = typeof(T);

			Ensure.That(enumType.IsEnum).Is.True();

			return (T)Enum.Parse(enumType, value, true);
		}

		public static string ToTitleCase(this string value)
		{
			return value == null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
		}

		public static bool IsNullOrWhiteSpace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		public static string NamedFormat(this string format, params object[] args)
		{
			const char OpenMnemonicChar = '{';
			const char CloseMnemonicChar = '}';

			var mnemonic = false;

			string mnemonicName = null;
			string formatted = null;
			var mnemonicIndex = 0;

			var mnemonicDictionary = new Dictionary<string, int>();

			foreach (var character in format)
			{
				if (character == OpenMnemonicChar)
				{
					mnemonic = true;
					mnemonicName = null;
					continue;
				}

				if (mnemonic)
				{
					if (!char.IsLetterOrDigit(character) && character != OpenMnemonicChar && character != CloseMnemonicChar)
					{
						new ArgumentException("Mnemonic tokens can contain only letters or digits.")
							.Throw();
					}

					if (character != CloseMnemonicChar)
					{
						mnemonicName += character;
					}
					else
					{
						if (string.IsNullOrEmpty(mnemonicName))
						{
							new ArgumentException("Mnemonic tokens cannnot be empty.")
								.Throw();
						}

						if (!mnemonicDictionary.ContainsKey(mnemonicName))
						{
							mnemonicDictionary.Add(mnemonicName, mnemonicIndex++);
						}

						var argumentIndex = mnemonicDictionary[mnemonicName];

						if (argumentIndex >= args.Length)
						{
							new ArgumentException("The arguments number is not correct.")
								.Throw();
						}

						formatted += args[argumentIndex];

						mnemonic = false;
					}
				}
				else
				{
					formatted += character;
				}
			}

			return formatted;
		}

		public static XElement ToXElement(this string text)
		{
			return XElement.Parse(text);
		}

		public static string JoinMultiLine(this IEnumerable<string> tokens)
		{
			return tokens.Join(Environment.NewLine);
		}

		public static string JoinWithSpace(this IEnumerable<string> tokens)
		{
			return tokens.Join(" ");
		}

		public static string Join(this IEnumerable<string> tokens, string joiner = null)
		{
			return tokens.Aggregate((current, next) => current + joiner + next);
		}

		public static int Count(this string value, char character, bool startFromEnd = false)
		{
			var localValue = value;

			if (startFromEnd)
			{
				localValue = value.Reverse();
			}

			var count = 0;

			foreach (var element in localValue)
			{
				if (element == character)
				{
					count++;
				}
				else
				{
					return count;
				}
			}

			return count;
		}

		public static string Reverse(this string value)
		{
			var charArray = value.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}

		public static Stream ToStream(this string text)
		{
			var stream = new MemoryStream();

			var writer = new StreamWriter(stream);
			writer.Write(text);
			writer.Flush();

			stream.Position = 0;
			return stream;
		}

		public static byte[] ToByteArray(this string s)
		{
			var bytes = new byte[s.Length * sizeof(char)];
			Buffer.BlockCopy(s.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public static string Replace(this string source, string[] oldValues, string newValue)
		{
			// Don't use LINQ, it's slower
			foreach (var old in oldValues)
			{
				source = source.Replace(old, newValue);
			}

			return source;
		}

		public static string Replace(this string source, char[] oldValues, char? newValue = null)
		{
			char[] replacedChars;

			if (!newValue.HasValue)
			{
				replacedChars = source.Where(c => !oldValues.Contains(c)).ToArray();

				return new string(replacedChars);
			}

			replacedChars = source.Select(c => oldValues.Contains(c) ? newValue.Value : c).ToArray();

			return new string(replacedChars);
		}

		////private static void CheckFormat(string format)
		////{
		////	const char OpenMnemonicChar = '{';
		////	const char CloseMnemonicChar = '}';
		////	var mnemonic = false;

		////	foreach (var character in format)
		////	{
		////		if (mnemonic)
		////		{
		////			if (!char.IsLetterOrDigit(character) && character != CloseMnemonicChar)
		////			{
		////				throw new ArgumentException("Mnemonic tokens can contain only letters or digits.");
		////			}
		////		}

		////		if (character == OpenMnemonicChar)
		////		{
		////			mnemonic = true;
		////		}

		////		if (character == CloseMnemonicChar)
		////		{
		////			mnemonic = false;
		////		}
		////	}
		////}
	}
}