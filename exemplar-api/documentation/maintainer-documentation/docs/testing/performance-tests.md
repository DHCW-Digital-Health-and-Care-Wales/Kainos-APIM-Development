# Performance Tests

This guide explains how to write and run performance tests using Gatling in Java, and how to execute them using Docker Compose for the **Patient Demographics API**.

---

## Writing Performance Tests

### Project Structure

Place your Gatling simulation in a dedicated project, e.g.:

```
test/performance/
└── src
    └── test
        └── DHCW
            └── PD
                └── PatientDemographicsSimulation.java
```

### Example Simulation: `PatientDemographicsSimulation.java`

```java
import io.gatling.javaapi.core.*;
import io.gatling.javaapi.http.*;

import static io.gatling.javaapi.core.CoreDsl.*;
import static io.gatling.javaapi.http.HttpDsl.*;

public class PatientDemographicsSimulation extends Simulation {

    HttpProtocolBuilder httpProtocol = http
        .baseUrl("http://api:8080")
        .acceptHeader("application/json");

    ScenarioBuilder scn = scenario("Load Test - Get Patient")
        .exec(http("Get Patient by ID")
            .get("/patients/1")
            .check(status().is(200)))
        .pause(1);

    {
        setUp(scn.injectOpen(rampUsers(100).during(60))).protocols(httpProtocol);
    }
}
```

---

## Running Tests

### Runing the Tests with Docker Compose

```bash
docker compose run --rm performance-tests
```

This will:
- Build the Gatling test project
- Run the simulation
- Output results to `./test-results/performance/`

---

## Viewing Results

After the test run, open:

```
test-results/performance/gatling/<simulation-folder>/index.html
```

in your browser to view detailed performance metrics including:
- Response times
- Requests per second
- Percentile distributions
- Error rates

---

## Tips

- Use `rampUsers`, `constantUsersPerSec`, or `heavisideUsers` to simulate realistic load.
- Parameterize requests with `.feed()` for dynamic data.
- Use assertions to fail tests on performance thresholds.

