using Dapper;
using DapperSample.Entity;
using System;
using System.Data.SqlClient;


namespace DapperSample.Model
{
    internal class SelectSample
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

        public void Execute()
        {
            Query();
            QueryFirstOrDefault();
            ExecuteScalar();
        }


        private void Query()
        {
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                // IEnumerable型が返る。
                string sql = "SELECT * FROM Person";
                var person = connection.Query<Person>(sql);
                foreach (var item in person)
                {
                    Console.WriteLine(item.ToString());
                }

                // 結果がない場合、空のIEnumerableが返る。
                string sql2 = "SELECT * FROM Person WHERE ID = @ID";
                var person2 = connection.Query<Person>(sql2, new { ID = 4 });
                foreach (var item in person2)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        private void QueryFirstOrDefault()
        {
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                // 1件のレコードが返る。
                string sql = "SELECT * FROM Person WHERE ID = @ID";
                var person = connection.QueryFirstOrDefault<Person>(sql,new { ID = 1});
                Console.WriteLine(person.ID + ":" + person.Name);

                // 1件もない場合はnullとなる
                var person2 = connection.QueryFirstOrDefault<Person>(sql, new { ID = 4 });
                Console.WriteLine(person2.ID + ":" + person2.Name);
            }
        }

        private void ExecuteScalar()
        {
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                // 単一レコードの特定の列を取得する
                string sql = "SELECT [Name] FROM [Person] WHERE [ID] = @ID";
                var name = connection.ExecuteScalar<string>(sql, new { ID = 1 });
                Console.WriteLine(name);

                // 1件もない場合はnullとなる
                var name2 = connection.ExecuteScalar<string>(sql, new { ID = 4 });
                Console.WriteLine(name2);
            }
        }
    }
}
