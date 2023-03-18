// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CFileInfoLink
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CFileInfoLink
  {
    private List<CFileInfo> m_filelist_chunk;
    private List<CFileInfo> m_filelist_refference;
    private CAttribute m_attribute;
    private uint groupIndex;
    private int next;
    private int child;
    private uint index;

    public int Next
    {
      get => this.next;
      set => this.next = value;
    }

    public int Child
    {
      get => this.child;
      set => this.child = value;
    }

    public List<CFileInfo> FileListContinuous => this.m_filelist_chunk;

    public List<CFileInfo> FileListUncontinuous => this.m_filelist_refference;

    public int FileCount
    {
      get
      {
        int fileCount = 0;
        List<CFileInfo> filelistChunk = this.m_filelist_chunk;
        if (filelistChunk != null)
          fileCount = filelistChunk.Count;
        List<CFileInfo> filelistRefference = this.m_filelist_refference;
        if (filelistRefference != null)
          fileCount = filelistRefference.Count + fileCount;
        return fileCount;
      }
    }

    public CAttribute ParentAttribute => this.m_attribute;

    public int ContinuousFiles => this.m_filelist_chunk.Count;

    public int UncontinuousFiles => this.m_filelist_refference.Count;

    public CFileInfoLink(CAttribute parentAttribute)
    {
      this.m_filelist_chunk = new List<CFileInfo>();
      this.m_filelist_refference = new List<CFileInfo>();
      this.m_attribute = parentAttribute;
    }

    public void AddFileInfo(CFileInfo finf, [MarshalAs(UnmanagedType.U1)] bool isClone)
    {
      if (!isClone)
        this.m_filelist_chunk.Add(finf);
      else
        this.m_filelist_refference.Add(finf);
    }

    public int GetId() => 1;
  }
}
