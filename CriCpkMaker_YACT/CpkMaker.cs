// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CpkMaker
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

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
    public bool DefineHeaderLocalFilePath;
    public string DefineHeaderUniqString;
    public bool DefineHeaderCsvOrder;

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

    public bool EnableToc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableToc;
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

    public bool EnableAfs2
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_bin.EnableAfs2;
    }

    public CpkMaker.EnumCpkFileMode CpkFileMode
    {
      get
      {
        if (this.m_bin.EnableToc && this.m_bin.EnableItoc && this.m_bin.EnableGtoc)
          return CpkMaker.EnumCpkFileMode.ModeIdAndGroup;
        if (this.m_bin.EnableToc && this.m_bin.EnableGtoc)
          return CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup;
        if (this.m_bin.EnableToc && this.m_bin.EnableItoc)
          return CpkMaker.EnumCpkFileMode.ModeFilenameAndId;
        if (this.m_bin.EnableToc)
          return CpkMaker.EnumCpkFileMode.ModeFilename;
        int num = 0;
        return this.m_bin.EnableItoc ? (CpkMaker.EnumCpkFileMode) num : (CpkMaker.EnumCpkFileMode) ~num;
      }
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
            break;
          case CpkMaker.EnumCpkFileMode.ModeFilename:
            this.m_bin.EnableToc = false;
            this.m_bin.EnableEtoc = false;
            this.m_bin.EnableItoc = true;
            this.m_bin.EnableGtoc = false;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            break;
          case CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = true;
            this.m_bin.EnableItoc = false;
            this.m_bin.EnableGtoc = false;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            break;
          case CpkMaker.EnumCpkFileMode.ModeFilenameAndGroup:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = true;
            this.m_bin.EnableItoc = true;
            this.m_bin.EnableGtoc = false;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            break;
          case CpkMaker.EnumCpkFileMode.ModeIdAndGroup:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = true;
            this.m_bin.EnableItoc = false;
            this.m_bin.EnableGtoc = true;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            break;
          case CpkMaker.EnumCpkFileMode.ModeFilenameIdGroup:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = false;
            this.m_bin.EnableItoc = true;
            this.m_bin.EnableGtoc = true;
            this.m_bin.EnableIdAndGroup = true;
            this.m_filedata.IdAndGroup = true;
            break;
          case CpkMaker.EnumCpkFileMode.ModeIdAndGroup | CpkMaker.EnumCpkFileMode.ModeFilenameAndId:
            this.m_bin.EnableToc = true;
            this.m_bin.EnableEtoc = true;
            this.m_bin.EnableItoc = true;
            this.m_bin.EnableGtoc = true;
            this.m_bin.EnableIdAndGroup = false;
            this.m_filedata.IdAndGroup = false;
            break;
        }
      }
    }

    public CpkMaker.EnumFileLayoutMode FileLayoutMode
    {
      get => (CpkMaker.EnumFileLayoutMode) this.m_bin.FileLayoutMode;
      set => this.m_bin.FileLayoutMode = (CBinary.EnumFileLayoutMode) value;
    }

    public int Version => this.m_bin.Version;

    public int Revision => this.m_bin.Revision;

    public uint TocSize => this.m_bin.TocSize;

    public uint ItocSize => this.m_bin.ItocSize;

    public uint GtocSize => this.m_bin.GtocSize;

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
      this.m_filedata.AddFile(local_fpath.Replace("\\", "/"), cpk_fpath, id, comp, groupName, attrName, dataAlign, uniTarget, -1);
      return (CFileInfo) null;
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
      this.m_filedata.AddFile(local_fpath.Replace("\\", "/"), cpk_fpath, id, comp, groupName, attrName, dataAlign, false, -1);
      return (CFileInfo) null;
    }

    public CFileInfo AddFile(string local_fpath, string cpk_fpath, uint id, [MarshalAs(UnmanagedType.U1)] bool comp)
    {
      this.m_filedata.AddFile(local_fpath.Replace("\\", "/"), cpk_fpath, id, comp, (string) null, (string) null, 0U, false, -1);
      return (CFileInfo) null;
    }

    public CFileInfo AddFile(string local_fpath, string cpk_fpath, uint id)
    {
      this.m_filedata.AddFile(local_fpath.Replace("\\", "/"), cpk_fpath, id, false, (string) null, (string) null, 0U, false, -1);
      return (CFileInfo) null;
    }

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

    public void ReloadUncompFileExtFile()
    {
      string path = Path.Combine(Utility.GetModulePath(), "CpkMaker.uncompfileext.settings");
      if (!File.Exists(path))
        return;
      StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
      string str = streamReader.ReadLine().Trim();
      Console.WriteLine(path + " found");
      Console.WriteLine("Uncompress File Extensions : " + str);
      this.m_bin.UncompFileExt = str;
      streamReader.Close();
    }

    public void StartToBuild(string fpath)
    {
      this.ReloadUncompFileExtFile();
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
          throw new Exception("Invalid file name mode in header exporting.");
      }
      if (coutputHeader is IDisposable disposable)
        disposable.Dispose();
      return coutputHeader.ErrorString;
    }

    public string ExportHeader(string outputfname, CpkMaker.EnumCpkFileMode filemode) => this.ExportHeader(outputfname, filemode, false);

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

    public string GetCpkInformationString([MarshalAs(UnmanagedType.U1)] bool contents_info, [MarshalAs(UnmanagedType.U1)] bool aligned)
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
      string str16 = "Enable Filename info.".PadLeft(num1);
      string str17 = str15 + (str16 + "：" + enableToc.ToString().PadRight(6));
      string str18;
      if (this.m_bin.EnableToc)
      {
        uint tocSize = this.TocSize;
        string str19 = str17 + (" (" + tocSize.ToString("N0") + " bytes) ");
        str18 = !this.Sorted ? str19 + "[Unsorted]\r\n" : str19 + "[Sorted]\r\n";
      }
      else
        str18 = str17 + "\r\n";
      bool enableItoc = this.EnableItoc;
      string str20 = str18 + (this.getAlignedString("Enable ID info.", num1) + "：" + enableItoc.ToString().PadRight(6));
      string str21;
      if (this.EnableItoc)
      {
        uint itocSize = this.ItocSize;
        str21 = str20 + (" (" + itocSize.ToString("N0") + " bytes)\r\n");
      }
      else
        str21 = str20 + "\r\n";
      bool enableGtoc = this.EnableGtoc;
      string str22 = str21 + (this.getAlignedString("Enable Group info.", num1) + "：" + enableGtoc.ToString().PadRight(6));
      string str23;
      if (this.EnableGtoc)
      {
        uint gtocSize = this.GtocSize;
        str23 = str22 + (" (" + gtocSize.ToString("N0") + " bytes)\r\n");
      }
      else
        str23 = str22 + "\r\n";
      bool enableGinfo = this.EnableGInfo;
      string str24 = str23 + (this.getAlignedString("Enable GInfo Table", num1) + "：" + enableGinfo.ToString().PadRight(6) + "\r\n");
      bool enableCrc = this.EnableCrc;
      string str25 = str24 + (this.getAlignedString("Enable CRC info.", num1) + "：" + enableCrc.ToString().PadRight(6) + "\r\n") + (this.getAlignedString("Compression Mode", num1) + "：" + this.CompressCodecString + "\r\n");
      if (this.DpkDividedSize > 64)
      {
        int dpkDividedSize = this.DpkDividedSize;
        str25 += this.getAlignedString("- Divided Size", num1) + "：" + dpkDividedSize.ToString("N0").PadRight(6) + "\r\n";
      }
      string informationString = str25 + (this.getAlignedString("Tool version", num1) + "：" + this.ToolVersion + "\r\n") + "\r\n";
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
            uint num3 = index;
            objArray[0] = (object) num3.ToString();
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
            uint num4 = index;
            objArray[0] = (object) num4.ToString();
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
            uint num5 = index;
            objArray[0] = (object) num5.ToString();
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
      string[] strArray = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.Replace(" ", "").Split(',');
      int index = 1;
      if (1 < strArray.Length)
      {
        do
        {
          if (strArray[index].Length == 1)
            strArray[index] = "0" + strArray[index];
          ++index;
        }
        while (index < strArray.Length);
      }
      return string.Format("{0}.{1}.{2}", (object) strArray[0], (object) strArray[1], (object) strArray[2]);
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
      CAsyncFile casyncFile = new CAsyncFile(codec);
      casyncFile.SetCompressOptions(co);
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
      CSelfTest cselfTest = new CSelfTest();
      string lastCpkFilename = this.m_last_cpk_filename;
      int num1 = 0;
      string path = "temp.dat";
      cselfTest.PrintWriteLine(eve, "\r\n=== CRC Check ===\r\n");
      int index = 0;
      if (0UL < this.m_filedata.Count)
      {
        CFileData filedata1;
        do
        {
          string contentFilePath = this.m_filedata.FileInfos[index].ContentFilePath;
          string str;
          if (string.IsNullOrEmpty(contentFilePath))
          {
            str = "ID=" + this.m_filedata.FileInfos[index].FileId.ToString();
          }
          else
          {
            uint fileId = this.m_filedata.FileInfos[index].FileId;
            str = contentFilePath + "(ID=" + fileId.ToString() + ")";
          }
          cselfTest.PrintWriteLine(eve, string.Format("Analyzing CRC in CPK \"{0}\" ... ", (object) str));
          if (this.m_filedata.FileInfos[index].Crc32 == 0U)
          {
            cselfTest.PrintWriteLine(eve, "no CRC.\r\n");
          }
          else
          {
            CFileData filedata2 = this.m_filedata;
            uint num2 = this.m_bin.RecalcCrc32InCpk(lastCpkFilename, filedata2.FileInfos[index]);
            uint crc32 = this.m_filedata.FileInfos[index].Crc32;
            uint num3 = num2;
            cselfTest.PrintWriteLine(eve, string.Format("0x{0} (CPK:0x{1}) ", (object) num3.ToString("X08"), (object) crc32.ToString("X08")));
            CFileData filedata3 = this.m_filedata;
            if ((int) num2 == (int) filedata3.FileInfos[index].Crc32)
            {
              cselfTest.PrintWriteLine(eve, "Succeed.\r\n");
            }
            else
            {
              cselfTest.PrintWriteLine(eve, "CRC Failure.\r\n");
              ++num1;
            }
          }
          ++index;
          filedata1 = this.m_filedata;
        }
        while ((ulong) index < filedata1.Count);
      }
      if (cselfTest is IDisposable disposable)
        disposable.Dispose();
      File.Delete(path);
      int num4 = num1;
      cselfTest.PrintWriteLine(eve, string.Format("Error count = {0}.\r\n", (object) num4.ToString()));
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
