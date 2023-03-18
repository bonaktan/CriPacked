// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerId
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System.Collections.Generic;

namespace CriCpkMaker
{
  public class FileDataComparerId : IComparer<CFileInfo>
  {
    public virtual int Compare(CFileInfo x, CFileInfo y)
    {
      uint fileId1 = x.FileId;
      uint fileId2 = y.FileId;
      if ((int) fileId1 == (int) fileId2)
        return 0;
      int num = -1;
      return fileId1 < fileId2 ? num : -num;
    }
  }
}
