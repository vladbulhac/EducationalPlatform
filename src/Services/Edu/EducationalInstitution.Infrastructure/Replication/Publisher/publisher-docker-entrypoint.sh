#!/bin/bash

# https://github.com/vishnubob/wait-for-it
# run SQL Server and wait-for scripts
# -> wait for Publisher database to be ready
# -> wait for Distributor database to accept connections
# -> wait for educationalinstitution-api to be ready
# -> wait for Subscriber database to accept connections
# -> run /publisher/publisher-execute-scripts.sh after Subscriber database is ready
./wait-for.sh 127.0.0.1:1433 -t 0 -- ./wait-for.sh edu-distributor:1433 -t 0 -- ./wait-for.sh educationalinstitution-api:50001 -t 0 -- ./wait-for.sh edu-subscriber:1433 -t 0 -- /publisher/publisher-execute-scripts.sh & /opt/mssql/bin/sqlservr