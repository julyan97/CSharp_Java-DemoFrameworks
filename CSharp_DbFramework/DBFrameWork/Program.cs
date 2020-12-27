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
            var db = new ApplicationDbContext();

            var user = new User("jake", "23");
            var user1 = new User("jojo", "32"); 
            var user2= new User("jojo", "58");
            var user3 = new User("jojo", "63");

            
            var en = db.Users.entities;
            en.Add(new User("Jake", "12222"));
            en.RemoveAt(0);
            en[0].Name = "GoshoDosho";
            Console.WriteLine("en: " +en.Count+" ");



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
