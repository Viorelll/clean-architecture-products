resource "azurerm_user_assigned_identity" "uami" {
  name                = "uami-${var.application_name}-${var.environment_name}-${var.region_identifier}"
  resource_group_name = azurerm_resource_group.rg_env.name
  location            = azurerm_resource_group.rg_env.location
}
