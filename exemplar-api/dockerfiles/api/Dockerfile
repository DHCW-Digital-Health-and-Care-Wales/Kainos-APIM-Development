FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

COPY . /code

WORKDIR /code

RUN apt-get update && \
    apt-get install -y --no-install-recommends ca-certificates make && \
    update-ca-certificates && \
	make clean build


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS api

COPY --from=build /api /api

WORKDIR /api

CMD [ "dotnet", "patient_demographics_api.dll" ]