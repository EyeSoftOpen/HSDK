// Copyright © GalaSoft Laurent Bugnion 2009-2012
// </copyright>
// ****************************************************************************
// <author>Laurent Bugnion</author>
// <email>laurent@galasoft.ch</email>
// <date>13.4.2009</date>
// <project>GalaSoft.MvvmLight.Messaging</project>
// <web>http://www.galasoft.ch</web>
// <license>
// See license.txt in this project or http://www.galasoft.ch/license_MIT.txt
// </license>
// ****************************************************************************

namespace EyeSoft.Core.Messanging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class LocalMessageBroker
		: IMessageBroker
	{
		private readonly object registerLock = new object();

		private Dictionary<Type, List<WeakActionAndToken>> recipientsOfSubclassesAction;
		private Dictionary<Type, List<WeakActionAndToken>> recipientsStrictAction;

		public virtual void Register<TMessage>(object recipient, Action<TMessage> action)
		{
			Register(recipient, null, false, action);
		}

		public virtual void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action)
		{
			Register(recipient, null, receiveDerivedMessagesToo, action);
		}

		public void Register<TMessage>(object recipient, object token, bool receiveDerivedMessagesToo, Action<TMessage> action)
		{
			Register(recipient, token, receiveDerivedMessagesToo, action, null);
		}

		public virtual void Register<TMessage>(object recipient, object token, Action<TMessage> action)
		{
			Register(recipient, token, false, action, null);
		}

		public void Register<TMessage>(object recipient, object token, Action<TMessage> action, Predicate<TMessage> condition)
		{
			Register(recipient, token, false, action, condition);
		}

		public virtual void Send<TMessage>(TMessage message)
		{
			SendToTargetOrType(message, null, null);
		}

		public virtual void Send<TMessage, TTarget>(TMessage message)
		{
			SendToTargetOrType(message, typeof(TTarget), null);
		}

		public virtual void Send<TMessage>(TMessage message, object token)
		{
			SendToTargetOrType(message, null, token);
		}

		public virtual void Unregister(object recipient)
		{
			UnregisterFromLists(recipient, recipientsOfSubclassesAction);
			UnregisterFromLists(recipient, recipientsStrictAction);
		}

		public virtual void Unregister<TMessage>(object recipient)
		{
			Unregister<TMessage>(recipient, null, null);
		}

		public virtual void Unregister<TMessage>(object recipient, object token)
		{
			Unregister<TMessage>(recipient, token, null);
		}

		public virtual void Unregister<TMessage>(object recipient, Action<TMessage> action)
		{
			Unregister(recipient, null, action);
		}

		public virtual void Unregister<TMessage>(object recipient, object token, Action<TMessage> action)
		{
			UnregisterFromLists(recipient, token, action, recipientsStrictAction);
			UnregisterFromLists(recipient, token, action, recipientsOfSubclassesAction);
			Cleanup();
		}

		private static void CleanupList(IDictionary<Type, List<WeakActionAndToken>> lists)
		{
			if (lists == null)
			{
				return;
			}

			lock (lists)
			{
				var listsToRemove = new List<Type>();
				foreach (var list in lists)
				{
					var recipientsToRemove =
						list
							.Value
							.Where(item => item.Action == null || !item.Action.IsAlive)
							.ToList();

					foreach (var recipient in recipientsToRemove)
					{
						list.Value.Remove(recipient);
					}

					if (list.Value.Count == 0)
					{
						listsToRemove.Add(list.Key);
					}
				}

				foreach (var key in listsToRemove)
				{
					lists.Remove(key);
				}
			}
		}

		private static bool Implements(Type instanceType, Type interfaceType)
		{
			if (interfaceType == null ||
				instanceType == null)
			{
				return false;
			}

			#if WIN8
			IEnumerable<Type> interfaces = instanceType.GetTypeInfo().ImplementedInterfaces;
			#else
			var interfaces = instanceType.GetInterfaces();
			#endif

			return
				interfaces
				.Any(currentInterface => currentInterface == interfaceType);
		}

		private static void SendToList<TMessage>(
			TMessage message,
			IEnumerable<WeakActionAndToken> collection,
			Type messageTargetType,
			object token)
		{
			if (collection == null)
			{
				return;
			}

			var listClone = collection.ToList();

			listClone =
				listClone
					.Take(listClone.Count())
					.Where(item => (item.Condition == null) || item.Condition.Convert<Predicate<TMessage>>().Invoke(message))
					.ToList();

			foreach (var item in listClone)
			{
				var executeAction = item.Action as IExecuteWithObject;

				if (executeAction != null &&
					item.Action.IsAlive &&
					item.Action.Target != null &&
					(messageTargetType == null ||
					item.Action.Target.GetType() == messageTargetType ||
					Implements(item.Action.Target.GetType(), messageTargetType))
					&& (item.Token == null && (token == null) ||
					item.Token != null && item.Token.Equals(token)))
				{
					executeAction.ExecuteWithObject(message);
				}
			}
		}

		private static void UnregisterFromLists(object recipient, Dictionary<Type, List<WeakActionAndToken>> lists)
		{
			if (recipient == null
				|| lists == null
				|| lists.Count == 0)
			{
				return;
			}

			lock (lists)
			{
				foreach (var messageType in lists.Keys)
				{
					foreach (var item in lists[messageType])
					{
						var weakAction = item.Action;

						if (weakAction != null &&
							recipient == weakAction.Target)
						{
							weakAction.MarkForDeletion();
						}
					}
				}
			}
		}

		private static void UnregisterFromLists<TMessage>(
			object recipient,
			object token,
			Action<TMessage> action,
			Dictionary<Type, List<WeakActionAndToken>> lists)
		{
			var messageType = typeof(TMessage);

			if (recipient == null ||
				lists == null ||
				lists.Count == 0 ||
				!lists.ContainsKey(messageType))
			{
				return;
			}

			lock (lists)
			{
				foreach (var item in lists[messageType])
				{
					var weakActionCasted = item.Action as WeakAction<TMessage>;

					if (weakActionCasted != null &&
						recipient == weakActionCasted.Target &&
						(action == null || action == weakActionCasted.Action) &&
						(token == null || token.Equals(item.Token)))
					{
						item.Action.MarkForDeletion();
					}
				}
			}
		}

		private void Register<TMessage>(
			object recipient,
			object token,
			bool receiveDerivedMessagesToo,
			Action<TMessage> action,
			Predicate<TMessage> condition)
		{
			lock (registerLock)
			{
				var messageType = typeof(TMessage);

				Dictionary<Type, List<WeakActionAndToken>> recipients;

				if (receiveDerivedMessagesToo)
				{
					if (recipientsOfSubclassesAction == null)
					{
						recipientsOfSubclassesAction = new Dictionary<Type, List<WeakActionAndToken>>();
					}

					recipients = recipientsOfSubclassesAction;
				}
				else
				{
					if (recipientsStrictAction == null)
					{
						recipientsStrictAction = new Dictionary<Type, List<WeakActionAndToken>>();
					}

					recipients = recipientsStrictAction;
				}

				lock (recipients)
				{
					List<WeakActionAndToken> list;

					if (!recipients.ContainsKey(messageType))
					{
						list = new List<WeakActionAndToken>();
						recipients.Add(messageType, list);
					}
					else
					{
						list = recipients[messageType];
					}

					var weakAction = new WeakAction<TMessage>(recipient, action);

					var item =
						new WeakActionAndToken
						{
							Action = weakAction,
							Token = token,
							Condition = condition
						};

					list.Add(item);
				}
			}

			Cleanup();
		}

		private void Cleanup()
		{
			CleanupList(recipientsOfSubclassesAction);
			CleanupList(recipientsStrictAction);
		}

		private void SendToTargetOrType<TMessage>(TMessage message, Type messageTargetType, object token)
		{
			var messageType = message.GetType();

			if (recipientsOfSubclassesAction != null)
			{
				var listClone =
				recipientsOfSubclassesAction.Keys.Take(recipientsOfSubclassesAction.Count()).ToList();

				foreach (var type in listClone)
				{
					List<WeakActionAndToken> list = null;

#if WIN8
					if (messageType == type
					|| type.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo())
					|| Implements(messageType, type))
					{
					lock (recipientsOfSubclassesAction)
					{
						list = recipientsOfSubclassesAction[type].Take(_recipientsOfSubclassesAction[type].Count()).ToList();
					}
					}
#else
					if (messageType == type ||
						messageType.IsSubclassOf(type) ||
						Implements(messageType, type))
					{
						lock (recipientsOfSubclassesAction)
						{
							list = recipientsOfSubclassesAction[type].Take(recipientsOfSubclassesAction[type].Count()).ToList();
						}
					}
#endif

					SendToList(message, list, messageTargetType, token);
				}
			}

			if (recipientsStrictAction != null)
			{
				if (recipientsStrictAction.ContainsKey(messageType))
				{
					List<WeakActionAndToken> list;

					lock (recipientsStrictAction)
					{
						list = recipientsStrictAction[messageType].Take(recipientsStrictAction[messageType].Count()).ToList();
					}

					SendToList(message, list, messageTargetType, token);
				}
			}

			Cleanup();
		}

		private struct WeakActionAndToken
		{
			public WeakAction Action { get; set; }

			public object Token { get; set; }

			public object Condition { get; set; }
		}
	}
}