-- https://medium.com/@gareth.newman/sql-server-replication-on-docker-a-glimpse-into-the-future-46086c7b3f2
-- https://docs.microsoft.com/en-us/sql/relational-databases/replication/sql-server-replication?view=sql-server-ver15

-- The Publisher is a database instance that makes data available to other locations through replication. 
-- The Publisher can have one or more publications, each defining a logically related set of objects and data to replicate.
-- Each Publisher is associated with a single database (known as a distribution database) at the Distributor.

-- Configure Publisher:

-- tell the Publisher who the remote Distributor is
EXEC sp_adddistributor @distributor = 'edu-distributor,1433',
                       @password = 'Pass@word';

-- enable database for replication
USE [ProjectOne.EducationalInstitutions.Writes];
EXEC sp_replicationdboption @dbname = N'ProjectOne.EducationalInstitutions.Writes', -- database for which the replication database option is being set
                            @optname = N'publish', -- database can be used for other types of publications
                            @value = N'true'; -- enable or disable the given replication database option

-- A publication is a collection of one or more articles from one database.
-- The grouping of multiple articles into a publication makes it easier to specify a logically related set of database objects and data that are replicated as a unit.

-- Configure Publication:
EXEC sp_addpublication @publication = N'ProjectOne.EducationalInstitutions', -- name of the publication to create, unique within the database
                       @description = N'Educational Institutions data replication',
                       @retention = 0, -- if a subscription is not active within the retention period, it expires and is removed, on "0" subscriptions to the publication will never expire and be removed
                       @allow_push = N'true', -- specifies if push subscriptions can be created for the given publication
                       @repl_freq = N'continuous', -- type of replication frequency, on "continuous" (default) Publisher provides output of all log-based transactions
                       @status = N'active', -- specifies if publication data is available, on "active" publication data is available for Subscribers immediately
                       @independent_agent = N'true'; -- specifies if there is a stand-alone Distribution Agent for this publication

-- An article identifies a database object that is included in a publication.
-- A publication can contain different types of articles, including tables, views, stored procedures, and other objects.
-- When tables are published as articles, filters can be used to restrict the columns and rows of the data sent to Subscribers.

-- Configure Articles:
USE [ProjectOne.EducationalInstitutions.Writes];
-- creates an article and adds it to a publication
EXEC sp_addarticle @publication = N'ProjectOne.EducationalInstitutions', -- name of the publication that contains the article, unique within the database
                   @article = N'EducationalInstitutions', -- name of the article, unique within the publication
                   @source_owner = N'dbo', -- owner of the source object
                   @source_object = N'EducationalInstitutions', -- database object to be published
                   @type = N'logbased', -- type of article, "logbased" (default)
                   @description = NULL, -- descriptive entry for the article
                   @creation_script = NULL, -- path and name of an optional article schema script used to create the article in the subscription database
                   @pre_creation_cmd = N'drop', -- specifies what the system should do if it detects an existing object of the same name at the subscriber when applying the snapshot for this article, on "drop" (default) it drops the destination table
                   @schema_option = 0x000000000803509D,-- bitmask of the schema generation option for the given article
                   @identityrangemanagementoption = N'manual', -- specifies how identity range management is handled for the article, on "manual" marks the identity column using NOT FOR REPLICATION to enable manual identity range handling
                   @destination_table = N'EducationalInstitutions', -- name of the subscrition table
                   @destination_owner = N'dbo', -- name of the owner of the destination object
                   @vertical_partition = N'false'; -- enables or disables column filtering on a table article, on "false" there is no vertical filtering and publishes all columns

EXEC sp_addarticle @publication = N'ProjectOne.EducationalInstitutions',
                   @article = N'Admins',
                   @source_owner = N'dbo',
                   @source_object = N'Admins',
                   @type = N'logbased',
                   @description = NULL,
                   @creation_script = NULL,
                   @pre_creation_cmd = N'drop',
                   @schema_option = 0x000000000803509D,
                   @identityrangemanagementoption = N'manual',
                   @destination_table = N'Admins',
                   @destination_owner = N'dbo',
                   @vertical_partition = N'false';

EXEC sp_addarticle @publication = N'ProjectOne.EducationalInstitutions',
                   @article = N'Buildings',
                   @source_owner = N'dbo',
                   @source_object = N'Buildings',
                   @type = N'logbased',
                   @description = NULL,
                   @creation_script = NULL,
                   @pre_creation_cmd = N'drop',
                   @schema_option = 0x000000000803509D,
                   @identityrangemanagementoption = N'manual',
                   @destination_table = N'Buildings',
                   @destination_owner = N'dbo',
                   @vertical_partition = N'false';

-- A subscription is a request for a copy of a publication to be delivered to a Subscriber.
-- The subscription defines what publication will be received, where, and when.
-- There are two types of subscriptions: push and pull.

--Configure Subscription:
USE [ProjectOne.EducationalInstitutions.Writes];
-- adds a subscription to a publication and sets the Subscriber status
EXEC sp_addsubscription @publication = N'ProjectOne.EducationalInstitutions', -- name of the publication 
                        @subscriber = 'edu-subscriber,1433', -- name of the Subscriber
                        @destination_db = 'ProjectOne.EducationalInstitutions.Reads', -- name of the destination database in which to place replicated data
                        @subscription_type = N'Push', -- type of subscription
                        @sync_type = N'replication support only', -- subscription synchronization type, on "replication support only" provides automatic generation at the Subscriber of article custom stored procedures and triggers that support updating subscriptions, if appropriate and assumes that the Subscriber already has the schema and initial data for published tables
                        @article = N'all', -- article to which the publication is subscribed, on "all" a subscription is added to all articles in that publication
                        @update_mode = N'read only', -- type of update, on "read only" (default) the subscription is read only and changes at the Subscriber are not sent to the Publisher
                        @subscriber_type = 0; -- on "0" (default) the Subscriber is a SQL Server 


--Configure Push Agent:
-- adds a new scheduled agent job used to synchronize a push subscription to a transactional publication
EXEC sp_addpushsubscription_agent @publication = N'ProjectOne.EducationalInstitutions', -- name of the publication
                                  @subscriber = 'edu-subscriber,1433', -- name of the Subscriber instance
                                  @subscriber_db = 'ProjectOne.EducationalInstitutions.Reads', -- name of the subscription database
                                  @subscriber_security_mode = 0, -- security mode to use when connecting to a Subscriber when synchronizing, on "0" specifies SQL Server Authentication, on "1" specifies Windows Authentication
                                  @subscriber_login =  'sa', -- Subscriber login to use when connecting to a Subscriber when synchronizing
                                  @subscriber_password =  'Pass@word', -- is required if subscriber_security_mode is "0"
                                  @frequency_type = 64, -- the frequency with which to schedule the Distribution Agent, on "64" (default) causes the Distribution Agent to run in continuous mode
                                  @frequency_interval = 0, -- value to apply to the frequency set by @frequency_type
                                  @frequency_relative_interval = 0, -- date of the Distribution Agent, is used when @frequency_type is set to "32"
                                  @frequency_recurrence_factor = 0, -- recurrence factor used by @frequency_type
                                  @frequency_subday = 0, -- how often to reschedule during the defined period
                                  @frequency_subday_interval = 0,
                                  @active_start_time_of_day = 0, -- time of the day when the Distribution Agent is first scheduled, formatted as HHMMSS, on "0" is default
                                  @active_end_time_of_day = 0, -- time of the day when the Distribution Agent stops being scheduled, formatted as HHMMSS
                                  @active_start_date = 0, -- date when the Distribution Agent is first scheduled, formatted as YYYYMMDD, on "0" is default
                                  @active_end_date = 19950101; -- the date when the Distribution Agent stops being scheduled, formatted as YYYYMMDD
GO

--Changes security properties of a Log Reader agent:
-- by default it sets up the log reader agent with a default account that won't work, you need to change that to something that will
EXEC sp_changelogreader_agent @publisher_security_mode = 0, -- the security mode used by the agent when connecting to the Publisher, on "0" specifies SQL Server Authentication, on "1" specifies Windows Authentication
                              @publisher_login = 'sa', -- the login used when connecting to the Publisher
                              @publisher_password = 'Pass@word'; -- the password used when connecting to the Publisher