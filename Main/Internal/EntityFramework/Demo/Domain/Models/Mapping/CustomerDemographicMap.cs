namespace EyeSoft.EntityFramework.Caching.Demo.Domain.Mapping
{
	using System.Data.Entity.ModelConfiguration;

	public class CustomerDemographicMap : EntityTypeConfiguration<CustomerDemographic>
	{
		public CustomerDemographicMap()
		{
			// Primary Key
			HasKey(t => t.CustomerTypeID);

			// Properties
			Property(t => t.CustomerTypeID)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(10);

			// Table & Column Mappings
			ToTable("CustomerDemographics");
			Property(t => t.CustomerTypeID).HasColumnName("CustomerTypeID");
			Property(t => t.CustomerDesc).HasColumnName("CustomerDesc");

			// Relationships
			HasMany(t => t.Customers)
				.WithMany(t => t.CustomerDemographics)
				.Map(m =>
					{
						m.ToTable("CustomerCustomerDemo");
						m.MapLeftKey("CustomerTypeID");
						m.MapRightKey("CustomerID");
					});
		}
	}
}