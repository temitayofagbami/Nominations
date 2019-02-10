FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY IndividualNominationsAPI/IndividualNominationsAPI.csproj IndividualNominationsAPI/
RUN dotnet restore IndividualNominationsAPI/IndividualNominationsAPI.csproj
COPY . .
WORKDIR /src/IndividualNominationsAPI
RUN dotnet build IndividualNominationsAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish IndividualNominationsAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IndividualNominationsAPI.dll"]
