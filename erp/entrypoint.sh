#!/bin/sh

ROOT_DIR=/app

# Replace env vars in JavaScript files
echo "Replacing env constants in JS"
for file in $ROOT_DIR/js/app.*.js* $ROOT_DIR/index.html $ROOT_DIR/precache-manifest*.js;
do
  echo "Processing $file ...";

  sed -i 's|VUE_APP_API_URL|'${VUE_APP_API_URL}'|g' $file 
  sed -i 's|VUE_APP_SHOP_URL|'${VUE_APP_SHOP_URL}'|g' $file 

done

echo "Starting app..."
nginx
