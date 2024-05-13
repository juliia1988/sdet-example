## Requirements

- As a User, I can have as many accounts as I want. --> Covered via Postman perfomance testing with run collection for Create account 10 (any numbers) of requests in parallell to check if those accounts will be created successfuly. Postman extantion was installed direct to the VS Code and configured with scenario for perfomance testing (could be showen during next interview call)


- As a User, I can create and delete accounts. --> Covered in /Users/olenavoloshyna/Downloads/sdet-example/apiTestRadancy/AccountTests/AccountCreate.cs


- As a User, I can deposit and withdraw from my accounts. --> Covered in /Users/olenavoloshyna/Downloads/sdet-example/apiTestRadancy/TransactionTests/TrancactionCreate.cs

- As a User, I can see the balance of my accounts.  --> Covered in /Users/olenavoloshyna/Downloads/sdet-example/apiTestRadancy/TransactionTests/TrancactionCreate.cs and /Users/olenavoloshyna/Downloads/sdet-example/apiTestRadancy/AccountTests/AccountGet.cs


- As a User, I cannot cannot have less than $100 at any time in an any account. --> Covered in /Users/olenavoloshyna/Downloads/sdet-example/apiTestRadancy/AccountTests/AccountCreate.cs

- As a User, I cannot withdraw more than 90% of my total balance from an account in a single transaction.  --> Covered in /Users/olenavoloshyna/Downloads/sdet-example/apiTestRadancy/TransactionTests/TrancactionCreate.cs

- As a User, I cannot deposit more than $10,000 in a single transaction. --> Covered to /Users/olenavoloshyna/Downloads/sdet-example/apiTestRadancy/TransactionTests/TrancactionCreate.cs and /Users/olenavoloshyna/Downloads/sdet-example/apiTestRadancy/AccountTests/AccountCreate.cs



Bonus points:

- What tests would you implement, in case that this API uses a real database? --> I would implement the integration tests that will check if records in db was success, if update/delete works, also performance testing and validation (for allowed data format, limits, etc.)


- Describe the test strategy (test types, test levels etc.) in case this would be a live product, used by a large number
 of users. Assume that the team wants to deploy quickly - multiple times a day, and that the business is willing to invest
 into good testing strategy. --> 
 
 
 
    Firstly I need to know more about our system architecture. According to CAP theory system could support only two from those three characteristics at the same time: partition tolerance / accessibility / consistent. I need to know our priorities and also our weakness and pains, to build testing strategy depends of our specific needs. 
 
   But in general testing will include all test levels. We could just agreed who will cover which test level (becouse unit level in general the prerogative of developers), part of integration test could also be covered by developers, to make all delivery process quicker. 
   
   Important also to have good coverage of regression testing by automation tests. Those tests should cover e2e scenarios (not to much, to make test process by effective but also quick) and we need to have well organized running schedule to run it in period of low user activities. 
   
   
   Regarding testing types, it will be functional, non-functional (performance and accessibility is a first priority in our case I think here), change related tests (regression, re-test), automation will also include the white-box tests (decision, statement)


- Explain how would you approach testing the performance of this API, assuming such requirements exist? --> We could use Jmeter with proper scenarios for performance testing for more deep testing (load, stress, volume) and as quick check we could use Postman (it allows you to run collection with configurable parameters like number of requests and time between those requests. Also Postman Extension already available for Visual Code, so its very convention now to test in the same app)

- Explain how would you approach testing the security of this API, assuming such requirements exist?  --> I will test authorization and authentication, rbac, will use Burp Suite + Postman to catch requests from browser and modify/grab data  


- Explain how would the test infrastructure look like? ---> If I understand this question correct the infrastructure that could be used for organized testing process will include Jenkins for CI/CD (pipeline is triggered by events like code commits or pull requests to the Git repository), Git, Test Automation Framework (VS Code or other depends of project needs), test environments and data bases, monitoring and logging, environment management (Kubernetes), project management tools like Jira, Confluence  


- Explain what tools for test management would you use? --> now I use X-Ray for Jira, I found it good enough, also previously have use TestRail, TesLink 


- Explain how would you manage a situation where this API exists, but there are no written requirements for it (e.g. in
case of legacy projects and apps). --> I will work with collecting the actual requirements and will document it. Its important to have API reference guides actual for each of release version and API version other way we could have issues (functional, security weakness)
