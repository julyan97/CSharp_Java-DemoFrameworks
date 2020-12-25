using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DBLibrary
{
    public class DbSet<TEntity> 
    {
        public List<TEntity> entities;
        private List<TEntity> checkList;
        private SqlConnection connection;




        public DbSet(SqlConnection connection)
        {
            this.connection = connection;
            entities = new List<TEntity>();
            FillLists();

        }

        public void DropTable()
        {
            connection.Open();
            var command = new SqlCommand($"DROP TABLE {typeof(TEntity).Name}s",connection).ExecuteNonQuery();
            connection.Close();
        }
        private void SaveChanges()
        {
            try
            {
                bool added = entities.Count > checkList.Count ? true : false;
                if (true)
                {
                    int counter = 0;
                    while (true)
                    {

                        var index = entities.Count - 1 - counter;
                        if (index < 0) break;
                        var cur = entities[index];

                        // INSERT INTO table_name (column1, column2, column3, ...)
                        // VALUES(value1, value2, value3, ...);
                        if (!checkList.Contains(cur))
                        {
                            var type = typeof(TEntity);
                            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                            var fieldTemp = fields[0].Name.ToCharArray();
                            Console.WriteLine();
                            string query = $"insert into {typeof(TEntity).Name}s ( ";

                            for (int i = 1; i < fields.Length; i++)
                            {
                                var field = fields[i];
                                query += $"{char.ToUpper(field.Name[0]) + field.Name.Substring(1)},";
                                //query += $",'{(string)field.GetValue(cur)}'";
                            }
                            query = query.Remove(query.Length - 1);
                            query += ") VALUES (";
                            for (int i = 1; i < fields.Length; i++)
                            {
                                var field = fields[i];
                                query += $"'{(string)field.GetValue(cur)}',";
                            }
                            query = query.Remove(query.Length - 1);
                            query += ");";
                            var command = new SqlCommand(query, connection).ExecuteNonQuery();


                            counter++;
                        }
                        else break;
                    }
                }
            }
            catch { }
        }
        public virtual void CreateInstanceOfTable()
        {
            var type = typeof(TEntity);
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var query = $"CREATE TABLE {type.Name}s (";

            var cur = fields[0];
            var fieldNameCapitalized = cur.Name.ToCharArray();
            fieldNameCapitalized[0] = char.ToUpper(fieldNameCapitalized[0]);
            query += "Id int IDENTITY(1,1) PRIMARY KEY";
            Console.WriteLine(fieldNameCapitalized);

            for (int i = 1; i < fields.Length; i++)
            {
                cur = fields[i];
                fieldNameCapitalized = cur.Name.ToCharArray();
                fieldNameCapitalized[0] = char.ToUpper(fieldNameCapitalized[0]);
                query += $",{new string(fieldNameCapitalized)} varchar(255)";
            }
            query += ");";
            Console.WriteLine(query);

            try
            {
                var command = new SqlCommand(query, connection).ExecuteNonQuery();
                Console.WriteLine("");
                FillLists();

            }
            catch(Exception ex)
            {
                FillLists();
                Console.WriteLine(ex.Message);
            }
        }

        private void FillLists()
        {
            try
            {
                var command = new SqlCommand($"Select * from {typeof(TEntity).Name}s", connection).ExecuteReader();
                while (command.Read())
                {
                    Console.WriteLine(command);
                    var entry = Activator.CreateInstance<TEntity>();
                    var type = entry.GetType();
                    var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    var field = fields[0];
                    Console.WriteLine($"{field} {command[field.Name]}");
                    field.SetValue(entry, command[field.Name]);
                    for (int i = 1; i < fields.Length; i++)
                    {
                        field = fields[i];
                        Console.WriteLine($"{field} {command[field.Name]}");
                        field.SetValue(entry, command[field.Name]);

                    }
                    entities.Add(entry);
                }
                checkList = new List<TEntity>(entities);
            }
            catch { }
        }
    }
}
