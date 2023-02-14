FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

ARG SA_PASSWORD
ENV SA_PASSWORD $SA_PASSWORD

ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

WORKDIR /app
COPY . .

# Wait to be sure that SQL Server came up
RUN echo $SA_PASSWORD

# # Run the setup script to create the DB and the schema in the DB
# # Note: make sure that your password matches what is in the Dockerfile

RUN chmod +x /app/init-data.sh
RUN chmod +x /app/entrypoint.sh

CMD /bin/bash /app/entrypoint.sh

