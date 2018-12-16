FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build /app/src/NStore.Web/out .
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet NStore.Web.dll