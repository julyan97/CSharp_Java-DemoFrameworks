using DBLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFrameWork
{
    public class User
    {
        [MyName("kobra")]
        public int id;
        private string name;
        private string age;

        public User()
        {

        }
        public User(string name, string age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get => name; set => name = value; }
        public string Age { get => age; set => age = value; }
        public int Id { get => id; set => id = value; }
    }
}
