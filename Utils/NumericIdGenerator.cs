using System;
using IdGen;

internal class NumericIdGenerator
{
    public static long _lastId = DateTime.UtcNow.Ticks;
    public static IdGenerator _generator;

    public static long GenerateId()
    {
        if (_generator == null)
        { _generator = new IdGenerator(0); }
        return _generator.CreateId();
    }
}