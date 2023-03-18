// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CCompFileCache
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using \u003CCppImplementationDetails\u003E;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CCompFileCache : IDisposable
  {
    private string cacheWorkPath;
    private long cacheLimitSize;

    private void \u007ECCompFileCache()
    {
    }

    public string GetCacheDirectory() => this.cacheWorkPath;

    [return: MarshalAs(UnmanagedType.U1)]
    public bool SetCacheDirectory(string cachedir)
    {
      bool flag1 = false;
      if (cachedir.Length > 200)
        throw new ApplicationException("Too long path name for the compress file cache (over 200). \"" + cachedir + "\"");
      if (string.IsNullOrEmpty(cachedir))
      {
        this.cacheWorkPath = "";
        return true;
      }
      string str = Path.Combine(Utility.GetModulePath(), cachedir);
      bool flag2;
      if (!Directory.Exists(str))
      {
        try
        {
          Directory.CreateDirectory(str);
        }
        catch (Exception ex)
        {
          throw new ApplicationException("Fail to CreateDirectory() for the compress file cache. \"" + str + "\"");
        }
        flag2 = true;
      }
      else if (!File.Exists(Path.Combine(str, "comp_file_cache_format_ver.005")))
      {
        flag1 = true;
        flag2 = true;
      }
      else
        flag2 = false;
      this.cacheWorkPath = str;
      if (flag1)
        this.ClearCacheFiles();
      if (flag2)
      {
        try
        {
          File.Create(Path.Combine(str, "comp_file_cache_format_ver.005"))?.Close();
        }
        catch (Exception ex)
        {
        }
      }
      return true;
    }

    public long GetCacheLimitSize() => this.cacheLimitSize;

    public void SetCacheLimitSize(long limitsize) => this.cacheLimitSize = limitsize;

    [return: MarshalAs(UnmanagedType.U1)]
    public bool ClearCacheFiles()
    {
      if (string.IsNullOrEmpty(this.cacheWorkPath))
        return true;
      try
      {
        foreach (string file in Directory.GetFiles(this.cacheWorkPath))
        {
          try
          {
            File.Delete(file);
          }
          catch (Exception ex)
          {
          }
        }
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    public long ClearCacheFilesLimit() => -1;

    private unsafe string MakeHashString(void* inbuf, ulong insize, string postfix)
    {
      \u0024ArrayType\u0024\u0024\u0024BY0CB\u0040D arrayTypeBy0CbD;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy0CbD + 32) = (sbyte) 0;
      \u003CModule\u003E.MD5Calc(inbuf, (uint) insize, (sbyte*) &arrayTypeBy0CbD, 33U);
      return new string((sbyte*) &arrayTypeBy0CbD) + "." + postfix;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool GetCacheFile(
      void* inbuf,
      ulong insize,
      string postfix,
      out string cachefpath)
    {
      if (insize == 0UL || string.IsNullOrEmpty(this.cacheWorkPath))
        return false;
      \u0024ArrayType\u0024\u0024\u0024BY0CB\u0040D arrayTypeBy0CbD;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy0CbD + 32) = (sbyte) 0;
      \u003CModule\u003E.MD5Calc(inbuf, (uint) insize, (sbyte*) &arrayTypeBy0CbD, 33U);
      string path = Path.Combine(this.cacheWorkPath, new string((sbyte*) &arrayTypeBy0CbD) + "." + postfix);
      cachefpath = path;
      bool cacheFile = File.Exists(path);
      if (cacheFile)
      {
        DateTime now = DateTime.Now;
        File.SetLastWriteTime(cachefpath, now);
      }
      return cacheFile;
    }

    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
        return;
      // ISSUE: explicit finalizer call
      this.Finalize();
    }

    public virtual void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
  }
}
