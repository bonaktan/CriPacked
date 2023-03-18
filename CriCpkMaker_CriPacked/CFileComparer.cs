// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CFileComparer
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CFileComparer
  {
    [return: MarshalAs(UnmanagedType.U1)]
    public static bool CompareFile(string filename1, string filename2)
    {
      FileStream fileStream1 = (FileStream) null;
      FileStream fileStream2 = (FileStream) null;
      FileInfo fileInfo1 = new FileInfo(filename1);
      FileInfo fileInfo2 = new FileInfo(filename2);
      bool flag = true;
      if (fileInfo1.Length != fileInfo2.Length)
        return false;
      try
      {
        fileStream1 = new FileStream(filename1, FileMode.Open, FileAccess.Read);
        fileStream2 = new FileStream(filename2, FileMode.Open, FileAccess.Read);
        int num1;
        int num2;
        do
        {
          num1 = fileStream1.ReadByte();
          num2 = fileStream2.ReadByte();
          if (num1 < 0)
            goto label_12;
        }
        while (num1 == num2);
        flag = false;
      }
      catch (Exception ex)
      {
        flag = false;
      }
      finally
      {
        fileStream1?.Close();
        fileStream2?.Close();
      }
label_12:
      return flag;
    }
  }
}
