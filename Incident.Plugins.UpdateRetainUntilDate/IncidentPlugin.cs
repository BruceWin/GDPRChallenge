using Common;
using Incident.Plugins.UpdateRetainUntilDate;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRChallenge.Incident.Plugins.UpdateRetainUntilDate
{
    /// <summary>
    /// On Close of Case (statecode change) set the GDPR retain until date on the related account
    /// 
    /// Plugin Registration Details
    /// Message: Update
    /// Primary Entity: incident
    /// Secondary Entity: none
    /// Stage of Execution: PostOperation
    /// Execution Mode: Asynchronous
    /// 
    /// Image Details: 
    /// Post Image Name - Image
    /// Post Image Parameters - modifiedon, customerid, statecode
    /// 
    /// Secure/Unsecure Configuration: NA
    /// 
    /// </summary>
    public class IncidentPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(null);//Impersonate system user

            tracingService.Trace($"Entered: {nameof(IncidentPlugin)}.{nameof(IncidentPlugin.Execute)}");

            var imageExists = context.PostEntityImages.TryGetValue(Metadata.SdkConstants.PostImageName.ToString(), out Entity image);

            if (context.MessageName != Metadata.SdkConstants.SdkMessage_Name.Update.ToString())
            {
                throw new InvalidPluginExecutionException($"Incorrectly registered plugin. Plugin is registered on {context.MessageName}, but expected {Metadata.SdkConstants.SdkMessage_Name.Update.ToString()}");
            }

            if (!imageExists)
            {
                throw new InvalidPluginExecutionException($"Incorrectly registered plugin. Image not found.");
            }

            if(!image?.Contains(Metadata.Incident.ModifiedOn) ?? false)
            {
                throw new InvalidPluginExecutionException($"Incorrectly registered plugin. {Metadata.Incident.ModifiedOn} not found on the image");
            }

            if (!image?.Contains(Metadata.Incident.CustomerId) ?? false)
            {
                throw new InvalidPluginExecutionException($"Incorrectly registered plugin. {Metadata.Incident.CustomerId} not found on the image");
            }

            if (!image?.Contains(Metadata.Incident.Status) ?? false)
            {
                throw new InvalidPluginExecutionException($"Incorrectly registered plugin. {Metadata.Incident.Status} not found on the image");
            }

            var incidentModifiedOn = image.GetAttributeValue<DateTime>(Metadata.Incident.ModifiedOn);
            var customerId = image.GetAttributeValue<EntityReference>(Metadata.Incident.CustomerId);
            var status = image.GetAttributeValue<OptionSetValue>(Metadata.Incident.Status);

            var updateAccountService = new UpdateAccountService(service, tracingService);
            updateAccountService.UpdateRetainUntilDate(incidentModifiedOn, customerId, status);

            tracingService.Trace($"Leaving: {nameof(IncidentPlugin)}.{nameof(IncidentPlugin.Execute)}");

        }
    }
}