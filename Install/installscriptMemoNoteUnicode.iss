
#define Name "MemoNote"
#define Version "0.0.0.16"
#define Publisher "Vold"
#define URL "http://www.google.com"
#define ExeName "MemoNote.exe"
[Setup]
AppId={{361E65A9-ABA7-44CA-A747-99B28252BA56}}
AppName={#Name}
AppVersion={#Version}
AppPublisher={#Publisher}
AppPublisherURL={#URL}
AppSupportURL={#URL}
AppUpdatesURL={#URL}
DefaultDirName={pf}\{#Name}
DefaultGroupName={#Name}
OutputDir="..\MemoNote\Install\Installer"
OutputBaseFileName=test-setup
SetupIconFile=MemoIco2.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "License_ENG.txt"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"; LicenseFile: "License_RUS.txt"
[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
[Files]


Source: "..\MemoNote\bin\Debug\MemoNote.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MemoNote\bin\Debug\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
[Icons]
Name: "{group}\{#Name}"; Filename: "{app}\{#ExeName}"
Name: "{commondesktop}\{#Name}"; Filename: "{app}\{#ExeName}"; Tasks: desktopicon
[Code]
#include "ReInstall.pas"



