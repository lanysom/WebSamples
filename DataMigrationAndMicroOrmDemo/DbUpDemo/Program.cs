using DbUp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbUpDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Builds connection string
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "WebZoo",
                IntegratedSecurity = true
            };

            EnsureDatabase.For.SqlDatabase(connectionStringBuilder.ConnectionString);

            var upgrader = DeployChanges.To
                .SqlDatabase(connectionStringBuilder.ConnectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
            }
            
#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
