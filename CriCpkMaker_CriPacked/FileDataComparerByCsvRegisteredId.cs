// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByCsvRegisteredId
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

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
