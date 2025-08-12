# Logging

## Overview
This document describes the logging strategy for the **[API Name]** hosted in Azure. It includes details on what is logged, where logs are stored, and how to access and interpret them for monitoring and troubleshooting.

---

## What Is Logged

### Request Logs
- **Fields**:
  - Timestamp
  - HTTP Method
  - Endpoint
  - Response Status Code
  - Latency
  - Caller IP
  - Authentication status

### Error Logs
- **Fields**:
  - Exception type
  - Stack trace
  - Request context
  - User ID (if available)
  - Correlation ID

### Custom Application Logs
- **Examples**:
  - Business logic events
  - Feature usage metrics
  - Debugging information (in non-prod)

### Security Logs
- **Fields**:
  - Failed login attempts
  - Token validation failures
  - Unauthorized access attempts

---

## Where Logs Are Stored

### Azure Services Used
- **Application Insights**: For telemetry, performance, and custom logs.
- **Azure Monitor Logs (Log Analytics Workspace)**: For querying and analyzing structured logs.
- **Azure Storage (optional)**: For long-term archival of raw logs.

---

## How to Access Logs

### Azure Portal
- Navigate to **Application Insights** or **Log Analytics Workspace**.
- Use **Log Search** or **Metrics Explorer** to view logs.

### Kusto Query Language (KQL)
- Use KQL to query logs in Log Analytics.
- Example:
  ```kql
  requests
  | where timestamp > ago(1h)
  | where resultCode == "500"
  | project timestamp, name, resultCode, duration, operation_Id
  ```
  
---

## Retention & Archiving
- Default Retention: 30 days in Log Analytics.
- Extended Retention: Configurable up to 2 years.
- Archival: Optionally export to Azure Storage or Event Hub.

## Contact & Support
- [Position], [Team Name] - [email@example.com]
