// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByInfoIndex
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System.Collections.Generic;

namespace CriCpkMaker
{
  public class FileDataComparerByInfoIndex : IComparer<CFileInfo>
  {
    public virtual int Compare(CFileInfo x, CFileInfo y)
    {
      long infoIndex1 = x.InfoIndex;
      long infoIndex2 = y.InfoIndex;
      if (infoIndex1 == infoIndex2)
        return 0;
      return infoIndex1 < infoIndex2 ? -1 : 1;
    }
  }
}
