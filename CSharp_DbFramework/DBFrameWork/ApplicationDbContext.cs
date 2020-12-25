using DBFrameWork.Models;
using DBLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBFrameWork
{
    class ApplicationDbContext : DBContext
    {

        
        public ApplicationDbContext(string connectionString) : base(connectionString)
        {
                base.OnCreate();
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Mark> Marks { get; set; }


    }
}
