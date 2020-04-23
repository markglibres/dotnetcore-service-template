FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .
RUN dotnet restore "BizzPo.Presentation.Api/BizzPo.Presentation.Api.csproj"
RUN dotnet build "BizzPo.Presentation.Api/BizzPo.Presentation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BizzPo.Presentation.Api/BizzPo.Presentation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BizzPo.Presentation.Api.dll"]