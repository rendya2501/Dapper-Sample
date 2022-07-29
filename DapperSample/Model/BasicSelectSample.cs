using Dapper;
using DapperSample.Entity;
using System;
using System.Data.SqlClient;

namespace DapperSample
{
    internal class BasicSelectSample
    {
        public void Execute()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = System.IO.Path.GetFullPath(@"..\..\Database1.mdf"),
                IntegratedSecurity = true,
            };
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                // 接続
                connection.Open();

                // クエリ
                var query = "SELECT * FROM Person";

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
