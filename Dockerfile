#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list
RUN apt-get update; apt-get install -y ttf-mscorefonts-installer fontconfig
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UC.WebApi/UC.WebApi.csproj", "UC.WebApi/"]
COPY ["UC.DataLayer/UC.DataLayer.csproj", "UC.DataLayer/"]
COPY ["UC.ClassDomain/UC.ClassDomain.csproj", "UC.ClassDomain/"]
COPY ["UC.FluentAPI/UC.FluentAPI.csproj", "UC.FluentAPI/"]
COPY ["UC.InterfaceService/UC.InterfaceService.csproj", "UC.InterfaceService/"]
COPY ["UC.ClassDTO/UC.ClassDTO.csproj", "UC.ClassDTO/"]
COPY ["UC.Service/UC.Service.csproj", "UC.Service/"]
COPY ["UC.Common/UC.Common.csproj", "UC.Common/"]
RUN dotnet restore "UC.WebApi/UC.WebApi.csproj"
COPY . .
WORKDIR "/src/UC.WebApi"
RUN dotnet build "UC.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UC.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UC.WebApi.dll"]
