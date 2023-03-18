// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CBinary
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

using \u003CCppImplementationDetails\u003E;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CBinary : IDisposable
  {
    private CFileData m_filedata;
    private Dictionary<string, List<CFileInfo>> m_unification_list;
    private CUtf m_utf;
    private Status m_stat;
    private ulong m_exec_file_cur;
    private CAsyncFile m_writer;
    private CAsyncFile m_reader;
    private CAsyncFile m_copier;
    private string m_basedir;
    private CBinary.ExecMode m_exec_mode;
    private string m_extract_path;
    private uint m_data_align;
    private bool m_force_compress;
    private bool m_enable_toc;
    private bool m_enable_htoc_fpath_to_toc;
    private bool m_enable_htoc_fid_to_toc;
    private bool m_enable_etoc;
    private bool m_enable_itoc;
    private bool m_enable_gtoc;
    private bool m_enable_hgtoc_gname_to_glink;
    private bool m_enable_hgtoc_ganame_to_ginf;
    private bool m_enable_hgtoc_gfpath_to_gfinf;
    private bool m_enable_hgtoc_gfid_to_gfinf;
    private bool m_is_toc_rtv_setup;
    private bool m_is_gtoc_rtv_setup;
    private unsafe byte* m_work_hash_fpath_to_toc;
    private unsafe byte* m_work_hash_fid_to_toc;
    private unsafe byte* m_work_hash_gname_to_glink;
    private unsafe byte* m_work_hash_ganame_to_ginf;
    private unsafe byte** m_work_hash_gfpath_to_gfinf;
    private unsafe byte** m_work_hash_gfid_to_gfinf;
    private int m_hash_array_nelem;
    private unsafe _CriCbnRtv16* m_toc_rtvobj;
    private unsafe _CriCbnRtv* m_toc_rtv;
    private uint m_gtoc_glink_num;
    private uint m_gtoc_flink_num;
    private uint m_gtoc_attr_num;
    private uint m_gtoc_ginf_num;
    private unsafe _CriCbnRtv16* m_gtoc_glink_rtvobj;
    private unsafe _CriCbnRtv* m_gtoc_glink_rtv;
    private unsafe _CriCbnRtv16* m_gtoc_flink_rtvobj;
    private unsafe _CriCbnRtv* m_gtoc_flink_rtv;
    private unsafe _CriCbnRtv16* m_gtoc_attr_rtvobj;
    private unsafe _CriCbnRtv* m_gtoc_attr_rtv;
    private unsafe _CriCbnRtv16* m_gtoc_ginf_rtvobj;
    private unsafe _CriCbnRtv* m_gtoc_ginf_rtv;
    private bool m_mask;
    private int m_ver;
    private int m_rev;
    private ulong m_cpk_datetime;
    private ulong m_toc_size;
    private ulong m_htoc_size;
    private ulong m_etoc_size;
    private ulong m_itoc_size;
    private ulong m_gtoc_size;
    private ulong m_hgtoc_size;
    private ulong m_toc_offset;
    private ulong m_htoc_offset;
    private ulong m_itoc_offset;
    private ulong m_gtoc_offset;
    private ulong m_hgtoc_offset;
    private ulong m_etoc_offset;
    private ulong m_contents_offset;
    private ulong m_contents_size;
    private uint m_updates;
    private string m_working_message;
    private string m_error_message;
    private bool m_sorted_filename;
    private bool m_enable_datetime_info;
    private CExternalBuffer m_external_buffer;
    private bool m_analyzed;
    private long m_cpk_file_size;
    private bool m_is_uncomp_file;
    private EnumUnificationMode m_unification_mode;
    private bool m_duplicate_by_group;
    private bool m_enable_id_and_group;
    private string[] m_uncomp_ext_list;
    private string[] m_image_ext_list;
    private bool m_enable_mself;
    private long m_pre_header_offset;
    private int m_self_count;
    private uint m_cpk_mode;
    private bool m_enable_filename;
    private bool m_is_extra_id;
    private bool m_enable_crc;
    private bool m_enable_crc32;
    private bool m_enable_checksum64;
    private bool m_info_crc_only;
    private CBinary.EnumFileLayoutMode m_layout_mode;
    private bool m_random_padding;
    private int m_group_data_align;
    private CodecOptions m_codec_options;
    private bool m_enable_top_toc_info;
    private uint m_calc_toc_size;
    private uint m_calc_htoc_size;
    private uint m_calc_itoc_size;
    private uint m_calc_gtoc_size;
    private uint m_calc_hgtoc_size;
    private ulong m_calc_toc_pos;
    private ulong m_calc_htoc_pos;
    private ulong m_calc_itoc_pos;
    private ulong m_calc_gtoc_pos;
    private ulong m_calc_hgtoc_pos;
    private ulong m_content_pos_top;
    private ulong m_content_pos_tale;
    private EnumCompressCodec m_comp_codec;
    private int m_dpk_divided_size;
    private int m_max_dpk_itoc;
    private string m_pre_attr;
    private string m_pre_group;
    private bool m_pre_trycompress;
    private bool m_afs2_mode;
    private int m_compress_codec_alignment;
    private string m_part_of_cpk_header_ext;
    private bool m_enable_ginfo;
    private bool m_enable_attribute_user_string;
    private CBinary.CBinaryDelegate m_try_compress_delegate;
    private CBinary.CBinaryDelegate m_try_unification_delegate;
    private bool m_try_compress_complete;
    private bool m_try_unification_complete;
    private float m_comp_percentage;
    private int m_comp_filesize;
    private bool m_layout_order_for_comp_and_uni;

    public string BaseDirectory => this.m_basedir;

    public bool ForceCompress
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_force_compress;
      [param: MarshalAs(UnmanagedType.U1)] set
      {
        this.m_writer.ForceCompress = value;
        this.m_reader.ForceCompress = value;
        this.m_copier.ForceCompress = value;
        this.m_force_compress = value;
      }
    }

    public uint DataAlign
    {
      get => this.m_data_align;
      set => this.m_data_align = value;
    }

    public uint CpkMode
    {
      get => this.m_cpk_mode;
      set => this.m_cpk_mode = value;
    }

    public bool EnableFileName
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_filename;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_filename = value;
    }

    public bool EnableToc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_toc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_toc = value;
    }

    public bool EnableHtoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_toc && ((!this.m_enable_htoc_fpath_to_toc || !this.m_enable_filename ? 0 : 1) != 0 || (!this.m_enable_htoc_fid_to_toc || !this.m_enable_itoc ? 0 : 1) != 0);
    }

    public bool EnableHtocFpathToToc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_htoc_fpath_to_toc && this.m_enable_filename;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_htoc_fpath_to_toc = value;
    }

    public bool EnableHtocFidToToc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_htoc_fid_to_toc && this.m_enable_itoc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_htoc_fid_to_toc = value;
    }

    public bool EnableEtoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_etoc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_etoc = value;
    }

    public bool EnableItoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_itoc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_itoc = value;
    }

    public bool EnableGtoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_gtoc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_gtoc = value;
    }

    public bool EnableHgtoc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_gtoc && (this.m_enable_hgtoc_gname_to_glink || this.m_enable_hgtoc_ganame_to_ginf || this.EnableHgtocGfpathToGfinf || this.EnableHgtocGfidToGfinf);
    }

    public bool EnableHgtocGnameToGlink
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_hgtoc_gname_to_glink;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_hgtoc_gname_to_glink = value;
    }

    public bool EnableHgtocGanameToGinf
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_hgtoc_ganame_to_ginf;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_hgtoc_ganame_to_ginf = value;
    }

    public bool EnableHgtocGfpathToGfinf
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_hgtoc_gfpath_to_gfinf && this.m_enable_filename;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_hgtoc_gfpath_to_gfinf = value;
    }

    public bool EnableHgtocGfidToGfinf
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_hgtoc_gfid_to_gfinf && this.m_enable_itoc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_hgtoc_gfid_to_gfinf = value;
    }

    public int Version => this.m_ver;

    public int Revision => this.m_rev;

    public ulong ContentsOffset => this.m_contents_offset;

    public ulong ContentsSize => this.m_contents_size;

    public uint TocSize => (uint) this.m_toc_size;

    public uint HtocSize => (uint) this.m_htoc_size;

    public uint EtocSize => (uint) this.m_etoc_size;

    public uint ItocSize => (uint) this.m_itoc_size;

    public uint GtocSize => (uint) this.m_gtoc_size;

    public uint HgtocSize => (uint) this.m_hgtoc_size;

    public int MaxDpkItoc => this.m_max_dpk_itoc;

    public string WorkingMessage => this.m_working_message;

    public string ErrorMessage => this.m_error_message;

    public string CacheDirectory
    {
      get => this.m_writer.CacheDirectory;
      set
      {
        this.m_writer.CacheDirectory = value;
        this.m_reader.CacheDirectory = value;
        this.m_copier.CacheDirectory = value;
      }
    }

    public long CacheLimitSize
    {
      get => this.m_writer.CacheLimitSize;
      set
      {
        this.m_writer.CacheLimitSize = value;
        this.m_reader.CacheLimitSize = value;
        this.m_copier.CacheLimitSize = value;
      }
    }

    public bool Mask
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_mask;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_mask = value;
    }

    public bool SortedFilename
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_sorted_filename;
    }

    public CExternalBuffer ExternalBuffer
    {
      set
      {
        this.m_writer.ExternalBuffer = value;
        this.m_reader.ExternalBuffer = value;
        this.m_copier.ExternalBuffer = value;
        this.m_external_buffer = value;
      }
    }

    public bool IsInvalidFileMode
    {
      [return: MarshalAs(UnmanagedType.U1)] get => !this.m_enable_toc && this.m_enable_gtoc;
    }

    public bool EnableDateTimeInfo
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_datetime_info;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_datetime_info = value;
    }

    public bool Analyzed
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_analyzed;
    }

    public EnumUnificationMode UnificationMode
    {
      get => this.m_unification_mode;
      set => this.m_unification_mode = value;
    }

    public bool EnableDuplicateByGroup
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_duplicate_by_group;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_duplicate_by_group = value;
    }

    public bool EnableMself
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_mself;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_mself = value;
    }

    public bool IsExtraId
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_is_extra_id;
    }

    public bool EnableIdAndGroup
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_id_and_group;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_id_and_group = value;
    }

    public CBinary.EnumFileLayoutMode FileLayoutMode
    {
      get => this.m_layout_mode;
      set => this.m_layout_mode = value;
    }

    public ulong CpkDateTime64 => this.m_cpk_datetime;

    public DateTime CpkDateTime => Utility.ConvToDateTime(this.m_cpk_datetime);

    public bool EnableCrc
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_crc;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_crc = value;
    }

    public bool EnableCrc32
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_crc32;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_crc32 = value;
    }

    public bool EnableCheckSum64
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_checksum64;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_checksum64 = value;
    }

    public bool InfoCrcOnly
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_info_crc_only;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_info_crc_only = value;
    }

    public bool RandomPadding
    {
      [param: MarshalAs(UnmanagedType.U1)] set
      {
        this.m_writer.RandomPadding = value;
        this.m_reader.RandomPadding = value;
        this.m_copier.RandomPadding = value;
        this.m_random_padding = value;
      }
    }

    public bool EnableTopTocInfo
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_top_toc_info;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_top_toc_info = value;
    }

    public EnumCompressCodec CompressCodec
    {
      get => this.m_comp_codec;
      set
      {
        this.m_writer.CompressCodec = value;
        this.m_reader.CompressCodec = value;
        this.m_copier.CompressCodec = value;
        this.m_comp_codec = value;
      }
    }

    public int DpkDividedSize
    {
      get => this.m_dpk_divided_size;
      set
      {
        this.m_writer.DpkDividedSize = value;
        this.m_reader.DpkDividedSize = value;
        this.m_copier.DpkDividedSize = value;
        this.m_dpk_divided_size = value;
      }
    }

    public CodecOptions codecOptions
    {
      get => this.m_codec_options;
      set
      {
        this.m_writer.SetCompressOptions(value);
        this.m_reader.SetCompressOptions(value);
        this.m_copier.SetCompressOptions(value);
        this.m_codec_options = value;
      }
    }

    public float CompPercentage
    {
      set
      {
        this.m_writer.CompPercentage = value;
        this.m_reader.CompPercentage = value;
        this.m_copier.CompPercentage = value;
        this.m_comp_percentage = value;
      }
    }

    public int CompFileSize
    {
      set
      {
        this.m_writer.CompFileSize = value;
        this.m_reader.CompFileSize = value;
        this.m_copier.CompFileSize = value;
        this.m_comp_filesize = value;
      }
    }

    public int GroupDataAlignment
    {
      get => this.m_group_data_align;
      set => this.m_group_data_align = value;
    }

    public bool EnableAfs2
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_afs2_mode;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_afs2_mode = value;
    }

    public string UncompFileExt
    {
      get
      {
        string[] uncompExtList = this.m_uncomp_ext_list;
        if (uncompExtList == null || uncompExtList.Length == 0)
          return "";
        string str1 = "";
        string[] strArray = uncompExtList;
        int index = 0;
        if (0 < strArray.Length)
        {
          do
          {
            string str2 = strArray[index];
            str1 = str1 + str2 + ";";
            ++index;
          }
          while (index < strArray.Length);
        }
        char[] chArray = new char[1]{ ';' };
        return str1.Trim(chArray);
      }
      set
      {
        if (value == (string) null)
        {
          do
          {
            value = ".afs;.acx;.aax;.adx;.ahx;.aif;.aiff;.wav;.sfd;.m1v;.hca;.hcamx;.cpk;.aix;.awb;.acb;.acf;.usa;.usm;.usv;.self;.sprx;.sdat;.mself;.mpg;.mp3;.h264;.aac;.at9;.vag;.cwav;.p256;.sfp;.sfpb;.sai;.saib";
          }
          while (value == (string) null);
        }
        value = value.Trim();
        char[] chArray1 = new char[1]{ ';' };
        value = value.Trim(chArray1);
        char[] chArray2 = new char[1]{ ';' };
        this.m_uncomp_ext_list = value.Split(chArray2);
      }
    }

    public int CompressCodecAlignment
    {
      get => this.m_compress_codec_alignment;
      set => this.m_compress_codec_alignment = value;
    }

    public string PartOfCpkHeaderExt
    {
      get => this.m_part_of_cpk_header_ext;
      set => this.m_part_of_cpk_header_ext = value;
    }

    public bool EnableGInfo
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_ginfo;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_ginfo = value;
    }

    public bool EnableAttributeUserString
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_enable_attribute_user_string;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_enable_attribute_user_string = value;
    }

    public bool LayoutOrderForCompAndUni
    {
      [return: MarshalAs(UnmanagedType.U1)] get => this.m_layout_order_for_comp_and_uni;
      [param: MarshalAs(UnmanagedType.U1)] set => this.m_layout_order_for_comp_and_uni = value;
    }

    public unsafe CBinary([MarshalAs(UnmanagedType.U1)] bool usedlg)
    {
      this.m_utf = (CUtf) null;
      this.m_writer = new CAsyncFile();
      this.m_reader = new CAsyncFile();
      this.m_copier = new CAsyncFile();
      this.m_writer.UseDelegate = usedlg;
      this.m_reader.UseDelegate = usedlg;
      this.m_copier.UseDelegate = usedlg;
      this.UncompFileExt = ".afs;.acx;.aax;.adx;.ahx;.aif;.aiff;.wav;.sfd;.m1v;.hca;.hcamx;.cpk;.aix;.awb;.acb;.acf;.usa;.usm;.usv;.self;.sprx;.sdat;.mself;.mpg;.mp3;.h264;.aac;.at9;.vag;.cwav;.p256;.sfp;.sfpb;.sai;.saib";
      this.m_enable_datetime_info = true;
      this.m_analyzed = false;
      this.m_unification_mode = EnumUnificationMode.ModeNone;
      this.m_duplicate_by_group = false;
      this.m_enable_mself = false;
      this.m_pre_header_offset = 0L;
      this.m_cpk_file_size = 0L;
      this.m_self_count = 0;
      this.m_is_extra_id = false;
      this.m_enable_id_and_group = false;
      this.m_layout_mode = CBinary.EnumFileLayoutMode.LayoutModeUser;
      this.m_random_padding = false;
      this.m_enable_crc = false;
      this.m_enable_crc32 = false;
      this.m_enable_checksum64 = false;
      this.m_afs2_mode = false;
      this.m_enable_ginfo = false;
      this.m_info_crc_only = File.Exists(Path.Combine(Utility.GetModulePath(), nameof (InfoCrcOnly)));
      this.m_enable_top_toc_info = false;
      this.m_comp_codec = EnumCompressCodec.CodecLayla;
      this.m_group_data_align = 0;
      this.m_compress_codec_alignment = 0;
      this.m_part_of_cpk_header_ext = (string) null;
      this.m_toc_rtvobj = (_CriCbnRtv16*) \u003CModule\u003E.@new(124U);
      this.m_gtoc_glink_rtvobj = (_CriCbnRtv16*) \u003CModule\u003E.@new(124U);
      this.m_gtoc_flink_rtvobj = (_CriCbnRtv16*) \u003CModule\u003E.@new(124U);
      this.m_gtoc_attr_rtvobj = (_CriCbnRtv16*) \u003CModule\u003E.@new(124U);
      this.m_gtoc_ginf_rtvobj = (_CriCbnRtv16*) \u003CModule\u003E.@new(124U);
      this.m_work_hash_fpath_to_toc = (byte*) 0;
      this.m_work_hash_fid_to_toc = (byte*) 0;
      this.m_work_hash_gname_to_glink = (byte*) 0;
      this.m_work_hash_ganame_to_ginf = (byte*) 0;
      this.m_work_hash_gfpath_to_gfinf = (byte**) 0;
      this.m_work_hash_gfid_to_gfinf = (byte**) 0;
      this.Initialize();
    }

    private void \u007ECBinary() => this.finalize();

    private void \u0021CBinary() => this.finalize();

    public void Initialize()
    {
      this.m_stat = Status.Stop;
      this.m_data_align = 2048U;
      this.m_force_compress = false;
      this.m_enable_toc = true;
      this.m_enable_etoc = true;
      this.m_enable_itoc = false;
      this.m_enable_gtoc = false;
      this.m_enable_htoc_fpath_to_toc = false;
      this.m_enable_htoc_fid_to_toc = false;
      this.m_enable_hgtoc_gname_to_glink = false;
      this.m_enable_hgtoc_ganame_to_ginf = false;
      this.m_enable_hgtoc_gfpath_to_gfinf = false;
      this.m_enable_hgtoc_gfid_to_gfinf = false;
      this.m_ver = 7;
      this.m_rev = 14;
      this.initializeWorkingValues();
      this.m_writer.Close();
      this.m_reader.Close();
      this.m_copier.Close();
      this.m_content_pos_tale = 0UL;
      this.m_content_pos_top = 0UL;
      this.m_mask = false;
      this.m_max_dpk_itoc = 0;
      this.m_unification_list = new Dictionary<string, List<CFileInfo>>();
      this.m_layout_order_for_comp_and_uni = false;
    }

    private void initializeWorkingValues()
    {
      this.m_cpk_datetime = 0UL;
      this.m_toc_size = 0UL;
      this.m_etoc_size = 0UL;
      this.m_itoc_size = 0UL;
      this.m_gtoc_size = 0UL;
      this.m_working_message = "";
    }

    private unsafe void finalize()
    {
      CAsyncFile reader = this.m_reader;
      if (reader != null)
      {
        reader.Dispose();
        this.m_reader = (CAsyncFile) null;
      }
      CAsyncFile writer = this.m_writer;
      if (writer != null)
      {
        writer.Dispose();
        this.m_writer = (CAsyncFile) null;
      }
      CAsyncFile copier = this.m_copier;
      if (copier != null)
      {
        copier.Dispose();
        this.m_copier = (CAsyncFile) null;
      }
      CUtf utf = this.m_utf;
      if (utf != null)
      {
        utf.Dispose();
        this.m_utf = (CUtf) null;
      }
      _CriCbnRtv16* tocRtvobj = this.m_toc_rtvobj;
      if ((IntPtr) tocRtvobj != IntPtr.Zero)
      {
        \u003CModule\u003E.delete((void*) tocRtvobj);
        this.m_toc_rtvobj = (_CriCbnRtv16*) 0;
      }
      _CriCbnRtv16* gtocGlinkRtvobj = this.m_gtoc_glink_rtvobj;
      if ((IntPtr) gtocGlinkRtvobj != IntPtr.Zero)
      {
        \u003CModule\u003E.delete((void*) gtocGlinkRtvobj);
        this.m_gtoc_glink_rtvobj = (_CriCbnRtv16*) 0;
      }
      _CriCbnRtv16* gtocFlinkRtvobj = this.m_gtoc_flink_rtvobj;
      if ((IntPtr) gtocFlinkRtvobj != IntPtr.Zero)
      {
        \u003CModule\u003E.delete((void*) gtocFlinkRtvobj);
        this.m_gtoc_flink_rtvobj = (_CriCbnRtv16*) 0;
      }
      _CriCbnRtv16* gtocAttrRtvobj = this.m_gtoc_attr_rtvobj;
      if ((IntPtr) gtocAttrRtvobj != IntPtr.Zero)
      {
        \u003CModule\u003E.delete((void*) gtocAttrRtvobj);
        this.m_gtoc_attr_rtvobj = (_CriCbnRtv16*) 0;
      }
      _CriCbnRtv16* gtocGinfRtvobj = this.m_gtoc_ginf_rtvobj;
      if ((IntPtr) gtocGinfRtvobj == IntPtr.Zero)
        return;
      \u003CModule\u003E.delete((void*) gtocGinfRtvobj);
      this.m_gtoc_ginf_rtvobj = (_CriCbnRtv16*) 0;
    }

    private void SetToError(string msg)
    {
      Console.WriteLine("SetError : " + msg);
      this.m_error_message = msg;
      this.m_stat = Status.Error;
    }

    public CUtf.EnumCrcMode GetCrcMode() => (CUtf.EnumCrcMode) (this.m_comp_codec == EnumCompressCodec.CodecLZMA);

    [return: MarshalAs(UnmanagedType.U1)]
    public bool StartBuildCpkFile(CFileData filedata, string fpath)
    {
      if (this.m_comp_codec == EnumCompressCodec.CodecDpk && this.m_enable_crc)
        this.CompressCodec = EnumCompressCodec.CodecDpkForceCrc;
      if (!this.m_enable_toc && this.m_enable_gtoc)
      {
        string str = "Invalid CPK file mode.";
        Console.WriteLine("SetError : " + str);
        this.m_error_message = str;
        this.m_stat = Status.Error;
        return false;
      }
      this.m_filedata = filedata;
      this.m_basedir = filedata.BaseDirectory;
      this.m_exec_mode = CBinary.ExecMode.MakeCpkFile;
      if (!this.m_analyzed && !Utility.DeleteFile(fpath))
      {
        this.SetToError("Could not access to a file. \"" + fpath + "\"");
        return false;
      }
      if (this.m_writer.WriteOpen(fpath, this.m_analyzed))
      {
        CUtf utf = this.m_utf;
        if (utf != null)
        {
          utf.Dispose();
          this.m_utf = (CUtf) null;
        }
        bool enableCrc = this.m_enable_crc;
        bool use_file_crc = enableCrc;
        EnumCompressCodec compCodec = this.m_comp_codec;
        if (compCodec == EnumCompressCodec.CodecDpkForceCrc)
          use_file_crc = false;
        if (enableCrc && this.m_info_crc_only)
          use_file_crc = false;
        CUtf cutf = new CUtf((CUtf.EnumCrcMode) (compCodec == EnumCompressCodec.CodecLZMA), enableCrc, use_file_crc, this.m_enable_ginfo);
        this.m_utf = cutf;
        cutf.RandomPadding = this.m_random_padding;
        this.m_utf.EnableAttributeUserString = this.m_enable_attribute_user_string;
        this.m_stat = Status.HeaderMselfSkipping;
        this.initializeWorkingValues();
        return true;
      }
      string str1 = "Cannot open a file. \"" + fpath + "\"";
      Console.WriteLine("SetError : " + str1);
      this.m_error_message = str1;
      this.m_stat = Status.Error;
      return false;
    }

    public Status Execute()
    {
      switch (this.m_stat)
      {
        case Status.HeaderMselfSkipping:
          CBinary cbinary1 = this;
          cbinary1.m_stat = cbinary1.ExecHeaderMselfSkipping();
          break;
        case Status.HeaderSkipping:
          CBinary cbinary2 = this;
          cbinary2.m_stat = cbinary2.ExecHeaderSkipping();
          break;
        case Status.PreFileDataBuilding:
          CBinary cbinary3 = this;
          cbinary3.m_stat = cbinary3.ExecPreFileDataBuilding();
          break;
        case Status.FileDataBuildingPrep:
          CBinary cbinary4 = this;
          cbinary4.m_stat = cbinary4.ExecFileDataBuildingPrep();
          break;
        case Status.FileDataBuildingStart:
          CBinary cbinary5 = this;
          cbinary5.m_stat = cbinary5.ExecFileDataBuildingStart();
          break;
        case Status.FileDataBuildingCopying:
          CBinary cbinary6 = this;
          cbinary6.m_stat = cbinary6.ExecFileDataBuildingCopying();
          break;
        case Status.TocInfoMaking:
          CBinary cbinary7 = this;
          cbinary7.m_stat = cbinary7.ExecTocInfoMaking();
          break;
        case Status.ItocInfoMaking:
          CBinary cbinary8 = this;
          cbinary8.m_stat = cbinary8.ExecItocInfoMaking();
          break;
        case Status.GtocInfoMaking:
          CBinary cbinary9 = this;
          cbinary9.m_stat = cbinary9.ExecGtocInfoMaking();
          break;
        case Status.PreTocBuilding:
          CBinary cbinary10 = this;
          cbinary10.m_stat = cbinary10.ExecPreTocBuilding();
          break;
        case Status.TocBuilding:
          CBinary cbinary11 = this;
          cbinary11.m_stat = cbinary11.ExecTocBuilding();
          break;
        case Status.HtocInfoMaking:
          CBinary cbinary12 = this;
          cbinary12.m_stat = cbinary12.ExecHtocInfoMaking();
          break;
        case Status.HtocBuilding:
          CBinary cbinary13 = this;
          cbinary13.m_stat = cbinary13.ExecHtocBuilding();
          break;
        case Status.ItocBuilding:
          CBinary cbinary14 = this;
          cbinary14.m_stat = cbinary14.ExecItocBuilding();
          break;
        case Status.GtocBuilding:
          CBinary cbinary15 = this;
          cbinary15.m_stat = cbinary15.ExecGtocBuilding();
          break;
        case Status.HgtocInfoMaking:
          CBinary cbinary16 = this;
          cbinary16.m_stat = cbinary16.ExecHgtocInfoMaking();
          break;
        case Status.HgtocBuilding:
          CBinary cbinary17 = this;
          cbinary17.m_stat = cbinary17.ExecHgtocBuilding();
          break;
        case Status.EtocBuilding:
          CBinary cbinary18 = this;
          cbinary18.m_stat = cbinary18.ExecEtocBuilding();
          break;
        case Status.HeaderRewriting:
          CBinary cbinary19 = this;
          cbinary19.m_stat = cbinary19.ExecHeaderRewriting();
          break;
        case Status.MselfRewriting:
          this.WriteMselfHeader();
          this.m_stat = Status.CpkBuildFinalize;
          break;
        case Status.CpkBuildFinalize:
          CBinary cbinary20 = this;
          cbinary20.m_stat = cbinary20.ExecCpkBuildFinalize();
          break;
        case Status.TryCompress:
          CBinary cbinary21 = this;
          cbinary21.m_stat = cbinary21.ExecTryCompress();
          break;
        case Status.TryUnification:
          CBinary cbinary22 = this;
          cbinary22.m_stat = cbinary22.ExecTryUnification();
          break;
        case Status.TryCompressEnd:
          this.m_stat = this.m_try_compress_complete ? Status.TryUnification : Status.TryCompressEnd;
          break;
        case Status.TryUnificationEnd:
          this.m_stat = this.m_try_unification_complete ? Status.PreFileDataBuilding : Status.TryUnificationEnd;
          break;
        case Status.ExtractPreparing:
          this.m_exec_file_cur = 0UL;
          this.m_stat = Status.ExtractStart;
          break;
        case Status.ExtractStart:
          CBinary cbinary23 = this;
          cbinary23.m_stat = cbinary23.ExecExtractStart();
          break;
        case Status.ExtractCopying:
          CBinary cbinary24 = this;
          cbinary24.m_stat = cbinary24.ExecExtractCopying();
          break;
        case Status.Extracted:
          this.m_reader.Close();
          this.m_stat = Status.Complete;
          break;
        case Status.VerifyPreparing:
          this.m_exec_file_cur = 0UL;
          this.m_stat = Status.Verifying;
          break;
        case Status.Verifying:
          CBinary cbinary25 = this;
          cbinary25.m_stat = cbinary25.ExecVerifying();
          break;
        case Status.Verified:
          this.m_reader.Close();
          this.m_stat = Status.Complete;
          break;
      }
      return this.m_stat;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool IsIncludedMselfFiles()
    {
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.FileInfos.GetEnumerator();
      if (enumerator.MoveNext())
      {
        do
        {
          string extension = Path.GetExtension(enumerator.Current.LocalFilePath);
          if (extension == ".self" || extension == ".sprx" || extension == ".sdat")
            goto label_3;
        }
        while (enumerator.MoveNext());
        goto label_4;
label_3:
        return true;
      }
label_4:
      return false;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool IsSelfFile(string fpath)
    {
      string extension = Path.GetExtension(fpath);
      return extension == ".self" || extension == ".sprx" || extension == ".sdat";
    }

    private Status ExecHeaderMselfSkipping()
    {
      if (this.m_data_align < 1U)
        this.m_data_align = 1U;
      if (this.m_enable_gtoc)
      {
        int groupDataAlign = this.m_group_data_align;
        if (groupDataAlign > 0)
        {
          CBinary cbinary = this;
          cbinary.m_data_align = Math.Max(cbinary.m_data_align, (uint) groupDataAlign);
        }
      }
      else
        this.m_group_data_align = 0;
      this.m_utf.Initialize();
      this.m_utf.SetMaskFlag(this.m_mask);
      this.m_utf.DataAlign = this.m_data_align;
      this.m_utf.CpkMode = this.m_cpk_mode;
      this.m_utf.EnableFileName = this.m_enable_filename ? (ushort) 1 : (ushort) 0;
      this.m_utf.Comment = this.m_filedata.Comment;
      this.m_utf.ToolVersion = this.m_filedata.ToolVersion;
      this.m_pre_header_offset = 0L;
      this.m_pre_attr = (string) null;
      this.m_pre_group = (string) null;
      this.m_pre_trycompress = true;
      if (this.m_enable_mself)
      {
        this.m_self_count = 0;
        List<CFileInfo>.Enumerator enumerator = this.m_filedata.FileInfos.GetEnumerator();
        if (enumerator.MoveNext())
        {
          do
          {
            string extension = Path.GetExtension(enumerator.Current.LocalFilePath);
            if (extension == ".self" || extension == ".sprx" || extension == ".sdat")
              ++this.m_self_count;
          }
          while (enumerator.MoveNext());
        }
        CBinary cbinary1 = this;
        cbinary1.m_pre_header_offset = (long) (uint) ((cbinary1.m_self_count + 1) * 64);
        long num = (long) this.m_writer.WriteZeroData((ulong) this.m_pre_header_offset);
        long alignment = (long) this.m_writer.WritePaddingToAlignment(2048UL);
        CBinary cbinary2 = this;
        cbinary2.m_pre_header_offset = (long) cbinary2.m_writer.Position;
        if (this.m_self_count == 0)
        {
          string str = "No .self/.sprx files in the CPK file.";
          this.m_error_message = str;
          Console.WriteLine(str);
          return Status.Error;
        }
      }
      this.m_working_message = "Preparing ...";
      return Status.HeaderSkipping;
    }

    private Status ExecHeaderSkipping()
    {
      if (!this.m_afs2_mode)
      {
        uint dataAlign = this.m_data_align;
        long num = (long) this.m_writer.WriteZeroData(dataAlign <= 2048U ? 2048UL : (ulong) (int) dataAlign);
      }
      bool enableToc = this.m_enable_toc;
      this.m_sorted_filename = enableToc;
      this.m_utf.SetSortedFilenameFlag(enableToc);
      this.m_utf.CpkDateTime64 = (ulong) this.m_enable_datetime_info;
      this.m_working_message = "Preparing ...";
      if (!this.m_layout_order_for_comp_and_uni)
        return Status.PreFileDataBuilding;
      this.m_stat = Status.TryCompress;
      return Status.TryCompress;
    }

    private Status ExecPreFileDataBuilding()
    {
      if (!this.m_enable_gtoc)
      {
        List<CFileInfo>.Enumerator enumerator = this.m_filedata.FileInfos.GetEnumerator();
        if (enumerator.MoveNext())
        {
          do
          {
            enumerator.Current.DataAlign = this.m_data_align;
          }
          while (enumerator.MoveNext());
        }
      }
      switch (this.m_layout_mode)
      {
        case CBinary.EnumFileLayoutMode.LayoutModeUser:
          this.m_filedata.SortByRegisteredId();
          break;
        case CBinary.EnumFileLayoutMode.LayoutModeId:
          this.m_filedata.SortById();
          break;
        case CBinary.EnumFileLayoutMode.LayoutModeFilename:
          this.m_filedata.SortByFilename();
          break;
        case CBinary.EnumFileLayoutMode.LayoutModeGroup:
          this.m_filedata.UpdateFileInfoPackingOrder();
          this.m_filedata.SortByPackingOrder();
          break;
      }
      if (this.m_enable_itoc && !this.m_enable_toc && !this.m_enable_gtoc)
        this.m_filedata.SortById();
      this.m_exec_file_cur = 0UL;
      this.m_calc_toc_size = 0U;
      this.m_calc_htoc_size = 0U;
      this.m_calc_itoc_size = 0U;
      this.m_calc_gtoc_size = 0U;
      this.m_calc_hgtoc_size = 0U;
      this.m_calc_toc_pos = 0UL;
      this.m_calc_htoc_pos = 0UL;
      this.m_calc_itoc_pos = 0UL;
      this.m_calc_gtoc_pos = 0UL;
      this.m_calc_hgtoc_pos = 0UL;
      if (this.m_enable_gtoc)
      {
        this.m_filedata.SetGroupObjectIndex();
        if (this.m_enable_ginfo)
        {
          CGInfoData cgInfoData = new CGInfoData();
          ulong index = 0;
          if (0UL < this.m_filedata.Count)
          {
            do
            {
              CFileInfo fileInfo = this.m_filedata.FileInfos[(int) index];
              cgInfoData.AddFile(fileInfo);
              ++index;
            }
            while (index < this.m_filedata.Count);
          }
          List<string> sortedList = cgInfoData.GetSortedList();
          int num = 0;
          if (0 < sortedList.Count)
          {
            do
            {
              string gaName = sortedList[num];
              int size = cgInfoData.GetSize(gaName);
              int files = cgInfoData.GetFiles(gaName);
              this.m_utf.SetGroupGinf(gaName, size, files, num);
              ++num;
            }
            while (num < sortedList.Count);
          }
        }
      }
      if (this.m_analyzed)
      {
        int ver = this.m_ver;
        if (ver < 5 && (ver != 4 || this.m_rev < 2))
        {
          CBinary cbinary = this;
          cbinary.m_content_pos_top = cbinary.m_writer.Position - (ulong) this.m_pre_header_offset;
          this.m_writer.Position = this.m_toc_offset;
        }
        else
        {
          ulong contentsOffset = this.m_contents_offset;
          this.m_content_pos_top = contentsOffset;
          this.m_writer.Position = this.m_contents_size + contentsOffset;
        }
        this.m_utf.SetUpdates(++this.m_updates);
      }
      else
      {
        if (this.m_enable_top_toc_info && this.calcPreInfoSizeAndWritePaddings(Status.PreFileDataBuilding) == Status.Error)
          return Status.Error;
        CBinary cbinary = this;
        cbinary.m_content_pos_top = cbinary.m_writer.Position - (ulong) this.m_pre_header_offset;
      }
      this.m_utf.SetContentsFileOffset(this.m_content_pos_top);
      return Status.FileDataBuildingPrep;
    }

    private unsafe Status calcPreInfoSizeAndWritePaddings(Status preStat)
    {
      uint utfbin_size = 0;
      void* utfbin_ptr = (void*) 0;
      if (this.m_enable_toc)
      {
        uint index = 0;
        if (0UL < this.m_filedata.Count)
        {
          ulong num = 0;
          do
          {
            this.m_filedata.FileInfos[(int) index].InfoIndex = (long) index;
            this.m_filedata.FileInfos[(int) index].Crc32 = index;
            this.m_filedata.FileInfos[(int) index].CheckSum64 = num;
            CBinary cbinary = this;
            if (cbinary.addTocFileInfo(cbinary.m_filedata.FileInfos[(int) index]))
            {
              ++index;
              num = (ulong) index;
            }
            else
              goto label_5;
          }
          while (num < this.m_filedata.Count);
          goto label_6;
label_5:
          return Status.Error;
        }
label_6:
        utfbin_ptr = this.m_utf.MakeTocImage(&utfbin_size, false, false);
        this.m_calc_toc_size = (uint) ((int) utfbin_size + (int) this.m_utf.GetIffSize() + 32);
        this.m_utf.ResetTocFileInfo();
        long alignment1 = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
        CBinary cbinary1 = this;
        cbinary1.m_calc_toc_pos = cbinary1.m_writer.Position;
        long num1 = (long) this.m_writer.WriteZeroData((ulong) this.m_calc_toc_size);
        long alignment2 = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
      }
      if ((this.EnableHtoc || this.EnableHgtoc) && !this.setupTocRtv((byte*) utfbin_ptr, utfbin_size))
        return Status.Error;
      if (this.EnableHtoc && this.m_is_toc_rtv_setup)
      {
        if ((!this.m_enable_htoc_fpath_to_toc || !this.m_enable_filename ? 0 : 1) == 1 && !this.addHashTableHtocCommonToToc(true, ref this.m_work_hash_fpath_to_toc) || (!this.m_enable_htoc_fid_to_toc || !this.m_enable_itoc ? 0 : 1) == 1 && !this.addHashTableHtocCommonToToc(false, ref this.m_work_hash_fid_to_toc))
          return Status.Error;
        this.m_utf.MakeHtocImage(&utfbin_size, false, false);
        this.m_calc_htoc_size = (uint) ((int) utfbin_size + (int) this.m_utf.GetIffSize() + 32);
        this.m_utf.ResetHtocFileInfo();
        this.releaseHashTableHtocBinaryImage();
        CBinary cbinary = this;
        cbinary.m_calc_htoc_pos = cbinary.m_writer.Position;
        long num = (long) this.m_writer.WriteZeroData((ulong) this.m_calc_htoc_size);
        long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
      }
      if (this.m_enable_itoc)
      {
        uint index1 = 0;
        if (0UL < this.m_filedata.Count)
        {
          ulong num = 0;
          do
          {
            this.m_filedata.FileInfos[(int) index1].InfoIndex = (long) index1;
            this.m_filedata.FileInfos[(int) index1].Crc32 = index1;
            this.m_filedata.FileInfos[(int) index1].CheckSum64 = num;
            ++index1;
            num = (ulong) index1;
          }
          while (num < this.m_filedata.Count);
        }
        this.addItocFileInfos();
        if (this.m_enable_toc)
          this.m_utf.MakeItocImageExtra(&utfbin_size, false, false);
        else
          this.m_utf.MakeItocImageCompact(&utfbin_size, false, false);
        this.m_calc_itoc_size = (uint) ((int) utfbin_size + (int) this.m_utf.GetIffSize() + 32);
        this.m_utf.ResetItocFileInfo();
        CBinary cbinary = this;
        cbinary.m_calc_itoc_pos = cbinary.m_writer.Position;
        if (this.m_afs2_mode)
        {
          long num2 = 0;
          int index2 = 0;
          if (0UL < this.m_filedata.Count)
          {
            do
            {
              num2 += (long) this.m_filedata.FileInfos[index2].FilesizeAligned;
              ++index2;
            }
            while ((ulong) index2 < this.m_filedata.Count);
          }
          afs2_header afs2Header;
          // ISSUE: initblk instruction
          __memset(ref afs2Header, 0, 12);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(short&) ((IntPtr) &afs2Header + 8) = (short) (ushort) this.m_data_align;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &afs2Header + 4) = (int) (uint) this.m_filedata.Count;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(sbyte&) ((IntPtr) &afs2Header + 2) = (sbyte) 2;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(sbyte&) ((IntPtr) &afs2Header + 1) = (sbyte) 2;
          ulong num3 = (ulong) \u003CModule\u003E.AFS2_CalcAfs2HeaderInfoSize(&afs2Header);
          if (num3 + (ulong) num2 > (ulong) ushort.MaxValue)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(sbyte&) ((IntPtr) &afs2Header + 1) = (sbyte) 4;
            num3 = (ulong) \u003CModule\u003E.AFS2_CalcAfs2HeaderInfoSize(&afs2Header);
            if (num3 + (ulong) num2 > (ulong) uint.MaxValue)
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(sbyte&) ((IntPtr) &afs2Header + 1) = (sbyte) 8;
              num3 = (ulong) \u003CModule\u003E.AFS2_CalcAfs2HeaderInfoSize(&afs2Header);
            }
          }
          ulong position = this.m_writer.Position;
          long num4 = (long) this.m_writer.WriteZeroData(num3 - position);
        }
        else
        {
          long num = (long) this.m_writer.WriteZeroData((ulong) this.m_calc_itoc_size);
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
        }
      }
      if (this.m_enable_gtoc)
      {
        this.m_filedata.SetGroupObjectIndex();
        utfbin_ptr = this.m_utf.MakeGtocImage(this.m_filedata.GroupingManager, &utfbin_size, false, false);
        this.m_calc_gtoc_size = (uint) ((int) utfbin_size + (int) this.m_utf.GetIffSize() + 32);
        CBinary cbinary = this;
        cbinary.m_calc_gtoc_pos = cbinary.m_writer.Position;
        long num = (long) this.m_writer.WriteZeroData((ulong) this.m_calc_gtoc_size);
        long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
      }
      if (this.EnableHgtoc)
      {
        if (!this.setupGtocRtv((byte*) utfbin_ptr, utfbin_size))
          return Status.Error;
        if (this.m_is_gtoc_rtv_setup)
        {
          if (this.m_enable_hgtoc_gname_to_glink && !this.addHashTableHgtocGnameToGlink() || this.m_enable_hgtoc_ganame_to_ginf && !this.addHashTableHgtocGanameToGinf() || (!this.m_enable_hgtoc_gfpath_to_gfinf || !this.m_enable_filename ? 0 : 1) == 1 && !this.addHashTableHgtocCommonToGfinf(true, ref this.m_work_hash_gfpath_to_gfinf) || (!this.m_enable_hgtoc_gfid_to_gfinf || !this.m_enable_itoc ? 0 : 1) == 1 && !this.addHashTableHgtocCommonToGfinf(false, ref this.m_work_hash_gfid_to_gfinf))
            return Status.Error;
          this.m_utf.MakeHgtocImage(&utfbin_size, false, false);
          this.m_calc_hgtoc_size = (uint) ((int) utfbin_size + (int) this.m_utf.GetIffSize() + 32);
          this.m_utf.ResetHgtocInfo();
          CBinary cbinary = this;
          cbinary.m_calc_hgtoc_pos = cbinary.m_writer.Position;
          long num = (long) this.m_writer.WriteZeroData((ulong) this.m_calc_hgtoc_size);
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
        }
      }
      this.m_utf.ReleaseBinaryImage();
      this.releaseHashTableHgtocBinaryImage();
      return preStat;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool IsUnificationFile(CFileInfo file)
    {
      bool uniTarget;
      switch (this.m_unification_mode)
      {
        case EnumUnificationMode.ModeNone:
          return false;
        case EnumUnificationMode.ModeFileAll:
          if (this.m_group_data_align > 0)
          {
            if (file.Filesize < (ulong) this.m_group_data_align)
              return false;
            ulong dataAlign = (ulong) file.DataAlign;
            if (file.Filesize < dataAlign)
              return false;
          }
          return true;
        case EnumUnificationMode.ModeFileChecked:
          uniTarget = file.UniTarget;
          goto default;
        default:
          if (!uniTarget)
            goto case EnumUnificationMode.ModeNone;
          else
            goto case EnumUnificationMode.ModeFileAll;
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool IsUnificationFile(CFileInfo a, CFileInfo b) => (long) a.Extractsize == (long) b.Extractsize && a.TryCompress == b.TryCompress && a.DataAlign <= b.DataAlign;

    private CFileInfo SearchInificationFile(CFileInfo file)
    {
      List<CFileInfo> cfileInfoList = (List<CFileInfo>) null;
      file.AnalyzeMd5Hash();
      if (this.m_unification_list.TryGetValue(file.Md5, out cfileInfoList))
      {
        int index = 0;
        if (0 < cfileInfoList.Count)
        {
          do
          {
            CFileInfo cfileInfo = cfileInfoList[index];
            if ((long) file.Extractsize != (long) cfileInfo.Extractsize || file.TryCompress != cfileInfo.TryCompress || file.DataAlign > cfileInfo.DataAlign)
              ++index;
            else
              goto label_4;
          }
          while (index < cfileInfoList.Count);
          goto label_6;
label_4:
          return cfileInfoList[index];
        }
      }
      else
      {
        cfileInfoList = new List<CFileInfo>();
        this.m_unification_list.Add(file.Md5, cfileInfoList);
      }
label_6:
      cfileInfoList.Add(file);
      return (CFileInfo) null;
    }

    private ulong getFileFrontPosition() => 2048;

    private Status ExecFileDataBuildingPrep()
    {
      if ((long) this.m_filedata.Count != (long) this.m_exec_file_cur)
      {
        CFileInfo fileInfo;
        do
        {
          fileInfo = this.m_filedata.FileInfos[(int) this.m_exec_file_cur];
          if (fileInfo.Burned)
          {
            fileInfo.Offset += 18446744073709549568UL;
            ++this.m_exec_file_cur;
          }
          else
            goto label_4;
        }
        while ((long) this.m_filedata.Count != (long) this.m_exec_file_cur);
        goto label_3;
label_4:
        if (this.IsUnificationFile(fileInfo))
        {
          CFileInfo cfileInfo = this.SearchInificationFile(fileInfo);
          if (cfileInfo != null)
          {
            fileInfo.Offset = cfileInfo.Offset;
            fileInfo.Filesize = cfileInfo.Filesize;
            fileInfo.Extractsize = cfileInfo.Extractsize;
            ulong extractsize = cfileInfo.Extractsize;
            this.m_working_message = string.Format("Found unification file ... \"{0}\" and \"{1}\" ({2} bytes).", (object) Path.GetFileName(fileInfo.LocalFilePath), (object) Path.GetFileName(cfileInfo.LocalFilePath), (object) extractsize.ToString("N0"));
            ++this.m_exec_file_cur;
            return Status.FileDataBuildingPrep;
          }
        }
        CBinary cbinary = this;
        cbinary.m_is_uncomp_file = cbinary.IsUncompresseFilename(fileInfo);
        if (this.m_reader.ReadOpen(fileInfo.LocalFilePath))
        {
          if (this.m_group_data_align > 0 && (!(this.m_pre_group == fileInfo.GroupString) || !(this.m_pre_attr == fileInfo.AttributeString) || this.m_pre_trycompress || fileInfo.TryCompress || fileInfo.UniTarget))
          {
            CFileInfo cfileInfo = fileInfo;
            cfileInfo.DataAlign = Math.Max(cfileInfo.DataAlign, (uint) this.m_group_data_align);
          }
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) fileInfo.DataAlign);
          fileInfo.Offset = this.m_writer.Position + (ulong) (-2048L - this.m_pre_header_offset);
          ulong fileSize = this.m_reader.FileSize;
          this.m_working_message = string.Format("Copying ... \"{0}\" ({1} bytes).", (object) Path.GetFileName(fileInfo.LocalFilePath), (object) fileSize.ToString("N0"));
          this.m_pre_group = fileInfo.GroupString;
          this.m_pre_attr = fileInfo.AttributeString;
          this.m_pre_trycompress = fileInfo.TryCompress;
          Debug.WriteLine(this.m_working_message);
          return Status.FileDataBuildingStart;
        }
        string str = "Cannot open a file. " + fileInfo.LocalFilePath;
        this.m_error_message = str;
        Console.WriteLine(str);
        return Status.Error;
      }
label_3:
      this.m_working_message = "Writing TOC ...";
      ulong dataAlign = (ulong) this.m_data_align;
      CBinary cbinary1 = this;
      cbinary1.m_content_pos_tale = (ulong) ((long) ((ulong) ((long) cbinary1.m_writer.Position + (long) dataAlign + -1L) / dataAlign) * (long) dataAlign - this.m_pre_header_offset);
      this.m_utf.SetContentsFileSize(this.m_content_pos_tale - this.m_content_pos_top);
      return Status.TocInfoMaking;
    }

    private Status ExecFileDataBuildingStart()
    {
      CFileInfo fileInfo = this.m_filedata.FileInfos[(int) this.m_exec_file_cur];
      this.m_copier.Ccrc.SetMode((CUtf.EnumCrcMode) (this.m_comp_codec == EnumCompressCodec.CodecLZMA));
      this.m_copier.Ccrc.Reset(false, 0U, 0UL);
      byte num;
      if (this.m_enable_crc)
      {
        switch (this.m_comp_codec)
        {
          case EnumCompressCodec.CodecLayla:
          case EnumCompressCodec.CodecLZMA:
          case EnumCompressCodec.CodecRELC:
            num = (byte) 1;
            goto label_4;
        }
      }
      num = (byte) 0;
label_4:
      this.m_copier.EnableCrcCheck = (bool) num;
      int compressCodecAlignment = this.m_compress_codec_alignment;
      this.m_copier.CompressCodecAlignment = (int) fileInfo.DataAlign >= compressCodecAlignment ? 0UL : (ulong) this.m_compress_codec_alignment;
      if ((this.m_force_compress || fileInfo.TryCompress) && !this.m_is_uncomp_file)
        this.m_copier.Copy(this.m_reader, this.m_writer, 0UL, this.m_reader.FileSize, CAsyncFile.CopyMode.TryWriteCompress);
      else
        this.m_copier.Copy(this.m_reader, this.m_writer);
      return Status.FileDataBuildingCopying;
    }

    private Status ExecFileDataBuildingCopying()
    {
      CFileInfo fileInfo = this.m_filedata.FileInfos[(int) this.m_exec_file_cur];
      switch (this.m_copier.Status)
      {
        case CAsyncFile.AsyncStatus.Complete:
          this.m_copier.WaitForComplete();
          if ((this.m_force_compress || fileInfo.TryCompress) && !this.m_is_uncomp_file)
          {
            if (this.m_copier.CompressedSize == 0UL)
            {
              fileInfo.SetCompressInfo(this.m_reader.FileSize);
              Debug.WriteLine("*** Failed commpession. " + fileInfo.LocalFilePath);
              if (this.m_reader.FileSize != 0UL)
              {
                ulong fileSize = this.m_reader.FileSize;
                string str = "Error in compression. (out of memory)\r\n" + "\"" + fileInfo.LocalFilePath + "\" (" + fileSize.ToString("N0") + " bytes).";
                this.m_error_message = str;
                Console.WriteLine(str);
                this.m_reader.Close();
                return Status.Error;
              }
            }
            else
            {
              fileInfo.SetCompressInfo(this.m_writer.CompressedSize);
              if (this.m_copier.CompressCodecAlignment > 0UL)
              {
                if ((ulong) fileInfo.DataAlign <= this.m_copier.CompressCodecAlignment)
                  fileInfo.DataAlign = (uint) this.m_copier.CompressCodecAlignment;
                fileInfo.Offset = this.m_copier.PreReadWritePosition + (ulong) (-2048L - this.m_pre_header_offset);
              }
            }
          }
          else
            fileInfo.Filesize = this.m_reader.FileSize;
          if (this.m_enable_crc)
          {
            switch (this.m_comp_codec)
            {
              case EnumCompressCodec.CodecLayla:
              case EnumCompressCodec.CodecLZMA:
              case EnumCompressCodec.CodecRELC:
                fileInfo.Crc32 = this.m_copier.Ccrc.Crc32;
                fileInfo.CheckSum64 = this.m_copier.Ccrc.CheckSum64;
                break;
            }
          }
          this.m_reader.Close();
          if (this.m_writer.DpkItocSize != 0)
          {
            int maxDpkItoc = this.m_max_dpk_itoc;
            this.m_max_dpk_itoc = this.m_writer.DpkItocSize <= maxDpkItoc ? maxDpkItoc : this.m_writer.DpkItocSize;
          }
          ++this.m_exec_file_cur;
          return Status.FileDataBuildingPrep;
        case CAsyncFile.AsyncStatus.Error:
          ulong fileSize1 = this.m_reader.FileSize;
          string str1 = "Error in compression encoder or Cannot read/write a file. " + "\"" + fileInfo.LocalFilePath + "\" (" + fileSize1.ToString("N0") + " bytes).";
          this.m_error_message = str1;
          Console.WriteLine(str1);
          this.m_reader.Close();
          return Status.Error;
        default:
          return Status.FileDataBuildingCopying;
      }
    }

    private Status ExecTocInfoMaking()
    {
      if (this.m_sorted_filename)
      {
        if (this.m_enable_id_and_group)
          this.m_filedata.SortById();
        else
          this.m_filedata.SortByFilename();
      }
      this.m_filedata.UpdateFileInfoIndex();
      Debug.WriteLine("---- Add File Info to UTF");
      ulong index = 0;
      if (0UL < this.m_filedata.Count)
      {
        string fullpath;
        do
        {
          CFileInfo fileInfo = this.m_filedata.FileInfos[(int) index];
          fullpath = fileInfo.ContentFilePath;
          if (this.m_enable_id_and_group)
            fullpath = (string) null;
          this.m_utf.EnableDateTime = this.m_enable_datetime_info;
          if (this.m_utf.AddFileInfo(fullpath, fileInfo.Filesize, fileInfo.Extractsize, fileInfo.Offset, fileInfo.FileId, 0U, fileInfo.UserString, fileInfo.DateTime64, fileInfo.Crc32, fileInfo.CheckSum64, fileInfo.AttributeString) != ulong.MaxValue)
            ++index;
          else
            goto label_9;
        }
        while (index < this.m_filedata.Count);
        goto label_10;
label_9:
        this.m_error_message = string.Format("The file name is too long. \"{0}\"", (object) fullpath);
        return Status.Error;
      }
label_10:
      return Status.ItocInfoMaking;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool addTocFileInfo(CFileInfo file)
    {
      string fullpath = file.ContentFilePath;
      if (this.m_enable_id_and_group)
        fullpath = (string) null;
      this.m_utf.EnableDateTime = this.m_enable_datetime_info;
      if (this.m_utf.AddFileInfo(fullpath, file.Filesize, file.Extractsize, file.Offset, file.FileId, 0U, file.UserString, file.DateTime64, file.Crc32, file.CheckSum64, file.AttributeString) != ulong.MaxValue)
        return true;
      this.m_error_message = string.Format("The file name is too long. \"{0}\"", (object) fullpath);
      return false;
    }

    private unsafe Status ExecHtocInfoMaking() => this.EnableHtoc && this.m_is_toc_rtv_setup && ((!this.m_enable_htoc_fpath_to_toc || !this.m_enable_filename ? 0 : 1) == 1 && !this.addHashTableHtocCommonToToc(true, ref this.m_work_hash_fpath_to_toc) || this.EnableHtocFidToToc && !this.addHashTableHtocCommonToToc(false, ref this.m_work_hash_fid_to_toc)) ? Status.Error : Status.HtocBuilding;

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool testFieldNo(
      byte* cbn_data,
      uint cbn_size,
      uint field_no,
      sbyte* name,
      CRICBN_FTYPE_TAG ftype)
    {
      sbyte* numPtr = name;
      CRICBN_FTYPE_TAG cricbnFtypeTag;
      sbyte* fieldName = \u003CModule\u003E.criCbn_GetFieldName(cbn_data, cbn_size, field_no, &cricbnFtypeTag);
      sbyte num1 = *fieldName;
      sbyte num2 = *name;
      if ((int) num1 >= (int) num2)
      {
        int num3 = (int) ((IntPtr) fieldName - (IntPtr) name);
        while ((int) num1 <= (int) num2)
        {
          if (num1 != (sbyte) 0)
          {
            ++numPtr;
            num1 = *(sbyte*) (num3 + (IntPtr) numPtr);
            num2 = *numPtr;
            if ((int) num1 < (int) num2)
              break;
          }
          else
          {
            if (cricbnFtypeTag == ftype)
              return true;
            this.m_error_message = string.Format("Internal error (mismatch filed type).");
            return false;
          }
        }
      }
      this.m_error_message = string.Format("Internal error (mismatch filed name).");
      return false;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool testFieldNoToc(byte* cbn_data, uint cbn_size) => this.testFieldNo(cbn_data, cbn_size, 0U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07FDBCPIKC\u0040DirName\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 10) && this.testFieldNo(cbn_data, cbn_size, 1U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08FDCJCDOL\u0040FileName\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 10) && this.testFieldNo(cbn_data, cbn_size, 5U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_02OLOABKKD\u0040ID\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4);

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool setupTocRtv(byte* utfbin_ptr, uint utfbin_size)
    {
      this.m_is_toc_rtv_setup = false;
      if (!this.testFieldNoToc(utfbin_ptr, utfbin_size))
        return false;
      CBinary cbinary = this;
      cbinary.m_toc_rtv = \u003CModule\u003E.criCbnRtv_SetupRtv16(cbinary.m_toc_rtvobj, utfbin_ptr, utfbin_size);
      this.m_is_toc_rtv_setup = true;
      return true;
    }

    private unsafe void readTocFpath(sbyte* ls_fpath, int index)
    {
      sbyte* numPtr1 = \u003CModule\u003E.criCbnRtv_ReadString(this.m_toc_rtv, (uint) index, 0U);
      sbyte* numPtr2 = \u003CModule\u003E.criCbnRtv_ReadString(this.m_toc_rtv, (uint) index, 1U);
      \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD;
      if ((IntPtr) numPtr1 != IntPtr.Zero && *numPtr1 != (sbyte) 0)
      {
        \u003CModule\u003E.criCrw_Strcpy((sbyte*) &arrayTypeBy0CaiD, 520U, numPtr1);
        \u003CModule\u003E.criCrw_Strcat((sbyte*) &arrayTypeBy0CaiD, 520U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_01KMDKNFGN\u0040\u003F1\u003F\u0024AA\u0040);
        \u003CModule\u003E.criCrw_Strcat((sbyte*) &arrayTypeBy0CaiD, 520U, numPtr2);
      }
      else
        \u003CModule\u003E.criCrw_Strcpy((sbyte*) &arrayTypeBy0CaiD, 520U, numPtr2);
      \u003CModule\u003E.criCpkAnalyzer_ConvToLargeToSlashStr(ls_fpath, 519, (sbyte*) &arrayTypeBy0CaiD);
    }

    private unsafe void readTocFid(uint* file_id, int index) => *file_id = \u003CModule\u003E.criCbnRtv_ReadUint32(this.m_toc_rtv, (uint) index, 5U);

    private unsafe void readGtocGlinkGroupname(sbyte* groupname, int index)
    {
      sbyte* numPtr = \u003CModule\u003E.criCbnRtv_ReadString(this.m_gtoc_glink_rtv, (uint) index, 0U);
      \u003CModule\u003E.criCpkAnalyzer_ConvToLargeToSlashStr(groupname, 519, numPtr);
    }

    private unsafe void readGtocGinfGaname(sbyte* ganame, int index)
    {
      sbyte* numPtr = \u003CModule\u003E.criCbnRtv_ReadString(this.m_gtoc_ginf_rtv, (uint) index, 0U);
      \u003CModule\u003E.criCpkAnalyzer_ConvToLargeToSlashStr(ganame, 519, numPtr);
    }

    private unsafe void readGtocAttrAname(sbyte* aname, int index)
    {
      sbyte* numPtr = \u003CModule\u003E.criCbnRtv_ReadString(this.m_gtoc_attr_rtv, (uint) index, 0U);
      \u003CModule\u003E.criCpkAnalyzer_ConvToLargeToSlashStr(aname, 519, numPtr);
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool addHashTableHtocCommonToToc([MarshalAs(UnmanagedType.U1)] bool is_fpath, ref byte* work_hash_xxx)
    {
      int num1 = *(int*) this.m_toc_rtv;
      int workSize = \u003CModule\u003E.criCht_CalculateWorkSize(num1);
      byte* numPtr = Utility.Malloc((uint) workSize);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref work_hash_xxx = (int) numPtr;
      if ((IntPtr) numPtr == IntPtr.Zero)
      {
        this.m_error_message = string.Format("Failed to malloc({0}) at addHashTableHtocCommonToToc.", (object) workSize);
        return false;
      }
      void* voidPtr = (void*) numPtr;
      CriChtObjTag criChtObjTag;
      // ISSUE: cast to a function pointer type
      // ISSUE: cast to a function pointer type
      CriChtObjTag* criChtObjTagPtr = !is_fpath ? \u003CModule\u003E.criChtId_CreateBlank(&criChtObjTag, num1, voidPtr, workSize, (__FnPtr<int (void*, uint, int)>) 0, (void*) 0) : \u003CModule\u003E.criCht_CreateBlank(&criChtObjTag, num1, voidPtr, workSize, (__FnPtr<int (void*, sbyte*, int)>) 0, (void*) 0);
      if ((IntPtr) criChtObjTagPtr == IntPtr.Zero)
      {
        this.m_error_message = string.Format("Failed to criCht_CreateBlank.");
        return false;
      }
      int index = 0;
      if (0 < num1)
      {
        do
        {
          if (is_fpath)
          {
            \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD;
            this.readTocFpath((sbyte*) &arrayTypeBy0CaiD, index);
            \u003CModule\u003E.criCht_SetValue(criChtObjTagPtr, (sbyte*) &arrayTypeBy0CaiD, index);
          }
          else
          {
            uint num2 = \u003CModule\u003E.criCbnRtv_ReadUint32(this.m_toc_rtv, (uint) index, 5U);
            \u003CModule\u003E.criChtId_SetValue(criChtObjTagPtr, num2, index);
          }
          ++index;
        }
        while (index < num1);
      }
      void* chtData = \u003CModule\u003E.criCht_GetChtData(criChtObjTagPtr);
      int chtDataSize = \u003CModule\u003E.criCht_GetChtDataSize(criChtObjTagPtr);
      if (is_fpath)
        this.m_utf.AddHashTableHtoc("ht_fpath_to_toc", chtData, chtDataSize);
      else
        this.m_utf.AddHashTableHtoc("ht_fid_to_toc", chtData, chtDataSize);
      \u003CModule\u003E.criCht_Destroy(criChtObjTagPtr);
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool addHashTableHtocFpathToToc() => this.addHashTableHtocCommonToToc(true, ref this.m_work_hash_fpath_to_toc);

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool addHashTableHtocFidToToc() => this.addHashTableHtocCommonToToc(false, ref this.m_work_hash_fid_to_toc);

    private unsafe void releaseHashTableHtocBinaryImage()
    {
      byte* workHashFpathToToc = this.m_work_hash_fpath_to_toc;
      if ((IntPtr) workHashFpathToToc != IntPtr.Zero)
      {
        \u003CModule\u003E.delete\u005B\u005D((void*) workHashFpathToToc);
        this.m_work_hash_fpath_to_toc = (byte*) 0;
      }
      byte* workHashFidToToc = this.m_work_hash_fid_to_toc;
      if ((IntPtr) workHashFidToToc == IntPtr.Zero)
        return;
      \u003CModule\u003E.delete\u005B\u005D((void*) workHashFidToToc);
      this.m_work_hash_fid_to_toc = (byte*) 0;
    }

    private Status ExecItocInfoMaking()
    {
      if (this.m_enable_itoc)
      {
        this.m_filedata.SortById();
        this.addItocFileInfos();
      }
      return Status.GtocInfoMaking;
    }

    private void addItocFileInfos()
    {
      ulong index = 0;
      if (0UL >= this.m_filedata.Count)
        return;
      do
      {
        CFileInfo fileInfo = this.m_filedata.FileInfos[(int) index];
        long num = (long) this.m_utf.AddFileInfoId(fileInfo.Filesize, fileInfo.Extractsize, fileInfo.FileId, (ulong) fileInfo.InfoIndex, fileInfo.Crc32, fileInfo.CheckSum64);
        ++index;
      }
      while (index < this.m_filedata.Count);
    }

    private Status ExecGtocInfoMaking()
    {
      if (this.m_enable_gtoc)
      {
        this.m_filedata.UpdateFileInfoPackingOrder();
        this.m_filedata.SortByPackingOrder();
        this.m_filedata.SetGroupObjectIndex();
      }
      return Status.PreTocBuilding;
    }

    private unsafe Status ExecHgtocInfoMaking()
    {
      if (this.EnableHgtoc && this.m_is_gtoc_rtv_setup)
      {
        if (this.m_enable_hgtoc_gname_to_glink && !this.addHashTableHgtocGnameToGlink() || this.m_enable_hgtoc_ganame_to_ginf && !this.addHashTableHgtocGanameToGinf() || this.EnableHgtocGfpathToGfinf && !this.addHashTableHgtocCommonToGfinf(true, ref this.m_work_hash_gfpath_to_gfinf) || this.EnableHgtocGfidToGfinf && !this.addHashTableHgtocCommonToGfinf(false, ref this.m_work_hash_gfid_to_gfinf))
          return Status.Error;
        this.m_working_message = "Writing HGTOC ...";
      }
      return Status.HgtocBuilding;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool testFieldNoGtoc(byte* cbn_data, uint cbn_size) => this.testFieldNo(cbn_data, cbn_size, 0U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05ICMMBCAL\u0040Glink\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4) && this.testFieldNo(cbn_data, cbn_size, 1U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05EJJAMBKO\u0040Flink\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4) && this.testFieldNo(cbn_data, cbn_size, 2U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04PGOCJFCI\u0040Attr\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4) && this.testFieldNo(cbn_data, cbn_size, 3U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05JMFGLPAJ\u0040Gdata\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 11) && this.testFieldNo(cbn_data, cbn_size, 4U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05FHAKGMKM\u0040Fdata\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 11) && this.testFieldNo(cbn_data, cbn_size, 5U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08KHDPMPAI\u0040AttrData\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 11) && this.testFieldNo(cbn_data, cbn_size, 6U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04OEMOBODJ\u0040Ginf\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4) && this.testFieldNo(cbn_data, cbn_size, 7U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08GIHCBBHF\u0040GinfData\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 11);

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool testFieldNoGtocGlink(byte* cbn_data, uint cbn_size) => this.testFieldNo(cbn_data, cbn_size, 0U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05KBHNPKFD\u0040Gname\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 10) && this.testFieldNo(cbn_data, cbn_size, 1U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 5) && this.testFieldNo(cbn_data, cbn_size, 2U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIMBDOHM\u0040Child\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 5);

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool testFieldNoGtocFlink(byte* cbn_data, uint cbn_size) => this.testFieldNo(cbn_data, cbn_size, 0U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06JEKIFDJD\u0040Aindex\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 2) && this.testFieldNo(cbn_data, cbn_size, 1U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04MDFHGMOB\u0040Next\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 5) && this.testFieldNo(cbn_data, cbn_size, 2U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05GIMBDOHM\u0040Child\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 5) && this.testFieldNo(cbn_data, cbn_size, 3U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09DMHDBJJA\u0040SortFlink\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 5);

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool testFieldNoGtocAttr(byte* cbn_data, uint cbn_size) => this.testFieldNo(cbn_data, cbn_size, 0U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05HHCEBJEO\u0040Aname\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 10) && this.testFieldNo(cbn_data, cbn_size, 1U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05CGDDDONM\u0040Align\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 2) && this.testFieldNo(cbn_data, cbn_size, 2U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DIOAMJFE\u0040Files\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4) && this.testFieldNo(cbn_data, cbn_size, 3U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4);

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool testFieldNoGtocGinf(byte* cbn_data, uint cbn_size) => this.testFieldNo(cbn_data, cbn_size, 0U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06NKCGCMGE\u0040GAname\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 10) && this.testFieldNo(cbn_data, cbn_size, 1U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08BHIEONMC\u0040FileSize\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4) && this.testFieldNo(cbn_data, cbn_size, 2U, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DIOAMJFE\u0040Files\u003F\u0024AA\u0040, (CRICBN_FTYPE_TAG) 4);

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool setupGtocRtv(byte* utfbin_ptr, uint utfbin_size)
    {
      this.m_is_gtoc_rtv_setup = false;
      if (!this.testFieldNoGtoc(utfbin_ptr, utfbin_size))
        return false;
      _CriCbnRtv16 criCbnRtv16;
      _CriCbnRtv* criCbnRtvPtr1 = \u003CModule\u003E.criCbnRtv_SetupRtv16(&criCbnRtv16, utfbin_ptr, utfbin_size);
      if (*(ushort*) ((IntPtr) criCbnRtvPtr1 + 24) < (ushort) 8)
        return false;
      *(int*) ((IntPtr) criCbnRtvPtr1 + 28) = *(int*) ((IntPtr) criCbnRtvPtr1 + 8);
      CriCbnCellValTag criCbnCellValTag1;
      \u003CModule\u003E.criCbnRtv_GetCellData(criCbnRtvPtr1, 0U, &criCbnCellValTag1);
      CriCbnCellValTag criCbnCellValTag2;
      \u003CModule\u003E.criCbnRtv_GetCellData(criCbnRtvPtr1, 1U, &criCbnCellValTag2);
      CriCbnCellValTag criCbnCellValTag3;
      \u003CModule\u003E.criCbnRtv_GetCellData(criCbnRtvPtr1, 2U, &criCbnCellValTag3);
      CriCbnCellValTag criCbnCellValTag4;
      \u003CModule\u003E.criCbnRtv_GetCellData(criCbnRtvPtr1, 3U, &criCbnCellValTag4);
      CriCbnCellValTag criCbnCellValTag5;
      \u003CModule\u003E.criCbnRtv_GetCellData(criCbnRtvPtr1, 4U, &criCbnCellValTag5);
      CriCbnCellValTag criCbnCellValTag6;
      \u003CModule\u003E.criCbnRtv_GetCellData(criCbnRtvPtr1, 5U, &criCbnCellValTag6);
      CriCbnCellValTag criCbnCellValTag7;
      \u003CModule\u003E.criCbnRtv_GetCellData(criCbnRtvPtr1, 6U, &criCbnCellValTag7);
      CriCbnCellValTag criCbnCellValTag8;
      \u003CModule\u003E.criCbnRtv_GetCellData(criCbnRtvPtr1, 7U, &criCbnCellValTag8);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      this.m_gtoc_glink_num = (uint) ^(int&) ((IntPtr) &criCbnCellValTag1 + 8);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      this.m_gtoc_flink_num = (uint) ^(int&) ((IntPtr) &criCbnCellValTag2 + 8);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      this.m_gtoc_attr_num = (uint) ^(int&) ((IntPtr) &criCbnCellValTag3 + 8);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      this.m_gtoc_ginf_num = (uint) ^(int&) ((IntPtr) &criCbnCellValTag7 + 8);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (!this.testFieldNoGtocGlink((byte*) ^(int&) ((IntPtr) &criCbnCellValTag4 + 8), (uint) ^(int&) ((IntPtr) &criCbnCellValTag4 + 12)) || !this.testFieldNoGtocFlink((byte*) ^(int&) ((IntPtr) &criCbnCellValTag5 + 8), (uint) ^(int&) ((IntPtr) &criCbnCellValTag5 + 12)) || !this.testFieldNoGtocAttr((byte*) ^(int&) ((IntPtr) &criCbnCellValTag6 + 8), (uint) ^(int&) ((IntPtr) &criCbnCellValTag6 + 12)) || !this.testFieldNoGtocGinf((byte*) ^(int&) ((IntPtr) &criCbnCellValTag8 + 8), (uint) ^(int&) ((IntPtr) &criCbnCellValTag8 + 12)))
        return false;
      CBinary cbinary1 = this;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      cbinary1.m_gtoc_glink_rtv = \u003CModule\u003E.criCbnRtv_SetupRtv16(cbinary1.m_gtoc_glink_rtvobj, (byte*) ^(int&) ((IntPtr) &criCbnCellValTag4 + 8), (uint) ^(int&) ((IntPtr) &criCbnCellValTag4 + 12));
      CBinary cbinary2 = this;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      cbinary2.m_gtoc_flink_rtv = \u003CModule\u003E.criCbnRtv_SetupRtv16(cbinary2.m_gtoc_flink_rtvobj, (byte*) ^(int&) ((IntPtr) &criCbnCellValTag5 + 8), (uint) ^(int&) ((IntPtr) &criCbnCellValTag5 + 12));
      CBinary cbinary3 = this;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      cbinary3.m_gtoc_attr_rtv = \u003CModule\u003E.criCbnRtv_SetupRtv16(cbinary3.m_gtoc_attr_rtvobj, (byte*) ^(int&) ((IntPtr) &criCbnCellValTag6 + 8), (uint) ^(int&) ((IntPtr) &criCbnCellValTag6 + 12));
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      _CriCbnRtv* criCbnRtvPtr2 = \u003CModule\u003E.criCbnRtv_SetupRtv16(this.m_gtoc_ginf_rtvobj, (byte*) ^(int&) ((IntPtr) &criCbnCellValTag8 + 8), (uint) ^(int&) ((IntPtr) &criCbnCellValTag8 + 12));
      this.m_gtoc_ginf_rtv = criCbnRtvPtr2;
      uint gtocGlinkNum = this.m_gtoc_glink_num;
      if (*(int*) this.m_gtoc_glink_rtv != (int) gtocGlinkNum || *(int*) this.m_gtoc_flink_rtv != (int) this.m_gtoc_flink_num || *(int*) this.m_gtoc_attr_rtv != (int) this.m_gtoc_attr_num || *(int*) criCbnRtvPtr2 != (int) this.m_gtoc_ginf_num)
        return false;
      uint num = 1;
      if (1U < gtocGlinkNum)
      {
        do
        {
          _CriCbnRtv* gtocGlinkRtv = this.m_gtoc_glink_rtv;
          *(int*) ((IntPtr) gtocGlinkRtv + 28) = *(int*) ((IntPtr) gtocGlinkRtv + 20) * (int) num + *(int*) ((IntPtr) gtocGlinkRtv + 8);
          CriCbnCellValTag criCbnCellValTag9;
          \u003CModule\u003E.criCbnRtv_GetCellData(this.m_gtoc_glink_rtv, 2U, &criCbnCellValTag9);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(int&) ((IntPtr) &criCbnCellValTag9 + 8) >= 0)
            ++num;
          else
            goto label_11;
        }
        while (num < this.m_gtoc_glink_num);
        goto label_12;
label_11:
        return true;
      }
label_12:
      this.m_is_gtoc_rtv_setup = true;
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool addHashTableHgtocGnameToGlink()
    {
      int num1 = *(int*) this.m_gtoc_glink_rtv;
      int workSize = \u003CModule\u003E.criCht_CalculateWorkSize(num1);
      byte* numPtr1 = Utility.Malloc((uint) workSize);
      this.m_work_hash_gname_to_glink = numPtr1;
      if ((IntPtr) numPtr1 == IntPtr.Zero)
      {
        this.m_error_message = string.Format("Failed to malloc({0}) at addHashTableHgtocGnameToGlink.", (object) workSize);
        return false;
      }
      CriChtObjTag criChtObjTag;
      // ISSUE: cast to a function pointer type
      CriChtObjTag* blank = \u003CModule\u003E.criCht_CreateBlank(&criChtObjTag, num1, (void*) numPtr1, workSize, (__FnPtr<int (void*, sbyte*, int)>) 0, (void*) 0);
      if ((IntPtr) blank == IntPtr.Zero)
      {
        this.m_error_message = string.Format("Failed to criCht_CreateBlank.");
        return false;
      }
      int num2 = 1;
      if (1 < num1)
      {
        do
        {
          sbyte* numPtr2 = \u003CModule\u003E.criCbnRtv_ReadString(this.m_gtoc_glink_rtv, (uint) num2, 0U);
          \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD;
          \u003CModule\u003E.criCpkAnalyzer_ConvToLargeToSlashStr((sbyte*) &arrayTypeBy0CaiD, 519, numPtr2);
          \u003CModule\u003E.criCht_SetValue(blank, (sbyte*) &arrayTypeBy0CaiD, num2);
          ++num2;
        }
        while (num2 < num1);
      }
      this.m_utf.AddHashTableHgtoc("ht_gname_to_glink", \u003CModule\u003E.criCht_GetChtData(blank), \u003CModule\u003E.criCht_GetChtDataSize(blank));
      \u003CModule\u003E.criCht_Destroy(blank);
      return true;
    }

    private unsafe CriChtObjTag* common_criCht_CreateBlank(
      [MarshalAs(UnmanagedType.U1)] bool is_gfpath,
      CriChtObjTag* chtobj,
      int files,
      void* work_hash_xxx,
      int worksize)
    {
      // ISSUE: cast to a function pointer type
      // ISSUE: cast to a function pointer type
      return is_gfpath ? \u003CModule\u003E.criCht_CreateBlank(chtobj, files, work_hash_xxx, worksize, (__FnPtr<int (void*, sbyte*, int)>) 0, (void*) 0) : \u003CModule\u003E.criChtId_CreateBlank(chtobj, files, work_hash_xxx, worksize, (__FnPtr<int (void*, uint, int)>) 0, (void*) 0);
    }

    private unsafe void common_criCht_SetValue(
      [MarshalAs(UnmanagedType.U1)] bool is_gfpath,
      CriChtObjTag* chthn,
      sbyte* filepath,
      uint fid,
      int flink_index_local)
    {
      if (is_gfpath)
        \u003CModule\u003E.criCht_SetValue(chthn, filepath, flink_index_local);
      else
        \u003CModule\u003E.criChtId_SetValue(chthn, fid, flink_index_local);
    }

    private unsafe void common_readTocFpathOrFid(
      [MarshalAs(UnmanagedType.U1)] bool is_fpath,
      sbyte* filepath,
      uint* fid,
      int toc_index)
    {
      if (is_fpath)
        this.readTocFpath(filepath, toc_index);
      else
        *fid = \u003CModule\u003E.criCbnRtv_ReadUint32(this.m_toc_rtv, (uint) toc_index, 5U);
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool addHashTableHgtocCommonToGfinf([MarshalAs(UnmanagedType.U1)] bool is_gfpath, ref byte** work_hash_xxx)
    {
      int num1 = *(int*) this.m_gtoc_glink_rtv;
      int num2 = *(int*) this.m_gtoc_flink_rtv;
      int num3 = *(int*) this.m_gtoc_attr_rtv;
      int num4 = *(int*) this.m_gtoc_ginf_rtv;
      int size = num4 << 2;
      byte* numPtr = Utility.Malloc((uint) size);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref work_hash_xxx = (int) numPtr;
      if ((IntPtr) numPtr == IntPtr.Zero)
      {
        this.m_error_message = string.Format("Failed to malloc({0}) at addHashTableHgtocCommonToGfinf #1.", (object) size);
        return false;
      }
      CriChtObjTag* ptr1 = (CriChtObjTag*) Utility.Malloc((uint) ((num3 + 1) * 20));
      CriChtObjTag** ptr2 = (CriChtObjTag**) Utility.Malloc((uint) (num3 * 4 + 4));
      int* ptr3 = (int*) Utility.Malloc((uint) (num3 * 4 + 4));
      int* ptr4 = (int*) Utility.Malloc((uint) (num3 * 4 + 4));
      int num5 = 0;
      int index1 = 1;
      if (1 < num1)
      {
        int workSize;
        do
        {
          \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD1;
          this.readGtocGlinkGroupname((sbyte*) &arrayTypeBy0CaiD1, index1);
          int num6 = (int) \u003CModule\u003E.criCbnRtv_ReadUint32(this.m_gtoc_glink_rtv, (uint) index1, 2U);
          int num7 = 0;
          int num8;
          if (num5 < num4)
          {
            int index2 = num5;
            CriChtObjTag** criChtObjTagPtr = ptr2;
            CriChtObjTag* chtobj = ptr1;
            int num9 = (int) ((IntPtr) ptr3 - (IntPtr) ptr2);
            do
            {
              \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD2;
              this.readGtocGinfGaname((sbyte*) &arrayTypeBy0CaiD2, index2);
              if (this.compareGroupname((sbyte*) &arrayTypeBy0CaiD1, (sbyte*) &arrayTypeBy0CaiD2) == 0)
              {
                int files = (int) \u003CModule\u003E.criCbnRtv_ReadUint32(this.m_gtoc_ginf_rtv, (uint) index2, 2U);
                int aindexFromGaname = this.getAindexFromGaname((sbyte*) &arrayTypeBy0CaiD2);
                *(int*) (num9 + (IntPtr) criChtObjTagPtr) = aindexFromGaname;
                if (aindexFromGaname == -1)
                  num8 = num7;
                workSize = \u003CModule\u003E.criCht_CalculateWorkSize(files);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                *(int*) (index2 * 4 + ^(int&) ref work_hash_xxx) = (int) Utility.Malloc((uint) workSize);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                int num10 = ^(int&) ref work_hash_xxx + index2 * 4;
                if (*(int*) num10 != 0)
                {
                  CriChtObjTag* blank = this.common_criCht_CreateBlank(is_gfpath, chtobj, files, (void*) *(int*) num10, workSize);
                  *(int*) criChtObjTagPtr = (int) blank;
                  if ((IntPtr) blank != IntPtr.Zero)
                  {
                    ++num7;
                    chtobj += 20;
                    criChtObjTagPtr += 4;
                    ++index2;
                  }
                  else
                    goto label_32;
                }
                else
                  goto label_31;
              }
              else
                break;
            }
            while (index2 < num4);
          }
          int num11 = num7 + num5;
          if (num6 < num2)
          {
            while ((int) \u003CModule\u003E.criCbnRtv_ReadUint32(this.m_gtoc_flink_rtv, (uint) num6, 2U) < 0)
            {
              ++num6;
              if (num6 >= num2)
                break;
            }
          }
          int num12 = num8 * 4;
          *(int*) (num12 + (IntPtr) ptr4) = num6;
          ushort num13 = ushort.MaxValue;
          int num14 = 0;
          int flink_index_local = 0;
          if (num6 < num2)
          {
            int num15 = num6;
            do
            {
              ushort num16 = \u003CModule\u003E.criCbnRtv_ReadUint16(this.m_gtoc_flink_rtv, (uint) num15, 0U);
              int toc_index = (int) \u003CModule\u003E.criCbnRtv_ReadUint32(this.m_gtoc_flink_rtv, (uint) num15, 2U);
              if (toc_index >= 0)
              {
                if ((int) num13 != (int) num16)
                {
                  num13 = num16;
                  num7 = 0;
                  if (num5 < num11)
                  {
                    int num17 = num5;
                    while (*(int*) (num7 * 4 + (IntPtr) ptr3) != (int) num16)
                    {
                      ++num7;
                      ++num17;
                      if (num17 >= num11)
                        break;
                    }
                  }
                  num14 = flink_index_local;
                  *(int*) (num7 * 4 + (IntPtr) ptr4) = num15;
                }
                \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD3;
                uint fid;
                this.common_readTocFpathOrFid(is_gfpath, (sbyte*) &arrayTypeBy0CaiD3, &fid, toc_index);
                this.common_criCht_SetValue(is_gfpath, (CriChtObjTag*) *(int*) (num12 + (IntPtr) ptr2), (sbyte*) &arrayTypeBy0CaiD3, fid, flink_index_local);
                this.common_criCht_SetValue(is_gfpath, (CriChtObjTag*) *(int*) (num7 * 4 + (IntPtr) ptr2), (sbyte*) &arrayTypeBy0CaiD3, fid, flink_index_local - num14);
                ++flink_index_local;
                ++num15;
              }
              else
                break;
            }
            while (num15 < num2);
          }
          int num18 = 0;
          if (num5 < num11)
          {
            int index3 = num5;
            CriChtObjTag** criChtObjTagPtr = ptr2;
            int num19 = (int) ((IntPtr) ptr4 - (IntPtr) ptr2);
            do
            {
              void* chtData = \u003CModule\u003E.criCht_GetChtData((CriChtObjTag*) *(int*) criChtObjTagPtr);
              int chtDataSize = \u003CModule\u003E.criCht_GetChtDataSize((CriChtObjTag*) *(int*) criChtObjTagPtr);
              if (is_gfpath)
                this.m_utf.SetHashTableHgtocGfpathToGfinf(index3, chtData, chtDataSize, *(int*) (num19 + (IntPtr) criChtObjTagPtr));
              else
                this.m_utf.SetHashTableHgtocGfidToGfinf(index3, chtData, chtDataSize, *(int*) (num19 + (IntPtr) criChtObjTagPtr));
              \u003CModule\u003E.criCht_Destroy((CriChtObjTag*) *(int*) criChtObjTagPtr);
              ++num18;
              criChtObjTagPtr += 4;
              ++index3;
            }
            while (index3 < num11);
          }
          num5 = num18 + num5;
          ++index1;
        }
        while (index1 < num1);
        goto label_33;
label_31:
        this.m_error_message = string.Format("Failed to malloc({0}) at addHashTableHgtocCommonToGfinf #2.", (object) workSize);
        return false;
label_32:
        this.m_error_message = string.Format("Failed to criCht_CreateBlank.");
        return false;
      }
label_33:
      Utility.Free((byte*) ptr4);
      Utility.Free((byte*) ptr3);
      Utility.Free((byte*) ptr2);
      Utility.Free((byte*) ptr1);
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool addHashTableHgtocGanameToGinf()
    {
      int num1 = *(int*) this.m_gtoc_ginf_rtv;
      int workSize = \u003CModule\u003E.criCht_CalculateWorkSize(num1);
      byte* numPtr1 = Utility.Malloc((uint) workSize);
      this.m_work_hash_ganame_to_ginf = numPtr1;
      if ((IntPtr) numPtr1 == IntPtr.Zero)
      {
        this.m_error_message = string.Format("Failed to malloc({0}) at addHashTableHgtocGanameToGinf.", (object) workSize);
        return false;
      }
      CriChtObjTag criChtObjTag;
      // ISSUE: cast to a function pointer type
      CriChtObjTag* blank = \u003CModule\u003E.criCht_CreateBlank(&criChtObjTag, num1, (void*) numPtr1, workSize, (__FnPtr<int (void*, sbyte*, int)>) 0, (void*) 0);
      if ((IntPtr) blank == IntPtr.Zero)
      {
        this.m_error_message = string.Format("Failed to criCht_CreateBlank.");
        return false;
      }
      int num2 = 0;
      if (0 < num1)
      {
        do
        {
          sbyte* numPtr2 = \u003CModule\u003E.criCbnRtv_ReadString(this.m_gtoc_ginf_rtv, (uint) num2, 0U);
          \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD;
          \u003CModule\u003E.criCpkAnalyzer_ConvToLargeToSlashStr((sbyte*) &arrayTypeBy0CaiD, 519, numPtr2);
          \u003CModule\u003E.criCht_SetValue(blank, (sbyte*) &arrayTypeBy0CaiD, num2);
          ++num2;
        }
        while (num2 < num1);
      }
      this.m_utf.AddHashTableHgtoc("ht_ganame_to_ginf", \u003CModule\u003E.criCht_GetChtData(blank), \u003CModule\u003E.criCht_GetChtDataSize(blank));
      \u003CModule\u003E.criCht_Destroy(blank);
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool addHashTableHgtocGfpathToGfinf() => this.addHashTableHgtocCommonToGfinf(true, ref this.m_work_hash_gfpath_to_gfinf);

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool addHashTableHgtocGfidToGfinf() => this.addHashTableHgtocCommonToGfinf(false, ref this.m_work_hash_gfid_to_gfinf);

    private unsafe int compareGroupname(sbyte* groupname, sbyte* ganame)
    {
      int num1 = 0;
      sbyte num2 = *ganame;
      \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD;
      if (num2 != (sbyte) 0)
      {
        sbyte num3 = num2;
        while (num3 != (sbyte) 58)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(sbyte&) (num1 + (IntPtr) &arrayTypeBy0CaiD) = num3;
          ++num1;
          num3 = *(sbyte*) (num1 + (IntPtr) ganame);
          if (num3 == (sbyte) 0)
            break;
        }
      }
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) (num1 + (IntPtr) &arrayTypeBy0CaiD) = (sbyte) 0;
      sbyte* numPtr1 = (sbyte*) &arrayTypeBy0CaiD;
      sbyte* numPtr2 = groupname;
      int num4 = 0;
      sbyte num5 = *groupname;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      sbyte num6 = ^(sbyte&) ref arrayTypeBy0CaiD;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if ((int) num5 >= (int) ^(sbyte&) ref arrayTypeBy0CaiD)
      {
        while ((int) num5 <= (int) num6)
        {
          if (num5 != (sbyte) 0)
          {
            ++numPtr2;
            ++numPtr1;
            num5 = *numPtr2;
            num6 = *numPtr1;
            if ((int) num5 < (int) num6)
              goto label_8;
          }
          else
            goto label_10;
        }
        num4 = 1;
        goto label_10;
      }
label_8:
      num4 = -1;
label_10:
      return num4;
    }

    private unsafe int getAindexFromGaname(sbyte* ganame)
    {
      int num1 = 0;
      sbyte num2 = *ganame;
      if (num2 != (sbyte) 0)
      {
        sbyte num3 = num2;
        while (num3 != (sbyte) 58)
        {
          ++num1;
          num3 = *(sbyte*) (num1 + (IntPtr) ganame);
          if (num3 == (sbyte) 0)
            goto label_4;
        }
        sbyte* numPtr1 = ganame + num1 + 1;
        int num4 = *(int*) this.m_gtoc_attr_rtv;
        int aindexFromGaname = 0;
        if (0 < num4)
        {
          do
          {
            sbyte* numPtr2 = \u003CModule\u003E.criCbnRtv_ReadString(this.m_gtoc_attr_rtv, (uint) aindexFromGaname, 0U);
            \u0024ArrayType\u0024\u0024\u0024BY0CAI\u0040D arrayTypeBy0CaiD;
            \u003CModule\u003E.criCpkAnalyzer_ConvToLargeToSlashStr((sbyte*) &arrayTypeBy0CaiD, 519, numPtr2);
            sbyte num5 = *numPtr1;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            if (num5 != (sbyte) 0 || ^(sbyte&) ref arrayTypeBy0CaiD != (sbyte) 0)
            {
              sbyte* numPtr3 = (sbyte*) &arrayTypeBy0CaiD;
              sbyte* numPtr4 = numPtr1;
              sbyte num6 = num5;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              sbyte num7 = ^(sbyte&) ref arrayTypeBy0CaiD;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              if ((int) num6 >= (int) ^(sbyte&) ref arrayTypeBy0CaiD)
              {
                do
                {
                  if ((int) num6 <= (int) num7)
                  {
                    if (num6 != (sbyte) 0)
                    {
                      ++numPtr4;
                      ++numPtr3;
                      num6 = *numPtr4;
                      num7 = *numPtr3;
                    }
                    else
                      goto label_13;
                  }
                  else
                    break;
                }
                while ((int) num6 >= (int) num7);
              }
              ++aindexFromGaname;
            }
            else
              goto label_12;
          }
          while (aindexFromGaname < num4);
          goto label_14;
label_12:
          return aindexFromGaname;
label_13:
          return aindexFromGaname;
        }
label_14:
        return aindexFromGaname;
      }
label_4:
      return -1;
    }

    private unsafe void releaseHashTableHgtocBinaryImage()
    {
      byte* hashGnameToGlink = this.m_work_hash_gname_to_glink;
      if ((IntPtr) hashGnameToGlink != IntPtr.Zero)
      {
        Utility.Free(hashGnameToGlink);
        this.m_work_hash_gname_to_glink = (byte*) 0;
      }
      byte* hashGanameToGinf = this.m_work_hash_ganame_to_ginf;
      if ((IntPtr) hashGanameToGinf != IntPtr.Zero)
      {
        Utility.Free(hashGanameToGinf);
        this.m_work_hash_ganame_to_ginf = (byte*) 0;
      }
      if ((IntPtr) this.m_work_hash_gfpath_to_gfinf != IntPtr.Zero)
      {
        int num = 0;
        if (0 < this.m_hash_array_nelem)
        {
          do
          {
            Utility.Free((byte*) *(int*) (num * 4 + (IntPtr) this.m_work_hash_gfpath_to_gfinf));
            ++num;
          }
          while (num < this.m_hash_array_nelem);
        }
        Utility.Free((byte*) this.m_work_hash_gfpath_to_gfinf);
        this.m_work_hash_gfpath_to_gfinf = (byte**) 0;
      }
      if ((IntPtr) this.m_work_hash_gfid_to_gfinf == IntPtr.Zero)
        return;
      int num1 = 0;
      if (0 < this.m_hash_array_nelem)
      {
        do
        {
          Utility.Free((byte*) *(int*) (num1 * 4 + (IntPtr) this.m_work_hash_gfid_to_gfinf));
          ++num1;
        }
        while (num1 < this.m_hash_array_nelem);
      }
      Utility.Free((byte*) this.m_work_hash_gfid_to_gfinf);
      this.m_work_hash_gfid_to_gfinf = (byte**) 0;
    }

    private unsafe Status ExecHgtocBuilding()
    {
      if (this.EnableHgtoc && this.m_is_gtoc_rtv_setup)
      {
        Debug.WriteLine("---- Hgtoc");
        uint writesize;
        void* writepointer = this.m_utf.MakeHgtocImage(&writesize, this.m_mask, true);
        try
        {
          ulong position;
          if (this.m_calc_hgtoc_pos != 0UL)
          {
            position = this.m_writer.Position;
            this.m_writer.Position = this.m_calc_hgtoc_pos;
          }
          Debug.WriteLine("$$$ pre hgtoc ofs = 0x" + this.m_writer.Position.ToString("X"));
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
          CBinary cbinary1 = this;
          cbinary1.m_hgtoc_offset = cbinary1.m_writer.Position - (ulong) this.m_pre_header_offset;
          this.m_utf.SetHgtocImageOffset(this.m_hgtoc_offset);
          Debug.WriteLine("$$$ hgtoc ofs = 0x" + this.m_writer.Position.ToString("X"));
          this.m_writer.Write(this.m_utf.GetHgtocIffPtr(), (ulong) this.m_utf.GetIffSize());
          this.m_writer.WaitForComplete();
          this.m_writer.Write(writepointer, (ulong) writesize);
          this.m_writer.WaitForComplete();
          CBinary cbinary2 = this;
          cbinary2.m_hgtoc_size = (ulong) (cbinary2.m_utf.GetIffSize() + writesize);
          uint calcHgtocSize = this.m_calc_hgtoc_size;
          if (calcHgtocSize != 0U)
          {
            ulong hgtocSize = this.m_hgtoc_size;
            if ((ulong) calcHgtocSize < hgtocSize)
            {
              ulong num = hgtocSize;
              this.m_error_message = "HTOCの事前計算サイズと実際のサイズが異なる" + calcHgtocSize.ToString() + "/" + num.ToString();
              return Status.Error;
            }
          }
          if (this.m_calc_hgtoc_pos != 0UL)
            this.m_writer.Position = position;
        }
        catch (Exception ex)
        {
          this.m_writer.Close();
          ApplicationException applicationException = new ApplicationException("ExecHgtocBuilding : " + ex.Message);
        }
        finally
        {
          this.m_utf.ReleaseHgtocImageBuffer();
        }
        if (this.m_enable_etoc)
          this.m_working_message = "Writing Extra TOC ...";
      }
      return Status.EtocBuilding;
    }

    private Status ExecPreTocBuilding() => this.m_afs2_mode ? this.ExecHeaderRewritingAfs2() : Status.TocBuilding;

    private unsafe Status ExecTocBuilding()
    {
      if (this.m_enable_toc)
      {
        Debug.WriteLine("---- Toc");
        uint num1;
        void* voidPtr = this.m_utf.MakeTocImage(&num1, this.m_mask, true);
        try
        {
          ulong position;
          if (this.m_calc_toc_pos != 0UL)
          {
            position = this.m_writer.Position;
            this.m_writer.Position = this.m_calc_toc_pos;
          }
          Debug.WriteLine("$$$ pre toc ofs = 0x" + this.m_writer.Position.ToString("X"));
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
          CBinary cbinary1 = this;
          cbinary1.m_toc_offset = cbinary1.m_writer.Position - (ulong) this.m_pre_header_offset;
          this.m_utf.SetTocImageOffset(this.m_toc_offset);
          Debug.WriteLine("$$$ toc ofs = 0x" + this.m_writer.Position.ToString("X"));
          this.m_writer.Write(this.m_utf.GetTocIffPtr(), (ulong) this.m_utf.GetIffSize());
          this.m_writer.WaitForComplete();
          this.m_writer.Write(voidPtr, (ulong) num1);
          this.m_writer.WaitForComplete();
          CBinary cbinary2 = this;
          cbinary2.m_toc_size = (ulong) (cbinary2.m_utf.GetIffSize() + num1);
          uint calcTocSize = this.m_calc_toc_size;
          if (calcTocSize != 0U)
          {
            ulong tocSize = this.m_toc_size;
            if ((ulong) calcTocSize < tocSize)
            {
              ulong num2 = tocSize;
              this.m_error_message = "TOCの事前計算サイズと実際のサイズが異なる" + calcTocSize.ToString() + "/" + num2.ToString();
              return Status.Error;
            }
          }
          if (this.m_calc_toc_pos != 0UL)
            this.m_writer.Position = position;
        }
        catch (Exception ex)
        {
          this.m_writer.Close();
          ApplicationException applicationException = new ApplicationException("ExecTocBuilding : " + ex.Message);
        }
        finally
        {
          this.m_utf.ReleaseTocImageBuffer();
        }
        if (this.EnableHtoc || this.EnableHgtoc)
        {
          if (!this.setupTocRtv((byte*) voidPtr, num1))
            return Status.Error;
          if (this.m_is_toc_rtv_setup)
            this.m_working_message = "Writing HTOC Info ...";
        }
      }
      return Status.HtocInfoMaking;
    }

    private unsafe Status ExecHtocBuilding()
    {
      if (this.EnableHtoc && this.m_is_toc_rtv_setup)
      {
        Debug.WriteLine("---- Htoc");
        uint writesize;
        void* writepointer = this.m_utf.MakeHtocImage(&writesize, this.m_mask, true);
        try
        {
          ulong position;
          if (this.m_calc_htoc_pos != 0UL)
          {
            position = this.m_writer.Position;
            this.m_writer.Position = this.m_calc_htoc_pos;
          }
          Debug.WriteLine("$$$ pre htoc ofs = 0x" + this.m_writer.Position.ToString("X"));
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
          CBinary cbinary1 = this;
          cbinary1.m_htoc_offset = cbinary1.m_writer.Position - (ulong) this.m_pre_header_offset;
          this.m_utf.SetHtocImageOffset(this.m_htoc_offset);
          Debug.WriteLine("$$$ htoc ofs = 0x" + this.m_writer.Position.ToString("X"));
          this.m_writer.Write(this.m_utf.GetHtocIffPtr(), (ulong) this.m_utf.GetIffSize());
          this.m_writer.WaitForComplete();
          this.m_writer.Write(writepointer, (ulong) writesize);
          this.m_writer.WaitForComplete();
          CBinary cbinary2 = this;
          cbinary2.m_htoc_size = (ulong) (cbinary2.m_utf.GetIffSize() + writesize);
          uint calcHtocSize = this.m_calc_htoc_size;
          if (calcHtocSize != 0U)
          {
            ulong htocSize = this.m_htoc_size;
            if ((ulong) calcHtocSize < htocSize)
            {
              ulong num = htocSize;
              this.m_error_message = "HTOCの事前計算サイズと実際のサイズが異なる" + calcHtocSize.ToString() + "/" + num.ToString();
              return Status.Error;
            }
          }
          if (this.m_calc_htoc_pos != 0UL)
            this.m_writer.Position = position;
        }
        catch (Exception ex)
        {
          this.m_writer.Close();
          ApplicationException applicationException = new ApplicationException("ExecHtocBuilding : " + ex.Message);
        }
        finally
        {
          this.m_utf.ReleaseHtocImageBuffer();
          this.releaseHashTableHtocBinaryImage();
        }
        if (this.m_enable_itoc)
          this.m_working_message = "Writing ID Info ...";
      }
      return Status.ItocBuilding;
    }

    private unsafe Status ExecItocBuilding()
    {
      if (this.m_enable_itoc)
      {
        uint writesize;
        void* writepointer = !this.m_enable_toc ? this.m_utf.MakeItocImageCompact(&writesize, this.m_mask, true) : this.m_utf.MakeItocImageExtra(&writesize, this.m_mask, true);
        try
        {
          ulong position;
          if (this.m_calc_itoc_pos != 0UL)
          {
            position = this.m_writer.Position;
            this.m_writer.Position = this.m_calc_itoc_pos;
          }
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
          CBinary cbinary1 = this;
          cbinary1.m_itoc_offset = cbinary1.m_writer.Position - (ulong) this.m_pre_header_offset;
          this.m_utf.SetItocImageOffset(this.m_itoc_offset);
          this.m_writer.Write(this.m_utf.GetItocIffPtr(), (ulong) this.m_utf.GetIffSize());
          this.m_writer.WaitForComplete();
          this.m_writer.Write(writepointer, (ulong) writesize);
          this.m_writer.WaitForComplete();
          CBinary cbinary2 = this;
          cbinary2.m_itoc_size = (ulong) (cbinary2.m_utf.GetIffSize() + writesize);
          uint calcItocSize = this.m_calc_itoc_size;
          if (calcItocSize != 0U)
          {
            ulong itocSize = this.m_itoc_size;
            if ((ulong) calcItocSize < itocSize)
            {
              ulong num = itocSize;
              this.m_error_message = "ITOCの事前計算サイズと実際のサイズが異なる" + calcItocSize.ToString() + "/" + num.ToString();
              return Status.Error;
            }
          }
          if (this.m_calc_itoc_pos != 0UL)
            this.m_writer.Position = position;
        }
        catch (Exception ex)
        {
          this.m_writer.Close();
          ApplicationException applicationException = new ApplicationException(ex.Message);
        }
        finally
        {
          if (this.m_enable_toc)
            this.m_utf.ReleaseITocImageExtraBuffer();
          else
            this.m_utf.ReleaseITocImageCompactBuffer();
        }
      }
      if (this.m_enable_gtoc)
        this.m_working_message = "Writing GTOC ...";
      return Status.GtocBuilding;
    }

    private unsafe Status ExecGtocBuilding()
    {
      if (this.m_enable_gtoc)
      {
        this.m_filedata.GroupingManager.DebugPrintGroupInfo();
        uint num1;
        void* voidPtr = this.m_utf.MakeGtocImage(this.m_filedata.GroupingManager, &num1, this.m_mask, true);
        try
        {
          ulong position;
          if (this.m_calc_gtoc_pos != 0UL)
          {
            position = this.m_writer.Position;
            this.m_writer.Position = this.m_calc_gtoc_pos;
          }
          Debug.WriteLine("$$$ pre gtoc ofs = 0x" + this.m_writer.Position.ToString("X"));
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
          CBinary cbinary1 = this;
          cbinary1.m_gtoc_offset = cbinary1.m_writer.Position - (ulong) this.m_pre_header_offset;
          this.m_utf.SetGtocImageOffset(this.m_gtoc_offset);
          Debug.WriteLine("$$$ gtoc ofs = 0x" + this.m_writer.Position.ToString("X"));
          this.m_writer.Write(this.m_utf.GetGtocIffPtr(), (ulong) this.m_utf.GetIffSize());
          this.m_writer.WaitForComplete();
          this.m_writer.Write(voidPtr, (ulong) num1);
          this.m_writer.WaitForComplete();
          CBinary cbinary2 = this;
          cbinary2.m_gtoc_size = (ulong) (cbinary2.m_utf.GetIffSize() + num1);
          uint calcGtocSize = this.m_calc_gtoc_size;
          if (calcGtocSize != 0U)
          {
            ulong gtocSize = this.m_gtoc_size;
            if ((ulong) calcGtocSize < gtocSize)
            {
              ulong num2 = gtocSize;
              this.m_error_message = "GTOCの事前計算サイズと実際のサイズが異なる " + calcGtocSize.ToString() + " / " + num2.ToString();
              return Status.Error;
            }
          }
          if (this.m_calc_gtoc_pos != 0UL)
            this.m_writer.Position = position;
        }
        catch (Exception ex)
        {
          this.m_writer.Close();
          ApplicationException applicationException = new ApplicationException(ex.Message);
        }
        finally
        {
          this.m_utf.ReleaseGtocImageBuffer();
        }
        if (this.EnableHgtoc)
        {
          if (!this.setupGtocRtv((byte*) voidPtr, num1))
            return Status.Error;
          if (this.m_is_gtoc_rtv_setup)
            this.m_working_message = "Writing HGTOC ...";
        }
      }
      return Status.HgtocInfoMaking;
    }

    private unsafe Status ExecEtocBuilding()
    {
      if (this.m_enable_etoc)
      {
        uint writesize;
        void* writepointer = this.m_utf.MakeEtocImage(&writesize, this.m_basedir, this.m_mask);
        Debug.WriteLine("---- EToc");
        try
        {
          long alignment = (long) this.m_writer.WritePaddingToAlignment((ulong) this.m_data_align);
          CBinary cbinary1 = this;
          cbinary1.m_etoc_offset = cbinary1.m_writer.Position - (ulong) this.m_pre_header_offset;
          this.m_utf.SetEtocImageOffset(this.m_etoc_offset);
          Debug.WriteLine("$$$ etoc ofs = 0x" + this.m_writer.Position.ToString("X"));
          this.m_writer.Write(this.m_utf.GetEtocIffPtr(), (ulong) this.m_utf.GetIffSize());
          this.m_writer.WaitForComplete();
          this.m_writer.Write(writepointer, (ulong) writesize);
          this.m_writer.WaitForComplete();
          CBinary cbinary2 = this;
          cbinary2.m_etoc_size = (ulong) (cbinary2.m_utf.GetIffSize() + writesize);
        }
        catch (Exception ex)
        {
          this.m_writer.Close();
          ApplicationException applicationException = new ApplicationException(ex.Message);
        }
        finally
        {
          this.m_utf.ReleaseEtocImageBuffer();
        }
        this.m_working_message = "Rewriting Header ...";
      }
      this.m_utf.ReleaseBinaryImage();
      this.releaseHashTableHgtocBinaryImage();
      return Status.HeaderRewriting;
    }

    private unsafe Status ExecHeaderRewriting()
    {
      int dpkDividedSize = this.m_dpk_divided_size;
      int codec;
      switch (this.m_comp_codec)
      {
        case EnumCompressCodec.CodecLayla:
          codec = 0;
          break;
        case EnumCompressCodec.CodecLZMA:
          codec = 128;
          break;
        case EnumCompressCodec.CodecRELC:
          codec = 129;
          break;
        default:
          codec = dpkDividedSize;
          break;
      }
      this.m_utf.SetCodec(codec);
      this.m_utf.SetMaxDpkItoc(this.m_max_dpk_itoc);
      uint writesize;
      void* writepointer = this.m_utf.MakeHeaderImage(&writesize, this.m_mask);
      Debug.WriteLine("---- HeaderRewrite");
      try
      {
        CBinary cbinary = this;
        cbinary.m_cpk_file_size = (long) cbinary.m_writer.Position;
        this.m_writer.Position = (ulong) this.m_pre_header_offset;
        this.m_writer.Write(this.m_utf.GetHeaderIffPtr(), (ulong) this.m_utf.GetIffSize());
        this.m_writer.WaitForComplete();
        Debug.WriteLine("wrote header iff");
        this.m_writer.Write(writepointer, (ulong) writesize);
        this.m_writer.WaitForComplete();
        Debug.WriteLine("wrote header data");
      }
      catch (Exception ex)
      {
        this.m_writer.Close();
        ApplicationException applicationException = new ApplicationException(ex.Message);
      }
      if (this.m_enable_mself)
        this.m_working_message = "Rewriting mself information ....";
      return Status.MselfRewriting;
    }

    private unsafe Status ExecHeaderRewritingAfs2()
    {
      Debug.WriteLine("---- HeaderRewrite(AFS2)");
      try
      {
        CBinary cbinary = this;
        cbinary.m_cpk_file_size = (long) cbinary.m_writer.Position;
        this.m_writer.Position = (ulong) this.m_pre_header_offset;
        sbyte* numPtr1 = (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04GBLLLKGC\u0040AFS2\u003F\u0024AA\u0040;
        this.m_writer.Write((void*) &\u003CModule\u003E.\u003F\u003F_C\u0040_04GBLLLKGC\u0040AFS2\u003F\u0024AA\u0040, 4UL);
        this.m_writer.WaitForComplete();
        Debug.WriteLine("wrote header iff");
        afs2_header afs2Header;
        // ISSUE: initblk instruction
        __memset(ref afs2Header, 0, 12);
        if (this.m_cpk_file_size > (long) ushort.MaxValue)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(sbyte&) ((IntPtr) &afs2Header + 1) = (sbyte) 4;
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(sbyte&) ((IntPtr) &afs2Header + 1) = (sbyte) 2;
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(sbyte&) ((IntPtr) &afs2Header + 2) = (sbyte) 2;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(sbyte&) ref afs2Header = (sbyte) 1;
        uint dataAlign = this.m_data_align;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(short&) ((IntPtr) &afs2Header + 8) = (short) (ushort) dataAlign;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &afs2Header + 4) = (int) (uint) this.m_filedata.Count;
        this.m_writer.Write((void*) &afs2Header, 12UL);
        this.m_writer.WaitForComplete();
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        uint writesize1 = (uint) ^(byte&) ((IntPtr) &afs2Header + 2) * (uint) ^(int&) ((IntPtr) &afs2Header + 4);
        void* writepointer1 = \u003CModule\u003E.malloc(writesize1);
        // ISSUE: initblk instruction
        __memset((IntPtr) writepointer1, 0, (int) writesize1);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(byte&) ((IntPtr) &afs2Header + 2) == (byte) 2)
        {
          ushort* numPtr2 = (ushort*) writepointer1;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          for (uint index = 0; index < (uint) ^(int&) ((IntPtr) &afs2Header + 4); ++index)
          {
            *numPtr2 = (ushort) this.m_filedata.FileInfos[(int) index].FileId;
            ++numPtr2;
          }
        }
        else
        {
          ApplicationException applicationException = new ApplicationException("ID2バイト以外未実装");
        }
        this.m_writer.Write(writepointer1, (ulong) writesize1);
        this.m_writer.WaitForComplete();
        \u003CModule\u003E.free(writepointer1);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        uint writesize2 = (uint) (^(int&) ((IntPtr) &afs2Header + 4) + 1) * (uint) ^(byte&) ((IntPtr) &afs2Header + 1);
        void* writepointer2 = \u003CModule\u003E.malloc(writesize2);
        // ISSUE: initblk instruction
        __memset((IntPtr) writepointer2, 0, (int) writesize2);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(byte&) ((IntPtr) &afs2Header + 1) == (byte) 2)
        {
          ushort num1 = (ushort) (uint) this.m_content_pos_top;
          *(short*) writepointer2 = (short) num1;
          ushort* numPtr3 = (ushort*) ((IntPtr) writepointer2 + 2);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          for (uint index = 0; index < (uint) ^(int&) ((IntPtr) &afs2Header + 4); ++index)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ushort num2 = (ushort) (uint) (this.m_filedata.FileInfos[(int) index].Filesize + (ulong) (((int) ^(ushort&) ((IntPtr) &afs2Header + 8) + (int) num1 - 1) / (int) ^(ushort&) ((IntPtr) &afs2Header + 8) * (int) ^(ushort&) ((IntPtr) &afs2Header + 8)));
            *numPtr3 = num2;
            num1 = num2;
            ++numPtr3;
          }
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(byte&) ((IntPtr) &afs2Header + 1) == (byte) 4)
          {
            uint num3 = (uint) this.m_content_pos_top;
            *(int*) writepointer2 = (int) num3;
            uint* numPtr4 = (uint*) ((IntPtr) writepointer2 + 4);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            for (uint index = 0; index < (uint) ^(int&) ((IntPtr) &afs2Header + 4); ++index)
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              uint num4 = (uint) (this.m_filedata.FileInfos[(int) index].Filesize + (ulong) ((uint) ((int) num3 + (int) ^(ushort&) ((IntPtr) &afs2Header + 8) - 1) / (uint) ^(ushort&) ((IntPtr) &afs2Header + 8) * (uint) ^(ushort&) ((IntPtr) &afs2Header + 8)));
              *numPtr4 = num4;
              num3 = num4;
              ++numPtr4;
            }
          }
          else
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            if (^(byte&) ((IntPtr) &afs2Header + 1) == (byte) 8)
            {
              ulong num5 = this.m_content_pos_top;
              *(long*) writepointer2 = (long) num5;
              ulong* numPtr5 = (ulong*) ((IntPtr) writepointer2 + 8);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              for (uint index = 0; index < (uint) ^(int&) ((IntPtr) &afs2Header + 4); ++index)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ulong num6 = (ulong) ^(ushort&) ((IntPtr) &afs2Header + 8);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                *numPtr5 = this.m_filedata.FileInfos[(int) index].Filesize + ((ulong) ((int) ^(ushort&) ((IntPtr) &afs2Header + 8) - 1) + num5) / num6 * num6;
                num5 = *numPtr5;
                ++numPtr5;
              }
            }
          }
        }
        this.m_writer.Write(writepointer2, (ulong) writesize2);
        this.m_writer.WaitForComplete();
        \u003CModule\u003E.free(writepointer2);
        Debug.WriteLine("wrote header data");
      }
      catch (Exception ex)
      {
        this.m_writer.Close();
        ApplicationException applicationException = new ApplicationException(ex.Message);
      }
      if (this.m_enable_mself)
        this.m_working_message = "Rewriting mself information ....";
      return Status.MselfRewriting;
    }

    private Status ExecMselfRewriting()
    {
      this.WriteMselfHeader();
      return Status.CpkBuildFinalize;
    }

    private Status ExecCpkBuildFinalize()
    {
      string filename = this.m_writer.Filename;
      this.m_writer.ClearCacheFilesLimit();
      this.m_writer.Close();
      this.MakeCpkh(filename);
      this.m_working_message = "Completed building a CPK file.";
      return Status.Complete;
    }

    private unsafe void WriteMselfHeader()
    {
      if (!this.m_enable_mself)
        return;
      this.m_writer.Position = 0UL;
      mself_header mselfHeader;
      // ISSUE: initblk instruction
      __memset(ref mselfHeader, 0, 64);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref mselfHeader = 4608845;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &mselfHeader + 4) = 16777216;
      ulong cpkFileSize = (ulong) this.m_cpk_file_size;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(long&) ((IntPtr) &mselfHeader + 8) = (long) ((((cpkFileSize & 71776119061217280UL | cpkFileSize >> 16) >> 16 | cpkFileSize & 280375465082880UL) >> 16 | cpkFileSize & 1095216660480UL) >> 8) | ((((long) cpkFileSize << 16 | (long) cpkFileSize & 65280L) << 16 | (long) cpkFileSize & 16711680L) << 16 | (long) cpkFileSize & 4278190080L) << 8;
      uint selfCount = (uint) this.m_self_count;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &mselfHeader + 16) = (int) ((selfCount & 16711680U | selfCount >> 16) >> 8) | ((int) selfCount << 16 | (int) selfCount & 65280) << 8;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &mselfHeader + 20) = 1073741824;
      this.m_writer.Write((void*) &mselfHeader, 64UL);
      List<CFileInfo>.Enumerator enumerator = this.m_filedata.FileInfos.GetEnumerator();
      if (!enumerator.MoveNext())
        return;
      do
      {
        CFileInfo current = enumerator.Current;
        string extension = Path.GetExtension(current.LocalFilePath);
        if (extension == ".self" || extension == ".sprx" || extension == ".sdat")
        {
          mself_entry mselfEntry;
          // ISSUE: initblk instruction
          __memset(ref mselfEntry, 0, 64);
          sbyte* str = Utility.AllocCharString(Path.GetFileName(current.ContentFilePath.Replace("/", "\\")));
          sbyte* numPtr = str;
          if (*str != (sbyte) 0)
          {
            do
            {
              ++numPtr;
            }
            while (*numPtr != (sbyte) 0);
          }
          int num1 = (int) ((IntPtr) numPtr - (IntPtr) str);
          if (num1 > 30)
          {
            // ISSUE: cpblk instruction
            __memcpy(ref mselfEntry, (IntPtr) str, 31);
          }
          else
          {
            // ISSUE: cpblk instruction
            __memcpy(ref mselfEntry, (IntPtr) str, num1);
          }
          Utility.FreeCharString(str);
          ulong num2 = current.Offset + (ulong) this.m_pre_header_offset + 2048UL;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ((IntPtr) &mselfEntry + 32) = (long) ((((num2 & 71776119061217280UL | num2 >> 16) >> 16 | num2 & 280375465082880UL) >> 16 | num2 & 1095216660480UL) >> 8) | ((((long) num2 << 16 | (long) num2 & 65280L) << 16 | (long) num2 & 16711680L) << 16 | (long) num2 & 4278190080L) << 8;
          ulong extractsize = current.Extractsize;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ((IntPtr) &mselfEntry + 40) = (long) ((((extractsize & 71776119061217280UL | extractsize >> 16) >> 16 | extractsize & 280375465082880UL) >> 16 | extractsize & 1095216660480UL) >> 8) | ((((long) extractsize << 16 | (long) extractsize & 65280L) << 16 | (long) extractsize & 16711680L) << 16 | (long) extractsize & 4278190080L) << 8;
          this.m_writer.Write((void*) &mselfEntry, 64UL);
        }
      }
      while (enumerator.MoveNext());
    }

    private unsafe void MakeCpkh(string cpkpath)
    {
      if (string.IsNullOrEmpty(this.m_part_of_cpk_header_ext) || !File.Exists(cpkpath))
        return;
      string path = Path.ChangeExtension(cpkpath, this.m_part_of_cpk_header_ext);
      FileStream fileStream1 = (FileStream) null;
      FileStream fileStream2 = (FileStream) null;
      try
      {
        fileStream1 = new FileStream(cpkpath, FileMode.Open, FileAccess.Read);
        long length = fileStream1.Length;
        if (this.m_analyzed)
        {
          ulong offset = (ulong) long.MaxValue;
          if (this.m_toc_size != 0UL)
          {
            ulong tocOffset = this.m_toc_offset;
            offset = (ulong) long.MaxValue >= tocOffset ? tocOffset : (ulong) long.MaxValue;
          }
          if (this.m_htoc_size != 0UL)
          {
            ulong htocOffset = this.m_htoc_offset;
            offset = offset >= htocOffset ? htocOffset : offset;
          }
          if (this.m_itoc_size != 0UL)
          {
            ulong itocOffset = this.m_itoc_offset;
            offset = offset >= itocOffset ? itocOffset : offset;
          }
          if (this.m_gtoc_size != 0UL)
          {
            ulong gtocOffset = this.m_gtoc_offset;
            offset = offset >= gtocOffset ? gtocOffset : offset;
          }
          if (this.m_hgtoc_size != 0UL)
          {
            ulong hgtocOffset = this.m_hgtoc_offset;
            offset = offset >= hgtocOffset ? hgtocOffset : offset;
          }
          if (this.m_etoc_size != 0UL)
          {
            ulong etocOffset = this.m_etoc_offset;
            offset = offset >= etocOffset ? etocOffset : offset;
          }
          fileStream1.Seek((long) offset, SeekOrigin.Begin);
          int count1 = (int) (length - (long) offset);
          byte[] array1 = new byte[count1];
          fileStream1.Read(array1, 0, count1);
          fileStream1.Close();
          long num1 = (long) offset + -2048L;
          if (this.m_toc_size != 0UL)
            this.m_utf.SetTocImageOffset(this.m_toc_offset - (ulong) num1);
          if (this.m_htoc_size != 0UL)
            this.m_utf.SetHtocImageOffset(this.m_htoc_offset - (ulong) num1);
          if (this.m_itoc_size != 0UL)
            this.m_utf.SetItocImageOffset(this.m_itoc_offset - (ulong) num1);
          if (this.m_gtoc_size != 0UL)
            this.m_utf.SetGtocImageOffset(this.m_gtoc_offset - (ulong) num1);
          if (this.m_hgtoc_size != 0UL)
            this.m_utf.SetHgtocImageOffset(this.m_hgtoc_offset - (ulong) num1);
          if (this.m_etoc_size != 0UL)
            this.m_utf.SetEtocImageOffset(this.m_etoc_offset - (ulong) num1);
          uint count2;
          byte* numPtr = (byte*) this.m_utf.MakeHeaderImage(&count2, this.m_mask);
          byte[] array2 = new byte[(int) count2];
          for (uint index1 = 0; index1 < count2; ++index1)
          {
            byte[] numArray = array2;
            int index2 = (int) index1;
            int num2 = (int) *(byte*) (index2 + (IntPtr) numPtr);
            numArray[index2] = (byte) num2;
          }
          byte* headerIffPtr = (byte*) this.m_utf.GetHeaderIffPtr();
          uint iffSize = this.m_utf.GetIffSize();
          byte[] array3 = new byte[(int) iffSize];
          for (uint index3 = 0; index3 < iffSize; ++index3)
          {
            byte[] numArray = array3;
            int index4 = (int) index3;
            int num3 = (int) *(byte*) (index4 + (IntPtr) headerIffPtr);
            numArray[index4] = (byte) num3;
          }
          fileStream2 = new FileStream(path, FileMode.Create, FileAccess.Write);
          fileStream2.Write(array3, 0, (int) iffSize);
          fileStream2.Write(array2, 0, (int) count2);
          fileStream2.Write(array1, 0, count1);
        }
        else
        {
          int count = (int) Math.Min(length, (long) this.m_content_pos_top);
          byte[] array = new byte[count];
          fileStream1.Read(array, 0, count);
          fileStream1.Close();
          fileStream2 = new FileStream(path, FileMode.Create, FileAccess.Write);
          fileStream2.Write(array, 0, array.Length);
        }
        fileStream2.Close();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        fileStream1?.Close();
        fileStream2?.Close();
      }
    }

    public double GetProgress()
    {
      if (this.m_stat == Status.Complete)
        return 100.0;
      CFileData filedata = this.m_filedata;
      if (filedata != null)
      {
        ulong execFileCur = this.m_exec_file_cur;
        if (execFileCur != 0UL)
        {
          double progress = (double) execFileCur / (double) filedata.Count * 98.399999976158142;
          Status stat = this.m_stat;
          if (stat == Status.HeaderRewriting)
            progress += 0.800000011920929;
          if (stat == Status.Complete)
            progress += 0.800000011920929;
          if (progress > 100.0)
            progress = 100.0;
          return progress;
        }
      }
      return 0.0;
    }

    public void Stop()
    {
      this.m_stat = Status.Stop;
      this.m_copier?.Close();
      this.m_reader.Close();
      this.m_writer.Close();
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool IsCrcCheck()
    {
      if (this.m_enable_crc)
      {
        switch (this.m_comp_codec)
        {
          case EnumCompressCodec.CodecLayla:
          case EnumCompressCodec.CodecLZMA:
          case EnumCompressCodec.CodecRELC:
            return true;
        }
      }
      return false;
    }

    private Status ExecTryCompress()
    {
      this.m_try_compress_complete = false;
      CBinary.CBinaryDelegate cbinaryDelegate = new CBinary.CBinaryDelegate(CBinary.ExecTryCompressCore);
      this.m_try_compress_delegate = cbinaryDelegate;
      cbinaryDelegate.BeginInvoke(this, (AsyncCallback) null, (object) this);
      return Status.TryCompressEnd;
    }

    private Status ExecTryUnification()
    {
      this.m_try_unification_complete = false;
      CBinary.CBinaryDelegate cbinaryDelegate = new CBinary.CBinaryDelegate(CBinary.ExecTryUnificationCore);
      this.m_try_unification_delegate = cbinaryDelegate;
      cbinaryDelegate.BeginInvoke(this, (AsyncCallback) null, (object) this);
      return Status.TryUnificationEnd;
    }

    private Status ExecTryCompressEnd() => !this.m_try_compress_complete ? Status.TryCompressEnd : Status.TryUnification;

    private Status ExecTryUnificationEnd() => !this.m_try_unification_complete ? Status.TryUnificationEnd : Status.PreFileDataBuilding;

    private static void ExecTryCompressCore(CBinary obj)
    {
      CAsyncFile casyncFile = new CAsyncFile((int) obj.m_comp_codec);
      casyncFile.CompPercentage = obj.m_comp_percentage;
      casyncFile.CompFileSize = obj.m_comp_filesize;
      long count = (long) obj.m_filedata.Count;
      obj.m_exec_file_cur = 0UL;
      long index = 0;
      if (0L < count)
      {
        do
        {
          CFileInfo fileInfo = obj.m_filedata.FileInfos[(int) index];
          if (obj.m_force_compress || fileInfo.TryCompress)
            fileInfo.TryCompress = !obj.IsUncompresseFilename(fileInfo) && (ulong) casyncFile.GetCompressedSize(fileInfo.LocalFilePath) < fileInfo.Filesize;
          ++index;
          ++obj.m_exec_file_cur;
        }
        while (index < count);
      }
      obj.m_try_compress_complete = true;
    }

    private static void ExecTryUnificationCore(CBinary obj)
    {
      long count = (long) obj.m_filedata.Count;
      obj.m_unification_list.Clear();
      obj.m_exec_file_cur = 0UL;
      long index = 0;
      if (0L < count)
      {
        do
        {
          CFileInfo fileInfo = obj.m_filedata.FileInfos[(int) index];
          if (obj.IsUnificationFile(fileInfo))
          {
            CFileInfo cfileInfo = obj.SearchInificationFile(fileInfo);
            if (cfileInfo == null)
            {
              fileInfo.UniTarget = false;
            }
            else
            {
              cfileInfo.UniTarget = true;
              fileInfo.UniTarget = true;
            }
          }
          ++index;
          ++obj.m_exec_file_cur;
        }
        while (index < count);
      }
      obj.m_unification_list.Clear();
      obj.m_try_unification_complete = true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool AnalyzeCpkFile(CFileData filedata, string fpath)
    {
      this.m_filedata = filedata;
      void* voidPtr = \u003CModule\u003E.malloc(4194304U);
      _criheap_struct* heap = \u003CModule\u003E.criHeap_Create(voidPtr, 4194304);
      bool flag = this.m_reader.ReadOpen(fpath);
      if (flag)
      {
        flag = this.AnalyzeCpkFileSub(filedata, heap);
        this.m_reader.Close();
      }
      \u003CModule\u003E.criHeap_Destroy(heap);
      \u003CModule\u003E.free(voidPtr);
      return flag;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool AnalyzeCpkFileSub(CFileData filedata, _criheap_struct* heap)
    {
      CriUtfHeapObj criUtfHeapObj;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref criUtfHeapObj = (int) \u003CModule\u003E.__unep\u0040\u003FUtfHeap_to_Heap_Alloc\u0040\u0040\u0024\u0024FYAPAXPAXJPBDJJ\u0040Z;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &criUtfHeapObj + 4) = (int) \u003CModule\u003E.__unep\u0040\u003FUtfHeap_to_Heap_Free\u0040\u0040\u0024\u0024FYAXPAX0\u0040Z;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &criUtfHeapObj + 8) = (int) heap;
      this.m_reader.ReadAlloc(0UL, 2048UL);
      this.m_reader.WaitForComplete();
      void* readBuffer1 = this.m_reader.ReadBuffer;
      afs2_header afs2Header;
      if (\u003CModule\u003E.AFS2_GetHeaderInfo(&afs2Header, readBuffer1, 2048) == 1)
      {
        int num = \u003CModule\u003E.AFS2_CalcAfs2HeaderInfoSize(&afs2Header);
        this.m_reader.Position = 0UL;
        this.m_reader.ReadAlloc(0UL, (ulong) num);
        this.m_reader.WaitForComplete();
        void* readBuffer2 = this.m_reader.ReadBuffer;
        \u003CModule\u003E.AFS2_GetHeaderInfo(&afs2Header, readBuffer2, num);
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0CD\u0040PMNDCBAC\u0040\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F5AFS2\u003F5Format\u003F5\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u003F\u0024DN\u0040, __arglist ());
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BM\u0040LNHDPCEH\u0040\u003F5\u003F5Number\u003F5of\u003F5Contents\u003F5\u003F3\u003F5\u003F\u0024CF8d\u003F6\u003F\u0024AA\u0040, __arglist (^(int&) ((IntPtr) &afs2Header + 4)));
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BM\u0040FLIJCALF\u0040\u003F5\u003F5Offset\u003F5Byte\u003F5\u003F1\u003F5file\u003F5\u003F3\u003F5\u003F\u0024CF8d\u003F6\u003F\u0024AA\u0040, __arglist ((int) ^(byte&) ((IntPtr) &afs2Header + 1)));
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BM\u0040MOOLLJDA\u0040\u003F5\u003F5ID\u003F5Byte\u003F5\u003F1\u003F5file\u003F5\u003F5\u003F5\u003F5\u003F5\u003F3\u003F5\u003F\u0024CF8d\u003F6\u003F\u0024AA\u0040, __arglist ((int) ^(byte&) ((IntPtr) &afs2Header + 2)));
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BM\u0040LMNKMGCD\u0040\u003F5\u003F5Content\u003F5Alignment\u003F5\u003F5\u003F3\u003F5\u003F\u0024CF8d\u003F6\u003F\u0024AA\u0040, __arglist ((int) ^(ushort&) ((IntPtr) &afs2Header + 8)));
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BM\u0040MEIBAMDJ\u0040\u003F5\u003F5Format\u003F5Version\u003F5\u003F5\u003F5\u003F5\u003F5\u003F3\u003F5\u003F\u0024CF8d\u003F6\u003F\u0024AA\u0040, __arglist ((int) ^(byte&) ref afs2Header));
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BD\u0040BINFPPEO\u0040\u003F6\u003F9\u003F5Contents\u003F5info\u003F4\u003F6\u003F\u0024AA\u0040, __arglist ());
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BN\u0040LFDHDJLG\u0040\u003F5\u003F5\u003F5ID\u003F5\u003F5\u003F5\u003F5\u003F5\u003F5\u003F5Size\u003F5\u003F5\u003F5\u003F5\u003F5Offset\u003F6\u003F\u0024AA\u0040, __arglist ());
        uint index = 0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (0U < (uint) ^(int&) ((IntPtr) &afs2Header + 4))
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          do
          {
            afs2_content_info afs2ContentInfo;
            if (\u003CModule\u003E.AFS2_GetInfoByIndex(&afs2Header, (int) index, readBuffer2, 2048, &afs2ContentInfo) == 1)
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040CBINNGBM\u0040\u003F\u0024CF5d\u003F5\u003F\u0024CF10d\u003F5\u003F\u0024CF10d\u003F6\u003F\u0024AA\u0040, __arglist (^(int&) ref afs2ContentInfo, ^(int&) ((IntPtr) &afs2ContentInfo + 4), ^(long&) ((IntPtr) &afs2ContentInfo + 8)));
            }
            ++index;
          }
          while (index < (uint) ^(int&) ((IntPtr) &afs2Header + 4));
        }
        \u003CModule\u003E.printf((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06GAOLIIF\u0040done\u003F4\u003F6\u003F\u0024AA\u0040, __arglist ());
        this.m_afs2_mode = true;
        return true;
      }
      CriCpkAnalyzerTag criCpkAnalyzerTag;
      \u003CModule\u003E.criCpkAnalyzer_Initialize(&criCpkAnalyzerTag);
      CriCpkHeaderInfoTag cpkHeaderInfoTag;
      switch (\u003CModule\u003E.criCpkAnalyzer_GetHeaderInfoRtE(&criCpkAnalyzerTag, &cpkHeaderInfoTag, readBuffer1, &criUtfHeapObj))
      {
        case (CriCpkErrTag) 0:
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.m_cpk_datetime = (ulong) ^(long&) ref cpkHeaderInfoTag;
          this.m_toc_size = \u003CModule\u003E.criCpkHeaderInfo_GetTocSizeByte(&cpkHeaderInfoTag);
          this.m_htoc_size = \u003CModule\u003E.criCpkHeaderInfo_GetHtocSizeByte(&cpkHeaderInfoTag);
          this.m_itoc_size = \u003CModule\u003E.criCpkHeaderInfo_GetItocSizeByte(&cpkHeaderInfoTag);
          this.m_gtoc_size = \u003CModule\u003E.criCpkHeaderInfo_GetGtocSizeByte(&cpkHeaderInfoTag);
          this.m_hgtoc_size = \u003CModule\u003E.criCpkHeaderInfo_GetHgtocSizeByte(&cpkHeaderInfoTag);
          this.m_etoc_size = (ulong) \u003CModule\u003E.criCpkHeaderInfo_GetEtocSizeByte(&cpkHeaderInfoTag);
          this.m_enable_crc = false;
          this.m_enable_crc32 = false;
          this.m_enable_checksum64 = false;
          if (\u003CModule\u003E.criCpkHeaderInfo_IsTocCrcEnabled(&cpkHeaderInfoTag) != 0 || \u003CModule\u003E.criCpkHeaderInfo_IsFileCrcEnabled(&cpkHeaderInfoTag) != 0)
          {
            this.m_enable_crc = true;
            if (\u003CModule\u003E.criCpkHeaderInfo_GetCrcMode(&cpkHeaderInfoTag) == (CriCpkCrcModeTag) 1)
              this.m_enable_checksum64 = true;
            else
              this.m_enable_crc32 = true;
          }
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.m_updates = (uint) ^(int&) ((IntPtr) &cpkHeaderInfoTag + 196);
          this.m_toc_offset = \u003CModule\u003E.criCpkHeaderInfo_GetTocOffset(&cpkHeaderInfoTag);
          this.m_htoc_offset = \u003CModule\u003E.criCpkHeaderInfo_GetHtocOffset(&cpkHeaderInfoTag);
          this.m_itoc_offset = \u003CModule\u003E.criCpkHeaderInfo_GetItocOffset(&cpkHeaderInfoTag);
          this.m_gtoc_offset = \u003CModule\u003E.criCpkHeaderInfo_GetGtocOffset(&cpkHeaderInfoTag);
          this.m_hgtoc_offset = \u003CModule\u003E.criCpkHeaderInfo_GetHgtocOffset(&cpkHeaderInfoTag);
          this.m_etoc_offset = (ulong) \u003CModule\u003E.criCpkHeaderInfo_GetEtocOffset(&cpkHeaderInfoTag);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.m_contents_offset = (ulong) (uint) ^(int&) ((IntPtr) &cpkHeaderInfoTag + 8);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.m_contents_size = (ulong) ^(long&) ((IntPtr) &cpkHeaderInfoTag + 16);
          this.m_ver = (int) \u003CModule\u003E.criCpkHeaderInfo_GetVersion(&cpkHeaderInfoTag);
          this.m_rev = (int) \u003CModule\u003E.criCpkHeaderInfo_GetRevision(&cpkHeaderInfoTag);
          this.m_cpk_mode = \u003CModule\u003E.criCpkHeaderInfo_GetCpkMode(&cpkHeaderInfoTag);
          this.m_enable_filename = \u003CModule\u003E.criCpkHeaderInfo_IsFileNameEnabled(&cpkHeaderInfoTag) != 0;
          this.m_data_align = (uint) \u003CModule\u003E.criCpkHeaderInfo_GetDataAlign(&cpkHeaderInfoTag);
          this.m_filedata.Comment = new string(\u003CModule\u003E.criCpkHeaderInfo_GetComment(&cpkHeaderInfoTag));
          this.m_filedata.ToolVersion = new string(\u003CModule\u003E.criCpkHeaderInfo_GetTver(&cpkHeaderInfoTag));
          this.m_sorted_filename = \u003CModule\u003E.criCpkHeaderInfo_IsSorted(&cpkHeaderInfoTag) != 0;
          this.m_is_extra_id = \u003CModule\u003E.criCpkHeaderInfo_IsExtraId(&cpkHeaderInfoTag) != 0;
          this.m_enable_toc = false;
          this.m_enable_etoc = false;
          this.m_enable_itoc = false;
          this.m_enable_gtoc = false;
          this.m_enable_toc = this.m_toc_size != 0UL;
          this.m_enable_etoc = this.m_etoc_size != 0UL;
          this.m_enable_itoc = this.m_itoc_size != 0UL;
          this.m_enable_gtoc = this.m_gtoc_size != 0UL;
          uint numFiles1 = (uint) \u003CModule\u003E.criCpkHeaderInfo_GetNumFiles(&cpkHeaderInfoTag);
          switch (\u003CModule\u003E.criCpkHeaderInfo_GetCodecType(&cpkHeaderInfoTag))
          {
            case (CriCpkCodecTypeTag) 128:
              this.CompressCodec = EnumCompressCodec.CodecLZMA;
              break;
            case (CriCpkCodecTypeTag) 129:
              this.CompressCodec = EnumCompressCodec.CodecRELC;
              break;
            default:
              this.CompressCodec = EnumCompressCodec.CodecLayla;
              break;
          }
          this.DpkDividedSize = (int) \u003CModule\u003E.criCpkHeaderInfo_GetDivideSize(&cpkHeaderInfoTag);
          this.m_max_dpk_itoc = (int) \u003CModule\u003E.criCpkHeaderInfo_GetDpkItocSize(&cpkHeaderInfoTag);
          CriCpkTocInfoTag criCpkTocInfoTag;
          if (this.m_enable_toc)
          {
            this.m_reader.ReadAlloc(\u003CModule\u003E.criCpkHeaderInfo_GetTocOffset(&cpkHeaderInfoTag), (ulong) (uint) \u003CModule\u003E.criCpkHeaderInfo_GetTocSizeByte(&cpkHeaderInfoTag));
            this.m_reader.WaitForComplete();
            void* readBuffer3 = this.m_reader.ReadBuffer;
            \u003CModule\u003E.criCpkHeaderInfo_GetTocInfo(&cpkHeaderInfoTag, &criCpkTocInfoTag, readBuffer3, &criUtfHeapObj);
            uint infoIndex = 0;
            if (0U < numFiles1)
            {
              do
              {
                CriFsCpkFileInfoTag fsCpkFileInfoTag;
                \u003CModule\u003E.criCpkTocInfo_GetFileInfo(&criCpkTocInfoTag, &fsCpkFileInfoTag, (int) infoIndex);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ulong checksum64 = ^(int&) ((IntPtr) &fsCpkFileInfoTag + 48) == 0 ? 0UL : (ulong) *(long*) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 48);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                this.m_filedata.AddDataByAnalyze((sbyte*) ^(int&) ref fsCpkFileInfoTag, (sbyte*) ^(int&) ref fsCpkFileInfoTag, (sbyte*) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 4), (ulong) ^(long&) ((IntPtr) &fsCpkFileInfoTag + 16), (uint) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 8), (uint) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 12), (uint) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 24), (uint) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 32), checksum64, (sbyte*) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 28), (ulong) infoIndex);
                ++infoIndex;
              }
              while (infoIndex < numFiles1);
            }
          }
          if (this.m_htoc_size != 0UL)
          {
            this.m_reader.ReadAlloc(\u003CModule\u003E.criCpkHeaderInfo_GetHtocOffset(&cpkHeaderInfoTag), (ulong) (uint) \u003CModule\u003E.criCpkHeaderInfo_GetHtocSizeByte(&cpkHeaderInfoTag));
            this.m_reader.WaitForComplete();
            void* readBuffer4 = this.m_reader.ReadBuffer;
            CriCpkHtocInfoTag criCpkHtocInfoTag;
            \u003CModule\u003E.criCpkHeaderInfo_GetHtocInfo(&cpkHeaderInfoTag, &criCpkHtocInfoTag, readBuffer4, &criUtfHeapObj, &criCpkTocInfoTag);
            if (\u003CModule\u003E.criCpkHtocInfo_IsHashTableEffective(&criCpkHtocInfoTag, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BA\u0040JJHANOGC\u0040ht_fpath_to_toc\u003F\u0024AA\u0040) != 0)
              this.m_enable_htoc_fpath_to_toc = true;
            if (\u003CModule\u003E.criCpkHtocInfo_IsHashTableEffective(&criCpkHtocInfoTag, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0O\u0040PLIBEFE\u0040ht_fid_to_toc\u003F\u0024AA\u0040) != 0)
              this.m_enable_htoc_fid_to_toc = true;
          }
          if (this.m_enable_etoc)
          {
            this.m_reader.ReadAlloc((ulong) \u003CModule\u003E.criCpkHeaderInfo_GetEtocOffset(&cpkHeaderInfoTag), (ulong) (uint) (int) \u003CModule\u003E.criCpkHeaderInfo_GetEtocSizeByte(&cpkHeaderInfoTag));
            this.m_reader.WaitForComplete();
            void* readBuffer5 = this.m_reader.ReadBuffer;
            CriCpkEtocInfoTag criCpkEtocInfoTag;
            \u003CModule\u003E.criCpkHeaderInfo_GetEtocInfo(&cpkHeaderInfoTag, &criCpkEtocInfoTag, readBuffer5, &criUtfHeapObj);
            string path1_1 = new string(\u003CModule\u003E.criCpkEtocInfo_GetBaseDirectory(&criCpkEtocInfoTag));
            this.m_filedata.BaseDirectory = path1_1;
            uint index = 0;
            if (0U < numFiles1)
            {
              do
              {
                CriCpkEfileInfoTag criCpkEfileInfoTag;
                \u003CModule\u003E.criCpkEtocInfo_GetFileInfo(&criCpkEtocInfoTag, &criCpkEfileInfoTag, (int) index);
                string path2 = this.m_filedata.FileInfos[(int) index].LocalFilePath;
                if (!path1_1.Equals(""))
                {
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  string path1_2 = Path.Combine(path1_1, new string((sbyte*) ^(int&) ref criCpkEfileInfoTag));
                  if (path2.StartsWith("\\") || path2.StartsWith("/"))
                    path2 = path2.Substring(1);
                  string str = Path.Combine(path1_2, path2).Replace("\\", "/");
                  this.m_filedata.FileInfos[(int) index].LocalFilePath = str;
                }
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                this.m_filedata.FileInfos[(int) index].DateTime64 = (ulong) ^(long&) ((IntPtr) &criCpkEfileInfoTag + 8);
                ++index;
              }
              while (index < numFiles1);
            }
            this.m_basedir = path1_1;
          }
          if (this.m_enable_itoc && !this.m_is_extra_id)
          {
            this.m_reader.ReadAlloc(\u003CModule\u003E.criCpkHeaderInfo_GetItocOffset(&cpkHeaderInfoTag), (ulong) (uint) \u003CModule\u003E.criCpkHeaderInfo_GetItocSizeByte(&cpkHeaderInfoTag));
            this.m_reader.WaitForComplete();
            void* readBuffer6 = this.m_reader.ReadBuffer;
            CriCpkItocInfoTag criCpkItocInfoTag;
            \u003CModule\u003E.criCpkHeaderInfo_GetItocInfo(&cpkHeaderInfoTag, &criCpkItocInfoTag, readBuffer6, &criUtfHeapObj);
            int numFiles2 = \u003CModule\u003E.criCpkItocInfo_GetNumFiles(&criCpkItocInfoTag);
            int index = 0;
            if (0 < numFiles2)
            {
              do
              {
                CriCpkIfileInfoTag criCpkIfileInfoTag;
                \u003CModule\u003E.criCpkItocInfo_GetFileInfo(&criCpkItocInfoTag, &criCpkIfileInfoTag, index);
                if (!this.m_enable_etoc)
                {
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  ulong checksum64 = ^(int&) ((IntPtr) &criCpkIfileInfoTag + 40) == 0 ? 0UL : (ulong) *(long*) ^(int&) ((IntPtr) &criCpkIfileInfoTag + 40);
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  this.m_filedata.AddDataByAnalyze((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_00CNPNBAHC\u0040\u003F\u0024AA\u0040, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_00CNPNBAHC\u0040\u003F\u0024AA\u0040, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_00CNPNBAHC\u0040\u003F\u0024AA\u0040, (ulong) ^(long&) ((IntPtr) &criCpkIfileInfoTag + 16), (uint) ^(int&) ((IntPtr) &criCpkIfileInfoTag + 4), (uint) ^(int&) ((IntPtr) &criCpkIfileInfoTag + 8), (uint) ^(int&) ref criCpkIfileInfoTag, (uint) ^(int&) ((IntPtr) &criCpkIfileInfoTag + 24), checksum64, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_00CNPNBAHC\u0040\u003F\u0024AA\u0040, ulong.MaxValue);
                }
                else
                {
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  this.m_filedata.FileInfos[index].FileId = (uint) ^(int&) ref criCpkIfileInfoTag;
                }
                ++index;
              }
              while (index < numFiles2);
            }
            this.m_filedata.SortById();
          }
          CriCpkGtocInfoTag criCpkGtocInfoTag;
          if (this.m_enable_gtoc)
          {
            this.m_reader.ReadAlloc(\u003CModule\u003E.criCpkHeaderInfo_GetGtocOffset(&cpkHeaderInfoTag), (ulong) (uint) \u003CModule\u003E.criCpkHeaderInfo_GetGtocSizeByte(&cpkHeaderInfoTag));
            this.m_reader.WaitForComplete();
            void* readBuffer7 = this.m_reader.ReadBuffer;
            \u003CModule\u003E.criCpkHeaderInfo_GetGtocInfo(&cpkHeaderInfoTag, &criCpkGtocInfoTag, readBuffer7, &criUtfHeapObj, &criCpkTocInfoTag);
            \u003CModule\u003E.criCpkHeaderInfo_GetNumAttributes(&cpkHeaderInfoTag);
            List<CAttribute> attrs = new List<CAttribute>();
            int num = 0;
            sbyte* numPtr;
            int alignment;
            if (\u003CModule\u003E.criCpkGtocInfo_GetAttributeInfoFromIndex(&criCpkGtocInfoTag, &numPtr, &alignment, 0) != 0)
            {
              do
              {
                string attrName = new string(numPtr);
                attrs.Add(new CAttribute(attrName)
                {
                  Alignment = alignment
                });
                filedata.GroupingManager.TryAddAttribute(attrName, alignment);
                ++num;
              }
              while (\u003CModule\u003E.criCpkGtocInfo_GetAttributeInfoFromIndex(&criCpkGtocInfoTag, &numPtr, &alignment, num) != 0);
            }
            CriCpkGroupLinkInfoTag groupLinkInfoTag;
            \u003CModule\u003E.criCpkGtocInfo_GetGroupLinkInfo(&criCpkGtocInfoTag, &groupLinkInfoTag, 0);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            CGroup parentGrp = new CGroup(new string((sbyte*) ^(int&) ref groupLinkInfoTag), (CGroup) null);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &groupLinkInfoTag + 8) = -1;
            this.MakeGroupStructure(&criCpkGtocInfoTag, parentGrp, attrs, 1);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            if (^(int&) ((IntPtr) &groupLinkInfoTag + 4) > 0)
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              this.MakeGroupStructure(&criCpkGtocInfoTag, (CGroup) null, attrs, ^(int&) ((IntPtr) &groupLinkInfoTag + 4));
            }
            List<CGroup>.Enumerator enumerator = parentGrp.SubGroups.GetEnumerator();
            if (enumerator.MoveNext())
            {
              do
              {
                CGroup current = enumerator.Current;
                current.ParentGroup = (CGroup) null;
                filedata.GroupingManager.Groups.Add(current);
              }
              while (enumerator.MoveNext());
            }
            this.EnableGInfo = \u003CModule\u003E.criCpkGtocInfo_GetNumGinf(&criCpkGtocInfoTag) > 0;
            this.m_filedata.ResetPackingOrderAferAnalyzed();
          }
          if (this.m_hgtoc_size != 0UL)
          {
            this.m_reader.ReadAlloc(\u003CModule\u003E.criCpkHeaderInfo_GetHgtocOffset(&cpkHeaderInfoTag), (ulong) (uint) \u003CModule\u003E.criCpkHeaderInfo_GetHgtocSizeByte(&cpkHeaderInfoTag));
            this.m_reader.WaitForComplete();
            void* readBuffer8 = this.m_reader.ReadBuffer;
            CriCpkHgtocInfoTag criCpkHgtocInfoTag;
            \u003CModule\u003E.criCpkHeaderInfo_GetHgtocInfo(&cpkHeaderInfoTag, &criCpkHgtocInfoTag, readBuffer8, &criUtfHeapObj, &criCpkGtocInfoTag);
            if (\u003CModule\u003E.criCpkHgtocInfo_IsHashTableEffective(&criCpkHgtocInfoTag, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BC\u0040BBEIPJCP\u0040ht_gname_to_glink\u003F\u0024AA\u0040) != 0)
              this.m_enable_hgtoc_gname_to_glink = true;
            if (\u003CModule\u003E.criCpkHgtocInfo_IsHashTableEffective(&criCpkHgtocInfoTag, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BC\u0040DPFHIBE\u0040ht_ganame_to_ginf\u003F\u0024AA\u0040) != 0)
              this.m_enable_hgtoc_ganame_to_ginf = true;
            if (\u003CModule\u003E.criCpkHgtocInfo_IsHashTableEffective(&criCpkHgtocInfoTag, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BD\u0040FEOHOPGI\u0040ht_gfpath_to_gfinf\u003F\u0024AA\u0040) != 0)
              this.m_enable_hgtoc_gfpath_to_gfinf = true;
            if (\u003CModule\u003E.criCpkHgtocInfo_IsHashTableEffective(&criCpkHgtocInfoTag, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BB\u0040GNBGBGHC\u0040ht_gfid_to_gfinf\u003F\u0024AA\u0040) != 0)
              this.m_enable_hgtoc_gfid_to_gfinf = true;
          }
          this.SetAnalyzedFlag();
          return true;
        case (CriCpkErrTag) 1:
          this.SetToError("CPK header analyzer error NG.");
          break;
        case (CriCpkErrTag) 2:
          this.SetToError("CPK header analyzer error NOT_CPK.");
          break;
        case (CriCpkErrTag) 3:
          this.SetToError("CPK header analyzer error NO_MEMORY.");
          break;
        default:
          this.SetToError("CPK header analyzer error ?.");
          break;
      }
      return false;
    }

    private void SetAnalyzedFlag()
    {
      this.m_analyzed = true;
      this.m_filedata.Analyzed = true;
    }

    private unsafe CGroup MakeGroupStructure(
      CriCpkGtocInfoTag* cpkg,
      CGroup parentGrp,
      List<CAttribute> attrs,
      int cur)
    {
      CriCpkGroupLinkInfoTag groupLinkInfoTag;
      \u003CModule\u003E.criCpkGtocInfo_GetGroupLinkInfo(cpkg, &groupLinkInfoTag, cur);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      CGroup cgroup = new CGroup(new string((sbyte*) ^(int&) ref groupLinkInfoTag), parentGrp);
      if (parentGrp != null)
      {
        parentGrp.CreateSubGroup();
        parentGrp.SubGroups.Add(cgroup);
      }
      if (cur != 0)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ((IntPtr) &groupLinkInfoTag + 8) >= 0)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.MakeGroupStructureSub(cpkg, cgroup, attrs, ^(int&) ((IntPtr) &groupLinkInfoTag + 8));
          goto label_7;
        }
      }
      else
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &groupLinkInfoTag + 8) = -1;
      }
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      this.MakeGroupStructure(cpkg, cgroup, attrs, -^(int&) ((IntPtr) &groupLinkInfoTag + 8));
label_7:
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (^(int&) ((IntPtr) &groupLinkInfoTag + 4) > 0)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        this.MakeGroupStructure(cpkg, parentGrp, attrs, ^(int&) ((IntPtr) &groupLinkInfoTag + 4));
      }
      return cgroup;
    }

    private unsafe void MakeGroupStructureSub(
      CriCpkGtocInfoTag* cpkg,
      CGroup group,
      List<CAttribute> attrs,
      int cur)
    {
      while (true)
      {
        CriCpkFileLinkInfoTag cpkFileLinkInfoTag;
        \u003CModule\u003E.criCpkGtocInfo_GetFileLinkInfo(cpkg, &cpkFileLinkInfoTag, cur);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ((IntPtr) &cpkFileLinkInfoTag + 8) >= 0)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          CFileInfo fileInfo = this.m_filedata.FileInfos[Math.Abs(^(int&) ((IntPtr) &cpkFileLinkInfoTag + 8))];
          fileInfo.AddGroupString(group.Path);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          int index = (int) ^(ushort&) ((IntPtr) &cpkFileLinkInfoTag + 2);
          fileInfo.AttributeString = attrs[index].AttributeName;
          CAttribute attr = this.GetAttributeByName(group, attrs[index].AttributeName);
          if (attr == null)
          {
            attr = new CAttribute(attrs[index].AttributeName, group);
            group.AddAttribute(attr);
            attr.Alignment = attrs[index].Alignment;
            CAttribute parentAttribute = attr;
            parentAttribute.FileInfoLink = new CFileInfoLink(parentAttribute);
          }
          fileInfo.DataAlign = (uint) attrs[index].Alignment;
          attr.FileInfoLink.AddFileInfo(fileInfo, true);
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.MakeGroupStructureSub(cpkg, group, attrs, -^(int&) ((IntPtr) &cpkFileLinkInfoTag + 8));
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ((IntPtr) &cpkFileLinkInfoTag + 4) > 0)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          cur = ^(int&) ((IntPtr) &cpkFileLinkInfoTag + 4);
        }
        else
          break;
      }
    }

    private void AddGroupsInfo()
    {
    }

    public CAttribute GetAttributeByName(CGroup group, string attrName)
    {
      if (group.AttributeList != null)
      {
        int index = 0;
        if (0 < group.AttributeList.Count)
        {
          CAttribute attribute;
          do
          {
            attribute = group.AttributeList[index];
            if (!(attribute.AttributeName == attrName))
              ++index;
            else
              goto label_4;
          }
          while (index < group.AttributeList.Count);
          goto label_5;
label_4:
          return attribute;
        }
      }
label_5:
      return (CAttribute) null;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool StartExtractFile(string cpkfname, string outdir)
    {
      this.m_exec_mode = CBinary.ExecMode.ExtractFile;
      this.m_stat = Status.ExtractPreparing;
      this.m_extract_path = outdir;
      if (this.m_reader.ReadOpen(cpkfname))
        return true;
      string str = "Cannot read a file. \"" + cpkfname + "\"";
      this.m_error_message = str;
      Console.WriteLine(str);
      this.m_stat = Status.Error;
      return false;
    }

    private Status ExecExtractPreparing()
    {
      this.m_exec_file_cur = 0UL;
      return Status.ExtractStart;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool ExtractFileOne(string cpkfilename, CFileInfo filedata, string extract_path)
    {
      this.m_exec_mode = CBinary.ExecMode.ExtractFile;
      this.m_stat = Status.ExtractPreparing;
      this.m_extract_path = extract_path;
      if (!this.m_reader.ReadOpen(cpkfilename))
        return false;
      bool file = this.ExtractFile(filedata, this.m_reader, extract_path, false);
      this.m_reader.Close();
      return file;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool ExtractFile(
      CFileInfo filedata,
      CAsyncFile reader,
      string extract_path,
      [MarshalAs(UnmanagedType.U1)] bool is_root_dir)
    {
      long offset = (long) filedata.Offset;
      long filesize = (long) filedata.Filesize;
      long extractsize = (long) filedata.Extractsize;
      string str1 = filedata.ContentFilePath.Replace("/", "\\");
      if (Path.GetFileName(str1).Equals(""))
        str1 = string.Format("ID{0:00000}", (object) filedata.FileId);
      string str2 = !is_root_dir ? extract_path : Path.Combine(extract_path, str1);
      filedata.LocalFilePath = str2;
      Debug.WriteLine("Try Extract : " + str2);
      if (!string.IsNullOrEmpty(str2))
      {
        string directoryName = Path.GetDirectoryName(str2);
        if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
      }
      CAsyncFile writer = this.m_writer;
      CAsyncFile copier = this.m_copier;
      if (!writer.WriteOpen(str2))
        return false;
      if (filedata.IsCompressed)
        copier.Copy(reader, writer, (ulong) offset, (ulong) filesize, CAsyncFile.CopyMode.ReadDecompress, (ulong) extractsize);
      else
        copier.Copy(reader, writer, (ulong) offset, (ulong) filesize);
      copier.WaitForComplete();
      copier.Close();
      writer.Close();
      if (copier.Status == CAsyncFile.AsyncStatus.Error)
        return false;
      if (filedata.DateTime64 != 282578783305728UL)
      {
        try
        {
          DateTime dateTime = filedata.DateTime;
          Directory.SetLastWriteTime(str2, dateTime);
        }
        catch (Exception ex)
        {
          Debug.WriteLine("failed set to time stamp " + ex.Message);
        }
      }
      return true;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool ExtractFileAsync(
      CFileInfo filedata,
      CAsyncFile reader,
      string extract_path,
      [MarshalAs(UnmanagedType.U1)] bool is_root_dir)
    {
      long offset = (long) filedata.Offset;
      long filesize = (long) filedata.Filesize;
      long extractsize = (long) filedata.Extractsize;
      string str1 = filedata.ContentFilePath.Replace("/", "\\");
      if (Path.GetFileName(str1).Equals(""))
        str1 = string.Format("ID{0:00000}", (object) filedata.FileId);
      string str2 = !is_root_dir ? extract_path : Path.Combine(extract_path, str1);
      filedata.LocalFilePath = str2;
      Debug.WriteLine("Try Extract : " + str2);
      if (!string.IsNullOrEmpty(str2))
      {
        string directoryName = Path.GetDirectoryName(str2);
        if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
      }
      CAsyncFile writer = this.m_writer;
      CAsyncFile copier = this.m_copier;
      if (!writer.WriteOpen(str2))
        return false;
      if (filedata.IsCompressed)
        copier.Copy(reader, writer, (ulong) offset, (ulong) filesize, CAsyncFile.CopyMode.ReadDecompress, (ulong) extractsize);
      else
        copier.Copy(reader, writer, (ulong) offset, (ulong) filesize);
      return copier.Status != CAsyncFile.AsyncStatus.Error;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool IsCompleteExtractFileAsync(CFileInfo filedata)
    {
      CAsyncFile writer = this.m_writer;
      CAsyncFile copier = this.m_copier;
      CAsyncFile casyncFile = copier;
      switch (copier.Status)
      {
        case CAsyncFile.AsyncStatus.Complete:
          casyncFile.WaitForComplete();
          casyncFile.Close();
          writer.Close();
          if (filedata.DateTime64 != 282578783305728UL)
          {
            try
            {
              DateTime dateTime = filedata.DateTime;
              Directory.SetLastWriteTime(filedata.LocalFilePath, dateTime);
            }
            catch (Exception ex)
            {
              Debug.WriteLine("failed set to time stamp " + ex.Message);
            }
          }
          return true;
        case CAsyncFile.AsyncStatus.Error:
          this.m_reader.Close();
          return true;
        default:
          return false;
      }
    }

    public uint RecalcCrc32InCpk(string cpkfilename, CFileInfo filedata)
    {
      if (!this.m_reader.ReadOpen(cpkfilename))
        return 0;
      int crc32 = (int) this.m_reader.GetCrc32(filedata.Offset, filedata.Filesize);
      this.m_reader.Close();
      return (uint) crc32;
    }

    public ulong RecalcCheckSum64InCpk(string cpkfilename, CFileInfo filedata)
    {
      if (!this.m_reader.ReadOpen(cpkfilename))
        return 0;
      long checkSum64 = (long) this.m_reader.GetCheckSum64(filedata.Offset, filedata.Filesize);
      this.m_reader.Close();
      return (ulong) checkSum64;
    }

    private Status ExecExtractStart()
    {
      ulong execFileCur = this.m_exec_file_cur;
      if ((long) execFileCur == (long) this.m_filedata.Count)
        return Status.Extracted;
      CFileInfo fileInfo = this.m_filedata.FileInfos[(int) execFileCur];
      if (fileInfo.IsCompressed && this.m_ver <= 1)
      {
        this.m_error_message = "Cannot extract a file. This is invalid CPK file format.";
        this.m_reader.Close();
        return Status.Error;
      }
      if (this.ExtractFileAsync(fileInfo, this.m_reader, this.m_extract_path.Replace("/", "\\"), true) && this.m_reader != null)
        return Status.ExtractCopying;
      object[] objArray = new object[4]
      {
        (object) fileInfo.ContentFilePath,
        null,
        null,
        null
      };
      uint fileId = fileInfo.FileId;
      objArray[1] = (object) fileId.ToString();
      ulong filesize = fileInfo.Filesize;
      objArray[2] = (object) filesize.ToString();
      ulong extractsize = fileInfo.Extractsize;
      objArray[3] = (object) extractsize.ToString();
      this.m_error_message = string.Format("Failed to extract a file.\n\"{0}\",\n ID={1},\n CompressedSize={2},\n ExtractedSize={3}", objArray);
      this.m_reader.Close();
      return Status.Error;
    }

    private Status ExecExtractCopying()
    {
      if (!this.IsCompleteExtractFileAsync(this.m_filedata.FileInfos[(int) this.m_exec_file_cur]))
        return Status.ExtractCopying;
      ++this.m_exec_file_cur;
      return Status.ExtractStart;
    }

    private Status ExecExtracted()
    {
      this.m_reader.Close();
      return Status.Complete;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool StartVerifyFile(string cpkfname, string outdir)
    {
      this.m_exec_mode = CBinary.ExecMode.VerifyFile;
      this.m_stat = Status.VerifyPreparing;
      this.m_extract_path = outdir;
      this.m_working_message = "";
      if (this.m_reader.ReadOpen(cpkfname))
        return true;
      string str = "Cannot read a file. \"" + cpkfname + "\"";
      this.m_error_message = str;
      Console.WriteLine(str);
      this.m_stat = Status.Error;
      return false;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool VerifyFile(string cpkfname, string outdir)
    {
      this.m_exec_mode = CBinary.ExecMode.VerifyFile;
      this.m_stat = Status.VerifyPreparing;
      this.m_extract_path = outdir;
      if (this.m_reader.ReadOpen(cpkfname))
        return true;
      string str = "Cannot read a file. \"" + cpkfname + "\"";
      this.m_error_message = str;
      Console.WriteLine(str);
      this.m_stat = Status.Error;
      return false;
    }

    public Status ExecVerifyPreparing()
    {
      this.m_exec_file_cur = 0UL;
      return Status.Verifying;
    }

    public Status ExecVerifying()
    {
      ulong execFileCur = this.m_exec_file_cur;
      if ((long) this.m_filedata.Count == (long) execFileCur)
        return Status.Verified;
      CFileInfo fileInfo = this.m_filedata.FileInfos[(int) execFileCur];
      string localFilePath = fileInfo.LocalFilePath;
      if (!File.Exists(localFilePath))
        throw new FileNotFoundException(fileInfo.LocalFilePath);
      if (this.ExtractFile(fileInfo, this.m_reader, this.m_extract_path.Replace("/", "\\"), true) && this.m_reader != null)
      {
        bool flag = CFileComparer.CompareFile(localFilePath, fileInfo.LocalFilePath);
        File.Delete(fileInfo.LocalFilePath);
        if (!flag)
        {
          this.m_error_message = string.Format("Verify Error : Not match with CPK content file. \"{1}\" (ID={0})", (object) fileInfo.FileId.ToString(), (object) fileInfo.ContentFilePath);
          this.m_reader.Close();
          return Status.Error;
        }
        uint fileId = fileInfo.FileId;
        this.m_working_message = string.Format("OK \"{0}\"(ID={1})", (object) fileInfo.ContentFilePath, (object) fileId.ToString());
        ++this.m_exec_file_cur;
        return Status.Verifying;
      }
      this.m_error_message = "Failed to extract. " + this.m_extract_path;
      this.m_reader.Close();
      return Status.Error;
    }

    public Status ExecVerified()
    {
      this.m_reader.Close();
      return Status.Complete;
    }

    private uint convToBigEndian32(uint val) => (val & 16711680U | val >> 16) >> 8 | (uint) (((int) val & 65280 | (int) val << 16) << 8);

    private ulong convToBigEndian64(ulong val) => (((val & 71776119061217280UL | val >> 16) >> 16 | val & 280375465082880UL) >> 16 | val & 1095216660480UL) >> 8 | (ulong) (((((long) val & 65280L | (long) val << 16) << 16 | (long) val & 16711680L) << 16 | (long) val & 4278190080L) << 8);

    [return: MarshalAs(UnmanagedType.U1)]
    private bool IsUncompresseFilename(CFileInfo file)
    {
      CExternalBuffer externalBuffer = this.m_external_buffer;
      if (externalBuffer != null)
      {
        ulong readBufferSize = (ulong) externalBuffer.ReadBufferSize;
        if (file.Filesize > readBufferSize)
          return true;
      }
      if (this.m_comp_codec == EnumCompressCodec.CodecDpkForceCrc)
        return false;
      string extension = Path.GetExtension(file.LocalFilePath);
      if (extension == "")
        return false;
      string[] uncompExtList = this.m_uncomp_ext_list;
      int index = 0;
      if (0 < uncompExtList.Length)
      {
        while (string.Compare(uncompExtList[index], extension, true) != 0)
        {
          ++index;
          if (index >= uncompExtList.Length)
            goto label_11;
        }
        return true;
      }
label_11:
      return false;
    }

    private static void MakeDirectory(string path)
    {
      if (string.IsNullOrEmpty(path))
        return;
      path = Path.GetDirectoryName(path);
      if (string.IsNullOrEmpty(path) || Directory.Exists(path))
        return;
      Directory.CreateDirectory(path);
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool SwapSwapFlag(void* ptr)
    {
      sbyte num = *(sbyte*) ((IntPtr) ptr + 4);
      *(sbyte*) ((IntPtr) ptr + 4) = ~num;
      return num != (sbyte) 0;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private unsafe bool SwapMaskOne(_iobuf* fp, ulong offset, ulong bufsize)
    {
      uint num1 = (uint) bufsize;
      void* voidPtr = \u003CModule\u003E.malloc(num1);
      int num2 = (int) offset;
      \u003CModule\u003E.fseek(fp, num2, 0);
      int num3 = (int) \u003CModule\u003E.fread(voidPtr, num1, 1U, fp);
      uint num4 = (uint) *(long*) ((IntPtr) voidPtr + 8);
      uint num5 = (uint) (bufsize + 18446744073709551600UL);
      if (num4 > num5)
        num4 = num5;
      byte* numPtr1 = (byte*) ((IntPtr) voidPtr + 16);
      uint num6 = 25951;
      if (0U < num4)
      {
        uint num7 = num4;
        do
        {
          byte* numPtr2 = numPtr1;
          int num8 = (int) *numPtr2 ^ (int) (byte) num6;
          *numPtr2 = (byte) num8;
          num6 *= 16661U;
          ++numPtr1;
          --num7;
        }
        while (num7 > 0U);
      }
      sbyte num9 = *(sbyte*) ((IntPtr) voidPtr + 4);
      *(sbyte*) ((IntPtr) voidPtr + 4) = ~num9;
      bool flag = num9 != (sbyte) 0;
      \u003CModule\u003E.fseek(fp, num2, 0);
      int num10 = (int) \u003CModule\u003E.fwrite(voidPtr, num1, 1U, fp);
      \u003CModule\u003E.free(voidPtr);
      return flag;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool SwapMask(string cpkFname)
    {
      sbyte* str = Utility.AllocCharString(cpkFname);
      _iobuf* fp = \u003CModule\u003E.fopen(str, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_03HMFOOINA\u0040r\u003F\u0024CLb\u003F\u0024AA\u0040);
      Utility.FreeCharString(str);
      if ((IntPtr) fp == IntPtr.Zero)
        return false;
      CBinary cbinary = this;
      cbinary.m_mask = cbinary.SwapMaskOne(fp, 0UL, 2048UL);
      ulong tocSize = this.m_toc_size;
      if (tocSize != 0UL)
        this.SwapMaskOne(fp, this.m_toc_offset, tocSize);
      ulong htocSize = this.m_htoc_size;
      if (htocSize != 0UL)
        this.SwapMaskOne(fp, this.m_htoc_offset, htocSize);
      ulong itocSize = this.m_itoc_size;
      if (itocSize != 0UL)
        this.SwapMaskOne(fp, this.m_itoc_offset, itocSize);
      ulong gtocSize = this.m_gtoc_size;
      if (gtocSize != 0UL)
        this.SwapMaskOne(fp, this.m_gtoc_offset, gtocSize);
      ulong hgtocSize = this.m_hgtoc_size;
      if (hgtocSize != 0UL)
        this.SwapMaskOne(fp, this.m_hgtoc_offset, hgtocSize);
      if (this.m_enable_etoc)
        this.SwapMaskOne(fp, this.m_etoc_offset, this.m_etoc_size);
      \u003CModule\u003E.fclose(fp);
      return true;
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (_param1)
      {
        this.\u007ECBinary();
      }
      else
      {
        try
        {
          this.\u0021CBinary();
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

    ~CBinary() => this.Dispose(false);

    private enum CopyStat
    {
      Prep,
      CopyStart,
      Copying,
    }

    private enum ExecMode
    {
      MakeCpkFile,
      ExtractFile,
      VerifyFile,
    }

    public enum EnumFileLayoutMode
    {
      LayoutModeUser,
      LayoutModeId,
      LayoutModeFilename,
      LayoutModeGroup,
    }

    private delegate void CBinaryDelegate(CBinary obj);
  }
}
