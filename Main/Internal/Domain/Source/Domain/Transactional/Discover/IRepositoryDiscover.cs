namespace EyeSoft.Domain.Transactional.Discover
{
	using System;

	public interface IRepositoryDiscover
	{
		Type GetEntityTypeIfRepository(Type type);
	}
}