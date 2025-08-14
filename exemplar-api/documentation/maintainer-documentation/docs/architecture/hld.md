# High-Level Design

**REMOVE ANY SECTIONS THAT ARE NOT RELEVANT**

## Patient Demographics API
###### [AUTHOR], [ROLE], [DATE]
###### [CONTACT_INFORMATION]


## Introduction

An introduction to the solution what it is and relevant system/s it's part of.

---

## User Research Analysis

This section should contain information about the need driving the development of the solution. 

---

## Problem Statement [P]

This section should contain the problem statement, along with a list of problems the solution is meant to solve.

Problems:

1. Problem 1
2. Problem 2

---

## Objectives [O]

A brief overview of the main objectives of the solution.

Objectives.

1. Objective 1
2. Objective 2

---

## Scope [S]

List of all functionality that is in scope.

1. Scope 1
2. Scope 2

### Not in scope

List of all functionality that is not in scope.

1. Not in scope 1
2. Not is scope 2

---

## Service Design

List of UI screens that will be part of the delivered solutions, including any content to be presented to the user.

---

## Context

Any additional business or technical context to be considered in the design.

---

## Solution Architecture

### As-is

Includes the documentation of existing architecture that the service will be integrating with or extending.

### To-be

Includes the documentation of to-be architecture, multiple stages can be outlined, if required.

UML, C4 or any relevant framework should be used for documenting architecrural overview. In HLD, keep only the high level overview - the exact inner workings of the service are not relevant here, along with very detailed view of the infrastructure. Keep the most relevant information.

#### Context View
``` mermaid
	C4Context
    Person(hp, "Healthcare Providers", "Hospitals, GP practices, healthcare boards, etc.")
    System(api_sys, "Patient Demographics API System", "Provides patient demographic data to authorized users")
    Person(is, "Internal Services", "Other internal systems interacting with the API")

    System_Ext(mpi, "MPI Service", "External System", "Provides patient demographic data")
    System_Ext(oid, "OID Provider", "Authentication Service", "Authenticates API requests")
    System_Ext(az, "AzureInsights", "Monitoring", "Receives logs and telemetry")

    Rel(hp, api_sys, "Uses")
    Rel(is, api_sys, "Uses")
    Rel(api_sys, mpi, "Queries for patient data")
    Rel(api_sys, oid, "Authenticates via")
    Rel(api_sys, az, "Sends logs and telemetry to") 
```

#### Container View
``` mermaid
	C4Container
    Person(hp, "Healthcare Providers", "Hospitals, GP practices, healthcare boards, etc.")
    Person(is, "Internal Services", "Other internal systems interacting with the API")

    System_Boundary(sys, "Infrastructure") {
        Container(apigee, "Apigee", "API Gateway", "Exposes the Patient Demographics API")
        Container(api, "Demographics API", "REST API", "Handles patient demographic queries")
        Container(oid, "OID Provider", "Auth Service", "Authenticates API requests")
        Container(az, "AzureInsights", "Monitoring", "Receives telemetry")
    }

    System_Ext(mpi, "MPI Service", "External System", "Data Source")

    Rel(hp, apigee, "Uses")
    Rel(is, apigee, "Uses")
    Rel(apigee, api, "Routes requests to")
    Rel(api, oid, "Authenticates via")
    Rel(api, mpi, "Queries for patient data")
    Rel(api, az, "Sends logs and telemetry to") 
```

#### Solution Capabilities

A description of high-level overview of the interaction of the components, as a list step-by-step.
This should include some additional details or context that is not included on diagram.

1. Step 1
2. Step 2

---

## Functional Requirements

List of required functionality, or use cases.

1. Functionality 1
2. Functionality 2

---

## Non-Functional Requirements

Include infomration about non-functional requirements, some of those are included below. This will be specific and tailored to each solution.

---

## Testing

Outline all testing required by the solution to ensure the goals and objectives are met. Include links to other parts of documentation if possible.

---

## Deployment

Describe how the service will be deployed. Include links to other parts of documentation if possible.

### Cloud platforms

List cloud platforms used for hosting infrastructure, mention on-prem services if applicable.

#### Components

Which cloud services are used and what are they used for.

### Infrastructure as Code

What IaC tooling will be used for creating the infrastructure

### CICD

---

## Support and monitoring

Who will be responsible for supporting and monitoring the live service.

---

## RAID

### Risks

### Assumptions

### Issues

### Dependencies

---

## Glossary

---

## Status updates

List all rejections and approvals from internal architecture board.

### 08/08/2025 - Approved
###### [PRESENTER]

Agreed plan and any additional considerations.

Feedback:

- item 1
- item 2

### 01/07/2025 - Rejected
###### [PRESENTER]
###### Reason for rejection

Feedback:

- item 1
- item 2

Modifications required for approval:

- modification 1
- modification 2