using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Order;
using Perfolizer.Horology;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Compression;
#if NETCOREAPP3_0_OR_GREATER
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

[DisassemblyDiagnoser(maxDepth: 1)] // change to 0 for just the [Benchmark] method
[MemoryDiagnoser(displayGenColumns: false)]
public class Program
{
    public static void Main(string[] args) =>
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance
            //.WithSummaryStyle(new SummaryStyle(CultureInfo.InvariantCulture, printUnitsInHeader: false, SizeUnit.B, TimeUnit.Microsecond))
        );

    // BENCHMARKS GO HERE
    [Benchmark]
    //[MethodImpl(MethodImplOptions.NoInlining)]
    public int Compute() => ComputeValue(123) * 11;

    //[MethodImpl(MethodImplOptions.NoInlining)]
    private static int ComputeValue(int length) => length * 7;

    private int _value = 12345;
    private byte[] _buffer = new byte[100];

    [Benchmark]
    public bool Format() => Utf8Formatter.TryFormat(_value, _buffer, out _, new StandardFormat('D', 2));

    private int[] _values = Enumerable.Range(0, 100_000).ToArray();

    [Benchmark]
    public int Find() => Find(_values, 99_999);

    private static int Find<T>(T[] array, T item)
    {
        for (int i = 0; i < array.Length; i++)
            if (EqualityComparer<T>.Default.Equals(array[i], item))
                return i;

        return -1;
    }

    private (int, long, int, long) _value1 = (5, 10, 15, 20);
    private (int, long, int, long) _value2 = (5, 10, 15, 20);

    [Benchmark]
    public int Compare() => _value1.CompareTo(_value2);
}