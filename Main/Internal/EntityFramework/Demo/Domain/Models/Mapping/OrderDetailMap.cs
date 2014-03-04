namespace EyeSoft.EntityFramework.Caching.Demo.Domain.Mapping
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.ModelConfiguration;

	public class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
	{
		public OrderDetailMap()
		{
			// Primary Key
			HasKey(t => new { t.OrderId, t.ProductId });

			// Properties
			Property(t => t.OrderId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			Property(t => t.ProductId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			// Table & Column Mappings
			ToTable("Order Details");
			Property(t => t.OrderId).HasColumnName("OrderID");
			Property(t => t.ProductId).HasColumnName("ProductID");
			Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			Property(t => t.Quantity).HasColumnName("Quantity");
			Property(t => t.Discount).HasColumnName("Discount");

			// Relationships
			HasRequired(t => t.Order)
				.WithMany(t => t.OrderDetails)
				.HasForeignKey(d => d.OrderId);

			HasRequired(t => t.Product)
				.WithMany(t => t.OrderDetails)
				.HasForeignKey(d => d.ProductId);
		}
	}
}