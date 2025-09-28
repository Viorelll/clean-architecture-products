resource "azurerm_resource_group" "rg_env" {
  location = var.region_full_identifier
  name     = "rg-${var.application_name}-${var.environment_name}-${var.region_identifier}"
}
