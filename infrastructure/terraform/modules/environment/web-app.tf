resource "azurerm_service_plan" "plan" {
  name                = "plan-${var.application_name}-${var.environment_name}-${var.region_identifier}"
  resource_group_name = azurerm_resource_group.rg_env.name
  location            = azurerm_resource_group.rg_env.location
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "webapp" {
  name                = "webapp-${var.application_name}-${var.environment_name}-${var.region_identifier}"
  resource_group_name = azurerm_resource_group.rg_env.name
  location            = azurerm_resource_group.rg_env.location
  service_plan_id     = azurerm_service_plan.plan.id

  identity {
    type         = "UserAssigned" # add SystemAssigned if not works (web app + user assigned => bug in azure on reading key vault reference?)
    identity_ids = [azurerm_user_assigned_identity.uami.id]
  }

  #key_vault_reference_identity_id = azurerm_user_assigned_identity.uami.id

  site_config {
    always_on = "false"

    application_stack {
      dotnet_version = "9.0"
    }
  }


  app_settings = {
    ConnectionStrings__CleanArchitectureApiDb = "@Microsoft.KeyVault(VaultName=${azurerm_key_vault.this.name};SecretName=${azurerm_key_vault_secret.sqldb_connection_string_products.name})"
  }
}


resource "azurerm_linux_web_app" "testwebapp" {
  name                = "testviobrio2"
  resource_group_name = azurerm_resource_group.rg_env.name
  location            = azurerm_resource_group.rg_env.location
  service_plan_id     = azurerm_service_plan.plan.id

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.uami.id]
  }

  key_vault_reference_identity_id = azurerm_user_assigned_identity.uami.id

  site_config {
    always_on = "false"

    application_stack {
      dotnet_version = "9.0"
    }
  }

  app_settings = {
    ConnectionStrings__CleanArchitectureApiDb = "@Microsoft.KeyVault(VaultName=${azurerm_key_vault.this.name};SecretName=${azurerm_key_vault_secret.sqldb_connection_string_products.name})"
  }
}
