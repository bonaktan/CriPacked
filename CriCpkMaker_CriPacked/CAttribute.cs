// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CAttribute
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System.Runtime.InteropServices;
using System.Text;

namespace CriCpkMaker
{
  public class CAttribute : CObjectLinkOrigin
  {
    private string attrName;
    private uint attrIndex;
    private CGroup parentGroup;
    private CFileInfoLink fileInfoLink;
    private int align;

    public string AttributeName => this.attrName;

    public CFileInfoLink FileInfoLink
    {
      get => this.fileInfoLink;
      set => this.fileInfoLink = value;
    }

    public int Alignment
    {
      get => this.align;
      set => this.align = value;
    }

    public uint AttrIndex
    {
      get => this.attrIndex;
      set => this.attrIndex = value;
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
          stringBuilder.Append(":");
        }
        stringBuilder.Append(this.attrName);
        return stringBuilder.ToString();
      }
    }

    public CGroup ParentGroup => this.parentGroup;

    public bool IsLastAttribute
    {
      [return: MarshalAs(UnmanagedType.U1)] get
      {
        CGroup parentGroup = this.parentGroup;
        return parentGroup.AttributeList[parentGroup.AttributeList.Count - 1] == this;
      }
    }

    public CAttribute(string attrName)
    {
      this.attrName = attrName;
      this.parentGroup = (CGroup) null;
    }

    public CAttribute(string attrName, CGroup parentGroup)
    {
      this.attrName = attrName;
      this.parentGroup = parentGroup;
    }

    public static CAttribute TryCreateAttribute(CGroup parentGroup, string attrName)
    {
      CAttribute attribute = CAttribute.SearchAttribute(parentGroup, attrName);
      if (attribute != null)
        return attribute;
      CAttribute attr = new CAttribute(attrName, parentGroup);
      parentGroup.AddAttribute(attr);
      return attr;
    }

    public static CAttribute SearchAttribute(CGroup parentGroup, string attrName)
    {
      if (parentGroup.AttributeList == null)
        return (CAttribute) null;
      int index = 0;
      if (0 < parentGroup.AttributeList.Count)
      {
        while (!(parentGroup.AttributeList[index].attrName == attrName))
        {
          ++index;
          if (index >= parentGroup.AttributeList.Count)
            goto label_6;
        }
        return parentGroup.AttributeList[index];
      }
label_6:
      return (CAttribute) null;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool IsSameName(string attrName) => this.attrName == attrName;

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"");
      stringBuilder.Append(this.attrName);
      stringBuilder.Append("\"");
      return stringBuilder.ToString();
    }
  }
}
