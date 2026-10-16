variable "aws_region" {
  type        = string
  description = "AWS region where the API is deployed."
  default     = "us-east-1"
}

variable "project_name" {
  type        = string
  description = "Base name used for AWS resources."
  default     = "gym-ai-api"
}

variable "environment" {
  type        = string
  description = "Deployment environment name."
  default     = "prod"
}

variable "cohere_api_key" {
  type        = string
  description = "Cohere API key injected into Lambda environment variables."
  sensitive   = true
}

variable "cohere_base_url" {
  type        = string
  description = "Cohere chat endpoint."
  default     = "https://api.cohere.com/v2/chat"
}

variable "lambda_timeout" {
  type        = number
  description = "Lambda timeout in seconds."
  default     = 60
}

variable "lambda_memory" {
  type        = number
  description = "Lambda memory in MB."
  default     = 1024
}
