#!/bin/bash 
AWS_ACCESS_KEY_ID='---'
AWS_SECRET_ACCESS_KEY='--'
AWS_REGION='ap-southeast-1' 
FUNCTION_NAME='VerifyPhoneNumber'
CI_PIPELINE_ID='VerifyPhoneNumber' 
dotnet lambda package -o publish/$CI_PIPELINE_ID.zip 
cd publish  

aws configure set aws_access_key_id $AWS_ACCESS_KEY_ID
aws configure set aws_secret_access_key $AWS_SECRET_ACCESS_KEY 
aws configure set region $AWS_REGION
aws configure set output json
aws lambda update-function-code  --function-name  $FUNCTION_NAME  --zip-file  fileb://$CI_PIPELINE_ID.zip 
sleep 200