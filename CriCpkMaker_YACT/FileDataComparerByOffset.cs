// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByOffset
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System.Collections.Generic;

namespace CriCpkMaker
{
  public class FileDataComparerByOffset : IComparer<CFileInfo>
  {
    public virtual int Compare(CFileInfo x, CFileInfo y)
    {
      long offset1 = (long) x.Offset;
      long offset2 = (long) y.Offset;
      if (offset1 == offset2)
        return 0;
      return offset1 < offset2 ? -1 : 1;
    }
  }
}
