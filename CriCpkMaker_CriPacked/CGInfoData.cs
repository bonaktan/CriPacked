// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CGInfoData
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System.Collections;
using System.Collections.Generic;

namespace CriCpkMaker
{
  public class CGInfoData
  {
    private Hashtable m_gdics;
    private Hashtable m_adics;

    public int GroupAttributes => this.m_gdics.Count;

    public Hashtable SortedGroupAttributeList => this.m_gdics;

    public CGInfoData()
    {
      this.m_gdics = new Hashtable();
      this.m_adics = new Hashtable();
    }

    public void Clear()
    {
      this.m_gdics = new Hashtable();
      this.m_adics = new Hashtable();
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
      int sizeAligned = gdic.SizeAligned;
      if (sizeAligned != 0)
        return gdic.MaxAlign + sizeAligned;
      int size = 0;
      foreach (string key in (IEnumerable) this.m_adics.Keys)
      {
        string str = gaName + ":" + key;
        if (this.m_gdics.ContainsKey((object) str))
          size = this.GetSize(str) + size;
      }
      return size;
    }

    public int GetFiles(string gaName) => ((CGInfo) this.m_gdics[(object) gaName]).Files;

    private void addFile(string groupname, string attrname, int fsize, int align)
    {
      string key = (groupname + ":" + attrname).Trim();
      CGInfo cgInfo1;
      if (!this.m_gdics.ContainsKey((object) key))
      {
        cgInfo1 = new CGInfo();
        this.m_gdics.Add((object) key, (object) cgInfo1);
      }
      else
        cgInfo1 = (CGInfo) this.m_gdics[(object) key];
      cgInfo1.IncrementSize(fsize, align);
      ++cgInfo1.Files;
      CGInfo cgInfo2;
      if (!this.m_gdics.ContainsKey((object) groupname))
      {
        cgInfo2 = new CGInfo();
        this.m_gdics.Add((object) groupname, (object) cgInfo2);
      }
      else
        cgInfo2 = (CGInfo) this.m_gdics[(object) groupname];
      ++cgInfo2.Files;
      if (this.m_adics.ContainsKey((object) attrname))
        return;
      this.m_adics.Add((object) attrname, (object) null);
    }
  }
}
