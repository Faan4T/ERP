FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["src/ERP.Web.Host/ERP.Web.Host.csproj", "src/ERP.Web.Host/"]
COPY ["src/ERP.Web.Core/ERP.Web.Core.csproj", "src/ERP.Web.Core/"]
COPY ["src/ERP.Application/ERP.Application.csproj", "src/ERP.Application/"]
COPY ["src/ERP.Core/ERP.Core.csproj", "src/ERP.Core/"]
COPY ["src/ERP.EntityFrameworkCore/ERP.EntityFrameworkCore.csproj", "src/ERP.EntityFrameworkCore/"]
WORKDIR "/src/src/ERP.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ERP.Web.Host", "src/ERP.Web.Host"]
COPY ["src/ERP.Web.Core", "src/ERP.Web.Core"]
COPY ["src/ERP.Application", "src/ERP.Application"]
COPY ["src/ERP.Core", "src/ERP.Core"]
COPY ["src/ERP.EntityFrameworkCore", "src/ERP.EntityFrameworkCore"]
WORKDIR "/src/src/ERP.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ERP.Web.Host.dll"]
