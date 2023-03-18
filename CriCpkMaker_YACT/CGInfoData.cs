// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CGInfoData
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System.Collections;
using System.Collections.Generic;

namespace CriCpkMaker
{
  public class CGInfoData
  {
    private string m_pre_group;
    private string m_pre_attr;
    private Hashtable m_gdics;
    private int m_file_size_aligned;
    private int m_files;

    public int GroupAttributes => this.m_gdics.Count;

    public Hashtable SortedGroupAttributeList => this.m_gdics;

    public CGInfoData()
    {
      this.m_file_size_aligned = 0;
      this.m_files = 0;
      this.m_gdics = new Hashtable();
      this.m_pre_group = (string) null;
      this.m_pre_attr = (string) null;
    }

    public void Clear()
    {
      this.m_file_size_aligned = 0;
      this.m_files = 0;
      this.m_gdics = new Hashtable();
      this.m_pre_group = (string) null;
      this.m_pre_attr = (string) null;
    }

    public void AddFile(CFileInfo finf)
    {
      string groupString = finf.GroupString;
      string attributeString = finf.AttributeString;
      int extractsize = (int) finf.Extractsize;
      int dataAlign = (int) finf.DataAlign;
      string[] strArray1 = new string[1]{ groupString };
      string[] strArray2;
      if (groupString.IndexOf(',') < 0)
      {
        strArray2 = strArray1;
      }
      else
      {
        char[] chArray = new char[1]{ ',' };
        strArray2 = groupString.Split(chArray);
      }
      int index = 0;
      if (0 >= strArray2.Length)
        return;
      do
      {
        string str = strArray2[index];
        char[] chArray = new char[1]{ '/' };
        this.addFile(str.Trim().Trim(chArray), attributeString, extractsize, dataAlign);
        ++index;
      }
      while (index < strArray2.Length);
    }

    public List<string> GetSortedList()
    {
      List<string> sortedList = new List<string>();
      foreach (string key in (IEnumerable) this.m_gdics.Keys)
        sortedList.Add(key);
      sortedList.Sort((IComparer<string>) new FilenameComparer());
      return sortedList;
    }

    public int GetSize(string gaName)
    {
      CGInfo gdic = (CGInfo) this.m_gdics[(object) gaName];
      return gdic.MaxAlign + gdic.SizeAligned;
    }

    public int GetFiles(string gaName) => ((CGInfo) this.m_gdics[(object) gaName]).Files;

    private void addFile(string groupname, string attrname, int fsize, int align)
    {
      string key = (groupname + ":" + attrname).Trim();
      CGInfo cgInfo;
      if (!this.m_gdics.ContainsKey((object) key))
      {
        cgInfo = new CGInfo();
        this.m_gdics.Add((object) key, (object) cgInfo);
      }
      else
        cgInfo = (CGInfo) this.m_gdics[(object) key];
      cgInfo.IncrementSize(fsize, align);
      ++cgInfo.Files;
      this.m_pre_group = groupname;
      this.m_pre_attr = attrname;
    }
  }
}
