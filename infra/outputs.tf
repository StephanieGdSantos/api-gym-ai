output "api_url" {
  description = "Public URL of the HTTP API."
  value       = aws_apigatewayv2_api.http.api_endpoint
}

output "lambda_name" {
  description = "Deployed Lambda function name."
  value       = aws_lambda_function.api.function_name
}
