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
        public DbSet<User> Users { get; set; } 
        public DbSet<Mark> Marks { get; set; }


    }
}
