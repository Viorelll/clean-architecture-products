terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "4.46.0"
    }
    mssql = {
      source  = "betr-io/mssql"
      version = "0.3.1"
    }
  }
  backend "azurerm" {
    resource_group_name  = "rg-products-shared-plc"
    storage_account_name = "stproductssharedplc"
    container_name       = "tfstateproductsdev"
    key                  = "terraform.tfstate"
    subscription_id      = "8e6b06a1-86b9-42bd-8971-45b0d844b544"
  }
}

provider "azurerm" {
  subscription_id = "8e6b06a1-86b9-42bd-8971-45b0d844b544"
  features {}
}

provider "azurerm" {
  alias           = "shared_connectivity"
  subscription_id = "8e6b06a1-86b9-42bd-8971-45b0d844b544"
  features {}
}

module "environment" {
  source                 = "../modules/environment"
  resource_group_name    = "rg-products-dev-plc"
  environment_name       = "dev"
  application_name       = "products"
  region_identifier      = "gwc"
  region_full_identifier = "Germany West Central"
  aspnetcore_environment = "Development"

  shared_resource_group_name = "rg-products-shared-plc"

  providers = {
    azurerm.shared_connectivity = azurerm.shared_connectivity
  }

  sqldb_auto_pause_delay_in_minutes = 120
  sqldb_sku                         = "S0"
  sqldb_min_capacity                = 0
  sqldb_admin_username              = "product_admin"
  sqldb_zone_redundant              = false
}
