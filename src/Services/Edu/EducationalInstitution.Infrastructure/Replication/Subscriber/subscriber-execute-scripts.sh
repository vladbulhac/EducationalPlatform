#!/bin/bash

#sleep for a period of time so that the educationalinstitution-api applied the Entity Framework Core migration
#creating all the necessary tables needed by the scripts
echo "> --- Sleeping for 30 seconds ---"
sleep 30

echo "> --- Running subscriber scripts ---"

/opt/mssql-tools/bin/sqlcmd -U sa -P Pass@word -l 30 -e -i /subscriber/subscriber.sql

echo "> --- Finished subscriber scripts ---"