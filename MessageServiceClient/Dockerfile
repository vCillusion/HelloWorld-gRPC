FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MessageServiceClient/MessageServiceClient.csproj", "MessageServiceClient/"]
RUN dotnet restore "MessageServiceClient/MessageServiceClient.csproj"
COPY . .
WORKDIR "/src/MessageServiceClient"
RUN dotnet build "MessageServiceClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageServiceClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageServiceClient.dll"]