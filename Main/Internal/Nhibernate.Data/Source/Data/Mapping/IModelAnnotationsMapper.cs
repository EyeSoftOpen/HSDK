namespace EyeSoft.Data.Nhibernate.Mapping
{
    using Core.Mapping.Data;
    using NHibernate.Cfg.MappingSchema;

	public interface IModelAnnotationsMapper
	{
		HbmMapping CompileMapping();

		IModelAnnotationsMapper Map<T>(MappingStrategy strategy = MappingStrategy.TablePerType)
			where T : class;
	}
}