namespace EyeSoft.Data.Nhibernate
{
	using System.Collections.Generic;
	using System.Diagnostics;
    using Collections.Concurrent;
    using NHibernate;
	using NHibernate.Impl;

	[DebuggerDisplay("{SessionDictionary.Count}")]
	public static class NhibernateSessionTracker
	{
		private static readonly IDictionary<string, string> sessionDictionary = new SafeConcurrentDictionary<string, string>();

		public static IEnumerable<KeyValuePair<string, string>> OpenedSessions => sessionDictionary;

        public static bool IsEmpty => sessionDictionary.Count == 0;

        public static bool Enabled
		{
			get; set;
		}

		[Conditional("DEBUG")]
		internal static void Open(ISession session)
		{
			if (!Enabled)
			{
				return;
			}

			var stackFrame = new StackTrace().ToString();
			sessionDictionary.Add(session.SessionId(), stackFrame);
		}

		[Conditional("DEBUG")]
		internal static void Remove(ISession session)
		{
			if (!Enabled)
			{
				return;
			}

			sessionDictionary.Remove(session.SessionId());
		}

		private static string SessionId(this ISession session)
		{
			var sessionImplementation = (AbstractSessionImpl)session;
			var sessionId = sessionImplementation.SessionId.ToString();

			return sessionId;
		}
	}
}