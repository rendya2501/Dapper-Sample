using Dapper;
using DapperSample.Entity;
using System;
using System.Data.SqlClient;

namespace DapperSample
{
    internal class CRUDSample
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
        {
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            AttachDBFilename = System.IO.Path.GetFullPath(@"..\..\Database1.mdf"),
            IntegratedSecurity = true,
        };

        /// <summary>
        /// 実行
        /// </summary>
        public void Execute()
        {
            Insert();
            Read();
            Update();
            Read();
            Delete();
            Read();
        }

        /// <summary>
        /// Insertサンプル
        /// </summary>
        private void Insert()
        {
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = "INSERT INTO [Person] VALUES (@ID,@Name)";
                        var person = new Person() { ID = 4, Name = "Test" };
                        var result = connection.Execute(query, person, transaction);
                        Console.WriteLine(result);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Updateサンプル
        /// </summary>
        private void Update()
        {
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = "UPDATE [Person] SET [Name] = @Name WHERE [ID] = @ID";
                        var person = new Person() { ID = 4, Name = "Updated" };
                        var result = connection.Execute(query, person, transaction);
                        Console.WriteLine(result);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Deleteサンプル
        /// </summary>
        private void Delete()
        {
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = "DELETE FROM [Person] WHERE ID = @ID";
                        var result = connection.Execute(query, new { ID = 4 }, transaction);
                        Console.WriteLine(result);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Selectサンプル
        /// </summary>
        private void Read()
        {
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                // 接続
                connection.Open();

                // クエリ
                var query = "SELECT * FROM [Person]";

                // SQLの発行とデータのマッピング
                // 取得データは IEnumerable<Person> 型
                var result = connection.Query<Person>(query);

                foreach (var p in result)
                {
                    Console.WriteLine($"{nameof(Person.ID)}: {p.ID}  {nameof(Person.Name)}: {p.Name}");
                }
            }
        }
    }
}
