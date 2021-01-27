namespace EyeSoft.Windows.Model.ViewModels
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