resource "azurerm_storage_container" "tfstate_dev" {
  name                  = "tfstate${var.application_name}${var.dev_identifier}"
  storage_account_name  = data.azurerm_storage_account.shared_storage.name
  container_access_type = "private"
}
