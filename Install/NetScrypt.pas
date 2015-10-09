//-----------------------------------------------------------------------------
//  I?iaa?ea iaee?ey io?iiai o?aeiai?ea
//-----------------------------------------------------------------------------
function IsDotNetDetected(version: string; release: cardinal): boolean;

var 
    reg_key: string; // I?iniao?eaaaiue iia?acaae nenoaiiiai ?aano?a
    success: boolean; // Oeaa iaee?ey cai?aoeaaaiie aa?nee .NET
    release45: cardinal; // Iiia? ?aeeca aey aa?nee 4.5.x
    key_value: cardinal; // I?i?eoaiiia ec ?aano?a cia?aiea ee??a
    sub_key: string;

begin

    success := false;
    reg_key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\';
    
    // A?aney 3.0
    if Pos('v3.0', version) = 1 then
      begin
          sub_key := 'v3.0';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'InstallSuccess', key_value);
          success := success and (key_value = 1);
      end;

    // A?aney 3.5
    if Pos('v3.5', version) = 1 then
      begin
          sub_key := 'v3.5';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Install', key_value);
          success := success and (key_value = 1);
      end;

     // A?aney 4.0 eeeaioneee i?ioeeu
     if Pos('v4.0 Client Profile', version) = 1 then
      begin
          sub_key := 'v4\Client';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Install', key_value);
          success := success and (key_value = 1);
      end;

     // A?aney 4.0 ?anoe?aiiue i?ioeeu
     if Pos('v4.0 Full Profile', version) = 1 then
      begin
          sub_key := 'v4\Full';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Install', key_value);
          success := success and (key_value = 1);
      end;

     // A?aney 4.5
     if Pos('v4.5', version) = 1 then
      begin
          sub_key := 'v4\Full';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Release', release45);
          success := success and (release45 >= release);
      end;
        
    result := success;

end;

//-----------------------------------------------------------------------------
//  Ooieoey-iaa?oea aey aaoaeoe?iaaiey eiie?aoiie io?iie iai aa?nee
//-----------------------------------------------------------------------------
function IsRequiredDotNetDetected(): boolean;
begin
    result := IsDotNetDetected('v4.0 Full Profile', 0);
end;

//-----------------------------------------------------------------------------
//    Callback-ooieoey, aucuaaaiay i?e eieoeaeecaoee onoaiiaee
//-----------------------------------------------------------------------------
function InitializeSetup(): boolean;
begin

  // Anee iao oa?aoaiie aa?nee .NET auaiaei niiauaiea i oii, ?oi einoaeeyoi?
  // iiiuoaaony onoaiiaeou a? ia aaiiue eiiiu?oa?
  if not IsDotNetDetected('v4.0 Full Profile', 0) then
    begin
      MsgBox('{#Name} requires Microsoft .NET Framework 4.0 Full Profile.'#13#13
             'The installer will attempt to install it', mbInformation, MB_OK);
    end;   

  result := true;
end;