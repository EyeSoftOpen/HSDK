namespace EyeSoft.Core.Reflection
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Extensions;

    public class InstanceBuilder
	{
		private readonly Type type;

		private readonly ConstructorInfo constructorInfo;

		private readonly object[] arguments;

		internal InstanceBuilder(Type type, ConstructorInfo constructorInfo, object[] arguments)
		{
			this.type = type;
			this.constructorInfo = constructorInfo;
			this.arguments = arguments;
		}

		public bool ConstructorFound => constructorInfo != null;

        public T Create<T>() where T : class
		{
			return (T)Create();
		}

		public object Create()
		{
            if (ConstructorFound)
            {
                return constructorInfo.Invoke(arguments);
            }

            var argumentsTypes = arguments.Select(argument => argument.GetType().Name).Join(",");

            const string Message =
                "A constructor for the type '{0}' with the specified parameters ({1}) was not found." + "\r\n" +
                "Call the ConstructorFound " + "property before call this method.";

            throw new InvalidOperationException(string.Format(Message, type.Name, argumentsTypes));

        }
	}
}