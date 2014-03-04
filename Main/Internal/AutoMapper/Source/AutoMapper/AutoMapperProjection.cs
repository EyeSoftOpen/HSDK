namespace EyeSoft.AutoMapper
{
	using System;
	using System.Linq;
	using System.Reflection;

	using global::AutoMapper.QueryableExtensions;

	using EyeSoft.Mapping;

	public class AutoMapperProjection : IProjection
	{
		private static readonly Type autoMapperProjectionType = typeof(AutoMapperProjection);

		private static readonly MethodInfo projectMethod;

		static AutoMapperProjection()
		{
			projectMethod =
				autoMapperProjectionType
					.GetMethods().Single(x => x.Name == "Project" && x.GetParameters().Single().ParameterType.IsGenericType);
		}

		public IQueryable<TResult> Project<TResult>(IQueryable source)
		{
			var genericProjectMethod = GetGenericProjectMethod<TResult>(source);

			var result = (IQueryable<TResult>)genericProjectMethod.Invoke(this, new object[] { source });

			return result;
		}

		public IQueryable<TResult> Project<TSource, TResult>(IQueryable<TSource> source)
		{
			var expression = global::AutoMapper.Mapper.Engine.CreateMapExpression<TSource, TResult>();

			return source.Select(expression);
		}

		private MethodInfo GetGenericProjectMethod<TResult>(IQueryable source)
		{
			var sourceElementType = source.ElementType;

			var argumentTypes = new[] { sourceElementType, typeof(TResult) };

			var genericProjectMethod = projectMethod.MakeGenericMethod(argumentTypes);

			return genericProjectMethod;
		}
	}
}