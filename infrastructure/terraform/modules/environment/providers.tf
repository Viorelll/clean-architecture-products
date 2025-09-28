
terraform {
  required_providers {
    azurerm = {
      source                = "hashicorp/azurerm"
      version               = "4.46.0"
      configuration_aliases = [azurerm.shared_connectivity]
    }

    mssql = {
      source  = "betr-io/mssql"
      version = "0.3.1"
    }
  }
}
