# SingleStone.Contact
This is part of the tech challenge for a job application.  The requirements were to create a simple RESTful service for a Contact database with a data store.  

I chose LiteDB for a local data store. I haven't used it before, but it looked interesting. Using the DB was very straight forward.  The hardest part was actually just getting the app folder location at startup so I didn't require a hard coded config value.  I hadn't used a local file reference in Core before; it's very different than framework.

TODO:
Add model validation with Fluent Validation
Add Unit Testing 
