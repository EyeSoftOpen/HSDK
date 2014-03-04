namespace EyeSoft.EntityFramework.Caching.Demo.Domain.Mapping
{
	using System.Data.Entity.ModelConfiguration;

	public class ShipperMap : EntityTypeConfiguration<Shipper>
	{
		public ShipperMap()
		{
			// Primary Key
			HasKey(t => t.ShipperId);

			// Properties
			Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);

			Property(t => t.Phone)
				.HasMaxLength(24);

			// Table & Column Mappings
			ToTable("Shippers");
			Property(t => t.ShipperId).HasColumnName("ShipperID");
			Property(t => t.CompanyName).HasColumnName("CompanyName");
			Property(t => t.Phone).HasColumnName("Phone");
		}
	}
}