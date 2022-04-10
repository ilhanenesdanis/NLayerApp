using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category() { CreateDate = DateTime.Now ,Id=1,Name="Bilgisayar",UpdateDate=DateTime.Now},
                new Category() { CreateDate = DateTime.Now, Id = 2, Name = "Mönitörler", UpdateDate = DateTime.Now },
                new Category() { CreateDate = DateTime.Now, Id = 3, Name = "Klavyeler", UpdateDate = DateTime.Now });
        }
    }
}
