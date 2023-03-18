// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.Status
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8124CD20-2A9C-473B-9245-6C6D20C16CB7
// Assembly location: E:\bon\Tools\CRIFileSystemTools\CpkMaker.dll

namespace CriCpkMaker
{
  public enum Status
  {
    Error = -1, // 0xFFFFFFFF
    Stop = 0,
    HeaderMselfSkipping = 1,
    HeaderSkipping = 2,
    PreFileDataBuilding = 3,
    FileDataBuildingPrep = 4,
    FileDataBuildingStart = 5,
    FileDataBuildingCopying = 6,
    TocInfoMaking = 7,
    ItocInfoMaking = 8,
    GtocInfoMaking = 9,
    PreTocBuilding = 10, // 0x0000000A
    TocBuilding = 11, // 0x0000000B
    HtocInfoMaking = 12, // 0x0000000C
    HtocBuilding = 13, // 0x0000000D
    ItocBuilding = 14, // 0x0000000E
    GtocBuilding = 15, // 0x0000000F
    HgtocInfoMaking = 16, // 0x00000010
    HgtocBuilding = 17, // 0x00000011
    EtocBuilding = 18, // 0x00000012
    HeaderRewriting = 19, // 0x00000013
    MselfRewriting = 20, // 0x00000014
    CpkBuildFinalize = 21, // 0x00000015
    TryCompress = 22, // 0x00000016
    TryUnification = 23, // 0x00000017
    TryCompressEnd = 24, // 0x00000018
    TryUnificationEnd = 25, // 0x00000019
    Complete = 50, // 0x00000032
    ExtractPreparing = 100, // 0x00000064
    ExtractStart = 101, // 0x00000065
    ExtractCopying = 102, // 0x00000066
    Extracted = 103, // 0x00000067
    VerifyPreparing = 200, // 0x000000C8
    Verifying = 201, // 0x000000C9
    Verified = 202, // 0x000000CA
  }
}
