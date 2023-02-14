# Wait to be sure that SQL Server came up
sleep 10s
echo $SA_PASSWORD

# # Run the setup script to create the DB and the schema in the DB
# # Note: make sure that your password matches what is in the Dockerfile

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i /app/db.erp.sql
