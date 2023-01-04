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

            //builder.HasMany<Comment>().WithOne(x => x.Article);//在Article端配置导航属性
            //builder.HasQueryFilter(x => !x.IsDelete); //设置全局筛选器
            //builder.Property("IsDelete").IsConcurrencyToken();//设置并发令牌
            builder.Property("RowVersion").IsRowVersion();

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
