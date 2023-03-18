// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.FileInfoLastWriteTimeComparer
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System.Collections.Generic;
using System.IO;

namespace CriCpkMaker
{
  public class FileInfoLastWriteTimeComparer : IComparer<FileInfo>
  {
    public virtual int Compare(FileInfo x, FileInfo y)
    {
      long ticks1 = x.LastWriteTime.Ticks;
      long ticks2 = y.LastWriteTime.Ticks;
      if (ticks1 == ticks2)
        return 0;
      return ticks1 < ticks2 ? -1 : 1;
    }
  }
}
