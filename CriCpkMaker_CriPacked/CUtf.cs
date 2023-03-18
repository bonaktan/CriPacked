// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CUtf
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CUtf : IDisposable
  {
    private unsafe _criheap_struct* m_heap;
    private unsafe void* m_heap_memptr;
    private unsafe CriUtfMaker* m_utf_header;
    private unsafe CriUtfMaker* m_utf_header_crc_table;
    private unsafe CriUtfMaker* m_utf_finf;
    private unsafe CriUtfMaker* m_utf_htoc;
    private unsafe CriUtfMaker* m_utf_efinf;
    private unsafe CriUtfMaker* m_utf_id;
    private unsafe CriUtfMaker* m_utf_id_l;
    private unsafe CriUtfMaker* m_utf_id_h;
    private unsafe CriUtfMaker* m_utf_eid;
    private unsafe CriUtfMaker* m_utf_ginf;
    private unsafe CriUtfMaker* m_utf_ginf_glink;
    private unsafe CriUtfMaker* m_utf_ginf_flink;
    private unsafe CriUtfMaker* m_utf_ginf_attr;
    private unsafe CriUtfMaker* m_utf_ginf_ginf;
    private unsafe CriUtfMaker* m_utf_hgtoc;
    private unsafe CriUtfMaker* m_utf_hgtoc_gfpath;
    private unsafe CriUtfMaker* m_utf_hgtoc_gfid;
    private unsafe CriUtfBinary* m_bin_header;
    private unsafe CriUtfBinary* m_bin_header_crc_table;
    private unsafe CriUtfBinary* m_bin_finf;
    private unsafe CriUtfBinary* m_bin_htoc;
    private unsafe CriUtfBinary* m_bin_efinf;
    private unsafe CriUtfBinary* m_bin_id;
    private unsafe CriUtfBinary* m_bin_id_l;
    private unsafe CriUtfBinary* m_bin_id_h;
    private unsafe CriUtfBinary* m_bin_eid;
    private unsafe CriUtfBinary* m_bin_gtoc;
    private unsafe CriUtfBinary* m_bin_glink;
    private unsafe CriUtfBinary* m_bin_flink;
    private unsafe CriUtfBinary* m_bin_attr;
    private unsafe CriUtfBinary* m_bin_ginf;
    private unsafe CriUtfBinary* m_bin_hgtoc;
    private unsafe CriUtfBinary* m_bin_hgtoc_gfpath;
    private unsafe CriUtfBinary* m_bin_hgtoc_gfid;
    private CUtfIff m_iff_header;
    private CUtfIff m_iff_toc;
    private CUtfIff m_iff_htoc;
    private CUtfIff m_iff_etoc;
    private CUtfIff m_iff_itoc;
    private CUtfIff m_iff_gtoc;
    private CUtfIff m_iff_hgtoc;
    private bool m_enable_datetime;
    private CUtf.EnumCrcMode m_crc_mode;
    private bool m_enable_toc_crc;
    private bool m_enable_file_crc;
    private bool m_enable_implant_attribute_name;
    private uint m_num_file;
    private uint m_num_htoc_tables;
    private uint m_num_id_l;
    private uint m_num_id_h;
    private uint m_num_eid;
    private uint m_data_align;
    private ulong m_total_packed_size;
    private ulong m_total_contents_file_size;
    private uint m_groups;
    private uint m_attrs;
    private int m_flinks;
    private uint m_num_hgtoc_tables;
    private bool m_enable_hgtoc_gfpath;
    private bool m_enable_hgtoc_gfid;
    private uint m_updates;
    private ulong m_cpk_datetime;
    private uint m_cpk_mode;
    private ushort m_enable_filename;
    private string m_comment;
    private string m_tool_version;
    private bool m_random_padding;

    public uint DataAlign
    {
      get => this.m_data_align;
      set => this.m_data_align = value;
    }

    public uint CpkMode
    {
      set => this.m_cpk_mode = value;
    }

    public ushort EnableFileName
    {
      set => this.m_enable_filename = value;
    }

    public string Comment
    {
      get => this.m_comment;
      set => this.m_comment = value;
    }

    public string ToolVersion
    {
      get => this.m_tool_version;
      set => this.m_tool_version = value;
    }

    public ulong CpkDateTime64
    {
      get => this.m_cpk_datetime;
      set => this.m_cpk_datetime = value;
    }

    public bool EnableDateTime
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_datetime;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_datetime = value;
    }

    public bool RandomPadding
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_random_padding;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_random_padding = value;
    }

    public bool EnableAttributeUserString
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_implant_attribute_name;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_implant_attribute_name = value;
    }

    public unsafe CUtf(
      CUtf.EnumCrcMode crc_mode,
      [MarshalAs(UnmanagedType.U1)] bool use_toc_crc,
      [MarshalAs(UnmanagedType.U1)] bool use_file_crc,
      [MarshalAs(UnmanagedType.U1)] bool enable_ginf)
    {
      void* voidPtr = \u003CModule\u003E.malloc(22528U);
      this.m_heap_memptr = voidPtr;
      this.m_heap = \u003CModule\u003E.criHeap_Create(voidPtr, 22528);
      CriUtfMaker* criUtfMakerPtr1 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr2;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr2 = (IntPtr) criUtfMakerPtr1 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr1);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr1);
      }
      this.m_utf_header = criUtfMakerPtr2;
      CriUtfMaker* criUtfMakerPtr3 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr4;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr4 = (IntPtr) criUtfMakerPtr3 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr3);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr3);
      }
      this.m_utf_header_crc_table = criUtfMakerPtr4;
      CriUtfMaker* criUtfMakerPtr5 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr6;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr6 = (IntPtr) criUtfMakerPtr5 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr5);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr5);
      }
      this.m_utf_finf = criUtfMakerPtr6;
      CriUtfMaker* criUtfMakerPtr7 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr8;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr8 = (IntPtr) criUtfMakerPtr7 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr7);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr7);
      }
      this.m_utf_htoc = criUtfMakerPtr8;
      CriUtfMaker* criUtfMakerPtr9 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr10;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr10 = (IntPtr) criUtfMakerPtr9 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr9);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr9);
      }
      this.m_utf_efinf = criUtfMakerPtr10;
      CriUtfMaker* criUtfMakerPtr11 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr12;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr12 = (IntPtr) criUtfMakerPtr11 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr11);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr11);
      }
      this.m_utf_id = criUtfMakerPtr12;
      CriUtfMaker* criUtfMakerPtr13 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr14;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr14 = (IntPtr) criUtfMakerPtr13 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr13);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr13);
      }
      this.m_utf_id_l = criUtfMakerPtr14;
      CriUtfMaker* criUtfMakerPtr15 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr16;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr16 = (IntPtr) criUtfMakerPtr15 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr15);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr15);
      }
      this.m_utf_id_h = criUtfMakerPtr16;
      CriUtfMaker* criUtfMakerPtr17 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr18;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr18 = (IntPtr) criUtfMakerPtr17 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr17);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr17);
      }
      this.m_utf_eid = criUtfMakerPtr18;
      CriUtfMaker* criUtfMakerPtr19 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr20;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr20 = (IntPtr) criUtfMakerPtr19 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr19);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr19);
      }
      this.m_utf_ginf = criUtfMakerPtr20;
      CriUtfMaker* criUtfMakerPtr21 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr22;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr22 = (IntPtr) criUtfMakerPtr21 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr21);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr21);
      }
      this.m_utf_ginf_glink = criUtfMakerPtr22;
      CriUtfMaker* criUtfMakerPtr23 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr24;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr24 = (IntPtr) criUtfMakerPtr23 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr23);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr23);
      }
      this.m_utf_ginf_flink = criUtfMakerPtr24;
      CriUtfMaker* criUtfMakerPtr25 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr26;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr26 = (IntPtr) criUtfMakerPtr25 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr25);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr25);
      }
      this.m_utf_ginf_attr = criUtfMakerPtr26;
      if (enable_ginf)
      {
        CriUtfMaker* criUtfMakerPtr27 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
        CriUtfMaker* criUtfMakerPtr28;
        // ISSUE: fault handler
        try
        {
          criUtfMakerPtr28 = (IntPtr) criUtfMakerPtr27 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr27);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) criUtfMakerPtr27);
        }
        this.m_utf_ginf_ginf = criUtfMakerPtr28;
      }
      else
        this.m_utf_ginf_ginf = (CriUtfMaker*) 0;
      CriUtfMaker* criUtfMakerPtr29 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr30;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr30 = (IntPtr) criUtfMakerPtr29 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr29);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr29);
      }
      this.m_utf_hgtoc = criUtfMakerPtr30;
      CriUtfMaker* criUtfMakerPtr31 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr32;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr32 = (IntPtr) criUtfMakerPtr31 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr31);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr31);
      }
      this.m_utf_hgtoc_gfpath = criUtfMakerPtr32;
      CriUtfMaker* criUtfMakerPtr33 = (CriUtfMaker*) \u003CModule\u003E.@new(108U);
      CriUtfMaker* criUtfMakerPtr34;
      // ISSUE: fault handler
      try
      {
        criUtfMakerPtr34 = (IntPtr) criUtfMakerPtr33 == IntPtr.Zero ? (CriUtfMaker*) 0 : \u003CModule\u003E.CriUtfMaker\u002E\u007Bctor\u007D(criUtfMakerPtr33);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfMakerPtr33);
      }
      this.m_utf_hgtoc_gfid = criUtfMakerPtr34;
      this.m_iff_header = new CUtfIff((sbyte) 67, (sbyte) 80, (sbyte) 75, (sbyte) 32);
      this.m_iff_toc = new CUtfIff((sbyte) 84, (sbyte) 79, (sbyte) 67, (sbyte) 32);
      this.m_iff_htoc = new CUtfIff((sbyte) 72, (sbyte) 84, (sbyte) 79, (sbyte) 67);
      this.m_iff_etoc = new CUtfIff((sbyte) 69, (sbyte) 84, (sbyte) 79, (sbyte) 67);
      this.m_iff_itoc = new CUtfIff((sbyte) 73, (sbyte) 84, (sbyte) 79, (sbyte) 67);
      this.m_iff_gtoc = new CUtfIff((sbyte) 71, (sbyte) 84, (sbyte) 79, (sbyte) 67);
      this.m_iff_hgtoc = new CUtfIff((sbyte) 72, (sbyte) 71, (sbyte) 84, (sbyte) 79);
      this.Initialize();
      this.m_crc_mode = crc_mode;
      this.m_enable_toc_crc = use_toc_crc;
      this.m_enable_file_crc = use_file_crc;
      this.m_random_padding = false;
      this.m_enable_implant_attribute_name = false;
      CriUtfDefScript* criUtfDefScriptPtr1 = !use_toc_crc ? &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_header_crc_table1 : (crc_mode != CUtf.EnumCrcMode.CrcModeTsbgp2 ? &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_header_crc_table1 : &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_header_crc_table2);
      CriUtfDefScript* criUtfDefScriptPtr2;
      CriUtfDefScript* criUtfDefScriptPtr3;
      CriUtfDefScript* criUtfDefScriptPtr4;
      if (use_file_crc)
      {
        if (crc_mode == CUtf.EnumCrcMode.CrcModeTsbgp2)
        {
          criUtfDefScriptPtr2 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_finfo_ex2;
          criUtfDefScriptPtr3 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_idata_l_ex2;
          criUtfDefScriptPtr4 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_idata_h_ex2;
        }
        else
        {
          criUtfDefScriptPtr2 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_finfo_ex;
          criUtfDefScriptPtr3 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_idata_l_ex;
          criUtfDefScriptPtr4 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_idata_h_ex;
        }
      }
      else
      {
        criUtfDefScriptPtr2 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_finfo;
        criUtfDefScriptPtr3 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_idata_l;
        criUtfDefScriptPtr4 = &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_idata_h;
      }
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_header, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_header);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_header_crc_table, criUtfDefScriptPtr1);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_finf, criUtfDefScriptPtr2);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_htoc, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_htoc);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_efinf, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_efinfo);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_id, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_id_finfo);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_id_l, criUtfDefScriptPtr3);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_id_h, criUtfDefScriptPtr4);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_eid, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_eid_finfo);
      if (enable_ginf)
      {
        \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_ginf, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_group_finfo_ginf);
        \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_ginf_ginf, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_group_ginf);
      }
      else
        \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_ginf, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_group_finfo);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_ginf_glink, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_group_glink);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_ginf_flink, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_group_flink);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_ginf_attr, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_group_attr);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_hgtoc, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_hgtoc);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_hgtoc_gfpath, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_hgtoc_chtbin_array);
      \u003CModule\u003E.CriUtfMaker\u002EAllocateTable(this.m_utf_hgtoc_gfid, &\u003CModule\u003E.\u003FA0x55349cc0\u002Estruct_utf_hgtoc_chtbin_array);
      CUtf cutf1 = this;
      cutf1.SetData(cutf1.m_utf_header_crc_table, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04FABLJDN\u0040Name\u003F\u0024AA\u0040, "TOC", 0U);
      CUtf cutf2 = this;
      cutf2.SetData(cutf2.m_utf_header_crc_table, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04FABLJDN\u0040Name\u003F\u0024AA\u0040, "ITOC", 1U);
      CUtf cutf3 = this;
      cutf3.SetData(cutf3.m_utf_header_crc_table, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04FABLJDN\u0040Name\u003F\u0024AA\u0040, "GTOC", 2U);
      CUtf cutf4 = this;
      cutf4.SetData(cutf4.m_utf_header_crc_table, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04FABLJDN\u0040Name\u003F\u0024AA\u0040, "HTOC", 3U);
      CUtf cutf5 = this;
      cutf5.SetData(cutf5.m_utf_header_crc_table, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04FABLJDN\u0040Name\u003F\u0024AA\u0040, "HGTOC", 4U);
    }

    private void \u007ECUtf() => this.finalize();

    private void \u0021CUtf() => this.finalize();

    private unsafe void finalize()
    {
      CUtfIff iffHgtoc = this.m_iff_hgtoc;
      if (iffHgtoc != null)
      {
        iffHgtoc.Dispose();
        this.m_iff_hgtoc = (CUtfIff) null;
      }
      CUtfIff iffGtoc = this.m_iff_gtoc;
      if (iffGtoc != null)
      {
        iffGtoc.Dispose();
        this.m_iff_gtoc = (CUtfIff) null;
      }
      CUtfIff iffItoc = this.m_iff_itoc;
      if (iffItoc != null)
      {
        iffItoc.Dispose();
        this.m_iff_itoc = (CUtfIff) null;
      }
      CUtfIff iffEtoc = this.m_iff_etoc;
      if (iffEtoc != null)
      {
        iffEtoc.Dispose();
        this.m_iff_etoc = (CUtfIff) null;
      }
      CUtfIff iffHtoc = this.m_iff_htoc;
      if (iffHtoc != null)
      {
        iffHtoc.Dispose();
        this.m_iff_htoc = (CUtfIff) null;
      }
      CUtfIff iffToc = this.m_iff_toc;
      if (iffToc != null)
      {
        iffToc.Dispose();
        this.m_iff_toc = (CUtfIff) null;
      }
      CUtfIff iffHeader = this.m_iff_header;
      if (iffHeader != null)
      {
        iffHeader.Dispose();
        this.m_iff_header = (CUtfIff) null;
      }
      CriUtfMaker* utfHtoc = this.m_utf_htoc;
      if ((IntPtr) utfHtoc != IntPtr.Zero)
      {
        CriUtfMaker* criUtfMakerPtr = utfHtoc;
        \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(criUtfMakerPtr);
        \u003CModule\u003E.delete((void*) criUtfMakerPtr);
        this.m_utf_htoc = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfFinf1 = this.m_utf_finf;
      if ((IntPtr) utfFinf1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfFinf1);
        CriUtfMaker* utfFinf2 = this.m_utf_finf;
        if ((IntPtr) utfFinf2 != IntPtr.Zero)
        {
          \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(utfFinf2);
          \u003CModule\u003E.delete((void*) utfFinf2);
        }
        this.m_utf_finf = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfHgtoc1 = this.m_utf_hgtoc;
      if ((IntPtr) utfHgtoc1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfHgtoc1);
        CriUtfMaker* utfHgtoc2 = this.m_utf_hgtoc;
        if ((IntPtr) utfHgtoc2 != IntPtr.Zero)
        {
          \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(utfHgtoc2);
          \u003CModule\u003E.delete((void*) utfHgtoc2);
        }
        this.m_utf_hgtoc = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfHgtocGfpath1 = this.m_utf_hgtoc_gfpath;
      if ((IntPtr) utfHgtocGfpath1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfHgtocGfpath1);
        CriUtfMaker* utfHgtocGfpath2 = this.m_utf_hgtoc_gfpath;
        if ((IntPtr) utfHgtocGfpath2 != IntPtr.Zero)
        {
          \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(utfHgtocGfpath2);
          \u003CModule\u003E.delete((void*) utfHgtocGfpath2);
        }
        this.m_utf_hgtoc_gfpath = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfHgtocGfid1 = this.m_utf_hgtoc_gfid;
      if ((IntPtr) utfHgtocGfid1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfHgtocGfid1);
        CriUtfMaker* utfHgtocGfid2 = this.m_utf_hgtoc_gfid;
        if ((IntPtr) utfHgtocGfid2 != IntPtr.Zero)
        {
          \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(utfHgtocGfid2);
          \u003CModule\u003E.delete((void*) utfHgtocGfid2);
        }
        this.m_utf_hgtoc_gfid = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfGinf1 = this.m_utf_ginf_ginf;
      if ((IntPtr) utfGinfGinf1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfGinfGinf1);
        CriUtfMaker* utfGinfGinf2 = this.m_utf_ginf_ginf;
        if ((IntPtr) utfGinfGinf2 != IntPtr.Zero)
        {
          \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(utfGinfGinf2);
          \u003CModule\u003E.delete((void*) utfGinfGinf2);
        }
        this.m_utf_ginf_ginf = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfAttr1 = this.m_utf_ginf_attr;
      if ((IntPtr) utfGinfAttr1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfGinfAttr1);
        CriUtfMaker* utfGinfAttr2 = this.m_utf_ginf_attr;
        if ((IntPtr) utfGinfAttr2 != IntPtr.Zero)
        {
          \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(utfGinfAttr2);
          \u003CModule\u003E.delete((void*) utfGinfAttr2);
        }
        this.m_utf_ginf_attr = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfFlink1 = this.m_utf_ginf_flink;
      if ((IntPtr) utfGinfFlink1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfGinfFlink1);
        CriUtfMaker* utfGinfFlink2 = this.m_utf_ginf_flink;
        if ((IntPtr) utfGinfFlink2 != IntPtr.Zero)
        {
          \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(utfGinfFlink2);
          \u003CModule\u003E.delete((void*) utfGinfFlink2);
        }
        this.m_utf_ginf_flink = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfGlink1 = this.m_utf_ginf_glink;
      if ((IntPtr) utfGinfGlink1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfGinfGlink1);
        CriUtfMaker* utfGinfGlink2 = this.m_utf_ginf_glink;
        if ((IntPtr) utfGinfGlink2 != IntPtr.Zero)
          \u003CModule\u003E.CriUtfMaker\u002E__delDtor(utfGinfGlink2, 1U);
        this.m_utf_ginf_glink = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinf1 = this.m_utf_ginf;
      if ((IntPtr) utfGinf1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfGinf1);
        CriUtfMaker* utfGinf2 = this.m_utf_ginf;
        if ((IntPtr) utfGinf2 != IntPtr.Zero)
          \u003CModule\u003E.CriUtfMaker\u002E__delDtor(utfGinf2, 1U);
        this.m_utf_ginf = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfEfinf1 = this.m_utf_efinf;
      if ((IntPtr) utfEfinf1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfEfinf1);
        CriUtfMaker* utfEfinf2 = this.m_utf_efinf;
        if ((IntPtr) utfEfinf2 != IntPtr.Zero)
          \u003CModule\u003E.CriUtfMaker\u002E__delDtor(utfEfinf2, 1U);
        this.m_utf_efinf = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfIdL1 = this.m_utf_id_l;
      if ((IntPtr) utfIdL1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfIdL1);
        CriUtfMaker* utfIdL2 = this.m_utf_id_l;
        if ((IntPtr) utfIdL2 != IntPtr.Zero)
          \u003CModule\u003E.CriUtfMaker\u002E__delDtor(utfIdL2, 1U);
        this.m_utf_id_l = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfIdH1 = this.m_utf_id_h;
      if ((IntPtr) utfIdH1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfIdH1);
        CriUtfMaker* utfIdH2 = this.m_utf_id_h;
        if ((IntPtr) utfIdH2 != IntPtr.Zero)
          \u003CModule\u003E.CriUtfMaker\u002E__delDtor(utfIdH2, 1U);
        this.m_utf_id_h = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfEid1 = this.m_utf_eid;
      if ((IntPtr) utfEid1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfEid1);
        CriUtfMaker* utfEid2 = this.m_utf_eid;
        if ((IntPtr) utfEid2 != IntPtr.Zero)
          \u003CModule\u003E.CriUtfMaker\u002E__delDtor(utfEid2, 1U);
        this.m_utf_eid = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfHeaderCrcTable1 = this.m_utf_header_crc_table;
      if ((IntPtr) utfHeaderCrcTable1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfHeaderCrcTable1);
        CriUtfMaker* utfHeaderCrcTable2 = this.m_utf_header_crc_table;
        if ((IntPtr) utfHeaderCrcTable2 != IntPtr.Zero)
          \u003CModule\u003E.CriUtfMaker\u002E__delDtor(utfHeaderCrcTable2, 1U);
        this.m_utf_header_crc_table = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if ((IntPtr) utfHeader1 != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfHeader1);
        CriUtfMaker* utfHeader2 = this.m_utf_header;
        if ((IntPtr) utfHeader2 != IntPtr.Zero)
          \u003CModule\u003E.CriUtfMaker\u002E__delDtor(utfHeader2, 1U);
        this.m_utf_header = (CriUtfMaker*) 0;
      }
      this.ReleaseBinaryImage();
      _criheap_struct* heap = this.m_heap;
      if ((IntPtr) heap == IntPtr.Zero)
        return;
      \u003CModule\u003E.criHeap_Destroy(heap);
      this.m_heap = (_criheap_struct*) 0;
      \u003CModule\u003E.free(this.m_heap_memptr);
      this.m_heap_memptr = (void*) 0;
    }

    public unsafe void Initialize()
    {
      this.m_num_file = 0U;
      this.m_num_htoc_tables = 0U;
      this.m_num_hgtoc_tables = 0U;
      this.m_enable_hgtoc_gfpath = false;
      this.m_enable_hgtoc_gfid = false;
      this.m_num_id_l = 0U;
      this.m_num_id_h = 0U;
      this.m_num_eid = 0U;
      this.m_cpk_datetime = 0UL;
      this.m_data_align = 2048U;
      this.m_total_packed_size = 0UL;
      this.m_total_contents_file_size = 0UL;
      this.m_bin_header = (CriUtfBinary*) 0;
      this.m_bin_header_crc_table = (CriUtfBinary*) 0;
      this.m_bin_finf = (CriUtfBinary*) 0;
      this.m_bin_htoc = (CriUtfBinary*) 0;
      this.m_bin_efinf = (CriUtfBinary*) 0;
      this.m_bin_id = (CriUtfBinary*) 0;
      this.m_bin_id_l = (CriUtfBinary*) 0;
      this.m_bin_id_h = (CriUtfBinary*) 0;
      this.m_bin_eid = (CriUtfBinary*) 0;
      this.m_bin_gtoc = (CriUtfBinary*) 0;
      this.m_bin_glink = (CriUtfBinary*) 0;
      this.m_bin_flink = (CriUtfBinary*) 0;
      this.m_bin_attr = (CriUtfBinary*) 0;
      this.m_bin_ginf = (CriUtfBinary*) 0;
      this.m_bin_hgtoc = (CriUtfBinary*) 0;
      this.m_bin_hgtoc_gfpath = (CriUtfBinary*) 0;
      this.m_bin_hgtoc_gfid = (CriUtfBinary*) 0;
    }

    public unsafe void ReleaseBinaryImage()
    {
      CriUtfBinary* binHeader = this.m_bin_header;
      if ((IntPtr) binHeader != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binHeader;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_header = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binHeaderCrcTable = this.m_bin_header_crc_table;
      if ((IntPtr) binHeaderCrcTable != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binHeaderCrcTable;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_header_crc_table = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binFinf = this.m_bin_finf;
      if ((IntPtr) binFinf != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binFinf;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_finf = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binHtoc = this.m_bin_htoc;
      if ((IntPtr) binHtoc != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binHtoc;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_htoc = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binEfinf = this.m_bin_efinf;
      if ((IntPtr) binEfinf != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binEfinf;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_efinf = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binId = this.m_bin_id;
      if ((IntPtr) binId != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binId;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_id = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binIdL = this.m_bin_id_l;
      if ((IntPtr) binIdL != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binIdL;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_id_l = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binIdH = this.m_bin_id_h;
      if ((IntPtr) binIdH != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binIdH;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_id_h = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binEid = this.m_bin_eid;
      if ((IntPtr) binEid != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binEid;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_eid = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binGtoc = this.m_bin_gtoc;
      if ((IntPtr) binGtoc != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binGtoc;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_gtoc = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binGlink = this.m_bin_glink;
      if ((IntPtr) binGlink != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binGlink;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_glink = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binFlink = this.m_bin_flink;
      if ((IntPtr) binFlink != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binFlink;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_flink = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binAttr = this.m_bin_attr;
      if ((IntPtr) binAttr != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binAttr;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          \u003CModule\u003E.free((void*) num);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
        this.m_bin_attr = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binGinf = this.m_bin_ginf;
      if ((IntPtr) binGinf != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfBinary\u002E__delDtor(binGinf, 1U);
        this.m_bin_ginf = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binHgtoc = this.m_bin_hgtoc;
      if ((IntPtr) binHgtoc != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfBinary\u002E__delDtor(binHgtoc, 1U);
        this.m_bin_hgtoc = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binHgtocGfpath = this.m_bin_hgtoc_gfpath;
      if ((IntPtr) binHgtocGfpath != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfBinary\u002E__delDtor(binHgtocGfpath, 1U);
        this.m_bin_hgtoc_gfpath = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binHgtocGfid = this.m_bin_hgtoc_gfid;
      if ((IntPtr) binHgtocGfid == IntPtr.Zero)
        return;
      \u003CModule\u003E.CriUtfBinary\u002E__delDtor(binHgtocGfid, 1U);
      this.m_bin_hgtoc_gfid = (CriUtfBinary*) 0;
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, ushort inval, uint recode)
    {
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname), inval);
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, ulong inval, uint recode)
    {
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname), inval);
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, int inval, uint recode)
    {
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname), inval);
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, uint inval, uint recode)
    {
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname), inval);
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, string inval, uint recode)
    {
      if (inval == (string) null)
        return;
      if (inval == "")
      {
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
          return;
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, fieldname), (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_00CNPNBAHC\u0040\u003F\u0024AA\u0040);
      }
      else
      {
        sbyte* numPtr1 = Utility.AllocCharString(inval);
        sbyte* numPtr2 = (IntPtr) numPtr1 != IntPtr.Zero ? numPtr1 : throw new Exception("failed make the marshaling string.");
        if (*numPtr1 != (sbyte) 0)
        {
          do
          {
            ++numPtr2;
          }
          while (*numPtr2 != (sbyte) 0);
        }
        uint num = (uint) ((IntPtr) numPtr2 - (IntPtr) numPtr1);
        if (num > 511U)
          throw new Exception(string.Format("invalid string length! len = {0}", (object) num.ToString()));
        \u003CModule\u003E.CriUtfMaker\u002ESetData\u003Cchar\u0020\u002A\u003E(utf, recode, fieldname, numPtr1);
        Utility.FreeCharString(numPtr1);
      }
    }

    public unsafe ulong AddFileInfo(
      string fullpath,
      ulong fsize,
      ulong extsize,
      ulong offs,
      uint id,
      uint inf,
      string usr,
      ulong datetime,
      uint crc32,
      ulong checksum64,
      string attribute_name)
    {
      try
      {
        string inval1 = Path.GetDirectoryName(fullpath);
        string fileName = Path.GetFileName(fullpath);
        if (!this.m_enable_datetime)
          datetime = 0UL;
        if (!string.IsNullOrEmpty(inval1))
          inval1 = inval1.Replace("\\", "/");
        if (!string.IsNullOrEmpty(fullpath))
          fullpath = fullpath.Replace("\\", "/");
        CUtf cutf1 = this;
        cutf1.SetData(cutf1.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07FDBCPIKC\u0040DirName\u003F\u0024AA\u0040, inval1, this.m_num_file);
        CUtf cutf2 = this;
        cutf2.SetData(cutf2.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08FDCJCDOL\u0040FileName\u003F\u0024AA\u0040, fileName, this.m_num_file);
        CUtf cutf3 = this;
        cutf3.SetData(cutf3.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040, fsize, this.m_num_file);
        CUtf cutf4 = this;
        cutf4.SetData(cutf4.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040HCBECAL\u0040ExtractSize\u003F\u0024AA\u0040, (uint) extsize, this.m_num_file);
        CUtf cutf5 = this;
        cutf5.SetData(cutf5.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040KDHCLPBL\u0040FileOffset\u003F\u0024AA\u0040, offs, this.m_num_file);
        CUtf cutf6 = this;
        cutf6.SetData(cutf6.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_02OLOABKKD\u0040ID\u003F\u0024AA\u0040, id, this.m_num_file);
        if (this.m_enable_implant_attribute_name)
        {
          string inval2 = attribute_name == (string) null || string.Compare(attribute_name, "") == 0 ? "" : "CRI_CFATTR:" + attribute_name;
          CUtf cutf7 = this;
          cutf7.SetData(cutf7.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040FHCJPPHJ\u0040UserString\u003F\u0024AA\u0040, inval2, this.m_num_file);
        }
        else
        {
          CUtf cutf8 = this;
          cutf8.SetData(cutf8.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040FHCJPPHJ\u0040UserString\u003F\u0024AA\u0040, usr, this.m_num_file);
        }
        if (this.m_enable_file_crc)
        {
          switch (this.m_crc_mode)
          {
            case CUtf.EnumCrcMode.CrcModeStandard:
              CUtf cutf9 = this;
              cutf9.SetData(cutf9.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040, crc32, this.m_num_file);
              break;
            case CUtf.EnumCrcMode.CrcModeTsbgp2:
              CUtf cutf10 = this;
              cutf10.SetData(cutf10.m_utf_finf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040, checksum64, this.m_num_file);
              break;
          }
        }
        CUtf cutf11 = this;
        cutf11.SetData(cutf11.m_utf_efinf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040HHMMOFOC\u0040UpdateDateTime\u003F\u0024AA\u0040, datetime, this.m_num_file);
        CUtf cutf12 = this;
        cutf12.SetData(cutf12.m_utf_efinf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08EDNGJNCP\u0040LocalDir\u003F\u0024AA\u0040, inval1, this.m_num_file);
        this.m_total_packed_size += extsize;
        this.m_total_contents_file_size += fsize;
      }
      catch (Exception ex)
      {
        return ulong.MaxValue;
      }
      return (ulong) ++this.m_num_file;
    }

    public unsafe void AddHashTableHtoc(string ht_name, void* ht_data_ptr, int ht_data_len)
    {
      int numHtocTables = (int) this.m_num_htoc_tables;
      CriUtfMaker* utfHtoc = this.m_utf_htoc;
      CriUtfCellTypeVldTag utfCellTypeVldTag1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag1 = (int) ht_data_ptr;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag1 + 4) = ht_data_len;
      this.SetData(utfHtoc, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07IBDBELFJ\u0040ht_name\u003F\u0024AA\u0040, ht_name, (uint) numHtocTables);
      CriUtfCellTypeVldTag utfCellTypeVldTag2 = utfCellTypeVldTag1;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHtoc, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07LMBKAOAD\u0040ht_data\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHtoc, (uint) numHtocTables, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHtoc, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07LMBKAOAD\u0040ht_data\u003F\u0024AA\u0040), utfCellTypeVldTag2);
      ++this.m_num_htoc_tables;
    }

    public unsafe void AddHashTableHgtoc(string ht_name, void* ht_data_ptr, int ht_data_len)
    {
      int numHgtocTables = (int) this.m_num_hgtoc_tables;
      CriUtfMaker* utfHgtoc = this.m_utf_hgtoc;
      CriUtfCellTypeVldTag utfCellTypeVldTag1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag1 = (int) ht_data_ptr;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag1 + 4) = ht_data_len;
      this.SetData(utfHgtoc, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07IBDBELFJ\u0040ht_name\u003F\u0024AA\u0040, ht_name, (uint) numHgtocTables);
      CriUtfCellTypeVldTag utfCellTypeVldTag2 = utfCellTypeVldTag1;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHgtoc, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07LMBKAOAD\u0040ht_data\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHgtoc, (uint) numHgtocTables, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHgtoc, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07LMBKAOAD\u0040ht_data\u003F\u0024AA\u0040), utfCellTypeVldTag2);
      ++this.m_num_hgtoc_tables;
    }

    private unsafe void SetHashTable(
      CriUtfMaker* utf,
      int index,
      string ht_name,
      void* ht_data_ptr,
      int ht_data_len)
    {
      CriUtfCellTypeVldTag utfCellTypeVldTag1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag1 = (int) ht_data_ptr;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag1 + 4) = ht_data_len;
      this.SetData(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07IBDBELFJ\u0040ht_name\u003F\u0024AA\u0040, ht_name, (uint) index);
      CriUtfCellTypeVldTag utfCellTypeVldTag2 = utfCellTypeVldTag1;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07LMBKAOAD\u0040ht_data\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, (uint) index, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07LMBKAOAD\u0040ht_data\u003F\u0024AA\u0040), utfCellTypeVldTag2);
    }

    public unsafe void SetHashTableHgtocGfpathToGfinf(
      int index,
      void* ht_data_ptr,
      int ht_data_len,
      int flink_index)
    {
      CUtf cutf = this;
      cutf.SetHashTableArray(cutf.m_utf_hgtoc_gfpath, index, ht_data_ptr, ht_data_len, flink_index);
      this.m_enable_hgtoc_gfpath = true;
    }

    public unsafe void SetHashTableHgtocGfidToGfinf(
      int index,
      void* ht_data_ptr,
      int ht_data_len,
      int flink_index)
    {
      CUtf cutf = this;
      cutf.SetHashTableArray(cutf.m_utf_hgtoc_gfid, index, ht_data_ptr, ht_data_len, flink_index);
      this.m_enable_hgtoc_gfid = true;
    }

    private unsafe void SetHashTableArray(
      CriUtfMaker* utf,
      int index,
      void* ht_data_ptr,
      int ht_data_len,
      int flink_index)
    {
      CriUtfCellTypeVldTag utfCellTypeVldTag1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag1 = (int) ht_data_ptr;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag1 + 4) = ht_data_len;
      CriUtfCellTypeVldTag utfCellTypeVldTag2 = utfCellTypeVldTag1;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09IAPALEPA\u0040ht_chtbin\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, (uint) index, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09IAPALEPA\u0040ht_chtbin\u003F\u0024AA\u0040), utfCellTypeVldTag2);
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040BAALDLJB\u0040ht_flink_index\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, (uint) index, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040BAALDLJB\u0040ht_flink_index\u003F\u0024AA\u0040), flink_index);
    }

    public unsafe ulong AddFileInfoId(
      ulong fsize,
      ulong extsize,
      uint id,
      ulong toc_index,
      uint crc32,
      ulong checksum64)
    {
      CriUtfMaker* utf;
      uint recode;
      if (extsize > (ulong) ushort.MaxValue)
      {
        utf = this.m_utf_id_h;
        uint numIdH = this.m_num_id_h;
        recode = numIdH;
        this.m_num_id_h = numIdH + 1U;
      }
      else
      {
        utf = this.m_utf_id_l;
        uint numIdL = this.m_num_id_l;
        recode = numIdL;
        this.m_num_id_l = numIdL + 1U;
      }
      this.SetData(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_02OLOABKKD\u0040ID\u003F\u0024AA\u0040, (ushort) id, recode);
      if (extsize > (ulong) ushort.MaxValue)
      {
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040), (uint) fsize);
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040HCBECAL\u0040ExtractSize\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040HCBECAL\u0040ExtractSize\u003F\u0024AA\u0040), (uint) extsize);
      }
      else
      {
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040), (ushort) (uint) fsize);
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040HCBECAL\u0040ExtractSize\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040HCBECAL\u0040ExtractSize\u003F\u0024AA\u0040), (ushort) (uint) extsize);
      }
      if (this.m_enable_file_crc)
      {
        switch (this.m_crc_mode)
        {
          case CUtf.EnumCrcMode.CrcModeStandard:
            if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040) != uint.MaxValue)
            {
              \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040), crc32);
              break;
            }
            break;
          case CUtf.EnumCrcMode.CrcModeTsbgp2:
            if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040) != uint.MaxValue)
            {
              \u003CModule\u003E.CriUtfMaker\u002ESetData(utf, recode, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040), checksum64);
              break;
            }
            break;
        }
      }
      uint numEid1 = this.m_num_eid;
      CriUtfMaker* utfEid1 = this.m_utf_eid;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfEid1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_02OLOABKKD\u0040ID\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfEid1, numEid1, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfEid1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_02OLOABKKD\u0040ID\u003F\u0024AA\u0040), (int) id);
      uint numEid2 = this.m_num_eid;
      CriUtfMaker* utfEid2 = this.m_utf_eid;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfEid2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08JGDGBHKN\u0040TocIndex\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfEid2, numEid2, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfEid2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08JGDGBHKN\u0040TocIndex\u003F\u0024AA\u0040), (int) toc_index);
      ++this.m_num_eid;
      return (ulong) (this.m_num_id_l + this.m_num_id_h);
    }

    public ulong SetDataAllGroupInfo(CGroupingManager gmanager)
    {
      gmanager.DebugPrintGroupInfo();
      List<CGroup> groups = gmanager.Groups;
      return 0;
    }

    private unsafe void IfMaskEnableThenUnMask(void* retptr, uint size, [MarshalAs(UnmanagedType.U1)] bool mask)
    {
      if (!mask)
        return;
      byte* numPtr1 = (byte*) retptr;
      uint num1 = 25951;
      if (0U >= size)
        return;
      uint num2 = size;
      do
      {
        byte* numPtr2 = numPtr1;
        int num3 = (int) *numPtr2 ^ (int) (byte) num1;
        *numPtr2 = (byte) num3;
        num1 *= 16661U;
        ++numPtr1;
        --num2;
      }
      while (num2 > 0U);
    }

    public void ResetTocFileInfo() => this.m_num_file = 0U;

    public unsafe void* MakeTocImage(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask, [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_finf, isCompress ? 1 : 0);
      CUtf.Check_new_utf_bin(ref this.m_bin_finf, (int) \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_finf));
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_finf, this.m_bin_finf);
      CriUtfBinary* binFinf = this.m_bin_finf;
      void* voidPtr = (void*) *(int*) binFinf;
      uint length = (uint) *(int*) ((IntPtr) binFinf + 8);
      *size = length;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeStandard)
      {
        uint num = CCrc32.CalcCrc32(3735928559U, length, voidPtr);
        CriUtfMaker* utfHeader = this.m_utf_header;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06CDNCOLO\u0040TocCrc\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06CDNCOLO\u0040TocCrc\u003F\u0024AA\u0040), num);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040), num);
      }
      if (mask)
      {
        byte* numPtr1 = (byte*) voidPtr;
        uint num1 = *size;
        uint num2 = 25951;
        if (0U < num1)
        {
          uint num3 = num1;
          do
          {
            byte* numPtr2 = numPtr1;
            int num4 = (int) *numPtr2 ^ (int) (byte) num2;
            *numPtr2 = (byte) num4;
            num2 *= 16661U;
            ++numPtr1;
            --num3;
          }
          while (num3 > 0U);
        }
      }
      this.m_iff_toc.ChunkSize = (ulong) *size;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeTsbgp2)
      {
        ulong num = CCheckSum.CalcCheckSum64Tsbgp2(this.m_iff_toc.GetFourCCPointer(), 16U, voidPtr, *size);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040), num);
      }
      ulong num5 = this.m_iff_toc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07MAFOODKE\u0040TocSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader1, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07MAFOODKE\u0040TocSize\u003F\u0024AA\u0040), num5);
      return voidPtr;
    }

    public void ResetItocFileInfo()
    {
      this.m_num_eid = 0U;
      this.m_num_id_h = 0U;
      this.m_num_id_l = 0U;
    }

    public unsafe void ReleaseTocImageBuffer() => \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_finf);

    public unsafe void* MakeHtocImage(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask, [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_htoc, isCompress ? 1 : 0);
      CUtf.Check_new_utf_bin(ref this.m_bin_htoc, (int) \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_htoc));
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_htoc, this.m_bin_htoc);
      CriUtfBinary* binHtoc = this.m_bin_htoc;
      void* voidPtr = (void*) *(int*) binHtoc;
      uint length = (uint) *(int*) ((IntPtr) binHtoc + 8);
      *size = length;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeStandard)
      {
        uint num = CCrc32.CalcCrc32(48879U, length, voidPtr);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 3U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040), num);
      }
      if (mask)
      {
        byte* numPtr1 = (byte*) voidPtr;
        uint num1 = *size;
        uint num2 = 25951;
        if (0U < num1)
        {
          uint num3 = num1;
          do
          {
            byte* numPtr2 = numPtr1;
            int num4 = (int) *numPtr2 ^ (int) (byte) num2;
            *numPtr2 = (byte) num4;
            num2 *= 16661U;
            ++numPtr1;
            --num3;
          }
          while (num3 > 0U);
        }
      }
      this.m_iff_htoc.ChunkSize = (ulong) *size;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeTsbgp2)
      {
        ulong num = CCheckSum.CalcCheckSum64Tsbgp2(this.m_iff_htoc.GetFourCCPointer(), 16U, voidPtr, *size);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 3U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040), num);
      }
      ulong num5 = this.m_iff_htoc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08NPEJOLCK\u0040HtocSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08NPEJOLCK\u0040HtocSize\u003F\u0024AA\u0040), num5);
      return voidPtr;
    }

    public void ResetHtocFileInfo() => this.m_num_htoc_tables = 0U;

    public unsafe void ReleaseHtocImageBuffer() => \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_htoc);

    public unsafe void* MakeItocImageCompact(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask, [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_id_l, isCompress ? 1 : 0);
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_id_h, isCompress ? 1 : 0);
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_id, isCompress ? 1 : 0);
      uint binarySize1 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_id_l);
      uint binarySize2 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_id_h);
      CUtf.Check_new_utf_bin(ref this.m_bin_id_l, (int) binarySize1);
      CUtf.Check_new_utf_bin(ref this.m_bin_id_h, (int) binarySize2);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_id_l, this.m_bin_id_l);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_id_h, this.m_bin_id_h);
      void* voidPtr1 = (void*) *(int*) this.m_bin_id_l;
      void* voidPtr2 = (void*) *(int*) this.m_bin_id_h;
      uint numRecord1 = \u003CModule\u003E.CriUtfMaker\u002EGetNumRecord(this.m_utf_id_l);
      uint numRecord2 = \u003CModule\u003E.CriUtfMaker\u002EGetNumRecord(this.m_utf_id_h);
      CriUtfMaker* utfId1 = this.m_utf_id;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfId1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06DAPCHFCN\u0040FilesL\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfId1, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfId1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06DAPCHFCN\u0040FilesL\u003F\u0024AA\u0040), numRecord1);
      CriUtfMaker* utfId2 = this.m_utf_id;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfId2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06FEJOLACJ\u0040FilesH\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfId2, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfId2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06FEJOLACJ\u0040FilesH\u003F\u0024AA\u0040), numRecord2);
      CriUtfCellTypeVldTag utfCellTypeVldTag1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag1 = (int) voidPtr1;
      CriUtfCellTypeVldTag utfCellTypeVldTag2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag2 = (int) voidPtr2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag1 + 4) = (int) binarySize1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag2 + 4) = (int) binarySize2;
      CriUtfCellTypeVldTag utfCellTypeVldTag3 = utfCellTypeVldTag1;
      CriUtfMaker* utfId3 = this.m_utf_id;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfId3, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05IPCCNOAO\u0040DataL\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfId3, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfId3, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05IPCCNOAO\u0040DataL\u003F\u0024AA\u0040), utfCellTypeVldTag3);
      CriUtfCellTypeVldTag utfCellTypeVldTag4 = utfCellTypeVldTag2;
      CriUtfMaker* utfId4 = this.m_utf_id;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfId4, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05OLEOBLAK\u0040DataH\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfId4, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfId4, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05OLEOBLAK\u0040DataH\u003F\u0024AA\u0040), utfCellTypeVldTag4);
      uint binarySize3 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_id);
      *size = binarySize3;
      CUtf.Check_new_utf_bin(ref this.m_bin_id, (int) binarySize3);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_id, this.m_bin_id);
      void* voidPtr3 = (void*) *(int*) this.m_bin_id;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeStandard)
      {
        uint num = CCrc32.CalcCrc32(3203391149U, *size, voidPtr3);
        CriUtfMaker* utfHeader = this.m_utf_header;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07DBMMOPD\u0040ItocCrc\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07DBMMOPD\u0040ItocCrc\u003F\u0024AA\u0040), num);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 1U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040), num);
      }
      if (mask)
      {
        byte* numPtr1 = (byte*) voidPtr3;
        uint num1 = *size;
        uint num2 = 25951;
        if (0U < num1)
        {
          uint num3 = num1;
          do
          {
            byte* numPtr2 = numPtr1;
            int num4 = (int) *numPtr2 ^ (int) (byte) num2;
            *numPtr2 = (byte) num4;
            num2 *= 16661U;
            ++numPtr1;
            --num3;
          }
          while (num3 > 0U);
        }
      }
      this.m_iff_itoc.ChunkSize = (ulong) *size;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeTsbgp2)
      {
        ulong inval = CCheckSum.CalcCheckSum64Tsbgp2(this.m_iff_itoc.GetFourCCPointer(), 16U, voidPtr3, *size);
        CUtf cutf = this;
        cutf.SetData(cutf.m_utf_header_crc_table, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040, inval, 1U);
      }
      ulong num5 = this.m_iff_itoc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08MIDCPPGJ\u0040ItocSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader1, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08MIDCPPGJ\u0040ItocSize\u003F\u0024AA\u0040), num5);
      CUtf cutf1 = this;
      cutf1.SetData(cutf1.m_utf_header, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03JJCPLEKC\u0040EID\u003F\u0024AA\u0040, 0, 0U);
      return voidPtr3;
    }

    public unsafe void ReleaseITocImageCompactBuffer()
    {
      \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_id_l);
      \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_id_h);
      \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_id);
    }

    public unsafe void* MakeItocImageExtra(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask, [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_eid, isCompress ? 1 : 0);
      uint binarySize = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_eid);
      *size = binarySize;
      CUtf.Check_new_utf_bin(ref this.m_bin_eid, (int) binarySize);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_eid, this.m_bin_eid);
      void* voidPtr = (void*) *(int*) this.m_bin_eid;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeStandard)
      {
        uint num = CCrc32.CalcCrc32(3203391149U, *size, voidPtr);
        CriUtfMaker* utfHeader = this.m_utf_header;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07DBMMOPD\u0040ItocCrc\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07DBMMOPD\u0040ItocCrc\u003F\u0024AA\u0040), num);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 1U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040), num);
      }
      if (mask)
      {
        byte* numPtr1 = (byte*) voidPtr;
        uint num1 = *size;
        uint num2 = 25951;
        if (0U < num1)
        {
          uint num3 = num1;
          do
          {
            byte* numPtr2 = numPtr1;
            int num4 = (int) *numPtr2 ^ (int) (byte) num2;
            *numPtr2 = (byte) num4;
            num2 *= 16661U;
            ++numPtr1;
            --num3;
          }
          while (num3 > 0U);
        }
      }
      this.m_iff_itoc.ChunkSize = (ulong) *size;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeTsbgp2)
      {
        ulong num = CCheckSum.CalcCheckSum64Tsbgp2(this.m_iff_itoc.GetFourCCPointer(), 16U, voidPtr, *size);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 1U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040), num);
      }
      ulong num5 = this.m_iff_itoc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08MIDCPPGJ\u0040ItocSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader1, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08MIDCPPGJ\u0040ItocSize\u003F\u0024AA\u0040), num5);
      CriUtfMaker* utfHeader2 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03JJCPLEKC\u0040EID\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader2, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03JJCPLEKC\u0040EID\u003F\u0024AA\u0040), 1);
      return voidPtr;
    }

    public unsafe void ReleaseITocImageExtraBuffer()
    {
      \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_eid);
      CriUtfMaker* utfEid = this.m_utf_eid;
      if ((IntPtr) utfEid != IntPtr.Zero)
      {
        \u003CModule\u003E.CriUtfMaker\u002E\u007Bdtor\u007D(utfEid);
        \u003CModule\u003E.delete((void*) utfEid);
      }
      this.m_utf_eid = (CriUtfMaker*) 0;
    }

    private unsafe int SetAttributeUtfInfo(CGroupingManager gmanager)
    {
      Hashtable attrInfo = gmanager.AttrInfo;
      int num = 0;
      foreach (DictionaryEntry dictionaryEntry in attrInfo)
      {
        ValueType valueType = (ValueType) dictionaryEntry;
        CAttrInfo cattrInfo = (CAttrInfo) ((DictionaryEntry) valueType).Value;
        string key = (string) ((DictionaryEntry) valueType).Key;
        CUtf cutf1 = this;
        cutf1.SetData(cutf1.m_utf_ginf_attr, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05HHCEBJEO\u0040Aname\u003F\u0024AA\u0040, key, (uint) cattrInfo.Index);
        CUtf cutf2 = this;
        cutf2.SetData(cutf2.m_utf_ginf_attr, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05CGDDDONM\u0040Align\u003F\u0024AA\u0040, cattrInfo.Alignment, (uint) cattrInfo.Index);
        CUtf cutf3 = this;
        cutf3.SetData(cutf3.m_utf_ginf_attr, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DIOAMJFE\u0040Files\u003F\u0024AA\u0040, 0U, (uint) cattrInfo.Index);
        CUtf cutf4 = this;
        cutf4.SetData(cutf4.m_utf_ginf_attr, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040, 0U, (uint) cattrInfo.Index);
        ++num;
      }
      return num;
    }

    public unsafe void* MakeGtocImage(
      CGroupingManager gmanager,
      uint* size,
      [MarshalAs(UnmanagedType.U1)] bool mask,
      [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_ginf_glink, isCompress ? 1 : 0);
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_ginf_flink, isCompress ? 1 : 0);
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_ginf_attr, isCompress ? 1 : 0);
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_ginf, isCompress ? 1 : 0);
      this.m_flinks = 0;
      this.m_attrs = 0U;
      this.m_groups = 0U;
      gmanager.Enumerate(new CGroupingManager.EnumerateFunction(this.Enumerate), (object) null);
      CUtf cutf1 = this;
      cutf1.m_attrs = (uint) cutf1.SetAttributeUtfInfo(gmanager);
      uint binarySize1 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_ginf_glink);
      uint binarySize2 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_ginf_flink);
      uint binarySize3 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_ginf_attr);
      CUtf.Check_new_utf_bin(ref this.m_bin_glink, (int) binarySize1);
      CUtf.Check_new_utf_bin(ref this.m_bin_flink, (int) binarySize2);
      CUtf.Check_new_utf_bin(ref this.m_bin_attr, (int) binarySize3);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_ginf_glink, this.m_bin_glink);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_ginf_flink, this.m_bin_flink);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_ginf_attr, this.m_bin_attr);
      void* voidPtr1 = (void*) *(int*) this.m_bin_glink;
      void* voidPtr2 = (void*) *(int*) this.m_bin_flink;
      void* voidPtr3 = (void*) *(int*) this.m_bin_attr;
      uint numRecord1 = \u003CModule\u003E.CriUtfMaker\u002EGetNumRecord(this.m_utf_ginf_glink);
      uint numRecord2 = \u003CModule\u003E.CriUtfMaker\u002EGetNumRecord(this.m_utf_ginf_flink);
      uint numRecord3 = \u003CModule\u003E.CriUtfMaker\u002EGetNumRecord(this.m_utf_ginf_attr);
      CUtf cutf2 = this;
      cutf2.SetData(cutf2.m_utf_ginf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05ICMMBCAL\u0040Glink\u003F\u0024AA\u0040, numRecord1, 0U);
      CUtf cutf3 = this;
      cutf3.SetData(cutf3.m_utf_ginf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05EJJAMBKO\u0040Flink\u003F\u0024AA\u0040, numRecord2, 0U);
      CUtf cutf4 = this;
      cutf4.SetData(cutf4.m_utf_ginf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04PGOCJFCI\u0040Attr\u003F\u0024AA\u0040, numRecord3, 0U);
      CriUtfCellTypeVldTag utfCellTypeVldTag1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag1 = (int) voidPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag1 + 4) = (int) binarySize1;
      CriUtfCellTypeVldTag utfCellTypeVldTag2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag2 = (int) voidPtr2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag2 + 4) = (int) binarySize2;
      CriUtfCellTypeVldTag utfCellTypeVldTag3;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utfCellTypeVldTag3 = (int) voidPtr3;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &utfCellTypeVldTag3 + 4) = (int) binarySize3;
      CriUtfCellTypeVldTag utfCellTypeVldTag4 = utfCellTypeVldTag1;
      CriUtfMaker* utfGinf1 = this.m_utf_ginf;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05JMFGLPAJ\u0040Gdata\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinf1, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05JMFGLPAJ\u0040Gdata\u003F\u0024AA\u0040), utfCellTypeVldTag4);
      CriUtfCellTypeVldTag utfCellTypeVldTag5 = utfCellTypeVldTag2;
      CriUtfMaker* utfGinf2 = this.m_utf_ginf;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05FHAKGMKM\u0040Fdata\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinf2, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05FHAKGMKM\u0040Fdata\u003F\u0024AA\u0040), utfCellTypeVldTag5);
      CriUtfCellTypeVldTag utfCellTypeVldTag6 = utfCellTypeVldTag3;
      CriUtfMaker* utfGinf3 = this.m_utf_ginf;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf3, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08KHDPMPAI\u0040AttrData\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinf3, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf3, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08KHDPMPAI\u0040AttrData\u003F\u0024AA\u0040), utfCellTypeVldTag6);
      CriUtfMaker* utfGinfGinf = this.m_utf_ginf_ginf;
      if ((IntPtr) utfGinfGinf != IntPtr.Zero)
      {
        int num = isCompress ? 1 : 0;
        \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(utfGinfGinf, num);
        uint binarySize4 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_ginf_ginf);
        CUtf.Check_new_utf_bin(ref this.m_bin_ginf, (int) binarySize4);
        \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_ginf_ginf, this.m_bin_ginf);
        void* voidPtr4 = (void*) *(int*) this.m_bin_ginf;
        uint numRecord4 = \u003CModule\u003E.CriUtfMaker\u002EGetNumRecord(this.m_utf_ginf_ginf);
        CriUtfMaker* utfGinf4 = this.m_utf_ginf;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf4, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04OEMOBODJ\u0040Ginf\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinf4, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf4, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04OEMOBODJ\u0040Ginf\u003F\u0024AA\u0040), numRecord4);
        CriUtfCellTypeVldTag utfCellTypeVldTag7;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref utfCellTypeVldTag7 = (int) voidPtr4;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &utfCellTypeVldTag7 + 4) = (int) binarySize4;
        CriUtfCellTypeVldTag utfCellTypeVldTag8 = utfCellTypeVldTag7;
        CriUtfMaker* utfGinf5 = this.m_utf_ginf;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf5, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08GIHCBBHF\u0040GinfData\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinf5, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinf5, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08GIHCBBHF\u0040GinfData\u003F\u0024AA\u0040), utfCellTypeVldTag8);
      }
      uint binarySize5 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_ginf);
      *size = binarySize5;
      CUtf.Check_new_utf_bin(ref this.m_bin_gtoc, (int) binarySize5);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_ginf, this.m_bin_gtoc);
      void* voidPtr5 = (void*) *(int*) this.m_bin_gtoc;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeStandard)
      {
        uint inval = CCrc32.CalcCrc32(12513024U, *size, voidPtr5);
        CUtf cutf5 = this;
        cutf5.SetData(cutf5.m_utf_header, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07BGJGNMMB\u0040GtocCrc\u003F\u0024AA\u0040, inval, 0U);
        CUtf cutf6 = this;
        cutf6.SetData(cutf6.m_utf_header_crc_table, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040, inval, 2U);
      }
      this.IfMaskEnableThenUnMask(voidPtr5, *size, mask);
      this.m_iff_gtoc.ChunkSize = (ulong) *size;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeTsbgp2)
      {
        ulong inval = CCheckSum.CalcCheckSum64Tsbgp2(this.m_iff_gtoc.GetFourCCPointer(), 16U, voidPtr5, *size);
        CUtf cutf7 = this;
        cutf7.SetData(cutf7.m_utf_header_crc_table, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040, inval, 2U);
      }
      CUtf cutf8 = this;
      cutf8.SetData(cutf8.m_utf_header, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08PACEPL\u0040GtocSize\u003F\u0024AA\u0040, this.m_iff_gtoc.ChunkSize + 16UL, 0U);
      return voidPtr5;
    }

    public unsafe void ResetGtocFileInfo()
    {
      \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_ginf_glink);
      \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_ginf_flink);
      \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_ginf_attr);
      CriUtfMaker* utfGinfGinf = this.m_utf_ginf_ginf;
      if ((IntPtr) utfGinfGinf != IntPtr.Zero)
        \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(utfGinfGinf);
      \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_ginf);
    }

    public void ReleaseGtocImageBuffer() => this.ResetGtocFileInfo();

    private static unsafe void Check_new_utf_bin(ref CriUtfBinary* utf_bin, int size)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      uint num1 = (uint) ^(int&) ref utf_bin;
      if (num1 != 0U)
      {
        CriUtfBinary* criUtfBinaryPtr = (CriUtfBinary*) num1;
        uint num2 = (uint) *(int*) criUtfBinaryPtr;
        if (num2 != 0U)
          \u003CModule\u003E.free((void*) num2);
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr);
      }
      CriUtfBinary* criUtfBinaryPtr1 = (CriUtfBinary*) \u003CModule\u003E.@new(20U);
      CriUtfBinary* criUtfBinaryPtr2;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) criUtfBinaryPtr1 != IntPtr.Zero)
        {
          *(int*) criUtfBinaryPtr1 = (int) \u003CModule\u003E.malloc((uint) size);
          *(int*) ((IntPtr) criUtfBinaryPtr1 + 12) = size;
          *(int*) ((IntPtr) criUtfBinaryPtr1 + 16) = 0;
          *(int*) ((IntPtr) criUtfBinaryPtr1 + 8) = 0;
          *(int*) ((IntPtr) criUtfBinaryPtr1 + 4) = 0;
          criUtfBinaryPtr2 = criUtfBinaryPtr1;
        }
        else
          criUtfBinaryPtr2 = (CriUtfBinary*) 0;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) criUtfBinaryPtr1);
      }
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref utf_bin = (int) criUtfBinaryPtr2;
    }

    public unsafe void* MakeHgtocImage(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask, [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      if (this.m_enable_hgtoc_gfpath)
      {
        \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_hgtoc_gfpath, isCompress ? 1 : 0);
        uint binarySize = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_hgtoc_gfpath);
        CUtf.Check_new_utf_bin(ref this.m_bin_hgtoc_gfpath, (int) binarySize);
        \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_hgtoc_gfpath, this.m_bin_hgtoc_gfpath);
        this.AddHashTableHgtoc("ht_gfpath_to_gfinf", (void*) *(int*) this.m_bin_hgtoc_gfpath, (int) binarySize);
      }
      if (this.m_enable_hgtoc_gfid)
      {
        \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_hgtoc_gfid, isCompress ? 1 : 0);
        uint binarySize = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_hgtoc_gfid);
        CUtf.Check_new_utf_bin(ref this.m_bin_hgtoc_gfid, (int) binarySize);
        \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_hgtoc_gfid, this.m_bin_hgtoc_gfid);
        this.AddHashTableHgtoc("ht_gfid_to_gfinf", (void*) *(int*) this.m_bin_hgtoc_gfid, (int) binarySize);
      }
      \u003CModule\u003E.CriUtfMaker\u002ESetCompressMode(this.m_utf_hgtoc, isCompress ? 1 : 0);
      uint binarySize1 = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_hgtoc);
      CUtf.Check_new_utf_bin(ref this.m_bin_hgtoc, (int) binarySize1);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_hgtoc, this.m_bin_hgtoc);
      void* voidPtr = (void*) *(int*) this.m_bin_hgtoc;
      *size = binarySize1;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeStandard)
      {
        uint num = CCrc32.CalcCrc32(782064U, binarySize1, voidPtr);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 4U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03ODHGDBCI\u0040CRC\u003F\u0024AA\u0040), num);
      }
      if (mask)
      {
        byte* numPtr1 = (byte*) voidPtr;
        uint num1 = *size;
        uint num2 = 25951;
        if (0U < num1)
        {
          uint num3 = num1;
          do
          {
            byte* numPtr2 = numPtr1;
            int num4 = (int) *numPtr2 ^ (int) (byte) num2;
            *numPtr2 = (byte) num4;
            num2 *= 16661U;
            ++numPtr1;
            --num3;
          }
          while (num3 > 0U);
        }
      }
      this.m_iff_hgtoc.ChunkSize = (ulong) *size;
      if (this.m_enable_toc_crc && this.m_crc_mode == CUtf.EnumCrcMode.CrcModeTsbgp2)
      {
        ulong num = CCheckSum.CalcCheckSum64Tsbgp2(this.m_iff_hgtoc.GetFourCCPointer(), 16U, voidPtr, *size);
        CriUtfMaker* utfHeaderCrcTable = this.m_utf_header_crc_table;
        if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040) != uint.MaxValue)
          \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeaderCrcTable, 4U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeaderCrcTable, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040DBGLFODI\u0040CheckSum64\u003F\u0024AA\u0040), num);
      }
      ulong num5 = this.m_iff_hgtoc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09KOPJNNE\u0040HgtocSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09KOPJNNE\u0040HgtocSize\u003F\u0024AA\u0040), num5);
      return voidPtr;
    }

    public void ResetHgtocInfo()
    {
      this.m_num_hgtoc_tables = 0U;
      this.m_enable_hgtoc_gfpath = false;
      this.m_enable_hgtoc_gfid = false;
    }

    public void ReleaseHgtocImageBuffer() => this.ResetGtocFileInfo();

    private unsafe void SetToUtfFileInfos(List<CAttribute> attrList)
    {
      CGroup cgroup = (CGroup) null;
      int flinks1 = this.m_flinks;
      int num1 = attrList.Count + this.m_flinks;
      this.m_flinks = attrList.Count + this.m_flinks;
      int index1 = 0;
      if (0 < attrList.Count)
      {
        int recode = flinks1;
        int num2 = -flinks1;
        do
        {
          CAttribute attr = attrList[index1];
          cgroup = attr.ParentGroup;
          CFileInfoLink fileInfoLink = attr.FileInfoLink;
          CUtf cutf1 = this;
          cutf1.SetData(cutf1.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06JEKIFDJD\u0040Aindex\u003F\u0024AA\u0040, (ushort) attr.AttrIndex, (uint) recode);
          CUtf cutf2 = this;
          cutf2.SetData(cutf2.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040, recode + 1, (uint) recode);
          CUtf cutf3 = this;
          cutf3.SetData(cutf3.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIMBDOHM\u0040Child\u003F\u0024AA\u0040, -this.m_flinks, (uint) recode);
          CUtf cutf4 = this;
          cutf4.SetData(cutf4.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09DMHDBJJA\u0040SortFlink\u003F\u0024AA\u0040, fileInfoLink.FileCount, (uint) recode);
          int index2 = 0;
          if (0 < fileInfoLink.ContinuousFiles)
          {
            do
            {
              CFileInfo fileListContinuou = fileInfoLink.FileListContinuous[index2];
              CUtf cutf5 = this;
              cutf5.SetData(cutf5.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06JEKIFDJD\u0040Aindex\u003F\u0024AA\u0040, (ushort) attr.AttrIndex, (uint) this.m_flinks);
              int flinks2 = this.m_flinks;
              CUtf cutf6 = this;
              cutf6.SetData(cutf6.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040, flinks2 + 1, (uint) flinks2);
              CUtf cutf7 = this;
              cutf7.SetData(cutf7.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIMBDOHM\u0040Child\u003F\u0024AA\u0040, (int) fileListContinuou.InfoIndex, (uint) this.m_flinks);
              ++this.m_flinks;
              ++index2;
            }
            while (index2 < fileInfoLink.ContinuousFiles);
          }
          int index3 = 0;
          if (0 < fileInfoLink.UncontinuousFiles)
          {
            do
            {
              CFileInfo fileListUncontinuou = fileInfoLink.FileListUncontinuous[index3];
              CUtf cutf8 = this;
              cutf8.SetData(cutf8.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06JEKIFDJD\u0040Aindex\u003F\u0024AA\u0040, (ushort) attr.AttrIndex, (uint) this.m_flinks);
              int flinks3 = this.m_flinks;
              CUtf cutf9 = this;
              cutf9.SetData(cutf9.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040, flinks3 + 1, (uint) flinks3);
              CUtf cutf10 = this;
              cutf10.SetData(cutf10.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIMBDOHM\u0040Child\u003F\u0024AA\u0040, (int) fileListUncontinuou.InfoIndex, (uint) this.m_flinks);
              ++this.m_flinks;
              ++index3;
            }
            while (index3 < fileInfoLink.UncontinuousFiles);
          }
          List<CFileInfo> flist = new List<CFileInfo>();
          List<CFileInfo>.Enumerator enumerator1 = fileInfoLink.FileListContinuous.GetEnumerator();
          if (enumerator1.MoveNext())
          {
            do
            {
              CFileInfo current = enumerator1.Current;
              flist.Add(current);
            }
            while (enumerator1.MoveNext());
          }
          List<CFileInfo>.Enumerator enumerator2 = fileInfoLink.FileListUncontinuous.GetEnumerator();
          if (enumerator2.MoveNext())
          {
            do
            {
              CFileInfo current = enumerator2.Current;
              flist.Add(current);
            }
            while (enumerator2.MoveNext());
          }
          int index4 = 0;
          if (0 < flist.Count)
          {
            do
            {
              flist[index4].SortedLink = index4 + num1;
              ++index4;
            }
            while (index4 < flist.Count);
          }
          CFileData.SortByInfoIndex(flist);
          int index5 = 0;
          if (0 < flist.Count)
          {
            do
            {
              CUtf cutf11 = this;
              cutf11.SetData(cutf11.m_utf_ginf_flink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09DMHDBJJA\u0040SortFlink\u003F\u0024AA\u0040, flist[index5].SortedLink, (uint) (index5 + num1));
              ++index5;
            }
            while (index5 < flist.Count);
          }
          num1 = flist.Count + num1;
          uint num3 = (uint) (this.m_flinks - 1);
          CriUtfMaker* utfGinfFlink = this.m_utf_ginf_flink;
          if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfFlink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040) != uint.MaxValue)
            \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinfFlink, num3, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfFlink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040), num2);
          ++index1;
          --num2;
          ++recode;
        }
        while (index1 < attrList.Count);
      }
      int num4 = -(int) cgroup.Index;
      CriUtfMaker* utfGinfFlink1 = this.m_utf_ginf_flink;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfFlink1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinfFlink1, (uint) (flinks1 + index1 - 1), \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfFlink1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040), num4);
    }

    private unsafe void Enumerate(object obj, object userObject)
    {
      if (!(obj is CGroup cgroup))
        return;
      CUtf cutf = this;
      cutf.SetData(cutf.m_utf_ginf_glink, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05KBHNPKFD\u0040Gname\u003F\u0024AA\u0040, cgroup.GroupName, cgroup.Index);
      uint index1 = cgroup.Index;
      int next = cgroup.Next;
      CriUtfMaker* utfGinfGlink1 = this.m_utf_ginf_glink;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfGlink1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinfGlink1, index1, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfGlink1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040), next);
      uint index2 = cgroup.Index;
      int child = cgroup.Child;
      CriUtfMaker* utfGinfGlink2 = this.m_utf_ginf_glink;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfGlink2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIMBDOHM\u0040Child\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinfGlink2, index2, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfGlink2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIMBDOHM\u0040Child\u003F\u0024AA\u0040), child);
      if (cgroup.AttributeList == null)
        return;
      ++this.m_groups;
      this.SetToUtfFileInfos(cgroup.AttributeList);
    }

    public unsafe void* MakeEtocImage(uint* size, string basedir, [MarshalAs(UnmanagedType.U1)] bool mask)
    {
      uint numFile = this.m_num_file;
      CriUtfMaker* utfEfinf = this.m_utf_efinf;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfEfinf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040HHMMOFOC\u0040UpdateDateTime\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfEfinf, numFile, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfEfinf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040HHMMOFOC\u0040UpdateDateTime\u003F\u0024AA\u0040), 0UL);
      CUtf cutf = this;
      cutf.SetData(cutf.m_utf_efinf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08EDNGJNCP\u0040LocalDir\u003F\u0024AA\u0040, basedir, this.m_num_file);
      uint binarySize = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_efinf);
      *size = binarySize;
      CUtf.Check_new_utf_bin(ref this.m_bin_efinf, (int) binarySize);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_efinf, this.m_bin_efinf);
      void* voidPtr = (void*) *(int*) this.m_bin_efinf;
      if (mask)
      {
        byte* numPtr1 = (byte*) voidPtr;
        uint num1 = *size;
        uint num2 = 25951;
        if (0U < num1)
        {
          uint num3 = num1;
          do
          {
            byte* numPtr2 = numPtr1;
            int num4 = (int) *numPtr2 ^ (int) (byte) num2;
            *numPtr2 = (byte) num4;
            num2 *= 16661U;
            ++numPtr1;
            --num3;
          }
          while (num3 > 0U);
        }
      }
      this.m_iff_etoc.ChunkSize = (ulong) *size;
      ulong num = this.m_iff_etoc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08COAGAMHN\u0040EtocSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08COAGAMHN\u0040EtocSize\u003F\u0024AA\u0040), num);
      return voidPtr;
    }

    public unsafe void* MakeHeaderImage(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask)
    {
      CUtf.EnumCrcMode inval = this.m_crc_mode;
      if (!this.m_enable_toc_crc && !this.m_enable_file_crc)
        inval = CUtf.EnumCrcMode.CrcModeStandard;
      ulong cpkDatetime = this.m_cpk_datetime;
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040HHMMOFOC\u0040UpdateDateTime\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader1, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040HHMMOFOC\u0040UpdateDateTime\u003F\u0024AA\u0040), cpkDatetime);
      uint numFile = this.m_num_file;
      CriUtfMaker* utfHeader2 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DIOAMJFE\u0040Files\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader2, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DIOAMJFE\u0040Files\u003F\u0024AA\u0040), numFile);
      CriUtfMaker* utfHeader3 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader3, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07NGFJPNPN\u0040Version\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader3, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader3, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07NGFJPNPN\u0040Version\u003F\u0024AA\u0040), (ushort) 7);
      CriUtfMaker* utfHeader4 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader4, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08DNLDHPFP\u0040Revision\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader4, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader4, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08DNLDHPFP\u0040Revision\u003F\u0024AA\u0040), (ushort) 14);
      ushort dataAlign = (ushort) this.m_data_align;
      CriUtfMaker* utfHeader5 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader5, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05CGDDDONM\u0040Align\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader5, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader5, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05CGDDDONM\u0040Align\u003F\u0024AA\u0040), dataAlign);
      ulong contentsFileSize = this.m_total_contents_file_size;
      CriUtfMaker* utfHeader6 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader6, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BA\u0040HPEFNJHM\u0040EnabledDataSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader6, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader6, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BA\u0040HPEFNJHM\u0040EnabledDataSize\u003F\u0024AA\u0040), contentsFileSize);
      ulong totalPackedSize = this.m_total_packed_size;
      CriUtfMaker* utfHeader7 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader7, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BC\u0040ILANKEOJ\u0040EnabledPackedSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader7, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader7, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BC\u0040ILANKEOJ\u0040EnabledPackedSize\u003F\u0024AA\u0040), totalPackedSize);
      CUtf cutf1 = this;
      cutf1.SetData(cutf1.m_utf_header, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07JABLCLAF\u0040Comment\u003F\u0024AA\u0040, this.m_comment, 0U);
      CUtf cutf2 = this;
      cutf2.SetData(cutf2.m_utf_header, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05PACPJAOI\u0040Tvers\u003F\u0024AA\u0040, this.m_tool_version, 0U);
      uint groups = this.m_groups;
      CriUtfMaker* utfHeader8 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader8, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06LCLFDFOL\u0040Groups\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader8, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader8, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06LCLFDFOL\u0040Groups\u003F\u0024AA\u0040), groups);
      uint attrs = this.m_attrs;
      CriUtfMaker* utfHeader9 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader9, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DBFBGAFK\u0040Attrs\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader9, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader9, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DBFBGAFK\u0040Attrs\u003F\u0024AA\u0040), attrs);
      uint cpkMode = this.m_cpk_mode;
      CriUtfMaker* utfHeader10 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader10, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07CHOGENJJ\u0040CpkMode\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader10, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader10, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07CHOGENJJ\u0040CpkMode\u003F\u0024AA\u0040), cpkMode);
      ushort enableFilename = this.m_enable_filename;
      CriUtfMaker* utfHeader11 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader11, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040KGMFBBHK\u0040EnableFileName\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader11, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader11, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040KGMFBBHK\u0040EnableFileName\u003F\u0024AA\u0040), enableFilename);
      ushort enableTocCrc = (ushort) this.m_enable_toc_crc;
      CriUtfMaker* utfHeader12 = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader12, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0N\u0040JHPAGLDG\u0040EnableTocCrc\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader12, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader12, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0N\u0040JHPAGLDG\u0040EnableTocCrc\u003F\u0024AA\u0040), enableTocCrc);
      CUtf cutf3 = this;
      cutf3.SetData(cutf3.m_utf_header, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0O\u0040DGBHGAAI\u0040EnableFileCrc\u003F\u0024AA\u0040, (ushort) this.m_enable_file_crc, 0U);
      CUtf cutf4 = this;
      cutf4.SetData(cutf4.m_utf_header, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07FMCKNONN\u0040CrcMode\u003F\u0024AA\u0040, (uint) inval, 0U);
      CriUtfCellTypeVldTag val;
      if (this.m_enable_toc_crc && inval == CUtf.EnumCrcMode.CrcModeTsbgp2)
      {
        uint binarySize = \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_header_crc_table);
        CUtf.Check_new_utf_bin(ref this.m_bin_header_crc_table, (int) binarySize);
        \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_header_crc_table, this.m_bin_header_crc_table);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref val = *(int*) this.m_bin_header_crc_table;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &val + 4) = (int) binarySize;
      }
      else
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref val = 0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &val + 4) = 0;
      }
      \u003CModule\u003E.CriUtfMaker\u002ESetData\u003Cstruct\u0020CriUtfCellTypeVldTag\u003E(this.m_utf_header, 0U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08FBOHJCDE\u0040CrcTable\u003F\u0024AA\u0040, val);
      CUtf.Check_new_utf_bin(ref this.m_bin_header, (int) \u003CModule\u003E.CriUtfMaker\u002EGetBinarySize(this.m_utf_header) + 4096);
      \u003CModule\u003E.CriUtfMaker\u002EMakeBinary(this.m_utf_header, this.m_bin_header);
      CriUtfBinary* binHeader = this.m_bin_header;
      void* ptr = (void*) *(int*) binHeader;
      uint length = (uint) *(int*) ((IntPtr) binHeader + 8);
      *size = length;
      uint num1 = CCrc32.CalcCrc32(2967527664U, length, ptr);
      *(int*) ((int) *size + (IntPtr) ptr) = (int) num1;
      if (mask)
      {
        byte* numPtr1 = (byte*) ptr;
        uint num2 = *size;
        uint num3 = 25951;
        if (0U < num2)
        {
          uint num4 = num2;
          do
          {
            byte* numPtr2 = numPtr1;
            int num5 = (int) *numPtr2 ^ (int) (byte) num3;
            *numPtr2 = (byte) num5;
            num3 *= 16661U;
            ++numPtr1;
            --num4;
          }
          while (num4 > 0U);
        }
      }
      this.m_iff_header.ChunkSize = (ulong) *size;
      if (!this.m_random_padding)
      {
        uint num6 = *size;
        // ISSUE: initblk instruction
        __memset((IntPtr) ptr + (int) num6 + 4, 0, 2048 - (int) num6);
      }
      else
      {
        Random random = new Random((int) *size);
        uint num7 = *size;
        byte* numPtr = (byte*) ((IntPtr) ptr + (int) num7 + 4);
        uint num8 = 0;
        if (0U < 2044U - num7)
        {
          do
          {
            *numPtr = (byte) random.Next();
            ++numPtr;
            ++num8;
          }
          while (num8 < 2044U - *size);
        }
        if (random is IDisposable disposable)
          disposable.Dispose();
      }
      CUtf.stamp_cri(2048U, ptr);
      *size = 2032U;
      return ptr;
    }

    public unsafe void ReleaseEtocImageBuffer() => \u003CModule\u003E.CriUtfMaker\u002EReleaseBinaryBuffer(this.m_utf_efinf);

    private static void stamp_cri(uint hsize, void* ptr)
    {
      // ISSUE: unable to decompile the method.
    }

    public unsafe void* GetHeaderIffPtr() => this.m_iff_header.GetFourCCPointer();

    public unsafe void* GetTocIffPtr() => this.m_iff_toc.GetFourCCPointer();

    public unsafe void* GetHtocIffPtr() => this.m_iff_htoc.GetFourCCPointer();

    public unsafe void* GetItocIffPtr() => this.m_iff_itoc.GetFourCCPointer();

    public unsafe void* GetGtocIffPtr() => this.m_iff_gtoc.GetFourCCPointer();

    public unsafe void* GetHgtocIffPtr() => this.m_iff_hgtoc.GetFourCCPointer();

    public unsafe void* GetEtocIffPtr() => this.m_iff_etoc.GetFourCCPointer();

    public uint GetIffSize() => 16;

    public unsafe void SetContentsFileOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0O\u0040DIKLBOAH\u0040ContentOffset\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0O\u0040DIKLBOAH\u0040ContentOffset\u003F\u0024AA\u0040), ofs);
    }

    public unsafe void SetContentsFileSize(ulong size)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040MJNKDBDC\u0040ContentSize\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040MJNKDBDC\u0040ContentSize\u003F\u0024AA\u0040), size);
    }

    public unsafe void SetTocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09HHGNIJOH\u0040TocOffset\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09HHGNIJOH\u0040TocOffset\u003F\u0024AA\u0040), ofs);
    }

    public unsafe void SetHtocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040NMLGKLAH\u0040HtocOffset\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040NMLGKLAH\u0040HtocOffset\u003F\u0024AA\u0040), ofs);
    }

    public unsafe void SetEtocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040BHCCOGAA\u0040EtocOffset\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040BHCCOGAA\u0040EtocOffset\u003F\u0024AA\u0040), ofs);
    }

    public unsafe void SetGtocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040EPEOFPMB\u0040GtocOffset\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040EPEOFPMB\u0040GtocOffset\u003F\u0024AA\u0040), ofs);
    }

    public unsafe void SetHgtocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040OPOPGGJL\u0040HgtocOffset\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0M\u0040OPOPGGJL\u0040HgtocOffset\u003F\u0024AA\u0040), ofs);
    }

    public unsafe void SetItocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040BNDIHEMH\u0040ItocOffset\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040BNDIHEMH\u0040ItocOffset\u003F\u0024AA\u0040), ofs);
    }

    public unsafe void SetMaxDpkItoc(int maxdpksize)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07FLEHKLJ\u0040DpkItoc\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07FLEHKLJ\u0040DpkItoc\u003F\u0024AA\u0040), maxdpksize);
    }

    public unsafe void SetCodec(int codec)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIBLJHPJ\u0040Codec\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIBLJHPJ\u0040Codec\u003F\u0024AA\u0040), codec);
    }

    public unsafe void SetSortedFilenameFlag([MarshalAs(UnmanagedType.U1)] bool sw)
    {
      ushort num = (ushort) sw;
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06KBMOENCI\u0040Sorted\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06KBMOENCI\u0040Sorted\u003F\u0024AA\u0040), num);
    }

    public void SetMaskFlag([MarshalAs(UnmanagedType.U1)] bool mask)
    {
      this.m_iff_header.SetMaskFlag(mask);
      this.m_iff_toc.SetMaskFlag(mask);
      this.m_iff_htoc.SetMaskFlag(mask);
      this.m_iff_etoc.SetMaskFlag(mask);
      this.m_iff_itoc.SetMaskFlag(mask);
      this.m_iff_gtoc.SetMaskFlag(mask);
      this.m_iff_hgtoc.SetMaskFlag(mask);
    }

    public unsafe void SetUpdates(uint updates)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07PCBHAKJL\u0040Updates\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfHeader, 0U, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfHeader, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07PCBHAKJL\u0040Updates\u003F\u0024AA\u0040), updates);
    }

    public unsafe void SetGroupGinf(string gaName, int size, int files, int records)
    {
      CUtf cutf = this;
      cutf.SetData(cutf.m_utf_ginf_ginf, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06NKCGCMGE\u0040GAname\u003F\u0024AA\u0040, gaName, (uint) records);
      CriUtfMaker* utfGinfGinf1 = this.m_utf_ginf_ginf;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfGinf1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040) != uint.MaxValue)
        \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinfGinf1, (uint) records, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfGinf1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040), size);
      CriUtfMaker* utfGinfGinf2 = this.m_utf_ginf_ginf;
      if (\u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfGinf2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DIOAMJFE\u0040Files\u003F\u0024AA\u0040) == uint.MaxValue)
        return;
      \u003CModule\u003E.CriUtfMaker\u002ESetData(utfGinfGinf2, (uint) records, \u003CModule\u003E.CriUtfMaker\u002EConvFieldNameToNo(utfGinfGinf2, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DIOAMJFE\u0040Files\u003F\u0024AA\u0040), files);
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECUtf();
      }
      else
      {
        try
        {
          this.\u0021CUtf();
        }
        finally
        {
          // ISSUE: explicit finalizer call
          base.Finalize();
        }
      }
    }

    public virtual void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~CUtf() => this.Dispose(false);

    public enum EnumCrcMode
    {
      CrcModeStandard,
      CrcModeTsbgp2,
    }
  }
}
