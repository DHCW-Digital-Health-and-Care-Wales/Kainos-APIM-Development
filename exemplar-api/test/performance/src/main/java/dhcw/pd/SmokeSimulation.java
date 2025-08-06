package dhcw.pd;

import static io.gatling.javaapi.core.CoreDsl.*;
import static io.gatling.javaapi.http.HttpDsl.*;

import io.gatling.javaapi.core.*;
import io.gatling.javaapi.http.*;

public class SmokeSimulation extends Simulation {
	private static final String BASE_URL = System.getProperty("BASE_URL", "http://localhost:3000/FHIR/R4");
	private static final int DURATION = Integer.parseInt(System.getProperty("DURATION", "10"));

	private static final HttpProtocolBuilder httpProtocol = http.baseUrl(BASE_URL)
		.acceptHeader("application/fhir+json")
		.userAgentHeader(
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36");

    private static ScenarioBuilder scenario = scenario("Smoke Test - Patient")
        .exec(http("Get Patient By NHS ID")
        .get("Patient/8888842799"));;

	private static final Assertion assertion = global()
		.failedRequests()
		.count()
		.lt(1L);

	{
		setUp(
			scenario.injectOpen(
				constantUsersPerSec(1).during(60)
			)
		)
		.assertions(assertion)
		.protocols(httpProtocol);
	}
}