// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileDataComparerIdName
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

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
      int num = \u003CModule\u003E.CriCpkMaker\u002E\u003FA0x99d4000a\u002EstriCompare(numPtr1, numPtr2);
      Utility.FreeCharString(numPtr2);
      Utility.FreeCharString(numPtr1);
      return num;
    }

    public static unsafe int CompareFileInfoString(string x, string y)
    {
      sbyte* numPtr1 = Utility.AllocCharString(x);
      sbyte* numPtr2 = Utility.AllocCharString(y);
      int num = \u003CModule\u003E.CriCpkMaker\u002E\u003FA0x99d4000a\u002EstriCompare(numPtr1, numPtr2);
      Utility.FreeCharString(numPtr2);
      Utility.FreeCharString(numPtr1);
      return num;
    }
  }
}
