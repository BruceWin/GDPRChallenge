using Common;
using Incident.Plugins.UpdateRetainUntilDate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using Tests.Common;

namespace UnitTests.UpdateRetainUntilDate
{
    [TestClass]
    public class UpdateAccountServiceTest
    {
        [TestMethod]
        public void UpdateRetainUntilDate_CustomerIdNull_UpdateNotExpected()
        {
            // set up            
            var service = new MockOrganizationService();
            var updateAccountService = new UpdateAccountService(service, new MockTracingService());
            var status = new OptionSetValue(1);
            var retainUntil = new DateTime(2020, 10, 12, 20, 33, 1);

            // run the test
            updateAccountService.UpdateRetainUntilDate(retainUntil, null, status);

            // verify results
            Assert.IsFalse(service.UpdateWasPerformed);
        }


        [TestMethod]
        public void UpdateRetainUntilDate_CaseIsNotForAnAccount_UpdateNotExpected()
        {
            // set up            
            var service = new MockOrganizationService();
            var updateAccountService = new UpdateAccountService(service, new MockTracingService());
            var status = new OptionSetValue(Metadata.Incident.Status_Resolved);
            var accountId = new EntityReference(Metadata.Contact.EntityLogicalName, new Guid("72301053-193b-eb11-a813-0022480069c4"));
            var retainUntil = new DateTime(2020, 10, 12, 20, 33, 1);

            // run the test
            updateAccountService.UpdateRetainUntilDate(retainUntil, accountId, status);

            // verify results
            Assert.IsFalse(service.UpdateWasPerformed);
        }

        [TestMethod]
        public void UpdateRetainUntilDate_CaseActive_UpdateNotExpected()
        {
            // set up            
            var service = new MockOrganizationService();
            var updateAccountService = new UpdateAccountService(service, new MockTracingService());
            var status = new OptionSetValue(Metadata.Incident.Status_Active);
            var accountId = new EntityReference(Metadata.Account.EntityLogicalName, new Guid("72301053-193b-eb11-a813-0022480069c4"));
            var retainUntil = new DateTime(2020, 10, 12, 20, 33, 1);

            // run the test
            updateAccountService.UpdateRetainUntilDate(retainUntil, accountId, status);

            // verify results
            Assert.IsFalse(service.UpdateWasPerformed);
        }

        [TestMethod]
        public void UpdateRetainUntilDate_CaseCancelled_UpdateExpected()
        {
            // set up            
            var service = new MockOrganizationService();
            var updateAccountService = new UpdateAccountService(service, new MockTracingService());
            var status = new OptionSetValue(Metadata.Incident.Status_Cancelled);
            var accountId = new EntityReference(Metadata.Account.EntityLogicalName, new Guid("72301053-193b-eb11-a813-0022480069c4"));
            var retainUntil = new DateTime(2020, 10, 12, 20, 33, 1);

            // run the test
            updateAccountService.UpdateRetainUntilDate(retainUntil, accountId, status);

            // verify results
            Assert.AreEqual(new DateTime(2023, 10, 12, 20, 33, 1), service.UpdatedEntity.GetAttributeValue<DateTime>(Metadata.Account.RetainUntil));
        }

        [TestMethod]
        public void UpdateRetainUntilDate_CaseResolved_UpdateExpected()
        {
            // set up            
            var service = new MockOrganizationService();
            var updateAccountService = new UpdateAccountService(service, new MockTracingService());
            var status = new OptionSetValue(Metadata.Incident.Status_Resolved);
            var accountId = new EntityReference(Metadata.Account.EntityLogicalName, new Guid("72301053-193b-eb11-a813-0022480069c4"));
            var retainUntil = new DateTime(2020, 10, 12, 20, 33, 1);

            // run the test
            updateAccountService.UpdateRetainUntilDate(retainUntil, accountId, status);

            // verify results
            Assert.AreEqual(new DateTime(2023, 10, 12, 20, 33, 1), service.UpdatedEntity.GetAttributeValue<DateTime>(Metadata.Account.RetainUntil));
        }
    }
}
