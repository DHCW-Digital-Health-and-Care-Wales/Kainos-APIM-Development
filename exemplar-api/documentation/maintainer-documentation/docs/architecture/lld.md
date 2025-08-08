# Low-Level Design

The aim of this section is to include details about inner workings of the service. 

It could include:

- Internal business logic described as digrams or pseudo-code, or a mixture of both
- Data mapping that happend between source and output

## Patient Resource

### Get Patient By ID

#### Flowchart mapping responses from the endpoint
``` mermaid
 flowchart TD
    A[Client sends GET /Patient/:id] --> B[Validate parameter and headers: id, x-api-key, Authorization]
    B --> C[Are parameters and headers valid?]
    C -->|No| D[Return 400 Bad Request]
    C -->|Yes| E[Authenticate ApiKey]
    E --> F[Is ApiKey valid?]
    F -->|No| G[Return 401 Unauthorized]
    F -->|Yes| H[Authorize access]
    H --> I[Is access allowed?]
    I -->|No| J[Return 403 Forbidden]
    I -->|Yes| K[Call MPI service to retrieve patient]
    K --> L[Did MPI return patient?]
    L -->|No| M[Return 404 Not Found]
    L -->|Yes| N[Return 200 OK]
    K --> O[Timeout or error?]
    O -->|Timeout| P[Return 408 Timeout]
    O -->|Too many requests| Q[Return 429 Too Many Requests]
    O -->|Internal error| R[Return 500 Internal Error] 
```

#### Sequence diagram showing request lifecycle
``` mermaid
 	sequenceDiagram
    participant HP as Healthcare Provider
    participant Apigee as Apigee Gateway
    participant API as Demographics API
    participant OID as OID Provider
    participant MPI as MPI Service
    participant Azure as AzureInsights

    HP->>Apigee: Request patient demographics
    Apigee->>API: Forward request
    API->>OID: Validate token
    OID-->>API: Token valid
    API->>MPI: Query patient data
	API->>API: Map MPI data to FHIR
    MPI-->>API: Return demographics
    API->>Azure: Telemetry data
    API-->>Apigee: Return response
    Apigee-->>HP: Response with demographics 
```