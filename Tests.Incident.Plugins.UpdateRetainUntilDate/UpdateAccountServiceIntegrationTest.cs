using System;
using System.Configuration;
using Common;
using Incident.Plugins.UpdateRetainUntilDate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Tests.Common;

namespace IntegrationTests.UpdateRetainUntilDate
{
    [TestClass]
    public class UpdateAccountServiceIntegrationTest
    {
        [TestMethod]
        public void UpdateRetainUntilDate()
        {
            // set up
            var service = new CrmServiceClient(new Uri(ConfigurationManager.AppSettings["crmUrl"]), ConfigurationManager.AppSettings["clientId"], ConfigurationManager.AppSettings["clientSecret"], false, "");

            var updateAccountService = new UpdateAccountService(service, new MockTracingService());
            var status = new OptionSetValue(1);
            var accountId = new EntityReference(Metadata.Account.EntityLogicalName, new Guid("72301053-193b-eb11-a813-0022480069c4"));
            var retainUntil = new DateTime(2017, 10, 12, 20, 35, 1);

            // run the test
            updateAccountService.UpdateRetainUntilDate(retainUntil, accountId, status);

            // verify results
            var record = service.Retrieve(Metadata.Account.EntityLogicalName, accountId.Id, new ColumnSet(Metadata.Account.RetainUntil));

            Assert.AreEqual(new DateTime(2020, 10, 12, 20, 35, 1), record.GetAttributeValue<DateTime?>(Metadata.Account.RetainUntil));
        }
    }
}