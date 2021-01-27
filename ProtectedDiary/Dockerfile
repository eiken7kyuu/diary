FROM node:12-alpine AS client
WORKDIR "/front"
COPY ./Client .
RUN npm install
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS publish
WORKDIR "/src/ProtectedDiary"
COPY . .
COPY --from=client /wwwroot ./wwwroot
RUN dotnet publish "ProtectedDiary.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ProtectedDiary.dll