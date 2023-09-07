using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ApiWebToken.Helpers;
public class GlobalVerbRoleRequirement: IAuthorizationRequirement
{
    public bool IsAllowed(string role, string verb)
    {
        //Allow all verbs if user is "admin"
        if (string.Equals("Administrador", role, StringComparison.OrdinalIgnoreCase)) return true;
        if (string.Equals("Gerente", role, StringComparison.OrdinalIgnoreCase)) return true;
        //Allow the "GET" verb if user is "support"
        if(string.Equals("empleado", role, StringComparison.OrdinalIgnoreCase) && string.Equals("GET", verb, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        };
        if(string.Equals("camper", role, StringComparison.OrdinalIgnoreCase) && string.Equals("GET", verb, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        };
        //... add other rules as you like
        return false;
    }
}