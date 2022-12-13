using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public double Price { get; set; }
        public DateTime PublicTime { get; set; }

    }
}
