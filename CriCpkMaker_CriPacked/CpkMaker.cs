// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CpkMaker
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using CriMw.CriGears.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace CriCpkMaker
{
  public class CpkMaker : IDisposable
  {
    private ulong m_filedata_count;
    private string m_last_cpk_filename;
    private CFileData m_filedata;
    private CBinary m_bin;
    private DateTime m_starttime;
    private DateTime m_prereturntime;
    private TimeSpan m_preremaintime;
    private Mutex m_mutex;
    private bool m_random_padding;
    private string m_cpkrootdir;
    public bool DefineHeaderLocalFilePath;
    public string DefineHeaderUniqString;
    public bool DefineHeaderCsvOrder;
    private bool \u003Cbacking_store\u003EIsProgramSymbolUsePrefix;
    private string \u003Cbacking_store\u003EProgramSymbolPrefix;
    private bool \u003Cbacking_store\u003EIsProgramSymbolUseSuffix;
    private string \u003Cbacking_store\u003EProgramSymbolSuffix;
    private bool \u003Cbacking_store\u003EIsRemoveFileExtension;

    public CFileData FileData
    {
      get => this.m_filedata;
      set => this.m_filedata = value;
    }

    public string BaseDirectory
    {
      get => this.m_filedata.BaseDirectory;
      set => this.m_filedata.BaseDirectory = value;
    }

    public string CpkRootDirectory
    {
      get => this.m_cpkrootdir;
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          this.m_cpkrootdir = "/";
        }
        else
        {
          this.m_cpkrootdir = value.Replace("\\", "/");
          this.m_cpkrootdir = "/" + this.m_cpkrootdir.Trim('/') + "/";
        }
      }
    }

    public string Comment
    {
      get => this.m_filedata.Comment;
      set => this.m_filedata.Comment = value;
    }

    public string ToolVersion
    {
      get => this.m_filedata.ToolVersion;
      set => this.m_filedata.ToolVersion = value + ", DLL" + CpkMaker.GetDllVersionString();
    }

    public int MaxDpkItoc => this.m_bin.MaxDpkItoc;

    public bool ForceCompress
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.ForceCompress;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.ForceCompress = value;
    }

    public bool EnableCrc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableCrc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableCrc = value;
    }

    public bool EnableCrc32
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableCrc32;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableCrc32 = value;
    }

    public bool EnableCheckSum64
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableCheckSum64;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableCheckSum64 = value;
    }

    public bool InfoCrcOnly
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.InfoCrcOnly;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.InfoCrcOnly = value;
    }

    public bool RandomPadding
    {
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.RandomPadding = value;
    }

    public uint DataAlign
    {
      get => this.m_bin.DataAlign;
      set
      {
        if (value <= 0U)
          value = 1U;
        this.m_bin.DataAlign = value;
        this.m_filedata.DataAlign = value;
      }
    }

    public ulong CpkDateTime64 => this.m_bin.CpkDateTime64;

    public bool EnableToc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableToc;
    }

    public bool EnableHtoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableHtoc;
    }

    public bool EnableHtocFpathToToc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableHtocFpathToToc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableHtocFpathToToc = value;
    }

    public bool EnableHtocFidToToc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableHtocFidToToc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableHtocFidToToc = value;
    }

    public bool EnableEtoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableEtoc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableEtoc = value;
    }

    public bool EnableItoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableItoc;
    }

    public bool EnableGtoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableGtoc;
    }

    public bool EnableHgtoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableHgtoc;
    }

    public bool EnableHgtocGnameToGlink
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableHgtocGnameToGlink;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableHgtocGnameToGlink = value;
    }

    public bool EnableHgtocGanameToGinf
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableHgtocGanameToGinf;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableHgtocGanameToGinf = value;
    }

    public bool EnableHgtocGfpathToGfinf
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableHgtocGfpathToGfinf;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableHgtocGfpathToGfinf = value;
    }

    public bool EnableHgtocGfidToGfinf
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableHgtocGfidToGfinf;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableHgtocGfidToGfinf = value;
    }

    public bool EnableAfs2
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableAfs2;
    }

    public CpkMaker.EnumCpkFileMode CpkFileMode
    {
      get => (CpkMaker.EnumCpkFileMode) this.m_bin.CpkMode;
      set
      {
        this.m_bin.CpkMode = (uint) value;
        switch (value + 1)
        {
          case CpkMaker.EnumCpkFileMode.ModeId:
            this.m_bin.EnableToc = false;
            this.m_bin.EnableEtoc = false;
            this.m_bin.EnableItoc = false;
            this.m_bin.EnableGtoc = false;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            this.m_bin.EnableFileName = false;
            break;
          case CpkMaker.EnumCpkFileMode.ModeFilename:
            this.m_bin.EnableToc = false;
            this.m_bin.EnableEtoc = false;
            this.m_bin.EnableItoc = true;
            this.m_bin.EnableGtoc = false;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            this.m_bin.EnableFileName = false;
            break;
          case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = true;
            this.m_bin.EnableItoc = false;
            this.m_bin.EnableGtoc = false;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            this.m_bin.EnableFileName = true;
            break;
          case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = true;
            this.m_bin.EnableItoc = true;
            this.m_bin.EnableGtoc = false;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            this.m_bin.EnableFileName = true;
            break;
          case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = true;
            this.m_bin.EnableItoc = false;
            this.m_bin.EnableGtoc = true;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            this.m_bin.EnableFileName = true;
            break;
          case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = false;
            this.m_bin.EnableItoc = true;
            this.m_bin.EnableGtoc = true;
            this.m_bin.EnableIdAndGroup = true;
            this.m_filedata.IdAndGroup = true;
            this.m_bin.EnableFileName = false;
            break;
          case CpkMaker.EnumCpkFileMode.ModeIdAndGroup | CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = true;
            this.m_bin.EnableItoc = true;
            this.m_bin.EnableGtoc = true;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            this.m_bin.EnableFileName = true;
            break;
        }
      }
    }

    public bool EnableFileName
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableFileName;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableFileName = value;
    }

    public CpkMaker.EnumFileLayoutMode FileLayoutMode
    {
      get => (CpkMaker.EnumFileLayoutMode) this.m_bin.FileLayoutMode;
      set => this.m_bin.FileLayoutMode = (CBinary.EnumFileLayoutMode) value;
    }

    public int Version => this.m_bin.Version;

    public int Revision => this.m_bin.Revision;

    public ulong ContentsOffset => this.m_bin.ContentsOffset;

    public ulong ContentsSize => this.m_bin.ContentsSize;

    public uint TocSize => this.m_bin.TocSize;

    public uint HtocSize => this.m_bin.HtocSize;

    public uint ItocSize => this.m_bin.ItocSize;

    public uint GtocSize => this.m_bin.GtocSize;

    public uint HgtocSize => this.m_bin.HgtocSize;

    public string WorkingMessage => this.m_bin.WorkingMessage;

    public string ErrorMessage => this.m_bin.ErrorMessage;

    public bool Mask
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.Mask;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.Mask = value;
    }

    public bool Sorted
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.SortedFilename;
    }

    public CExternalBuffer ExternalBuffer
    {
      set => this.m_bin.ExternalBuffer = value;
    }

    public bool EnableDateTimeInfo
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableDateTimeInfo;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableDateTimeInfo = value;
    }

    public EnumUnificationMode UnificationMode
    {
      get => this.m_bin.UnificationMode;
      set => this.m_bin.UnificationMode = value;
    }

    public bool EnableDuplicateByGroup
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableDuplicateByGroup;
      [param: MarshalAs(UnmanagedType.U1)] set
      {
        this.m_bin.EnableDuplicateByGroup = value;
        this.m_filedata.EnableDuplicateByGroup = value;
      }
    }

    public bool EnableMself
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableMself;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableMself = value;
    }

    public long TotalFileSize => this.m_filedata.TotalFileSize;

    public bool SortContinuousGroupFile
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_filedata.SortContinuousGroupFile;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_filedata.SortContinuousGroupFile = value;
    }

    public bool EnableTopTocInfo
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableTopTocInfo;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableTopTocInfo = value;
    }

    public EnumCompressCodec CompressCodec
    {
      get => this.m_bin.CompressCodec;
      set => this.m_bin.CompressCodec = value;
    }

    public string CompressCodecString
    {
      get
      {
        if (this.m_bin.CompressCodec == EnumCompressCodec.CodecLayla || this.m_bin.CompressCodec == EnumCompressCodec.CodecDpk)
          return "Layla Standard Compression";
        if (this.m_bin.CompressCodec == EnumCompressCodec.CodecLZMA)
          return "LZMA Compression";
        return this.m_bin.CompressCodec == EnumCompressCodec.CodecRELC ? "RELC Compression" : "Unknown";
      }
    }

    public int DpkDividedSize
    {
      get => this.m_bin.DpkDividedSize;
      set => this.m_bin.DpkDividedSize = value;
    }

    public CodecOptions codecOptions
    {
      get => this.m_bin.codecOptions;
      set => this.m_bin.codecOptions = value;
    }

    public float CompPercentage
    {
      set => this.m_bin.CompPercentage = value;
    }

    public int CompFileSize
    {
      set => this.m_bin.CompFileSize = value;
    }

    public int GroupDataAlignment
    {
      get => this.m_bin.GroupDataAlignment;
      set => this.m_bin.GroupDataAlignment = value;
    }

    public string UncompFileExt
    {
      get => this.m_bin.UncompFileExt;
      set => this.m_bin.UncompFileExt = value;
    }

    public int CompFileAlign
    {
      get => this.m_bin.CompressCodecAlignment;
      set => this.m_bin.CompressCodecAlignment = value;
    }

    public string PartOfCpkHeaderExt
    {
      get => this.m_bin.PartOfCpkHeaderExt;
      set => this.m_bin.PartOfCpkHeaderExt = value;
    }

    public bool EnableGInfo
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableGInfo;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableGInfo = value;
    }

    public string CacheDirectory
    {
      get => this.m_bin.CacheDirectory;
      set => this.m_bin.CacheDirectory = value;
    }

    public long CacheLimitSize
    {
      get => this.m_bin.CacheLimitSize;
      set => this.m_bin.CacheLimitSize = value;
    }

    public bool EnableAttributeUserString
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableAttributeUserString;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.EnableAttributeUserString = value;
    }

    public bool LayoutOrderForCompAndUni
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.LayoutOrderForCompAndUni;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_bin.LayoutOrderForCompAndUni = value;
    }

    public bool IsProgramSymbolUsePrefix
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.\u003Cbacking_store\u003EIsProgramSymbolUsePrefix;
      [param: MarshalAs(UnmanagedType.U1)] set => this.\u003Cbacking_store\u003EIsProgramSymbolUsePrefix = value;
    }

    public string ProgramSymbolPrefix
    {
      get => this.\u003Cbacking_store\u003EProgramSymbolPrefix;
      set => this.\u003Cbacking_store\u003EProgramSymbolPrefix = value;
    }

    public bool IsProgramSymbolUseSuffix
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.\u003Cbacking_store\u003EIsProgramSymbolUseSuffix;
      [param: MarshalAs(UnmanagedType.U1)] set => this.\u003Cbacking_store\u003EIsProgramSymbolUseSuffix = value;
    }

    public string ProgramSymbolSuffix
    {
      get => this.\u003Cbacking_store\u003EProgramSymbolSuffix;
      set => this.\u003Cbacking_store\u003EProgramSymbolSuffix = value;
    }

    public bool IsRemoveFileExtension
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.\u003Cbacking_store\u003EIsRemoveFileExtension;
      [param: MarshalAs(UnmanagedType.U1)] set => this.\u003Cbacking_store\u003EIsRemoveFileExtension = value;
    }

    public CpkMaker([MarshalAs(UnmanagedType.U1)] bool usedlg) => this.initialize(usedlg);

    public CpkMaker() => this.initialize(false);

    private void initialize([MarshalAs(UnmanagedType.U1)] bool usedlg)
    {
      this.m_mutex = new Mutex();
      this.m_filedata = new CFileData();
      CBinary cbinary = new CBinary(usedlg);
      this.m_bin = cbinary;
      cbinary.DataAlign = 2048U;
      this.m_filedata.ToolVersion = "N/A" + ", DLL" + CpkMaker.GetDllVersionString();
      this.m_random_padding = false;
      this.DefineHeaderLocalFilePath = false;
      this.DefineHeaderCsvOrder = false;
      this.DefineHeaderUniqString = (string) null;
      this.m_cpkrootdir = "/";
    }

    private void \u007ECpkMaker() => this.finalize();

    private void \u0021CpkMaker() => this.finalize();

    private void finalize()
    {
      CFileData filedata = this.m_filedata;
      if (filedata != null)
      {
        filedata.Dispose();
        this.m_filedata = (CFileData) null;
      }
      CBinary bin = this.m_bin;
      if (bin != null)
      {
        bin.Dispose();
        this.m_bin?.Dispose();
        this.m_bin = (CBinary) null;
      }
      Mutex mutex = this.m_mutex;
      if (mutex == null)
        return;
      mutex.Dispose();
      this.m_mutex = (Mutex) null;
    }

    public void ClearFile()
    {
      this.m_filedata.Clear();
      this.CpkFileMode = CpkMaker.EnumCpkFileMode.ModeFilename;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public static bool IsEnableGroup(CpkMaker.EnumCpkFileMode fmode)
    {
      switch (fmode)
      {
        case CpkMaker.EnumCpkFileMode.ModeId:
        case CpkMaker.EnumCpkFileMode.ModeFilename:
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
          return false;
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
          return true;
        default:
          throw new ApplicationException("Unknown cpk file mode");
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public static bool IsEnableExtraId(CpkMaker.EnumCpkFileMode fmode)
    {
      switch (fmode)
      {
        case CpkMaker.EnumCpkFileMode.ModeId:
        case CpkMaker.EnumCpkFileMode.ModeFilename:
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
          return false;
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
        case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
          return true;
        default:
          throw new ApplicationException("Unknown cpk file mode");
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public static bool IsEnableId(CpkMaker.EnumCpkFileMode fmode)
    {
      switch (fmode)
      {
        case CpkMaker.EnumCpkFileMode.ModeId:
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
        case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
          return true;
        case CpkMaker.EnumCpkFileMode.ModeFilename:
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
          return false;
        default:
          throw new ApplicationException("Unknown cpk file mode");
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public static bool IsEnableContentsFilePath(CpkMaker.EnumCpkFileMode fmode)
    {
      switch (fmode)
      {
        case CpkMaker.EnumCpkFileMode.ModeId:
        case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
          return false;
        case CpkMaker.EnumCpkFileMode.ModeFilename:
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
          return true;
        default:
          throw new ApplicationException("Unknown cpk file mode");
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public static bool CanDuplicateFile(CpkMaker.EnumCpkFileMode fmode)
    {
      switch (fmode)
      {
        case CpkMaker.EnumCpkFileMode.ModeId:
        case CpkMaker.EnumCpkFileMode.ModeFilename:
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
          return false;
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
          return true;
        default:
          throw new ApplicationException("Unknown cpk file mode");
      }
    }

    public CFileInfo AddFile(
      string local_fpath,
      string cpk_fpath,
      uint id,
      [MarshalAs(UnmanagedType.U1)] bool comp,
      string groupName,
      string attrName,
      uint dataAlign,
      [MarshalAs(UnmanagedType.U1)] bool uniTarget,
      int regId)
    {
      local_fpath = local_fpath.Replace("\\", "/");
      char[] chArray = new char[1]{ '/' };
      cpk_fpath = this.m_cpkrootdir + cpk_fpath.Replace("\\", "/").Trim(chArray);
      this.m_filedata.AddFile(local_fpath, cpk_fpath, id, comp, groupName, attrName, dataAlign, uniTarget, regId);
      return (CFileInfo) null;
    }

    public CFileInfo AddFile(
      string local_fpath,
      string cpk_fpath,
      uint id,
      [MarshalAs(UnmanagedType.U1)] bool comp,
      string groupName,
      string attrName,
      uint dataAlign,
      [MarshalAs(UnmanagedType.U1)] bool uniTarget)
    {
      return this.AddFile(local_fpath, cpk_fpath, id, comp, groupName, attrName, dataAlign, uniTarget, -1);
    }

    public CFileInfo AddFile(
      string local_fpath,
      string cpk_fpath,
      uint id,
      [MarshalAs(UnmanagedType.U1)] bool comp,
      string groupName,
      string attrName,
      uint dataAlign)
    {
      return this.AddFile(local_fpath, cpk_fpath, id, comp, groupName, attrName, dataAlign, false, -1);
    }

    public CFileInfo AddFile(string local_fpath, string cpk_fpath, uint id, [MarshalAs(UnmanagedType.U1)] bool comp) => this.AddFile(local_fpath, cpk_fpath, id, comp, (string) null, (string) null, 0U, false, -1);

    public CFileInfo AddFile(string local_fpath, string cpk_fpath, uint id) => this.AddFile(local_fpath, cpk_fpath, id, false, (string) null, (string) null, 0U, false, -1);

    [return: MarshalAs(UnmanagedType.U1)]
    public bool AnalyzeCpkFile(string fpath)
    {
      this.m_mutex.WaitOne();
      this.m_filedata.Clear();
      bool flag = this.m_bin.AnalyzeCpkFile(this.m_filedata, fpath);
      if (flag)
        this.m_last_cpk_filename = fpath;
      this.m_mutex.ReleaseMutex();
      return flag;
    }

    public int TestConsistency(
      CpkMaker.EnumCpkFileMode test_cpk_file_mode,
      uint test_align,
      int test_group_data_align)
    {
      if (test_cpk_file_mode != (CpkMaker.EnumCpkFileMode) this.m_bin.CpkMode)
        return -1;
      uint num = test_align;
      switch (test_cpk_file_mode)
      {
        case CpkMaker.EnumCpkFileMode.ModeId:
        case CpkMaker.EnumCpkFileMode.ModeFilename:
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
          return (int) num == (int) this.m_bin.DataAlign ? 0 : -2;
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
        case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
          if (test_group_data_align > 0)
          {
            num = Math.Max(test_align, (uint) test_group_data_align);
            goto case CpkMaker.EnumCpkFileMode.ModeId;
          }
          else
            goto case CpkMaker.EnumCpkFileMode.ModeId;
        default:
          throw new ApplicationException("Unknown cpk file mode");
      }
    }

    public void DeleteFile(string fpath) => this.m_filedata.DeleteFile(fpath);

    private void resetTimer()
    {
      this.m_starttime = DateTime.UtcNow;
      this.m_prereturntime = new DateTime(0L);
    }

    private void createDirectory(string fpath)
    {
      string directoryName = Path.GetDirectoryName(fpath);
      if (directoryName == (string) null || directoryName == "" || Directory.Exists(directoryName))
        return;
      Directory.CreateDirectory(directoryName);
    }

    private static string LoadTextOneLine(string fname, int line)
    {
      string path = Path.Combine(Utility.GetModulePath(), fname);
      if (!File.Exists(path))
        return (string) null;
      Console.WriteLine(path + " found");
      StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
      int num = 0;
      string str1 = streamReader.ReadLine();
      string str2;
      if (!(str1 == (string) null))
      {
        do
        {
          str2 = str1.Trim();
          ++num;
          if (num != line)
            str1 = streamReader.ReadLine();
          else
            goto label_5;
        }
        while (!(str1 == (string) null));
      }
      str2 = "";
label_5:
      streamReader.Close();
      return str2;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public static bool LoadSettingFileUncompFileExt(out string uncompFileExt) => CpkMaker.LoadSettingFileCommonFileExt("CpkMaker.uncompfileext.settings", ".afs;.acx;.aax;.adx;.ahx;.aif;.aiff;.wav;.sfd;.m1v;.hca;.hcamx;.cpk;.aix;.awb;.acb;.acf;.usa;.usm;.usv;.self;.sprx;.sdat;.mself;.mpg;.mp3;.h264;.aac;.at9;.vag;.cwav;.p256;.sfp;.sfpb;.sai;.saib", out uncompFileExt);

    [return: MarshalAs(UnmanagedType.U1)]
    private static bool LoadSettingFileImageFileExt(out string imageFileExt) => CpkMaker.LoadSettingFileCommonFileExt("CpkMaker.imagefileext.settings", ".tpk", out imageFileExt);

    [return: MarshalAs(UnmanagedType.U1)]
    private static bool LoadSettingFileCommonFileExt(
      string fname,
      string defaultExt,
      out string fileExt)
    {
      string str = CpkMaker.LoadTextOneLine(fname, 1);
      if (str == (string) null)
      {
        Console.WriteLine(fname + " dose not exist. Default :" + defaultExt);
        fileExt = defaultExt;
        return false;
      }
      if (str == "")
      {
        Console.WriteLine(fname + " : [Unspecifyed]");
        fileExt = "";
        return true;
      }
      Console.WriteLine(fname + " : " + str);
      fileExt = str;
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private static bool LoadSettingFileCompCache(out string CacheDirectory, out long CacheLimitSize)
    {
      string str = CpkMaker.LoadTextOneLine("CpkMaker.compcache.settings", 1);
      if (str == (string) null)
      {
        Console.WriteLine("CpkMaker.compcache.settings" + " does not exist. Default setting is used.");
        return false;
      }
      if (str == "")
      {
        Console.WriteLine("Compress Cache Directory   : [Disabled]");
        CacheDirectory = "";
        CacheLimitSize = -1L;
        return true;
      }
      Console.WriteLine("Compress Cache Directory   : " + str);
      CacheDirectory = str;
      string s = CpkMaker.LoadTextOneLine("CpkMaker.compcache.settings", 2);
      if (string.IsNullOrEmpty(s))
      {
        Console.WriteLine("CpkMaker.compcache.settings" + " is not available. Please set limit size at 2nd line.");
        return false;
      }
      Console.WriteLine("Compress Cache Limit Size  : " + s);
      int startIndex = s.IndexOf("TB");
      long num;
      long result;
      bool flag;
      if (startIndex >= 0)
      {
        num = 1099511627776L;
      }
      else
      {
        startIndex = s.IndexOf("GB");
        if (startIndex >= 0)
        {
          num = 1073741824L;
        }
        else
        {
          startIndex = s.IndexOf("MB");
          if (startIndex >= 0)
          {
            num = 1048576L;
          }
          else
          {
            startIndex = s.IndexOf("KB");
            if (startIndex >= 0)
            {
              num = 1024L;
            }
            else
            {
              num = 1L;
              flag = long.TryParse(s, out result);
              goto label_16;
            }
          }
        }
      }
      flag = long.TryParse(s.Remove(startIndex), out result);
label_16:
      if (!flag)
      {
        Console.WriteLine("CpkMaker.compcache.settings" + " is not available. Parse error at 2nd line.");
        return false;
      }
      CacheLimitSize = result * num;
      return true;
    }

    public void LoadSettingFiles()
    {
      string fileExt = (string) null;
      if (!CpkMaker.LoadSettingFileCommonFileExt("CpkMaker.uncompfileext.settings", ".afs;.acx;.aax;.adx;.ahx;.aif;.aiff;.wav;.sfd;.m1v;.hca;.hcamx;.cpk;.aix;.awb;.acb;.acf;.usa;.usm;.usv;.self;.sprx;.sdat;.mself;.mpg;.mp3;.h264;.aac;.at9;.vag;.cwav;.p256;.sfp;.sfpb;.sai;.saib", out fileExt))
        return;
      this.m_bin.UncompFileExt = fileExt;
    }

    public static string GetCompCacheDirectory() => (string) null;

    public static long GetCompCacheLimitSize() => -1;

    public void StartToBuild(string fpath)
    {
      string fileExt = (string) null;
      if (CpkMaker.LoadSettingFileCommonFileExt("CpkMaker.uncompfileext.settings", ".afs;.acx;.aax;.adx;.ahx;.aif;.aiff;.wav;.sfd;.m1v;.hca;.hcamx;.cpk;.aix;.awb;.acb;.acf;.usa;.usm;.usv;.self;.sprx;.sdat;.mself;.mpg;.mp3;.h264;.aac;.at9;.vag;.cwav;.p256;.sfp;.sfpb;.sai;.saib", out fileExt))
        this.m_bin.UncompFileExt = fileExt;
      this.m_mutex.WaitOne();
      if (this.m_bin.EnableMself)
        fpath += ".mself";
      string directoryName = Path.GetDirectoryName(fpath);
      if (!(directoryName == (string) null) && !(directoryName == "") && !Directory.Exists(directoryName))
        Directory.CreateDirectory(directoryName);
      this.m_starttime = DateTime.UtcNow;
      this.m_prereturntime = new DateTime(0L);
      if (this.m_bin.StartBuildCpkFile(this.m_filedata, fpath))
        this.m_last_cpk_filename = fpath;
      this.m_mutex.ReleaseMutex();
    }

    public void StartToBuildAfs2(string fpath)
    {
      string fileExt = (string) null;
      if (CpkMaker.LoadSettingFileCommonFileExt("CpkMaker.uncompfileext.settings", ".afs;.acx;.aax;.adx;.ahx;.aif;.aiff;.wav;.sfd;.m1v;.hca;.hcamx;.cpk;.aix;.awb;.acb;.acf;.usa;.usm;.usv;.self;.sprx;.sdat;.mself;.mpg;.mp3;.h264;.aac;.at9;.vag;.cwav;.p256;.sfp;.sfpb;.sai;.saib", out fileExt))
        this.m_bin.UncompFileExt = fileExt;
      this.CpkFileMode = CpkMaker.EnumCpkFileMode.ModeId;
      this.m_bin.EnableCrc = false;
      this.m_bin.Mask = false;
      this.m_bin.ForceCompress = false;
      this.m_bin.EnableAfs2 = true;
      uint index = 0;
      if (0UL < this.m_filedata.Count)
      {
        CFileData filedata;
        do
        {
          this.m_filedata.FileInfos[(int) index].TryCompress = false;
          ++index;
          filedata = this.m_filedata;
        }
        while ((ulong) index < filedata.Count);
      }
      this.StartToBuild(fpath);
    }

    public void StartToExtract(string outdirdir)
    {
      this.m_mutex.WaitOne();
      this.m_starttime = DateTime.UtcNow;
      this.m_prereturntime = new DateTime(0L);
      this.m_bin.StartExtractFile(this.m_last_cpk_filename, outdirdir);
      this.m_mutex.ReleaseMutex();
    }

    public Status Execute()
    {
      try
      {
        this.m_mutex.WaitOne();
        return this.m_bin.Execute();
      }
      catch (FileNotFoundException ex)
      {
        Console.WriteLine("File not found.\r\n" + ex.Message);
        throw new FileNotFoundException(ex.Message);
      }
      catch (Exception ex)
      {
        Console.WriteLine("ExceptionCatch : Excute " + ex.Message);
        throw new Exception(ex.ToString());
      }
      finally
      {
        this.m_mutex.ReleaseMutex();
      }
    }

    public void Stop()
    {
      this.m_mutex.WaitOne();
      this.m_bin.Stop();
      this.m_mutex.ReleaseMutex();
    }

    public double GetProgress()
    {
      CBinary bin = this.m_bin;
      if (bin == null)
        return 0.0;
      double progress = bin.GetProgress();
      if (progress < 0.0)
        return 0.0;
      return progress > 100.0 ? 100.0 : progress;
    }

    public TimeSpan GetElapsedTime() => DateTime.UtcNow - this.m_starttime;

    public TimeSpan GetRemainTime()
    {
      CBinary bin = this.m_bin;
      double num1;
      if (bin == null)
      {
        num1 = 0.0;
      }
      else
      {
        double progress = bin.GetProgress();
        num1 = progress >= 0.0 ? (progress <= 100.0 ? progress : 100.0) : 0.0;
      }
      TimeSpan timeSpan1 = DateTime.UtcNow - this.m_starttime;
      DateTime utcNow = DateTime.UtcNow;
      DateTime dateTime = utcNow;
      double num2 = num1 > 0.0 ? 100.0 / num1 : 0.0;
      TimeSpan timeSpan2 = new TimeSpan();
      if ((utcNow - this.m_prereturntime).TotalSeconds >= 1.0)
      {
        TimeSpan timeSpan3 = DateTime.UtcNow - this.m_starttime;
        this.m_preremaintime = new TimeSpan((long) ((double) timeSpan1.Ticks * num2)) - timeSpan3;
        this.m_prereturntime = dateTime;
      }
      if (this.m_preremaintime.TotalSeconds < 0.0)
        this.m_preremaintime = new TimeSpan(0L);
      return this.m_preremaintime;
    }

    public string GetRemainTimeString()
    {
      TimeSpan remainTime = this.GetRemainTime();
      return string.Format("{0}:{1:00}:{2:00}", (object) remainTime.Hours, (object) remainTime.Minutes, (object) remainTime.Seconds);
    }

    public string GetElapsedTimeString()
    {
      TimeSpan timeSpan = DateTime.UtcNow - this.m_starttime;
      return string.Format("{0}:{1:00}:{2:00}", (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
    }

    public string ExportHeader(
      string outputfname,
      CpkMaker.EnumCpkFileMode filemode,
      [MarshalAs(UnmanagedType.U1)] bool shortFilename)
    {
      COutputHeader coutputHeader = new COutputHeader();
      coutputHeader.DefineLocalFilePath = this.DefineHeaderLocalFilePath;
      coutputHeader.DefineHeaderUniqString = this.DefineHeaderUniqString;
      coutputHeader.DefineHeaderCsvOrder = this.DefineHeaderCsvOrder;
      coutputHeader.FilenameOnly = shortFilename;
      bool programSymbolUsePrefix = this.\u003Cbacking_store\u003EIsProgramSymbolUsePrefix;
      coutputHeader.IsProgramSymbolUsePrefix = programSymbolUsePrefix;
      string programSymbolPrefix = this.\u003Cbacking_store\u003EProgramSymbolPrefix;
      coutputHeader.ProgramSymbolPrefix = programSymbolPrefix;
      bool programSymbolUseSuffix = this.\u003Cbacking_store\u003EIsProgramSymbolUseSuffix;
      coutputHeader.IsProgramSymbolUseSuffix = programSymbolUseSuffix;
      string programSymbolSuffix = this.\u003Cbacking_store\u003EProgramSymbolSuffix;
      coutputHeader.ProgramSymbolSuffix = programSymbolSuffix;
      bool removeFileExtension = this.\u003Cbacking_store\u003EIsRemoveFileExtension;
      coutputHeader.IsRemoveFileExtension = removeFileExtension;
      switch (filemode)
      {
        case CpkMaker.EnumCpkFileMode.ModeId:
          coutputHeader.ExportHeaderId(this.m_filedata, this.m_bin, outputfname, this.m_last_cpk_filename);
          break;
        case CpkMaker.EnumCpkFileMode.ModeFilename:
          coutputHeader.ExportHeaderFilename(this.m_filedata, this.m_bin, outputfname, this.m_last_cpk_filename);
          break;
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
          coutputHeader.ExportHeaderFilenameId(this.m_filedata, this.m_bin, outputfname, this.m_last_cpk_filename);
          break;
        case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
          coutputHeader.ExportHeaderFilenameGroup(this.m_filedata, this.m_bin, outputfname, this.m_last_cpk_filename);
          break;
        case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
          coutputHeader.ExportHeaderIdGroup(this.m_filedata, this.m_bin, outputfname, this.m_last_cpk_filename);
          break;
        case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
          coutputHeader.ExportHeaderFilenameIdGroup(this.m_filedata, this.m_bin, outputfname, this.m_last_cpk_filename);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (filemode), "Invalid file name mode in header exporting.");
      }
      return coutputHeader.ErrorString;
    }

    public string ExportHeader(string outputfname, CpkMaker.EnumCpkFileMode filemode) => this.ExportHeader(outputfname, filemode, false);

    [return: MarshalAs(UnmanagedType.U1)]
    public bool SaveAddRangeCsvFile(string addRangeCsvFname, string cpkFilename)
    {
      if (!File.Exists(cpkFilename))
        return false;
      long length = new FileInfo(cpkFilename).Length;
      try
      {
        StreamWriter streamWriter1 = new StreamWriter(addRangeCsvFname, false);
        StreamWriter streamWriter2;
        // ISSUE: fault handler
        try
        {
          streamWriter2 = streamWriter1;
          if (this.m_bin.Analyzed)
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            long num1 = 0;
            stringBuilder1.Append(num1.ToString());
            stringBuilder1.Append(",");
            long num2 = 2048;
            stringBuilder1.Append(num2.ToString());
            streamWriter2.WriteLine(stringBuilder1.ToString());
            StringBuilder stringBuilder2 = new StringBuilder();
            long num3 = (long) this.m_bin.ContentsOffset + (long) this.m_bin.ContentsSize;
            long num4 = length - num3;
            long num5 = num3;
            stringBuilder2.Append(num5.ToString());
            stringBuilder2.Append(",");
            long num6 = num4;
            stringBuilder2.Append(num6.ToString());
            streamWriter2.WriteLine(stringBuilder2.ToString());
          }
          else
          {
            StringBuilder stringBuilder = new StringBuilder();
            long num7 = 0;
            stringBuilder.Append(num7.ToString());
            stringBuilder.Append(",");
            long num8 = length;
            stringBuilder.Append(num8.ToString());
            streamWriter2.WriteLine(stringBuilder.ToString());
          }
          streamWriter2.Close();
        }
        __fault
        {
          streamWriter2.Dispose();
        }
        streamWriter2.Dispose();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Failed to save a CSV File. " + ex.Message);
        throw new Exception(ex.ToString());
      }
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool SwapCpkMask(string cpkFname) => this.m_bin.SwapMask(cpkFname);

    public void StartToVerify(string cpkFname, string outdir)
    {
      this.m_mutex.WaitOne();
      this.m_starttime = DateTime.UtcNow;
      this.m_prereturntime = new DateTime(0L);
      this.m_bin.StartVerifyFile(cpkFname, outdir);
      this.m_mutex.ReleaseMutex();
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool WaitForComplete()
    {
      Status status = this.Execute();
      if (status != Status.Complete)
      {
        while (status != Status.Error)
        {
          status = this.Execute();
          if (status == Status.Complete)
            goto label_3;
        }
        return false;
      }
label_3:
      return true;
    }

    public unsafe string GetCpkInformationString([MarshalAs(UnmanagedType.U1)] bool contents_info, [MarshalAs(UnmanagedType.U1)] bool aligned)
    {
      string str1 = "";
      string lastCpkFilename = this.m_last_cpk_filename;
      ulong count = this.m_filedata.Count;
      int num1 = 22;
      if (!aligned)
        num1 = 0;
      string str2 = "CPK Filename".PadLeft(num1);
      string str3 = str1 + (str2 + "：" + Path.GetFileName(lastCpkFilename) + "\r\n");
      int revision = this.m_bin.Revision;
      int version = this.m_bin.Version;
      string str4 = "File format version".PadLeft(num1);
      string str5 = str3 + (str4 + "：Ver." + version.ToString() + ", Rev." + revision.ToString() + "\r\n");
      uint dataAlign = this.m_bin.DataAlign;
      string str6 = "Data alignment".PadLeft(num1);
      string str7 = str5 + (str6 + "：" + dataAlign.ToString("N0") + "\r\n");
      ulong num2 = count;
      string str8 = "Content files".PadLeft(num1);
      string str9 = str7 + (str8 + "：" + num2.ToString("N0") + "\r\n");
      ulong compressedFiles = this.m_filedata.CompressedFiles;
      string str10 = "Compressed files".PadLeft(num1);
      string str11 = str9 + (str10 + "：" + compressedFiles.ToString("N0") + "\r\n");
      ulong originalContentsSize = this.m_filedata.OriginalContentsSize;
      string str12 = "Content file size".PadLeft(num1);
      string str13 = str11 + (str12 + "：" + originalContentsSize.ToString("N0") + "\r\n");
      double compressionPercentage = this.m_filedata.CompressionPercentage;
      ulong contentsSize = this.m_filedata.ContentsSize;
      string str14 = "Compressed file size".PadLeft(num1);
      string str15 = str13 + (str14 + "：" + contentsSize.ToString("N0") + " (" + compressionPercentage.ToString("F2") + "%)" + "\r\n");
      bool enableToc = this.m_bin.EnableToc;
      string str16 = str15 + (this.getAlignedString("Enable Filename info.", num1) + "：" + enableToc.ToString().PadRight(6));
      if (this.m_bin.EnableToc)
      {
        uint htocSize = this.HtocSize;
        uint tocSize = this.TocSize;
        string str17 = str16 + (" (" + tocSize.ToString("N0") + " + " + htocSize.ToString("N0") + " bytes) ");
        str16 = !this.Sorted ? str17 + "[Unsorted]" : str17 + "[Sorted]";
      }
      string str18 = str16 + "\r\n";
      bool enableItoc = this.EnableItoc;
      string str19 = str18 + (this.getAlignedString("Enable ID info.", num1) + "：" + enableItoc.ToString().PadRight(6));
      if (this.EnableItoc)
      {
        uint itocSize = this.ItocSize;
        str19 += " (" + itocSize.ToString("N0") + " bytes)";
      }
      string str20 = str19 + "\r\n";
      bool enableGtoc = this.EnableGtoc;
      string str21 = str20 + (this.getAlignedString("Enable Group info.", num1) + "：" + enableGtoc.ToString().PadRight(6));
      if (this.EnableGtoc)
      {
        uint hgtocSize = this.HgtocSize;
        uint gtocSize = this.GtocSize;
        str21 += " (" + gtocSize.ToString("N0") + " + " + hgtocSize.ToString("N0") + " bytes) ";
      }
      string str22 = str21 + "\r\n";
      bool enableGinfo = this.EnableGInfo;
      string str23 = str22 + (this.getAlignedString("Enable GInfo Table", num1) + "：" + enableGinfo.ToString().PadRight(6) + "\r\n");
      bool enableCrc32 = this.EnableCrc32;
      string str24 = str23 + (this.getAlignedString("Enable CRC32 info.", num1) + "：" + enableCrc32.ToString().PadRight(6) + "\r\n");
      bool enableCheckSum64 = this.EnableCheckSum64;
      string str25 = str24 + (this.getAlignedString("Enable CheckSum64 info.", num1) + "：" + enableCheckSum64.ToString().PadRight(6) + "\r\n") + (this.getAlignedString("Compression Mode", num1) + "：" + this.CompressCodecString + "\r\n");
      if (this.DpkDividedSize > 64)
      {
        int dpkDividedSize = this.DpkDividedSize;
        str25 += this.getAlignedString("- Divided Size", num1) + "：" + dpkDividedSize.ToString("N0").PadRight(6) + "\r\n";
      }
      sbyte* pointer = (sbyte*) Marshal.StringToHGlobalAnsi(lastCpkFilename).ToPointer();
      int workSizeForBindCpk = \u003CModule\u003E.CpkBinder_CalculateWorkSizeForBindCpk(pointer);
      Marshal.FreeHGlobal((IntPtr) (void*) pointer);
      int num3 = workSizeForBindCpk;
      string informationString = str25 + (this.getAlignedString("Work size to bind CPK", num1) + "：" + num3.ToString("N0") + " bytes\r\n") + (this.getAlignedString("Tool version", num1) + "：" + this.ToolVersion + "\r\n") + "\r\n";
      if (!contents_info)
        return informationString;
      if (this.EnableToc && this.EnableItoc)
      {
        informationString += "No.         ID    Filesize  Compressed       %  Contents Filename" + "\r\n";
        uint index = 0;
        if (0UL < count)
        {
          do
          {
            CFileInfo fileInfo = this.FileData.FileInfos[(int) index];
            object[] objArray = new object[6];
            uint num4 = index;
            objArray[0] = (object) num4.ToString();
            objArray[1] = (object) fileInfo.FileId;
            objArray[2] = (object) fileInfo.Extractsize;
            objArray[3] = (object) fileInfo.Filesize;
            objArray[4] = (object) fileInfo.CompressPercentage;
            objArray[5] = (object) fileInfo.ContentFilePath;
            string str26 = string.Format("[{0,5:00000}] {1,6} {2,11:###,##0} {3,11:###,##0} {4,7:###.#0}  {5}", objArray);
            informationString += str26 + "\r\n";
            ++index;
          }
          while ((ulong) index < count);
        }
      }
      else if (this.EnableToc)
      {
        informationString += "No.        Filesize  Compressed       %  Contents Filename" + "\r\n";
        uint index = 0;
        if (0UL < count)
        {
          do
          {
            CFileInfo fileInfo = this.FileData.FileInfos[(int) index];
            object[] objArray = new object[5];
            uint num5 = index;
            objArray[0] = (object) num5.ToString();
            objArray[1] = (object) fileInfo.Extractsize;
            objArray[2] = (object) fileInfo.Filesize;
            objArray[3] = (object) fileInfo.CompressPercentage;
            objArray[4] = (object) fileInfo.ContentFilePath;
            string str27 = string.Format("[{0,5:00000}] {1,11:###,##0} {2,11:###,##0} {3,7:###.#0}  {4}", objArray);
            informationString += str27 + "\r\n";
            ++index;
          }
          while ((ulong) index < count);
        }
      }
      else if (this.EnableItoc)
      {
        informationString += "No.         ID    Filesize  Compressed       %" + "\r\n";
        uint index = 0;
        if (0UL < count)
        {
          do
          {
            CFileInfo fileInfo = this.FileData.FileInfos[(int) index];
            object[] objArray = new object[5];
            uint num6 = index;
            objArray[0] = (object) num6.ToString();
            objArray[1] = (object) fileInfo.FileId;
            objArray[2] = (object) fileInfo.Extractsize;
            objArray[3] = (object) fileInfo.Filesize;
            objArray[4] = (object) fileInfo.CompressPercentage;
            string str28 = string.Format("[{0,5:00000}] {1,6} {2,11:###,##0} {3,11:###,##0} {4,7:###.#0} ", objArray);
            informationString += str28 + "\r\n";
            ++index;
          }
          while ((ulong) index < count);
        }
      }
      return informationString;
    }

    public string GetCpkInformationString() => this.GetCpkInformationString(true, true);

    private string getAlignedString(string instr, int space) => instr.PadLeft(space);

    public static string GetDllVersionString()
    {
      FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
      return string.Format("{0}.{1}.{2}", (object) versionInfo.FileMajorPart.ToString("D"), (object) versionInfo.FileMinorPart.ToString("D2"), (object) versionInfo.FileBuildPart.ToString("D2"));
    }

    public static int GetDllRemainDays(int defday) => UtValidatedDate.GetRemainderValidatedDate(defday);

    public static string GetDllRemainDays365String()
    {
      string modulePath = Utility.GetModulePath();
      string str1 = "CriAudioCraft.exe";
      string str2 = "CriAtomCraft.exe";
      return !File.Exists(Path.Combine(modulePath, "1f25c695")) && !File.Exists(str1) && !File.Exists(Path.Combine(modulePath, str1)) && !File.Exists(str2) && !File.Exists(Path.Combine(modulePath, str2)) ? (string) null : (string) null;
    }

    public static int GetCompressedSize(string fpath, int codec, CodecOptions co)
    {
      string fileExt = (string) null;
      CAsyncFile casyncFile = new CAsyncFile(codec);
      casyncFile.SetCompressOptions(co);
      string defaultExt = ".afs;.acx;.aax;.adx;.ahx;.aif;.aiff;.wav;.sfd;.m1v;.hca;.hcamx;.cpk;.aix;.awb;.acb;.acf;.usa;.usm;.usv;.self;.sprx;.sdat;.mself;.mpg;.mp3;.h264;.aac;.at9;.vag;.cwav;.p256;.sfp;.sfpb;.sai;.saib";
      string ext_list = defaultExt;
      if (CpkMaker.LoadSettingFileCommonFileExt("CpkMaker.uncompfileext.settings", defaultExt, out fileExt))
        ext_list = fileExt;
      string fpath1 = fpath;
      if (Utility.SearchExtList(ext_list, fpath1))
        return (int) new FileInfo(fpath).Length;
      int compressedSize = casyncFile.GetCompressedSize(fpath);
      if (casyncFile == null)
        return compressedSize;
      casyncFile.Dispose();
      return compressedSize;
    }

    public static string GetDllVersionFromResource() => CpkMaker.GetDllVersionString();

    public static string GetUpdateCpkFileName(string cpkFilePath)
    {
      string fileName = Path.GetFileName(cpkFilePath);
      string newValue = Path.GetFileNameWithoutExtension(cpkFilePath) + "_update" + Path.GetExtension(cpkFilePath);
      return cpkFilePath.Replace(fileName, newValue);
    }

    public List<string> GetGroupStrings([MarshalAs(UnmanagedType.U1)] bool allGroups) => this.m_filedata.GetGroupStrings(allGroups);

    public List<string> GetAttributeStrings() => this.m_filedata.GetAttributeStrings();

    [return: MarshalAs(UnmanagedType.U1)]
    public bool TestCpkFile(string fpath, CSelfTest.PrintEvent eve, string group, string attr)
    {
      CSelfTest cselfTest = new CSelfTest();
      bool flag = cselfTest.TestCpkFile(fpath, eve, group, attr);
      if (cselfTest is IDisposable disposable)
        disposable.Dispose();
      return flag;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool TestCpkFile(string fpath, CSelfTest.PrintEvent eve)
    {
      CSelfTest cselfTest = new CSelfTest();
      bool flag = cselfTest.TestCpkFile(fpath, eve, (string) null, (string) null);
      if (cselfTest is IDisposable disposable)
        disposable.Dispose();
      return flag;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool TestCpkFileCompress(CSelfTest.PrintEvent eve)
    {
      string str1 = (string) null;
      CSelfTest cselfTest = new CSelfTest();
      string lastCpkFilename = this.m_last_cpk_filename;
      int num1 = 0;
      string path = "temp.dat";
      if (this.m_bin.EnableCrc)
      {
        if (this.m_bin.EnableCrc32)
          str1 = "CRC32";
        if (this.m_bin.EnableCheckSum64)
          str1 = "CheckSum64";
        cselfTest.PrintWriteLine(eve, string.Format("\r\n=== {0} Check ===\r\n", (object) str1));
        int index = 0;
        if (0UL < this.m_filedata.Count)
        {
          CFileData filedata1;
          do
          {
            string contentFilePath = this.m_filedata.FileInfos[index].ContentFilePath;
            string str2;
            if (string.IsNullOrEmpty(contentFilePath))
            {
              str2 = "ID=" + this.m_filedata.FileInfos[index].FileId.ToString();
            }
            else
            {
              uint fileId = this.m_filedata.FileInfos[index].FileId;
              str2 = contentFilePath + "(ID=" + fileId.ToString() + ")";
            }
            cselfTest.PrintWriteLine(eve, string.Format("Analyzing {0} in CPK \"{1}\" ... ", (object) str1, (object) str2));
            bool flag;
            if (this.m_bin.EnableCrc32)
            {
              CFileData filedata2 = this.m_filedata;
              uint num2 = this.m_bin.RecalcCrc32InCpk(lastCpkFilename, filedata2.FileInfos[index]);
              uint crc32 = this.m_filedata.FileInfos[index].Crc32;
              uint num3 = crc32;
              uint num4 = num2;
              cselfTest.PrintWriteLine(eve, string.Format("0x{0} (CPK:0x{1}) ", (object) num4.ToString("X08"), (object) num3.ToString("X08")));
              flag = (int) num2 == (int) crc32;
            }
            else
            {
              CFileData filedata3 = this.m_filedata;
              ulong num5 = this.m_bin.RecalcCheckSum64InCpk(lastCpkFilename, filedata3.FileInfos[index]);
              ulong checkSum64 = this.m_filedata.FileInfos[index].CheckSum64;
              ulong num6 = checkSum64;
              ulong num7 = num5;
              cselfTest.PrintWriteLine(eve, string.Format("0x{0} (CPK:0x{1}) ", (object) num7.ToString("X016"), (object) num6.ToString("X016")));
              flag = (long) num5 == (long) checkSum64;
            }
            if (flag)
            {
              cselfTest.PrintWriteLine(eve, "Succeed.\r\n");
            }
            else
            {
              cselfTest.PrintWriteLine(eve, string.Format("{0} Failure.\r\n", (object) str1));
              ++num1;
            }
            ++index;
            filedata1 = this.m_filedata;
          }
          while ((ulong) index < filedata1.Count);
        }
      }
      else
        cselfTest.PrintWriteLine(eve, "This CPK file has no CRC/CheckSum info.\r\n");
      if (cselfTest is IDisposable disposable)
        disposable.Dispose();
      File.Delete(path);
      int num8 = num1;
      cselfTest.PrintWriteLine(eve, string.Format("Error count = {0}.\r\n", (object) num8.ToString()));
      return num1 == 0;
    }

    public void DebugPrintInternalInfo()
    {
      Console.WriteLine("##################################################################");
      Console.WriteLine("# File Info.");
      this.m_filedata.DebugPrintFileInfo();
      Console.WriteLine("# Attribute Info.");
      this.m_filedata.GroupingManager.DebugPrintAttributeInfo();
      Console.WriteLine("##################################################################");
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECpkMaker();
      }
      else
      {
        try
        {
          this.\u0021CpkMaker();
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

    ~CpkMaker() => this.Dispose(false);

    public enum EnumCpkFileMode
    {
      ModeNone = -1, // 0xFFFFFFFF
      ModeId = 0,
      ModeFilename = 1,
      ModeFilenameAndId = 2,
      ModeFilenameAndGroup = 3,
      ModeIdAndGroup = 4,
      ModeFilenameIdGroup = 5,
    }

    public enum EnumFileLayoutMode
    {
      LayoutModeUser,
      LayoutModeId,
      LayoutModeFilename,
      LayoutModeGroup,
    }
  }
}
