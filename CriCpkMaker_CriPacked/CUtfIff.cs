// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CUtfIff
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CUtfIff : IDisposable
  {
    private unsafe byte* m_iff_bin;
    private unsafe ulong* m_chunksize;

    public unsafe ulong ChunkSize
    {
      get => *this.m_chunksize;
      set => *this.m_chunksize = value;
    }

    public unsafe CUtfIff(sbyte c1, sbyte c2, sbyte c3, sbyte c4)
    {
      void* voidPtr1 = \u003CModule\u003E.malloc(16U);
      this.m_iff_bin = (byte*) voidPtr1;
      *(sbyte*) voidPtr1 = (sbyte) (byte) c1;
      *(sbyte*) ((IntPtr) voidPtr1 + 1) = (sbyte) (byte) c2;
      *(sbyte*) ((IntPtr) voidPtr1 + 2) = (sbyte) (byte) c3;
      *(sbyte*) ((IntPtr) voidPtr1 + 3) = (sbyte) (byte) c4;
      *(sbyte*) ((IntPtr) voidPtr1 + 4) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 5) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 6) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 7) = (sbyte) 0;
      void* voidPtr2 = (void*) ((IntPtr) voidPtr1 + 8);
      *(sbyte*) voidPtr2 = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 9) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 10) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 11) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 12) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 13) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 14) = (sbyte) 0;
      *(sbyte*) ((IntPtr) voidPtr1 + 15) = (sbyte) 0;
      this.m_chunksize = (ulong*) voidPtr2;
    }

    private void \u007ECUtfIff()
    {
    }

    private unsafe void \u0021CUtfIff()
    {
      byte* iffBin = this.m_iff_bin;
      if ((IntPtr) iffBin == IntPtr.Zero)
        return;
      \u003CModule\u003E.free((void*) iffBin);
      this.m_iff_bin = (byte*) 0;
    }

    public unsafe void SetMaskFlag([MarshalAs(UnmanagedType.U1)] bool mask)
    {
      if (mask)
        this.m_iff_bin[4] = (byte) 0;
      else
        this.m_iff_bin[4] = byte.MaxValue;
    }

    public unsafe void* GetFourCCPointer() => (void*) this.m_iff_bin;

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECUtfIff();
      }
      else
      {
        try
        {
          this.\u0021CUtfIff();
        }
        finally
        {
          // ISSUE: explicit finalizer call
          base.Finalize();
        }
      }
    }

    public virtual void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~CUtfIff() => this.Dispose(false);
  }
}
