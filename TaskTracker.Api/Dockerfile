#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskTracker.Api/TaskTracker.Api.csproj", "TaskTracker.Api/"]
COPY ["TaskTracker.Contract/TaskTracker.Contract.csproj", "TaskTracker.Contract/"]
COPY ["TaskTracker.Entities/TaskTracker.Entities.csproj", "TaskTracker.Entities/"]
COPY ["TaskTracker.Service/TaskTracker.Service.csproj", "TaskTracker.Service/"]
COPY ["TaskTracker.Repository/TaskTracker.Repository.csproj", "TaskTracker.Repository/"]
RUN dotnet restore "TaskTracker.Api/TaskTracker.Api.csproj"
COPY . .
WORKDIR "/src/TaskTracker.Api"
RUN dotnet build "TaskTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskTracker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskTracker.Api.dll"]