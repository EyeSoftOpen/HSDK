namespace EyeSoft.CodeDom
{
	using System;

	public class MethodParameter
	{
		public MethodParameter(Type type, string name)
		{
			Name = name;
			Type = type;
		}

		public string Name { get; private set; }

		public Type Type { get; private set; }

		public static MethodParameter Create<T>(string parameterName)
		{
			return
				new MethodParameter(typeof(T), parameterName);
		}
	}
}