using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using EfCoreDatabaseBenchmark.Data;
using EfCoreDatabaseBenchmark.Entities;
using Microsoft.EntityFrameworkCore;
using CsvWriter = CsvHelper.CsvWriter;

namespace EfCoreDatabaseBenchmark
{
    public class ReportGenerator
    {
        public static void Generate()
        {
            try
            {
                Console.WriteLine("Generating reports ...");
                Console.WriteLine("MySQL Reports ...");
                using (var context = new MySqlDbContext())
                {
                    if (context.Exists())
                    {
                        MySqlReport(context.Results, "mysql");
                    }
                }
                Console.WriteLine("Mariadb Reports ...");
                using (var context = new MariadbDbContext())
                {
                    if (context.Exists())
                    {
                        MySqlReport(context.Results, "mariadb");
                    }
                }
                Console.WriteLine("PostgreDb Reports ...");
                using (var context = new PostgreDbContext())
                {
                    if (context.Exists())
                    {
                        MySqlReport(context.Results, "Postgredb");
                    }
                }
                Console.WriteLine("Cockroachdb Reports ...");
                using (var context = new CockroachdbDbContext())
                {
                    if (context.Exists())
                    {
                        MySqlReport(context.Results, "cockroachdb");
                    }
                }
                Console.WriteLine("All Report Generated.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void MySqlReport(DbSet<BenchmarkResult> contextResults, string databaseName)
        {
            if (!contextResults.Any()) return;

            var tableNamesList = typeof(BenchmarkTables)
                .GetFields()
                .Select(x => new
                {
                    att = x.GetCustomAttributes(false)
                        .OfType<EnumMemberAttribute>()
                        .FirstOrDefault(),
                    member = x
                })
                .Where(x => x.att != null)
                .Select(x => x.member.GetValue(null))
                .ToList();

            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {

                var groups = contextResults.AsEnumerable().GroupBy(x => x.SessionId)
                    .ToList();

                foreach (var group in groups)
                {
                    // to prevent messing up csv file,if no match means benchmark has not generated result for all cases, maybe duo to cancellation 
                    if (group.Count() == tableNamesList.Count())
                    {
                        dynamic obj = new System.Dynamic.ExpandoObject();


                        foreach (var benchmarkProp in new BenchmarkResult().GetType().GetProperties()
                            .Where(x =>
                                x.Name != "Id" &&
                                x.Name != "BenchmarkCase" &&
                                x.Name != "Sum" &&
                                x.Name != "Min" &&
                                x.Name != "Max" &&
                                x.Name != "Avg" &&
                                x.Name != "SessionId" &&
                                x.Name != "CountSession"))
                        {
                            foreach (var tableName in tableNamesList)
                            {
                                var results = group.First(x => x.BenchmarkCase == tableName.ToString());

                                var benchmarkPropName = benchmarkProp.Name;
                                var value = results.GetType().GetProperty(benchmarkPropName)?.GetValue(results, null);
                                ((IDictionary<String, Object>)obj)[tableName + "_" + benchmarkPropName] = value;
                            }
                        }

                        csv.WriteRecord(obj);
                        csv.NextRecord();
                    }


                }
                writer.Flush();
                stream.Position = 0;
                var csvResult = reader.ReadToEnd();

                if (!Directory.Exists("results"))
                {
                    Directory.CreateDirectory("results");
                }

                File.WriteAllText("results/" + databaseName + "-report.csv", csvResult);

            }
        }

    }
}
