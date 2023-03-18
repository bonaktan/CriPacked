// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerId
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

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
