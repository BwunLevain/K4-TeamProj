[![CI](https://github.com/BwunLevain/K4-TeamProj/actions/workflows/ci.yml/badge.svg)](https://github.com/OWNER/REPO/actions/workflows/ci.yml)

# About
This solution contains multiple ASP.NET Core projects demonstrating API architecture, proxy communication, testing, and secure configuration.

## Architecture
Projects:
* TimelogAPI is the main backend API that manages TimeLogs and Categories.
* ProxyAPI is a gateway/proxy API that forwards requests to TimelogAPI using HttpClientFactory Typed Clients.
* K4_TeamProj.Tests is our test project containing integration tests using WebApplicationFactory.

ProxyAPI acts as a middle layer and can handle:

* Authentication headers
* API keys
* CORS
* Future caching / logging / rate limiting

## Run Locally
Requirements:
* .NET 9 SDK
* Visual Studio 2022
  
Since the solution contains multiple projects; you should right click the solution and do:
* "Configure Startup Projects"
* "Multiple Startup Projects" and select:
* TimelogAPI
* ProxyAPI

This is because you want to start the different projects at the same time.

You can find the right ports in launchSettings.json

## User Secrets Setup
API keys and secrets are never stored in source control.
Run inside ProxyAPI:

* dotnet user-secrets init
* dotnet user-secrets set "ApiSettings:TimeLogApiKey" "YOUR_KEY_HERE"

To double check your user secrets:
* dotnet user-secrets list
