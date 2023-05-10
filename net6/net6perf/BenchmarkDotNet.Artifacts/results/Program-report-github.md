``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1165 (21H1/May2021Update)
Intel Core i7-9850H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  Job-YHYGQI : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT
  Job-FYOECP : .NET 6.0.0 (6.0.21.42010), X64 RyuJIT
  Job-XTPWQX : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT


```
|  Method |        Job |            Runtime | Toolchain |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Code Size | Allocated |
|-------- |----------- |------------------- |---------- |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|
| Compute | Job-YHYGQI |           .NET 5.0 |    net5.0 | 0.1918 ns | 0.0586 ns | 0.1728 ns | 0.1190 ns |     ? |       ? |       6 B |         - |
| Compute | Job-FYOECP |           .NET 6.0 |    net6.0 | 0.1896 ns | 0.0400 ns | 0.0877 ns | 0.1943 ns |     ? |       ? |       6 B |         - |
| Compute | Job-XTPWQX | .NET Framework 4.8 |     net48 | 0.1089 ns | 0.0505 ns | 0.1373 ns | 0.0643 ns |     ? |       ? |       6 B |         - |
