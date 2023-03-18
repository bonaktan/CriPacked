// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerIdName
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System.Collections.Generic;

namespace CriCpkMaker
{
  public class FileDataComparerIdName : IComparer<CFileInfo>
  {
    public virtual unsafe int Compare(CFileInfo x, CFileInfo y)
    {
      uint fileId1 = y.FileId;
      uint fileId2 = x.FileId;
      string instr = fileId1.ToString();
      sbyte* numPtr1 = Utility.AllocCharString(fileId2.ToString());
      sbyte* numPtr2 = Utility.AllocCharString(instr);
      int num = \u003CModule\u003E.CriCpkMaker\u002E\u003FA0x24cabba6\u002EstriCompare(numPtr1, numPtr2);
      Utility.FreeCharString(numPtr2);
      Utility.FreeCharString(numPtr1);
      return num;
    }

    public static unsafe int CompareFileInfoString(string x, string y)
    {
      sbyte* numPtr1 = Utility.AllocCharString(x);
      sbyte* numPtr2 = Utility.AllocCharString(y);
      int num = \u003CModule\u003E.CriCpkMaker\u002E\u003FA0x24cabba6\u002EstriCompare(numPtr1, numPtr2);
      Utility.FreeCharString(numPtr2);
      Utility.FreeCharString(numPtr1);
      return num;
    }
  }
}
