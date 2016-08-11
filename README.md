# Example_WebApi2_Project
Example with Json.net, Windsor.Castle, JWT security and Namespace versioning wired up.

Application folder contains the entry point Web-Application assembly.
Domain folder contains the Business-Logic assembly.
The Common folder contains Cross-Cutting features (preventing circular dependencies).
The Extensions folder contains an assembly which is not referenced by any of the above.
It's purpose is to show example of extending functionality without compiling the solution, using the IOC framework.

The main entry point to the application is the "Global.asax" file of the Web assembly. Those are the plumbing actions:
Configure IOC - setup container and register types - it also calls all relevant "installers" that register types from the containing assemblies.
Configure Routing - make use of routing attributes in favor of clean versions routing.
Configure Data Formatters - set Json.Net as the default json serializer, remove xml serializers and setup some features.
Register Filters - register Authorisation filter to the request pipeline.

The Web project provides example API with 2 available versions (divided by namespaces).

The Api can be accessed via the swagger ui (if not done automatically, browse to http://[base url:port]/swagger).
Notice that you can switch between API versions with the drop down at the page header.
Method that require authentication will have extra "Authorization" field. Notice that it's type is "header".
This is where you put your token.

Notice that you can provide via the swagger ui, all available response types according to response code.

Notice the "DomainInstaller.Install" method which uses "FromAssemblyInDirectory".
This is how it's possible to add functionality just by adding a new ".dll" to the deployment folder, without recompiling or deploying the entire application.
The "extensions" assembly has post-build event that copies it's output to the folder where IOC will look for it.

Notice the "ProcessData" method in V2/Demo controller.
It uses the "array resolver" functionality of the IOC framework and "chain of responsibility" design pattern, to dynamically resolve the required processor.

