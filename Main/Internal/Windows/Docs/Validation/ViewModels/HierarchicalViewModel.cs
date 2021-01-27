namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
	using System.Collections.Generic;
    using Core.Logging;
    using Core.Timers;
    using Core.Validation;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.ViewModels;
    using Validators;

    public class HierarchicalViewModel : ViewModel
	{
		private readonly ITimer timer;

		public HierarchicalViewModel()
		{
			Subject = new SubjectViewModel();

			timer = TimerFactory.Start(500, () => OnPropertyChanged("IsValid"));
		}

		public SubjectViewModel Subject
		{
			get => GetProperty<SubjectViewModel>();
            set => SetProperty(value);
        }

		public override bool IsValid
		{
			get
			{
				Logger.Write("Is valid");
				return base.IsValid;
			}
		}

		public override IEnumerable<ValidationError> Validate()
		{
			return new HierarchicalViewModelValidator().Validate(this);
		}

		protected override void Dispose(bool disposing)
		{
			timer?.Dispose();
			base.Dispose(disposing);
		}
	}
}