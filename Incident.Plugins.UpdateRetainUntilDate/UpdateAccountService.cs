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

        public void UpdateRetainUntilDate(DateTime incidentModifiedOn, EntityReference customerId, OptionSetValue status)
        {
            tracingService.Trace($"Entered: {nameof(UpdateAccountService)}.{nameof(UpdateRetainUntilDate)}");
            tracingService.Trace($"{nameof(incidentModifiedOn)}: {incidentModifiedOn} - {incidentModifiedOn.Kind}");
            tracingService.Trace($"{nameof(customerId)}: {customerId?.Name} {customerId?.Id}");
            tracingService.Trace($"{nameof(status)}: {status?.Value}");

            if (customerId == null)
            {
                tracingService.Trace("customer missing. stopping processing.");
                return;
            }
            if (customerId?.LogicalName != Metadata.Account.EntityLogicalName)
            {
                tracingService.Trace("case not for an account. stopping processing.");
                return;
            }
            if (status?.Value == 0)
            {
                tracingService.Trace("case is still active. stopping processing.");
                return;
            }

            var retainUntilDate = incidentModifiedOn.AddYears(3);

            tracingService.Trace($"Setting {Metadata.Account.RetainUntil} to {retainUntilDate}");
            var record = new Entity(Metadata.Account.EntityLogicalName, customerId.Id);
            record[Metadata.Account.RetainUntil] = retainUntilDate;
            service.Update(record);
            tracingService.Trace($"Update done.");
        }
    }
}