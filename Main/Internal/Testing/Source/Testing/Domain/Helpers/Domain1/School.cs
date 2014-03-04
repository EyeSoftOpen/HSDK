namespace EyeSoft.Testing.Domain.Helpers.Domain1
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Domain;

	public class School
		: Aggregate
	{
		protected internal School()
		{
		}

		public virtual string Name { get; protected internal set; }

		public virtual ReadOnlyObservableCollection<Child> Children
		{
			get { return ChildList.ToReadOnlyObservableCollection(); }
		}

		public virtual IList<Child> ChildList
		{
			get;
			protected set;
		}

		public virtual void AddChild(string firstName)
		{
			if (ChildList == null)
			{
				ChildList = new List<Child>();
			}

			ChildList.Add(Child.Create(this, firstName));
		}
	}
}