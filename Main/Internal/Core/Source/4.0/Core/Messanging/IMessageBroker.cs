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

    public interface IMessageBroker
	{
		void Register<TMessage>(object recipient, Action<TMessage> action);

		void Register<TMessage>(object recipient, object token, Action<TMessage> action);

		void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action);

		void Register<TMessage>(object recipient, object token, bool receiveDerivedMessagesToo, Action<TMessage> action);

		void Register<TMessage>(object recipient, object token, Action<TMessage> action, Predicate<TMessage> condition);

		void Send<TMessage>(TMessage message);

		void Send<TMessage, TTarget>(TMessage message);

		void Send<TMessage>(TMessage message, object token);

		void Unregister(object recipient);

		void Unregister<TMessage>(object recipient);

		void Unregister<TMessage>(object recipient, object token);

		void Unregister<TMessage>(object recipient, Action<TMessage> action);

		void Unregister<TMessage>(object recipient, object token, Action<TMessage> action);
	}
}