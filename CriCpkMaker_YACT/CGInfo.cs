// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CGInfo
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

namespace CriCpkMaker
{
  public class CGInfo
  {
    public int SizeAligned;
    public int Files;
    public int MaxAlign;

    public void IncrementSize(int size, int align)
    {
      this.SizeAligned = (align + this.SizeAligned - 1) / align * align + size;
      int maxAlign = this.MaxAlign;
      this.MaxAlign = maxAlign >= align ? maxAlign : align;
    }
  }
}
