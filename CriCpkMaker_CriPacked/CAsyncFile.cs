// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CAsyncFile
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace CriCpkMaker
{
  public class CAsyncFile : IDisposable
  {
    private CAsyncFile m_reader;
    private CAsyncFile m_writer;
    private CAsyncFile.Delegate m_rw_delegate;
    private CAsyncFile.Delegate m_copy_delegate;
    private ulong m_filesize;
    private ulong m_read_size;
    private ulong m_read_offset;
    private unsafe void* m_write_pointer;
    private ulong m_write_size;
    private CAsyncFile.AsyncStatus m_stat;
    private unsafe _iobuf* m_file;
    private unsafe void* m_data_pointer;
    private CAsyncFile.OpenMode m_open_mode;
    private CAsyncFile.WriteMode m_write_mode;
    private CAsyncFile.ReadMode m_read_mode;
    private CAsyncFile.CopyMode m_copy_mode;
    private CAsyncFileArchive m_archive;
    private ulong m_compressed_size;
    private ulong m_decompressed_size;
    private bool m_copied;
    private bool m_use_delegate;
    private unsafe void* m_zeromem;
    private string m_filename;
    private CExternalBuffer m_extbuf;
    private unsafe void* m_dpkbuf;
    private bool m_enable_crc_check;
    private bool m_enable_random_padding;
    private ulong m_pre_position;
    private ulong m_compress_codec_alignment;
    public CCrc Ccrc;

    public ulong FileSize => this.m_filesize;

    public CAsyncFile.AsyncStatus Status
    {
      get => this.m_stat;
      set => this.m_stat = value;
    }

    public CAsyncFile.AsyncStatus CopyStatus
    {
      get
      {
        CAsyncFile.AsyncStatus stat1 = this.m_stat;
        if (stat1 == CAsyncFile.AsyncStatus.Complete && this.m_reader.m_stat == CAsyncFile.AsyncStatus.Complete)
          return CAsyncFile.AsyncStatus.Complete;
        if (stat1 != CAsyncFile.AsyncStatus.Error)
        {
          CAsyncFile.AsyncStatus stat2 = this.m_reader.m_stat;
          if (stat2 != CAsyncFile.AsyncStatus.Error)
            return stat1 == CAsyncFile.AsyncStatus.Stop && stat2 == CAsyncFile.AsyncStatus.Stop ? CAsyncFile.AsyncStatus.Stop : CAsyncFile.AsyncStatus.Busy;
        }
        return CAsyncFile.AsyncStatus.Error;
      }
    }

    public unsafe ulong Position
    {
      get
      {
        _iobuf* file = this.m_file;
        return (IntPtr) file == IntPtr.Zero ? 0UL : (ulong) \u003CModule\u003E._ftelli64(file);
      }
      set => \u003CModule\u003E._fseeki64(this.m_file, (long) value, 0);
    }

    public unsafe void* ReadBuffer => this.m_data_pointer;

    public string Filename => this.m_filename;

    public ulong CompressedSize => this.m_compressed_size;

    public bool UseDelegate
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_use_delegate;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_use_delegate = value;
    }

    public CExternalBuffer ExternalBuffer
    {
      set
      {
        this.m_extbuf = value;
        this.m_archive.ExternalBuffer = value;
      }
    }

    public bool EnableCrcCheck
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_crc_check;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_crc_check = value;
    }

    public unsafe bool RandomPadding
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_random_padding;
      [param: MarshalAs(UnmanagedType.U1)] set
      {
        this.m_enable_random_padding = value;
        if (value)
          return;
        void* zeromem = this.m_zeromem;
        if ((IntPtr) zeromem == IntPtr.Zero)
          return;
        __memset((IntPtr) zeromem, 0, 32768);
      }
    }

    public EnumCompressCodec CompressCodec
    {
      get => this.m_archive.CompressCodec;
      set => this.m_archive.CompressCodec = value;
    }

    public int DpkDividedSize
    {
      set => this.m_archive.DpkDividedSize = value;
    }

    public float CompPercentage
    {
      set => this.m_archive.CompPercentage = value;
    }

    public int CompFileSize
    {
      set => this.m_archive.CompFileSize = value;
    }

    public bool ForceCompress
    {
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_archive.ForceCompress = value;
    }

    public int DpkItocSize => this.m_archive.ItocSize;

    public ulong PreReadWritePosition => this.m_writer.m_pre_position;

    public ulong CompressCodecAlignment
    {
      set => this.m_compress_codec_alignment = value;
      get => this.m_compress_codec_alignment;
    }

    public string CacheDirectory
    {
      get => this.m_archive.CacheDirectory;
      set => this.m_archive.CacheDirectory = value;
    }

    public long CacheLimitSize
    {
      get => this.m_archive.CacheLimitSize;
      set => this.m_archive.CacheLimitSize = value;
    }

    public CAsyncFile() => this.initialize(0);

    public CAsyncFile(int codec) => this.initialize(codec);

    private unsafe void initialize(int codec)
    {
      this.m_enable_crc_check = false;
      this.m_enable_random_padding = false;
      this.m_extbuf = (CExternalBuffer) null;
      this.m_dpkbuf = (void*) 0;
      this.m_archive = new CAsyncFileArchive(codec);
      this.m_rw_delegate = new CAsyncFile.Delegate(CAsyncFile.AsyncReadWriteFunction);
      this.m_copy_delegate = new CAsyncFile.Delegate(CAsyncFile.AsyncCopyFunction2);
      this.Ccrc = new CCrc();
      this.m_use_delegate = false;
      this.m_stat = CAsyncFile.AsyncStatus.Stop;
      this.m_file = (_iobuf*) 0;
      this.m_compress_codec_alignment = 0UL;
      void* voidPtr = \u003CModule\u003E.AllocateMemory(32768U);
      this.m_zeromem = voidPtr;
      // ISSUE: initblk instruction
      __memset((IntPtr) voidPtr, 0, 32768);
    }

    private void \u007ECAsyncFile() => this.finalize();

    private void \u0021CAsyncFile() => this.finalize();

    private unsafe void finalize()
    {
      this.clearBuffer();
      this.Close();
      CCrc ccrc = this.Ccrc;
      if (ccrc != null)
      {
        ccrc.Dispose();
        this.Ccrc = (CCrc) null;
      }
      CAsyncFileArchive archive = this.m_archive;
      if (archive != null)
      {
        archive.Dispose();
        this.m_archive = (CAsyncFileArchive) null;
      }
      if ((MulticastDelegate) this.m_rw_delegate != (MulticastDelegate) null)
        this.m_rw_delegate = (CAsyncFile.Delegate) null;
      \u003CModule\u003E.FreeMemory(this.m_zeromem);
      this.m_zeromem = (void*) 0;
    }

    private unsafe void clearBuffer()
    {
      void* dataPointer = this.m_data_pointer;
      if ((IntPtr) dataPointer == IntPtr.Zero)
        return;
      void* ptr = dataPointer;
      if (this.m_extbuf == null)
        \u003CModule\u003E.FreeMemory(ptr);
      else if (this.m_dpkbuf == ptr)
      {
        \u003CModule\u003E.FreeMemory(ptr);
        this.m_dpkbuf = (void*) 0;
      }
      this.m_data_pointer = (void*) 0;
    }

    private unsafe void clearMember()
    {
      this.m_read_mode = CAsyncFile.ReadMode.Normal;
      this.m_write_mode = CAsyncFile.WriteMode.Normal;
      this.m_data_pointer = (void*) 0;
      this.m_decompressed_size = 0UL;
      this.m_compressed_size = 0UL;
      this.m_write_pointer = (void*) 0;
      this.m_reader = (CAsyncFile) null;
      this.Ccrc.Reset(false, 0U, 0UL);
    }

    private static void DebugWriteLine(string str)
    {
    }

    private unsafe ulong GetPosition(_iobuf* file) => (ulong) \u003CModule\u003E._ftelli64(file);

    private unsafe void* allocBuffer(ulong bufsize)
    {
      void* voidPtr = \u003CModule\u003E.AllocateMemory((uint) bufsize + 64U);
      if ((IntPtr) voidPtr != IntPtr.Zero)
        return voidPtr;
      string str = "Internal error : failed malloc in allocBuffer. size=" + bufsize.ToString();
      this.m_stat = CAsyncFile.AsyncStatus.Error;
      return (void*) 0;
    }

    public void ReleaseBuffer() => this.clearBuffer();

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool Open(string fpath, CAsyncFile.OpenMode opmode, [MarshalAs(UnmanagedType.U1)] bool writeAppend)
    {
      this.Close();
      this.m_filename = fpath;
      sbyte* numPtr;
      switch (opmode)
      {
        case CAsyncFile.OpenMode.Read:
          string str1 = "read open : " + fpath;
          numPtr = (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03MFEIFJCN\u0040rbS\u003F\u0024AA\u0040;
          break;
        case CAsyncFile.OpenMode.Write:
          if (!writeAppend)
          {
            string str2 = "write open : " + fpath;
            numPtr = (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03PCJGKJBP\u0040wbS\u003F\u0024AA\u0040;
            break;
          }
          string str3 = "write open (append): " + fpath;
          numPtr = (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03HMFOOINA\u0040r\u003F\u0024CLb\u003F\u0024AA\u0040;
          break;
      }
      sbyte* str4 = Utility.AllocCharString(fpath);
      _iobuf* iobufPtr;
      for (int index = 0; index < 10; ++index)
      {
        try
        {
          \u003CModule\u003E.fopen_s(&iobufPtr, str4, numPtr);
        }
        catch (Exception ex)
        {
          string str5 = "fopen error " + ex.Message;
        }
        if ((IntPtr) iobufPtr == IntPtr.Zero)
        {
          Thread.Sleep(500);
          Console.WriteLine("Retry");
        }
        else
          break;
      }
      Utility.FreeCharString(str4);
      if ((IntPtr) iobufPtr == IntPtr.Zero)
        return false;
      \u003CModule\u003E._fseeki64(iobufPtr, 0L, 2);
      this.m_filesize = (ulong) \u003CModule\u003E._ftelli64(iobufPtr);
      this.m_file = iobufPtr;
      \u003CModule\u003E._fseeki64(iobufPtr, 0L, 0);
      this.m_stat = CAsyncFile.AsyncStatus.Stop;
      return true;
    }

    public unsafe void Close()
    {
      if (this.m_stat == CAsyncFile.AsyncStatus.Busy)
        this.WaitForComplete();
      if ((IntPtr) this.m_file == IntPtr.Zero)
        return;
      string str = "close : " + this.m_filename;
      \u003CModule\u003E.fclose(this.m_file);
      this.m_file = (_iobuf*) 0;
      this.m_filename = (string) null;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool SeekToEnd()
    {
      _iobuf* file = this.m_file;
      if ((IntPtr) file == IntPtr.Zero)
        return false;
      \u003CModule\u003E._fseeki64(file, 0L, 2);
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool ReadOpen(string fpath)
    {
      if (!File.Exists(fpath) || (IntPtr) this.m_file != IntPtr.Zero)
        return false;
      this.clearMember();
      this.m_open_mode = CAsyncFile.OpenMode.Read;
      return this.Open(fpath, CAsyncFile.OpenMode.Read, false);
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool WriteOpen(string fpath)
    {
      byte num;
      if ((IntPtr) this.m_file != IntPtr.Zero)
      {
        num = (byte) 0;
      }
      else
      {
        this.clearMember();
        this.m_open_mode = CAsyncFile.OpenMode.Write;
        num = (byte) this.Open(fpath, CAsyncFile.OpenMode.Write, false);
      }
      return (bool) num;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool WriteOpen(string fpath, [MarshalAs(UnmanagedType.U1)] bool appendWrite)
    {
      if ((IntPtr) this.m_file != IntPtr.Zero)
        return false;
      this.clearMember();
      this.m_open_mode = CAsyncFile.OpenMode.Write;
      return this.Open(fpath, CAsyncFile.OpenMode.Write, appendWrite);
    }

    public ulong GetCompressedSize()
    {
      if (this.m_write_mode != CAsyncFile.WriteMode.TyrCompress)
        throw new InvalidOperationException("Invalid mode.");
      return this.m_compressed_size;
    }

    public unsafe int GetCompressedSize(string fpath, uint* crc32, ulong* checksum64)
    {
      CAsyncFile casyncFile = new CAsyncFile();
      if (!casyncFile.ReadOpen(fpath))
        return -1;
      ulong filesize1 = casyncFile.m_filesize;
      casyncFile.ReadAlloc(0UL, filesize1, CAsyncFile.ReadMode.Normal, filesize1);
      casyncFile.WaitForComplete();
      long num1 = (long) casyncFile.m_filesize * 2L;
      CExternalBuffer extbuf = this.m_extbuf;
      void* voidPtr;
      if (extbuf == null)
      {
        voidPtr = this.allocBuffer((ulong) num1);
      }
      else
      {
        if (num1 > (long) extbuf.CompBufferSize)
          return -1;
        voidPtr = this.m_extbuf.CompBuffer;
      }
      if ((IntPtr) voidPtr == IntPtr.Zero)
        return -1;
      ulong filesize2 = casyncFile.m_filesize;
      long compressedSize = (long) this.m_archive.Compress(casyncFile.m_data_pointer, filesize2, voidPtr, (ulong) num1);
      if ((IntPtr) crc32 != IntPtr.Zero)
      {
        uint num2 = (uint) (int) compressedSize;
        uint* numPtr = crc32;
        int num3 = (int) num2;
        int num4 = (int) CCrc32.CalcCrc32((uint) num3, (uint) num3, voidPtr);
        *numPtr = (uint) num4;
      }
      casyncFile.clearBuffer();
      this.clearBuffer();
      casyncFile.Close();
      if (this.m_extbuf == null)
        \u003CModule\u003E.FreeMemory(voidPtr);
      return (int) compressedSize;
    }

    public unsafe int GetCompressedSize(string fpath) => this.GetCompressedSize(fpath, (uint*) 0, (ulong*) 0);

    public unsafe uint GetCrc32(ulong offset, ulong readsize)
    {
      this.ReadAlloc(offset, readsize, CAsyncFile.ReadMode.Normal, readsize);
      this.WaitForComplete();
      void* dataPointer = this.m_data_pointer;
      if ((IntPtr) dataPointer == IntPtr.Zero)
        return 0;
      void* ptr = dataPointer;
      int num = (int) (uint) readsize;
      int crc32 = (int) CCrc32.CalcCrc32((uint) num, (uint) num, ptr);
      this.clearBuffer();
      return (uint) crc32;
    }

    public static unsafe uint GetCrc32(string fpath)
    {
      CAsyncFile casyncFile = new CAsyncFile();
      if (File.Exists(fpath) && (IntPtr) casyncFile.m_file == IntPtr.Zero)
      {
        casyncFile.clearMember();
        casyncFile.m_open_mode = CAsyncFile.OpenMode.Read;
        if (casyncFile.Open(fpath, CAsyncFile.OpenMode.Read, false))
        {
          ulong filesize1 = casyncFile.m_filesize;
          casyncFile.ReadAlloc(0UL, filesize1, CAsyncFile.ReadMode.Normal, filesize1);
          casyncFile.WaitForComplete();
          void* dataPointer = casyncFile.m_data_pointer;
          ulong filesize2 = casyncFile.m_filesize;
          ulong length = filesize2;
          int crc32 = (int) CCrc32.CalcCrc32((uint) filesize2, (uint) length, dataPointer);
          casyncFile.clearBuffer();
          casyncFile.Close();
          return (uint) crc32;
        }
      }
      return uint.MaxValue;
    }

    public unsafe ulong GetCheckSum64(ulong offset, ulong readsize)
    {
      this.ReadAlloc(offset, readsize, CAsyncFile.ReadMode.Normal, readsize);
      this.WaitForComplete();
      void* dataPointer = this.m_data_pointer;
      if ((IntPtr) dataPointer == IntPtr.Zero)
        return 0;
      long checkSum64 = (long) CCheckSum.CalcCheckSum64Tsbgp2(dataPointer, (uint) readsize);
      this.clearBuffer();
      return (ulong) checkSum64;
    }

    public static unsafe ulong GetCheckSum64(string fpath)
    {
      CAsyncFile casyncFile = new CAsyncFile();
      if (File.Exists(fpath) && (IntPtr) casyncFile.m_file == IntPtr.Zero)
      {
        casyncFile.clearMember();
        casyncFile.m_open_mode = CAsyncFile.OpenMode.Read;
        if (casyncFile.Open(fpath, CAsyncFile.OpenMode.Read, false))
        {
          ulong filesize1 = casyncFile.m_filesize;
          casyncFile.ReadAlloc(0UL, filesize1, CAsyncFile.ReadMode.Normal, filesize1);
          casyncFile.WaitForComplete();
          ulong filesize2 = casyncFile.m_filesize;
          long checkSum64 = (long) CCheckSum.CalcCheckSum64Tsbgp2(casyncFile.m_data_pointer, (uint) filesize2);
          casyncFile.clearBuffer();
          casyncFile.Close();
          return (ulong) checkSum64;
        }
      }
      return ulong.MaxValue;
    }

    private unsafe void readPreparing(
      ulong offset,
      ulong readsize,
      CAsyncFile.ReadMode rmode,
      ulong allocsize)
    {
      this.clearBuffer();
      this.m_read_mode = rmode;
      this.m_read_size = readsize;
      \u003CModule\u003E._fseeki64(this.m_file, (long) offset, 0);
      if (this.m_extbuf == null)
      {
        CAsyncFile casyncFile = this;
        casyncFile.m_data_pointer = casyncFile.allocBuffer(allocsize);
        this.m_dpkbuf = (void*) 0;
      }
      else if (this.m_archive.CompressCodec != EnumCompressCodec.CodecDpk && this.m_archive.CompressCodec != EnumCompressCodec.CodecDpkForceCrc)
      {
        if (allocsize > (ulong) this.m_extbuf.ReadBufferSize)
          return;
        CAsyncFile casyncFile = this;
        casyncFile.m_data_pointer = casyncFile.m_extbuf.ReadBuffer;
      }
      else
      {
        void* voidPtr = this.allocBuffer(allocsize);
        this.m_data_pointer = voidPtr;
        this.m_dpkbuf = voidPtr;
      }
      if ((IntPtr) this.m_data_pointer == IntPtr.Zero)
        return;
      if (this.m_read_mode == CAsyncFile.ReadMode.TyrDecompress)
        this.m_decompressed_size = allocsize;
      else
        this.m_decompressed_size = 0UL;
    }

    private void readBeginInvoke()
    {
      if (this.m_use_delegate)
      {
        this.m_rw_delegate.BeginInvoke(this, new AsyncCallback(CAsyncFile.AsyncEndCallback), (object) this);
      }
      else
      {
        switch (this.m_open_mode)
        {
          case CAsyncFile.OpenMode.Read:
            this.InternalRead();
            break;
          case CAsyncFile.OpenMode.Write:
            this.InternalWrite();
            break;
        }
        this.AsyncEndCallbackSub();
      }
    }

    private void writeBeginInvoke()
    {
      if (this.m_use_delegate)
      {
        this.m_rw_delegate.BeginInvoke(this, new AsyncCallback(CAsyncFile.AsyncEndCallback), (object) this);
      }
      else
      {
        switch (this.m_open_mode)
        {
          case CAsyncFile.OpenMode.Read:
            this.InternalRead();
            break;
          case CAsyncFile.OpenMode.Write:
            this.InternalWrite();
            break;
        }
        this.AsyncEndCallbackSub();
      }
    }

    public unsafe void Read(void* readpointer)
    {
      _iobuf* file = this.m_file;
      this.m_pre_position = (IntPtr) file != IntPtr.Zero ? (ulong) \u003CModule\u003E._ftelli64(file) : 0UL;
      int num = (int) \u003CModule\u003E.fread(readpointer, 1U, (uint) this.m_filesize, this.m_file);
    }

    public void ReadAlloc(CAsyncFile.ReadMode rmode, ulong decomp_size) => this.ReadAlloc(0UL, this.m_filesize, rmode, decomp_size);

    public void ReadAlloc(ulong offset, ulong readsize) => this.ReadAlloc(offset, readsize, CAsyncFile.ReadMode.Normal, readsize);

    public void ReadAlloc()
    {
      ulong filesize = this.m_filesize;
      this.ReadAlloc(0UL, filesize, CAsyncFile.ReadMode.Normal, filesize);
    }

    public unsafe void ReadAlloc(
      ulong offset,
      ulong readsize,
      CAsyncFile.ReadMode rmode,
      ulong decomp_size)
    {
      if (this.m_stat == CAsyncFile.AsyncStatus.Busy)
        throw new ApplicationException("Busying request.");
      if (this.m_open_mode != CAsyncFile.OpenMode.Read)
        return;
      this.m_stat = CAsyncFile.AsyncStatus.Busy;
      _iobuf* file = this.m_file;
      this.m_pre_position = (IntPtr) file != IntPtr.Zero ? (ulong) \u003CModule\u003E._ftelli64(file) : 0UL;
      this.readPreparing(offset, readsize, rmode, Math.Max(readsize, decomp_size));
      this.readBeginInvoke();
    }

    public unsafe void* Write(void* writepointer, ulong writesize) => this.Write(writepointer, writesize, CAsyncFile.WriteMode.Normal);

    public unsafe void* Write(void* writepointer, ulong writesize, CAsyncFile.WriteMode wmode)
    {
      if (this.m_stat == CAsyncFile.AsyncStatus.Busy)
        throw new ApplicationException("Already requested.");
      if ((IntPtr) writepointer == IntPtr.Zero)
        throw new ApplicationException("Invalid write pointer.");
      if (this.m_open_mode != CAsyncFile.OpenMode.Write)
        return (void*) 0;
      this.m_stat = CAsyncFile.AsyncStatus.Busy;
      _iobuf* file = this.m_file;
      this.m_pre_position = (IntPtr) file != IntPtr.Zero ? (ulong) \u003CModule\u003E._ftelli64(file) : 0UL;
      this.m_write_pointer = writepointer;
      this.m_write_size = writesize;
      this.m_write_mode = wmode;
      this.m_compressed_size = 0UL;
      this.writeBeginInvoke();
      return this.m_data_pointer;
    }

    private unsafe void RefleshRandomPaddingData(int size)
    {
      if ((IntPtr) this.m_zeromem == IntPtr.Zero)
        return;
      if (size > 32768)
        size = 32768;
      Random random = new Random((int) this.m_filesize);
      sbyte* zeromem = (sbyte*) this.m_zeromem;
      if (0 >= size)
        return;
      int num = size;
      do
      {
        *zeromem = (sbyte) random.Next();
        ++zeromem;
        --num;
      }
      while (num != 0);
    }

    private unsafe void RefleshZeroPaddingData(int size)
    {
      void* zeromem = this.m_zeromem;
      if ((IntPtr) zeromem == IntPtr.Zero)
        return;
      if (size < 32768)
        size = 32768;
      // ISSUE: initblk instruction
      __memset((IntPtr) zeromem, 0, size);
    }

    public unsafe ulong WriteZeroData(ulong inssize)
    {
      if (this.m_open_mode != CAsyncFile.OpenMode.Write)
        throw new ArgumentException("Opened by not Writring mode.");
      CAsyncFile casyncFile = this;
      return casyncFile.WriteZeroData(casyncFile.m_file, inssize);
    }

    private unsafe ulong WriteZeroData(_iobuf* file, ulong inssize)
    {
      \u003CModule\u003E._ftelli64(file);
      int num1 = (int) inssize;
      int num2 = num1;
      int num3 = num1;
      while (true)
      {
        if (this.m_enable_random_padding)
        {
          int num4 = num3;
          if ((IntPtr) this.m_zeromem != IntPtr.Zero)
          {
            if (num3 > 32768)
              num4 = 32768;
            Random random = new Random((int) this.m_filesize);
            sbyte* zeromem = (sbyte*) this.m_zeromem;
            if (0 < num4)
            {
              int num5 = num4;
              do
              {
                *zeromem = (sbyte) random.Next();
                ++zeromem;
                --num5;
              }
              while (num5 != 0);
            }
          }
        }
        int num6 = num2 <= 32768 ? num2 : 32768;
        if ((int) \u003CModule\u003E.fwrite(this.m_zeromem, 1U, (uint) num6, file) == num6)
        {
          \u003CModule\u003E.fflush(file);
          num2 -= num6;
          if (num2 > 0)
            ++num3;
          else
            goto label_12;
        }
        else
          break;
      }
      throw new ArgumentException("Writring error. Cannot write the padding space.");
label_12:
      return inssize;
    }

    public unsafe ulong WritePaddingToAlignment(ulong padsize)
    {
      if (this.m_open_mode != CAsyncFile.OpenMode.Write)
        throw new ArgumentException("Opened by not Writring mode.");
      _iobuf* file = this.m_file;
      ulong alignment;
      if (padsize <= 0UL)
      {
        alignment = 0UL;
      }
      else
      {
        ulong num1 = (ulong) \u003CModule\u003E._ftelli64(file) % padsize;
        if (num1 == 0UL)
        {
          alignment = 0UL;
        }
        else
        {
          ulong inssize = padsize - num1;
          long num2 = (long) this.WriteZeroData(file, inssize);
          alignment = inssize;
        }
      }
      return alignment;
    }

    private unsafe ulong WritePaddingToAlignment(_iobuf* file, ulong align)
    {
      if (align <= 0UL)
        return 0;
      ulong num1 = (ulong) \u003CModule\u003E._ftelli64(file) % align;
      if (num1 == 0UL)
        return 0;
      ulong inssize = align - num1;
      long num2 = (long) this.WriteZeroData(file, inssize);
      return inssize;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool judgeDoCompress(ulong originalsize, ulong compresssize) => compresssize != 0UL && (this.m_archive.CompressCodec == EnumCompressCodec.CodecDpkForceCrc || originalsize > compresssize);

    public void WaitForComplete()
    {
      if (this.m_reader != null)
      {
        if (this.CopyStatus == CAsyncFile.AsyncStatus.Complete)
          return;
        while (this.CopyStatus != CAsyncFile.AsyncStatus.Error)
        {
          Thread.Sleep(10);
          if (this.CopyStatus == CAsyncFile.AsyncStatus.Complete)
            break;
        }
      }
      else
      {
        if (this.m_stat != CAsyncFile.AsyncStatus.Busy)
          return;
        do
        {
          Thread.Sleep(10);
        }
        while (this.m_stat == CAsyncFile.AsyncStatus.Busy);
      }
    }

    private unsafe int InternalRead()
    {
      if (this.m_read_size == 0UL)
        return 0;
      if ((IntPtr) this.m_data_pointer == IntPtr.Zero || (IntPtr) this.m_file == IntPtr.Zero)
        return -1;
      int num1 = 0;
      int num2;
      do
      {
        num2 = (int) \u003CModule\u003E.fread(this.m_data_pointer, 1U, (uint) this.m_read_size, this.m_file);
        if ((long) num2 != (long) this.m_read_size)
        {
          if (num1 != 9)
          {
            Thread.Sleep(500);
            ++num1;
          }
          else
            goto label_7;
        }
        else
          break;
      }
      while (num1 < 10);
      goto label_8;
label_7:
      _iobuf* file = this.m_file;
      ulong num3 = (IntPtr) file != IntPtr.Zero ? (ulong) \u003CModule\u003E._ftelli64(file) : 0UL;
      ulong readSize = this.m_read_size;
      string str = "Reading error. fread ret=" + num2.ToString() + " rsize=" + readSize.ToString() + " skpos=" + num3.ToString();
      this.m_stat = CAsyncFile.AsyncStatus.Error;
label_8:
      if (this.m_read_mode == CAsyncFile.ReadMode.TyrDecompress)
      {
        EnumCompressCodec compressCodec = this.m_archive.CompressCodec;
        void* dataPointer = this.m_data_pointer;
        this.m_decompressed_size = CAsyncFileArchive.DecompressStatic(dataPointer, this.m_read_size, dataPointer, this.m_decompressed_size, compressCodec);
      }
      return num2;
    }

    private unsafe int InternalWrite()
    {
      ulong writeSize = this.m_write_size;
      ulong num1 = writeSize;
      void* ptr = this.m_write_pointer;
      void* voidPtr = (void*) 0;
      if (num1 == 0UL)
        return 0;
      if (this.m_write_mode == CAsyncFile.WriteMode.TyrCompress)
      {
        ulong num2 = writeSize * 2UL;
        CExternalBuffer extbuf = this.m_extbuf;
        if (extbuf == null)
          voidPtr = \u003CModule\u003E.AllocateMemory((uint) num2);
        else if ((ulong) extbuf.CompBufferSize >= num2)
        {
          voidPtr = this.m_extbuf.CompBuffer;
        }
        else
        {
          this.m_stat = CAsyncFile.AsyncStatus.Error;
          return -1;
        }
        if ((IntPtr) voidPtr == IntPtr.Zero)
        {
          this.m_stat = CAsyncFile.AsyncStatus.Error;
          return -1;
        }
        ulong compresssize;
        try
        {
          compresssize = this.m_archive.Compress(this.m_write_pointer, this.m_write_size, voidPtr, num2);
        }
        catch (Exception ex)
        {
          Console.WriteLine("Internal error : failed compression." + ex.Message);
          this.TryFreeMemory(voidPtr);
          this.m_stat = CAsyncFile.AsyncStatus.Error;
          return -1;
        }
        CAsyncFile casyncFile1 = this;
        if (casyncFile1.judgeDoCompress(casyncFile1.m_write_size, compresssize))
        {
          num1 = compresssize;
          ptr = voidPtr;
          this.m_compressed_size = compresssize;
          ulong compressCodecAlignment = this.m_compress_codec_alignment;
          if (compressCodecAlignment > 0UL)
          {
            long alignment = (long) this.WritePaddingToAlignment(compressCodecAlignment);
            CAsyncFile casyncFile2 = this;
            casyncFile2.m_pre_position = casyncFile2.Position;
          }
        }
        else
        {
          CAsyncFile casyncFile3 = this;
          casyncFile3.m_compressed_size = casyncFile3.m_write_size;
        }
        if (this.m_enable_crc_check)
        {
          uint num3 = (uint) num1;
          this.Ccrc.Reset(false, num3, 0UL);
          this.Ccrc.Calc(ptr, num3);
        }
      }
      uint num4 = (uint) num1;
      int num5 = (int) \u003CModule\u003E.fwrite(ptr, 1U, num4, this.m_file);
      if ((IntPtr) voidPtr != IntPtr.Zero)
        this.TryFreeMemory(voidPtr);
      if (num5 != (int) num4)
      {
        ulong num6 = num1;
        string str = "Writing error ret=" + num5.ToString() + " wsize=" + num6.ToString();
      }
      return num5;
    }

    public unsafe void TryFreeMemory(void* buf)
    {
      if (this.m_extbuf == null)
      {
        \u003CModule\u003E.FreeMemory(buf);
      }
      else
      {
        if (this.m_dpkbuf != buf)
          return;
        \u003CModule\u003E.FreeMemory(buf);
        this.m_dpkbuf = (void*) 0;
      }
    }

    private static CAsyncFile AsyncReadWriteFunction(CAsyncFile obj)
    {
      switch (obj.m_open_mode)
      {
        case CAsyncFile.OpenMode.Read:
          obj.InternalRead();
          break;
        case CAsyncFile.OpenMode.Write:
          obj.InternalWrite();
          break;
      }
      return obj;
    }

    private unsafe ulong InternalCopy()
    {
      this.m_reader.readBeginInvoke();
      this.m_reader.WaitForComplete();
      CAsyncFile reader1 = this.m_reader;
      void* dataPointer = reader1.m_data_pointer;
      if ((IntPtr) dataPointer == IntPtr.Zero)
        throw new ApplicationException("ReadBuffer is null at Copy function");
      this.Write(dataPointer, reader1.m_read_size, this.m_write_mode);
      this.WaitForComplete();
      CAsyncFile reader2 = this.m_reader;
      long readSize = (long) reader2.m_read_size;
      reader2.clearBuffer();
      this.m_copied = true;
      return (ulong) readSize;
    }

    public ulong GetDecompressedSize()
    {
      if (this.m_read_mode != CAsyncFile.ReadMode.TyrDecompress)
        throw new InvalidOperationException("Invalid mode.");
      return this.m_decompressed_size;
    }

    private void InternalReadEnd()
    {
      if (this.m_stat == CAsyncFile.AsyncStatus.Error)
        return;
      this.m_stat = CAsyncFile.AsyncStatus.Complete;
    }

    private void InternalWriteEnd()
    {
      if (this.m_stat == CAsyncFile.AsyncStatus.Error)
        return;
      this.m_stat = CAsyncFile.AsyncStatus.Complete;
    }

    private static CAsyncFile AsyncCopyFunction(CAsyncFile obj)
    {
      long num = (long) obj.InternalCopy();
      return obj;
    }

    private static void AsyncEndCallback(IAsyncResult ar) => (ar.AsyncState as CAsyncFile).AsyncEndCallbackSub();

    private void AsyncEndCallbackSub()
    {
      switch (this.m_open_mode)
      {
        case CAsyncFile.OpenMode.Read:
          if (this.m_stat == CAsyncFile.AsyncStatus.Error)
            break;
          this.m_stat = CAsyncFile.AsyncStatus.Complete;
          break;
        case CAsyncFile.OpenMode.Write:
          if (this.m_stat == CAsyncFile.AsyncStatus.Error)
            break;
          this.m_stat = CAsyncFile.AsyncStatus.Complete;
          break;
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool Copy(CAsyncFile reader, CAsyncFile writer)
    {
      ulong filesize = reader.m_filesize;
      ulong decompsize = filesize;
      ulong size = filesize;
      return this.Copy(reader, writer, 0UL, size, CAsyncFile.CopyMode.Normal, decompsize);
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool Copy(CAsyncFile reader, CAsyncFile writer, ulong offset, ulong size) => this.Copy(reader, writer, offset, size, CAsyncFile.CopyMode.Normal, size);

    [return: MarshalAs(UnmanagedType.U1)]
    public bool Copy(
      CAsyncFile reader,
      CAsyncFile writer,
      ulong offset,
      ulong size,
      CAsyncFile.CopyMode cmode)
    {
      if (cmode == CAsyncFile.CopyMode.ReadDecompress)
        throw new ApplicationException("Invalid copy mode");
      return this.Copy(reader, writer, offset, size, cmode, size);
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool Copy(
      CAsyncFile reader,
      CAsyncFile writer,
      ulong offset,
      ulong size,
      CAsyncFile.CopyMode cmode,
      ulong decompsize)
    {
      if (this.m_stat == CAsyncFile.AsyncStatus.Busy)
        return false;
      this.m_copy_mode = cmode;
      this.m_reader = reader;
      this.m_writer = writer;
      reader.m_use_delegate = false;
      this.m_writer.m_use_delegate = false;
      this.m_read_size = size;
      this.m_read_offset = offset;
      this.m_decompressed_size = decompsize;
      this.m_compressed_size = 0UL;
      this.Ccrc.Reset(false, 0U, 0UL);
      this.m_stat = CAsyncFile.AsyncStatus.Busy;
      if (this.m_use_delegate)
      {
        this.m_copy_delegate.BeginInvoke(this, new AsyncCallback(CAsyncFile.AsyncCopyEndCallback2), (object) this);
      }
      else
      {
        this.InternalCopyFunction();
        if (this.m_stat != CAsyncFile.AsyncStatus.Error)
          this.m_stat = CAsyncFile.AsyncStatus.Complete;
      }
      return true;
    }

    private static CAsyncFile AsyncCopyFunction2(CAsyncFile obj)
    {
      obj.InternalCopyFunction();
      return obj;
    }

    private static void AsyncCopyEndCallback2(IAsyncResult ar)
    {
      CAsyncFile asyncState = ar.AsyncState as CAsyncFile;
      if (asyncState.m_stat == CAsyncFile.AsyncStatus.Error)
        return;
      asyncState.m_stat = CAsyncFile.AsyncStatus.Complete;
    }

    private unsafe void InternalCopyFunction()
    {
      CAsyncFile.CopyMode copyMode = this.m_copy_mode;
      CAsyncFile.ReadMode rmode = (CAsyncFile.ReadMode) (copyMode == CAsyncFile.CopyMode.ReadDecompress);
      CAsyncFile.WriteMode wmode = (CAsyncFile.WriteMode) (copyMode == CAsyncFile.CopyMode.TryWriteCompress);
      if (this.m_extbuf != null && wmode == CAsyncFile.WriteMode.Normal && rmode != CAsyncFile.ReadMode.TyrDecompress)
      {
        long readOffset = (long) this.m_read_offset;
        ulong readSize = this.m_read_size;
        long num1 = (long) readSize;
        this.Ccrc.Reset(false, (uint) readSize, 0UL);
        do
        {
          long num2 = num1;
          if (num1 > (long) this.m_extbuf.ReadBufferSize)
            num2 = (long) this.m_extbuf.ReadBufferSize;
          this.m_reader.ReadAlloc((ulong) readOffset, (ulong) num2, CAsyncFile.ReadMode.Normal, (ulong) num2);
          this.m_reader.WaitForComplete();
          CAsyncFile reader = this.m_reader;
          if (reader.m_stat != CAsyncFile.AsyncStatus.Error)
          {
            if (this.m_enable_crc_check)
              this.Ccrc.Calc(reader.m_data_pointer, (uint) (int) num2);
            this.m_writer.m_enable_crc_check = false;
            this.m_writer.m_reader = this.m_reader;
            this.m_writer.Write(this.m_reader.m_data_pointer, (ulong) num2, CAsyncFile.WriteMode.Normal);
            this.m_writer.WaitForComplete();
            if (this.m_writer.m_stat != CAsyncFile.AsyncStatus.Error)
            {
              num1 -= num2;
              readOffset += num2;
            }
            else
              goto label_11;
          }
          else
            goto label_10;
        }
        while (num1 > 0L);
        return;
label_10:
        this.m_stat = CAsyncFile.AsyncStatus.Error;
        return;
label_11:
        this.m_stat = CAsyncFile.AsyncStatus.Error;
      }
      else
      {
        this.m_reader.ReadAlloc(this.m_read_offset, this.m_read_size, rmode, this.m_decompressed_size);
        this.m_reader.WaitForComplete();
        CAsyncFile reader = this.m_reader;
        if (reader.m_stat == CAsyncFile.AsyncStatus.Error)
        {
          Console.WriteLine("Copying .. Read error");
          this.m_reader.clearBuffer();
          this.m_stat = CAsyncFile.AsyncStatus.Error;
        }
        else
        {
          void* dataPointer = reader.m_data_pointer;
          this.m_writer.m_compress_codec_alignment = this.m_compress_codec_alignment;
          this.m_writer.m_enable_crc_check = this.m_enable_crc_check;
          this.m_writer.Ccrc.SetMode(this.Ccrc.GetMode());
          this.m_writer.m_reader = this.m_reader;
          this.m_writer.Write(dataPointer, this.m_decompressed_size, wmode);
          this.m_writer.WaitForComplete();
          if (this.m_enable_crc_check)
          {
            if (this.m_read_size == 0UL)
              this.Ccrc.Reset(true, 0U, (ulong) uint.MaxValue);
            else
              this.Ccrc.Reset(true, this.m_writer.Ccrc.Crc32, this.m_writer.Ccrc.CheckSum64);
          }
          this.m_reader.clearBuffer();
          CAsyncFile writer = this.m_writer;
          if (writer.m_stat == CAsyncFile.AsyncStatus.Error)
          {
            Console.WriteLine("Copying .. Write error");
            this.m_stat = CAsyncFile.AsyncStatus.Error;
          }
          else
          {
            if (this.m_copy_mode != CAsyncFile.CopyMode.TryWriteCompress)
              return;
            this.m_compressed_size = writer.GetCompressedSize();
          }
        }
      }
    }

    private void InternalCopyEnd()
    {
      if (this.m_stat == CAsyncFile.AsyncStatus.Error)
        return;
      this.m_stat = CAsyncFile.AsyncStatus.Complete;
    }

    public static unsafe void WriteFile(string fname, void* ptr, long size)
    {
      CAsyncFile casyncFile = new CAsyncFile();
      if ((IntPtr) casyncFile.m_file == IntPtr.Zero)
      {
        casyncFile.clearMember();
        casyncFile.m_open_mode = CAsyncFile.OpenMode.Write;
        casyncFile.Open(fname, CAsyncFile.OpenMode.Write, false);
      }
      casyncFile.Write(ptr, (ulong) size, CAsyncFile.WriteMode.Normal);
      casyncFile.WaitForComplete();
      casyncFile.Close();
    }

    public static unsafe long ReadFile(string fname, void* ptr, long bufsize)
    {
      CAsyncFile casyncFile = new CAsyncFile();
      casyncFile.ReadOpen(fname);
      long filesize = (long) casyncFile.m_filesize;
      if (filesize > bufsize)
        return -1;
      casyncFile.Read(ptr);
      casyncFile.WaitForComplete();
      casyncFile.Close();
      return filesize;
    }

    public long ClearCacheFilesLimit() => this.m_archive.ClearCacheFilesLimit();

    public override string ToString() => string.Format("fsize = {0}", (object) this.m_filesize);

    public void SetCompressOptions(CodecOptions co) => this.m_archive.SetCompressOptions(co);

    public void SetFileAttribute(int file_attribute) => this.m_archive.SetFileAttribute(file_attribute);

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECAsyncFile();
      }
      else
      {
        try
        {
          this.\u0021CAsyncFile();
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

    ~CAsyncFile() => this.Dispose(false);

    public enum AsyncStatus
    {
      Stop,
      Busy,
      Complete,
      Error,
    }

    public enum OpenMode
    {
      Unknown,
      Read,
      Write,
      Copy,
    }

    public enum WriteMode
    {
      Normal,
      TyrCompress,
    }

    public enum ReadMode
    {
      Normal,
      TyrDecompress,
    }

    public enum CopyMode
    {
      Normal,
      TryWriteCompress,
      ReadDecompress,
    }

    private delegate CAsyncFile Delegate(CAsyncFile obj);
  }
}
