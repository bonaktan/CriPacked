// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CCrc32
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using \u003CCppImplementationDetails\u003E;
using System;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CCrc32 : IDisposable
  {
    private void \u007ECCrc32()
    {
    }

    public static unsafe uint CalcCrc32(uint iniCrc32, uint length, void* ptr)
    {
      if (length == 0U)
        return iniCrc32;
      uint num1 = iniCrc32;
      byte* numPtr = (byte*) ptr;
      uint num2 = length;
      do
      {
        --num2;
        num1 = num1 * 769U + (uint) *numPtr;
        ++numPtr;
      }
      while (num2 != 0U);
      return (uint) ~(num1 != 0U ? (int) num1 : 1);
    }

    public static unsafe uint CalcCrc32Cont(uint iniCrc32, uint length, void* ptr)
    {
      if (length == 0U)
        return iniCrc32;
      uint num1 = iniCrc32;
      byte* numPtr = (byte*) ptr;
      uint num2 = length;
      do
      {
        --num2;
        num1 = num1 * 769U + (uint) *numPtr;
        ++numPtr;
      }
      while (num2 != 0U);
      return num1;
    }

    public static unsafe string ConvToCrc32Base64String(uint crc32)
    {
      \u0024ArrayType\u0024\u0024\u0024BY06D arrayTypeBy06D;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ref arrayTypeBy06D = (sbyte) (((int) crc32 & 63) + 60);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 1) = (sbyte) (((int) (crc32 >> 6) & 63) + 60);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 2) = (sbyte) (((int) (crc32 >> 12) & 63) + 60);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 3) = (sbyte) (((int) (crc32 >> 18) & 63) + 60);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 4) = (sbyte) (((int) (crc32 >> 24) & 63) + 60);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 5) = (sbyte) ((int) (crc32 >> 30) + 60);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 6) = (sbyte) 0;
      return new string((sbyte*) &arrayTypeBy06D);
    }

    public static unsafe void CheckCrc(string crcstr, uint crc32)
    {
      \u0024ArrayType\u0024\u0024\u0024BY06D arrayTypeBy06D;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ref arrayTypeBy06D = (sbyte) crcstr[0];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 1) = (sbyte) crcstr[1];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 2) = (sbyte) crcstr[2];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 3) = (sbyte) crcstr[3];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 4) = (sbyte) crcstr[4];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 5) = (sbyte) crcstr[5];
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if ((int) ^(sbyte&) ref arrayTypeBy06D + ((int) ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 1) + ((int) ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 2) + ((int) ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 3) + ((int) ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 4) + (int) ^(sbyte&) ((IntPtr) &arrayTypeBy06D + 5) * 64 - 60) * 64 - 60) * 64 - 60) * 64 - 60) * 64 - 60 != (int) crc32)
        throw new Exception("CRC 検算エラー");
    }

    public unsafe uint FullCRC(byte* sData, uint ulDataLength, uint iniParam)
    {
      uint num1 = ulDataLength;
      byte* numPtr = sData;
      uint num2 = iniParam;
      if (ulDataLength != 0U)
      {
        do
        {
          --num1;
          num2 = num2 * 769U + (uint) *numPtr;
          ++numPtr;
        }
        while (num1 != 0U);
      }
      return ~num2;
    }

    public unsafe void PartialCRC(uint* ulCRC, byte* sData, uint ulDataLength)
    {
      uint num = *ulCRC;
      if (ulDataLength != 0U)
      {
        do
        {
          --ulDataLength;
          num = num * 769U + (uint) *sData;
          ++sData;
        }
        while (ulDataLength != 0U);
      }
      *ulCRC = num;
    }

    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECCrc32();
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
