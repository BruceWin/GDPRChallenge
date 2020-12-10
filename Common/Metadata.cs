using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class Metadata
    {
        public static class Account
        {
            public const string EntityLogicalName = "account";
            public const string Name = "name";
            public const string RetainUntil = "bw_retainuntil";
        }

        public static class Contact
        {
            public const string EntityLogicalName = "contact";
        }

        public static class Incident
        {
            public const string ModifiedOn = "modifiedon";
            public const string CustomerId = "customerid";
            public const string Status = "statecode";
            public const int Status_Active = 0;
            public const int Status_Resolved = 1;
            public const int Status_Cancelled = 2;
        }

        public static class SdkConstants
        {
            public enum SdkMessage_Name
            {
                Create,
                Update,
                Delete,
                Associate,
                Disassociate,
                Retrieve,
                Assign,
                GrantAccess,
                ModifyAccess,
                RetrieveMultiple,
                RetrievePrincipalAccess,
                RetrieveSharedPrincipalsAndAccess,
                RevokeAccess,
                SetState,
                SetStateDynamicEntity,
                PickFromQueue,
                Merge
            };

            public const string Context_Target = "Target";
            public const string PostImageName = "Image";

        }
    }
}