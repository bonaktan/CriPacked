// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByOffset
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

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
