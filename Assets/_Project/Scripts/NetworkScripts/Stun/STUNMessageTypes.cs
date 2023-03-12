using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUN
{
    public enum STUNMessageTypes : ushort
    {
        BindingRequest = 0x0001,
        BindingResponse = 0x0101,
        BindingErrorResponse = 0x0111,
        SharedSecretRequest = 0x0002,
        SharedSecretResponse = 0x0102,
        SharedSecretErrorResponse = 0x0112
    }
}
