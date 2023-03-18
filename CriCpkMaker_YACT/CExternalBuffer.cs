// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CExternalBuffer
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CExternalBuffer : IDisposable
  {
    private unsafe void* m_read_buffer;
    private unsafe void* m_comp_buffer;
    private unsafe void* m_heap_buffer;
    private int m_read_buffer_size;
    private int m_comp_buffer_size;
    private int m_heap_buffer_size;
    private unsafe _criheap_struct* m_heap;

    public unsafe void* ReadBuffer => this.m_read_buffer;

    public unsafe void* CompBuffer => this.m_comp_buffer;

    public int ReadBufferSize => this.m_read_buffer_size;

    public int CompBufferSize => this.m_comp_buffer_size;

    public unsafe CExternalBuffer()
    {
      this.m_read_buffer = (void*) 0;
      this.m_comp_buffer = (void*) 0;
      this.m_heap_buffer = (void*) 0;
      this.m_heap = (_criheap_struct*) 0;
    }

    private void \u007ECExternalBuffer() => this.ClearBuffer();

    private void \u0021CExternalBuffer() => this.ClearBuffer();

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool SetBuffers(
      void* readbuf,
      int readbufsize,
      void* compbuf,
      int compbufsize,
      void* heapbuf,
      int heapbufsize)
    {
      this.m_read_buffer = readbuf;
      this.m_read_buffer_size = readbufsize;
      this.m_comp_buffer = compbuf;
      this.m_comp_buffer_size = compbufsize;
      this.m_heap_buffer = heapbuf;
      this.m_heap_buffer_size = heapbufsize;
      _criheap_struct* criheapStructPtr = \u003CModule\u003E.criHeap_Create(heapbuf, heapbufsize);
      this.m_heap = criheapStructPtr;
      return (IntPtr) this.m_read_buffer != IntPtr.Zero && (IntPtr) this.m_comp_buffer != IntPtr.Zero && (IntPtr) this.m_heap_buffer != IntPtr.Zero && (IntPtr) criheapStructPtr != IntPtr.Zero;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool SetBuffers(int readbufsize, int compbufsize, int heapbufsize)
    {
      void* readbuf = \u003CModule\u003E.AllocateMemory((uint) readbufsize);
      Console.Write("Memory allocation for file reading " + (object) (readbufsize / 1024) + "KB ");
      if ((IntPtr) readbuf != IntPtr.Zero)
        Console.WriteLine("OK");
      else
        Console.WriteLine("NG");
      void* compbuf = \u003CModule\u003E.AllocateMemory((uint) compbufsize);
      Console.Write("Memory allocation for file writing " + (object) (compbufsize / 1024) + "KB ");
      if ((IntPtr) compbuf != IntPtr.Zero)
        Console.WriteLine("OK");
      else
        Console.WriteLine("NG");
      void* heapbuf = \u003CModule\u003E.AllocateMemory((uint) heapbufsize);
      Console.Write("Memory allocation for compression  " + (object) (heapbufsize / 1024) + "KB ");
      if ((IntPtr) heapbuf != IntPtr.Zero)
        Console.WriteLine("OK");
      else
        Console.WriteLine("NG");
      return this.SetBuffers(readbuf, readbufsize, compbuf, compbufsize, heapbuf, heapbufsize);
    }

    public unsafe void ClearBuffer()
    {
      _criheap_struct* heap = this.m_heap;
      if ((IntPtr) heap != IntPtr.Zero)
      {
        \u003CModule\u003E.criHeap_Destroy(heap);
        this.m_heap = (_criheap_struct*) 0;
      }
      void* compBuffer = this.m_comp_buffer;
      if ((IntPtr) compBuffer != IntPtr.Zero)
      {
        \u003CModule\u003E.FreeMemory(compBuffer);
        this.m_comp_buffer = (void*) 0;
      }
      void* readBuffer = this.m_read_buffer;
      if ((IntPtr) readBuffer != IntPtr.Zero)
      {
        \u003CModule\u003E.FreeMemory(readBuffer);
        this.m_read_buffer = (void*) 0;
      }
      void* heapBuffer = this.m_heap_buffer;
      if ((IntPtr) heapBuffer == IntPtr.Zero)
        return;
      \u003CModule\u003E.FreeMemory(heapBuffer);
      this.m_heap_buffer = (void*) 0;
    }

    public unsafe _criheap_struct* GetCriHeap() => this.m_heap;

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECExternalBuffer();
      }
      else
      {
        try
        {
          this.\u0021CExternalBuffer();
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

    ~CExternalBuffer() => this.Dispose(false);
  }
}
