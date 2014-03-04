namespace EyeSoft.EntityFramework.Caching.Demo.Domain.Mapping
{
	using System.Data.Entity.ModelConfiguration;

	public class OrderMap : EntityTypeConfiguration<Order>
	{
		public OrderMap()
		{
			// Primary Key
			HasKey(t => t.OrderId);

			// Properties
			Property(t => t.CustomerId)
				.IsFixedLength()
				.HasMaxLength(5);

			Property(t => t.ShipName)
				.HasMaxLength(40);

			Property(t => t.ShipAddress)
				.HasMaxLength(60);

			Property(t => t.ShipCity)
				.HasMaxLength(15);

			Property(t => t.ShipRegion)
				.HasMaxLength(15);

			Property(t => t.ShipPostalCode)
				.HasMaxLength(10);

			Property(t => t.ShipCountry)
				.HasMaxLength(15);

			// Table & Column Mappings
			ToTable("Orders");
			Property(t => t.OrderId).HasColumnName("OrderID");
			Property(t => t.CustomerId).HasColumnName("CustomerID");
			Property(t => t.EmployeeId).HasColumnName("EmployeeID");
			Property(t => t.OrderDate).HasColumnName("OrderDate");
			Property(t => t.RequiredDate).HasColumnName("RequiredDate");
			Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			Property(t => t.ShipVia).HasColumnName("ShipVia");
			Property(t => t.Freight).HasColumnName("Freight");
			Property(t => t.ShipName).HasColumnName("ShipName");
			Property(t => t.ShipAddress).HasColumnName("ShipAddress");
			Property(t => t.ShipCity).HasColumnName("ShipCity");
			Property(t => t.ShipRegion).HasColumnName("ShipRegion");
			Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
			Property(t => t.ShipCountry).HasColumnName("ShipCountry");

			// Relationships
			HasOptional(t => t.Customer)
				.WithMany(t => t.Orders)
				.HasForeignKey(d => d.CustomerId);

			HasOptional(t => t.Employee)
				.WithMany(t => t.Orders)
				.HasForeignKey(d => d.EmployeeId);

			HasOptional(t => t.Shipper)
				.WithMany(t => t.Orders)
				.HasForeignKey(d => d.ShipVia);
		}
	}
}