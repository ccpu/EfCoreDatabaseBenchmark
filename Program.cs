using System;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Data;


namespace EfCoreDatabaseBenchmark
{
    internal class Program
    {
        static DbNumber databaseNum = DbNumber.Mysql;

        internal enum DbNumber
        {
            Mysql = 1,
            Postgre = 2,
            Mariadb = 3,
            CockroachDB = 4
        }
        private static DbNumber ConsoleWriteDatabaseList(DbNumber defaultDb)
        {
            Console.WriteLine();

            Console.WriteLine("Enter database number:");
            Console.WriteLine();
            Console.WriteLine("1. Mysql");
            Console.WriteLine("2. Postgre");
            Console.WriteLine("3. Mariadb");
            Console.WriteLine("4. CockroachDB ");
            Console.WriteLine("default=" + defaultDb + "");
            var readLineSequence = Console.ReadLine();

            if (string.IsNullOrEmpty(readLineSequence))
            {
                return databaseNum;
            }

            var dbNum = int.Parse(
                readLineSequence
            );

            return (DbNumber)dbNum;
        }
        private static async Task Main()
        {
            await Start();
        }

        private static async Task Start()
        {

            //Console.WriteLine();
            //Console.WriteLine("Select operation type:");
            //Console.WriteLine();
            //Console.WriteLine("1. Benchmarking (default)");
            //Console.WriteLine("2. Generate Report");
            //var operationType = Console.ReadLine();

            //if (operationType == "2")
            //{
            //    
            ReportGenerator.Generate();
            //    
            //    _ = Start();
            //}

            DbNumber databaseNum = DbNumber.Mysql;

            var sequence = 100;
            var numOfItems = 10000;

            while (true)
            {

                databaseNum = ConsoleWriteDatabaseList(databaseNum);

                Console.Write(
                    $"Enter number of sequence ({sequence}): "
                );
                var readLineSequence = Console.ReadLine();
                sequence = string.IsNullOrEmpty(
                    readLineSequence
                )
                    ? sequence
                    : int.Parse(
                        readLineSequence
                    );

                Console.Write(
                    $"Enter number of item to insert ({numOfItems}): "
                );
                var readLineNumOfItems = Console.ReadLine();
                numOfItems = string.IsNullOrEmpty(
                    readLineNumOfItems
                )
                    ? numOfItems
                    : int.Parse(
                        readLineNumOfItems
                    );


                switch (databaseNum)
                {
                    case DbNumber.Mysql:
                    await Benchmark.Run(
                        () => new MySqlDbContext(),
                        sequence,
                        numOfItems
                    );
                    break;
                    case DbNumber.Postgre:
                    await Benchmark.Run(
                        () => new PostgreDbContext(),
                        sequence,
                        numOfItems
                    );
                    break;
                    case DbNumber.Mariadb:
                    await Benchmark.Run(
                        () => new MariadbDbContext(),
                        sequence,
                        numOfItems
                    );
                    break;
                    case DbNumber.CockroachDB:
                    await Benchmark.Run(
                        () => new CockroachdbDbContext(),
                        sequence,
                        numOfItems
                    );
                    break;
                }
            }
        }
    }
}
