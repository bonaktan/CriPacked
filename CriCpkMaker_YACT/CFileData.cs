// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CFileData
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CFileData : IDisposable
  {
    private List<CFileInfo> m_filedata;
    private string m_basedir;
    private string m_comment;
    private string m_tool_version;
    private uint m_data_align;
    private CGroupingManager m_grouping_manager;
    private string m_last_sorted_type;
    private bool m_analyzed;
    private bool m_duplicate_by_group;
    private int m_duplicates;
    private int m_invalidfiles;
    private long m_total_file_size;
    private uint m_registered_current_id;
    private bool m_id_and_group;
    private bool m_sort_cont_group_file;

    public uint RegisteredCurrentId
    {
      set => this.m_registered_current_id = value;
      get => ++this.m_registered_current_id;
    }

    public ulong Count => (ulong) this.m_filedata.Count;

    public List<CFileInfo> FileInfos => this.m_filedata;

    public CFileInfo LastFileData => this.m_filedata[(int) (ulong) (this.m_filedata.Count - 1)];

    public string Comment
    {
      get => this.m_comment;
      set => this.m_comment = value;
    }

    public string ToolVersion
    {
      get => this.m_tool_version;
      set => this.m_tool_version = value;
    }

    public ulong ContentsSize
    {
      get
      {
        ulong contentsSize = 0;
        List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
        if (enumerator.MoveNext())
        {
          do
          {
            CFileInfo current = enumerator.Current;
            contentsSize += current.Filesize;
          }
          while (enumerator.MoveNext());
        }
        return contentsSize;
      }
    }

    public ulong OriginalContentsSize
    {
      get
      {
        ulong originalContentsSize = 0;
        List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
        if (enumerator.MoveNext())
        {
          do
          {
            CFileInfo current = enumerator.Current;
            originalContentsSize += current.Extractsize;
          }
          while (enumerator.MoveNext());
        }
        return originalContentsSize;
      }
    }

    public double CompressionPercentage
    {
      get
      {
        ulong originalContentsSize = this.OriginalContentsSize;
        return originalContentsSize == 0UL ? 0.0 : (double) this.ContentsSize / (double) originalContentsSize * 100.0;
      }
    }

    public ulong ContentsSizeWithPadding
    {
      get
      {
        ulong contentsSizeWithPadding = 0;
        List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
        if (enumerator.MoveNext())
        {
          do
          {
            CFileInfo current = enumerator.Current;
            contentsSizeWithPadding += current.FilesizeAligned;
          }
          while (enumerator.MoveNext());
        }
        return contentsSizeWithPadding;
      }
    }

    public ulong ContentsSizeBySectorWithPadding
    {
      get
      {
        ulong dataAlign = (ulong) this.m_data_align;
        return (ulong) ((long) this.ContentsSizeWithPadding - (long) dataAlign + -1L) / dataAlign;
      }
    }

    public ulong CompressedFiles
    {
      get
      {
        ulong compressedFiles = 0;
        List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
        if (enumerator.MoveNext())
        {
          do
          {
            if (enumerator.Current.IsCompressed)
              ++compressedFiles;
          }
          while (enumerator.MoveNext());
        }
        return compressedFiles;
      }
    }

    public string BaseDirectory
    {
      get => this.m_basedir;
      set => this.m_basedir = value.Replace("\\", "/");
    }

    public uint DataAlign
    {
      set
      {
        this.m_data_align = value;
        List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
        if (!enumerator.MoveNext())
          return;
        do
        {
          enumerator.Current.DataAlign = value;
        }
        while (enumerator.MoveNext());
      }
    }

    public CGroupingManager GroupingManager => this.m_grouping_manager;

    public bool Analyzed
    {
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_analyzed = value;
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_analyzed;
    }

    public bool EnableDuplicateByGroup
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_duplicate_by_group;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_duplicate_by_group = value;
    }

    public int Duplicates
    {
      get => this.m_duplicates;
      set => this.m_duplicates = value;
    }

    public int InvalidFiles
    {
      get => this.m_invalidfiles;
      set => this.m_invalidfiles = value;
    }

    public long TotalFileSize => this.m_total_file_size;

    public bool IdAndGroup
    {
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_id_and_group = value;
    }

    public bool SortContinuousGroupFile
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_sort_cont_group_file;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_sort_cont_group_file = value;
    }

    public CFileData()
    {
      this.m_filedata = new List<CFileInfo>();
      this.m_grouping_manager = new CGroupingManager();
      this.m_data_align = 2048U;
      this.m_basedir = "";
      this.m_last_sorted_type = "No sorted";
      this.m_analyzed = false;
      this.m_registered_current_id = 0U;
      this.m_invalidfiles = 0;
      this.m_duplicates = 0;
      this.m_sort_cont_group_file = true;
    }

    private void \u007ECFileData()
    {
    }

    private void \u0021CFileData()
    {
      List<CFileInfo> filedata = this.m_filedata;
      if (filedata == null)
        return;
      if (filedata is IDisposable disposable)
        disposable.Dispose();
      this.m_filedata = (List<CFileInfo>) null;
    }

    public ulong UpperToPadding(ulong val)
    {
      uint dataAlign = this.m_data_align;
      ulong num = (ulong) dataAlign;
      return ((ulong) (dataAlign - 1U) + val) / num * num;
    }

    public void Clear()
    {
      this.m_total_file_size = 0L;
      this.m_registered_current_id = 0U;
      this.m_filedata.Clear();
    }

    private CFileInfo GetRegistererdFileInfo(string contPath)
    {
      if (this.m_filedata.Count == 0)
        return (CFileInfo) null;
      char[] chArray = new char[1]{ '/' };
      contPath = contPath.Trim(chArray);
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
      if (enumerator.MoveNext())
      {
        CFileInfo current;
        do
        {
          current = enumerator.Current;
          if (current.ContentFilePath == contPath)
            goto label_5;
        }
        while (enumerator.MoveNext());
        goto label_6;
label_5:
        return current;
      }
label_6:
      return (CFileInfo) null;
    }

    public void AddFile(
      string fpath,
      string contPath,
      uint id,
      [MarshalAs(UnmanagedType.U1)] bool comp,
      string groupName,
      string attrName,
      uint dataAlign,
      [MarshalAs(UnmanagedType.U1)] bool uniTarget,
      int regId)
    {
      if (!this.m_duplicate_by_group)
      {
        this.AddFileOne(fpath, contPath, id, comp, groupName, attrName, dataAlign, uniTarget, regId);
      }
      else
      {
        if (regId >= 0)
          regId *= 100000;
        char[] chArray = new char[1]{ ',' };
        string[] strArray = groupName.Split(chArray);
        int index1 = 0;
        if (0 < strArray.Length)
        {
          do
          {
            strArray[index1] = strArray[index1].Trim();
            ++index1;
          }
          while (index1 < strArray.Length);
        }
        int index2 = 0;
        if (0 >= strArray.Length)
          return;
        do
        {
          this.AddFileOne(fpath, contPath, id, comp, strArray[index2], attrName, dataAlign, uniTarget, regId);
          if (regId >= 0)
            ++regId;
          ++index2;
        }
        while (index2 < strArray.Length);
      }
    }

    private void AddFileOne(
      string fpath,
      string contPath,
      uint id,
      [MarshalAs(UnmanagedType.U1)] bool comp,
      string groupName,
      string attrName,
      uint dataAlign,
      [MarshalAs(UnmanagedType.U1)] bool uniTarget,
      int regId)
    {
      if (this.m_analyzed)
      {
        string contPath1 = contPath;
        if (this.m_id_and_group)
          contPath1 = id.ToString();
        CFileInfo registererdFileInfo = this.GetRegistererdFileInfo(contPath1);
        if (registererdFileInfo != null)
        {
          registererdFileInfo.FileId = id;
          return;
        }
      }
      if (!File.Exists(fpath))
        throw new ArgumentException("\"" + fpath + "\"is not found.");
      CFileInfo finfo = new CFileInfo();
      FileInfo fileInfo = new FileInfo(fpath);
      if (contPath == (string) null)
        contPath = fpath;
      if (groupName == "")
        groupName = "(none)";
      if (dataAlign == 0U)
        dataAlign = this.m_data_align;
      finfo.LocalFilePath = fpath.Replace("\\", "/");
      finfo.ContentFilePath = contPath.Replace("\\", "/");
      finfo.Filesize = (ulong) fileInfo.Length;
      finfo.Extractsize = (ulong) fileInfo.Length;
      DateTime lastWriteTime = fileInfo.LastWriteTime;
      finfo.DateTime = lastWriteTime;
      finfo.FileId = id;
      finfo.DataAlign = dataAlign;
      finfo.TryCompress = comp;
      finfo.PackingOrder = -1L;
      finfo.GroupString = groupName;
      finfo.AttributeString = attrName;
      uint num1 = ++this.m_registered_current_id;
      finfo.RegisteredId = num1;
      finfo.InfoIndex = 0L;
      finfo.UniTarget = uniTarget;
      finfo.CsvRegisteredId = (long) regId;
      this.m_total_file_size = fileInfo.Length + this.m_total_file_size;
      if (this.m_filedata.Count > 0)
      {
        long filesize = (long) this.LastFileData.Filesize;
        ulong num2 = this.LastFileData.Filesize + this.LastFileData.Offset;
        uint dataAlign1 = this.m_data_align;
        ulong num3 = (ulong) dataAlign1;
        finfo.Offset = ((ulong) (dataAlign1 - 1U) + num2) / num3 * num3;
      }
      this.m_filedata.Add(finfo);
      this.m_grouping_manager.TryCreateGroupAndAttribute(finfo, groupName, attrName, this.m_sort_cont_group_file);
      this.m_grouping_manager.TryAddAttribute(attrName, (int) dataAlign);
    }

    public unsafe void AddDataByAnalyze(
      sbyte* vdir,
      sbyte* dir,
      sbyte* fname,
      ulong offs,
      uint fsize,
      uint extsize,
      uint id,
      uint crc32,
      sbyte* user,
      ulong infoIndex)
    {
      new string(vdir).Replace("\\", "/");
      string str1 = new string(dir);
      string str2 = str1.Replace("\\", "/");
      string str3 = new string(fname);
      CFileInfo cfileInfo = new CFileInfo();
      cfileInfo.ContentFilePath = str2 + "/" + str3;
      cfileInfo.LocalFilePath = str1 + "/" + str3;
      cfileInfo.Offset = offs;
      cfileInfo.Filesize = (ulong) fsize;
      cfileInfo.Extractsize = (ulong) extsize;
      DateTime dateTime = new DateTime(0L);
      cfileInfo.DateTime = dateTime;
      cfileInfo.Burned = true;
      cfileInfo.FileId = id;
      cfileInfo.Crc32 = crc32;
      uint num = ++this.m_registered_current_id;
      cfileInfo.RegisteredId = num;
      if (infoIndex != ulong.MaxValue)
        cfileInfo.InfoIndex = (long) infoIndex;
      this.m_filedata.Add(cfileInfo);
    }

    public CFileInfo GetFileData(string fpath)
    {
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
      if (enumerator.MoveNext())
      {
        CFileInfo current;
        do
        {
          current = enumerator.Current;
          if (current.LocalFilePath.Equals(fpath))
            goto label_3;
        }
        while (enumerator.MoveNext());
        goto label_4;
label_3:
        return current;
      }
label_4:
      return (CFileInfo) null;
    }

    public CFileInfo GetFileInfoByContentFileName(string contpath)
    {
      if (contpath[0] == '/')
        contpath = contpath.Substring(1);
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
      if (enumerator.MoveNext())
      {
        CFileInfo current;
        do
        {
          current = enumerator.Current;
          if (current.ContentFilePath.Equals(contpath))
            goto label_5;
        }
        while (enumerator.MoveNext());
        goto label_6;
label_5:
        return current;
      }
label_6:
      return (CFileInfo) null;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool DeleteFile(string fpath)
    {
      CFileInfo fileData = this.GetFileData(fpath);
      if (fileData == null)
        return false;
      this.m_total_file_size -= (long) fileData.Extractsize;
      return this.m_filedata.Remove(fileData);
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool DeleteFile(CFileInfo filedata)
    {
      this.m_total_file_size -= (long) filedata.Extractsize;
      return this.m_filedata.Remove(filedata);
    }

    public void RemoveNoTagFileInfos()
    {
      List<CFileInfo> cfileInfoList = new List<CFileInfo>();
      int index1 = 0;
      if (0 < this.m_filedata.Count)
      {
        List<CFileInfo> filedata1;
        do
        {
          if (this.m_filedata[index1].Tag == null)
          {
            List<CFileInfo> filedata2 = this.m_filedata;
            cfileInfoList.Add(filedata2[index1]);
          }
          ++index1;
          filedata1 = this.m_filedata;
        }
        while (index1 < filedata1.Count);
      }
      int index2 = 0;
      if (0 >= cfileInfoList.Count)
        return;
      do
      {
        Debug.WriteLine("&& Delete CpkMakrInf " + cfileInfoList[index2].ContentFilePath);
        this.m_total_file_size -= (long) cfileInfoList[index2].Extractsize;
        this.m_grouping_manager.DeleteFileInfoFromGroup(cfileInfoList[index2]);
        this.m_filedata.Remove(cfileInfoList[index2]);
        ++index2;
      }
      while (index2 < cfileInfoList.Count);
    }

    public void UpdateFileInfoIndex()
    {
      long num = 0;
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
      if (!enumerator.MoveNext())
        return;
      do
      {
        enumerator.Current.InfoIndex = num;
        ++num;
      }
      while (enumerator.MoveNext());
    }

    public void ResetPackingOrderAferAnalyzed()
    {
      long num = 0;
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerByOffset());
      this.m_last_sorted_type = "Sorted by Offset";
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
      if (!enumerator.MoveNext())
        return;
      do
      {
        enumerator.Current.PackingOrder = num;
        ++num;
      }
      while (enumerator.MoveNext());
    }

    public void UpdateFileInfoPackingOrder() => this.m_grouping_manager.UpdateFileInfoPackingOrder();

    public void SortByFilename()
    {
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerFname());
      this.m_last_sorted_type = "Sorted by Filename";
    }

    public void SortByIdName()
    {
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerIdName());
      this.m_last_sorted_type = "Sorted by IdName";
    }

    public void SortById()
    {
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerId());
      this.m_last_sorted_type = "Sorted by ID";
    }

    public void SortByPackingOrder()
    {
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerByOrder());
      this.m_last_sorted_type = "Sorted by PackingOrder";
    }

    public void SortByOffset()
    {
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerByOffset());
      this.m_last_sorted_type = "Sorted by Offset";
    }

    public void SortByRegisteredId()
    {
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerByRegisteredId());
      this.m_last_sorted_type = "Sorted by RegisterdId";
    }

    public void SortByCsvRegisteredId()
    {
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerByCsvRegisteredId());
      this.m_last_sorted_type = "Sorted by CsvRegisterdId";
    }

    public static void SortByInfoIndex(List<CFileInfo> flist) => flist.Sort((IComparer<CFileInfo>) new FileDataComparerByInfoIndex());

    public void SortByInfoIndex()
    {
      this.m_filedata.Sort((IComparer<CFileInfo>) new FileDataComparerByInfoIndex());
      this.m_last_sorted_type = "Sorted by InfoIndex";
    }

    public void SetGroupObjectIndex() => this.m_grouping_manager.SetObjectIndex();

    public void DebugPrintFileInfo()
    {
      Console.WriteLine("Analyzed  : " + this.m_analyzed.ToString());
      Console.WriteLine("Sort Type : " + this.m_last_sorted_type);
      string format = "[{0,05}] {1,6} {2, 6} {3,-32} {4,5} {5,10} {6,10} {7,10} {8,-18} {9}";
      object[] objArray1 = new object[10]
      {
        (object) "-----",
        (object) "burned",
        (object) "InfIdx",
        (object) "ContPath",
        (object) "Id",
        (object) "OrgSize",
        (object) "CompSize",
        (object) "Ofs.",
        (object) "Attr.",
        (object) "Grps."
      };
      Console.WriteLine(string.Format(format, objArray1));
      int num1 = 0;
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
      if (!enumerator.MoveNext())
        return;
      do
      {
        CFileInfo current = enumerator.Current;
        object[] objArray2 = new object[10];
        int num2 = num1;
        ++num1;
        objArray2[0] = (object) num2;
        bool burned = current.Burned;
        objArray2[1] = (object) burned.ToString();
        long infoIndex = current.InfoIndex;
        objArray2[2] = (object) infoIndex.ToString();
        objArray2[3] = (object) current.ContentFilePath;
        uint fileId = current.FileId;
        objArray2[4] = (object) fileId.ToString("N0");
        ulong extractsize = current.Extractsize;
        objArray2[5] = (object) extractsize.ToString("N0");
        ulong filesize = current.Filesize;
        objArray2[6] = (object) filesize.ToString("N0");
        ulong offset = current.Offset;
        objArray2[7] = (object) offset.ToString("N0");
        objArray2[8] = (object) current.AttributeString;
        objArray2[9] = (object) current.GroupString;
        Console.WriteLine(string.Format(format, objArray2));
      }
      while (enumerator.MoveNext());
    }

    public List<string> GetGroupStrings([MarshalAs(UnmanagedType.U1)] bool allGroups) => this.m_grouping_manager.GetGroupStrings(allGroups);

    public List<string> GetAttributeStrings() => this.m_grouping_manager.GetAttributeStrings();

    public override string ToString()
    {
      string str = (string) null;
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.GetEnumerator();
      if (enumerator.MoveNext())
      {
        do
        {
          CFileInfo current = enumerator.Current;
          str += current.ToString() + "\r\n";
        }
        while (enumerator.MoveNext());
      }
      return str;
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECFileData();
      }
      else
      {
        try
        {
          this.\u0021CFileData();
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

    ~CFileData() => this.Dispose(false);
  }
}
