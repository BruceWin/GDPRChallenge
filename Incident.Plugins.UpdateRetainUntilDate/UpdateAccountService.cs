using Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incident.Plugins.UpdateRetainUntilDate
{
    public class UpdateAccountService
    {
        private readonly IOrganizationService service;
        private readonly ITracingService tracingService;
        public UpdateAccountService(IOrganizationService service, ITracingService tracingService)
        {
            this.service = service;
            this.tracingService = tracingService;
        }

        public DateTime Add3Years(DateTime dateTime)
        {
            return dateTime.AddYears(3);
        }

        public void UpdateRetainUntilDate(Guid accountId, DateTime retainUntilDate)
        {
            var record = new Entity(Metadata.Account.EntityLogicalName, accountId);
            record[Metadata.Account.RetainUntil] = retainUntilDate;
            service.Update(record);
        }
    }
}