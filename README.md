# ArrWriteBench
Benching write performance of a regular array against stack allocated memory

# Results
|          Method |     Mean |   Error |  StdDev | Code Size |
|---------------- |---------:|--------:|--------:|----------:|
| StackAllocWrite | 231.3 ns | 0.25 ns | 0.24 ns |     107 B |
|        ArrWrite | 231.4 ns | 0.13 ns | 0.12 ns |      48 B |

# Codegen
**.NET 7.0.0 (7.0.22.42610), X64 RyuJIT**

```assembly
; Bench.StackAllocWrite()
       push      rbp
       sub       rsp,30
       lea       rbp,[rsp+20]
       mov       rax,8B686FE0DB65
       mov       [rbp+8],rax
       test      [rsp],esp
       sub       rsp,0FA0
       lea       rax,[rsp+20]
       lea       rdx,[rax+0FA0]
       cmp       rax,rdx
       je        short M00_L01
       nop       dword ptr [rax+rax]
       nop       dword ptr [rax+rax]
M00_L00:
       mov       dword ptr [rax],45
       add       rax,4
       cmp       rax,rdx
       jne       short M00_L00
M00_L01:
       mov       rcx,8B686FE0DB65
       cmp       [rbp+8],rcx
       je        short M00_L02
       call      CORINFO_HELP_FAIL_FAST
M00_L02:
       nop
       lea       rsp,[rbp+10]
       pop       rbp
       ret
; Total bytes of code 107
```

```assembly
; Bench.ArrWrite()
       mov       rax,25D48401F48
       mov       rax,[rax]
       add       rax,10
       lea       rdx,[rax+0FA0]
       cmp       rax,rdx
       je        short M00_L01
       nop       dword ptr [rax]
M00_L00:
       mov       dword ptr [rax],45
       add       rax,4
       cmp       rax,rdx
       jne       short M00_L00
M00_L01:
       ret
; Total bytes of code 48
```


# Conclusion
Not a huge difference so long both are hot in cache! Notice how `M00_L00` for both methods are exactly the same, that is basically the for loop
