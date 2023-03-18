// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.CSelfTest
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

using \u003CCppImplementationDetails\u003E;
using System;
using System.Runtime.InteropServices;

namespace CriCpkMaker
{
  public class CSelfTest
  {
    private CSelfTest.PrintEvent m_event;
    private int m_line;
    private unsafe void* g_heapbuf;

    public CSelfTest.PrintEvent PrintEventDelegate
    {
      get => this.m_event;
      set => this.m_event = value;
    }

    public void PrintWriteLine(CSelfTest.PrintEvent cb, string message)
    {
      if (!((MulticastDelegate) cb != (MulticastDelegate) null))
        return;
      int line1 = this.m_line;
      int line2 = line1;
      this.m_line = line1 + 1;
      cb(message, line2);
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public bool TestCpkFile(string fpath, CSelfTest.PrintEvent cb) => this.TestCpkFile(fpath, cb, (string) null, (string) null);

    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe bool TestCpkFile(
      string fpath,
      CSelfTest.PrintEvent cb,
      string testGgroupname,
      string testAttrName)
    {
      this.m_event = cb;
      this.m_line = 0;
      \u003CModule\u003E.criHeap_Initialize();
      sbyte* str1 = Utility.AllocCharString(fpath);
      _iobuf* iobufPtr;
      \u003CModule\u003E.fopen_s(&iobufPtr, str1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_02JDPG\u0040rb\u003F\u0024AA\u0040);
      Utility.FreeCharString(str1);
      if ((IntPtr) iobufPtr == IntPtr.Zero)
      {
        this.PrintWriteLine(cb, string.Format("Cannot open a file. \"{0}\"\r\n", (object) fpath));
        return false;
      }
      \u0024ArrayType\u0024\u0024\u0024BY0IAA\u0040D arrayTypeBy0IaaD1;
      int num1 = (int) \u003CModule\u003E.fread((void*) &arrayTypeBy0IaaD1, 1U, 2048U, iobufPtr);
      \u0024ArrayType\u0024\u0024\u0024BY0IAA\u0040D arrayTypeBy0IaaD2;
      _criheap_struct* criheapStructPtr1 = \u003CModule\u003E.criHeap_Create((void*) &arrayTypeBy0IaaD2, 2048);
      int fyapaxpaxjpbdjjZ = (int) \u003CModule\u003E.__unep\u0040\u003FUtfHeap_to_Heap_Alloc\u0040\u0040\u0024\u0024FYAPAXPAXJPBDJJ\u0040Z;
      CriUtfHeapObj criUtfHeapObj;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref criUtfHeapObj = fyapaxpaxjpbdjjZ;
      int heapFreeFyaxpaX0Z = (int) \u003CModule\u003E.__unep\u0040\u003FUtfHeap_to_Heap_Free\u0040\u0040\u0024\u0024FYAXPAX0\u0040Z;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &criUtfHeapObj + 4) = heapFreeFyaxpaX0Z;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &criUtfHeapObj + 8) = (int) criheapStructPtr1;
      CriCpkAnalyzerTag criCpkAnalyzerTag;
      \u003CModule\u003E.criCpkAnalyzer_Initialize(&criCpkAnalyzerTag);
      CriCpkHeaderInfoTag cpkHeaderInfoTag;
      if (\u003CModule\u003E.criCpkAnalyzer_GetHeaderInfoRtE(&criCpkAnalyzerTag, &cpkHeaderInfoTag, (void*) &arrayTypeBy0IaaD1, &criUtfHeapObj) == (CriCpkErrTag) 0)
      {
        \u003CModule\u003E.criHeap_Destroy(criheapStructPtr1);
        ulong tocOffset = \u003CModule\u003E.criCpkHeaderInfo_GetTocOffset(&cpkHeaderInfoTag);
        ulong tocSizeByte = \u003CModule\u003E.criCpkHeaderInfo_GetTocSizeByte(&cpkHeaderInfoTag);
        ulong etocOffset = (ulong) \u003CModule\u003E.criCpkHeaderInfo_GetEtocOffset(&cpkHeaderInfoTag);
        ulong etocSizeByte = (ulong) \u003CModule\u003E.criCpkHeaderInfo_GetEtocSizeByte(&cpkHeaderInfoTag);
        ulong gtocOffset = \u003CModule\u003E.criCpkHeaderInfo_GetGtocOffset(&cpkHeaderInfoTag);
        ulong gtocSizeByte = \u003CModule\u003E.criCpkHeaderInfo_GetGtocSizeByte(&cpkHeaderInfoTag);
        ulong itocOffset = \u003CModule\u003E.criCpkHeaderInfo_GetItocOffset(&cpkHeaderInfoTag);
        ulong itocSizeByte = \u003CModule\u003E.criCpkHeaderInfo_GetItocSizeByte(&cpkHeaderInfoTag);
        int version = (int) \u003CModule\u003E.criCpkHeaderInfo_GetVersion(&cpkHeaderInfoTag);
        int revision = (int) \u003CModule\u003E.criCpkHeaderInfo_GetRevision(&cpkHeaderInfoTag);
        ushort dataAlign = \u003CModule\u003E.criCpkHeaderInfo_GetDataAlign(&cpkHeaderInfoTag);
        this.PrintWriteLine(cb, string.Format("CPK File   = {0}\r\n", (object) fpath));
        int num2 = revision;
        int num3 = version;
        this.PrintWriteLine(cb, string.Format("CPK Ver.   = Ver.{0} Rev.{1}\r\n", (object) num3.ToString(), (object) num2.ToString()));
        uint num4 = (uint) tocSizeByte;
        this.PrintWriteLine(cb, string.Format("toc  offs  = 0x{0,8:X8} ({1,6})\r\n", (object) (uint) tocOffset, (object) num4));
        uint num5 = (uint) gtocSizeByte;
        this.PrintWriteLine(cb, string.Format("gtoc offs  = 0x{0,8:X8} ({1,6})\r\n", (object) (uint) gtocOffset, (object) num5));
        uint num6 = (uint) etocSizeByte;
        this.PrintWriteLine(cb, string.Format("etoc offs  = 0x{0,8:X8} ({1,6})\r\n", (object) (uint) etocOffset, (object) num6));
        uint num7 = (uint) itocSizeByte;
        this.PrintWriteLine(cb, string.Format("itoc offs  = 0x{0,8:X8} ({1,6})\r\n", (object) (uint) itocOffset, (object) num7));
        this.PrintWriteLine(cb, string.Format("Data align = {0}\r\n", (object) dataAlign));
        this.PrintWriteLine(cb, string.Format("Sorted     = {0}\r\n", (object) \u003CModule\u003E.criCpkHeaderInfo_IsSorted(&cpkHeaderInfoTag)));
        string str2 = (IntPtr) \u003CModule\u003E.criCpkHeaderInfo_GetTver(&cpkHeaderInfoTag) != IntPtr.Zero ? new string(\u003CModule\u003E.criCpkHeaderInfo_GetTver(&cpkHeaderInfoTag)) : "N/A";
        this.PrintWriteLine(cb, string.Format("Tool Ver.  = {0}\r\n", (object) str2));
        void* voidPtr1 = (void*) 0;
        void* voidPtr2 = (void*) 0;
        void* voidPtr3 = (void*) 0;
        void* voidPtr4 = (void*) 0;
        uint num8 = (uint) (itocSizeByte + gtocSizeByte + etocSizeByte + tocSizeByte + 8192UL);
        void* voidPtr5 = \u003CModule\u003E.malloc(num8);
        this.g_heapbuf = voidPtr5;
        _criheap_struct* criheapStructPtr2 = \u003CModule\u003E.criHeap_Create(voidPtr5, (int) num8);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref criUtfHeapObj = fyapaxpaxjpbdjjZ;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &criUtfHeapObj + 4) = heapFreeFyaxpaX0Z;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &criUtfHeapObj + 8) = (int) criheapStructPtr2;
        if (tocSizeByte != 0UL)
        {
          voidPtr1 = \u003CModule\u003E.criHeap_AllocFix(criheapStructPtr2, (int) tocSizeByte, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_06KNDGDNLJ\u0040tocbuf\u003F\u0024AA\u0040, 0);
          \u003CModule\u003E._fseeki64(iobufPtr, (long) tocOffset, 0);
          int num9 = (int) \u003CModule\u003E.fread(voidPtr1, 1U, num4, iobufPtr);
        }
        if (etocSizeByte != 0UL)
        {
          voidPtr2 = \u003CModule\u003E.criHeap_AllocFix(criheapStructPtr2, (int) etocSizeByte, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07MNHDJDHB\u0040etocbuf\u003F\u0024AA\u0040, 0);
          \u003CModule\u003E._fseeki64(iobufPtr, (long) etocOffset, 0);
          int num10 = (int) \u003CModule\u003E.fread(voidPtr2, 1U, num6, iobufPtr);
        }
        if (itocSizeByte != 0UL)
        {
          voidPtr3 = \u003CModule\u003E.criHeap_AllocFix(criheapStructPtr2, (int) itocSizeByte, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07JKNMIGDO\u0040itocbuf\u003F\u0024AA\u0040, 0);
          \u003CModule\u003E._fseeki64(iobufPtr, (long) itocOffset, 0);
          int num11 = (int) \u003CModule\u003E.fread(voidPtr3, 1U, num7, iobufPtr);
        }
        if (gtocSizeByte != 0UL)
        {
          voidPtr4 = \u003CModule\u003E.criHeap_AllocFix(criheapStructPtr2, (int) gtocSizeByte, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07IPFGJEAM\u0040gtocbuf\u003F\u0024AA\u0040, 0);
          \u003CModule\u003E._fseeki64(iobufPtr, (long) gtocOffset, 0);
          int num12 = (int) \u003CModule\u003E.fread(voidPtr4, 1U, num5, iobufPtr);
        }
        \u003CModule\u003E.fclose(iobufPtr);
        CriCpkTocInfoTag criCpkTocInfoTag;
        if (tocSizeByte != 0UL && etocSizeByte != 0UL && \u003CModule\u003E.criCpkHeaderInfo_GetTocInfo(&cpkHeaderInfoTag, &criCpkTocInfoTag, voidPtr1, &criUtfHeapObj) == 1)
        {
          int numFiles = \u003CModule\u003E.criCpkTocInfo_GetNumFiles(&criCpkTocInfoTag);
          CriCpkEtocInfoTag criCpkEtocInfoTag;
          if (\u003CModule\u003E.criCpkHeaderInfo_GetEtocInfo(&cpkHeaderInfoTag, &criCpkEtocInfoTag, voidPtr2, &criUtfHeapObj) == 1)
          {
            this.PrintWriteLine(cb, string.Format("Files      = {0}\r\n", (object) numFiles));
            this.PrintWriteLine(cb, string.Format("BaseDir    = {0}\r\n", (object) new string(\u003CModule\u003E.criCpkEtocInfo_GetBaseDirectory(&criCpkEtocInfoTag))));
            object[] objArray1 = new object[0];
            this.PrintWriteLine(cb, string.Format("\r\n=== Detecting Test ===\r\n", objArray1));
            int num13 = 0;
            if (0 < numFiles)
            {
              do
              {
                CriFsCpkFileInfoTag fsCpkFileInfoTag;
                \u003CModule\u003E.criCpkTocInfo_GetFileInfo(&criCpkTocInfoTag, &fsCpkFileInfoTag, num13);
                CriCpkEfileInfoTag criCpkEfileInfoTag;
                \u003CModule\u003E.criCpkEtocInfo_GetFileInfo(&criCpkEtocInfoTag, &criCpkEfileInfoTag, num13);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                object[] objArray2 = new object[4]
                {
                  (object) num13,
                  (object) (uint) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 8),
                  (object) (uint) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 12),
                  (object) (^(long&) ((IntPtr) &fsCpkFileInfoTag + 16) + 2048L)
                };
                this.PrintWriteLine(cb, string.Format("[{0,5}] {1,8}/{2,8} 0x{3,8:X8} ", objArray2));
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                if (^(int&) ref fsCpkFileInfoTag != 0)
                {
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  this.PrintWriteLine(cb, string.Format("\"{0}/{1}\"\r\n", (object) new string((sbyte*) ^(int&) ref fsCpkFileInfoTag), (object) new string((sbyte*) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 4))));
                }
                else
                {
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  this.PrintWriteLine(cb, string.Format("\"{0}\"\r\n", (object) new string((sbyte*) ^(int&) ((IntPtr) &fsCpkFileInfoTag + 4))));
                }
                ++num13;
              }
              while (num13 < numFiles);
            }
          }
          this.PrintWriteLine(cb, "\r\n=== File Search Test ===\r\n");
          int num14 = 0;
          if (0 < numFiles)
          {
            do
            {
              CriFsCpkFileInfoTag fsCpkFileInfoTag1;
              \u003CModule\u003E.criCpkTocInfo_GetFileInfo(&criCpkTocInfoTag, &fsCpkFileInfoTag1, num14);
              \u0024ArrayType\u0024\u0024\u0024BY0CAA\u0040D arrayTypeBy0CaaD;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              if (*(sbyte*) ^(int&) ref fsCpkFileInfoTag1 != (sbyte) 0)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                \u003CModule\u003E.strcpy_s\u003C512\u003E(&arrayTypeBy0CaaD, (sbyte*) ^(int&) ref fsCpkFileInfoTag1);
                \u003CModule\u003E.strcat_s\u003C512\u003E(&arrayTypeBy0CaaD, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_01KMDKNFGN\u0040\u003F1\u003F\u0024AA\u0040);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                \u003CModule\u003E.strcat_s\u003C512\u003E(&arrayTypeBy0CaaD, (sbyte*) ^(int&) ((IntPtr) &fsCpkFileInfoTag1 + 4));
              }
              else
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                \u003CModule\u003E.strcpy_s\u003C512\u003E(&arrayTypeBy0CaaD, (sbyte*) ^(int&) ((IntPtr) &fsCpkFileInfoTag1 + 4));
              }
              this.PrintWriteLine(cb, string.Format("[{0,5}] check \"{1}\" ", (object) num14, (object) new string((sbyte*) &arrayTypeBy0CaaD)));
              CriFsCpkFileInfoTag fsCpkFileInfoTag2;
              if ((\u003CModule\u003E.criCpkHeaderInfo_IsSorted(&cpkHeaderInfoTag) != 1 ? \u003CModule\u003E.criCpkTocInfo_GetFileInfoLinearSearch(&criCpkTocInfoTag, &fsCpkFileInfoTag2, (sbyte*) &arrayTypeBy0CaaD) : \u003CModule\u003E.criCpkTocInfo_GetFileInfoBinarySearch(&criCpkTocInfoTag, &fsCpkFileInfoTag2, (sbyte*) &arrayTypeBy0CaaD)) == 1)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                if (^(long&) ((IntPtr) &fsCpkFileInfoTag2 + 16) == ^(long&) ((IntPtr) &fsCpkFileInfoTag1 + 16))
                {
                  this.PrintWriteLine(cb, "OK \r\n");
                  ++num14;
                }
                else
                  goto label_25;
              }
              else
                goto label_26;
            }
            while (num14 < numFiles);
            goto label_27;
label_25:
            object[] objArray = new object[0];
            this.PrintWriteLine(cb, string.Format("\r\nfile offset error\r\n", objArray));
            \u003CModule\u003E.free(this.g_heapbuf);
            return false;
label_26:
            this.PrintWriteLine(cb, "not found\r\n ");
            \u003CModule\u003E.free(this.g_heapbuf);
            return false;
          }
        }
label_27:
        CriCpkItocInfoTag criCpkItocInfoTag;
        if (itocSizeByte != 0UL && \u003CModule\u003E.criCpkHeaderInfo_GetItocInfo(&cpkHeaderInfoTag, &criCpkItocInfoTag, voidPtr3, &criUtfHeapObj) == 1 && \u003CModule\u003E.criCpkHeaderInfo_IsExtraId(&cpkHeaderInfoTag) == 0)
        {
          int numFilesL = \u003CModule\u003E.criCpkItocInfo_GetNumFilesL(&criCpkItocInfoTag);
          int numFilesH = \u003CModule\u003E.criCpkItocInfo_GetNumFilesH(&criCpkItocInfoTag);
          int numFiles = \u003CModule\u003E.criCpkItocInfo_GetNumFiles(&criCpkItocInfoTag);
          this.PrintWriteLine(cb, "\r\n=== Detecting Test (ID) ===\r\n");
          this.PrintWriteLine(cb, "- Low ---------\r\n");
          uint num15 = 0;
          int num16 = 0;
          CriCpkIfileInfoTag criCpkIfileInfoTag1;
          if (0 < numFilesL)
          {
            do
            {
              if (\u003CModule\u003E.criCpkItocInfo_GetFileInfoL(&criCpkItocInfoTag, &criCpkIfileInfoTag1, (int) (ushort) num16) == 1)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                object[] objArray = new object[5]
                {
                  (object) num16,
                  (object) ^(int&) ref criCpkIfileInfoTag1,
                  (object) (uint) ^(int&) ((IntPtr) &criCpkIfileInfoTag1 + 4),
                  (object) (uint) ((int) ((uint) (^(int&) ((IntPtr) &criCpkIfileInfoTag1 + 4) + (int) dataAlign - 1) / (uint) dataAlign) * (int) dataAlign),
                  (object) ^(long&) ((IntPtr) &criCpkIfileInfoTag1 + 16)
                };
                this.PrintWriteLine(cb, string.Format("[{0,5}] ID={1,5} fsize={2,8}({3,8}), offs=0x{4,8:X8} ", objArray));
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                num15 = (uint) (^(int&) ((IntPtr) &criCpkIfileInfoTag1 + 4) + (int) dataAlign - 1) / (uint) dataAlign * (uint) dataAlign + num15;
                this.PrintWriteLine(cb, string.Format(" {0,8} ", (object) (int) num15));
                CriCpkIfileInfoTag criCpkIfileInfoTag2;
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
                if (\u003CModule\u003E.criCpkItocInfo_GetFileInfoById(&criCpkItocInfoTag, &criCpkIfileInfoTag2, ^(int&) ref criCpkIfileInfoTag1) == 1 && ^(long&) ((IntPtr) &criCpkIfileInfoTag1 + 16) == ^(long&) ((IntPtr) &criCpkIfileInfoTag2 + 16) && ^(int&) ref criCpkIfileInfoTag1 == ^(int&) ref criCpkIfileInfoTag2)
                  this.PrintWriteLine(cb, "OK\r\n");
                else
                  goto label_33;
              }
              ++num16;
            }
            while (num16 < numFilesL);
            goto label_34;
label_33:
            this.PrintWriteLine(cb, "error\r\n");
            \u003CModule\u003E.free(this.g_heapbuf);
            return false;
          }
label_34:
          this.PrintWriteLine(cb, "- Hi ----------\r\n");
          uint num17 = 0;
          int num18 = 0;
          if (0 < numFilesH)
          {
            do
            {
              if (\u003CModule\u003E.criCpkItocInfo_GetFileInfoH(&criCpkItocInfoTag, &criCpkIfileInfoTag1, (int) (ushort) num18) == 1)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                object[] objArray = new object[5]
                {
                  (object) num18,
                  (object) ^(int&) ref criCpkIfileInfoTag1,
                  (object) (uint) ^(int&) ((IntPtr) &criCpkIfileInfoTag1 + 4),
                  (object) (uint) ((int) ((uint) (^(int&) ((IntPtr) &criCpkIfileInfoTag1 + 4) + (int) dataAlign - 1) / (uint) dataAlign) * (int) dataAlign),
                  (object) ^(long&) ((IntPtr) &criCpkIfileInfoTag1 + 16)
                };
                this.PrintWriteLine(cb, string.Format("[{0,5}] ID={1,5} fsize={2,8}({3,8}), offs=0x{4,8:X8} ", objArray));
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                num17 = (uint) (^(int&) ((IntPtr) &criCpkIfileInfoTag1 + 4) + (int) dataAlign - 1) / (uint) dataAlign * (uint) dataAlign + num17;
                this.PrintWriteLine(cb, string.Format(" {0,8} ", (object) (int) num17));
                CriCpkIfileInfoTag criCpkIfileInfoTag3;
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
                if (\u003CModule\u003E.criCpkItocInfo_GetFileInfoById(&criCpkItocInfoTag, &criCpkIfileInfoTag3, ^(int&) ref criCpkIfileInfoTag1) == 1 && ^(long&) ((IntPtr) &criCpkIfileInfoTag1 + 16) == ^(long&) ((IntPtr) &criCpkIfileInfoTag3 + 16) && ^(int&) ref criCpkIfileInfoTag1 == ^(int&) ref criCpkIfileInfoTag3)
                  this.PrintWriteLine(cb, "OK\r\n");
                else
                  goto label_39;
              }
              ++num18;
            }
            while (num18 < numFilesH);
            goto label_40;
label_39:
            this.PrintWriteLine(cb, "error\r\n");
            \u003CModule\u003E.free(this.g_heapbuf);
            return false;
          }
label_40:
          this.PrintWriteLine(cb, "\r\n=== File Search Test (ID) ===\r\n");
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          uint num19 = (uint) ^(int&) ((IntPtr) &cpkHeaderInfoTag + 8);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(int&) ((IntPtr) &cpkHeaderInfoTag + 8) == 0)
            num19 = 2048U;
          int num20 = 0;
          if (0 < numFiles)
          {
            do
            {
              if (\u003CModule\u003E.criCpkItocInfo_GetFileInfo(&criCpkItocInfoTag, &criCpkIfileInfoTag1, (int) (ushort) num20) == 1)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                this.PrintWriteLine(cb, string.Format("ID={0,5} fsize={1,8}, offs=0x{2,8:X8} /", (object) ^(int&) ref criCpkIfileInfoTag1, (object) (uint) ^(int&) ((IntPtr) &criCpkIfileInfoTag1 + 4), (object) ^(long&) ((IntPtr) &criCpkIfileInfoTag1 + 16)));
                this.PrintWriteLine(cb, string.Format(" {0,8} ", (object) (int) num19));
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                if (^(long&) ((IntPtr) &criCpkIfileInfoTag1 + 16) == (long) num19)
                {
                  this.PrintWriteLine(cb, "OK\r\n");
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  num19 = (uint) (^(int&) ((IntPtr) &criCpkIfileInfoTag1 + 4) + (int) dataAlign - 1) / (uint) dataAlign * (uint) dataAlign + num19;
                }
                else
                  goto label_47;
              }
              ++num20;
            }
            while (num20 < numFiles);
            goto label_48;
label_47:
            this.PrintWriteLine(cb, "error\r\n");
            \u003CModule\u003E.free(this.g_heapbuf);
            return false;
          }
label_48:
          \u003CModule\u003E.criCpkItocInfo_GetFileInfoById(&criCpkItocInfoTag, &criCpkIfileInfoTag1, 10);
        }
        if (tocSizeByte != 0UL && gtocSizeByte != 0UL)
        {
          CriCpkGtocInfoTag criCpkGtocInfoTag;
          \u003CModule\u003E.criCpkHeaderInfo_GetGtocInfo(&cpkHeaderInfoTag, &criCpkGtocInfoTag, voidPtr4, &criUtfHeapObj, &criCpkTocInfoTag);
        }
        if ((IntPtr) voidPtr1 != IntPtr.Zero)
          \u003CModule\u003E.criHeap_Free(criheapStructPtr2, voidPtr1);
        if ((IntPtr) voidPtr2 != IntPtr.Zero)
          \u003CModule\u003E.criHeap_Free(criheapStructPtr2, voidPtr2);
        if ((IntPtr) voidPtr3 != IntPtr.Zero)
          \u003CModule\u003E.criHeap_Free(criheapStructPtr2, voidPtr3);
        if ((IntPtr) voidPtr4 != IntPtr.Zero)
          \u003CModule\u003E.criHeap_Free(criheapStructPtr2, voidPtr4);
      }
      \u003CModule\u003E.free(this.g_heapbuf);
      \u003CModule\u003E.criHeap_Finalize();
      return true;
    }

    public delegate void PrintEvent(string msg, int line);
  }
}
