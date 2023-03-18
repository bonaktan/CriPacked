// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByOrder
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System.Collections.Generic;

namespace CriCpkMaker
{
  public class FileDataComparerByOrder : IComparer<CFileInfo>
  {
    public virtual int Compare(CFileInfo x, CFileInfo y)
    {
      long packingOrder1 = x.PackingOrder;
      long packingOrder2 = y.PackingOrder;
      if (packingOrder1 == packingOrder2)
        return 0;
      return packingOrder1 < packingOrder2 ? -1 : 1;
    }
  }
}
