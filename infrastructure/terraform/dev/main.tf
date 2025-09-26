terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.103.1"
    }
  }
  backend "azurerm" {
    resource_group_name  = "rg-products-shared-plc-01"
    storage_account_name = "stproductssharedplc02"
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
  resource_group_name    = "rg-products-dev-plc-01"
  environment_name       = "dev"
  application_name       = "products"
  region_identifier      = "plc"
  region_full_identifier = "polandcentral"
  aspnetcore_environment = "Development"

  shared_resource_group_name     = "rg-products-shared-plc-01"

  providers = {
    azurerm.shared_connectivity = azurerm.shared_connectivity
  }

  sqldb_auto_pause_delay_in_minutes = 120
  sqldb_sku                         = "B_Standard_B1ms"
  sqldb_min_capacity                = 0.5
  sqldb_admin_username              = "product_admin"
  sqldb_zone_redundant              = false
}
