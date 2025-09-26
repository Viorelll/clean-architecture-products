data "azurerm_resource_group" "shared_resource" {
  name = local.shared_resource_group_name
}

data "azurerm_storage_account" "shared_storage" {
  name                = local.shared_storage_account_name
  resource_group_name = local.shared_resource_group_name
}
