namespace EyeSoft.Core.Diagnostic
{
	public class DefaultDebugger : IDebugger
	{
		private readonly bool? isAttached;

		private readonly bool? isLogging;

		public DefaultDebugger(bool? isAttached = null, bool? isLogging = null)
		{
			this.isAttached = isAttached;
			this.isLogging = isLogging;
		}

		public virtual bool IsAttached
		{
			get { return isAttached.HasValue ? isAttached.Value : System.Diagnostics.Debugger.IsAttached; }
		}

		public virtual bool IsLogging()
		{
			return isLogging.HasValue ? isLogging.Value : System.Diagnostics.Debugger.IsLogging();
		}
	}
}