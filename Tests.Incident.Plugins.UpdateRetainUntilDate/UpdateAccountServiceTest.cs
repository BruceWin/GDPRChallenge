using System;
using System.Configuration;
using Common;
using Incident.Plugins.UpdateRetainUntilDate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Tests.Common;

namespace Tests.Incident.Plugins.UpdateRetainUntilDate
{
    [TestClass]
    public class UpdateAccountServiceTest
    {
        [TestMethod]
        public void UpdateRetainUntilDate()
        {
            // get the CRM service
            var service = new CrmServiceClient(new Uri(ConfigurationManager.AppSettings["crmUrl"]), ConfigurationManager.AppSettings["clientId"], ConfigurationManager.AppSettings["clientSecret"], false, "");

            // set up the test
            var updateAccountService = new UpdateAccountService(service, new MockTracingService());

            var accountId = new Guid("aaa19cdd-88df-e311-b8e5-6c3be5a8b200");
            var retainUntil = new DateTime(2022, 10, 12, 9, 40,1);

            // run the test
            updateAccountService.UpdateRetainUntilDate(accountId, retainUntil);
            
            // verify results
            var record = service.Retrieve(Metadata.Account.EntityLogicalName, accountId, new ColumnSet(Metadata.Account.RetainUntil));

            Assert.AreEqual(retainUntil, record.GetAttributeValue<DateTime?>(Metadata.Account.RetainUntil));
        }

        [TestMethod]
        public void AddThreeYears()
        {
            // get the CRM service
            var service = new CrmServiceClient(new Uri(ConfigurationManager.AppSettings["crmUrl"]), ConfigurationManager.AppSettings["clientId"], ConfigurationManager.AppSettings["clientSecret"], false, "");

            // set up the test
            var updateAccountService = new UpdateAccountService(service, new MockTracingService());

            var retainUntil = new DateTime(2020, 10, 12, 9, 40, 1);

            var actual = updateAccountService.Add3Years(retainUntil);

            var expected = new DateTime(2023, 10, 12, 9, 40, 1);

            Assert.AreEqual(expected, actual);

        }
    }
}