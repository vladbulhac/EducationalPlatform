FROM mcr.microsoft.com/mssql/server:2019-latest
EXPOSE 1433

WORKDIR /src
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Publisher/publisher.sql" /publisher/
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Publisher/publisher-execute-scripts.sh" /publisher/
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Publisher/publisher-docker-entrypoint.sh" /publisher/

USER root
# Copy wait-for-it.sh into our image
COPY ./wait-for.sh ./wait-for.sh
# Make scripts executable in Linux
RUN chmod +x ./wait-for.sh && chmod +x /publisher/publisher-docker-entrypoint.sh && chmod +x /publisher/publisher-execute-scripts.sh
CMD /bin/bash /publisher/publisher-docker-entrypoint.sh