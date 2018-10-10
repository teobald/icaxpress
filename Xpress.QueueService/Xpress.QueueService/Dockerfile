#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 50763
EXPOSE 44381

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["Xpress.QueService/Xpress.QueService.csproj", "Xpress.QueService/"]
RUN dotnet restore "Xpress.QueService/Xpress.QueService.csproj"
COPY . .
WORKDIR "/src/Xpress.QueService"
RUN dotnet build "Xpress.QueService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Xpress.QueService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Xpress.QueService.dll"]