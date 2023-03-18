// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.COutputHeader
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CriCpkMaker
{
  public class COutputHeader
  {
    private bool m_define_local_file_path;
    private CFileData m_filedata;
    private Dictionary<string, string> m_hash;
    private COutputHeader.EnumFilemode filemode;
    private bool m_filename_only;
    private StringBuilder m_error_string_builder;
    public string DefineHeaderUniqString;
    public bool DefineHeaderCsvOrder;

    public bool DefineLocalFilePath
    {
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_define_local_file_path = value;
    }

    public bool FilenameOnly
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_filename_only;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_filename_only = value;
    }

    public string ErrorString => this.m_error_string_builder.ToString();

    public COutputHeader()
    {
      this.m_error_string_builder = new StringBuilder();
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      this.m_hash = dictionary;
      dictionary.Clear();
      this.m_filename_only = false;
      this.m_define_local_file_path = false;
      this.DefineHeaderUniqString = (string) null;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool IsRegistered(string word, string filename)
    {
      if (filename == (string) null)
        return this.m_hash.ContainsKey(word);
      return this.m_hash.ContainsKey(word) && this.m_hash[word].CompareTo(filename) == 0;
    }

    private void ClearHash() => this.m_hash.Clear();

    private void Register(string word, string filename) => this.m_hash.Add(word, filename);

    private string makeKakkoString(int val) => "(" + val.ToString() + ")";

    private string makeDoubleQuoteString(string val)
    {
      string str = "\"";
      return str + val + str;
    }

    private string makeDefinitionString(string fname)
    {
      if (string.IsNullOrEmpty(fname))
        return fname;
      fname = fname.ToUpper();
      fname = fname.Replace(".", "_");
      fname = fname.Replace("/", "_");
      fname = fname.Replace(" ", "_");
      fname = fname.Replace("-", "_");
      fname = fname.Replace("\\", "_");
      fname = fname.Replace("(", "_");
      fname = fname.Replace(")", "_");
      fname = fname.Replace("[", "_");
      fname = fname.Replace("]", "_");
      fname = fname.Replace("{", "_");
      fname = fname.Replace("}", "_");
      fname = fname.Replace("!", "_");
      fname = fname.Replace("#", "_");
      fname = fname.Replace(",", "_");
      fname = fname.Replace("'", "_");
      fname = fname.Replace(":", "_");
      if (fname[0] >= '0' && fname[0] <= '9')
        fname = "_" + fname;
      return fname;
    }

    private string makeDefinitionStringId(CFileInfo filedata, int maxstr, string unique)
    {
      string str1 = !this.m_define_local_file_path ? filedata.ContentFilePath : filedata.LocalFilePath;
      string str2 = this.makeDefinitionString(this.getFileName(filedata));
      return "#define " + (this.getUniqueDefString(unique + str2, (string) null, true).PadRight(maxstr + 4) + " " + ("(" + ((int) filedata.FileId).ToString() + ")")) + " // " + str1;
    }

    private string getFileName(CFileInfo filedata)
    {
      if (this.m_define_local_file_path)
      {
        if (this.m_filename_only)
          return Path.GetFileName(filedata.LocalFilePath);
        string fileName = Path.Combine(Path.GetDirectoryName(filedata.LocalFilePath), Path.GetFileName(filedata.LocalFilePath));
        if (fileName.Length > 2 && fileName[1] == ':')
        {
          char[] chArray1 = new char[1]{ '/' };
          char[] chArray2 = new char[1]{ '\\' };
          fileName = fileName.Substring(2).Trim(chArray2).Trim(chArray1);
        }
        return fileName;
      }
      return !this.m_filename_only ? filedata.ContentFilePath : filedata.ContentFilename;
    }

    private string makeDefinitionStringFile(string instr, int maxstr, string unique)
    {
      string str1 = this.makeDefinitionString(instr).PadRight(maxstr + 4);
      string uniqueDefString = this.getUniqueDefString(unique + str1, (string) null, false);
      string str2 = "\"";
      string str3 = str2 + instr + str2;
      return "#define " + uniqueDefString + " " + str3;
    }

    private string makeDefinitionStringFile(CFileInfo filedata, int maxstr, string unique)
    {
      string str1 = this.makeDefinitionString(this.getFileName(filedata));
      string uniqueDefString = this.getUniqueDefString(unique + str1.PadRight(maxstr + 4), filedata.ContentFilePath, false);
      if (uniqueDefString == (string) null)
        return (string) null;
      string str2 = "#define " + uniqueDefString + " ";
      string str3;
      if (this.filemode == COutputHeader.EnumFilemode.IdGroup)
      {
        uint fileId = filedata.FileId;
        str3 = str2 + "(" + fileId.ToString() + ") \t// " + filedata.ContentFilePath;
      }
      else
      {
        string contentFilePath = filedata.ContentFilePath;
        string str4 = "\"";
        string str5 = str4 + contentFilePath + str4;
        str3 = str2 + str5;
      }
      return str3;
    }

    private int getMaxFilenameId(CFileData filedatas)
    {
      int maxFilenameId = 0;
      List<CFileInfo>.Enumerator enumerator = filedatas.FileInfos.GetEnumerator();
      if (enumerator.MoveNext())
      {
        do
        {
          CFileInfo current = enumerator.Current;
          string s;
          if (this.m_define_local_file_path)
          {
            if (this.m_filename_only)
            {
              s = Path.GetFileName(current.LocalFilePath);
            }
            else
            {
              string str = Path.Combine(Path.GetDirectoryName(current.LocalFilePath), Path.GetFileName(current.LocalFilePath));
              if (str.Length > 2 && str[1] == ':')
              {
                char[] chArray1 = new char[1]{ '/' };
                char[] chArray2 = new char[1]{ '\\' };
                str = str.Substring(2).Trim(chArray2).Trim(chArray1);
              }
              s = str;
            }
          }
          else
            s = this.m_filename_only ? current.ContentFilename : current.ContentFilePath;
          int byteCount = Encoding.GetEncoding("Shift_JIS").GetByteCount(s);
          if (s[0] >= '0' && s[0] <= '9')
            ++byteCount;
          if (byteCount > maxFilenameId)
            maxFilenameId = byteCount;
        }
        while (enumerator.MoveNext());
      }
      return maxFilenameId;
    }

    private int getMaxFilename(CFileData filedatas)
    {
      int maxFilename = 0;
      List<CFileInfo>.Enumerator enumerator = filedatas.FileInfos.GetEnumerator();
      if (enumerator.MoveNext())
      {
        do
        {
          CFileInfo current = enumerator.Current;
          string s;
          if (this.m_define_local_file_path)
          {
            if (this.m_filename_only)
            {
              s = Path.GetFileName(current.LocalFilePath);
            }
            else
            {
              string str = Path.Combine(Path.GetDirectoryName(current.LocalFilePath), Path.GetFileName(current.LocalFilePath));
              if (str.Length > 2 && str[1] == ':')
              {
                char[] chArray1 = new char[1]{ '/' };
                char[] chArray2 = new char[1]{ '\\' };
                str = str.Substring(2).Trim(chArray2).Trim(chArray1);
              }
              s = str;
            }
          }
          else
            s = this.m_filename_only ? current.ContentFilename : current.ContentFilePath;
          int byteCount = Encoding.GetEncoding("Shift_JIS").GetByteCount(s);
          if (s[0] >= '0' && s[0] <= '9')
            ++byteCount;
          if (byteCount > maxFilename)
            maxFilename = byteCount;
        }
        while (enumerator.MoveNext());
      }
      return maxFilename;
    }

    private int getMaxGroupName(List<string> groups)
    {
      int maxGroupName = 0;
      List<string>.Enumerator enumerator = groups.GetEnumerator();
      if (enumerator.MoveNext())
      {
        do
        {
          string current = enumerator.Current;
          if (!string.IsNullOrEmpty(current))
          {
            int byteCount = Encoding.GetEncoding("Shift_JIS").GetByteCount(current);
            if (current[0] >= '0' && current[0] <= '9')
              ++byteCount;
            if (byteCount > maxGroupName)
              maxGroupName = byteCount;
          }
        }
        while (enumerator.MoveNext());
      }
      return maxGroupName;
    }

    private void writeHeader(StreamWriter wr, string cpk_fname)
    {
      FileInfo fileInfo = new FileInfo(cpk_fname);
      string str = "";
      switch (this.filemode)
      {
        case COutputHeader.EnumFilemode.Id:
          str = "ID Only";
          break;
        case COutputHeader.EnumFilemode.Filename:
          str = "Filename Only";
          break;
        case COutputHeader.EnumFilemode.FilenameGroup:
          str = "Filename + Group";
          break;
        case COutputHeader.EnumFilemode.IdGroup:
          str = "Id + Group";
          break;
        case COutputHeader.EnumFilemode.FilenameId:
          str = "Filename + Id";
          break;
        case COutputHeader.EnumFilemode.FilenameIdGroup:
          str = "filename + Id + Group";
          break;
      }
      wr.WriteLine("/*===========================================================================*");
      wr.WriteLine(" *\tContents file Information header");
      wr.WriteLine(" *\tCPK Filename  : " + cpk_fname);
      long length = fileInfo.Length;
      wr.WriteLine(" *\t    File Size : " + length.ToString("N0") + " bytes");
      DateTime lastWriteTime1 = fileInfo.LastWriteTime;
      DateTime lastWriteTime2 = fileInfo.LastWriteTime;
      wr.WriteLine(" *\t    Date Time : " + lastWriteTime2.ToShortDateString() + " " + lastWriteTime1.ToShortTimeString());
      wr.WriteLine(" *\t    File Mode : " + str);
      wr.WriteLine(" *===========================================================================*/");
      wr.WriteLine();
    }

    private string getUniqDefString(string cpk_fname) => this.makeDefinitionString(Path.GetFileNameWithoutExtension(cpk_fname)) + "_";

    private void writeSizeInfo(StreamWriter wr, string cpk_def_fname, [MarshalAs(UnmanagedType.U1)] bool uniq, CBinary bin)
    {
      int num1 = 0;
      int num2 = 0;
      string str1 = "";
      if (uniq)
        str1 = " (Unique)";
      wr.WriteLine("/* Information size of the CPK file" + str1 + " */");
      switch (this.filemode)
      {
        case COutputHeader.EnumFilemode.Id:
          string str2 = "(" + ((int) bin.ItocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_ITOC_INFO_SIZE", "         \t" + str2, uniq);
          num1 = (int) bin.ItocSize;
          num2 = ((int) bin.ItocSize + (int) sbyte.MaxValue) * 128 >>> 7;
          break;
        case COutputHeader.EnumFilemode.Filename:
          string str3 = "(" + ((int) bin.TocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_TOC_INFO_SIZE", "         \t" + str3, uniq);
          num1 = (int) bin.TocSize;
          num2 = ((int) bin.TocSize + (int) sbyte.MaxValue) * 128 >>> 7;
          break;
        case COutputHeader.EnumFilemode.FilenameGroup:
          string str4 = "(" + ((int) bin.TocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_TOC_INFO_SIZE", "         \t" + str4, uniq);
          string str5 = "(" + ((int) bin.GtocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_GTOC_INFO_SIZE", "        \t" + str5, uniq);
          num1 = (int) bin.TocSize + (int) bin.GtocSize;
          uint num3 = (uint) (((int) bin.TocSize + (int) sbyte.MaxValue) * 128 >>> 7);
          num2 = (((int) bin.GtocSize + (int) sbyte.MaxValue) * 128 >>> 7) + (int) num3;
          break;
        case COutputHeader.EnumFilemode.IdGroup:
        case COutputHeader.EnumFilemode.FilenameIdGroup:
          string str6 = "(" + ((int) bin.ItocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_ITOC_INFO_SIZE", "         \t" + str6, uniq);
          string str7 = "(" + ((int) bin.TocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_TOC_INFO_SIZE", "         \t" + str7, uniq);
          string str8 = "(" + ((int) bin.GtocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_GTOC_INFO_SIZE", "        \t" + str8, uniq);
          int num4 = (int) bin.TocSize + (int) bin.ItocSize;
          num1 = (int) bin.GtocSize + num4;
          int num5 = (((int) bin.ItocSize + (int) sbyte.MaxValue) * 128 >>> 7) + (((int) bin.TocSize + (int) sbyte.MaxValue) * 128 >>> 7);
          num2 = (((int) bin.GtocSize + (int) sbyte.MaxValue) * 128 >>> 7) + num5;
          break;
        case COutputHeader.EnumFilemode.FilenameId:
          string str9 = "(" + ((int) bin.ItocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_ITOC_INFO_SIZE", "         \t" + str9, uniq);
          string str10 = "(" + ((int) bin.TocSize).ToString() + ")";
          this.writeDefinition(wr, cpk_def_fname + "CPK_TOC_INFO_SIZE", "         \t" + str10, uniq);
          num1 = (int) bin.ItocSize + (int) bin.TocSize;
          uint num6 = (uint) (((int) bin.ItocSize + (int) sbyte.MaxValue) * 128 >>> 7);
          num2 = (((int) bin.TocSize + (int) sbyte.MaxValue) * 128 >>> 7) + (int) num6;
          break;
      }
      string str11 = "(" + num1.ToString() + ")";
      this.writeDefinition(wr, cpk_def_fname + "CPK_TOTAL_INFO_SIZE           ", "        \t" + str11, uniq);
      string str12 = "(" + num2.ToString() + ")";
      this.writeDefinition(wr, cpk_def_fname + "CPK_TOTAL_INFO_SIZE_ALIGNED   ", "\t" + str12, uniq);
      wr.WriteLine();
    }

    private string getUniqueDefString(string str, string defstr, [MarshalAs(UnmanagedType.U1)] bool forceReg)
    {
      string str1 = str.Trim();
      int length = str.Length;
      if (!this.IsRegistered(str1, (string) null))
      {
        this.m_hash.Add(str1, defstr);
        return str;
      }
      if (!forceReg && this.IsRegistered(str1, defstr))
        return (string) null;
      int num1 = 0;
      int num2 = 0;
      string str2 = "_" + num2.ToString();
      string str3 = str1 + str2;
      if (this.IsRegistered(str3, (string) null))
      {
        do
        {
          ++num1;
          num2 = num1;
          string str4 = "_" + num2.ToString();
          str3 = str1 + str4;
        }
        while (this.IsRegistered(str3, (string) null));
      }
      this.m_error_string_builder.Append("Warning : Found the redundant header definition \"");
      this.m_error_string_builder.Append(str1);
      this.m_error_string_builder.Append("\". Replace to \"");
      this.m_error_string_builder.Append(str3);
      this.m_error_string_builder.Append("\".\r\n");
      this.m_hash.Add(str3, (string) null);
      return str3.PadRight(length);
    }

    private void writeDefinition(StreamWriter wr, string defname, string content, [MarshalAs(UnmanagedType.U1)] bool uniq)
    {
      if (uniq)
      {
        defname = this.getUniqueDefString(defname.Trim(), (string) null, false);
      }
      else
      {
        defname = defname.Trim();
        if (!uniq)
          wr.WriteLine("#ifndef " + defname);
      }
      wr.WriteLine("#define " + defname + "\t" + content);
      if (uniq)
        return;
      wr.WriteLine("#endif");
    }

    private void writeContentInfo(
      StreamWriter wr,
      string cpk_fname,
      CFileData filedatas,
      string cpk_def_fname,
      [MarshalAs(UnmanagedType.U1)] bool uniq)
    {
      long count = (long) filedatas.Count;
      string str1 = "";
      if (uniq)
        str1 = " (Unique)";
      wr.WriteLine("/* CPK file information" + str1 + " */");
      string fileName = Path.GetFileName(cpk_fname);
      string str2 = "\"";
      string str3 = str2 + fileName + str2;
      this.writeDefinition(wr, cpk_def_fname + "CPK_FILENAME", "  \t" + str3, uniq);
      wr.WriteLine();
      wr.WriteLine("/* Number of contents" + str1 + " */");
      string str4 = "(" + ((int) count).ToString() + ")";
      this.writeDefinition(wr, cpk_def_fname + "NUM_CONTENS", "    \t" + str4, uniq);
      wr.WriteLine();
      switch (this.filemode)
      {
        case COutputHeader.EnumFilemode.FilenameGroup:
        case COutputHeader.EnumFilemode.IdGroup:
        case COutputHeader.EnumFilemode.FilenameIdGroup:
          this.writeContentInfoGroup(wr, filedatas, cpk_def_fname, uniq);
          break;
      }
    }

    private void writeContentInfoGroup(
      StreamWriter wr,
      CFileData filedatas,
      string cpk_def_fname,
      [MarshalAs(UnmanagedType.U1)] bool uniq)
    {
      string str1 = "";
      if (uniq)
        str1 = " (Unique)";
      wr.WriteLine("/* Number of groups" + str1 + " */");
      string str2 = "(" + filedatas.GetGroupStrings(false).Count.ToString() + ")";
      this.writeDefinition(wr, cpk_def_fname + "NUM_GROUPS", "    \t" + str2, uniq);
      wr.WriteLine();
      wr.WriteLine("/* Number of attributes" + str1 + " */");
      string str3 = "(" + filedatas.GetAttributeStrings().Count.ToString() + ")";
      this.writeDefinition(wr, cpk_def_fname + "NUM_ATTRIBUTES", "\t" + str3, uniq);
      wr.WriteLine();
    }

    private void writeContentsFiles(StreamWriter wr, CFileData filedatas, string unique)
    {
      int maxFilename = this.getMaxFilename(filedatas);
      wr.WriteLine("/* Content File definitions */");
      List<CFileInfo>.Enumerator enumerator = filedatas.FileInfos.GetEnumerator();
      if (!enumerator.MoveNext())
        return;
      do
      {
        string str = this.makeDefinitionStringFile(enumerator.Current, maxFilename, unique);
        if (str != (string) null)
          wr.WriteLine(str);
      }
      while (enumerator.MoveNext());
    }

    private void writeContentsGroupAttr(StreamWriter wr, CFileData filedatas, string unique)
    {
      List<string> groupStrings = filedatas.GetGroupStrings(false);
      if (groupStrings.Count > 0)
      {
        wr.WriteLine();
        wr.WriteLine("/* Groups definitions */");
        int maxGroupName = this.getMaxGroupName(groupStrings);
        List<string>.Enumerator enumerator = groupStrings.GetEnumerator();
        if (enumerator.MoveNext())
        {
          do
          {
            string str = this.makeDefinitionStringFile(enumerator.Current, maxGroupName, unique + "GROUP_");
            if (str != (string) null)
              wr.WriteLine(str);
          }
          while (enumerator.MoveNext());
        }
      }
      wr.WriteLine();
      wr.WriteLine("/* Attributes definitions */");
      List<string> attributeStrings = filedatas.GetAttributeStrings();
      int maxGroupName1 = this.getMaxGroupName(attributeStrings);
      string str1 = unique + "ATTRIBUTE_ALL";
      string str2 = "#ifndef " + str1 + "\r\n#define " + str1 + "\tNULL // All Attribute\r\n#endif";
      wr.WriteLine(str2);
      List<string>.Enumerator enumerator1 = attributeStrings.GetEnumerator();
      if (!enumerator1.MoveNext())
        return;
      do
      {
        string current = enumerator1.Current;
        string str3 = !(current == "(none)") ? this.makeDefinitionStringFile(current, maxGroupName1, unique + "ATTRIBUTE_") : this.makeDefinitionStringFile("", maxGroupName1, unique + "ATTRIBUTE_NONE") + " // (none)";
        wr.WriteLine(str3);
      }
      while (enumerator1.MoveNext());
    }

    private void writeFooter(StreamWriter wr)
    {
      wr.WriteLine("");
      wr.WriteLine("/* end of file */");
    }

    private void ExportHeaderIdSub(
      CFileData filedatas,
      string outputfname,
      string cpk_fname,
      CBinary bin)
    {
      StreamWriter wr = new StreamWriter(outputfname);
      if (this.DefineHeaderCsvOrder)
        filedatas.SortByCsvRegisteredId();
      try
      {
        if (wr == null)
          return;
        this.writeHeader(wr, cpk_fname);
        int maxFilenameId = this.getMaxFilenameId(filedatas);
        string uniqDefString = this.getUniqDefString(cpk_fname);
        wr.WriteLine("#ifndef CPK_DISABLE_COMMON_DEFINITION /////////////////////////////////////////\r\n");
        this.writeSizeInfo(wr, "", false, bin);
        this.writeSizeInfo(wr, uniqDefString, true, bin);
        this.writeContentInfo(wr, cpk_fname, filedatas, "", false);
        this.writeContentInfo(wr, cpk_fname, filedatas, uniqDefString, true);
        switch (this.filemode)
        {
          case COutputHeader.EnumFilemode.IdGroup:
          case COutputHeader.EnumFilemode.FilenameIdGroup:
            this.writeContentsGroupAttr(wr, filedatas, "");
            wr.WriteLine();
            break;
        }
        wr.WriteLine("/* Content ID definitions */");
        List<CFileInfo>.Enumerator enumerator1 = filedatas.FileInfos.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          string str = this.makeDefinitionStringId(enumerator1.Current, maxFilenameId, "");
          wr.WriteLine(str);
        }
        wr.WriteLine();
        wr.WriteLine("#endif // end of CPK_DISABLE_COMMON_DEFINITION\r\n");
        wr.WriteLine();
        wr.WriteLine("#ifndef CPK_DISABLE_UNIQUE_DEFINITION /////////////////////////////////////////\r\n");
        this.writeContentInfo(wr, cpk_fname, filedatas, uniqDefString, false);
        switch (this.filemode)
        {
          case COutputHeader.EnumFilemode.IdGroup:
          case COutputHeader.EnumFilemode.FilenameIdGroup:
            this.writeContentsGroupAttr(wr, filedatas, uniqDefString);
            wr.WriteLine();
            break;
        }
        wr.WriteLine("/* Content ID definitions */");
        string str1;
        if (string.IsNullOrEmpty(this.DefineHeaderUniqString))
        {
          str1 = uniqDefString;
        }
        else
        {
          COutputHeader coutputHeader = this;
          str1 = coutputHeader.getUniqDefString(coutputHeader.DefineHeaderUniqString);
        }
        string unique = str1;
        List<CFileInfo>.Enumerator enumerator2 = filedatas.FileInfos.GetEnumerator();
        while (enumerator2.MoveNext())
        {
          string str2 = this.makeDefinitionStringId(enumerator2.Current, maxFilenameId, unique);
          wr.WriteLine(str2);
        }
        wr.WriteLine();
        wr.WriteLine("#endif // end of CPK_DISABLPAE_UNIQUE_DEFINITION\r\n");
        this.writeFooter(wr);
        wr.Close();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        wr?.Dispose();
      }
    }

    private void ExportHeaderFileSub(
      CFileData filedatas,
      string outputfname,
      string cpk_fname,
      CBinary bin)
    {
      StreamWriter wr = new StreamWriter(outputfname);
      if (this.DefineHeaderCsvOrder)
        filedatas.SortByCsvRegisteredId();
      try
      {
        if (wr == null)
          return;
        this.writeHeader(wr, cpk_fname);
        string uniqDefString = this.getUniqDefString(cpk_fname);
        wr.WriteLine();
        wr.WriteLine("#ifndef CPK_DISABLE_COMMON_DEFINITION /////////////////////////////////////////\r\n");
        this.writeSizeInfo(wr, "", false, bin);
        this.writeSizeInfo(wr, uniqDefString, true, bin);
        this.writeContentInfo(wr, cpk_fname, filedatas, "", false);
        this.writeContentInfo(wr, cpk_fname, filedatas, uniqDefString, true);
        this.writeContentsFiles(wr, filedatas, "");
        this.writeContentsGroupAttr(wr, filedatas, "");
        wr.WriteLine();
        wr.WriteLine("#endif // end of CPK_DISABLE_COMMON_DEFINITION\r\n");
        wr.WriteLine();
        wr.WriteLine("#ifndef CPK_DISABLE_UNIQUE_DEFINITION /////////////////////////////////////////\r\n");
        this.ClearHash();
        this.writeSizeInfo(wr, uniqDefString, true, bin);
        this.writeContentInfo(wr, cpk_fname, filedatas, uniqDefString, true);
        string unique;
        if (string.IsNullOrEmpty(this.DefineHeaderUniqString))
        {
          unique = uniqDefString;
        }
        else
        {
          COutputHeader coutputHeader = this;
          unique = coutputHeader.getUniqDefString(coutputHeader.DefineHeaderUniqString);
        }
        this.writeContentsFiles(wr, filedatas, unique);
        this.writeContentsGroupAttr(wr, filedatas, uniqDefString);
        wr.WriteLine();
        wr.WriteLine("#endif // end of CPK_DISABLE_UNIQUE_DEFINITION\r\n");
        this.writeFooter(wr);
        wr.Close();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        wr?.Dispose();
      }
    }

    public void ExportHeaderId(
      CFileData filedatas,
      CBinary binary,
      string outputfname,
      string cpk_fname)
    {
      this.filemode = COutputHeader.EnumFilemode.Id;
      this.ExportHeaderIdSub(filedatas, outputfname, cpk_fname, binary);
    }

    public void ExportHeaderFilename(
      CFileData filedatas,
      CBinary binary,
      string outputfname,
      string cpk_fname)
    {
      this.filemode = COutputHeader.EnumFilemode.Filename;
      this.ExportHeaderFileSub(filedatas, outputfname, cpk_fname, binary);
    }

    public void ExportHeaderFilenameGroup(
      CFileData filedatas,
      CBinary binary,
      string outputfname,
      string cpk_fname)
    {
      this.filemode = COutputHeader.EnumFilemode.FilenameGroup;
      this.ExportHeaderFileSub(filedatas, outputfname, cpk_fname, binary);
    }

    public void ExportHeaderIdGroup(
      CFileData filedatas,
      CBinary binary,
      string outputfname,
      string cpk_fname)
    {
      this.filemode = COutputHeader.EnumFilemode.IdGroup;
      this.ExportHeaderIdSub(filedatas, outputfname, cpk_fname, binary);
    }

    public void ExportHeaderFilenameId(
      CFileData filedatas,
      CBinary binary,
      string outputfname,
      string cpk_fname)
    {
      this.filemode = COutputHeader.EnumFilemode.FilenameId;
      this.ExportHeaderIdSub(filedatas, outputfname, cpk_fname, binary);
    }

    public void ExportHeaderFilenameIdGroup(
      CFileData filedatas,
      CBinary binary,
      string outputfname,
      string cpk_fname)
    {
      this.filemode = COutputHeader.EnumFilemode.FilenameIdGroup;
      this.ExportHeaderIdSub(filedatas, outputfname, cpk_fname, binary);
    }

    private enum EnumFilemode
    {
      Id,
      Filename,
      FilenameGroup,
      IdGroup,
      FilenameId,
      FilenameIdGroup,
    }
  }
}
