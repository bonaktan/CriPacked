// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CCheckSum
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using \u003CCppImplementationDetails\u003E;
using System;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CCheckSum : IDisposable
  {
    private void \u007ECCheckSum()
    {
    }

    private static void Checksum_Config(int rom_type) => \u003CModule\u003E.\u003FA0xead22f07\u002Eg_rom_type = rom_type;

    private static unsafe void MakeChecksum(void* in_ptr, int in_bytes, ulong* check_sum)
    {
      if (\u003CModule\u003E.\u003FA0xead22f07\u002Eg_rom_type == 0)
      {
        \u0024ArrayType\u0024\u0024\u0024BY03G arrayTypeBy03G;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(short&) ((IntPtr) &arrayTypeBy03G + 6) = (short) 0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(short&) ((IntPtr) &arrayTypeBy03G + 4) = (short) 0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(short&) ((IntPtr) &arrayTypeBy03G + 2) = (short) 0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(short&) ref arrayTypeBy03G = (short) 0;
        int num1 = in_bytes / 8;
        if (0 < num1)
        {
          void* voidPtr = (void*) ((IntPtr) in_ptr + 4);
          uint num2 = (uint) num1;
          do
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(short&) ref arrayTypeBy03G = (short) ((int) *(ushort*) ((IntPtr) voidPtr - 4) + (int) ^(ushort&) ref arrayTypeBy03G);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(short&) ((IntPtr) &arrayTypeBy03G + 2) = (short) ((int) *(ushort*) ((IntPtr) voidPtr - 2) + (int) ^(ushort&) ((IntPtr) &arrayTypeBy03G + 2));
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(short&) ((IntPtr) &arrayTypeBy03G + 4) = (short) ((int) *(ushort*) voidPtr + (int) ^(ushort&) ((IntPtr) &arrayTypeBy03G + 4));
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(short&) ((IntPtr) &arrayTypeBy03G + 6) = (short) ((int) *(ushort*) ((IntPtr) voidPtr + 2) + (int) ^(ushort&) ((IntPtr) &arrayTypeBy03G + 6));
            voidPtr += 8;
            --num2;
          }
          while (num2 > 0U);
        }
        CCheckSum.PackCheckSum16x4((ushort*) &arrayTypeBy03G, check_sum);
      }
      else
      {
        uint* numPtr = (uint*) in_ptr;
        \u0024ArrayType\u0024\u0024\u0024BY01I arrayTypeBy01I;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &arrayTypeBy01I + 4) = 0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref arrayTypeBy01I = 0;
        int num3 = in_bytes / 8;
        if (0 < num3)
        {
          uint num4 = (uint) num3;
          do
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref arrayTypeBy01I = (int) *numPtr + ^(int&) ref arrayTypeBy01I;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &arrayTypeBy01I + 4) = (int) numPtr[1] + ^(int&) ((IntPtr) &arrayTypeBy01I + 4);
            numPtr += 2;
            --num4;
          }
          while (num4 > 0U);
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        *check_sum = (ulong) (uint) (~^(int&) ((IntPtr) &arrayTypeBy01I + 4) + 1) << 32 | (ulong) (uint) (~^(int&) ref arrayTypeBy01I + 1);
      }
    }

    private static unsafe void UnpackCheckSum16x4(ushort* sum16, ulong sum64)
    {
      *sum16 = (ushort) (uint) ~((long) sum64 + -1L);
      sum16[1] = (ushort) (uint) ~((long) (sum64 >> 16) + -1L);
      sum16[2] = (ushort) (uint) ~((long) (sum64 >> 32) + -1L);
      sum16[3] = (ushort) (uint) ~((long) (sum64 >> 48) + -1L);
    }

    private static unsafe void PackCheckSum16x4(ushort* sum16, ulong* sum64) => *sum64 = (ulong) ((((long) (ushort) ((uint) ~sum16[3] + 1U) << 16 | (long) (ushort) ((uint) ~sum16[2] + 1U)) << 16 | (long) (ushort) ((uint) ~sum16[1] + 1U)) << 16) | (ulong) (ushort) ((uint) ~*sum16 + 1U);

    private static unsafe void UnpackCheckSum32x2(uint* sum32, ulong sum64)
    {
      *sum32 = ~(uint) (sum64 + ulong.MaxValue);
      sum32[1] = ~(uint) ((sum64 >> 32) + ulong.MaxValue);
    }

    private static unsafe void PackCheckSum32x2(uint* sum32, ulong* sum64) => *sum64 = (ulong) (uint) (~(int) sum32[1] + 1) << 32 | (ulong) (uint) (~(int) *sum32 + 1);

    public static unsafe ulong AddCheckSum64Tsbgp2(ulong sum0, ulong sum1)
    {
      \u0024ArrayType\u0024\u0024\u0024BY01I arrayTypeBy01I1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref arrayTypeBy01I1 = ~(int) (uint) (sum0 + ulong.MaxValue);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &arrayTypeBy01I1 + 4) = ~(int) (uint) ((sum0 >> 32) + ulong.MaxValue);
      \u0024ArrayType\u0024\u0024\u0024BY01I arrayTypeBy01I2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref arrayTypeBy01I2 = ~(int) (uint) (sum1 + ulong.MaxValue);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &arrayTypeBy01I2 + 4) = ~(int) (uint) ((sum1 >> 32) + ulong.MaxValue);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref arrayTypeBy01I1 = ^(int&) ref arrayTypeBy01I2 + ^(int&) ref arrayTypeBy01I1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &arrayTypeBy01I1 + 4) = ^(int&) ((IntPtr) &arrayTypeBy01I2 + 4) + ^(int&) ((IntPtr) &arrayTypeBy01I1 + 4);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      return (ulong) (uint) (~^(int&) ((IntPtr) &arrayTypeBy01I1 + 4) + 1) << 32 | (ulong) (uint) (~^(int&) ref arrayTypeBy01I1 + 1);
    }

    public static unsafe ulong CalcAddCheckSum64Tsbgp2(ulong sum32x2, void* ptr, uint len)
    {
      ulong sum1 = CCheckSum.CalcCheckSum64Tsbgp2(ptr, len);
      return CCheckSum.AddCheckSum64Tsbgp2(sum32x2, sum1);
    }

    public static unsafe ulong CalcCheckSum64Tsbgp2(void* ptr0, uint len0, void* ptr1, uint len1) => CCheckSum.AddCheckSum64Tsbgp2(CCheckSum.CalcCheckSum64Tsbgp2(ptr0, len0), CCheckSum.CalcCheckSum64Tsbgp2(ptr1, len1));

    public static unsafe ulong CalcCheckSum64Tsbgp2(void* ptr, uint len)
    {
      uint in_bytes = len & 4294967288U;
      uint num = len & 7U;
      \u003CModule\u003E.\u003FA0xead22f07\u002Eg_rom_type = 1;
      ulong sum0;
      CCheckSum.MakeChecksum(ptr, (int) in_bytes, &sum0);
      if (num != 0U)
      {
        \u0024ArrayType\u0024\u0024\u0024BY07E arrayTypeBy07E;
        // ISSUE: initblk instruction
        __memset(ref arrayTypeBy07E, 0, 8);
        // ISSUE: cpblk instruction
        __memcpy(ref arrayTypeBy07E, (int) in_bytes + (IntPtr) ptr, (int) num);
        ulong sum1;
        CCheckSum.MakeChecksum((void*) &arrayTypeBy07E, 8, &sum1);
        sum0 = CCheckSum.AddCheckSum64Tsbgp2(sum0, sum1);
      }
      return sum0;
    }

    public static unsafe uint GetCheckSumRangeTsbgp2(void* ptr, uint len) => 0;

    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECCheckSum();
      }
      else
      {
        // ISSUE: explicit finalizer call
        this.Finalize();
      }
    }

    public virtual void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
  }
}
