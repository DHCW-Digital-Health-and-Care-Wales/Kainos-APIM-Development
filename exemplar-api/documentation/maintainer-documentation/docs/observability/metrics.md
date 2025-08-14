# Metrics

## Overview
This document outlines the metrics strategy for the **[API Name]** hosted in Azure. It describes the types of metrics collected, where they are stored, how to access them, and how they support performance monitoring and operational insights.

---

## What Metrics Are Collected

### Performance Metrics
- **Request Count**: Total number of API requests.
- **Response Time (Latency)**: Average and percentile response times.
- **Success Rate**: Percentage of successful responses (2xx).
- **Error Rate**: Percentage of failed responses (4xx, 5xx).

### Infrastructure Metrics
- **CPU Usage**: Percentage of CPU utilization.
- **Memory Usage**: RAM consumption over time.
- **Disk I/O**: Read/write operations.
- **Network I/O**: Incoming/outgoing traffic volume.

### Custom Business Metrics
- **User Signups**
- **Transactions Processed**
- **Feature Usage Events**

---

## Where Metrics Are Stored

### Azure Services Used
- **Azure Monitor**: Centralized metrics collection and visualization.
- **Application Insights**: Telemetry and performance metrics.
- **Log Analytics Workspace**: For querying and correlating metrics with logs.

---

## How to Access Metrics
### Azure Portal
- Navigate to **Application Insights** or **Azure Monitor**.
- Use **Metrics Explorer** to view charts and dashboards.
- Set filters by time range, resource, and metric type.

### Azure CLI / PowerShell
- Example CLI command:
  ```bash
  az monitor metrics list --resource <resource-id> --metric "Requests"
  ```

### Dashboards
- Custom dashboards available in Azure Portal under Dashboard section.
- Includes real-time charts for latency, error rate, and traffic volume.

### Alerts Based on Metrics
- Latency Alert: Triggered when average response time > 1s.
- Error Rate Alert: Triggered when error rate > 5% over 5 minutes.
- Traffic Drop Alert: Triggered when request count drops >50% compared to baseline.

### Retention & Export
- Default Retention: 90 days for metrics in Azure Monitor.
- Export Options:
	- Continuous export to Azure Storage or Event Hub.
	- Integration with Power BI or Grafana for advanced visualization.

### Contact & Support
- [Position], [Team Name] - [email@example.com]
