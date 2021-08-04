-- https://medium.com/@gareth.newman/sql-server-replication-on-docker-a-glimpse-into-the-future-46086c7b3f2
-- https://docs.microsoft.com/en-us/sql/relational-databases/replication/sql-server-replication?view=sql-server-ver15

-- The Distributor is a database instance that acts as a store for replication specific data associated with one or more Publishers.
-- The distribution database stores replication status data, metadata about the publication, and, in some cases, acts as a queue for data moving from the Publisher to the Subscribers. 

-- Configure remote Distributor:
-- mark this server as a Distributor
EXEC sp_adddistributor @distributor = 'edu-distributor', -- name of the Distributor instance
                       @password = 'Pass@word'; -- password of the distributor_admin

-- create the distribution database which stores procedures, schema, and metadata used in replication
EXEC sp_adddistributiondb @database = 'distribution';

-- tell the Distributor who the Publisher is 
EXEC sp_adddistpublisher @publisher = 'edu-publisher', -- name of the Publisher instance
                         @distribution_db = 'distribution';