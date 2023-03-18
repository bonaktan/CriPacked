// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CGInfo
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

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
