using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OOS.Shared.Enums
{
    public enum UserStatus
    {
        [Description("Pending Approval")]
        PendingApproval = 0,

        [Description("Approved")]
        Approved = 1,

        [Description("Rejected")]
        Rejected = 2
    }
}