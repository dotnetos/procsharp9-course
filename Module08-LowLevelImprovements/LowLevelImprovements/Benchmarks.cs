using System;
using BenchmarkDotNet.Attributes;

namespace LowLevelImprovements
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        static readonly Guid Guid1 = new("8104D954-3AC1-439B-A124-7FE71C8986A6");
        static readonly Guid Guid2 = new("CC944B7C-F150-4219-8F62-3D8B10FF491E");

        static ulong Mix(ulong u1, ulong u2) => u1 ^ u2;

        [GlobalSetup]
        public void GlobalSetup()
        {
            if (Mixer.Before(Guid1, Guid2, Mix) != Mixer.After(Guid1, Guid2, Mix))
            {
                // this is a really dummy unit test :-)
                throw new Exception("Implementation has changed!");
            }
        }

        [Benchmark(Baseline = true)]
        public Guid Before()
        {
            return Mixer.Before(Guid1, Guid2, Mix);
        }

        [Benchmark()]
        public Guid After()
        {
            return Mixer.After(Guid1, Guid2, Mix);
        }
    }
}