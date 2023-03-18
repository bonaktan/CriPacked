// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByInfoIndex
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

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
