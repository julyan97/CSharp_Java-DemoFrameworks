using DataCollector;
using System;

namespace CsvBinder
{
    public class Program
    {
        public class Employee : DataDistributor // to have your data binded to the file you need to inherit DataDistributor class
        {
            public string Id { get; set; }
            public string Id2 { get; set; }
            public string Date { get; set; }
            public string Date2 { get; set; }

        }
        static void Main(string[] args)
        {
            DbSet<Employee> datas = new DbSet<Employee>();
            DbSet<Employee> employees = new DbSet<Employee>("file.txt");// binds data to an ID prop and a arr prop of strings
            DbSet<Employee> employees2 = new DbSet<Employee>("file.txt", true);//binds the props that have the same name as the header to the exact column
            DbSet<Employee> employees3 = new DbSet<Employee>("file.txt", false, true);// binds the props lineary to the headers

        }
    }
}
