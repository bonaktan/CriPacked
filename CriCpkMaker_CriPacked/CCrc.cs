// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CCrc
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CCrc : IDisposable
  {
    private CUtf.EnumCrcMode m_crc_mode;
    private bool m_is_fixed;
    private uint m_crc32;
    private ulong m_checksum64;
    private unsafe byte* m_pending_data;
    private uint m_pending_size;

    public uint Crc32
    {
      get
      {
        uint crc32 = this.m_crc32;
        if (!this.m_is_fixed)
          crc32 = (uint) ~(crc32 != 0U ? (int) crc32 : 1);
        return crc32;
      }
    }

    public unsafe ulong CheckSum64
    {
      get
      {
        ulong sum32x2 = this.m_checksum64;
        if (!this.m_is_fixed && this.m_pending_size > 0U)
          sum32x2 = CCheckSum.CalcAddCheckSum64Tsbgp2(sum32x2, (void*) this.m_pending_data, 8U);
        return sum32x2;
      }
    }

    public unsafe CCrc()
    {
      this.m_crc_mode = CUtf.EnumCrcMode.CrcModeStandard;
      this.m_is_fixed = false;
      this.m_crc32 = 0U;
      this.m_checksum64 = 0UL;
      byte* numPtr = Utility.Malloc(8U);
      this.m_pending_data = numPtr;
      // ISSUE: initblk instruction
      __memset((IntPtr) numPtr, 0, 8);
      this.m_pending_size = 0U;
    }

    private unsafe void \u007ECCrc()
    {
      byte* pendingData = this.m_pending_data;
      if ((IntPtr) pendingData == IntPtr.Zero)
        return;
      Utility.Free(pendingData);
      this.m_pending_data = (byte*) 0;
    }

    public void SetMode(CUtf.EnumCrcMode crc_mode) => this.m_crc_mode = crc_mode;

    public CUtf.EnumCrcMode GetMode() => this.m_crc_mode;

    public unsafe void Reset([MarshalAs(UnmanagedType.U1)] bool is_fixed, uint crc32, ulong checksum64)
    {
      this.m_is_fixed = is_fixed;
      CUtf.EnumCrcMode crcMode = this.m_crc_mode;
      if (crcMode == CUtf.EnumCrcMode.CrcModeStandard)
        this.m_crc32 = crc32;
      if (crcMode != CUtf.EnumCrcMode.CrcModeTsbgp2)
        return;
      this.m_checksum64 = checksum64;
      // ISSUE: initblk instruction
      __memset((IntPtr) this.m_pending_data, 0, 8);
      this.m_pending_size = 0U;
    }

    public unsafe void Calc(void* ptr, uint len)
    {
      byte* ptr1 = (byte*) ptr;
      if (len == 0U)
        return;
      if (this.m_is_fixed)
      {
        this.m_is_fixed = false;
        this.Reset(false, 0U, 0UL);
      }
      CUtf.EnumCrcMode crcMode = this.m_crc_mode;
      if (crcMode == CUtf.EnumCrcMode.CrcModeStandard)
      {
        byte* numPtr = (byte*) ptr;
        uint num1 = len;
        uint num2 = this.m_crc32;
        do
        {
          --num1;
          num2 = num2 * 769U + (uint) *numPtr;
          this.m_crc32 = num2;
          ++numPtr;
        }
        while (num1 != 0U);
      }
      if (crcMode != CUtf.EnumCrcMode.CrcModeTsbgp2)
        return;
      uint pendingSize = this.m_pending_size;
      if (pendingSize != 0U)
      {
        uint num3 = 8U - pendingSize;
        if (num3 > len)
          num3 = len;
        ptr1 = (byte*) ((int) num3 + (IntPtr) ptr);
        len -= num3;
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) (this.m_pending_data + (int) this.m_pending_size), (IntPtr) ptr1, (int) num3);
        uint num4 = num3 + this.m_pending_size;
        this.m_pending_size = num4;
        if (num4 == 8U)
        {
          CCrc ccrc = this;
          ccrc.m_checksum64 = CCheckSum.CalcAddCheckSum64Tsbgp2(ccrc.m_checksum64, (void*) this.m_pending_data, 8U);
          // ISSUE: initblk instruction
          __memset((IntPtr) this.m_pending_data, 0, 8);
          this.m_pending_size = 0U;
        }
      }
      if (len <= 0U)
        return;
      uint len1 = len & 4294967288U;
      uint num = len & 7U;
      CCrc ccrc1 = this;
      ccrc1.m_checksum64 = CCheckSum.CalcAddCheckSum64Tsbgp2(ccrc1.m_checksum64, (void*) ptr1, len1);
      if (num <= 0U)
        return;
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) this.m_pending_data, (int) len1 + (IntPtr) ptr1, (int) num);
      this.m_pending_size = num;
    }

    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECCrc();
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
