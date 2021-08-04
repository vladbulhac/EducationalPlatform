#!/bin/bash

#sleep for a period of time so that the educationalinstitution-api applied Entity Framework Core migrations,
#creating all the necessary tables needed by the scripts
echo "> --- Sleeping for 30 seconds ---"
sleep 30

echo "> --- Running publisher scripts ---"

/opt/mssql-tools/bin/sqlcmd -U sa -P Pass@word -l 30 -e -i /publisher/publisher.sql

echo "> --- Finished publisher scripts ---"