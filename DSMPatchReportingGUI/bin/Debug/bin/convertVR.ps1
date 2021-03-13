param
(
  [string]$filePath = ''
)
if (Test-Path -Path $filePath) {
    $data = Import-Csv -Path $filePath -Delimiter ';';
    
    $fileName = $filePath.Replace(".csv", ".xlsx")
    $dataList = $data
    $dataList | Export-Excel $fileName -Show -AutoSize -IncludePivotTable -PivotRows Patch -PivotData @{ Count = "sum" } -IncludePivotChart -ChartType ColumnClustered
}
else {
    Write-Host "File does not exists"
}
