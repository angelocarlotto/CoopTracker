# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /source

# copy csproj and restore as distinct layers
COPY CoopTracker/*.csproj .
RUN dotnet restore -a $TARGETARCH

# copy and publish app and libraries
COPY CoopTracker/. .
RUN dotnet publish -a $TARGETARCH --no-restore -o /app


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV DefaultConnection="Server=6.tcp.ngrok.io,19781;Database=CoopTrackerDb;User Id=sa;Password=123456@toronto;TrustServerCertificate=True"
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
WORKDIR /app
COPY --from=build /app .
USER $APP_UID
ENTRYPOINT ["./CoopTracker"]
