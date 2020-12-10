# GDPRChallenge

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


