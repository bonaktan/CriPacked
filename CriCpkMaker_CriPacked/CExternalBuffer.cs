// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CExternalBuffer
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

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
      void* voidPtr1 = \u003CModule\u003E.AllocateMemory((uint) readbufsize);
      Console.Write("Memory allocation for file reading " + (object) (readbufsize / 1024) + "KB ");
      if ((IntPtr) voidPtr1 != IntPtr.Zero)
      {
        Console.WriteLine("OK");
        void* voidPtr2 = \u003CModule\u003E.AllocateMemory((uint) compbufsize);
        Console.Write("Memory allocation for file writing " + (object) (compbufsize / 1024) + "KB ");
        if ((IntPtr) voidPtr2 != IntPtr.Zero)
        {
          Console.WriteLine("OK");
          void* heapbuf = \u003CModule\u003E.AllocateMemory((uint) heapbufsize);
          Console.Write("Memory allocation for compression  " + (object) (heapbufsize / 1024) + "KB ");
          if ((IntPtr) heapbuf != IntPtr.Zero)
          {
            Console.WriteLine("OK");
            return this.SetBuffers(voidPtr1, readbufsize, voidPtr2, compbufsize, heapbuf, heapbufsize);
          }
          Console.WriteLine("NG");
          \u003CModule\u003E.FreeMemory(voidPtr1);
          \u003CModule\u003E.FreeMemory(voidPtr2);
          return false;
        }
        Console.WriteLine("NG");
        \u003CModule\u003E.FreeMemory(voidPtr1);
        return false;
      }
      Console.WriteLine("NG");
      return false;
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
