namespace EyeSoft.Linq.Expressions.CodeDom
{
	using System;
	using System.Linq.Expressions;

	using EyeSoft.CodeDom;
	using EyeSoft.CodeDom.Compiler;
	using EyeSoft.Reflection;

	public class CodeDomExpressionParser
		: IExpressionParser
	{
		public virtual Expression<Func<T, bool>> Parse<T>(string expression)
		{
			Enforce
				.Argument(() => expression);

			const string ClassName = "ExpressionBuilder";
			const string MethodName = "GetExpression";
			const string NamespaceName = "EyeSoft.CodeDom";

			var codeDomFluent =
				new CodeDomFluent()
					.ReferenceAssembly(typeof(T).Assembly.Location)
					.ReferenceAssembly("System.dll")
					.ReferenceAssembly("System.Core.dll")
						.Namespace(NamespaceName)
							.Class(ClassName)
								.Using("System")
								.Using("System.Linq.Expressions")
								.AddMethod<Expression<Func<T, bool>>>(MethodName)
									.Return(expression);

			var assembly =
				new CodeDomFluentCompiler()
					.Compile(codeDomFluent);

			return
				assembly
				.GetType(string.Format("{0}.{1}", NamespaceName, ClassName))
				.GetMethod(MethodName)
				.Invoke<Expression<Func<T, bool>>>(null);
		}
	}
}