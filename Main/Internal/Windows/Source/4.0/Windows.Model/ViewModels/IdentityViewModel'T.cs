using System;

namespace EyeSoft.Windows.Model.ViewModels
{
	public abstract class IdentityViewModel<T> : AutoRegisterViewModel, IIdentityViewModel<T>
		where T : IComparable
	{
		private T id;

		protected IdentityViewModel()
		{
		}

		protected IdentityViewModel(T id)
		{
			Id = id;
		}

		public T Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
				OnPropertyChanged("Id");
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (IdentityViewModel<T>)obj;
			return id.Equals(other.id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}