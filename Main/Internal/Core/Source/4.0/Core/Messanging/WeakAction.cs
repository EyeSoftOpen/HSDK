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

namespace EyeSoft.Messanging
{
    using System;

    public class WeakAction
	{
		private readonly Action action;

		private WeakReference reference;

		/// <summary>
		/// Initializes a new instance of the <see cref="WeakAction" /> class.
		/// </summary>
		/// <param name="target">The action's owner.</param>
		/// <param name="action">The action that will be associated to this instance.</param>
		public WeakAction(object target, Action action)
		{
			reference = new WeakReference(target);
			this.action = action;
		}

		/// <summary>
		/// Gets the Action associated to this instance.
		/// </summary>
		public Action Action => action;

        /// <summary>
		/// Gets a value indicating whether the Action's owner is still alive, or if it was collected
		/// by the Garbage Collector already.
		/// </summary>
		public bool IsAlive => reference != null && reference.IsAlive;

        /// <summary>
		/// Gets the Action's owner. This object is stored as a <see cref="WeakReference" />.
		/// </summary>
		public object Target => reference == null ? null : reference.Target;

        /// <summary>
		/// Executes the action. This only happens if the action's owner
		/// is still alive.
		/// </summary>
		public void Execute()
		{
			if (action != null && IsAlive)
			{
				action();
			}
		}

		/// <summary>
		/// Sets the reference that this instance stores to null.
		/// </summary>
		public void MarkForDeletion()
		{
			reference = null;
		}
	}
}