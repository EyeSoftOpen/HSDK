namespace EyeSoft.CodeDom
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using EyeSoft.Text;

	public class Method
		: IMethodBody, IMethodBodyMultiLine, IReturnValueMethod, IVoidMethod
	{
		private readonly CodeDomFluent codeDomFluent;

		private readonly IDictionary<string, MethodParameter> parameterDictionary =
			new Dictionary<string, MethodParameter>();

		private StringBuilder methodBodyBuilder;

		public Method(CodeDomFluent codeDomFluent, Type type, string name)
		{
			this.codeDomFluent = codeDomFluent;
			Name = name;
			ReturnType = type ?? typeof(void);
			ParameterList = parameterDictionary.Values;
			Visibility = MemberVisibility.Public;
		}

		internal string Name { get; private set; }

		internal Type ReturnType { get; private set; }

		internal string Source { get; private set; }

		internal IEnumerable<MethodParameter> ParameterList { get; private set; }

		internal MemberVisibility Visibility { get; private set; }

		public IMethodBody SetVisibility(MemberVisibility visibility)
		{
			Visibility = visibility;
			return this;
		}

		public IMethodBody AddParameter<T>(string parameterName)
		{
			var methodParameter = MethodParameter.Create<T>(parameterName);
			parameterDictionary.Add(parameterName, methodParameter);
			return this;
		}

		public IMethodBody AddReturnType<T>()
		{
			ReturnType = typeof(T);
			return this;
		}

		CodeDomFluent IMethodBody.Body(string body)
		{
			Source = string.Format("{0};", body);
			return codeDomFluent;
		}

		public IMethodBodyMultiLine Body()
		{
			methodBodyBuilder = new StringBuilder();
			return this;
		}

		public IMethodBodyMultiLine AddLine(string line)
		{
			methodBodyBuilder.AppendLineFormat("{0};", line);
			return this;
		}

		public CodeDomFluent Return(string line)
		{
			if (methodBodyBuilder == null)
			{
				methodBodyBuilder = new StringBuilder();
			}

			methodBodyBuilder.AppendFormat("return {0};", line);
			return ReturnVoid();
		}

		public CodeDomFluent ReturnVoid()
		{
			Source = methodBodyBuilder.ToString();
			return codeDomFluent;
		}
	}
}