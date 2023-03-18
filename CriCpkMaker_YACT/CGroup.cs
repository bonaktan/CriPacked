// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CGroup
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CriCpkMaker
{
  public class CGroup : CObjectLinkOrigin
  {
    private string groupName;
    private List<CGroup> groupList;
    private List<CAttribute> attrList;
    private CGroup parentGroup;
    private bool taleGroup;

    public string GroupName
    {
      get => this.groupName;
      set => this.groupName = value;
    }

    public string Path
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        CGroup parentGroup = this.parentGroup;
        if (parentGroup != null)
        {
          stringBuilder.Append(parentGroup.Path);
          stringBuilder.Append("/");
        }
        stringBuilder.Append(this.groupName);
        return stringBuilder.ToString();
      }
    }

    public List<CGroup> SubGroups => this.groupList;

    public int SubGroupsCount
    {
      get
      {
        List<CGroup> groupList = this.groupList;
        return groupList == null ? 0 : groupList.Count;
      }
    }

    public List<CAttribute> AttributeList => this.attrList;

    public CGroup ParentGroup
    {
      get => this.parentGroup;
      set => this.parentGroup = value;
    }

    public bool HasSubGroups
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.groupList != null;
    }

    public bool HasAttribute
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.attrList != null;
    }

    public bool TaleGroup
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.taleGroup;
      [param: MarshalAs(UnmanagedType.U1)] set => this.taleGroup = value;
    }

    public CGroup(string name, CGroup parentGroup)
    {
      this.groupName = name;
      this.parentGroup = parentGroup;
      this.Next = 0;
      this.Child = 0;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool CreateSubGroup()
    {
      if (this.groupList != null)
        return false;
      this.groupList = new List<CGroup>();
      return true;
    }

    public static CGroup TryCreateGroup(List<CGroup> groupList, string groupName)
    {
      if (groupName.Length > 1 && groupName[0] == '/')
        groupName = groupName.Substring(1);
      CGroup group = CGroup.SearchGroup(groupList, groupName, 0);
      if (group != null)
        return group;
      List<CGroup> cgroupList = groupList;
      CGroup parentGroup = (CGroup) null;
      char[] chArray = new char[1]{ '/' };
      string[] strArray = groupName.Split(chArray);
      int index1 = 0;
      if (0 < strArray.Length)
      {
        do
        {
          int index2 = 0;
          if (0 < cgroupList.Count)
          {
            while (!(cgroupList[index2].groupName == strArray[index1]))
            {
              ++index2;
              if (index2 >= cgroupList.Count)
                goto label_9;
            }
            parentGroup = cgroupList[index2];
            goto label_10;
          }
label_9:
          parentGroup = new CGroup(strArray[index1], parentGroup);
          cgroupList.Add(parentGroup);
label_10:
          if (index1 != strArray.Length - 1)
          {
            if (parentGroup.groupList == null)
              parentGroup.groupList = new List<CGroup>();
            cgroupList = parentGroup.groupList;
            ++index1;
          }
          else
            break;
        }
        while (index1 < strArray.Length);
      }
      parentGroup.taleGroup = true;
      return parentGroup;
    }

    public static CGroup SearchGroup(List<CGroup> curGroups, string groupName, int level)
    {
      if (curGroups != null)
      {
label_1:
        while (curGroups.Count != 0)
        {
          char[] chArray = new char[1]{ '/' };
          string[] strArray = groupName.Split(chArray);
          for (int index = 0; index < curGroups.Count; ++index)
          {
            CGroup curGroup = curGroups[index];
            if (curGroup.groupName == strArray[level])
            {
              int num = level + 1;
              if (strArray.Length == num)
                return curGroup;
              level = num;
              curGroups = curGroup.groupList;
              if (curGroups != null)
                goto label_1;
              else
                goto label_8;
            }
          }
          return (CGroup) null;
        }
        return (CGroup) null;
      }
label_8:
      return (CGroup) null;
    }

    public void AddAttribute(CAttribute attr)
    {
      if (this.attrList == null)
        this.attrList = new List<CAttribute>();
      this.attrList.Add(attr);
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"");
      stringBuilder.Append(this.groupName);
      stringBuilder.Append("\"\t");
      stringBuilder.Append(" { Index=");
      uint index1 = this.Index;
      stringBuilder.Append(index1.ToString());
      stringBuilder.Append(", Child=");
      int child = this.Child;
      stringBuilder.Append(child.ToString());
      stringBuilder.Append(", Path=\"");
      stringBuilder.Append(this.Path);
      stringBuilder.Append("\", ");
      if (this.parentGroup == null)
      {
        stringBuilder.Append("Parent=null, ");
      }
      else
      {
        stringBuilder.Append("Parent=\"");
        stringBuilder.Append(this.parentGroup.groupName);
        stringBuilder.Append("\", ");
      }
      if (this.groupList == null)
      {
        stringBuilder.Append("SubGroup=null ");
      }
      else
      {
        stringBuilder.Append("SubGroup=");
        int index2 = 0;
        if (0 < this.groupList.Count)
        {
          List<CGroup> groupList1;
          do
          {
            stringBuilder.Append("\"");
            List<CGroup> groupList2 = this.groupList;
            stringBuilder.Append(groupList2[index2].ToString());
            stringBuilder.Append("\" ");
            ++index2;
            groupList1 = this.groupList;
          }
          while (index2 < groupList1.Count);
        }
      }
      if (this.attrList == null)
        stringBuilder.Append("AttributeList=null");
      else
        stringBuilder.Append("AttributeList=nullじゃない");
      stringBuilder.Append(" }");
      return stringBuilder.ToString();
    }
  }
}
