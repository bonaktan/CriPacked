// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CObjectLinkOrigin
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

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
