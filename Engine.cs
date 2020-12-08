using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Entities;
using EfCoreDatabaseBenchmark.PerformanceCounter;
using EfCoreDatabaseBenchmark.Services;
using EfCoreDatabaseBenchmark.Utils;

namespace EfCoreDatabaseBenchmark
{

    public class BenchmarkCase
    {
        public string CaseName { get; set; }
        public Action<int> SelectFunc { get; set; }
        public Func<int, Task> InsertFunc { get; set; }
    }

    public class Column
    {
        public string Header { get; set; }
        public string Line { get; set; }
    }

    public class Engine
    {
        public readonly IResultService _context;
        public readonly int _sequence;
        public readonly List<BenchmarkCase> _cases = new List<BenchmarkCase>();
        public readonly int _numOfItemToInsert;
        public int _totalInsert;
        public readonly string _mainFile;

        public Engine(IResultService context, string dbName, int sequence, int numOfItemToInsert)
        {
            _context = context;
            _sequence = sequence;
            _numOfItemToInsert = numOfItemToInsert;
            const string dir = "Results";
            _mainFile = dir + "/" + dbName + ".md";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public static Column GenerateHeader(IReadOnlyCollection<string> headerColumn, bool firstToLeft = false)
        {
            var firstToLeftSet = false;
            return new Column
            {
                Header = "|" + headerColumn.Aggregate("", (str, obj) => str + obj.ToString() + "|"),
                Line = "|" + headerColumn.Aggregate("", (str, obj) =>
                {
                    if (firstToLeft && !firstToLeftSet)
                    {
                        firstToLeftSet = true;
                        return str + ":------------|";
                    }

                    return str + ":------------:|";
                })
            };
        }

        public void Add(BenchmarkCase newCase)
        {
            _cases.Add(newCase);
        }

        public async Task Run()
        {
            var props = typeof(BenchmarkResult)
                .GetProperties()
                .OrderBy(p => p.GetCustomAttributes(typeof(DisplayAttribute), true)
                               .Cast<DisplayAttribute>()
                               .Select(a => a.Order)
                               .FirstOrDefault()).Filter(x => x.Name != "Id");

            var columns = props.Map(x => x.Name).ToList();
            columns[0] = "<div style='width:155px'>Case</div>";

            var tableHeader = GenerateHeader(columns, true);

            Console.WriteLine("Benchmark started ...");

            for (var x = 0; x < _sequence; x++)
            {
                Console.WriteLine("---------- Sequence " + (x + 1) + " ----------");
                var mainFileExist = File.Exists(_mainFile);
                if (mainFileExist)
                {
                    await File.AppendAllLinesAsync(_mainFile, new[] { tableHeader.Header });
                }
                else
                {
                    await File.WriteAllLinesAsync(_mainFile, new[] { tableHeader.Header, tableHeader.Line });
                }

                _totalInsert += (x + _numOfItemToInsert);

                foreach (var item in _cases)
                {
                    Console.Write("Benchmarking " + item.CaseName + " ");

                    var result = await Collect(item.CaseName, item.SelectFunc, item.InsertFunc);

                    var values = props.Map(p => p.GetValue(result, null)).ToList();

                    var valuesString = "|" + values.Aggregate("", (str, obj) => str + obj + "|");

                    await File.AppendAllLinesAsync(_mainFile, new[] { valuesString });

                    Console.Write(result.InsertTime);
                    Console.WriteLine();
                }
            }
            Console.WriteLine(_totalInsert + " item inserted.");
        }

        public async Task<BenchmarkResult> Collect(string name, Action<int> select, Func<int, Task> batchInsert)
        {

            var diskUsage = new PerformanceCollector();
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            diskUsage.Start();
            await batchInsert(_numOfItemToInsert);
            stopWatch.Stop();
            diskUsage.Stop();

            var pos = Math.Abs(_context.Count(name) / 2);
            var insertTime = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).TotalSeconds;
            stopWatch = new Stopwatch();
            stopWatch.Start();
            select(pos);
            var selectTime = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).TotalSeconds;
            stopWatch.Stop();


            var perfResult = diskUsage.GetData();

            var benchmarkResult = new BenchmarkResult();

            foreach (var result in perfResult.GetType().GetProperties())
            {
                var value = result.GetValue(perfResult);
                benchmarkResult.GetType().GetProperty(result.Name)?.SetValue(benchmarkResult, value);
            }
            benchmarkResult.SelectTime = selectTime;
            benchmarkResult.Inserts = _numOfItemToInsert;
            benchmarkResult.InsertTime = insertTime;
            benchmarkResult.BenchmarkCase = name;

            await _context.Add(benchmarkResult);

            return benchmarkResult;
        }
    }
}