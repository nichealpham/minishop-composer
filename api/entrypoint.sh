#!/bin/sh

ROOT_DIR=/app

# Replace env vars in JavaScript files
echo "Replacing env variables in appsetting"
for file in $ROOT_DIR/appsettings.json;
do
  echo "Processing $file ...";

  sed -i 's|VUE_APP_ERP_URL|'${VUE_APP_ERP_URL}'|g' $file
  sed -i 's|VUE_APP_MSSQL_DB_HOST|'${VUE_APP_MSSQL_DB_HOST}'|g' $file
  sed -i 's|VUE_APP_MSSQL_DB_USER|'${VUE_APP_MSSQL_DB_USER}'|g' $file
  sed -i 's|VUE_APP_MSSQL_DB_PASSWORD|'${VUE_APP_MSSQL_DB_PASSWORD}'|g' $file
  sed -i 's|VUE_APP_ACCESS_TOKEN_ENCRYPTION_KEY|'${VUE_APP_ACCESS_TOKEN_ENCRYPTION_KEY}'|g' $file 
done

echo "Starting dotnet..."
dotnet Api.dll