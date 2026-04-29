# K4-TeamProj

[![CI](https://github.com/BwunLevain/K4-TeamProj/actions/workflows/ci.yml/badge.svg)](https://github.com/BwunLevain/K4-TeamProj/actions/workflows/ci.yml)

## About

A distributed backend solution built with ASP.NET Core Web API using a microservices architecture. The system allows users to log time, manage categories, and receive AI-generated feedback based on their time logs.

---

## Architecture

The solution consists of three projects:

**TimelogAPI** — The main backend API responsible for managing TimeLogs and Categories. Includes JWT authentication, rate limiting, caching, pagination, and AI message integration.

**ProxyAPI** — A gateway API that handles communication with the Ollama AI service. Acts as a middle layer between the client and external AI.

**K4-TeamProj.Tests** — Test project containing unit tests and integration tests using WebApplicationFactory and Moq.

### Inter-service Communication

TimelogAPI communicates with ProxyAPI via `IHttpClientFactory` to request AI-generated feedback based on time log data.

---

## Features

- RESTful API with versioned endpoints (`/api/v1/`)
- JWT authentication with `[Authorize]` on protected endpoints
- Custom Exception Middleware with RFC 7807 ProblemDetails responses
- Custom Action Filter for centralized model validation
- Rate Limiting (Sliding Window)
- HybridCache on GET endpoints
- Offset-based pagination with filtering and sorting
- Scalar API documentation
- XML comments on all endpoints
- Strict CORS policy
- External AI integration via Ollama
- Unit tests with Moq
- Integration tests with WebApplicationFactory
- DI validation with ValidateOnBuild and ValidateScopes
- User Secrets for all API keys and secrets

---

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Asp.Versioning 8.x
- HybridCache
- JWT Bearer Authentication
- OllamaSharp
- Scalar.AspNetCore
- xUnit + Moq

---

## Run Locally

**Requirements:**
- .NET 9 SDK
- Visual Studio 2022
- Ollama running on cloud using OllamaSharp using the gemma3:4b model

**Steps:**

1. Clone the repository
2. Right-click the solution → **Configure Startup Projects** → **Multiple Startup Projects**
3. Set both **TimelogAPI** and **ProxyAPI** to **Start**
4. Set up User Secrets (see below)
5. Press F5

Ports can be found in each project's `launchSettings.json`.

---

## User Secrets Setup

Secrets are never stored in source control. Run the following commands to configure them locally.

**TimelogAPI:**
```bash
cd TimelogAPI
dotnet user-secrets set "Jwt:Key" "YOUR_JWT_KEY"
dotnet user-secrets set "Jwt:Issuer" "YOUR_ISSUER"
dotnet user-secrets set "Jwt:Audience" "YOUR_AUDIENCE"
dotnet user-secrets set "ApiKey" "YOUR_API_KEY"
```

**ProxyAPI:**
```bash
cd ProxyAPI
dotnet user-secrets set "OllamaApiKey" "YOUR_OLLAMA_API_KEY"
```

To verify secrets are set:
```bash
dotnet user-secrets list
```

---

## API Endpoints

All endpoints are versioned under `/api/v1/`.

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/api/v1/auth/login` | No | Login and receive JWT token |
| GET | `/api/v1/timelogs` | Yes | Get paged timelogs |
| GET | `/api/v1/timelogs/{id}` | Yes | Get timelog by ID |
| POST | `/api/v1/timelogs` | Yes | Create timelog |
| PUT | `/api/v1/timelogs/{id}` | Yes | Update timelog |
| DELETE | `/api/v1/timelogs/{id}` | Yes | Delete timelog |
| GET | `/api/v1/categories` | Yes | Get paged categories |
| GET | `/api/v1/categories/{id}` | Yes | Get category by ID |
| POST | `/api/v1/categories` | Yes | Create category |
| PUT | `/api/v1/categories/{id}` | Yes | Update category |
| DELETE | `/api/v1/categories/{id}` | Yes | Delete category |
| POST | `/api/v1/savedcontent/generic` | Yes | Get generic AI response |
| POST | `/api/v1/savedcontent/feedback` | Yes | Get AI feedback on timelogs |

---

## Running Tests

```bash
dotnet test
```

---

## Branch Strategy

- `main` — production-ready code
- `dev` — active development branch
- Feature branches merged into `dev` via Pull Requests, reviewed by at least one team member before merge
