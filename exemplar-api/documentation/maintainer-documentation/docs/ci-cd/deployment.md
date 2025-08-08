# Deployment Pipeline

## Overview

The deployment pipeline is responsible for safely and reliably delivering built application artifacts to target environments. It ensures that only validated, secure, and approved builds are deployed, and that deployments are traceable and auditable.

The deployment pipeline is responsible for:

- Getting a correct artifact for the deployment
- Deploying the artifact to the target environment
- Running post-deployment tests and health checks

## Flowchart
```mermaid
flowchart TD
    A[Start Deployment Pipeline] --> B[Get Artifact]
    B --> B1{Artifact Retrieved?}
    B1 -- Yes --> C[Deploy Artifact To Environment]
    B1 -- No --> Z[Error: Step Failed]

    C --> C1{Deployment Successful?}
    C1 -- Yes --> D[Run API Contract Tests]
    C1 -- No --> Z[Error: Step Failed]

    D --> D1{Api Contract Tests Passing?}
	D1 -- Yes --> E1{Is UAT Environment?}
    D1 -- No --> Z[Error: Step Failed]

    E1 -- Yes --> F[Run Performance Tests]
    E1 -- No --> H[Deployment Successful]
	F -- Yes --> G{Performance Tests Passing?}

	G -- Yes --> H[Deployment Successful]
	G -- No --> Z[Error: Step Failed]
```
