#!/bin/bash

# https://github.com/vishnubob/wait-for-it
# run SQL Server and wait-for.sh scripts
# -> wait for Distributor database to accept connections (to be ready)
# -> wait for Publisher database to be ready
# -> after publisher database is ready run /distributor/distributor-execute-scripts.sh
./wait-for.sh 127.0.0.1:1433 -t 0 -- ./wait-for.sh edu-publisher:1433 -t 0 -- /distributor/distributor-execute-scripts.sh & /opt/mssql/bin/sqlservr