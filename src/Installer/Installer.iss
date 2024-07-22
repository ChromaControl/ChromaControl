[Setup]
AppId=ChromaControl
AppName={#Product}
AppVersion={#Version}
AppPublisher={#Authors}
WizardStyle=modern
DefaultDirName={autopf}\{#Product}
DefaultGroupName={#Product}
UninstallDisplayName={#Product}
UninstallDisplayIcon={app}\ChromaControl.App.exe
Compression=lzma2
SolidCompression=yes
OutputDir={#ObjDir}
OutputBaseFileName=ChromaControlSetup-{#Version}
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
PrivilegesRequired=admin
CloseApplications=force
SetupIconFile={#PublishDir}\wwwroot\favicon.ico
LicenseFile={#LicenseFile}
MissingRunOnceIdsWarning=no

[Files]
Source: "{#PublishDir}\**"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion

[Icons]
Name: "{group}\{#Product}"; Filename: "{app}\ChromaControl.App.exe"
Name: "{commonstartup}\{#Product}"; Filename: "{app}\ChromaControl.Service.exe"

[Run]
Filename: "{app}\ChromaControl.Service.exe"; StatusMsg: "Starting Chroma Control Service..."; Flags: runhidden nowait
Filename: "{app}\ChromaControl.App.exe"; Description: "Start Chroma Control"; Flags: postinstall nowait skipifsilent

[UninstallRun]
Filename: "{sys}\taskkill.exe"; Parameters: "/F /IM ChromaControl.App.exe"; StatusMsg: "Stopping Chroma Control..."; Flags: runhidden
Filename: "{sys}\taskkill.exe"; Parameters: "/F /IM ChromaControl.Service.exe"; StatusMsg: "Stopping Chroma Control Service..."; Flags: runhidden
