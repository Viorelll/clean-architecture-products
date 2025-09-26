variable "application_name" {
  type    = string
  default = "products"
}

variable "region_identifier" {
  type    = string
  default = "plc"
}

variable "shared_identifier" {
  type    = string
  default = "shared"
}

variable "dev_identifier" {
  type    = string
  default = "dev"
}

locals {
  shared_resource_group_name  = "rg-${var.application_name}-${var.shared_identifier}-${var.region_identifier}-01"
  shared_storage_account_name = "st${var.application_name}${var.shared_identifier}${var.region_identifier}02"
}
