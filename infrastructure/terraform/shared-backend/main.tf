terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "4.46.0"
    }
  }
}

provider "azurerm" {
  subscription_id = "8e6b06a1-86b9-42bd-8971-45b0d844b544"
  features {}
}
