using System;
using System.Management.Automation;

namespace PSB2
{
    [Cmdlet(VerbsCommunications.Connect, "B2Cloud")]
    [CmdletBinding(ConfirmImpact = ConfirmImpact.None)]
    public class ConnectB2CloudCommand : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            throw new NotImplementedException();
        }
    }
}
