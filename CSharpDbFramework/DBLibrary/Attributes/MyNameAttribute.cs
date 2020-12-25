using System;
using System.Collections.Generic;
using System.Text;

namespace DBLibrary.Attributes
{
    public class MyNameAttribute : Attribute
    {
        public MyNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
