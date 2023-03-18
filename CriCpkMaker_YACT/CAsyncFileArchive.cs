// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CAsyncFileArchive
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CAsyncFileArchive : IDisposable
  {
    private CMallocHeap m_malloc_heap;
    private CExternalBuffer m_extbuf;
    private EnumCompressCodec m_comp_mode;
    private int m_divided_size;
    private int m_itoc_size;
    private float m_comp_percentage;
    private int m_comp_file_size;
    private bool m_force_compress;
    private readonly CodecOptions m_codec_options;

    public CExternalBuffer ExternalBuffer
    {
      set => this.m_extbuf = value;
    }

    public EnumCompressCodec CompressCodec
    {
      get => this.m_comp_mode;
      set => this.m_comp_mode = value;
    }

    public int DpkDividedSize
    {
      set => this.m_divided_size = value;
    }

    public float CompPercentage
    {
      set => this.m_comp_percentage = value;
    }

    public int CompFileSize
    {
      set => this.m_comp_file_size = value;
    }

    public bool ForceCompress
    {
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_force_compress = value;
    }

    public int ItocSize => this.m_itoc_size;

    public CAsyncFileArchive(CExternalBuffer extbuf)
    {
      CodecOptions codecOptions = new CodecOptions();
      // ISSUE: fault handler
      try
      {
        this.m_codec_options = codecOptions;
        // ISSUE: explicit constructor call
        base.\u002Ector();
        \u003CModule\u003E.criHeap_Initialize();
        this.m_extbuf = extbuf;
        this.m_malloc_heap = (CMallocHeap) null;
      }
      __fault
      {
        this.m_codec_options.Dispose();
      }
    }

    public CAsyncFileArchive(int codec)
    {
      CodecOptions codecOptions = new CodecOptions();
      // ISSUE: fault handler
      try
      {
        this.m_codec_options = codecOptions;
        // ISSUE: explicit constructor call
        base.\u002Ector();
        \u003CModule\u003E.criHeap_Initialize();
        this.m_extbuf = (CExternalBuffer) null;
        this.m_malloc_heap = new CMallocHeap();
        this.m_comp_mode = codec >= 2048 ? EnumCompressCodec.CodecDpk : (EnumCompressCodec) codec;
        this.m_divided_size = codec < 2048 ? 0 : codec;
        this.m_itoc_size = 0;
        this.m_comp_percentage = 100f;
      }
      __fault
      {
        this.m_codec_options.Dispose();
      }
    }

    private void \u007ECAsyncFileArchive()
    {
      if (this.m_malloc_heap != null)
        return;
      this.m_malloc_heap = (CMallocHeap) null;
    }

    private void \u0021CAsyncFileArchive()
    {
      if (this.m_malloc_heap != null)
        return;
      this.m_malloc_heap = (CMallocHeap) null;
    }

    private void Clean()
    {
      if (this.m_malloc_heap != null)
        return;
      this.m_malloc_heap = (CMallocHeap) null;
    }

    private unsafe ulong compress(void* inbuf, ulong insize, void* outbuf, ulong outsize)
    {
      CExternalBuffer extbuf = this.m_extbuf;
      _criheap_struct* heap = extbuf != null ? extbuf.GetCriHeap() : this.m_malloc_heap.CriHeapObj;
      return this.compress_with_heap(inbuf, insize, outbuf, outsize, heap);
    }

    private unsafe ulong compress_with_heap(
      void* inbuf,
      ulong insize,
      void* outbuf,
      ulong outsize,
      _criheap_struct* heap)
    {
      CriLosslessComp* criLosslessCompPtr1 = (CriLosslessComp*) 0;
      // ISSUE: initblk instruction
      __memset((IntPtr) outbuf, 0, (int) (uint) outsize);
      try
      {
        switch (this.m_comp_mode)
        {
          case EnumCompressCodec.CodecLayla:
            criLosslessCompPtr1 = \u003CModule\u003E.CriLosslessCompLayla\u002ECreate(heap);
            break;
          case EnumCompressCodec.CodecLZMA:
            throw new Exception("LZMA Codec is not implements!");
          case EnumCompressCodec.CodecRELC:
            throw new Exception("RELC Codec is not implements!");
        }
        float compPercentage = this.m_comp_percentage;
        *(float*) ((IntPtr) criLosslessCompPtr1 + 8) = compPercentage;
        int compFileSize = this.m_comp_file_size;
        *(int*) ((IntPtr) criLosslessCompPtr1 + 12) = compFileSize;
      }
      catch (Exception ex)
      {
        string message = "CriLosslessComp???::Create " + ex.ToString() + " Codec = " + this.m_comp_mode.ToString();
        Console.WriteLine(message);
        throw new Exception(message);
      }
      if ((IntPtr) criLosslessCompPtr1 == IntPtr.Zero)
      {
        string message = "codec == NULL";
        Console.WriteLine(message);
        throw new Exception(message);
      }
      ulong ret_size;
      try
      {
        bool flag = false;
        int in_size = (int) insize;
        byte num1;
        if (in_size > *(int*) ((IntPtr) criLosslessCompPtr1 + 12))
        {
          num1 = (byte) 1;
          CriLosslessComp* criLosslessCompPtr2 = criLosslessCompPtr1;
          void* voidPtr1 = inbuf;
          long num2 = (long) insize;
          void* voidPtr2 = outbuf;
          long num3 = (long) outsize;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          ret_size = __calli((__FnPtr<ulong (IntPtr, byte*, ulong, byte*, ulong)>) *(int*) (*(int*) criLosslessCompPtr1 + 4))((ulong) criLosslessCompPtr2, (byte*) voidPtr1, (ulong) num2, (byte*) voidPtr2, (IntPtr) num3);
          if (\u003CModule\u003E.CriLosslessComp\u002ECheckDoCompPercentage(criLosslessCompPtr1, in_size, (int) ret_size))
          {
            flag = true;
            goto label_15;
          }
        }
        else
          num1 = (byte) 0;
        uint num4 = (uint) insize;
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) outbuf, (IntPtr) inbuf, (int) num4);
        ret_size = (ulong) num4;
      }
      catch (Exception ex)
      {
        CriLosslessComp* criLosslessCompPtr3 = criLosslessCompPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        __calli((__FnPtr<void (IntPtr)>) *(int*) *(int*) criLosslessCompPtr3)((IntPtr) criLosslessCompPtr3);
        string message = "CriLosslessCompLayla::Create" + ex.ToString();
        Console.WriteLine(message);
        throw new Exception(message);
      }
label_15:
      try
      {
        CriLosslessComp* criLosslessCompPtr4 = criLosslessCompPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        __calli((__FnPtr<void (IntPtr)>) *(int*) *(int*) criLosslessCompPtr4)((IntPtr) criLosslessCompPtr4);
      }
      catch (Exception ex)
      {
        string message = "codec->Destroy {" + ex.ToString() + "}";
        Console.WriteLine(message);
        throw new Exception(message);
      }
      return ret_size;
    }

    private unsafe ulong compressDpk(
      void* inbuf,
      ulong insize,
      void* outbuf,
      ulong outsize,
      [MarshalAs(UnmanagedType.U1)] bool forceBuild)
    {
      CDpkMaker cdpkMaker = new CDpkMaker(this.m_extbuf);
      if (this.m_divided_size <= 0)
        throw new Exception("Invalid divided size in dpk");
      bool flag = this.m_comp_mode == EnumCompressCodec.CodecDpkForceCrc;
      cdpkMaker.ForceCrc = flag;
      IntPtr optr = new IntPtr(outbuf);
      IntPtr ptr = new IntPtr(inbuf);
      cdpkMaker.StartToBuild(ptr, (int) insize, optr, (int) outsize, this.m_divided_size);
      cdpkMaker.WaitForComplete();
      this.m_itoc_size = cdpkMaker.ItocSize;
      long dpkFileSize = (long) cdpkMaker.DpkFileSize;
      if (cdpkMaker == null)
        return (ulong) dpkFileSize;
      cdpkMaker.Dispose();
      return (ulong) dpkFileSize;
    }

    public unsafe ulong Compress(void* inbuf, ulong insize, void* outbuf, ulong outsize)
    {
      ulong num = 0;
      switch (this.m_comp_mode)
      {
        case EnumCompressCodec.CodecLayla:
        case EnumCompressCodec.CodecLZMA:
        case EnumCompressCodec.CodecRELC:
          num = this.compress(inbuf, insize, outbuf, outsize);
          break;
        case EnumCompressCodec.CodecDpk:
          num = this.compressDpk(inbuf, insize, outbuf, outsize, false);
          break;
        case EnumCompressCodec.CodecDpkForceCrc:
          num = this.compressDpk(inbuf, insize, outbuf, outsize, true);
          break;
      }
      return num;
    }

    public unsafe ulong Decompress(void* inbuf, ulong insize, void* outbuf, ulong outsize)
    {
      ulong num = 0;
      switch (this.m_comp_mode)
      {
        case EnumCompressCodec.CodecLayla:
          num = this.decompress(inbuf, insize, outbuf, outsize);
          break;
        case EnumCompressCodec.CodecDpk:
          throw new Exception("error! cnannot decode ");
      }
      return num;
    }

    private unsafe ulong decompress(void* inbuf, ulong insize, void* outbuf, ulong outsize)
    {
      bool flag = false;
      if (this.m_malloc_heap == null)
      {
        this.m_malloc_heap = new CMallocHeap();
        flag = true;
      }
      CriLosslessDecomp* criLosslessDecompPtr1 = \u003CModule\u003E.CriLosslessDecompLayla\u002ECreate(this.m_malloc_heap.CriHeapObj);
      try
      {
        CriLosslessDecomp* criLosslessDecompPtr2 = criLosslessDecompPtr1;
        void* voidPtr1 = inbuf;
        long num1 = (long) insize;
        void* voidPtr2 = outbuf;
        long num2 = (long) outsize;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        return __calli((__FnPtr<ulong (IntPtr, byte*, ulong, byte*, ulong)>) *(int*) (*(int*) criLosslessDecompPtr1 + 8))((ulong) criLosslessDecompPtr2, (byte*) voidPtr1, (ulong) num1, (byte*) voidPtr2, (IntPtr) num2);
      }
      catch (Exception ex)
      {
        throw new ApplicationException("failed decompress : " + ex.Message);
      }
      finally
      {
        CriLosslessDecomp* criLosslessDecompPtr3 = criLosslessDecompPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        __calli((__FnPtr<void (IntPtr)>) *(int*) *(int*) criLosslessDecompPtr3)((IntPtr) criLosslessDecompPtr3);
        if (flag)
        {
          this.m_malloc_heap?.Dispose();
          this.m_malloc_heap = (CMallocHeap) null;
        }
      }
    }

    public static unsafe ulong DecompressStatic(
      void* inbuf,
      ulong insize,
      void* outbuf,
      ulong outsize,
      EnumCompressCodec comp_mode)
    {
      CMallocHeap cmallocHeap = new CMallocHeap();
      if (insize >= 4UL && *(int*) inbuf == 541806659)
        return CAsyncFileArchive.DecompressDpk(inbuf, insize, outbuf, outsize);
      CriLosslessDecomp* criLosslessDecompPtr1;
      if (comp_mode != EnumCompressCodec.CodecLayla)
      {
        if (comp_mode == EnumCompressCodec.CodecLZMA)
          throw new Exception("LZMA Codec is not implements!");
        if (comp_mode == EnumCompressCodec.CodecRELC)
          throw new Exception("RELC Codec is not implements!");
      }
      else
        criLosslessDecompPtr1 = \u003CModule\u003E.CriLosslessDecompLayla\u002ECreate(cmallocHeap.CriHeapObj);
      try
      {
        CriLosslessDecomp* criLosslessDecompPtr2 = criLosslessDecompPtr1;
        void* voidPtr1 = inbuf;
        long num1 = (long) insize;
        void* voidPtr2 = outbuf;
        long num2 = (long) outsize;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        return __calli((__FnPtr<ulong (IntPtr, byte*, ulong, byte*, ulong)>) *(int*) (*(int*) criLosslessDecompPtr1 + 8))((ulong) criLosslessDecompPtr2, (byte*) voidPtr1, (ulong) num1, (byte*) voidPtr2, (IntPtr) num2);
      }
      catch (Exception ex)
      {
        throw new ApplicationException("failed decompress : " + ex.Message);
      }
      finally
      {
        CriLosslessDecomp* criLosslessDecompPtr3 = criLosslessDecompPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        __calli((__FnPtr<void (IntPtr)>) *(int*) *(int*) criLosslessDecompPtr3)((IntPtr) criLosslessDecompPtr3);
        cmallocHeap?.Dispose();
      }
    }

    private static unsafe ulong DecompressDpk(
      void* inbuf,
      ulong insize,
      void* outbuf,
      ulong outsize)
    {
      CAsyncFile casyncFile1 = new CAsyncFile();
      string fpath = "tmp.cpk";
      if (!casyncFile1.WriteOpen(fpath, false))
        return 0;
      casyncFile1.Write(inbuf, insize, CAsyncFile.WriteMode.Normal);
      casyncFile1.Close();
      CpkMaker cpkMaker = new CpkMaker();
      if (!cpkMaker.AnalyzeCpkFile(fpath))
        return 0;
      cpkMaker.StartToExtract("tmpext");
      Status status = cpkMaker.Execute();
      if (status != Status.Error)
      {
        while (status != Status.Complete)
        {
          status = cpkMaker.Execute();
          if (status == Status.Error)
            goto label_7;
        }
        sbyte* readpointer = (sbyte*) outbuf;
        ulong num = 0;
        List<CFileInfo>.Enumerator enumerator = cpkMaker.FileData.FileInfos.GetEnumerator();
        if (enumerator.MoveNext())
        {
          do
          {
            CFileInfo current = enumerator.Current;
            CAsyncFile casyncFile2 = new CAsyncFile();
            if (casyncFile2.ReadOpen(current.LocalFilePath))
            {
              casyncFile2.Read((void*) readpointer);
              casyncFile2.WaitForComplete();
              readpointer = (sbyte*) ((int) casyncFile2.FileSize + (IntPtr) readpointer);
              num += casyncFile2.FileSize;
              casyncFile2.Close();
            }
            else
              goto label_11;
          }
          while (enumerator.MoveNext());
          goto label_12;
label_11:
          return 0;
        }
label_12:
        cpkMaker?.Dispose();
        return num;
      }
label_7:
      return 0;
    }

    private static unsafe ulong GetOriginalFileSize(
      void* compdata,
      int dtsize,
      int* org_size,
      EnumCompressCodec codec)
    {
      return 0;
    }

    public void SetCompressOptions(CodecOptions co)
    {
      CodecOptions codecOptions;
      this.m_codec_options.op_Assign(ref codecOptions, co);
      // ISSUE: explicit non-virtual call
      __nonvirtual (codecOptions.Dispose());
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        try
        {
          this.\u007ECAsyncFileArchive();
        }
        finally
        {
          this.m_codec_options.Dispose();
        }
      }
      else
      {
        try
        {
          this.\u0021CAsyncFileArchive();
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

    ~CAsyncFileArchive() => this.Dispose(false);
  }
}
