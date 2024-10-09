Design Choices
- Entity Framework Core is used for interacting with the database, allowing me to write queries using LINQ.
- Tried to use SOLID principles for easy to read code.
- Used Dependency Injection in the constructor and Program class. This helps with code reusability.

Challenges
- Setting up RabbitMQ and making sure messages get published and consumed. This was initially tricky but I eventually managed.
- Messages were being queued in RabbitMQ but were not consumed.
- Ensuring the API returns the correct pagination results.
- Testing by running the OT.ASSESSMENT.TESTING using fake data gave me issues. I was getting internal server errors.
- Swagger not opening via Chrome, used Edge instead

Improvements
- Would have wanted to add authorization and authentication for the APIs. This would have enhanced the API’s security and access control
- Would have wanted to add Unit Testing (test each line of code) for code quality.
- Track the result of every API that was ran and store in the database. This would have been useful for monitoring and auditing.
- Would like to work more on performance.

