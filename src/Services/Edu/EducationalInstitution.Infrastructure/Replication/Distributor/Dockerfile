FROM mcr.microsoft.com/mssql/server:2019-latest
EXPOSE 1433

RUN mkdir /var/opt/mssql/ReplData

WORKDIR /src
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Distributor/distributor.sql" /distributor/
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Distributor/distributor-execute-scripts.sh" /distributor/
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Distributor/distributor-docker-entrypoint.sh" /distributor/

USER root
# Copy wait-for-it.sh into our image
COPY ./wait-for.sh ./wait-for.sh
# Make scripts executable in Linux
RUN chmod +x ./wait-for.sh && chmod +x /distributor/distributor-docker-entrypoint.sh && chmod +x /distributor/distributor-execute-scripts.sh
CMD /bin/bash /distributor/distributor-docker-entrypoint.sh