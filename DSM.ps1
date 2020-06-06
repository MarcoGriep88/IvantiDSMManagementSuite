$test = 0;
$global:isLoggedIn = $false

function IdentifyWhoIAm() {
  whoami;
}

function Login() {
  $usr = $global:setting.Username;
  $pass = $global:setting.Password;
  $srv = $global:setting.API;

  Write-Host "Trying to Login on $srv"

  $postParams = @{"username"=$usr;"password"=$pass }  | ConvertTo-Json

  $response = Invoke-WebRequest -Uri "$srv/Auth/login" -Method POST -ContentType "application/json" -Body $postParams
  
  if ($response.StatusDescription -eq "OK") {
    Write-Host "Successful"
    $token = ConvertFrom-Json $response.Content
    $global:authToken = $token.token
	$global:isLoggedIn = $true
  }
  else {
    Write-Host "Failed on Host $srv"
    Write-Host $response
  }
}

function PostTestData() {
  $usr = $global:setting.Username;
  $pass = $global:setting.Password;
  $srv = $global:setting.API;

  Write-Host "Trying to Post Data To $srv"

  $now = [System.DateTime]::Now
  $postParams = @{"Computer"="Test";"Patch"="Microsoft Patch";"Compliance"="Fixed";"FoundDate"=$now;"FixedDate"=$now }  | ConvertTo-Json
  $headerParams = @{"Authorization"="Bearer $global:authToken"}

  $response = Invoke-WebRequest -Uri "$srv/PatchData" -Method POST -ContentType "application/json" -Body $postParams -Header $headerParams
  
  if ($response.StatusDescription -eq "Created") {
    Write-Host "Successful"
  }
}

function PostData($postParams) {
  $usr = $global:setting.Username;
  $pass = $global:setting.Password;
  $srv = $global:setting.API;

  Write-Host "Trying to Post Data to $srv"

  $headerParams = @{"Authorization"="Bearer $global:authToken"}

  $postData = $postParams | ConvertTo-Json
  Write-Host $postData
  $response = Invoke-WebRequest -Uri "$srv/PatchData" -Method POST -ContentType "application/json" -Body $postData -Header $headerParams
  
  if ($response.StatusDescription -eq "Created") {
    Write-Host "Successful"
  }
}

function ConnectDSM() {
  import-module psx7 -DisableNameChecking
  $srv = $global:setting.BLS
  $Server = "\\$srv";
  $Username = $global:setting.BLSUser;
  $global:path = $global:setting.Context
  $password = $global:setting.BLSPassword | ConvertTo-SecureString -asPlainText -Force
  $credential = New-Object System.Management.Automation.PSCredential($Username, $password)
  Write-Host "Using context: " + $global:path
  new-psdrive -name emdb -root $Server -scope script -psprovider blsemdb -Credential $credential
  emdb:
  $contextTokens = $global:path.Split('\\')
  $token = $contextTokens[$contextTokens.Length-2].Replace('&','').Replace(' ','_').Replace('(','').Replace(')','').Replace('.','')
  Write-Host $token
}

function ProductiveSequence() {
    ConnectDSM

    $computers = Get-EmdbComputer $context -Recurse
  
    foreach ($machine in $computers)
    {
      $issues = Get-EmdbComputer $context -Name $machine.Name -Recurse | ForEach-Object { $_.GetAssociations("ComputerMissingPatch") } | Add-EmdbRelatedItem -PassThru
      
      foreach ($secIssue in $issues)
      {
		$fixDate = $null
	    if ($secIssue.FixDate -ne $null) 
		{
			$fixDate = $secIssue.FixDate.ToString("yyyy-MM-dd hh:mm:ss")
		}
		
		$foundDate = $null
		if ($secIssue.DetectDate -ne $null) 
		{
			$foundDate = $secIssue.DetectDate.ToString("yyyy-MM-dd hh:mm:ss")
		}
		
        Write-Host $secIssue.GetTargetObject().Name " - " $secIssue.Status
        $postParams = @{"Computer"=$machine.Name;"Patch"=$secIssue.GetTargetObject().Name.Replace(',',' ');"Compliance"=$secIssue.Status;"FoundDate"=$foundDate;"FixDate"=$fixDate }
        PostData($postParams)
      }
    }
}

IdentifyWhoIAm

$dllPath = "C:\Program Files (x86)\DSMPatchReporting\dsmaddons.dll"
if (!(Test-Path $dllPath)) {
  Write-Host "DLL does not exists"
  return
}
[System.Reflection.Assembly]::LoadFile($dllPath)
$iConfigWriter = New-Object DSMAddons.EncryptedConfigWriter

$setting = $iConfigWriter.DefaultSettings
$global:setting = $iConfigWriter.ReadConfig()
Login

if ($global:isLoggedIn -eq $true) {
	if ($test -ne 1) { 
	  ProductiveSequence
	}
	else {
	  PostTestData
	}
}
