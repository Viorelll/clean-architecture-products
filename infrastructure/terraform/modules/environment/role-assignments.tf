### ASSIGN KEY VAULT ADMINISTRATOR & APP CONFIGURATION DATA OWNER FOR TERRAFORM APPLY USER (LOCAL SIGN IN USER/TERRAFORM APP REGISTRATION)
### ASSIGN THIS ROLES IS POSSIBLE IF TERRAFORM APP REGISTRATION IS OWNER IN KEY_VALUT/APP_CONFIG
# Terraform user as keyvault admin
resource "azurerm_role_assignment" "terraform_keyvault_admin" {
  scope                = azurerm_key_vault.this.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = data.azurerm_client_config.current.object_id
}

# Terraform user as App Configuration Data Owner so it can create app config keys 
resource "azurerm_role_assignment" "terraform_app_config_admin" {
  scope                = azurerm_app_configuration.this.id
  role_definition_name = "App Configuration Data Owner"
  principal_id         = data.azurerm_client_config.current.object_id
}

## Set roles for Managed Identity (UAI)
#Access for container apps to read keyvault
resource "azurerm_role_assignment" "container_apps_keyvault" {
  scope                = azurerm_key_vault.this.id
  role_definition_name = "Key Vault Secrets User"
  principal_id         = azurerm_user_assigned_identity.container_apps.principal_id
}

# Access for container apps to read app configuration
resource "azurerm_role_assignment" "container_apps_appconfig" {
  scope                = azurerm_app_configuration.this.id
  role_definition_name = "App Configuration Data Reader"
  principal_id         = azurerm_user_assigned_identity.container_apps.principal_id
}

# Access for container apps identity to pull images from container registry
# ASSIGN THIS ROLES IS POSSIBLE IF TERRAFORM APP REGISTRATION IS OWNER IN SHARED_CONTAINER_REGISTRY
resource "azurerm_role_assignment" "container_apps_container_registry" {
  scope                = data.azurerm_container_registry.shared.id
  role_definition_name = "acrpull"
  principal_id         = azurerm_user_assigned_identity.container_apps.principal_id
}
