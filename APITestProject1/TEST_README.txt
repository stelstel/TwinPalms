To run tests:

1. The database must be new and recently seeded

2. The solution must have been run at least once

3. Most of the test dosn't work when tesks has authorization.
	For example: [HttpPost, DisableRequestSizeLimit, Authorize(Roles = "Basic")]