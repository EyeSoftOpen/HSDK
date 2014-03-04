namespace EyeSoft.EntityFramework.Caching.Demo.Domain.Mapping
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.ModelConfiguration;

	public class RegionMap : EntityTypeConfiguration<Region>
	{
		public RegionMap()
		{
			// Primary Key
			HasKey(t => t.RegionID);

			// Properties
			Property(t => t.RegionID)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			Property(t => t.RegionDescription)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(50);

			// Table & Column Mappings
			ToTable("Region");
			Property(t => t.RegionID).HasColumnName("RegionID");
			Property(t => t.RegionDescription).HasColumnName("RegionDescription");
		}
	}
}