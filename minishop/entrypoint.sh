#!/bin/sh

ROOT_DIR=/app

# Replace env vars in JavaScript files
echo "Replacing env constants in JS"
for file in $ROOT_DIR/_nuxt/*.js* $ROOT_DIR/index.html
do
  echo "Processing $file ...";

  sed -i 's|VUE_APP_API_URL|'${VUE_APP_API_URL}'|g' $file 
  sed -i 's|VUE_APP_ERP_URL|'${VUE_APP_ERP_URL}'|g' $file 

done

echo "Starting app..."
nginx
