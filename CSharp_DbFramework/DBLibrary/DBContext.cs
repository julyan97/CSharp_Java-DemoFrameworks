
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Reflection;

namespace DBLibrary
{
    public abstract class DBContext
    {
        private SqlConnection connection;

        public DBContext(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }
        public virtual void OnCreate()
        {
            connection.Open();
            var currentClass = this.GetType();
            var properties = currentClass.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var pro in properties)
            {

                var curinstance = Activator.CreateInstance(pro.PropertyType, connection );
                var generics = curinstance.GetType().GetGenericArguments()[0];
                Console.WriteLine(generics.Name);
                pro.GetAccessors()[1].Invoke(this, new object[] { curinstance });

                dynamic dynamicInastance = curinstance;
                dynamicInastance.CreateInstanceOfTable();
                //curinstance.GetType().GetMethod("CreateInstanceOfTable").Invoke(curinstance, null);
                Console.WriteLine("-----------");

            }
            connection.Close();

        }

        public void SaveChanges()
        {
            try
            {
                connection.Open();
                var currentClass = this.GetType();
                var properties = currentClass.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (var prop in properties)
                {
                    var instance = prop.GetAccessors()[0].Invoke(this, null);
                    instance.GetType().GetMethod("SaveChanges", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(instance, null);
                    //Console.WriteLine("-----------");

                }
                connection.Close();
            }
            catch { }
        }

        public void DropAllTables()
        {
            var currentClass = this.GetType();
            var properties = currentClass.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var prop in properties)
            {
                dynamic instance = prop.GetAccessors()[0].Invoke(this, null);
                instance.DropTable();
                //instance.GetType().GetMethod("DropTable", BindingFlags.Public | BindingFlags.Instance).Invoke(instance, null);
                //Console.WriteLine("-----------");
            }
        }


    }
}
