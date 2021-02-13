namespace EyeSoft
{
    using System;

    [Serializable]
	public class ComponentResolutionException : Exception
	{
		public ComponentResolutionException(Exception exception)
			: this("Component resolution exception. See the inner exception for defails.", exception)
		{
		}

		public ComponentResolutionException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}
}