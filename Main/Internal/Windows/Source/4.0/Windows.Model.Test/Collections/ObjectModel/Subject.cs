namespace EyeSoft.Windows.Model.Test.ObjectModel
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Windows.Model.Collections;

	internal class Subject : IFilterable
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public IEnumerable<string> Keys
		{
			get
			{
				var filteredKeys = new[] { FirstName, LastName }.FilteredKeys().ToList();

				return filteredKeys;
			}
		}
	}
}