namespace EyeSoft.Core.Extensions
{
	internal class ObjectExtender<T> : IObjectExtender<T>
	{
		private readonly T obj;

		public ObjectExtender(T obj)
		{
			this.obj = obj;
		}

		public T Instance => obj;
    }
}