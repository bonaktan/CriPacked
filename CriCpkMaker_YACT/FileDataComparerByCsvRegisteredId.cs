// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByCsvRegisteredId
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System.Collections.Generic;

namespace CriCpkMaker
{
  public class FileDataComparerByCsvRegisteredId : IComparer<CFileInfo>
  {
    public virtual int Compare(CFileInfo x, CFileInfo y)
    {
      long csvRegisteredId1 = x.CsvRegisteredId;
      long csvRegisteredId2 = y.CsvRegisteredId;
      if (csvRegisteredId1 == csvRegisteredId2)
        return 0;
      return csvRegisteredId1 < csvRegisteredId2 ? -1 : 1;
    }
  }
}
