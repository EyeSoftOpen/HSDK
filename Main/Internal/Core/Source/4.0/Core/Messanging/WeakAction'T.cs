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

    /// <summary>
	/// Stores an Action without causing a hard reference
	/// to be created to the Action's owner. The owner can be garbage collected at any time.
	/// </summary>
	/// <typeparam name="T">The type of the Action's parameter.</typeparam>
	public class WeakAction<T> : WeakAction, IExecuteWithObject
	{
		private readonly Action<T> action;

		/// <summary>
		/// Initializes a new instance of the WeakAction class.
		/// </summary>
		/// <param name="target">The action's owner.</param>
		/// <param name="action">The action that will be associated to this instance.</param>
		public WeakAction(object target, Action<T> action)
			: base(target, null)
		{
			this.action = action;
		}

		/// <summary>
		/// Gets the Action associated to this instance.
		/// </summary>
		public new Action<T> Action
		{
			get
			{
				return action;
			}
		}

		/// <summary>
		/// Executes the action. This only happens if the action's owner
		/// is still alive. The action's parameter is set to default(T).
		/// </summary>
		public new void Execute()
		{
			if (action != null && IsAlive)
			{
				action(default(T));
			}
		}

		/// <summary>
		/// Executes the action. This only happens if the action's owner
		/// is still alive.
		/// </summary>
		/// <param name="parameter">A parameter to be passed to the action.</param>
		public void Execute(T parameter)
		{
			if (action != null
				&& IsAlive)
			{
				action(parameter);
			}
		}

		/// <summary>
		/// Executes the action with a parameter of type object. This parameter
		/// will be casted to T. This method implements <see cref="IExecuteWithObject.ExecuteWithObject" />
		/// and can be useful if you store multiple WeakAction{T} instances but don't know in advance
		/// what type T represents.
		/// </summary>
		/// <param name="parameter">The parameter that will be passed to the action after
		/// being casted to T.</param>
		public void ExecuteWithObject(object parameter)
		{
			var parameterCasted = (T)parameter;
			Execute(parameterCasted);
		}
	}
}