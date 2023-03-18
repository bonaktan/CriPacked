// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FilenameComparer
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System.Collections.Generic;

namespace CriCpkMaker
{
  public class FilenameComparer : IComparer<string>
  {
    public virtual unsafe int Compare(string x, string y)
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
