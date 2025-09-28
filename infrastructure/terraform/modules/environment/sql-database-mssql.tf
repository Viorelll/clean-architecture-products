locals {
  ci_cd_username = "ci_cd_user"
}

resource "azurerm_mssql_server" "server" {
  name                         = "sql-${var.application_name}-${var.environment_name}-${var.region_identifier}-01"
  resource_group_name          = azurerm_resource_group.rg_env.name
  location                     = var.region_full_identifier
  administrator_login          = var.sqldb_admin_username
  administrator_login_password = random_password.sqldb_admin_password.result
  version                      = "12.0"

  # azuread_administrator {
  #   login_username = "AzureAD Admin"
  #   object_id      = data.azuread_service_principal.sql_server_ad_admin.id
  # }
}

resource "azurerm_mssql_firewall_rule" "allow_azure_services" {
  name             = "AllowAzureTrustedServices"
  server_id        = azurerm_mssql_server.server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_database" "db" {
  name                        = "sqldb-${var.application_name}-${var.environment_name}-${var.region_identifier}"
  server_id                   = azurerm_mssql_server.server.id
  sku_name                    = var.sqldb_sku
  zone_redundant              = var.sqldb_zone_redundant
  auto_pause_delay_in_minutes = var.sqldb_auto_pause_delay_in_minutes
  min_capacity                = var.sqldb_min_capacity

  # long_term_retention_policy {
  #   weekly_retention  = var.sqldb_backup_ltr_weekly_retention
  #   monthly_retention = var.sqldb_backup_ltr_monthly_retention
  #   yearly_retention  = var.sqldb_backup_ltr_yearly_retention
  # }

  # short_term_retention_policy {
  #   backup_interval_in_hours = var.sqldb_backup_str_interval_in_hours
  #   retention_days           = var.sqldb_backup_str_retention_days
  # }

  # prevent the possibility of accidental data loss
  lifecycle {
    prevent_destroy = true
  }
}

# User for ci/cd system
# resource "mssql_user" "ci_cd_user" {
#   server {
#     host = azurerm_mssql_server.server.fully_qualified_domain_name
#     login {
#       username = var.sqldb_admin_username
#       password = random_password.sqldb_admin_password.result
#     }
#   }
#   database   = azurerm_mssql_database.db.name
#   login_name = local.ci_cd_username
#   username   = local.ci_cd_username
#   roles      = ["db_owner"]
# }

# Login for ci/cd system
# resource "mssql_login" "ci_cd_login" {
#   server {
#     host = azurerm_mssql_server.server.fully_qualified_domain_name
#     login {
#       username = var.sqldb_admin_username
#       password = random_password.sqldb_admin_password.result
#     }
#   }
#   login_name = local.ci_cd_username
#   password   = random_password.sqldb_ci_cd_user_password.result
# }
