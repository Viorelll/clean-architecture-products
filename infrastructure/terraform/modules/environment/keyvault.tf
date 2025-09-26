resource "azurerm_key_vault" "this" {
  name                      = "kv${var.application_name}${var.environment_name}${var.region_identifier}"
  location                  = data.azurerm_resource_group.this.location
  resource_group_name       = data.azurerm_resource_group.this.name
  tenant_id                 = data.azurerm_client_config.current.tenant_id
  sku_name                  = "standard"
  enable_rbac_authorization = true
}

resource "random_password" "sqldb_admin_password" {
  length           = 16
  special          = true
  override_special = "!#*()-_+?"
}

resource "azurerm_key_vault_secret" "sqldb_admin_password" {
  name         = "sqldb-admin-password"
  key_vault_id = azurerm_key_vault.this.id
  value        = random_password.sqldb_admin_password.result

  depends_on = [azurerm_role_assignment.terraform_keyvault_admin] #wait until the role for creating new secret will be assigned
}

resource "azurerm_key_vault_secret" "sqldb_connection_string_viqupapp" {
  name         = "sqldb-connectionstring"
  key_vault_id = azurerm_key_vault.this.id
  value        = "Server=${azurerm_postgresql_flexible_server.server.fqdn};Database=${azurerm_postgresql_flexible_server_database.db.name};Port=5432;User Id=${var.sqldb_admin_username};Password=${azurerm_postgresql_flexible_server.server.administrator_password};Ssl Mode=Require;"

  depends_on = [azurerm_role_assignment.terraform_keyvault_admin] #wait until the role for creating new secret will be assigned
}
