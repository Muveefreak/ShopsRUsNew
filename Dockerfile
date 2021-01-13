FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY ./NuGet.Config ./ShopsRUs.sln ./
COPY ./ShopsRUs.Api/*.csproj ./ShopsRUs.Api/
COPY ./ShopsRUs.Infrastructure/*.csproj ./ShopsRUs.Infrastructure/
COPY ./ShopsRUs.Core/*.csproj ./ShopsRUs.Core/
COPY ./ShopsRUs.UnitTests/*.csproj ./ShopsRUs.UnitTests/
COPY ./ShopsRUs.Infrastructure/CustomerSeedData.json ./
COPY ./ShopsRUs.Infrastructure/DiscountSeedData.json ./
COPY ./ShopsRUs.Infrastructure/OrderSeedData.json ./
RUN dotnet restore --disable-parallel

COPY . ./
RUN dotnet build -c Release
# ENV ASPNETCORE_ENVIRONMENT IntegrationTesting
# RUN dotnet test -c Release --no-build
# ENV ASPNETCORE_ENVIRONMENT Development
RUN dotnet publish -c Release -o published --no-restore --no-build ./ShopsRUs.Api

# Runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/published . 
CMD ["dotnet", "ShopsRUs.Api.dll"]

ARG VERSION=0.0.1
ENV BUILDNUMBER=${VERSION}
