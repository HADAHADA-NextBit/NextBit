# Set the base image as the .NET 7.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
WORKDIR /app
COPY . ./
RUN dotnet publish ./DotNet.GitHubAction/DotNet.GitHubAction.csproj -c Release -o out --no-self-contained

# Label the container
LABEL maintainer="ekfka4863 <ekfka4863@gmail.com>"
LABEL repository="https://github.com/hadahada-project/NextBit.git"
LABEL homepage="https://github.com/hadahada-project/NextBit.git"

# Label as GitHub action
LABEL com.github.actions.name="c# client backapi"
# Limit to 160 characters
LABEL com.github.actions.description="GitHub Action for c# client backapi"
# See branding:
# https://docs.github.com/actions/creating-actions/metadata-syntax-for-github-actions#branding
LABEL com.github.actions.icon="activity"
LABEL com.github.actions.color="orange"

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/sdk:7.0
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "/DotNet.GitHubAction.dll" ]


#!/bin/sh -l

cd /app

dotnet restore
dotnet build
dotnet run --project src/GitHubActions.Teams.ConsoleApp -- \
    --input-parameter "$INPUT_PARAMETERi"
