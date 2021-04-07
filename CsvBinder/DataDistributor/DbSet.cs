using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    public class DbSet<T> : List<T> where T : DataDistributor
    {
        public DbSet()
        {
            var list = new List<string>() { "1,r,t,y", "2,r,t,y" };
            CreateSet(list);
        }
        public DbSet(string file, bool hasHeader = false, bool linearBinding = false, string delim = ", ")
        {
            CreateSet(file, hasHeader, linearBinding, delim);
        }

        private void CreateSet(string file, bool hasHeader = false, bool linearBinding = false, string delim = ", ")//ToDo add linearBinding
        {
            var list = File.ReadLines(file).ToList();
            var header = list.First();
            list = list.Skip(1).ToList();

            foreach (var line in list)
            {
                dynamic obj = Activator.CreateInstance<T>();
                try
                {
                    if (!hasHeader)
                    {
                        if (linearBinding)
                        {
                            obj.CreateInstance(line, hasHeader, linearBinding, delim);
                            Add((T)obj);

                        }
                        else
                        {
                            obj.CreateInstance(line);
                            Add((T)obj);
                        }
                    }
                    else
                    {
                        obj.CreateInstance(line, hasHeader, linearBinding, header.Split(", "));
                        Add((T)obj);
                    }
                }
                catch (Exception)
                {

                    throw new Exception("Your generic class needs to inherite DataDistributor to work");
                }
            }
        }

        private void CreateSet(List<string> list)
        {
            foreach (var line in list)
            {
                dynamic obj = Activator.CreateInstance<T>();
                try
                {
                    obj.CreateInstance(line);
                    Add((T)obj);
                }
                catch (Exception)
                {

                    throw new Exception("Your generic class needs to inherite DataDistributor to work");
                }
            }
        }
    }
}
