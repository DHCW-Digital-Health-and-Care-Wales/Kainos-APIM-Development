# Build Pipeline

## Overview

The Build Pipeline is a foundational component of the CI/CD process. It automates the validation and packaging of source code to ensure that only high-quality, secure, and standards-compliant artifacts proceed to deployment. This pipeline is triggered on code changes and is designed to catch issues early in the development lifecycle.

The build pipeline is responsible for:

- ensuring code adheres to coding standards
- vulnerabilities and any leaked secrets are highlighted
- building API artifact (Docker image, DLL, or a ZIP package)

## Flowchart illustrating the build pipeline steps
``` mermaid
	flowchart TD
    A[Start Build Pipeline] --> B[Linting]
    B --> B1{Linting Succeeded?}
    B1 -- Yes --> C[Commit Message Checks<br>on Dev Branches]
    B1 -- No --> Z[Error]

    C --> C1{Commit Check Succeeded?}
    C1 -- Yes --> D[Branch Name Checks<br>on Dev Branches]
    C1 -- No --> Z

    D --> D1{Branch Name Check Succeeded?}
    D1 -- Yes --> E[Vulnerability Checks]
    D1 -- No --> Z

    E --> E1{Vulnerability Check Succeeded?}
    E1 -- Yes --> F[Secret Scanning]
    E1 -- No --> Z

    F --> F1{Secret Scanning Succeeded?}
    F1 -- Yes --> G[Build Application]
    F1 -- No --> Z

    G --> G1{Secret Scanning Succeeded?}
    G1 -- Yes --> H[Build Application]
    G1 -- No --> Z

    H --> H1{Build Succeeded?}
    H1 -- Yes --> I[Publish to Internal Artifact Store]
    H1 -- No --> Z

    I --> I1{Publish Succeeded?}
    I1 -- Yes --> J[End]
    I1 -- No --> Z

    Z[Error: Step Failed] 
```