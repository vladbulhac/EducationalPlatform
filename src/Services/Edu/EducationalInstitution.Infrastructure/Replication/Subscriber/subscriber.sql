-- https://medium.com/@gareth.newman/sql-server-replication-on-docker-a-glimpse-into-the-future-46086c7b3f2
-- https://docs.microsoft.com/en-us/sql/relational-databases/replication/sql-server-replication?view=sql-server-ver15

-- A Subscriber is a database instance that receives replicated data.
-- A Subscriber can receive data from multiple Publishers and publications.
-- Depending on the type of replication chosen, the Subscriber can also pass data changes back to the Publisher or republish the data to other Subscribers.