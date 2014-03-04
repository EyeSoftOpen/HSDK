namespace EyeSoft.EntityFramework.Caching.Demo.Domain.Mapping
{
	using System.Data.Entity.ModelConfiguration;

	public class TerritoryMap : EntityTypeConfiguration<Territory>
	{
		public TerritoryMap()
		{
			// Primary Key
			HasKey(t => t.TerritoryId);

			// Properties
			Property(t => t.TerritoryId)
				.IsRequired()
				.HasMaxLength(20);

			Property(t => t.TerritoryDescription)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(50);

			// Table & Column Mappings
			ToTable("Territories");
			Property(t => t.TerritoryId).HasColumnName("TerritoryID");
			Property(t => t.TerritoryDescription).HasColumnName("TerritoryDescription");
			Property(t => t.RegionId).HasColumnName("RegionID");

			// Relationships
			HasRequired(t => t.Region)
				.WithMany(t => t.Territories)
				.HasForeignKey(d => d.RegionId);
		}
	}
}