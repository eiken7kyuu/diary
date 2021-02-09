FROM node:12-alpine AS client
WORKDIR "/front"
COPY ProtectedDiary.Web/Client .
RUN npm install
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS publish
WORKDIR "/src/ProtectedDiary"
COPY . .
COPY --from=client /wwwroot ./ProtectedDiary.Web/wwwroot
RUN dotnet publish "ProtectedDiary.Web/ProtectedDiary.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ProtectedDiary.Web.dll