using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Domain.Users.Models
{
    public class PredefinedPermissions
    {
        public static IEnumerable<PredefinedPermission> GetPredefinedPermissions()
        {
            return new List<PredefinedPermission>()
            {
            };
        }
    }
}
