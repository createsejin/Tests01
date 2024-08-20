function mk_sym_link_old([string]$target_path, [string]$link_name) {
  $command = '-p PowerShell -d "C:\Users\creat\Projects\the_cave\tests\downloader_test02\LoggerTest" New-Item -ItemType SymbolicLink -Path "config.yaml" -Target "C:\Program Files\OpenTelemetry Collector\config.yaml"'
  Start-Process -FilePath "wt" -WorkingDirectory "$pwd" -Wait -Verb RunAs -ArgumentList $command
}
function mk_sym_link_old2([string]$target_path, [string]$link_name) {
  Start-Process -FilePath "wt" -WorkingDirectory "$pwd" -Wait -Verb RunAs -ArgumentList "-p PowerShell -d $pwd" Write-Output, `"test` test`"
}
mk_sym_link_old2