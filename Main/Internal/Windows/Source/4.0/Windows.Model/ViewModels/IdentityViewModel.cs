namespace EyeSoft.Windows.Model
{
	using System;

	public abstract class IdentityViewModel : IdentityViewModel<Guid>
	{
		protected IdentityViewModel()
		{
		}

		protected IdentityViewModel(Guid id) : base(id)
		{
		}
	}
}