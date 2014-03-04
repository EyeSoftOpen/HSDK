namespace EyeSoft.Data.Common
{
	using System;

	public interface IDatabaseSchema
		: IDisposable
	{
		string Drop();

		string Update();

		string Create();

		void Validate();
	}
}