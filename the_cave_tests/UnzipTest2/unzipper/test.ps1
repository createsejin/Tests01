function CargoTest {
  param (
    [Parameter(Mandatory = $true)]
    [string]$parent_module,
    [Parameter(Mandatory = $true)]
    [int]$test_module_num,
    [int]$test_method_num
  )
  if (0 -eq $test_method_num) {
    cargo test -p rar_unzipper --bin rar_unzipper -- `
      $parent_module::tests$test_module_num --show-output
  }
  else {
    cargo test -p rar_unzipper --bin rar_unzipper -- `
      $parent_module::tests$test_module_num::test$test_method_num --show-output
  }
}

if ($args[0] -is [int]) {
  CargoTest unzipper $args[0] $args[1]
  <#  
  Test script @#script #> 
}