FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Radancy.Interviews.Examples.Banking.Sdet/Radancy.Interviews.Examples.Banking.Sdet.csproj", "Radancy.Interviews.Examples.Banking.Sdet/"]
RUN dotnet restore "Radancy.Interviews.Examples.Banking.Sdet/Radancy.Interviews.Examples.Banking.Sdet.csproj"
COPY . .
WORKDIR "/src/Radancy.Interviews.Examples.Banking.Sdet"
RUN dotnet build "Radancy.Interviews.Examples.Banking.Sdet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Radancy.Interviews.Examples.Banking.Sdet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Radancy.Interviews.Examples.Banking.Sdet.dll"]
