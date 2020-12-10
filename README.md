# GDPR Challenge

## Build
Build the solution in Visual Studio 2019.

## Run
Register the plugins with the Plugin Registration Tool
https://www.nuget.org/packages/Microsoft.CrmSdk.XrmTooling.PluginRegistrationTool

Upload the JavaScript files as web resources to D365.

## Tests
Run the tests with VS2019 Test Explorer.
Before running the test update the App.config file with crmUrl, clientId, and clientSecret.
NB: do not check the test project App.config file into source control, as it contains credentials.

The tests are integration tests rather than unit tests; please check the data in Dataverse after running the tests.
Change the accountId and retainUntil for other test cases.

## Deployment
Import the managed solution into CDS.
The managed solution includes all of the components.

## Post Deployment: Power Automate Config
The CDS connection needs to be set on the Power Automate called "Delete Accounts and Related Data where Retail Until Date has expired".
NB: the user running the flow must have permissions to read the legal hold fields.
The column security profile called "Legal Hold" provides this access.
