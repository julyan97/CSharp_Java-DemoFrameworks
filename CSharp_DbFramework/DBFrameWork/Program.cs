using DBFrameWork.Models;
using DBLibrary;
using DBLibrary.Attributes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DBFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            //To use add a config.json file to the project with your connectionString
            Configurations configurations = new Configurations();

            ApplicationDbContext db = new ApplicationDbContext(configurations["ConnectionString"]);

            var user = new User("jojo", "23");

            db.Users.entities.Add(user);
            db.SaveChanges();



            //var assemblies = Assembly.GetExecutingAssembly().GetTypes();
            //Console.WriteLine(assemblies);
            //assemblies.ToList().ForEach(x => Console.WriteLine(x.Name));
            //var user = new User("jojo", "23");
            //var fields = user.GetType()
            //    .GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //foreach (var field in fields)
            //{
            //    var atrs = field.GetCustomAttributes(false);
            //    atrs.ToList().ForEach(a => Console.WriteLine(a.GetType().Name));
            //}

        }
    }
}
