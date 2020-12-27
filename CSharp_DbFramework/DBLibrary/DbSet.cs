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
            var command = new SqlCommand($"DROP TABLE {typeof(TEntity).Name}s", connection).ExecuteNonQuery();
            connection.Close();
        }
        private void SaveChanges()
        {
            AddEntitiesToDataBase();
            DeleteEntitiesFromDataBase();
            UpdateEntitiesFromDataBase();
        }

        private void AddEntitiesToDataBase()
        {
            // INSERT INTO table_name (column1, column2, column3, ...)
            // VALUES(value1, value2, value3, ...);
            try
            {
                Func<TEntity, bool> func = (x) => ((dynamic)x).Id == 0;
                var list = entities.Where(func).ToList();

                for (int x = 0; x < list.Count; x++)
                {
                    var cur = list[x];
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
                }

            }
            catch { }
        }
        private void DeleteEntitiesFromDataBase()
        {
            //DELETE FROM Customers WHERE CustomerName = 'Alfreds Futterkiste';

            Func<TEntity, bool> func = (x) => ((dynamic)x).Id == 0;
            List<int> checkListId = checkList.Select(x => (int)((dynamic)x).Id).ToList();
            var listOfIdiesToRemove = new List<int>();
            foreach (var cur in checkListId)
            {
                //var test = entities.Where(x => ((dynamic)x).Id == cur && ((dynamic)x).Id != 0).ToList();
                if (entities.Any(x => ((dynamic)x).Id == cur && ((dynamic)x).Id != 0))
                {
                    continue;
                }
                listOfIdiesToRemove.Add(cur);
            }
            //TODO: check all idies if they exist if bot delete them from the database
            var list = entities.Where(func).ToList();

            for (int x = 0; x < listOfIdiesToRemove.Count; x++)
            {
                var cur = listOfIdiesToRemove[x];
                var type = typeof(TEntity);

                string query = $"DELETE FROM {typeof(TEntity).Name}s where Id = '{cur}'; ";
                Console.WriteLine(query);
                var command = new SqlCommand(query, connection).ExecuteNonQuery();
            }

        }
        private void UpdateEntitiesFromDataBase()
        {
            //UPDATE Customers
            //SET ContactName = 'Alfred Schmidt', City = 'Frankfurt'
            //WHERE CustomerID = 1;

            try
            {
                Func<TEntity,TEntity, int> compare = (x, y) => ((dynamic)x).Id.CompareTo(((dynamic)x).Id);
                bool same = true;

                for (int x = 0; x < entities.Count; x++)
                {
                    var cur = entities[x];
                    var type = typeof(TEntity);
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    

                    int curId = (int)fields[0].GetValue(cur);
                    var curEntityToCheck = checkList.FirstOrDefault(x => ((dynamic)x).Id == curId);
                    for (int i = 0; i < fields.Length; i++)
                    {
                        var field = fields[i];
                        if (field.GetValue(curEntityToCheck) != field.GetValue(cur))
                        {
                            same = false;
                            break;
                        }
                    }
                    if(same == false)
                    {
                        string query = $"Update {typeof(TEntity).Name}s Set ";
                        for (int i = 1; i < fields.Length; i++)
                        {
                            var FieldName = fields[i].Name;
                            FieldName = char.ToUpper(FieldName[0]) + FieldName.Substring(1);
                            var fieldValue = fields[i].GetValue(cur);

                            query += $"{FieldName} = '{fieldValue}',";
                        }
                        query = query.Remove(query.Length - 1);
                        query += $" where Id = {curId}";
                        var command = new SqlCommand(query, connection).ExecuteNonQuery();
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
                //FillLists();

            }
            catch (Exception ex)
            {
                //FillLists();
                //Console.WriteLine(ex.Message);
            }
        }

        private void FillLists()
        {
            try
            {
                var command = new SqlCommand($"Select * from {typeof(TEntity).Name}s", connection).ExecuteReader();
                while (command.Read())
                {
                    //Console.WriteLine(command);
                    var entry = Activator.CreateInstance<TEntity>();
                    var type = entry.GetType();
                    var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    var field = fields[0];
                    //Console.WriteLine($"{field} {command[field.Name]}");
                    field.SetValue(entry, command[field.Name]);
                    for (int i = 1; i < fields.Length; i++)
                    {
                        field = fields[i];
                        // Console.WriteLine($"{field} {command[field.Name]}");
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
