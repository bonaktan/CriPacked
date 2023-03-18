// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByRegisteredId
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System.Collections.Generic;

namespace CriCpkMaker
{
  public class FileDataComparerByRegisteredId : IComparer<CFileInfo>
  {
    public virtual int Compare(CFileInfo x, CFileInfo y)
    {
      uint registeredId1 = x.RegisteredId;
      uint registeredId2 = y.RegisteredId;
      if ((int) registeredId1 == (int) registeredId2)
        return 0;
      int num = -1;
      return registeredId1 < registeredId2 ? num : -num;
    }
  }
}
