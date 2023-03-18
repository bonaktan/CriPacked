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
    private unsafe CriUtfMaker* m_utf_finf;
    private unsafe CriUtfMaker* m_utf_efinf;
    private unsafe CriUtfMaker* m_utf_id;
    private unsafe CriUtfMaker* m_utf_id_l;
    private unsafe CriUtfMaker* m_utf_id_h;
    private unsafe CriUtfMaker* m_utf_eid;
    private unsafe CriUtfMaker* m_utf_ginf;
    private unsafe CriUtfMaker* m_utf_ginf_glink;
    private unsafe CriUtfMaker* m_utf_ginf_alink;
    private unsafe CriUtfMaker* m_utf_ginf_flink;
    private unsafe CriUtfMaker* m_utf_ginf_attr;
    private unsafe CriUtfMaker* m_utf_ginf_ginf;
    private unsafe CriUtfBinary* m_bin_header;
    private unsafe CriUtfBinary* m_bin_finf;
    private unsafe CriUtfBinary* m_bin_id;
    private unsafe CriUtfBinary* m_bin_id_l;
    private unsafe CriUtfBinary* m_bin_id_h;
    private unsafe CriUtfBinary* m_bin_eid;
    private unsafe CriUtfBinary* m_bin_glink;
    private unsafe CriUtfBinary* m_bin_alink;
    private unsafe CriUtfBinary* m_bin_flink;
    private unsafe CriUtfBinary* m_bin_attr;
    private unsafe CriUtfBinary* m_bin_ginf;
    private CUtfIff m_iff_header;
    private CUtfIff m_iff_toc;
    private CUtfIff m_iff_etoc;
    private CUtfIff m_iff_itoc;
    private CUtfIff m_iff_gtoc;
    private bool m_enable_datetime;
    private bool m_enable_toc_crc;
    private bool m_enable_file_crc;
    private uint m_crc_toc;
    private uint m_crc_itoc;
    private uint m_crc_gtoc;
    private uint m_num_file;
    private uint m_num_id_l;
    private uint m_num_id_h;
    private uint m_num_eid;
    private uint m_data_align;
    private ulong m_total_packed_size;
    private ulong m_total_contents_file_size;
    private uint m_groups;
    private uint m_attrs;
    private int m_flinks;
    private uint m_updates;
    private ulong m_cpk_datetime;
    private uint m_cpk_mode;
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

    public unsafe CUtf([MarshalAs(UnmanagedType.U1)] bool use_toc_crc, [MarshalAs(UnmanagedType.U1)] bool use_file_crc, [MarshalAs(UnmanagedType.U1)] bool enable_ginf)
    {
      void* voidPtr = __cpp__.malloc(22528U);
      this.m_heap_memptr = voidPtr;
      this.m_heap = __cpp__.criHeap_Create(voidPtr, 22528);  //TODO: find criHeap_Create()
      CriUtfMaker* criUtfMakerPtr1 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr2;

      // how does this work?
      // 1. allocate memory to voidPtr
      // 2. memory creation again???
      // 3. allocate memory to criUtfMakerPtr1/2
      try {
          criUtfMakerPtr2 = (IntPtr) criUtfMakerPtr1 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr1); }
      __fault {
        __cpp__.delete((void*) criUtfMakerPtr1); }
      // ???
      // TODO: find CriUtfMaker.{ctor}()

      this.m_utf_header = criUtfMakerPtr2;
      CriUtfMaker* criUtfMakerPtr3 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr4;
      try
      {
        criUtfMakerPtr4 = (IntPtr) criUtfMakerPtr3 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr3);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr3);
      }
      this.m_utf_finf = criUtfMakerPtr4;
      CriUtfMaker* criUtfMakerPtr5 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr6;
      try
      {
        criUtfMakerPtr6 = (IntPtr) criUtfMakerPtr5 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr5);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr5);
      }
      this.m_utf_efinf = criUtfMakerPtr6;
      CriUtfMaker* criUtfMakerPtr7 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr8;
      try
      {
        criUtfMakerPtr8 = (IntPtr) criUtfMakerPtr7 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr7);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr7);
      }
      this.m_utf_id = criUtfMakerPtr8;
      CriUtfMaker* criUtfMakerPtr9 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr10;
      try
      {
        criUtfMakerPtr10 = (IntPtr) criUtfMakerPtr9 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr9);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr9);
      }
      this.m_utf_id_l = criUtfMakerPtr10;
      CriUtfMaker* criUtfMakerPtr11 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr12;
      try
      {
        criUtfMakerPtr12 = (IntPtr) criUtfMakerPtr11 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr11);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr11);
      }
      this.m_utf_id_h = criUtfMakerPtr12;
      CriUtfMaker* criUtfMakerPtr13 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr14;
      try
      {
        criUtfMakerPtr14 = (IntPtr) criUtfMakerPtr13 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr13);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr13);
      }
      this.m_utf_eid = criUtfMakerPtr14;
      CriUtfMaker* criUtfMakerPtr15 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr16;
      try
      {
        criUtfMakerPtr16 = (IntPtr) criUtfMakerPtr15 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr15);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr15);
      }
      this.m_utf_ginf = criUtfMakerPtr16;
      CriUtfMaker* criUtfMakerPtr17 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr18;
      try
      {
        criUtfMakerPtr18 = (IntPtr) criUtfMakerPtr17 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr17);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr17);
      }
      this.m_utf_ginf_glink = criUtfMakerPtr18;
      CriUtfMaker* criUtfMakerPtr19 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr20;
      try
      {
        criUtfMakerPtr20 = (IntPtr) criUtfMakerPtr19 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr19);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr19);
      }
      this.m_utf_ginf_alink = criUtfMakerPtr20;
      CriUtfMaker* criUtfMakerPtr21 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr22;
      try
      {
        criUtfMakerPtr22 = (IntPtr) criUtfMakerPtr21 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr21);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr21);
      }
      this.m_utf_ginf_flink = criUtfMakerPtr22;
      CriUtfMaker* criUtfMakerPtr23 = (CriUtfMaker*) __cpp__.@new(108U);
      CriUtfMaker* criUtfMakerPtr24;
      try
      {
        criUtfMakerPtr24 = (IntPtr) criUtfMakerPtr23 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr23);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfMakerPtr23);
      }
      this.m_utf_ginf_attr = criUtfMakerPtr24;
      if (enable_ginf)
      {
        CriUtfMaker* criUtfMakerPtr25 = (CriUtfMaker*) __cpp__.@new(108U);
        CriUtfMaker* criUtfMakerPtr26;
        try
        {
          criUtfMakerPtr26 = (IntPtr) criUtfMakerPtr25 == IntPtr.Zero ? (CriUtfMaker*) 0 : __cpp__.CriUtfMaker.{ctor}(criUtfMakerPtr25);
        }
        __fault
        {
          __cpp__.delete((void*) criUtfMakerPtr25);
        }
        this.m_utf_ginf_ginf = criUtfMakerPtr26;
      }
      else
        this.m_utf_ginf_ginf = (CriUtfMaker*) 0;
      // After Init, do this
      this.m_iff_header = new CUtfIff((sbyte) 67, (sbyte) 80, (sbyte) 75, (sbyte) 32);
      this.m_iff_toc = new CUtfIff((sbyte) 84, (sbyte) 79, (sbyte) 67, (sbyte) 32);
      this.m_iff_etoc = new CUtfIff((sbyte) 69, (sbyte) 84, (sbyte) 79, (sbyte) 67);
      this.m_iff_itoc = new CUtfIff((sbyte) 73, (sbyte) 84, (sbyte) 79, (sbyte) 67);
      this.m_iff_gtoc = new CUtfIff((sbyte) 71, (sbyte) 84, (sbyte) 79, (sbyte) 67);
      this.Initialize();
      this.m_enable_toc_crc = use_toc_crc;
      this.m_enable_file_crc = use_file_crc;
      this.m_random_padding = false;
      if (!use_file_crc)
      {     // TODO: find CriUtfMaker.AllocateTable() and its structs
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_header, &__cpp__.?A0x121e0818.struct_utf_header);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_finf, &__cpp__.?A0x121e0818.struct_utf_finfo);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_efinf, &__cpp__.?A0x121e0818.struct_utf_efinfo);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_id, &__cpp__.?A0x121e0818.struct_utf_id_finfo);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_id_l, &__cpp__.?A0x121e0818.struct_utf_idata_l);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_id_h, &__cpp__.?A0x121e0818.struct_utf_idata_h);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_eid, &__cpp__.?A0x121e0818.struct_utf_eid_finfo);
        if (enable_ginf)
        {
          __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf, &__cpp__.?A0x121e0818.struct_utf_group_finfo_ginf);
          __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf_ginf, &__cpp__.?A0x121e0818.struct_utf_group_ginf);
        }
        else
          __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf, &__cpp__.?A0x121e0818.struct_utf_group_finfo);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf_glink, &__cpp__.?A0x121e0818.struct_utf_group_glink);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf_flink, &__cpp__.?A0x121e0818.struct_utf_group_flink);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf_attr, &__cpp__.?A0x121e0818.struct_utf_group_attr);
      }
      else
      {
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_header, &__cpp__.?A0x121e0818.struct_utf_header);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_finf, &__cpp__.?A0x121e0818.struct_utf_finfo_ex);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_efinf, &__cpp__.?A0x121e0818.struct_utf_efinfo);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_id, &__cpp__.?A0x121e0818.struct_utf_id_finfo);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_id_l, &__cpp__.?A0x121e0818.struct_utf_idata_l_ex);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_id_h, &__cpp__.?A0x121e0818.struct_utf_idata_h_ex);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_eid, &__cpp__.?A0x121e0818.struct_utf_eid_finfo);
        if (enable_ginf)
        {
          __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf, &__cpp__.?A0x121e0818.struct_utf_group_finfo_ginf);
          __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf_ginf, &__cpp__.?A0x121e0818.struct_utf_group_ginf);
        }
        else
          __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf, &__cpp__.?A0x121e0818.struct_utf_group_finfo);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf_glink, &__cpp__.?A0x121e0818.struct_utf_group_glink);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf_flink, &__cpp__.?A0x121e0818.struct_utf_group_flink);
        __cpp__.CriUtfMaker.AllocateTable(this.m_utf_ginf_attr, &__cpp__.?A0x121e0818.struct_utf_group_attr);
      }
    }

    private void ~CUtf() => this.finalize();

    private void !CUtf() => this.finalize();

    private unsafe void finalize()
    {
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
      if ((IntPtr) this.m_utf_finf != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_ginf_attr);
        CriUtfMaker* utfFinf = this.m_utf_finf;
        if ((IntPtr) utfFinf != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfFinf);
          __cpp__.delete((void*) utfFinf);
        }
        this.m_utf_finf = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfGinf1 = this.m_utf_ginf_ginf;
      if ((IntPtr) utfGinfGinf1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfGinfGinf1);
        CriUtfMaker* utfGinfGinf2 = this.m_utf_ginf_ginf;
        if ((IntPtr) utfGinfGinf2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfGinfGinf2);
          __cpp__.delete((void*) utfGinfGinf2);
        }
        this.m_utf_ginf_ginf = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfAttr1 = this.m_utf_ginf_attr;
      if ((IntPtr) utfGinfAttr1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfGinfAttr1);
        CriUtfMaker* utfGinfAttr2 = this.m_utf_ginf_attr;
        if ((IntPtr) utfGinfAttr2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfGinfAttr2);
          __cpp__.delete((void*) utfGinfAttr2);
        }
        this.m_utf_ginf_attr = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfFlink1 = this.m_utf_ginf_flink;
      if ((IntPtr) utfGinfFlink1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfGinfFlink1);
        CriUtfMaker* utfGinfFlink2 = this.m_utf_ginf_flink;
        if ((IntPtr) utfGinfFlink2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfGinfFlink2);
          __cpp__.delete((void*) utfGinfFlink2);
        }
        this.m_utf_ginf_flink = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfAlink1 = this.m_utf_ginf_alink;
      if ((IntPtr) utfGinfAlink1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfGinfAlink1);
        CriUtfMaker* utfGinfAlink2 = this.m_utf_ginf_alink;
        if ((IntPtr) utfGinfAlink2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfGinfAlink2);
          __cpp__.delete((void*) utfGinfAlink2);
        }
        this.m_utf_ginf_alink = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinfGlink1 = this.m_utf_ginf_glink;
      if ((IntPtr) utfGinfGlink1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfGinfGlink1);
        CriUtfMaker* utfGinfGlink2 = this.m_utf_ginf_glink;
        if ((IntPtr) utfGinfGlink2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfGinfGlink2);
          __cpp__.delete((void*) utfGinfGlink2);
        }
        this.m_utf_ginf_glink = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfGinf1 = this.m_utf_ginf;
      if ((IntPtr) utfGinf1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfGinf1);
        CriUtfMaker* utfGinf2 = this.m_utf_ginf;
        if ((IntPtr) utfGinf2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfGinf2);
          __cpp__.delete((void*) utfGinf2);
        }
        this.m_utf_ginf = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfEfinf1 = this.m_utf_efinf;
      if ((IntPtr) utfEfinf1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfEfinf1);
        CriUtfMaker* utfEfinf2 = this.m_utf_efinf;
        if ((IntPtr) utfEfinf2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfEfinf2);
          __cpp__.delete((void*) utfEfinf2);
        }
        this.m_utf_efinf = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfIdL1 = this.m_utf_id_l;
      if ((IntPtr) utfIdL1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfIdL1);
        CriUtfMaker* utfIdL2 = this.m_utf_id_l;
        if ((IntPtr) utfIdL2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfIdL2);
          __cpp__.delete((void*) utfIdL2);
        }
        this.m_utf_id_l = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfIdH1 = this.m_utf_id_h;
      if ((IntPtr) utfIdH1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfIdH1);
        CriUtfMaker* utfIdH2 = this.m_utf_id_h;
        if ((IntPtr) utfIdH2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfIdH2);
          __cpp__.delete((void*) utfIdH2);
        }
        this.m_utf_id_h = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfEid1 = this.m_utf_eid;
      if ((IntPtr) utfEid1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfEid1);
        CriUtfMaker* utfEid2 = this.m_utf_eid;
        if ((IntPtr) utfEid2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfEid2);
          __cpp__.delete((void*) utfEid2);
        }
        this.m_utf_eid = (CriUtfMaker*) 0;
      }
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if ((IntPtr) utfHeader1 != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfHeader1);
        CriUtfMaker* utfHeader2 = this.m_utf_header;
        if ((IntPtr) utfHeader2 != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfHeader2);
          __cpp__.delete((void*) utfHeader2);
        }
        this.m_utf_header = (CriUtfMaker*) 0;
      }
      this.ReleaseBinaryImage();
      _criheap_struct* heap = this.m_heap;
      if ((IntPtr) heap == IntPtr.Zero)
        return;
      __cpp__.criHeap_Destroy(heap);
      this.m_heap = (_criheap_struct*) 0;
      __cpp__.free(this.m_heap_memptr);
      this.m_heap_memptr = (void*) 0;
    }

    public unsafe void Initialize()
    {
      this.m_num_file = 0U;
      this.m_num_id_l = 0U;
      this.m_num_id_h = 0U;
      this.m_num_eid = 0U;
      this.m_cpk_datetime = 0UL;
      this.m_data_align = 2048U;
      this.m_total_packed_size = 0UL;
      this.m_total_contents_file_size = 0UL;
      this.m_crc_toc = 0U;
      this.m_crc_itoc = 0U;
      this.m_crc_gtoc = 0U;
      this.m_bin_header = (CriUtfBinary*) 0;
      this.m_bin_finf = (CriUtfBinary*) 0;
      this.m_bin_id = (CriUtfBinary*) 0;
      this.m_bin_id_l = (CriUtfBinary*) 0;
      this.m_bin_id_h = (CriUtfBinary*) 0;
      this.m_bin_eid = (CriUtfBinary*) 0;
    }

    public unsafe void ReleaseBinaryImage()
    {
      CriUtfBinary* binHeader = this.m_bin_header;
      if ((IntPtr) binHeader != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binHeader;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          __cpp__.free((void*) num);
        __cpp__.delete((void*) criUtfBinaryPtr);
        this.m_bin_header = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binFinf = this.m_bin_finf;
      if ((IntPtr) binFinf != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binFinf;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          __cpp__.free((void*) num);
        __cpp__.delete((void*) criUtfBinaryPtr);
        this.m_bin_finf = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binId = this.m_bin_id;
      if ((IntPtr) binId != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binId;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          __cpp__.free((void*) num);
        __cpp__.delete((void*) criUtfBinaryPtr);
        this.m_bin_id = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binIdL = this.m_bin_id_l;
      if ((IntPtr) binIdL != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binIdL;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          __cpp__.free((void*) num);
        __cpp__.delete((void*) criUtfBinaryPtr);
        this.m_bin_id_l = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binIdH = this.m_bin_id_h;
      if ((IntPtr) binIdH != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binIdH;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          __cpp__.free((void*) num);
        __cpp__.delete((void*) criUtfBinaryPtr);
        this.m_bin_id_h = (CriUtfBinary*) 0;
      }
      CriUtfBinary* binEid = this.m_bin_eid;
      if ((IntPtr) binEid == IntPtr.Zero)
        return;
      CriUtfBinary* criUtfBinaryPtr1 = binEid;
      uint num1 = (uint) *(int*) criUtfBinaryPtr1;
      if (num1 != 0U)
        __cpp__.free((void*) num1);
      __cpp__.delete((void*) criUtfBinaryPtr1);
      this.m_bin_eid = (CriUtfBinary*) 0;
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, ushort inval, uint recode)
    {
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utf, recode, __cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname), inval);
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, ulong inval, uint recode)
    {
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utf, recode, __cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname), inval);
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, int inval, uint recode)
    {
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utf, recode, __cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname), inval);
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, uint inval, uint recode)
    {
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utf, recode, __cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname), inval);
    }

    private unsafe void SetData(CriUtfMaker* utf, sbyte* fieldname, string inval, uint recode)
    {
      if (inval == (string) null)
        return;
      if (inval == "")
      {
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname) == uint.MaxValue)
          return;
        __cpp__.CriUtfMaker.SetData(utf, recode, __cpp__.CriUtfMaker.ConvFieldNameToNo(utf, fieldname), (sbyte*) &__cpp__.??_C@_00CNPNBAHC@?$AA@);
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
        __cpp__.CriUtfMaker.SetData<char *>(utf, recode, fieldname, numPtr1);
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
      uint crc32)
    {
      try
      {
        string inval = Path.GetDirectoryName(fullpath);
        string fileName = Path.GetFileName(fullpath);
        if (!this.m_enable_datetime)
          datetime = 0UL;
        if (!string.IsNullOrEmpty(inval))
          inval = inval.Replace("\\", "/");
        if (!string.IsNullOrEmpty(fullpath))
          fullpath = fullpath.Replace("\\", "/");
        CUtf cutf1 = this;
        cutf1.SetData(cutf1.m_utf_finf, (sbyte*) &__cpp__.??_C@_07FDBCPIKC@DirName?$AA@, inval, this.m_num_file);
        CUtf cutf2 = this;
        cutf2.SetData(cutf2.m_utf_finf, (sbyte*) &__cpp__.??_C@_08FDCJCDOL@FileName?$AA@, fileName, this.m_num_file);
        CUtf cutf3 = this;
        cutf3.SetData(cutf3.m_utf_finf, (sbyte*) &__cpp__.??_C@_08BHIEONMC@FileSize?$AA@, fsize, this.m_num_file);
        CUtf cutf4 = this;
        cutf4.SetData(cutf4.m_utf_finf, (sbyte*) &__cpp__.??_C@_0M@HCBECAL@ExtractSize?$AA@, (uint) extsize, this.m_num_file);
        CUtf cutf5 = this;
        cutf5.SetData(cutf5.m_utf_finf, (sbyte*) &__cpp__.??_C@_0L@KDHCLPBL@FileOffset?$AA@, offs, this.m_num_file);
        CUtf cutf6 = this;
        cutf6.SetData(cutf6.m_utf_finf, (sbyte*) &__cpp__.??_C@_02OLOABKKD@ID?$AA@, id, this.m_num_file);
        CUtf cutf7 = this;
        cutf7.SetData(cutf7.m_utf_finf, (sbyte*) &__cpp__.??_C@_0L@FHCJPPHJ@UserString?$AA@, usr, this.m_num_file);
        if (this.m_enable_file_crc)
        {
          CUtf cutf8 = this;
          cutf8.SetData(cutf8.m_utf_finf, (sbyte*) &__cpp__.??_C@_03ODHGDBCI@CRC?$AA@, crc32, this.m_num_file);
        }
        CUtf cutf9 = this;
        cutf9.SetData(cutf9.m_utf_efinf, (sbyte*) &__cpp__.??_C@_0P@HHMMOFOC@UpdateDateTime?$AA@, datetime, this.m_num_file);
        CUtf cutf10 = this;
        cutf10.SetData(cutf10.m_utf_efinf, (sbyte*) &__cpp__.??_C@_08EDNGJNCP@LocalDir?$AA@, inval, this.m_num_file);
        this.m_total_packed_size += extsize;
        this.m_total_contents_file_size += fsize;
      }
      catch (Exception ex)
      {
        return ulong.MaxValue;
      }
      return (ulong) ++this.m_num_file;
    }

    public unsafe ulong AddFileInfoId(
      ulong fsize,
      ulong extsize,
      uint id,
      ulong toc_index,
      uint crc32)
    {
      if (extsize > (ulong) ushort.MaxValue)
      {
        uint numIdH1 = this.m_num_id_h;
        CriUtfMaker* utfIdH1 = this.m_utf_id_h;
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdH1, (sbyte*) &__cpp__.??_C@_02OLOABKKD@ID?$AA@) != uint.MaxValue)
          __cpp__.CriUtfMaker.SetData(utfIdH1, numIdH1, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdH1, (sbyte*) &__cpp__.??_C@_02OLOABKKD@ID?$AA@), (ushort) id);
        uint numIdH2 = this.m_num_id_h;
        CriUtfMaker* utfIdH2 = this.m_utf_id_h;
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdH2, (sbyte*) &__cpp__.??_C@_08BHIEONMC@FileSize?$AA@) != uint.MaxValue)
          __cpp__.CriUtfMaker.SetData(utfIdH2, numIdH2, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdH2, (sbyte*) &__cpp__.??_C@_08BHIEONMC@FileSize?$AA@), (uint) fsize);
        uint numIdH3 = this.m_num_id_h;
        CriUtfMaker* utfIdH3 = this.m_utf_id_h;
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdH3, (sbyte*) &__cpp__.??_C@_0M@HCBECAL@ExtractSize?$AA@) != uint.MaxValue)
          __cpp__.CriUtfMaker.SetData(utfIdH3, numIdH3, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdH3, (sbyte*) &__cpp__.??_C@_0M@HCBECAL@ExtractSize?$AA@), (uint) extsize);
        if (this.m_enable_file_crc)
        {
          uint numIdH4 = this.m_num_id_h;
          CriUtfMaker* utfIdH4 = this.m_utf_id_h;
          if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdH4, (sbyte*) &__cpp__.??_C@_03ODHGDBCI@CRC?$AA@) != uint.MaxValue)
            __cpp__.CriUtfMaker.SetData(utfIdH4, numIdH4, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdH4, (sbyte*) &__cpp__.??_C@_03ODHGDBCI@CRC?$AA@), crc32);
        }
        ++this.m_num_id_h;
      }
      else
      {
        uint numIdL1 = this.m_num_id_l;
        CriUtfMaker* utfIdL1 = this.m_utf_id_l;
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdL1, (sbyte*) &__cpp__.??_C@_02OLOABKKD@ID?$AA@) != uint.MaxValue)
          __cpp__.CriUtfMaker.SetData(utfIdL1, numIdL1, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdL1, (sbyte*) &__cpp__.??_C@_02OLOABKKD@ID?$AA@), (ushort) id);
        uint numIdL2 = this.m_num_id_l;
        CriUtfMaker* utfIdL2 = this.m_utf_id_l;
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdL2, (sbyte*) &__cpp__.??_C@_08BHIEONMC@FileSize?$AA@) != uint.MaxValue)
          __cpp__.CriUtfMaker.SetData(utfIdL2, numIdL2, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdL2, (sbyte*) &__cpp__.??_C@_08BHIEONMC@FileSize?$AA@), (ushort) (uint) fsize);
        uint numIdL3 = this.m_num_id_l;
        CriUtfMaker* utfIdL3 = this.m_utf_id_l;
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdL3, (sbyte*) &__cpp__.??_C@_0M@HCBECAL@ExtractSize?$AA@) != uint.MaxValue)
          __cpp__.CriUtfMaker.SetData(utfIdL3, numIdL3, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdL3, (sbyte*) &__cpp__.??_C@_0M@HCBECAL@ExtractSize?$AA@), (ushort) (uint) extsize);
        if (this.m_enable_file_crc)
        {
          uint numIdL4 = this.m_num_id_l;
          CriUtfMaker* utfIdL4 = this.m_utf_id_l;
          if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdL4, (sbyte*) &__cpp__.??_C@_03ODHGDBCI@CRC?$AA@) != uint.MaxValue)
            __cpp__.CriUtfMaker.SetData(utfIdL4, numIdL4, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfIdL4, (sbyte*) &__cpp__.??_C@_03ODHGDBCI@CRC?$AA@), crc32);
        }
        ++this.m_num_id_l;
      }
      uint numEid1 = this.m_num_eid;
      CriUtfMaker* utfEid1 = this.m_utf_eid;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfEid1, (sbyte*) &__cpp__.??_C@_02OLOABKKD@ID?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfEid1, numEid1, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfEid1, (sbyte*) &__cpp__.??_C@_02OLOABKKD@ID?$AA@), (int) id);
      uint numEid2 = this.m_num_eid;
      CriUtfMaker* utfEid2 = this.m_utf_eid;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfEid2, (sbyte*) &__cpp__.??_C@_08JGDGBHKN@TocIndex?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfEid2, numEid2, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfEid2, (sbyte*) &__cpp__.??_C@_08JGDGBHKN@TocIndex?$AA@), (int) toc_index);
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

    public unsafe int GetTocImageSize() => (int) __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_finf) + 16;

    public void ResetTocFileInfo() => this.m_num_file = 0U;

    public unsafe void* MakeTocImage(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask, [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_finf, isCompress ? 1 : 0);
      int binarySize = (int) __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_finf);
      CriUtfBinary* binFinf1 = this.m_bin_finf;
      if ((IntPtr) binFinf1 != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binFinf1;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          __cpp__.free((void*) num);
        __cpp__.delete((void*) criUtfBinaryPtr);
      }
      CriUtfBinary* criUtfBinaryPtr1 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr2;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) criUtfBinaryPtr1 != IntPtr.Zero)
        {
          *(int*) criUtfBinaryPtr1 = (int) __cpp__.malloc((uint) binarySize);
          *(int*) ((IntPtr) criUtfBinaryPtr1 + 12) = binarySize;
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
        __cpp__.delete((void*) criUtfBinaryPtr1);
      }
      this.m_bin_finf = criUtfBinaryPtr2;
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_finf, criUtfBinaryPtr2);
      CriUtfBinary* binFinf2 = this.m_bin_finf;
      void* ptr = (void*) *(int*) binFinf2;
      uint length = (uint) *(int*) ((IntPtr) binFinf2 + 8);
      *size = length;
      if (this.m_enable_toc_crc)
      {
        uint num = CCrc32.CalcCrc32(3735928559U, length, ptr);
        CriUtfMaker* utfHeader = this.m_utf_header;
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_06CDNCOLO@TocCrc?$AA@) != uint.MaxValue)
          __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_06CDNCOLO@TocCrc?$AA@), num);
      }
      if (mask)
      {
        byte* numPtr1 = (byte*) ptr;
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
      ulong num5 = this.m_iff_toc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader1, (sbyte*) &__cpp__.??_C@_07MAFOODKE@TocSize?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader1, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader1, (sbyte*) &__cpp__.??_C@_07MAFOODKE@TocSize?$AA@), num5);
      return ptr;
    }

    public void ResetItocFileInfo()
    {
      this.m_num_eid = 0U;
      this.m_num_id_h = 0U;
      this.m_num_id_l = 0U;
    }

    public unsafe void ReleaseTocImageBuffer() => __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_finf);

    public unsafe void* MakeItocImageCompact(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask, [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_id_l, isCompress ? 1 : 0);
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_id_h, isCompress ? 1 : 0);
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_id, isCompress ? 1 : 0);
      uint binarySize1 = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_id_l);
      uint binarySize2 = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_id_h);
      CriUtfBinary* criUtfBinaryPtr1 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr2;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) criUtfBinaryPtr1 != IntPtr.Zero)
        {
          *(int*) criUtfBinaryPtr1 = (int) __cpp__.malloc(binarySize1);
          *(int*) ((IntPtr) criUtfBinaryPtr1 + 12) = (int) binarySize1;
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
        __cpp__.delete((void*) criUtfBinaryPtr1);
      }
      this.m_bin_id_l = criUtfBinaryPtr2;
      CriUtfBinary* criUtfBinaryPtr3 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr4;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) criUtfBinaryPtr3 != IntPtr.Zero)
        {
          *(int*) criUtfBinaryPtr3 = (int) __cpp__.malloc(binarySize2);
          *(int*) ((IntPtr) criUtfBinaryPtr3 + 12) = (int) binarySize2;
          *(int*) ((IntPtr) criUtfBinaryPtr3 + 16) = 0;
          *(int*) ((IntPtr) criUtfBinaryPtr3 + 8) = 0;
          *(int*) ((IntPtr) criUtfBinaryPtr3 + 4) = 0;
          criUtfBinaryPtr4 = criUtfBinaryPtr3;
        }
        else
          criUtfBinaryPtr4 = (CriUtfBinary*) 0;
      }
      __fault
      {
        __cpp__.delete((void*) criUtfBinaryPtr3);
      }
      this.m_bin_id_h = criUtfBinaryPtr4;
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_id_l, this.m_bin_id_l);
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_id_h, this.m_bin_id_h);
      void* voidPtr1 = (void*) *(int*) this.m_bin_id_l;
      void* voidPtr2 = (void*) *(int*) this.m_bin_id_h;
      uint numRecord1 = __cpp__.CriUtfMaker.GetNumRecord(this.m_utf_id_l);
      uint numRecord2 = __cpp__.CriUtfMaker.GetNumRecord(this.m_utf_id_h);
      CriUtfMaker* utfId1 = this.m_utf_id;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfId1, (sbyte*) &__cpp__.??_C@_06DAPCHFCN@FilesL?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfId1, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfId1, (sbyte*) &__cpp__.??_C@_06DAPCHFCN@FilesL?$AA@), numRecord1);
      CriUtfMaker* utfId2 = this.m_utf_id;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfId2, (sbyte*) &__cpp__.??_C@_06FEJOLACJ@FilesH?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfId2, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfId2, (sbyte*) &__cpp__.??_C@_06FEJOLACJ@FilesH?$AA@), numRecord2);
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
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfId3, (sbyte*) &__cpp__.??_C@_05IPCCNOAO@DataL?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfId3, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfId3, (sbyte*) &__cpp__.??_C@_05IPCCNOAO@DataL?$AA@), utfCellTypeVldTag3);
      CriUtfCellTypeVldTag utfCellTypeVldTag4 = utfCellTypeVldTag2;
      CriUtfMaker* utfId4 = this.m_utf_id;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfId4, (sbyte*) &__cpp__.??_C@_05OLEOBLAK@DataH?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfId4, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfId4, (sbyte*) &__cpp__.??_C@_05OLEOBLAK@DataH?$AA@), utfCellTypeVldTag4);
      *size = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_id);
      CriUtfBinary* criUtfBinaryPtr5 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr6;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) criUtfBinaryPtr5 != IntPtr.Zero)
        {
          uint num = *size;
          *(int*) criUtfBinaryPtr5 = (int) __cpp__.malloc(num);
          *(int*) ((IntPtr) criUtfBinaryPtr5 + 12) = (int) num;
          *(int*) ((IntPtr) criUtfBinaryPtr5 + 16) = 0;
          *(int*) ((IntPtr) criUtfBinaryPtr5 + 8) = 0;
          *(int*) ((IntPtr) criUtfBinaryPtr5 + 4) = 0;
          criUtfBinaryPtr6 = criUtfBinaryPtr5;
        }
        else
          criUtfBinaryPtr6 = (CriUtfBinary*) 0;
      }
      __fault
      {
        __cpp__.delete((void*) criUtfBinaryPtr5);
      }
      this.m_bin_id = criUtfBinaryPtr6;
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_id, criUtfBinaryPtr6);
      void* voidPtr3 = (void*) *(int*) this.m_bin_id;
      if (this.m_enable_toc_crc)
      {
        uint inval = CCrc32.CalcCrc32(3203391149U, *size, voidPtr3);
        CUtf cutf = this;
        cutf.SetData(cutf.m_utf_header, (sbyte*) &__cpp__.??_C@_07DBMMOPD@ItocCrc?$AA@, inval, 0U);
      }
      this.IfMaskEnableThenUnMask(voidPtr3, *size, mask);
      this.m_iff_itoc.ChunkSize = (ulong) *size;
      CUtf cutf1 = this;
      cutf1.SetData(cutf1.m_utf_header, (sbyte*) &__cpp__.??_C@_08MIDCPPGJ@ItocSize?$AA@, this.m_iff_itoc.ChunkSize + 16UL, 0U);
      CUtf cutf2 = this;
      cutf2.SetData(cutf2.m_utf_header, (sbyte*) &__cpp__.??_C@_03JJCPLEKC@EID?$AA@, 0, 0U);
      return voidPtr3;
    }

    public unsafe void ReleaseITocImageCompactBuffer()
    {
      __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_id_l);
      __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_id_h);
      __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_id);
    }

    public unsafe void* MakeItocImageExtra(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask, [MarshalAs(UnmanagedType.U1)] bool isCompress)
    {
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_eid, isCompress ? 1 : 0);
      *size = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_eid);
      if ((IntPtr) this.m_bin_eid != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_eid);
        CriUtfMaker* utfEid = this.m_utf_eid;
        if ((IntPtr) utfEid != IntPtr.Zero)
        {
          __cpp__.CriUtfMaker.{dtor}(utfEid);
          __cpp__.delete((void*) utfEid);
        }
      }
      CriUtfBinary* criUtfBinaryPtr1 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr2;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) criUtfBinaryPtr1 != IntPtr.Zero)
        {
          uint num = *size;
          *(int*) criUtfBinaryPtr1 = (int) __cpp__.malloc(num);
          *(int*) ((IntPtr) criUtfBinaryPtr1 + 12) = (int) num;
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
        __cpp__.delete((void*) criUtfBinaryPtr1);
      }
      this.m_bin_eid = criUtfBinaryPtr2;
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_eid, criUtfBinaryPtr2);
      void* ptr = (void*) *(int*) this.m_bin_eid;
      if (this.m_enable_toc_crc)
      {
        uint num = CCrc32.CalcCrc32(3203391149U, *size, ptr);
        CriUtfMaker* utfHeader = this.m_utf_header;
        if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_07DBMMOPD@ItocCrc?$AA@) != uint.MaxValue)
          __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_07DBMMOPD@ItocCrc?$AA@), num);
      }
      if (mask)
      {
        byte* numPtr1 = (byte*) ptr;
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
      ulong num5 = this.m_iff_itoc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader1, (sbyte*) &__cpp__.??_C@_08MIDCPPGJ@ItocSize?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader1, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader1, (sbyte*) &__cpp__.??_C@_08MIDCPPGJ@ItocSize?$AA@), num5);
      CriUtfMaker* utfHeader2 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader2, (sbyte*) &__cpp__.??_C@_03JJCPLEKC@EID?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader2, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader2, (sbyte*) &__cpp__.??_C@_03JJCPLEKC@EID?$AA@), 1);
      return ptr;
    }

    public unsafe void ReleaseITocImageExtraBuffer()
    {
      __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_eid);
      CriUtfMaker* utfEid = this.m_utf_eid;
      if ((IntPtr) utfEid != IntPtr.Zero)
      {
        __cpp__.CriUtfMaker.{dtor}(utfEid);
        __cpp__.delete((void*) utfEid);
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
        cutf1.SetData(cutf1.m_utf_ginf_attr, (sbyte*) &__cpp__.??_C@_05HHCEBJEO@Aname?$AA@, key, (uint) cattrInfo.Index);
        CUtf cutf2 = this;
        cutf2.SetData(cutf2.m_utf_ginf_attr, (sbyte*) &__cpp__.??_C@_05CGDDDONM@Align?$AA@, cattrInfo.Alignment, (uint) cattrInfo.Index);
        CUtf cutf3 = this;
        cutf3.SetData(cutf3.m_utf_ginf_attr, (sbyte*) &__cpp__.??_C@_05DIOAMJFE@Files?$AA@, 0U, (uint) cattrInfo.Index);
        CUtf cutf4 = this;
        cutf4.SetData(cutf4.m_utf_ginf_attr, (sbyte*) &__cpp__.??_C@_08BHIEONMC@FileSize?$AA@, 0U, (uint) cattrInfo.Index);
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
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_ginf_glink, isCompress ? 1 : 0);
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_ginf_flink, isCompress ? 1 : 0);
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_ginf_attr, isCompress ? 1 : 0);
      __cpp__.CriUtfMaker.SetCompressMode(this.m_utf_ginf, isCompress ? 1 : 0);
      this.m_flinks = 0;
      this.m_attrs = 0U;
      this.m_groups = 0U;
      gmanager.Enumerate(new CGroupingManager.EnumerateFunction(this.Enumerate), (object) null);
      CUtf cutf1 = this;
      cutf1.m_attrs = (uint) cutf1.SetAttributeUtfInfo(gmanager);
      uint binarySize1 = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_ginf_glink);
      uint binarySize2 = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_ginf_flink);
      uint binarySize3 = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_ginf_attr);
      CriUtfBinary* binGlink = this.m_bin_glink;
      if ((IntPtr) binGlink != IntPtr.Zero)
        __cpp__.CriUtfBinary.__delDtor(binGlink, 1U);
      CriUtfBinary* binFlink = this.m_bin_flink;
      if ((IntPtr) binFlink != IntPtr.Zero)
        __cpp__.CriUtfBinary.__delDtor(binFlink, 1U);
      CriUtfBinary* binAttr = this.m_bin_attr;
      if ((IntPtr) binAttr != IntPtr.Zero)
        __cpp__.CriUtfBinary.__delDtor(binAttr, 1U);
      CriUtfBinary* criUtfBinaryPtr1 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr2;
      // ISSUE: fault handler
      try
      {
        criUtfBinaryPtr2 = (IntPtr) criUtfBinaryPtr1 == IntPtr.Zero ? (CriUtfBinary*) 0 : __cpp__.CriUtfBinary.{ctor}(criUtfBinaryPtr1, binarySize1);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfBinaryPtr1);
      }
      this.m_bin_glink = criUtfBinaryPtr2;
      CriUtfBinary* criUtfBinaryPtr3 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr4;
      // ISSUE: fault handler
      try
      {
        criUtfBinaryPtr4 = (IntPtr) criUtfBinaryPtr3 == IntPtr.Zero ? (CriUtfBinary*) 0 : __cpp__.CriUtfBinary.{ctor}(criUtfBinaryPtr3, binarySize2);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfBinaryPtr3);
      }
      this.m_bin_flink = criUtfBinaryPtr4;
      CriUtfBinary* criUtfBinaryPtr5 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr6;
      // ISSUE: fault handler
      try
      {
        criUtfBinaryPtr6 = (IntPtr) criUtfBinaryPtr5 == IntPtr.Zero ? (CriUtfBinary*) 0 : __cpp__.CriUtfBinary.{ctor}(criUtfBinaryPtr5, binarySize3);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfBinaryPtr5);
      }
      this.m_bin_attr = criUtfBinaryPtr6;
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_ginf_glink, this.m_bin_glink);
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_ginf_flink, this.m_bin_flink);
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_ginf_attr, this.m_bin_attr);
      void* voidPtr1 = (void*) *(int*) this.m_bin_glink;
      void* voidPtr2 = (void*) *(int*) this.m_bin_flink;
      void* voidPtr3 = (void*) *(int*) this.m_bin_attr;
      uint numRecord1 = __cpp__.CriUtfMaker.GetNumRecord(this.m_utf_ginf_glink);
      uint numRecord2 = __cpp__.CriUtfMaker.GetNumRecord(this.m_utf_ginf_flink);
      uint numRecord3 = __cpp__.CriUtfMaker.GetNumRecord(this.m_utf_ginf_attr);
      CUtf cutf2 = this;
      cutf2.SetData(cutf2.m_utf_ginf, (sbyte*) &__cpp__.??_C@_05ICMMBCAL@Glink?$AA@, numRecord1, 0U);
      CUtf cutf3 = this;
      cutf3.SetData(cutf3.m_utf_ginf, (sbyte*) &__cpp__.??_C@_05EJJAMBKO@Flink?$AA@, numRecord2, 0U);
      CUtf cutf4 = this;
      cutf4.SetData(cutf4.m_utf_ginf, (sbyte*) &__cpp__.??_C@_04PGOCJFCI@Attr?$AA@, numRecord3, 0U);
      CriUtfCellTypeVldTag val1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref val1 = (int) voidPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &val1 + 4) = (int) binarySize1;
      CriUtfCellTypeVldTag val2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref val2 = (int) voidPtr2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &val2 + 4) = (int) binarySize2;
      CriUtfCellTypeVldTag val3;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref val3 = (int) voidPtr3;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &val3 + 4) = (int) binarySize3;
      __cpp__.CriUtfMaker.SetData<struct CriUtfCellTypeVldTag>(this.m_utf_ginf, 0U, (sbyte*) &__cpp__.??_C@_05JMFGLPAJ@Gdata?$AA@, val1);
      __cpp__.CriUtfMaker.SetData<struct CriUtfCellTypeVldTag>(this.m_utf_ginf, 0U, (sbyte*) &__cpp__.??_C@_05FHAKGMKM@Fdata?$AA@, val2);
      __cpp__.CriUtfMaker.SetData<struct CriUtfCellTypeVldTag>(this.m_utf_ginf, 0U, (sbyte*) &__cpp__.??_C@_08KHDPMPAI@AttrData?$AA@, val3);
      CriUtfMaker* utfGinfGinf = this.m_utf_ginf_ginf;
      if ((IntPtr) utfGinfGinf != IntPtr.Zero)
      {
        int num = isCompress ? 1 : 0;
        __cpp__.CriUtfMaker.SetCompressMode(utfGinfGinf, num);
        uint binarySize4 = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_ginf_ginf);
        CriUtfBinary* binGinf = this.m_bin_ginf;
        if ((IntPtr) binGinf != IntPtr.Zero)
          __cpp__.CriUtfBinary.__delDtor(binGinf, 1U);
        CriUtfBinary* criUtfBinaryPtr7 = (CriUtfBinary*) __cpp__.@new(20U);
        CriUtfBinary* criUtfBinaryPtr8;
        // ISSUE: fault handler
        try
        {
          criUtfBinaryPtr8 = (IntPtr) criUtfBinaryPtr7 == IntPtr.Zero ? (CriUtfBinary*) 0 : __cpp__.CriUtfBinary.{ctor}(criUtfBinaryPtr7, binarySize4);
        }
        __fault
        {
          __cpp__.delete((void*) criUtfBinaryPtr7);
        }
        this.m_bin_ginf = criUtfBinaryPtr8;
        __cpp__.CriUtfMaker.MakeBinary(this.m_utf_ginf_ginf, criUtfBinaryPtr8);
        void* voidPtr4 = (void*) *(int*) this.m_bin_ginf;
        uint numRecord4 = __cpp__.CriUtfMaker.GetNumRecord(this.m_utf_ginf_ginf);
        CUtf cutf5 = this;
        cutf5.SetData(cutf5.m_utf_ginf, (sbyte*) &__cpp__.??_C@_04OEMOBODJ@Ginf?$AA@, numRecord4, 0U);
        CriUtfCellTypeVldTag val4;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref val4 = (int) voidPtr4;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &val4 + 4) = (int) binarySize4;
        __cpp__.CriUtfMaker.SetData<struct CriUtfCellTypeVldTag>(this.m_utf_ginf, 0U, (sbyte*) &__cpp__.??_C@_08GIHCBBHF@GinfData?$AA@, val4);
      }
      *size = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_ginf);
      CriUtfBinary* criUtfBinaryPtr9 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr10;
      // ISSUE: fault handler
      try
      {
        criUtfBinaryPtr10 = (IntPtr) criUtfBinaryPtr9 == IntPtr.Zero ? (CriUtfBinary*) 0 : __cpp__.CriUtfBinary.{ctor}(criUtfBinaryPtr9, *size);
      }
      __fault
      {
        __cpp__.delete((void*) criUtfBinaryPtr9);
      }
      this.m_bin_id = criUtfBinaryPtr10;
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_ginf, criUtfBinaryPtr10);
      void* voidPtr5 = (void*) *(int*) this.m_bin_id;
      if (this.m_enable_toc_crc)
      {
        uint inval = CCrc32.CalcCrc32(12513024U, *size, voidPtr5);
        CUtf cutf6 = this;
        cutf6.SetData(cutf6.m_utf_header, (sbyte*) &__cpp__.??_C@_07BGJGNMMB@GtocCrc?$AA@, inval, 0U);
      }
      this.IfMaskEnableThenUnMask(voidPtr5, *size, mask);
      this.m_iff_gtoc.ChunkSize = (ulong) *size;
      ulong num1 = this.m_iff_gtoc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_08PACEPL@GtocSize?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_08PACEPL@GtocSize?$AA@), num1);
      return voidPtr5;
    }

    public unsafe void ResetGtocFileInfo()
    {
      __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_ginf_glink);
      __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_ginf_flink);
      __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_ginf_attr);
      CriUtfMaker* utfGinfGinf = this.m_utf_ginf_ginf;
      if ((IntPtr) utfGinfGinf != IntPtr.Zero)
        __cpp__.CriUtfMaker.ReleaseBinaryBuffer(utfGinfGinf);
      __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_ginf);
    }

    public void ReleaseGtocImageBuffer() => this.ResetGtocFileInfo();

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
          cutf1.SetData(cutf1.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_06JEKIFDJD@Aindex?$AA@, (ushort) attr.AttrIndex, (uint) recode);
          CUtf cutf2 = this;
          cutf2.SetData(cutf2.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@, recode + 1, (uint) recode);
          CUtf cutf3 = this;
          cutf3.SetData(cutf3.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_05GIMBDOHM@Child?$AA@, -this.m_flinks, (uint) recode);
          CUtf cutf4 = this;
          cutf4.SetData(cutf4.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_09DMHDBJJA@SortFlink?$AA@, fileInfoLink.FileCount, (uint) recode);
          int index2 = 0;
          if (0 < fileInfoLink.ContinuousFiles)
          {
            do
            {
              CFileInfo fileListContinuou = fileInfoLink.FileListContinuous[index2];
              CUtf cutf5 = this;
              cutf5.SetData(cutf5.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_06JEKIFDJD@Aindex?$AA@, (ushort) attr.AttrIndex, (uint) this.m_flinks);
              int flinks2 = this.m_flinks;
              CUtf cutf6 = this;
              cutf6.SetData(cutf6.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@, flinks2 + 1, (uint) flinks2);
              CUtf cutf7 = this;
              cutf7.SetData(cutf7.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_05GIMBDOHM@Child?$AA@, (int) fileListContinuou.InfoIndex, (uint) this.m_flinks);
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
              cutf8.SetData(cutf8.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_06JEKIFDJD@Aindex?$AA@, (ushort) attr.AttrIndex, (uint) this.m_flinks);
              int flinks3 = this.m_flinks;
              CUtf cutf9 = this;
              cutf9.SetData(cutf9.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@, flinks3 + 1, (uint) flinks3);
              CUtf cutf10 = this;
              cutf10.SetData(cutf10.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_05GIMBDOHM@Child?$AA@, (int) fileListUncontinuou.InfoIndex, (uint) this.m_flinks);
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
              cutf11.SetData(cutf11.m_utf_ginf_flink, (sbyte*) &__cpp__.??_C@_09DMHDBJJA@SortFlink?$AA@, flist[index5].SortedLink, (uint) (index5 + num1));
              ++index5;
            }
            while (index5 < flist.Count);
          }
          num1 = flist.Count + num1;
          uint num3 = (uint) (this.m_flinks - 1);
          CriUtfMaker* utfGinfFlink = this.m_utf_ginf_flink;
          if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfFlink, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@) != uint.MaxValue)
            __cpp__.CriUtfMaker.SetData(utfGinfFlink, num3, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfFlink, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@), num2);
          ++index1;
          --num2;
          ++recode;
        }
        while (index1 < attrList.Count);
      }
      int num4 = -(int) cgroup.Index;
      CriUtfMaker* utfGinfFlink1 = this.m_utf_ginf_flink;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfFlink1, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfGinfFlink1, (uint) (flinks1 + index1 - 1), __cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfFlink1, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@), num4);
    }

    private unsafe void Enumerate(object obj, object userObject)
    {
      if (!(obj is CGroup cgroup))
        return;
      CUtf cutf = this;
      cutf.SetData(cutf.m_utf_ginf_glink, (sbyte*) &__cpp__.??_C@_05KBHNPKFD@Gname?$AA@, cgroup.GroupName, cgroup.Index);
      uint index1 = cgroup.Index;
      int next = cgroup.Next;
      CriUtfMaker* utfGinfGlink1 = this.m_utf_ginf_glink;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfGlink1, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfGinfGlink1, index1, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfGlink1, (sbyte*) &__cpp__.??_C@_04MDFHGMOB@Next?$AA@), next);
      uint index2 = cgroup.Index;
      int child = cgroup.Child;
      CriUtfMaker* utfGinfGlink2 = this.m_utf_ginf_glink;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfGlink2, (sbyte*) &__cpp__.??_C@_05GIMBDOHM@Child?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfGinfGlink2, index2, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfGlink2, (sbyte*) &__cpp__.??_C@_05GIMBDOHM@Child?$AA@), child);
      if (cgroup.AttributeList == null)
        return;
      ++this.m_groups;
      this.SetToUtfFileInfos(cgroup.AttributeList);
    }

    public unsafe void* MakeEtocImage(uint* size, string basedir, [MarshalAs(UnmanagedType.U1)] bool mask)
    {
      uint numFile = this.m_num_file;
      CriUtfMaker* utfEfinf = this.m_utf_efinf;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfEfinf, (sbyte*) &__cpp__.??_C@_0P@HHMMOFOC@UpdateDateTime?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfEfinf, numFile, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfEfinf, (sbyte*) &__cpp__.??_C@_0P@HHMMOFOC@UpdateDateTime?$AA@), 0UL);
      CUtf cutf = this;
      cutf.SetData(cutf.m_utf_efinf, (sbyte*) &__cpp__.??_C@_08EDNGJNCP@LocalDir?$AA@, basedir, this.m_num_file);
      *size = __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_efinf);
      CriUtfBinary* binId = this.m_bin_id;
      if ((IntPtr) binId != IntPtr.Zero)
      {
        CriUtfBinary* criUtfBinaryPtr = binId;
        uint num = (uint) *(int*) criUtfBinaryPtr;
        if (num != 0U)
          __cpp__.free((void*) num);
        __cpp__.delete((void*) criUtfBinaryPtr);
      }
      CriUtfBinary* criUtfBinaryPtr1 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr2;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) criUtfBinaryPtr1 != IntPtr.Zero)
        {
          uint num = *size;
          *(int*) criUtfBinaryPtr1 = (int) __cpp__.malloc(num);
          *(int*) ((IntPtr) criUtfBinaryPtr1 + 12) = (int) num;
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
        __cpp__.delete((void*) criUtfBinaryPtr1);
      }
      this.m_bin_id = criUtfBinaryPtr2;
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_efinf, criUtfBinaryPtr2);
      void* voidPtr = (void*) *(int*) this.m_bin_id;
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
      ulong num5 = this.m_iff_etoc.ChunkSize + 16UL;
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_08COAGAMHN@EtocSize?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_08COAGAMHN@EtocSize?$AA@), num5);
      return voidPtr;
    }

    public unsafe void* MakeHeaderImage(uint* size, [MarshalAs(UnmanagedType.U1)] bool mask)
    {
      ulong cpkDatetime = this.m_cpk_datetime;
      CriUtfMaker* utfHeader1 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader1, (sbyte*) &__cpp__.??_C@_0P@HHMMOFOC@UpdateDateTime?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader1, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader1, (sbyte*) &__cpp__.??_C@_0P@HHMMOFOC@UpdateDateTime?$AA@), cpkDatetime);
      uint numFile = this.m_num_file;
      CriUtfMaker* utfHeader2 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader2, (sbyte*) &__cpp__.??_C@_05DIOAMJFE@Files?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader2, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader2, (sbyte*) &__cpp__.??_C@_05DIOAMJFE@Files?$AA@), numFile);
      CriUtfMaker* utfHeader3 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader3, (sbyte*) &__cpp__.??_C@_07NGFJPNPN@Version?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader3, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader3, (sbyte*) &__cpp__.??_C@_07NGFJPNPN@Version?$AA@), (ushort) 7);
      CriUtfMaker* utfHeader4 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader4, (sbyte*) &__cpp__.??_C@_08DNLDHPFP@Revision?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader4, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader4, (sbyte*) &__cpp__.??_C@_08DNLDHPFP@Revision?$AA@), (ushort) 2);
      ushort dataAlign = (ushort) this.m_data_align;
      CriUtfMaker* utfHeader5 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader5, (sbyte*) &__cpp__.??_C@_05CGDDDONM@Align?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader5, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader5, (sbyte*) &__cpp__.??_C@_05CGDDDONM@Align?$AA@), dataAlign);
      ulong contentsFileSize = this.m_total_contents_file_size;
      CriUtfMaker* utfHeader6 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader6, (sbyte*) &__cpp__.??_C@_0BA@HPEFNJHM@EnabledDataSize?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader6, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader6, (sbyte*) &__cpp__.??_C@_0BA@HPEFNJHM@EnabledDataSize?$AA@), contentsFileSize);
      ulong totalPackedSize = this.m_total_packed_size;
      CriUtfMaker* utfHeader7 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader7, (sbyte*) &__cpp__.??_C@_0BC@ILANKEOJ@EnabledPackedSize?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader7, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader7, (sbyte*) &__cpp__.??_C@_0BC@ILANKEOJ@EnabledPackedSize?$AA@), totalPackedSize);
      CUtf cutf1 = this;
      cutf1.SetData(cutf1.m_utf_header, (sbyte*) &__cpp__.??_C@_07JABLCLAF@Comment?$AA@, this.m_comment, 0U);
      CUtf cutf2 = this;
      cutf2.SetData(cutf2.m_utf_header, (sbyte*) &__cpp__.??_C@_05PACPJAOI@Tvers?$AA@, this.m_tool_version, 0U);
      uint groups = this.m_groups;
      CriUtfMaker* utfHeader8 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader8, (sbyte*) &__cpp__.??_C@_06LCLFDFOL@Groups?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader8, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader8, (sbyte*) &__cpp__.??_C@_06LCLFDFOL@Groups?$AA@), groups);
      uint attrs = this.m_attrs;
      CriUtfMaker* utfHeader9 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader9, (sbyte*) &__cpp__.??_C@_05DBFBGAFK@Attrs?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader9, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader9, (sbyte*) &__cpp__.??_C@_05DBFBGAFK@Attrs?$AA@), attrs);
      uint cpkMode = this.m_cpk_mode;
      CriUtfMaker* utfHeader10 = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader10, (sbyte*) &__cpp__.??_C@_07CHOGENJJ@CpkMode?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfHeader10, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader10, (sbyte*) &__cpp__.??_C@_07CHOGENJJ@CpkMode?$AA@), cpkMode);
      int binarySize = (int) __cpp__.CriUtfMaker.GetBinarySize(this.m_utf_header);
      CriUtfBinary* binHeader1 = this.m_bin_header;
      if ((IntPtr) binHeader1 != IntPtr.Zero)
        __cpp__.CriUtfBinary.__delDtor(binHeader1, 1U);
      CriUtfBinary* criUtfBinaryPtr1 = (CriUtfBinary*) __cpp__.@new(20U);
      CriUtfBinary* criUtfBinaryPtr2;
      // ISSUE: fault handler
      try
      {
        criUtfBinaryPtr2 = (IntPtr) criUtfBinaryPtr1 == IntPtr.Zero ? (CriUtfBinary*) 0 : __cpp__.CriUtfBinary.{ctor}(criUtfBinaryPtr1, (uint) (binarySize + 4096));
      }
      __fault
      {
        __cpp__.delete((void*) criUtfBinaryPtr1);
      }
      this.m_bin_header = criUtfBinaryPtr2;
      __cpp__.CriUtfMaker.MakeBinary(this.m_utf_header, criUtfBinaryPtr2);
      CriUtfBinary* binHeader2 = this.m_bin_header;
      void* ptr = (void*) *(int*) binHeader2;
      uint length = (uint) *(int*) ((IntPtr) binHeader2 + 8);
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

    public unsafe void ReleaseEtocImageBuffer() => __cpp__.CriUtfMaker.ReleaseBinaryBuffer(this.m_utf_efinf);

    private static void stamp_cri(uint hsize, void* ptr)
    {
      // ISSUE: unable to decompile the method.
    }

    public unsafe void* GetHeaderIffPtr() => this.m_iff_header.GetFourCCPointer();

    public unsafe void* GetTocIffPtr() => this.m_iff_toc.GetFourCCPointer();

    public unsafe void* GetItocIffPtr() => this.m_iff_itoc.GetFourCCPointer();

    public unsafe void* GetGtocIffPtr() => this.m_iff_gtoc.GetFourCCPointer();

    public unsafe void* GetEtocIffPtr() => this.m_iff_etoc.GetFourCCPointer();

    public uint GetIffSize() => 16;

    public unsafe void SetContentsFileOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0O@DIKLBOAH@ContentOffset?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0O@DIKLBOAH@ContentOffset?$AA@), ofs);
    }

    public unsafe void SetContentsFileSize(ulong size)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0M@MJNKDBDC@ContentSize?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0M@MJNKDBDC@ContentSize?$AA@), size);
    }

    public unsafe void SetTocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_09HHGNIJOH@TocOffset?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_09HHGNIJOH@TocOffset?$AA@), ofs);
    }

    public unsafe void SetEtocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0L@BHCCOGAA@EtocOffset?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0L@BHCCOGAA@EtocOffset?$AA@), ofs);
    }

    public unsafe void SetGtocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0L@EPEOFPMB@GtocOffset?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0L@EPEOFPMB@GtocOffset?$AA@), ofs);
    }

    public unsafe void SetItocImageOffset(ulong ofs)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0L@BNDIHEMH@ItocOffset?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_0L@BNDIHEMH@ItocOffset?$AA@), ofs);
    }

    public unsafe void SetMaxDpkItoc(int maxdpksize)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_07FLEHKLJ@DpkItoc?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_07FLEHKLJ@DpkItoc?$AA@), maxdpksize);
    }

    public unsafe void SetCodec(int codec)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_05GIBLJHPJ@Codec?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_05GIBLJHPJ@Codec?$AA@), codec);
    }

    public unsafe void SetSortedFilenameFlag([MarshalAs(UnmanagedType.U1)] bool sw)
    {
      ushort num = (ushort) sw;
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_06KBMOENCI@Sorted?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_06KBMOENCI@Sorted?$AA@), num);
    }

    public void SetMaskFlag([MarshalAs(UnmanagedType.U1)] bool mask)
    {
      this.m_iff_header.SetMaskFlag(mask);
      this.m_iff_toc.SetMaskFlag(mask);
      this.m_iff_etoc.SetMaskFlag(mask);
      this.m_iff_itoc.SetMaskFlag(mask);
      this.m_iff_gtoc.SetMaskFlag(mask);
    }

    public unsafe void SetUpdates(uint updates)
    {
      CriUtfMaker* utfHeader = this.m_utf_header;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_07PCBHAKJL@Updates?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfHeader, 0U, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfHeader, (sbyte*) &__cpp__.??_C@_07PCBHAKJL@Updates?$AA@), updates);
    }

    public unsafe void SetGroupGinf(string gaName, int size, int files, int records)
    {
      CUtf cutf = this;
      cutf.SetData(cutf.m_utf_ginf_ginf, (sbyte*) &__cpp__.??_C@_06NKCGCMGE@GAname?$AA@, gaName, (uint) records);
      CriUtfMaker* utfGinfGinf1 = this.m_utf_ginf_ginf;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfGinf1, (sbyte*) &__cpp__.??_C@_08BHIEONMC@FileSize?$AA@) != uint.MaxValue)
        __cpp__.CriUtfMaker.SetData(utfGinfGinf1, (uint) records, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfGinf1, (sbyte*) &__cpp__.??_C@_08BHIEONMC@FileSize?$AA@), size);
      CriUtfMaker* utfGinfGinf2 = this.m_utf_ginf_ginf;
      if (__cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfGinf2, (sbyte*) &__cpp__.??_C@_05DIOAMJFE@Files?$AA@) == uint.MaxValue)
        return;
      __cpp__.CriUtfMaker.SetData(utfGinfGinf2, (uint) records, __cpp__.CriUtfMaker.ConvFieldNameToNo(utfGinfGinf2, (sbyte*) &__cpp__.??_C@_05DIOAMJFE@Files?$AA@), files);
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.~CUtf();
      }
      else
      {
        try
        {
          this.!CUtf();
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
  }
}
