using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace EfCoreDatabaseBenchmark.PerformanceCounter
{

    public class CounterInfo
    {
        [DisplayAttribute(Order = 20)]
        public float DiskTime { get; set; }
        [DisplayAttribute(Order = 21)]
        public float DiskReadTime { get; set; }
        [DisplayAttribute(Order = 22)]
        public float DiskWriteTime { get; set; }
        [DisplayAttribute(Order = 23)]
        public float CurrentDiskQueueLength { get; set; }
        [DisplayAttribute(Order = 24)]
        public float AvgDiskQueueLength { get; set; }
        [DisplayAttribute(Order = 25)]
        public float AvgDiskReadQueueLength { get; set; }
        [DisplayAttribute(Order = 26)]
        public float AvgDiskWriteQueueLength { get; set; }
    }

    public class Counters
    {
        public System.Diagnostics.PerformanceCounter DiskTime { get; set; }
        public System.Diagnostics.PerformanceCounter DiskReadTime { get; set; }
        public System.Diagnostics.PerformanceCounter DiskWriteTime { get; set; }
        public System.Diagnostics.PerformanceCounter CurrentDiskQueueLength { get; set; }
        public System.Diagnostics.PerformanceCounter AvgDiskQueueLength { get; set; }
        public System.Diagnostics.PerformanceCounter AvgDiskReadQueueLength { get; set; }
        public System.Diagnostics.PerformanceCounter AvgDiskWriteQueueLength { get; set; }
    }

    public class PerformanceCollector : IDisposable
    {
        public readonly Thread _thread;
        public bool _stop = false;
        public readonly List<CounterInfo> _counterData = new List<CounterInfo>();
        public Counters _performanceCounters;

        public PerformanceCollector()
        {
            _thread = new Thread(Collect);
            InitCounters();
        }

        public void Start()
        {
            _thread.Start();
        }

        public void InitCounters()
        {
            _performanceCounters = new Counters
            {
                CurrentDiskQueueLength = new System.Diagnostics.PerformanceCounter("LogicalDisk", "Current Disk Queue Length", "E:"),
                DiskReadTime = new System.Diagnostics.PerformanceCounter("LogicalDisk", "% Disk Read Time", "E:"),
                DiskWriteTime = new System.Diagnostics.PerformanceCounter("LogicalDisk", "% Disk Write Time", "E:"),
                AvgDiskQueueLength = new System.Diagnostics.PerformanceCounter("LogicalDisk", "Avg. Disk Queue Length", "E:"),
                AvgDiskReadQueueLength = new System.Diagnostics.PerformanceCounter("LogicalDisk", "Avg. Disk Read Queue Length", "E:"),
                AvgDiskWriteQueueLength = new System.Diagnostics.PerformanceCounter("LogicalDisk", "Avg. Disk Write Queue Length", "E:"),
                DiskTime = new System.Diagnostics.PerformanceCounter("LogicalDisk", "% Disk Time", "E:"),
            };
        }


        public void Collect()
        {
            Thread.Sleep(2000);

            while (!_stop)
            {
                _performanceCounters.DiskReadTime.NextValue();
                _performanceCounters.DiskWriteTime.NextSample();
                _performanceCounters.DiskTime.NextValue();
                _performanceCounters.CurrentDiskQueueLength.NextValue();
                _performanceCounters.AvgDiskQueueLength.NextValue();
                _performanceCounters.AvgDiskReadQueueLength.NextValue();
                _performanceCounters.AvgDiskWriteQueueLength.NextValue();

                Thread.Sleep(1000);

                try
                {
                    _counterData.Add(new CounterInfo()
                    {
                        DiskReadTime = _performanceCounters.DiskReadTime.NextValue(),
                        DiskWriteTime = _performanceCounters.DiskWriteTime.NextValue(),
                        DiskTime = _performanceCounters.DiskTime.NextValue(),
                        CurrentDiskQueueLength = _performanceCounters.CurrentDiskQueueLength.NextValue(),
                        AvgDiskQueueLength = _performanceCounters.AvgDiskQueueLength.NextValue(),
                        AvgDiskReadQueueLength = _performanceCounters.AvgDiskReadQueueLength.NextValue(),
                        AvgDiskWriteQueueLength = _performanceCounters.AvgDiskWriteQueueLength.NextValue(),

                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

        }

        public CounterInfo GetData()
        {
            var AvgDiskQueueLength = (float)Math.Round(_counterData.Aggregate(0, (float acc, CounterInfo x) => acc + x.AvgDiskQueueLength) / _counterData.Count, 2);
            var DiskTime = (float)Math.Round(_counterData.Aggregate(0, (float acc, CounterInfo x) => acc + x.DiskTime) / _counterData.Count, 2);
            var CurrentDiskQueueLength = (float)Math.Round(_counterData.Aggregate(0, (float acc, CounterInfo x) => acc + x.CurrentDiskQueueLength) / _counterData.Count, 2);
            var AvgDiskWriteQueueLength = (float)Math.Round(_counterData.Aggregate(0, (float acc, CounterInfo x) => acc + x.AvgDiskWriteQueueLength) / _counterData.Count, 2);
            var AvgDiskReadQueueLength = (float)Math.Round(_counterData.Aggregate(0, (float acc, CounterInfo x) => acc + x.AvgDiskReadQueueLength) / _counterData.Count, 2);
            var DiskReadTime = (float)Math.Round(_counterData.Aggregate(0, (float acc, CounterInfo x) => acc + x.DiskReadTime) / _counterData.Count, 2);
            var DiskWriteTime = (float)Math.Round(_counterData.Aggregate(0, (float acc, CounterInfo x) => acc + x.DiskWriteTime) / _counterData.Count, 2);

            return new CounterInfo
            {
                AvgDiskQueueLength = double.IsInfinity(AvgDiskQueueLength) || Double.IsNaN(AvgDiskQueueLength) ? 0 : AvgDiskQueueLength,
                DiskTime = double.IsInfinity(DiskTime) || Double.IsNaN(DiskTime) ? 0 : DiskTime,
                CurrentDiskQueueLength = double.IsInfinity(CurrentDiskQueueLength) || Double.IsNaN(CurrentDiskQueueLength) ? 0 : CurrentDiskQueueLength,
                AvgDiskWriteQueueLength = double.IsInfinity(AvgDiskWriteQueueLength) || Double.IsNaN(AvgDiskWriteQueueLength) ? 0 : AvgDiskWriteQueueLength,
                AvgDiskReadQueueLength = double.IsInfinity(AvgDiskReadQueueLength) || Double.IsNaN(AvgDiskReadQueueLength) ? 0 : AvgDiskReadQueueLength,
                DiskReadTime = double.IsInfinity(DiskReadTime) || Double.IsNaN(DiskReadTime) ? 0 : DiskReadTime,
                DiskWriteTime = double.IsInfinity(DiskWriteTime) || Double.IsNaN(AvgDiskQueueLength) ? 0 : DiskWriteTime
            };
        }

        public void Stop()
        {
            _stop = true;
        }

        public void Dispose()
        {
            _counterData.Clear();
            _performanceCounters.DiskReadTime.Dispose();
            _performanceCounters.DiskWriteTime.Dispose();
            _performanceCounters.DiskTime.Dispose();
            _performanceCounters.CurrentDiskQueueLength.Dispose();
            _performanceCounters.AvgDiskQueueLength.Dispose();
            _performanceCounters.AvgDiskReadQueueLength.Dispose();
            _performanceCounters.AvgDiskWriteQueueLength.Dispose();
        }
    }
}
