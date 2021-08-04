FROM mcr.microsoft.com/mssql/server:2019-latest
EXPOSE 1433

WORKDIR /src
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Subscriber/subscriber.sql" /subscriber/
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Subscriber/subscriber-execute-scripts.sh" /subscriber/
COPY "Services/Edu/EducationalInstitution.Infrastructure/Replication/Subscriber/subscriber-docker-entrypoint.sh" /subscriber/

USER root
# Copy wait-for-it.sh into our image
COPY ./wait-for.sh ./wait-for.sh
# Make scripts executable in Linux
RUN chmod +x ./wait-for.sh && chmod +x /subscriber/subscriber-docker-entrypoint.sh && chmod +x /subscriber/subscriber-execute-scripts.sh
CMD /bin/bash /subscriber/subscriber-docker-entrypoint.sh