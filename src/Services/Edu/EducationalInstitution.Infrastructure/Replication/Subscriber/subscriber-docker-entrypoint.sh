#!/bin/bash

# https://github.com/vishnubob/wait-for-it
# run SQL Server and wait-for scripts
# -> wait for Subscriber database to be ready
# -> wait for Publisher database to accept connections
# -> wait for educationalinstitution-api to be ready
# -> after educationalinstitution-api is ready run /subscriber/subscriber-execute-scripts.sh
./wait-for.sh 127.0.0.1:1433 -t 0 -- ./wait-for.sh edu-publisher:1433 -t 0 -- ./wait-for.sh educationalinstitution-api:50001 -t 0 -- /subscriber/subscriber-execute-scripts.sh & /opt/mssql/bin/sqlservr