// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CGroupingManager
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CGroupingManager
  {
    private List<CGroup> groupList;
    private Hashtable attrTable;
    private long indexOrder;
    private int groupIndexCount;
    private int attrIndexCount;
    private int attrCount;
    private int flinkIndexCount;
    private uint groups;
    private uint rootLinks;
    private List<CGroup> delGroups;

    public List<CGroup> Groups => this.groupList;

    public Hashtable AttrInfo => this.attrTable;

    public CGroupingManager() => this.initialize();

    private void initialize()
    {
      this.groupList = new List<CGroup>();
      this.attrTable = new Hashtable();
      this.attrTable.Add((object) "", (object) new CAttrInfo(0, 0));
      this.rootLinks = 0U;
      this.attrCount = 1;
      CGroup cgroup = new CGroup("", (CGroup) null);
      cgroup.Child = -1;
      this.groupList.Add(cgroup);
    }

    public void ClearGroupList()
    {
      CGroup cgroup = new CGroup("", (CGroup) null);
      cgroup.Child = -1;
      this.groupList.Add(cgroup);
    }

    public void ClearGroup()
    {
      List<CGroup> groupList = this.groupList;
      if (groupList != null)
      {
        groupList.Clear();
        CGroup cgroup = new CGroup("", (CGroup) null);
        cgroup.Child = -1;
        this.groupList.Add(cgroup);
        this.rootLinks = 0U;
      }
      this.attrCount = 0;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool TryAddAttribute(string attrName, int alignment)
    {
      if (attrName == (string) null || alignment == 0)
        return false;
      if (attrName == "")
      {
        this.attrTable[(object) ""] = (object) new CAttrInfo(0, alignment);
        return true;
      }
      if (this.attrTable.ContainsKey((object) attrName))
        return false;
      this.attrTable.Add((object) attrName, (object) new CAttrInfo(this.attrCount, alignment));
      ++this.attrCount;
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool IsContainsAttr(string attrName) => this.attrTable.ContainsKey((object) attrName);

    private CAttrInfo GetAttrInfo(string attrName) => (CAttrInfo) this.attrTable[(object) attrName];

    [return: MarshalAs(UnmanagedType.U1)]
    public bool TryCreateGroupAndAttribute(
      CFileInfo finfo,
      string groupName,
      string attrName,
      [MarshalAs(UnmanagedType.U1)] bool sepaGroupFileInfo)
    {
      if (groupName == (string) null)
        return false;
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
      if (0 < strArray.Length)
      {
        do
        {
          if (sepaGroupFileInfo)
          {
            bool isClone = index2 != 0;
            CAttribute attribute = CAttribute.TryCreateAttribute(CGroup.TryCreateGroup(this.groupList, strArray[index2]), attrName);
            if (attribute.FileInfoLink == null)
            {
              CAttribute parentAttribute = attribute;
              parentAttribute.FileInfoLink = new CFileInfoLink(parentAttribute);
            }
            attribute.FileInfoLink.AddFileInfo(finfo, isClone);
          }
          else
          {
            CAttribute attribute = CAttribute.TryCreateAttribute(CGroup.TryCreateGroup(this.groupList, strArray[index2]), attrName);
            if (attribute.FileInfoLink == null)
            {
              CAttribute parentAttribute = attribute;
              parentAttribute.FileInfoLink = new CFileInfoLink(parentAttribute);
            }
            attribute.FileInfoLink.AddFileInfo(finfo, false);
          }
          ++index2;
        }
        while (index2 < strArray.Length);
      }
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool CreateGroupSub(CFileInfo finfo, string groupName, string attrName, [MarshalAs(UnmanagedType.U1)] bool isClone)
    {
      CAttribute attribute = CAttribute.TryCreateAttribute(CGroup.TryCreateGroup(this.groupList, groupName), attrName);
      if (attribute.FileInfoLink == null)
      {
        CAttribute parentAttribute = attribute;
        parentAttribute.FileInfoLink = new CFileInfoLink(parentAttribute);
      }
      attribute.FileInfoLink.AddFileInfo(finfo, isClone);
      return true;
    }

    public void DeleteFileInfoFromGroup(CFileInfo finf)
    {
      CGroupingManager cgroupingManager1 = this;
      cgroupingManager1.Enumerate(new CGroupingManager.EnumerateFunction(cgroupingManager1.DeleteFileInfoFromGroupSub), (object) finf);
      this.delGroups = new List<CGroup>();
      CGroupingManager cgroupingManager2 = this;
      cgroupingManager2.Enumerate(new CGroupingManager.EnumerateFunction(cgroupingManager2.DeleteBlankGroup), (object) finf);
      int index = 0;
      if (0 >= this.delGroups.Count)
        return;
      do
      {
        CGroup delGroup = this.delGroups[index];
        if (delGroup.ParentGroup == null)
          this.groupList.Remove(delGroup);
        else
          delGroup.ParentGroup.SubGroups.Remove(delGroup);
        ++index;
      }
      while (index < this.delGroups.Count);
    }

    private void DeleteGroup(CGroup group)
    {
      if (group.ParentGroup == null)
        this.groupList.Remove(group);
      else
        group.ParentGroup.SubGroups.Remove(group);
    }

    private void DeleteFileInfoFromGroupSub(object obj, object userObject)
    {
      CFileInfo cfileInfo = userObject as CFileInfo;
      if (!(obj is CFileInfoLink cfileInfoLink))
        return;
      List<CFileInfo> cfileInfoList = new List<CFileInfo>();
      int index1 = 0;
      if (0 < cfileInfoLink.FileListContinuous.Count)
      {
        do
        {
          if (cfileInfoLink.FileListContinuous[index1] == cfileInfo)
            cfileInfoList.Add(cfileInfoLink.FileListContinuous[index1]);
          ++index1;
        }
        while (index1 < cfileInfoLink.FileListContinuous.Count);
      }
      int num1 = 0;
      if (0 < cfileInfoList.Count)
      {
        do
        {
          cfileInfoLink.FileListContinuous.Remove(cfileInfoList[0]);
          ++num1;
        }
        while (num1 < cfileInfoList.Count);
      }
      cfileInfoList.Clear();
      int index2 = 0;
      if (0 < cfileInfoLink.FileListUncontinuous.Count)
      {
        do
        {
          if (cfileInfoLink.FileListUncontinuous[index2] == cfileInfo)
            cfileInfoList.Add(cfileInfoLink.FileListUncontinuous[index2]);
          ++index2;
        }
        while (index2 < cfileInfoLink.FileListUncontinuous.Count);
      }
      int num2 = 0;
      if (0 >= cfileInfoList.Count)
        return;
      do
      {
        cfileInfoLink.FileListUncontinuous.Remove(cfileInfoList[0]);
        ++num2;
      }
      while (num2 < cfileInfoList.Count);
    }

    private void DeleteBlankGroup(object obj, object userObject)
    {
      if (!(obj is CGroup group) || this.GetFiles(group) != 0 || !(group.GroupName != ""))
        return;
      this.delGroups.Add(group);
    }

    private int GetFiles(CAttribute attr) => attr.FileInfoLink != null ? attr.FileInfoLink.FileCount : 0;

    private int GetFiles(CGroup group)
    {
      if (group == null)
        return 0;
      if (group.HasSubGroups)
        return -1;
      if (group.AttributeList == null)
        return 0;
      int files = 0;
      int index = 0;
      if (0 < group.AttributeList.Count)
      {
        do
        {
          CAttribute attribute = group.AttributeList[index];
          files = (attribute.FileInfoLink == null ? 0 : attribute.FileInfoLink.FileCount) + files;
          ++index;
        }
        while (index < group.AttributeList.Count);
      }
      return files;
    }

    public void Enumerate(CGroupingManager.EnumerateFunction enumFunc, object userObject)
    {
      List<CGroup> groupList = this.groupList;
      int index1 = 0;
      if (0 >= groupList.Count)
        return;
      do
      {
        if (index1 == 52)
          Console.WriteLine(52.ToString());
        enumFunc((object) groupList[index1], userObject);
        CGroup cgroup = groupList[index1];
        if (cgroup.HasSubGroups)
        {
          int index2 = 0;
          if (0 < cgroup.SubGroups.Count)
          {
            do
            {
              enumFunc((object) cgroup.SubGroups[index2], userObject);
              this.EnumerateSub(cgroup.SubGroups[index2], enumFunc, userObject);
              ++index2;
            }
            while (index2 < cgroup.SubGroups.Count);
          }
        }
        if (cgroup.AttributeList != null)
        {
          int index3 = 0;
          if (0 < cgroup.AttributeList.Count)
          {
            do
            {
              CAttribute attribute = cgroup.AttributeList[index3];
              enumFunc((object) attribute, userObject);
              enumFunc((object) attribute.FileInfoLink, userObject);
              ++index3;
            }
            while (index3 < cgroup.AttributeList.Count);
          }
        }
        ++index1;
      }
      while (index1 < groupList.Count);
    }

    private void EnumerateSub(
      CGroup group,
      CGroupingManager.EnumerateFunction enumFunc,
      object userObject)
    {
      if (group.HasSubGroups)
      {
        int index = 0;
        if (0 < group.SubGroups.Count)
        {
          do
          {
            enumFunc((object) group.SubGroups[index], userObject);
            this.EnumerateSub(group.SubGroups[index], enumFunc, userObject);
            ++index;
          }
          while (index < group.SubGroups.Count);
        }
      }
      if (group.AttributeList == null)
        return;
      int index1 = 0;
      if (0 >= group.AttributeList.Count)
        return;
      do
      {
        CAttribute attribute = group.AttributeList[index1];
        enumFunc((object) attribute, userObject);
        enumFunc((object) attribute.FileInfoLink, userObject);
        ++index1;
      }
      while (index1 < group.AttributeList.Count);
    }

    public void UpdateFileInfoPackingOrder()
    {
      this.indexOrder = 0L;
      CGroupingManager cgroupingManager = this;
      cgroupingManager.Enumerate(new CGroupingManager.EnumerateFunction(cgroupingManager.UpdateFileInfoPackingOrderEnum), (object) false);
    }

    private void UpdateFileInfoPackingOrderEnum(object obj, object userObject)
    {
      if (!(obj is CFileInfoLink cfileInfoLink))
        return;
      // ISSUE: explicit reference operation
      ref bool local = @(bool) userObject;
      int index1 = 0;
      if (0 < cfileInfoLink.FileListContinuous.Count)
      {
        do
        {
          CFileInfo fileListContinuou = cfileInfoLink.FileListContinuous[index1];
          if (!fileListContinuou.UniTarget && !fileListContinuou.TryCompress)
          {
            fileListContinuou.PackingOrder = this.indexOrder;
            ++this.indexOrder;
          }
          ++index1;
        }
        while (index1 < cfileInfoLink.FileListContinuous.Count);
      }
      int index2 = 0;
      if (0 < cfileInfoLink.FileListContinuous.Count)
      {
        do
        {
          CFileInfo fileListContinuou = cfileInfoLink.FileListContinuous[index2];
          if (!fileListContinuou.UniTarget && fileListContinuou.TryCompress)
          {
            fileListContinuou.PackingOrder = this.indexOrder;
            ++this.indexOrder;
          }
          ++index2;
        }
        while (index2 < cfileInfoLink.FileListContinuous.Count);
      }
      int index3 = 0;
      if (0 >= cfileInfoLink.FileListContinuous.Count)
        return;
      do
      {
        CFileInfo fileListContinuou = cfileInfoLink.FileListContinuous[index3];
        if (fileListContinuou.UniTarget)
        {
          fileListContinuou.PackingOrder = this.indexOrder;
          ++this.indexOrder;
        }
        ++index3;
      }
      while (index3 < cfileInfoLink.FileListContinuous.Count);
    }

    public int SetObjectIndex()
    {
      this.groupIndexCount = 0;
      this.flinkIndexCount = 0;
      this.attrIndexCount = 0;
      CGroupingManager cgroupingManager1 = this;
      cgroupingManager1.SetGroupIndexCountSub(cgroupingManager1.groupList);
      CGroupingManager cgroupingManager2 = this;
      cgroupingManager2.SetGroupNextIndexSub(cgroupingManager2.groupList);
      return this.groupIndexCount;
    }

    public List<string> GetGroupStrings([MarshalAs(UnmanagedType.U1)] bool allGroups)
    {
      List<string> gpaths = new List<string>();
      CGroupingManager cgroupingManager = this;
      cgroupingManager.GetGroupStringsSub(cgroupingManager.groupList, gpaths, allGroups);
      return gpaths;
    }

    public void GetGroupStringsSub(List<CGroup> groups, List<string> gpaths, [MarshalAs(UnmanagedType.U1)] bool allGroups)
    {
      int index = 0;
      if (0 >= groups.Count)
        return;
      do
      {
        if (groups[index].HasSubGroups)
        {
          if (allGroups)
            gpaths.Add(groups[index].Path);
          this.GetGroupStringsSub(groups[index].SubGroups, gpaths, allGroups);
        }
        else if (groups[index].Path != "" && groups[index].Path != "(none)")
          gpaths.Add(groups[index].Path);
        ++index;
      }
      while (index < groups.Count);
    }

    public List<string> GetAttributeStrings()
    {
      List<string> attributeStrings = new List<string>();
      foreach (string key in (IEnumerable) this.attrTable.Keys)
        attributeStrings.Add(key);
      attributeStrings.Remove("");
      attributeStrings.Add("(none)");
      return attributeStrings;
    }

    private void SetGroupIndexCountSub(List<CGroup> groups)
    {
      if (groups == null || groups.Count <= 0)
        return;
      CGroup parentGroup = groups[0].ParentGroup;
      if (parentGroup != null)
      {
        parentGroup.Child = -this.groupIndexCount;
        parentGroup.Next = groups.Count;
      }
      int index = 0;
      if (0 >= groups.Count)
        return;
      do
      {
        CGroup group = groups[index];
        group.Index = (uint) this.groupIndexCount;
        ++this.groupIndexCount;
        if (group.HasAttribute)
          this.SetAttributeIndex(group.AttributeList);
        if (group.HasSubGroups)
          this.SetGroupIndexCountSub(group.SubGroups);
        ++index;
      }
      while (index < groups.Count);
    }

    private void SetAttributeIndex(List<CAttribute> attrs)
    {
      CGroup parentGroup = attrs[0].ParentGroup;
      if (parentGroup != null)
      {
        parentGroup.Child = this.attrIndexCount;
        parentGroup.Next = attrs.Count;
      }
      int index1 = 0;
      if (0 < attrs.Count)
      {
        CAttribute attr;
        do
        {
          attr = attrs[index1];
          attr.Index = (uint) this.attrIndexCount;
          CGroupingManager cgroupingManager = this;
          cgroupingManager.attrIndexCount = cgroupingManager.attrIndexCount + attr.FileInfoLink.FileCount + 1;
          attr.Child = this.flinkIndexCount;
          if (this.attrTable.ContainsKey((object) attr.AttributeName))
          {
            CAttrInfo cattrInfo = (CAttrInfo) this.attrTable[(object) attr.AttributeName];
            attr.AttrIndex = (uint) cattrInfo.Index;
            CFileInfoLink fileInfoLink = attr.FileInfoLink;
            if (fileInfoLink.FileListContinuous.Count > 0)
              attr.Next = fileInfoLink.FileListUncontinuous.Count + 1;
            int index2 = 0;
            if (0 < fileInfoLink.FileListContinuous.Count)
            {
              do
              {
                CFileInfo fileListContinuou = fileInfoLink.FileListContinuous[index2];
                ++index2;
              }
              while (index2 < fileInfoLink.FileListContinuous.Count);
            }
            if (fileInfoLink.FileListContinuous.Count > 0)
              ++this.flinkIndexCount;
            int index3 = 0;
            if (0 < fileInfoLink.FileListUncontinuous.Count)
            {
              do
              {
                CFileInfo fileListUncontinuou = fileInfoLink.FileListUncontinuous[index3];
                ++this.flinkIndexCount;
                ++index3;
              }
              while (index3 < fileInfoLink.FileListUncontinuous.Count);
            }
            ++index1;
          }
          else
            goto label_14;
        }
        while (index1 < attrs.Count);
        return;
label_14:
        throw new Exception("Unknown Attribute : " + attr.AttributeName);
      }
    }

    public void SetGroupNextIndexSub(List<CGroup> groupList)
    {
      if (groupList == null)
        return;
      int count = groupList.Count;
      int index1 = 0;
      if (0 < count)
      {
        do
        {
          CGroup group = groupList[index1];
          if (index1 + 1 == count)
          {
            if (group.ParentGroup != null)
            {
              CGroup cgroup = group;
              cgroup.Next = -(int) cgroup.ParentGroup.Index;
            }
            else
              group.Next = 0;
          }
          else
          {
            group.Next = (int) groupList[index1 + 1].Index;
            if (group.Index == 0U)
              group.Next = 0;
          }
          ++index1;
        }
        while (index1 < count);
      }
      int index2 = 0;
      if (0 >= count)
        return;
      do
      {
        CGroup group = groupList[index2];
        if (group.HasSubGroups)
          this.SetGroupNextIndexSub(group.SubGroups);
        ++index2;
      }
      while (index2 < count);
    }

    public void DebugPrintGroupInfo()
    {
    }

    public void DebugPrintGroupInfoSub(object obj, object userObject)
    {
    }

    public void DebugPrintAttributeInfo()
    {
      int num1 = 0;
      foreach (string key in (IEnumerable) this.attrTable.Keys)
      {
        CAttrInfo cattrInfo = this.attrTable[(object) key] as CAttrInfo;
        object[] objArray = new object[4];
        int num2 = num1;
        ++num1;
        objArray[0] = (object) num2;
        string str = "\"";
        objArray[1] = (object) (str + key + str);
        objArray[2] = (object) cattrInfo.Index;
        objArray[3] = (object) cattrInfo.Alignment;
        Debug.WriteLine("[{0,4}] {1,-14} : Index={2, -4} Align={3,-4}", objArray);
      }
    }

    public delegate void EnumerateFunction(object obj, object userObject);
  }
}
