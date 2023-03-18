// Decompiled with JetBrains decompiler
// Type: CriCpkMaker.Status
// Assembly: CpkMaker, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8438E53E-A2A1-46CA-8FB7-83B4637BCEA1
// Assembly location: C:\Users\PC\source\repos\YACpkTool\YACT\CpkMaker.dll

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
    ItocBuilding = 12, // 0x0000000C
    GtocBuilding = 13, // 0x0000000D
    EtocBuilding = 14, // 0x0000000E
    HeaderRewriting = 15, // 0x0000000F
    MselfRewriting = 16, // 0x00000010
    CpkBuildFinalize = 17, // 0x00000011
    Complete = 50, // 0x00000032
    ExtractPreparing = 100, // 0x00000064
    Extracting = 101, // 0x00000065
    Extracted = 102, // 0x00000066
    VerifyPreparing = 200, // 0x000000C8
    Verifying = 201, // 0x000000C9
    Verified = 202, // 0x000000CA
  }
}
