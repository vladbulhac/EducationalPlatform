#!/usr/bin/env bash

#sleep for a period of time so that educationalinstitution-api finishes applying Entity Framework Core migrations to Publisher
echo "> --- Sleeping for 30 seconds ---"
sleep 30

echo "> --- Running distributor scripts ---"

/opt/mssql-tools/bin/sqlcmd -U sa -P Pass@word -l 30 -e -i /distributor/distributor.sql

echo "> --- Finished distributor scripts ---"