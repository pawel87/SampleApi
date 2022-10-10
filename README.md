# Sample Web API

***
Overview:

Web API with Sign Up/Sign In capabilities based on JWT Token.

***

User Story: I can register through api providing desired username and password.

User Story: I can authenticate providing my username and password to be able to use private set of APIs.

User Story: [private] As an authenticated user I can see an amount of registered users.

***
Prerequirements:
- SQL Database
***
Initial steps:
- Run database migration (update-database command)

***
Planned improvements:
- Validate commands using validation framework (eg. FluentValidator)
- Add unit tests for Mediator command handlers
- Exception handling (and usage inside handlers)
