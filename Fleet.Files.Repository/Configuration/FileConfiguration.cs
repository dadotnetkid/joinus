using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Files.Repository.Configuration
{
    public class FileConfiguration: IEntityTypeConfiguration<Entities.Files>
    {
        public void Configure(EntityTypeBuilder<Entities.Files> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
