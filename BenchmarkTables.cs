using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EfCoreDatabaseBenchmark.Entities;

namespace EfCoreDatabaseBenchmark
{
    public enum BenchmarkTables
    {
        [EnumMember(Value = nameof(Entities.AutoIncrementKey))]
        AutoIncrementKey,
        [EnumMember(Value = nameof(Entities.GuidCombIndexed))]
        GuidCombIndexed,
        [EnumMember(Value = nameof(Entities.GuidCombKey))]
        GuidCombKey,
        [EnumMember(Value = nameof(Entities.GuidIndexed))]
        GuidIndexed,
        [EnumMember(Value = nameof(Entities.GuidKey))]
        GuidKey,
        [EnumMember(Value = nameof(Entities.GuidSequentialIndexed))]
        GuidSequentialIndexed,
        [EnumMember(Value = nameof(Entities.GuidSequentialKey))]
        GuidSequentialKey,
        [EnumMember(Value = nameof(Entities.NumericIndexed))]
        NumericIndexed,
        [EnumMember(Value = nameof(Entities.ObjectIdCharIndexed))]
        ObjectIdCharIndexed,
        [EnumMember(Value = nameof(Entities.ObjectIdIndexed))]
        ObjectIdIndexed,
        [EnumMember(Value = nameof(Entities.ObjectIdKey))]
        ObjectIdKey,
    }

}
