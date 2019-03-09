FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Products/Products.Catalog/Products.Catalog.csproj", "Products/Products.Catalog/"]
RUN dotnet restore "Products/Products.Catalog/Products.Catalog.csproj"
COPY . .
WORKDIR "/src/Products/Products.Catalog"
RUN dotnet build "Products.Catalog.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Products.Catalog.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Products.Catalog.dll"]