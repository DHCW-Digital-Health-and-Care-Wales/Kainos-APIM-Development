# Running API Locally

To run the Patient Demographics API locally, ensure you have the following installed:

- Docker Desktop
- .NET 9.0 SDK
- Git

## Docker Compose Setup

The project uses a `docker-compose.yml` file to orchestrate multiple services:

- **`api`** – The main API service, exposed on port `3000`.
- **`unit-tests`** – Runs unit tests using the `build` target. Outputs results to `./test-results/unit/`.
- **`performance-tests`** – Runs performance tests using the `build` target. Outputs results to `./test-results/performance/`.
- **`api-documentation`** – Serves API documentation from `./documentation/api-documentation` on port `8000`.
- **`maintainer-documentation`** – Serves maintainer documentation from `./documentation/maintainer-documentation` on port `8010`.

## Starting the Services

To start the API and documentation services:

```bash
docker compose up
```
