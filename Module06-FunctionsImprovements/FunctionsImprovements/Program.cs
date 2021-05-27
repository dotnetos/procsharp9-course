using BenchmarkDotNet.Running;

namespace FunctionsImprovements
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
