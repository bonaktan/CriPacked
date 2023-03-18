// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CodecOptions
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CodecOptions : IDisposable
  {
    public uint m_window_size;
    public uint m_block_size;
    public uint m_page_size;
    public uint m_rom_type;

    [SpecialName]
    public void op_Assign([In] ref CodecOptions obj0, CodecOptions co)
    {
      uint num1 = 0;
      this.m_window_size = co.m_window_size;
      this.m_block_size = co.m_block_size;
      this.m_page_size = co.m_page_size;
      this.m_rom_type = co.m_rom_type;
      CodecOptions codecOptions = new CodecOptions(this);
      // ISSUE: fault handler
      try
      {
        num1 = 1U;
        obj0 = codecOptions;
      }
      __fault
      {
        if (((int) num1 & 1) != 0)
        {
          uint num2 = num1 & 4294967294U;
          obj0.Dispose();
        }
      }
    }

    public CodecOptions()
    {
      this.m_window_size = 3U;
      this.m_block_size = 17U;
      this.m_page_size = 12U;
      this.m_rom_type = 1U;
    }

    public CodecOptions(CodecOptions obj)
    {
      this.m_window_size = obj.m_window_size;
      this.m_block_size = obj.m_block_size;
      this.m_page_size = obj.m_page_size;
      this.m_rom_type = obj.m_rom_type;
    }

    private void \u007ECodecOptions()
    {
    }

    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECodecOptions();
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
