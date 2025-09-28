resource "azurerm_static_web_app" "webapp" {
  name                = "stapp-${var.application_name}-${var.environment_name}-${var.region_identifier}"
  resource_group_name = azurerm_resource_group.rg_env.name
  location            = "westeurope" //data.azurerm_resource_group.this.location
}
