# ArrWriteBench
Benching write performance of a regular array against stack allocated memory

# Results
|          Method |     Mean |   Error |  StdDev | Code Size |
|---------------- |---------:|--------:|--------:|----------:|
| StackAllocWrite | 231.3 ns | 0.25 ns | 0.24 ns |     107 B |
|        ArrWrite | 231.4 ns | 0.13 ns | 0.12 ns |      48 B |

# Conclusion
Not a huge difference so long both are hot in cache!
