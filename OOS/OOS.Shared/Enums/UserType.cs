using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OOS.Shared.Enums
{
    public enum UserType
    {
        [Description("Guest")]
        Guest = 0,

        [Description("Member")]
        Member = 1,

        [Description("Administrator")]
        Administrator = 2,

        [Description("Manager")]
        Manager = 3,
    }
}
