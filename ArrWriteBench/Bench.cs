using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[DisassemblyDiagnoser(exportDiff: true, exportCombinedDisassemblyReport: true)]
public class Bench
{
    [ModuleInitializer]
    public static void RunCctor()
    {
        RuntimeHelpers.RunClassConstructor(typeof(Bench).TypeHandle);
    }

    private static readonly int[] Arr;

    private const int ArrSize = 1000, NumToWrite = 69;
    
    static Bench()
    {
        Arr = GC.AllocateUninitializedArray<int>(ArrSize);
    }

    [Benchmark]
    public void ArrWrite()
    {
        ref var Current = ref MemoryMarshal.GetArrayDataReference(Arr);

        ref var LastOffsetByOne = ref Unsafe.Add(ref Current, ArrSize);
        
        var Num = NumToWrite;
        
        for (; !Unsafe.AreSame(ref Current, ref LastOffsetByOne); Current = ref Unsafe.Add(ref Current, 1))
        {
            Current = Num;
        }
    }

    [Benchmark]
    [SkipLocalsInit]
    public unsafe void StackAllocWrite()
    {
        var Size = ArrSize;

        var Current = stackalloc int[Size];

        var LastOffsetByOne = Current + Size;

        var Num = NumToWrite;
        
        for (; Current != LastOffsetByOne; Current++)
        {
            *Current = Num;
        }
    }
}