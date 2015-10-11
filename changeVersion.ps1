$VerFromAssemblyInfo = get-content SharedAssemblyInfo.cs
$InstallScript = get-content Install\installscriptMemoNoteUnicode.iss
$VerFromAssemblyInfo[27][30]
$i = 31
$verString = ""
do
	{
	$verString = $verString + $VerFromAssemblyInfo[27][$i]
	$i = $i + 1
	}
while($VerFromAssemblyInfo[27][$i] -ne ')')
$verString
$InstallScript[2]
$InstallScript[2] = "#define Version " + $verString
$InstallScript[2]
$InstallScript | set-content Install\installscriptMemoNoteUnicode.iss

  
