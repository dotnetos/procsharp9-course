using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LowLevelImprovements
{
    public static class Mixer
    {
        const int Low = 0;
        const int High = 1;

        // Leave this as it is. It's the foundation for benchmarking.
        public static Guid Before(Guid guid1, Guid guid2, Func<ulong, ulong, ulong> mixer)
        {
            Span<byte> buffer1 = stackalloc byte[16];
            Span<byte> buffer2 = stackalloc byte[16];

            guid1.TryWriteBytes(buffer1);
            guid2.TryWriteBytes(buffer2);

            // read parts, low and high
            var low1 = ReadOptimized(buffer1, Low);
            var high1 = ReadOptimized(buffer1, High);
            var low2 = ReadOptimized(buffer2, Low);
            var high2 = ReadOptimized(buffer2, High);

            // mix them and write back to buffer1
            WriteOptimized(buffer1, Low, mixer(low1, low2));
            WriteOptimized(buffer1, High, mixer(high1, high2));

            return new Guid(buffer1);
        }

        // TODO: optimize this method by skipping initialization of locals and introducing a function pointer instead of a delegate
        public static Guid After(Guid guid1, Guid guid2, Func<ulong, ulong, ulong> mixer)
        {
            Span<byte> buffer1 = stackalloc byte[16];
            Span<byte> buffer2 = stackalloc byte[16];

            guid1.TryWriteBytes(buffer1);
            guid2.TryWriteBytes(buffer2);

            // read parts, low and high
            var low1 = ReadOptimized(buffer1, Low);
            var high1 = ReadOptimized(buffer1, High);
            var low2 = ReadOptimized(buffer2, Low);
            var high2 = ReadOptimized(buffer2, High);

            // mix them and write back to buffer1
            WriteOptimized(buffer1, Low, mixer(low1, low2));
            WriteOptimized(buffer1, High, mixer(high1, high2));

            return new Guid(buffer1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ulong ReadOptimized(Span<byte> buffer, int ulongIndex) => Unsafe.ReadUnaligned<ulong>(ref Unsafe.Add(ref MemoryMarshal.GetReference(buffer), ulongIndex * sizeof(ulong)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void WriteOptimized(Span<byte> buffer, int ulongIndex, ulong value) => Unsafe.WriteUnaligned(ref Unsafe.Add(ref MemoryMarshal.GetReference(buffer), ulongIndex * sizeof(ulong)), value);
    }
}