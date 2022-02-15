# escape=`
#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/powershell AS downloadnodejs
ENV NODE_VERSION=14.16.0
SHELL ["pwsh", "-Command", "$ErrorActionPreference = 'Stop';$ProgressPreference='silentlyContinue';"]
RUN Invoke-WebRequest -OutFile nodejs.zip -UseBasicParsing "https://nodejs.org/dist/v$($env:NODE_VERSION)/node-v$($env:NODE_VERSION)-win-x64.zip"; `
    Expand-Archive nodejs.zip -DestinationPath C:\; `
    Rename-Item "C:\node-v$($env:NODE_VERSION)-win-x64" c:\nodejs

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs


COPY --from=downloadnodejs C:\nodejs\ C:\Windows\system32\
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
COPY --from=downloadnodejs C:\nodejs\ C:\Windows\system32\

WORKDIR /src
COPY ["SweaterBrain/SweaterBrain.csproj", "SweaterBrain/"]
RUN dotnet restore "SweaterBrain/SweaterBrain.csproj"
COPY . .
WORKDIR "/src/SweaterBrain"
RUN dotnet build "SweaterBrain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SweaterBrain.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SweaterBrain.dll"]
