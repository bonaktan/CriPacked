// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.Utility
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace CriCpkMaker
{
  public class Utility
  {
    public static unsafe sbyte* AllocCharString(string instr) => (sbyte*) Marshal.StringToHGlobalAnsi(instr).ToPointer();

    public static unsafe void FreeCharString(sbyte* str) => Marshal.FreeHGlobal(new IntPtr((void*) str));

    [return: MarshalAs(UnmanagedType.U1)]
    public static bool DeleteFile(string fpath)
    {
      if (File.Exists(fpath))
      {
        try
        {
          File.Delete(fpath);
        }
        catch (Exception ex)
        {
          return false;
        }
      }
      return true;
    }

    public static DateTime ConvToDateTime(ulong timevalue)
    {
      if (timevalue == 0UL)
        return new DateTime(1990, 1, 1, 0, 0, 0);
      try
      {
        uint num1 = (uint) (timevalue >> 32);
        uint year = num1 >> 16;
        uint month = num1 >> 8 & (uint) byte.MaxValue;
        uint day = num1 & (uint) byte.MaxValue;
        uint num2 = (uint) timevalue;
        uint hour = num2 >> 24;
        uint minute = num2 >> 16 & (uint) byte.MaxValue;
        uint second = num2 >> 8 & (uint) byte.MaxValue;
        return new DateTime((int) year, (int) month, (int) day, (int) hour, (int) minute, (int) second);
      }
      catch (Exception ex)
      {
        return new DateTime(1990, 1, 1, 0, 0, 0);
      }
    }

    public static string GetModulePath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

    public static string GetMd5Hash(string fname)
    {
      byte[] numArray;
      if (!File.Exists(fname))
      {
        numArray = (byte[]) null;
      }
      else
      {
        FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
        byte[] md5Bytes = Utility.GetMd5Bytes(fs, 32768);
        fs.Close();
        numArray = md5Bytes;
      }
      return numArray == null ? (string) null : BitConverter.ToString(numArray).ToLower().Replace("-", "");
    }

    public static byte[] GetMd5HashBytes(string fname)
    {
      if (!File.Exists(fname))
        return (byte[]) null;
      FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
      byte[] md5Bytes = Utility.GetMd5Bytes(fs, 32768);
      fs.Close();
      return md5Bytes;
    }

    public static byte[] GetMd5Bytes(FileStream fs, int intbufsize)
    {
      MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
      long length = fs.Length;
      byte[] numArray = new byte[intbufsize];
      if (fs.Position < length)
      {
        do
        {
          int inputCount = fs.Read(numArray, 0, numArray.Length);
          if (fs.Position < length)
            cryptoServiceProvider.TransformBlock(numArray, 0, inputCount, (byte[]) null, 0);
          else
            cryptoServiceProvider.TransformFinalBlock(numArray, 0, inputCount);
        }
        while (fs.Position < length);
      }
      return cryptoServiceProvider.Hash;
    }

    public static byte[] GetMd5Bytes(FileStream fs) => Utility.GetMd5Bytes(fs, 32768);
  }
}
