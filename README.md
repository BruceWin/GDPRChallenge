# GDPR Challenge

## Build
Build the solution in Visual Studio 2019.

## Run
Register the plugins with the Plugin Registration Tool
https://www.nuget.org/packages/Microsoft.CrmSdk.XrmTooling.PluginRegistrationTool

Upload the JavaScript files as web resources to D365.

The scheduled process that deleted accounts and related data is built with Power Automate.

## Unit Tests
The unit test project is named UnitTests.UpdateRetainUntilDate.
The tests in this project are self contained and do not reach out to CDS (while the tests in the integration test project do).
These tests could be executed within a build pipeline.

Run the tests with VS2019 Test Explorer.

## Integration Tests
The integration test project is named IntegrationTests.UpdateRetainUntilDate.
These tests connect to CDS.
These tests are useful for during development.
They allow for the code to be tested outside of the CDS event execution pipeline.

Before running the test update the App.config file with crmUrl, clientId, and clientSecret.
NB: do not check the test project App.config file into source control, as it contains credentials.

The tests are integration tests rather than unit tests; please check the data in Dataverse after running the tests.
Change the accountId and retainUntil for other test cases.

Run the tests with VS2019 Test Explorer.

## Deployment
Import the managed solution into CDS.
The managed solution includes all of the components.

## Post Deployment: Power Automate Config
The CDS connection needs to be set on the Power Automate called "Delete Accounts and Related Data where Retail Until Date has expired".
NB: the user running the flow must have permissions to read the legal hold fields.
The column security profile called "Legal Hold" provides this access.

## Solution Notes
Accounts are deleted via Power Automate, running on a schedule.
Cases, Contacts, Activities, and Attachments are deleted when the account is deleted due to the cascade delete behaviour of these relationships.

The Retain Until date field is set on the account via an async plugin (GDPRChallenge.Incident.Plugins.UpdateRetainUntilDate.IncidentPlugin).

The Legal Hold fields on the account have a column (field) security profile "Legal Hold"; only users assigned this column security profile can view or change the 2 fields "Legal Hold" and "Legal Hold Reason". I could not do it via the security role "CEO-Business Manager" because security roles can only apply permissions at the table (entity) level.

There is JavaScript on the account form that shows an alert when a legal hold is applied.
The JavaScript also sets "Legal Hold Reason" to business required.
