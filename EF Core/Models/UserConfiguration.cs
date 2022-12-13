using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            //单向导航  显示指定外键并加上.OnDelete(DeleteBehavior.Restrict)
            builder.HasOne<User>(x => x.AuditUser).WithMany().HasForeignKey(x=>x.AuditUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>(x => x.OperUser).WithMany().HasForeignKey(x=>x.OperUserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
