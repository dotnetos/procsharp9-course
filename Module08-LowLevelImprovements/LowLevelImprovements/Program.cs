using BenchmarkDotNet.Running;

namespace LowLevelImprovements
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Benchmarks).Assembly);
        }
    }
}
