using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Infrastracture.Config
{
    public class UserConfig : IEntityTypeConfiguration<User.Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.ToTable("T_Users");
            builder.OwnsOne(x => x.PhoneNumber, z =>
            {
                z.Property(c => c.RegionCode).HasMaxLength(20).IsUnicode(false);
            });
            builder.HasOne(x => x.UserAccessFail).WithOne(f => f.User).HasForeignKey<UserAccessFail>(f=>f.UserId);
            builder.Property("PasswordHash").HasMaxLength(100);

        }
    }
}
