
function IsRequiredDotNetDetected(version: string; service: cardinal): boolean;
var
    key: string;
    install, serviceCount: cardinal;
    success: boolean;
begin
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;
    // .NET 3.0 uses value InstallSuccess in subkey Setup
    if Pos('v3.0', version) = 1 then begin
        success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
    end else begin
        success := RegQueryDWordValue(HKLM, key, 'Install', install);
    end;
    if Pos('v4', version) = 1 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
    end else begin
        success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
    end;
    result := success and (install = 1) and (serviceCount >= service);
end;

function GetUninstallString: string;
var
  sUnInstPath: string;
  sUnInstallString: String;
begin
  Result := '';
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{{667DDB08-76CB-4DB6-B35D-D4B48F4AD8CB}_is1'); //Your App GUID/ID
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;
function InitializeSetup: Boolean;
var
  V: Integer;
  iResultCode: Integer;
  sUnInstallString: string;
  ErrCode: integer;
begin
  Result := True;
  if RegValueExists(HKEY_LOCAL_MACHINE,'Software\Microsoft\Windows\CurrentVersion\Uninstall\{667DDB08-76CB-4DB6-B35D-D4B48F4AD8CB}_is1', 'UninstallString') then  
  begin
    V := MsgBox(ExpandConstant('WARNING! Old version of MemoNote is found on your computer. Do you want to uninstall it?'), mbInformation, MB_YESNO); 
    if V = IDYES then
    begin
      sUnInstallString := GetUninstallString();
      sUnInstallString :=  RemoveQuotes(sUnInstallString);
      Exec(ExpandConstant(sUnInstallString), '', '', SW_SHOW, ewWaitUntilTerminated, iResultCode);
      Result := True;                
    end
    else
      Result := False;
  end;
  if not IsRequiredDotNetDetected('v4\Client', 0) then 
	begin
        V := MsgBox('MemoNote requires Microsoft .NET Framework 4.0 Client Profile.'#13#13
            'Please install Microsoft .NET Framework 4.0 and re-install this app.'#13
            'Do you want to go to microsoft.com and download it now?', mbInformation, MB_YESNO);
        if V = IDYES then
		begin
			ShellExec('open', 'http://www.microsoft.com/en-us/download/details.aspx?id=42643','', '', SW_SHOW, ewNoWait, ErrCode);
			result := false;
		end
		else
			result := false;		
    end 
  else  
    result := true;
end;