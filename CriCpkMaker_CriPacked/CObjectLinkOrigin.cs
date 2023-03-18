// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CObjectLinkOrigin
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

namespace CriCpkMaker
{
  public class CObjectLinkOrigin
  {
    private uint groupIndex;
    private int next;
    private int child;
    private uint index;

    public int Next
    {
      get => this.next;
      set => this.next = value;
    }

    public int Child
    {
      get => this.child;
      set => this.child = value;
    }

    public uint Index
    {
      get => this.index;
      set => this.index = value;
    }
  }
}
