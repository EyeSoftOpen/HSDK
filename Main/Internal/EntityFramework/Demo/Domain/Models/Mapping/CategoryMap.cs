namespace EyeSoft.EntityFramework.Caching.Demo.Domain.Mapping
{
	using System.Data.Entity.ModelConfiguration;

	public class CategoryMap : EntityTypeConfiguration<Category>
	{
		public CategoryMap()
		{
			// Primary Key
			HasKey(t => t.CategoryId);

			// Properties
			Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);

			// Table & Column Mappings
			ToTable("Categories");
			Property(t => t.CategoryId).HasColumnName("CategoryID");
			Property(t => t.CategoryName).HasColumnName("CategoryName");
			Property(t => t.Description).HasColumnName("Description");
			Property(t => t.Picture).HasColumnName("Picture");
		}
	}
}