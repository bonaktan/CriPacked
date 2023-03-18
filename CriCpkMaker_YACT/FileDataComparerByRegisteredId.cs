// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerByRegisteredId
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

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
