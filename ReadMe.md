# Intro

The example given here is a very simple API, that allows SDET candidates to do a brief demonstration of their skills.
As such, this API is not state of the art, and does not represent best practices or patterns.

The requirements given below for this task, are simplistic, and the focus will be more on the discussion that is a follow-up
on this example, hypothetical scenarios around that. However, if the candidate wants to demonstrate an advanced approach,
additional/extra skills, or best practices, they are welcome to do so.

> Due to this example bing more or less a hack, bugs are possible. In case you find some, it would be great to report them. 
> Also, if you have any suggestions, or improvements, feel free to suggest or make them. We are also available for any 
> questions or clarifications.

# Project Description

## Requirements

- As a User, I can have as many accounts as I want.
- As a User, I can create and delete accounts.
- As a User, I can deposit and withdraw from my accounts.
- As a User, I can see the balance of my accounts.
- As a User, I cannot cannot have less than $100 at any time in an any account.
- As a User, I cannot withdraw more than 90% of my total balance from an account in a single transaction. 
- As a User, I cannot deposit more than $10,000 in a single transaction. 

> There are no other requirements for this API, like transferring money between accounts and such. Also, there are no
requirements for authentication, authorization at the moment. This is to keep the example simple.

## Technical Information

- The API is a RESTful API, and it is implemented using ASP.NET 8.0.
- The API is using a a simple in-memory data store, no real database.
- The API can be run directly from the IDE, running the `Radancy.Interviews.Banking.Sdet.Api` project.
- The API can be run from the command line, using the `dotnet run` command in the `Radancy.Interviews.Banking.Sdet.Api` folder.
- After being run, the API can be accessed at `http://localhost:5128`, and the Swagger UI is available at `http://localhost:5128/swagger/index.html`.

# Task Description

Minimum requirements:

- Develop a set of tests for the given project, that will cover the requirements given below. Tests can be on any level,
and of any kind. 
- It is preferable to implement the tests using XUnit and C#, but other tools and approach are welcome,
with a good argumentation.
- Tests must be runnable from within the IDE (e.g. Visual Studio), or at least there must be a "one click" solution to run
them easily, and quickly.
- A short explanation of the test strategy, and the reasoning behind the chosen tests, should be provided in the form of
a ReadMe file, or similar.

Bonus points:

- What tests would you implement, in case that this API uses a real database?
- Describe the test strategy (test types, test levels etc.) in case this would be a live product, used by a large number
 of users. Assume that the team wants to deploy quickly - multiple times a day, and that the business is willing to invest
 into good testing strategy.
- Explain how would you approach testing the performance of this API, assuming such requirements exist?
- Explain how would you approach testing the security of this API, assuming such requirements exist?
- Explain how would the test infrastructure look like?
- Explain what tools for test management would you use?
- Explain how would you manage a situation where this API exists, but there are no written requirements for it (e.g. in
case of legacy projects and apps).

> Explanation can be brief, and a broader, more detailed explanation can be given during the verbal discussion.



