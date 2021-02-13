namespace EyeSoft.Messanging
{
	public abstract class Message<T>
	{
		protected Message(T sender)
		{
			Sender = sender;
		}

		public T Sender { get; }
	}
}