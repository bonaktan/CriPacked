// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CDpkMaker
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace CriCpkMaker
{
  public class CDpkMaker : IDisposable
  {
    private CDpkMaker.DelegateCompressFile m_delegate_comp_file;
    private CDpkMaker.DelegateCompressMem m_delegate_comp_mem;
    private bool m_is_running;
    private bool m_error;
    private bool m_succeed;
    private bool m_force_crc;
    private CExternalBuffer m_buffer;
    private CFileSeparater m_sepa;
    private int m_fsize;
    private uint m_itoc_size;

    public bool IsFinish
    {
      [return: MarshalAs(UnmanagedType.U1)] get => !this.m_is_running;
    }

    public List<string> FilePaths => this.m_sepa.FilePaths;

    public int DpkFileSize => this.m_fsize;

    public int ItocSize => (int) this.m_itoc_size;

    public bool ForceCrc
    {
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_force_crc = value;
    }

    public CDpkMaker(CExternalBuffer buffer)
    {
      this.m_is_running = false;
      this.m_error = false;
      this.m_sepa = new CFileSeparater(buffer);
      this.m_buffer = buffer;
      this.m_fsize = 0;
      this.m_itoc_size = 0U;
      this.m_force_crc = false;
    }

    private void \u007ECDpkMaker()
    {
    }

    private void \u0021CDpkMaker()
    {
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool StartToBuild(IntPtr ptr, int size, IntPtr optr, int osize, int sepaSize)
    {
      if (this.m_is_running)
        return false;
      this.m_error = false;
      this.m_is_running = true;
      this.m_delegate_comp_mem = new CDpkMaker.DelegateCompressMem(CDpkMaker.AsyncCompressMem);
      this.m_delegate_comp_mem.BeginInvoke((object) this, ptr, size, optr, osize, sepaSize, new AsyncCallback(CDpkMaker.AsyncEndCallback), (object) this);
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool StartToBuild(string fpath, string dpkFpath, int sepaSize)
    {
      if (this.m_is_running)
        return false;
      this.m_error = false;
      this.m_is_running = true;
      this.m_delegate_comp_file = new CDpkMaker.DelegateCompressFile(CDpkMaker.AsyncCompressFile);
      this.m_delegate_comp_file.BeginInvoke(this, fpath, dpkFpath, sepaSize, new AsyncCallback(CDpkMaker.AsyncEndCallback), (object) this);
      return true;
    }

    public static unsafe void AsyncDecompressFile(
      CDpkMaker dpkMaker,
      string dpkFname,
      string extPath)
    {
      string str = "cpkmakertmp";
      CpkMaker cpkMaker = new CpkMaker(true);
      cpkMaker.ExternalBuffer = dpkMaker.m_buffer;
      cpkMaker.AnalyzeCpkFile(dpkFname);
      cpkMaker.StartToExtract(str);
      do
        ;
      while (cpkMaker.Execute() != Status.Complete);
      CAsyncFile casyncFile1 = new CAsyncFile();
      CAsyncFile casyncFile2 = new CAsyncFile();
      casyncFile1.WriteOpen(extPath, false);
      int num1 = 0;
      int num2 = 0;
      string fpath1 = Path.Combine(str, "ID" + num2.ToString("D5"));
      if (casyncFile2.ReadOpen(fpath1))
      {
        string fpath2;
        do
        {
          casyncFile2.ReadAlloc();
          casyncFile2.WaitForComplete();
          casyncFile2.Close();
          casyncFile1.Write(casyncFile2.ReadBuffer, casyncFile2.FileSize, CAsyncFile.WriteMode.Normal);
          casyncFile1.WaitForComplete();
          ++num1;
          num2 = num1;
          fpath2 = Path.Combine(str, "ID" + num2.ToString("D5"));
        }
        while (casyncFile2.ReadOpen(fpath2));
      }
      casyncFile2.Close();
      casyncFile1.Close();
    }

    private static void AsyncCompressFile(
      CDpkMaker dpkMaker,
      string fpath,
      string dpkFpath,
      int sepaSize)
    {
      dpkMaker.m_error = false;
      dpkMaker.m_succeed = false;
      if (!dpkMaker.m_sepa.Separate(fpath, sepaSize))
      {
        dpkMaker.m_error = true;
      }
      else
      {
        dpkMaker.MakeCpk(dpkFpath);
        if (!dpkMaker.m_error)
        {
          if (new FileInfo(fpath).Length > (long) dpkMaker.m_fsize)
            dpkMaker.m_succeed = true;
          else
            File.Delete(dpkFpath);
        }
        dpkMaker.m_sepa.RemoveSeparatedFiles();
      }
    }

    private static unsafe void AsyncCompressMem(
      object obj,
      IntPtr ptr,
      int size,
      IntPtr optr,
      int osize,
      int sepaSize)
    {
      CDpkMaker cdpkMaker = obj as CDpkMaker;
      void* pointer1 = ptr.ToPointer();
      void* pointer2 = optr.ToPointer();
      string str1 = "tmp.dpk";
      cdpkMaker.m_error = false;
      cdpkMaker.m_succeed = false;
      if (!cdpkMaker.m_sepa.Separate(pointer1, size, sepaSize))
      {
        cdpkMaker.m_error = true;
      }
      else
      {
        cdpkMaker.MakeCpk(str1);
        if (!cdpkMaker.m_error && File.Exists(str1))
        {
          if (!cdpkMaker.m_force_crc && cdpkMaker.m_fsize > size)
          {
            cdpkMaker.m_error = true;
            return;
          }
          sbyte* str2 = Utility.AllocCharString(str1);
          _iobuf* iobufPtr;
          \u003CModule\u003E.fopen_s(&iobufPtr, str2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_02JDPG\u0040rb\u003F\u0024AA\u0040);
          Utility.FreeCharString(str2);
          int num = (int) \u003CModule\u003E.fread(pointer2, (uint) cdpkMaker.m_fsize, 1U, iobufPtr);
          \u003CModule\u003E.fclose(iobufPtr);
          File.Delete(str1);
        }
        cdpkMaker.m_sepa.RemoveSeparatedFiles();
      }
    }

    public void MakeCpk(string fpath)
    {
      CpkMaker cpkMaker1 = new CpkMaker(true);
      cpkMaker1.ExternalBuffer = this.m_buffer;
      cpkMaker1.ForceCompress = true;
      cpkMaker1.CompressCodec = EnumCompressCodec.CodecLayla;
      cpkMaker1.EnableCrc = this.m_force_crc;
      int index = 0;
      if (0 < this.m_sepa.FilePaths.Count)
      {
        do
        {
          string filePath = this.m_sepa.FilePaths[index];
          CpkMaker cpkMaker2 = cpkMaker1;
          string str = filePath;
          int id = index;
          cpkMaker2.AddFile(str, str, (uint) id, true);
          ++index;
        }
        while (index < this.m_sepa.FilePaths.Count);
      }
      cpkMaker1.EnableTopTocInfo = true;
      cpkMaker1.CpkFileMode = CpkMaker.EnumCpkFileMode.ModeId;
      cpkMaker1.StartToBuild(fpath);
      Status status = cpkMaker1.Execute();
      if (status != Status.Complete)
      {
        while (status != Status.Error)
        {
          status = cpkMaker1.Execute();
          if (status == Status.Complete)
            goto label_6;
        }
        this.m_fsize = 0;
        this.m_error = true;
      }
label_6:
      this.m_itoc_size = cpkMaker1.ItocSize;
      cpkMaker1?.Dispose();
      if (this.m_error)
        this.m_fsize = 0;
      else
        this.m_fsize = (int) new FileInfo(fpath).Length;
    }

    private static long GetFileSize(string fpath) => new FileInfo(fpath).Length;

    private static void AsyncEndCallback(IAsyncResult ar) => (ar.AsyncState as CDpkMaker).m_is_running = false;

    public void WaitForComplete()
    {
      while (this.m_is_running)
        Thread.Sleep(10);
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECDpkMaker();
      }
      else
      {
        try
        {
          this.\u0021CDpkMaker();
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

    ~CDpkMaker() => this.Dispose(false);

    private delegate void DelegateCompressFile(
      CDpkMaker dpkMaker,
      string fpath,
      string dpkFpath,
      int sepaSize);

    private delegate void DelegateCompressMem(
      object obj,
      IntPtr ptr,
      int size,
      IntPtr optr,
      int osize,
      int sepaSize);
  }
}
