resource "azurerm_resource_group" "rg_shared" {
  location = var.region_name
  name     = local.shared_resource_group_name
}
