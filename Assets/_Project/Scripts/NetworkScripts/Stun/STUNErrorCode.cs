using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN
{
    public enum STUNErrorCodes : ushort
    {
        BadRequest = 400,
        Unauthorized = 401,
        UnknownAttribute = 420,
        StaleCredentials = 430,
        IntegrityCheckFailure = 431,
        MissingUsername = 432,
        UseTLS = 433,
        ServerError = 500,
        GloablFailure = 600,
    }
}
