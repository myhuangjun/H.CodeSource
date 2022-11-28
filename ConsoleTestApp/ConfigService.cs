using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    public class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    internal class ConfigService
    {
        /// <summary>
        /// IOptionsSnapshot--在同一范围类保持一致   IOtions<T>  不会读取新的值
        /// </summary>
        private readonly IOptionsSnapshot<Student> optionsSnapshot;

        public ConfigService(IOptionsSnapshot<Student> optionsSnapshot)
        {
            this.optionsSnapshot = optionsSnapshot;
        }

        public void Test()
        {
            Console.WriteLine($"{optionsSnapshot.Value.Name}********{optionsSnapshot.Value.Id}");
        }
    }
}
