// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CFileSeparater
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CFileSeparater : IDisposable
  {
    private CFileSeparater.DelegateFunction m_delegate_func;
    private bool m_is_running;
    private bool m_error;
    private CAsyncFile m_reader;
    private CExternalBuffer m_buffer;
    private int m_separates;
    private List<string> m_filepaths;

    public List<string> FilePaths => this.m_filepaths;

    public CFileSeparater()
    {
      this.m_separates = 0;
      CAsyncFile casyncFile = new CAsyncFile();
      this.m_reader = casyncFile;
      casyncFile.ExternalBuffer = (CExternalBuffer) null;
      this.m_buffer = (CExternalBuffer) null;
      this.m_filepaths = (List<string>) null;
    }

    public CFileSeparater(CExternalBuffer buffer)
    {
      this.m_separates = 0;
      CAsyncFile casyncFile = new CAsyncFile();
      this.m_reader = casyncFile;
      casyncFile.ExternalBuffer = buffer;
      this.m_buffer = buffer;
      this.m_filepaths = new List<string>();
    }

    private void \u007ECFileSeparater()
    {
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool Separate(void* buf, int size, int sepaSize)
    {
      if (size <= sepaSize)
        return false;
      int num1 = size;
      this.m_separates = (sepaSize + size - 1) / sepaSize;
      string modulePath = Utility.GetModulePath();
      int num2 = 0;
      if (0 < this.m_separates)
      {
        do
        {
          int num3 = num2;
          string fpath = Path.Combine(modulePath, "data." + num3.ToString("D5"));
          int writesize = num1 >= sepaSize ? sepaSize : num1;
          CAsyncFile casyncFile = new CAsyncFile();
          casyncFile.WriteOpen(fpath);
          casyncFile.Write(buf, (ulong) writesize);
          casyncFile.WaitForComplete();
          buf = (void*) (writesize + (IntPtr) buf);
          casyncFile.Close();
          num1 -= writesize;
          this.m_filepaths.Add(fpath);
          ++num2;
        }
        while (num2 < this.m_separates);
      }
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool Separate(string fpath, int sepaSize)
    {
      if (!this.m_reader.ReadOpen(fpath))
        return false;
      int fileSize = (int) this.m_reader.FileSize;
      if (fileSize <= sepaSize)
        return false;
      this.m_filepaths.Clear();
      string modulePath = Utility.GetModulePath();
      int num1 = (int) ((long) this.m_reader.FileSize + (long) (sepaSize - 1)) / sepaSize;
      this.m_separates = num1;
      int num2 = 0;
      if (0 < num1)
      {
        do
        {
          int num3 = num2;
          string fpath1 = Path.Combine(modulePath, Path.GetFileName(fpath)) + "." + num3.ToString("D5");
          int num4 = fileSize >= sepaSize ? sepaSize : fileSize;
          ulong num5 = (ulong) num4;
          this.m_reader.ReadAlloc(this.m_reader.Position, num5);
          this.m_reader.WaitForComplete();
          CAsyncFile casyncFile = new CAsyncFile();
          casyncFile.WriteOpen(fpath1);
          casyncFile.Write(this.m_reader.ReadBuffer, num5);
          casyncFile.WaitForComplete();
          casyncFile.Close();
          fileSize -= num4;
          this.m_filepaths.Add(fpath1);
          ++num2;
        }
        while (num2 < this.m_separates);
      }
      this.m_reader.Close();
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool Join(string fpath, List<string> paths)
    {
      CAsyncFile casyncFile = new CAsyncFile();
      if (!casyncFile.WriteOpen(fpath, false))
      {
        casyncFile.Close();
        return false;
      }
      List<string>.Enumerator enumerator = paths.GetEnumerator();
      if (enumerator.MoveNext())
      {
        while (this.m_reader.ReadOpen(enumerator.Current))
        {
          this.m_reader.ReadAlloc();
          this.m_reader.WaitForComplete();
          casyncFile.Write(this.m_reader.ReadBuffer, this.m_reader.FileSize);
          casyncFile.WaitForComplete();
          this.m_reader.Close();
          if (!enumerator.MoveNext())
            goto label_6;
        }
        casyncFile.Close();
        return false;
      }
label_6:
      casyncFile.Close();
      return true;
    }

    private static void AsyncEndCallback(IAsyncResult ar) => (ar.AsyncState as CFileSeparater).m_is_running = false;

    public void RemoveSeparatedFiles()
    {
      List<string> filepaths = this.m_filepaths;
      if (filepaths == null)
        return;
      List<string>.Enumerator enumerator = filepaths.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string current = enumerator.Current;
        try
        {
          File.Delete(current);
        }
        catch (Exception ex)
        {
        }
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool RemoveFiles(List<string> paths)
    {
      List<string>.Enumerator enumerator = paths.GetEnumerator();
      if (enumerator.MoveNext())
      {
        do
        {
          File.Delete(enumerator.Current);
        }
        while (enumerator.MoveNext());
      }
      return true;
    }

    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECFileSeparater();
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

    private delegate void DelegateFunction(CFileSeparater obj, string fpath, int sepaSize);
  }
}
