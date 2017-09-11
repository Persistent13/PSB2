using System;
using System.Management.Automation;
using PSB2.Functions;
using PSB2.Types;

namespace PSB2
{
    internal static class ModuleVars
    {
        public static string _authorizationToken { get; set; }
        public static string _accountId { get; set; }
        public static string _downloadUri { get; set; }
        public static string _apiUri { get; set; }
        public static Account _account { get; set; }
    }

    [OutputType(typeof(Account))]
    [Cmdlet(VerbsCommunications.Connect, "B2Cloud")]
    [CmdletBinding(ConfirmImpact = ConfirmImpact.None, PositionalBinding = true)]
    public class ConnectB2CloudCommand : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty()]
        public string AccountId { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty()]
        public string ApplicationKey { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(AccountId) | string.IsNullOrEmpty(ApplicationKey))
            {
                try
                {
                    WriteDebug("AccountId and/or ApplicationKey is empty and/or null, prompting for credentials.");
                    PSCredential cred = Host.UI.PromptForCredential(string.Empty, "Please enter the AccountId and ApplicationKey below.", string.Empty, string.Empty);
                    AccountId = cred.UserName;
                    ApplicationKey = cred.GetNetworkCredential().Password;
                }
                catch (Exception err)
                {
                    ThrowTerminatingError(new ErrorRecord(new ArgumentException("AccountId and ApplicationKey cannot be empty."), "InvalidCredentials", ErrorCategory.InvalidData, err));
                }
            }
            Account accountData = B2Functions.AuthorizeAccount(AccountId, ApplicationKey);

            // These are used to automatically fill any required credential data in further cmdlets 
            // so the auth token will not need to be filled out each time a cmdlet is run.
            // This makes running the cmdlets interactivly easier.
            // These will be referenced elsewhere so DO NOT overwrite them
            ModuleVars._authorizationToken = accountData.AuthorizationToken;
            ModuleVars._downloadUri = accountData.DownloadUri;
            ModuleVars._accountId = accountData.AccountId;
            ModuleVars._apiUri = accountData.ApiUri;
            ModuleVars._account = accountData;

            WriteObject(accountData);
        }
    }

    [Alias("gb2b")]
    [OutputType(typeof(Bucket))]
    [CmdletBinding(PositionalBinding = false)]
    [Cmdlet(VerbsCommon.Get, "B2Bucket")]
    public class GetB2BucketCommand : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            if (ModuleVars._account == null)
            {
                ThrowTerminatingError(new ErrorRecord(new FieldAccessException("Please run Connect-B2Cloud to authenticate."), "B2AccountNotAuthenticated", ErrorCategory.AuthenticationError, null));
            }
            BucketContainer bucketData = B2Functions.ListBuckets(ModuleVars._account);

            foreach (Bucket i in bucketData)
            {
                WriteObject(i);
            }
        }
    }
}
