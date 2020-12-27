using System;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Data;


namespace EfCoreDatabaseBenchmark
{
    internal class Program
    {
        private static async Task Main()
        {
            var sequence = 100;
            var numOfItems = 10000;
            int databaseNum = 1;

            while (true)
            {

                Console.WriteLine();

                Console.WriteLine("Enter database number:");
                Console.WriteLine();
                Console.WriteLine("1. Mysql");
                Console.WriteLine("2. Postgre");
                Console.WriteLine("3. Mariadb");
                Console.WriteLine("4. CockroachDB ");
                Console.WriteLine("default=" + databaseNum + "");
                var databaseResult = Console.ReadLine();
                databaseNum = !string.IsNullOrEmpty(
                    databaseResult
                )
                    ? Int32.Parse(
                        databaseResult
                    )
                    : databaseNum;

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
                    case 1:
                    await Benchmark.Run(
                        () => new MySqlDbContext(),
                        sequence,
                        numOfItems
                    );
                    break;
                    case 2:
                    await Benchmark.Run(
                        () => new PostgreDbContext(),
                        sequence,
                        numOfItems
                    );
                    break;
                    case 3:
                    await Benchmark.Run(
                        () => new MariadbDbContext(),
                        sequence,
                        numOfItems
                    );
                    break;
                    case 4:
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
