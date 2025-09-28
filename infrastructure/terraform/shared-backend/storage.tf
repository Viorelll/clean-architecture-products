resource "azurerm_storage_account" "sharedstorage" {
  name                     = "st${var.application_name}${var.shared_identifier}${var.region_identifier}"
  resource_group_name      = azurerm_resource_group.rg_shared.name
  location                 = azurerm_resource_group.rg_shared.location
  account_tier             = "Standard"
  account_replication_type = "LRS" //GRS

  blob_properties {
    last_access_time_enabled = true
    delete_retention_policy {
      days = 90
    }
    versioning_enabled = true
  }
}

resource "azurerm_storage_management_policy" "delete_old_versions" {
  storage_account_id = azurerm_storage_account.sharedstorage.id

  rule {
    name    = "delete_old_versions"
    enabled = true
    filters {
      blob_types = ["blockBlob"]
    }
    actions {
      version {
        delete_after_days_since_creation = 90
      }
    }
  }
}

resource "azurerm_storage_container" "tfstateshared" {
  name                  = "tfstate${var.application_name}${var.shared_identifier}"
  storage_account_id    = azurerm_storage_account.sharedstorage.id
  container_access_type = "private"
}

# container for dev tf state files
resource "azurerm_storage_container" "tfstatedev" {
  name                  = "tfstate${var.application_name}${var.dev_identifier}"
  storage_account_id    = azurerm_storage_account.sharedstorage.id
  container_access_type = "private"
}
