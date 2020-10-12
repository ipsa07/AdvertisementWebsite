# Summary of Microservices project:

## Advertising Website :

The system allows registered users to advertise their products or services. Public users can browse the advertisements or search by keywords. The system is based on Microservices architecture.

###### Technical Stack:
* Asp.Net Core MVC

* Restful Api

* Amazon Web Services

  * Cognito: User Authentication and logging

  * S3 : Storing images added while creation of advertisement

  * Dynamo DB : Storing Advertisement details

  * Lambda : Gets triggered as part of SNS. Writes advertisements to elastic search

  * Cloudwatch : Monitoring and logs

  * Cloudfront : Edge cache for images in S3

  * Elastic Search : Creates search index on advertisements text to serve search API.

  * CodeDeploy : Artifact deployment

  * ECS : Orchestration

  * SNS : Advertisement microservice notifies lambda to write adv details to elastic search

  * API Gateway : Acts as a reverse proxy to accept all application programming interface (API) calls.

  * EC2

  * CloudTrail : Used to explore or navigate Api service logs

  * Kibana : Monitoring dashboard

  * Cloud Map : Registration of microservices

     * Implement service discovery
     * Monitors health of the service
     * Register service and service instances
 * Load Balancer : Separate load balancer for advertisement and serach microservice

* Swagger UI : Dynamic doumentation and web client

* Docker

###### Actors:
**Registered user**: A user who has created an account in the website, and is logged in to the system using their username and password.
**Public user**: All users whether they are registered users or unregistered users .

###### Requirements
Users register in the system and create an account

  * Email address must be used as the username.

  * Password must be at least 6 character long.

  * Only registered users can create an advertisement.

An advertisement includes the below attributes:

 * Title

 * Description

 * Expiry Date

 * Price

 * One image

Public users must be able to browse through the advertisements.

Public users must be able to see the details of an advertisement.

Public users must be able to search for advertisements by keyword(s).

###### Description:
User will sign up and upon signing up, user will receive a confirmation mail to verify the account. Once the account is verified, user is a registered user in AWS Cognito Database. Registered users can create and publish their advertisements. Any Registered/Non Registered user can search for the advertisement by search term.

Monitoring and health check is done by CloudWatch.

###### Patterns
**Eventual Consistency**

**Exponential Backoff and Circuit breaker pattern for resiliency**

**CQRS(Command and Query responsibility segregation)— Microservices handling queries should be separated from Microservices handling commands**

**Service Discovery Pattern**

###### Logging:
The infrastructure logs are placed in cloud watch. AWS cloud trail is used for logging security,AWS API logs.I have used Kibana to see the dumped logs in elastic search.

###### API gateway

###### AWS deployment options:
**Cloudformation** — Template for creating everthing like ec2 instances, load balancer etc. Implements rolling deployment

**CodeDeploy** - Takes care of deploying code in already created EC2 instance

**Docker and AWS ECS (Used in project)**
