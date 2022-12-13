using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //在Article端配置
            //builder.HasMany<Comment>().WithOne(x => x.Article);
        }
    }


    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //在comment设置一对多的关系
            builder.HasOne(x => x.Article).WithMany(a => a.Comments).IsRequired();
        }
    }
}
