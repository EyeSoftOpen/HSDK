using System;

namespace EyeSoft.Windows.Model.ViewModels
{
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