// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CFileInfo
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace CriCpkMaker
{
  public class CFileInfo
  {
    private string m_filepath;
    private string m_vfilepath;
    private string m_groupstring;
    private string m_attributestring;
    private string m_userstring;
    private ulong m_datetime;
    private ulong m_filesize;
    private ulong m_extsize;
    private ulong m_offset;
    private bool m_burned;
    private uint m_data_align;
    private uint m_file_id;
    private bool m_try_force_compress;
    private string m_md5;
    private long m_packing_order;
    private long m_info_index;
    private int m_sorted_link;
    private uint m_registered_id;
    private uint m_crc32;
    private bool m_is_unification;
    private object m_tag;
    private int m_group_top_align;
    public long CsvRegisteredId;

    public uint RegisteredId
    {
      get => this.m_registered_id;
      set => this.m_registered_id = value;
    }

    public string LocalFilePath
    {
      get => this.m_filepath;
      set => this.m_filepath = value;
    }

    public string ContentFilePath
    {
      get => this.m_vfilepath;
      set
      {
        string str = value;
        if (value.Substring(0, 1).Equals("/"))
          str = value.Substring(1, value.Length - 1);
        this.m_vfilepath = str;
      }
    }

    public string ContentFilename => Path.GetFileName(this.m_vfilepath);

    public string GroupString
    {
      get => this.m_groupstring;
      set => this.m_groupstring = value;
    }

    public string GroupDisplayString => string.IsNullOrEmpty(this.m_groupstring) || this.m_groupstring == "/" + "(none)" ? "(none)" : this.m_groupstring;

    public string AttributeString
    {
      get => this.m_attributestring;
      set => this.m_attributestring = value;
    }

    public string AttributeDisplayString => string.IsNullOrEmpty(this.m_attributestring) ? "(none)" : this.m_attributestring;

    public string UserString
    {
      get => this.m_userstring;
      set => this.m_userstring = value;
    }

    public ulong DateTime64
    {
      get => this.m_datetime;
      set => this.m_datetime = value;
    }

    public ulong Filesize
    {
      get => this.m_filesize;
      set => this.m_filesize = value;
    }

    public ulong Extractsize
    {
      get => this.m_extsize;
      set => this.m_extsize = value;
    }

    public uint DataAlign
    {
      set => this.m_data_align = value;
      get => this.m_data_align;
    }

    public bool IsCompressed
    {
      [return: MarshalAs(UnmanagedType.U1)] get
      {
        ulong extsize = this.m_extsize;
        return (extsize != 0UL || this.m_filesize == 0UL) && (long) extsize != (long) this.m_filesize;
      }
    }

    public double CompressPercentage
    {
      get
      {
        ulong filesize = this.m_filesize;
        return filesize == 0UL ? 0.0 : (double) filesize / (double) this.m_extsize * 100.0;
      }
    }

    public ulong FilesizeAligned
    {
      get
      {
        ulong dataAlign = (ulong) this.m_data_align;
        return (ulong) ((long) this.m_filesize + (long) dataAlign + -1L) / dataAlign * dataAlign;
      }
    }

    public ulong FilesizeSector
    {
      get
      {
        ulong dataAlign = (ulong) this.m_data_align;
        return (ulong) ((long) this.m_filesize + (long) dataAlign + -1L) / dataAlign;
      }
    }

    public ulong ExtractsizeAligned
    {
      get
      {
        ulong dataAlign = (ulong) this.m_data_align;
        return (ulong) ((long) this.m_filesize + (long) dataAlign + -1L) / dataAlign * dataAlign;
      }
    }

    public ulong ExtractsizeSector
    {
      get
      {
        ulong dataAlign = (ulong) this.m_data_align;
        return (ulong) ((long) this.m_filesize + (long) dataAlign + -1L) / dataAlign;
      }
    }

    public DateTime DateTime
    {
      get => Utility.ConvToDateTime(this.m_datetime);
      set
      {
        uint day = (uint) value.Day;
        uint month = (uint) value.Month;
        ulong num1 = (ulong) ((((long) (uint) value.Year * 256L + (long) month) * 256L + (long) day) * 4294967296L);
        CFileInfo cfileInfo1 = this;
        cfileInfo1.m_datetime = (cfileInfo1.m_datetime & (ulong) uint.MaxValue) + num1;
        uint second = (uint) value.Second;
        uint minute = (uint) value.Minute;
        ulong num2 = ((ulong) (((long) (uint) value.Hour * 256L + (long) minute) * 256L) + (ulong) second) * 256UL;
        CFileInfo cfileInfo2 = this;
        cfileInfo2.m_datetime = (cfileInfo2.m_datetime & 18446744069414584320UL) + num2;
      }
    }

    public string DateTimeString
    {
      get
      {
        ulong datetime = this.m_datetime;
        if (datetime == 0UL)
          return "0000/00/00";
        DateTime dateTime = Utility.ConvToDateTime(datetime);
        return string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}", (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute, (object) dateTime.Second);
      }
    }

    public ulong Offset
    {
      get => this.m_offset;
      set => this.m_offset = value;
    }

    public bool Burned
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_burned;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_burned = value;
    }

    public uint FileId
    {
      get => this.m_file_id;
      set => this.m_file_id = value;
    }

    public bool TryCompress
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_try_force_compress;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_try_force_compress = value;
    }

    public string Md5 => this.m_md5;

    public long PackingOrder
    {
      get => this.m_packing_order;
      set => this.m_packing_order = value;
    }

    public long InfoIndex
    {
      get => this.m_info_index;
      set => this.m_info_index = value;
    }

    public int SortedLink
    {
      get => this.m_sorted_link;
      set => this.m_sorted_link = value;
    }

    public object Tag
    {
      get => this.m_tag;
      set => this.m_tag = value;
    }

    public uint Crc32
    {
      get => this.m_crc32;
      set => this.m_crc32 = value;
    }

    public bool UniTarget
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_is_unification;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_is_unification = value;
    }

    public int GroupTopAlign
    {
      get => this.m_group_top_align;
      set => this.m_group_top_align = value;
    }

    public CFileInfo()
    {
      this.m_burned = false;
      this.m_data_align = 2048U;
      this.m_file_id = uint.MaxValue;
      this.m_try_force_compress = false;
      this.m_md5 = (string) null;
      this.m_packing_order = -1L;
      this.m_info_index = -1L;
      this.m_tag = (object) null;
      this.m_registered_id = 0U;
      this.m_is_unification = false;
      this.m_group_top_align = 0;
      this.CsvRegisteredId = -1L;
    }

    public void SetDate(uint year, uint month, uint day)
    {
      CFileInfo cfileInfo = this;
      cfileInfo.m_datetime = (ulong) (((long) cfileInfo.m_datetime & (long) uint.MaxValue) + (((long) year * 256L + (long) month) * 256L + (long) day) * 4294967296L);
    }

    public void SetTime(uint hour, uint minute, uint second)
    {
      CFileInfo cfileInfo = this;
      cfileInfo.m_datetime = (ulong) (((long) cfileInfo.m_datetime & -4294967296L) + (((long) hour * 256L + (long) minute) * 256L + (long) second) * 256L);
    }

    private string CutFrontSlash(string str)
    {
      if (str.Substring(0, 1).Equals("/"))
        str = str.Substring(1, str.Length - 1);
      return str;
    }

    public void SetCompressInfo(ulong compsize) => this.m_filesize = compsize;

    public static string AnalyzeMd5Hash(string fname) => Utility.GetMd5Hash(fname);

    public void AnalyzeMd5Hash()
    {
      if (this.m_filepath != (string) null)
      {
        string str = this.m_filepath.Replace('/', '\\');
        if (str != "" && File.Exists(str))
        {
          if (!(this.m_md5 == (string) null))
            return;
          this.m_md5 = Utility.GetMd5Hash(str);
          return;
        }
      }
      this.m_md5 = (string) null;
    }

    private static byte[] ReadBinary(string fname)
    {
      if (!File.Exists(fname))
        return (byte[]) null;
      FileInfo fileInfo = new FileInfo(fname);
      BinaryReader binaryReader = new BinaryReader((Stream) File.Open(fname, FileMode.Open, FileAccess.Read));
      if (binaryReader == null)
        return (byte[]) null;
      byte[] numArray = binaryReader.ReadBytes((int) fileInfo.Length);
      binaryReader.Dispose();
      return numArray;
    }

    private static long GetFileSize(string fname) => !File.Exists(fname) ? -1L : new FileInfo(fname).Length;

    public static string GetMd5String(byte[] bytes) => BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(bytes)).ToLower().Replace("-", "");

    public void AddGroupString(string groupString)
    {
      if (this.m_groupstring == (string) null)
      {
        this.m_groupstring = groupString;
      }
      else
      {
        string[] strArray = this.m_groupstring.Split(',');
        int index = 0;
        if (0 < strArray.Length)
        {
          while (!strArray[index].Trim().Equals(groupString))
          {
            ++index;
            if (index >= strArray.Length)
              goto label_5;
          }
          return;
        }
label_5:
        if (!(this.m_groupstring != (string) null))
          return;
        CFileInfo cfileInfo = this;
        cfileInfo.m_groupstring = cfileInfo.m_groupstring + ", " + groupString;
      }
    }

    public override string ToString()
    {
      object[] objArray = new object[7]
      {
        (object) this.m_filepath,
        (object) this.m_vfilepath,
        (object) this.m_extsize,
        (object) this.m_filesize,
        (object) this.m_offset,
        (object) this.DateTimeString,
        null
      };
      bool burned = this.m_burned;
      objArray[6] = (object) burned.ToString();
      return string.Format("Local={0}, CPK={1}, OrgSize={2}, FileSize={3}, Offset={4}, DateTime={5}, Burned={6}", objArray);
    }
  }
}
