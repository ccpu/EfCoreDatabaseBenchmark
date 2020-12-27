using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Entities;
using EfCoreDatabaseBenchmark.PerformanceCounter;
using EfCoreDatabaseBenchmark.Repositories;


namespace EfCoreDatabaseBenchmark
{

    public class BenchmarkCase
    {
        public string CaseName { get; set; }
        public Action<int> SelectFunc { get; set; }
        public Func<int, Task> InsertFunc { get; set; }
        public Func<int, Task> UpdateFunc { get; set; }
        public IRepository Repo { get; set; }
    }

    public class Column
    {
        public string Header { get; set; }
        public string Line { get; set; }
    }

    public class Engine
    {
        //public readonly IResultService _context;
        public readonly int _sequence;
        public readonly List<Func<BenchmarkCase>> _cases = new List<Func<BenchmarkCase>>();
        public readonly int _numOfItemToInsert;
        public int _totalInsert;

        public Engine(int sequence, int numOfItemToInsert)
        {
            //_context = context;
            _sequence = sequence;
            _numOfItemToInsert = numOfItemToInsert;
            const string dir = "Results";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }


        public void Add(Func<BenchmarkCase> func)
        {
            _cases.Add(func);
        }

        public async Task Run()
        {
            Console.WriteLine("Benchmark started ...");

            for (var x = 0; x < _sequence; x++)
            {
                Console.WriteLine("---------- Benchmark Sequence " + (x + 1) + " ----------");

                _totalInsert += (x + _numOfItemToInsert);

                foreach (var func in _cases)
                {
                    BenchmarkCase item = func();

                    Console.Write(item.CaseName + " ");

                    var result = await Collect(item);

                    Console.Write(result.InsertTime);
                    Console.WriteLine();
                }
            }
            Console.WriteLine(_totalInsert + " item inserted.");
        }

        public async Task<BenchmarkResult> Collect(BenchmarkCase @case)
        {
            var benchmarkResult = new BenchmarkResult();

            using (var diskUsage = new PerformanceCollector())
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                diskUsage.Start();
                await @case.InsertFunc(_numOfItemToInsert);
                stopWatch.Stop();
                diskUsage.Stop();

                var totalRows = @case.Repo.Count(@case.CaseName);
                var pos = Math.Abs(totalRows / 2);
                var insertTime = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).TotalSeconds;
                stopWatch = new Stopwatch();
                stopWatch.Start();
                @case.SelectFunc(pos);
                var selectTime = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).TotalSeconds;
                stopWatch.Stop();

                stopWatch = new Stopwatch();
                stopWatch.Start();
                await @case.UpdateFunc(pos);
                var updateTime = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).TotalSeconds;
                stopWatch.Stop();

                var perfResult = diskUsage.GetData();

                foreach (var result in perfResult.GetType().GetProperties())
                {
                    var value = result.GetValue(perfResult);
                    benchmarkResult.GetType().GetProperty(result.Name)?.SetValue(benchmarkResult, value);
                }
                benchmarkResult.SelectTime = selectTime;
                benchmarkResult.Inserts = _numOfItemToInsert;
                benchmarkResult.InsertTime = insertTime;
                benchmarkResult.UpdateTime = updateTime;
                benchmarkResult.BenchmarkCase = @case.CaseName;
                await @case.Repo.Add(benchmarkResult);
            }

            return benchmarkResult;
        }
    }
}