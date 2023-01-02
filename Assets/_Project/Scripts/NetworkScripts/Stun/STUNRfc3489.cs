using System.Linq;
using System.Net;
using System.Net.Sockets;
using STUN.Attributes;

namespace STUN.Attributes
{
    public class STUNRfc3489
    {
        public static STUNQueryResult Query(Socket socket, IPEndPoint server, STUNQueryType queryType, int ReceiveTimeout)
        {
            var result = new STUNQueryResult(); // the query result
            result.Socket = socket;
            result.ServerEndPoint = server;
            result.NATType = STUNNATType.Unspecified;
            result.QueryType = queryType;
            var transID = STUNMessage.GenerateTransactionID(); // get a random trans id
            var message = new STUNMessage(STUNMessageTypes.BindingRequest, transID); // create a bind request
            // send the request to server
            socket.SendTo(message.GetBytes(), server);
            // we set result local endpoint after calling SendTo,
            // because if socket is unbound, the system will bind it after SendTo call.
            result.LocalEndPoint = socket.LocalEndPoint as IPEndPoint;

            // wait for response
            var responseBuffer = STUNUtils.Receive(socket, ReceiveTimeout);

            // didn't receive anything
            if (responseBuffer == null)
            {
                result.QueryError = STUNQueryError.Timedout;
                return result;
            }

            // try to parse message
            if (!message.TryParse(responseBuffer))
            {
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }

            // check trans id
            if (!STUNUtils.ByteArrayCompare(message.TransactionID, transID))
            {
                result.QueryError = STUNQueryError.BadTransactionID;
                return result;
            }

            // finds error-code attribute, used in case of binding error
            var errorAttr = message.Attributes.FirstOrDefault(p => p is STUNErrorCodeAttribute)
                as STUNErrorCodeAttribute;

            // if server responsed our request with error
            if (message.MessageType == STUNMessageTypes.BindingErrorResponse)
            {
                if (errorAttr == null)
                {
                    // we count a binding error without error-code attribute as bad response (no?)
                    result.QueryError = STUNQueryError.BadResponse;
                    return result;
                }

                result.QueryError = STUNQueryError.ServerError;
                result.ServerError = errorAttr.Error;
                result.ServerErrorPhrase = errorAttr.Phrase;
                return result;
            }

            // return if receive something else binding response
            if (message.MessageType != STUNMessageTypes.BindingResponse)
            {
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }

            // not used for now.
            var changedAddr =
                message.Attributes.FirstOrDefault(p => p is STUNChangedAddressAttribute) as
                    STUNChangedAddressAttribute;

            // find mapped address attribue in message
            // this attribue should present
            var mappedAddressAttr = message.Attributes.FirstOrDefault(p => p is STUNMappedAddressAttribute)
                as STUNMappedAddressAttribute;
            if (mappedAddressAttr == null)
            {
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }
            else
            {
                result.PublicEndPoint = mappedAddressAttr.EndPoint;
            }

            // stop querying and return the public ip if user just wanted to know public ip
            if (queryType == STUNQueryType.PublicIP)
            {
                result.QueryError = STUNQueryError.Success;
                return result;
            }

            // if our local ip and port equals to mapped address
            if (mappedAddressAttr.EndPoint.Equals(socket.LocalEndPoint))
            {
                // we send to a binding request again but with change-request attribute
                // that tells to server to response us with different endpoint
                message = new STUNMessage(STUNMessageTypes.BindingRequest, transID);
                message.Attributes.Add(new STUNChangeRequestAttribute(true, true));

                socket.SendTo(message.GetBytes(), server);
                responseBuffer = STUNUtils.Receive(socket, ReceiveTimeout);

                // if we didnt receive a response
                if (responseBuffer == null)
                {
                    result.QueryError = STUNQueryError.Success;
                    result.NATType = STUNNATType.SymmetricUDPFirewall;
                    return result;
                }

                if (!message.TryParse(responseBuffer))
                {
                    result.QueryError = STUNQueryError.BadResponse;
                    return result;
                }

                if (!STUNUtils.ByteArrayCompare(message.TransactionID, transID))
                {
                    result.QueryError = STUNQueryError.BadTransactionID;
                    return result;
                }

                if (message.MessageType == STUNMessageTypes.BindingResponse)
                {
                    result.QueryError = STUNQueryError.Success;
                    result.NATType = STUNNATType.OpenInternet;
                    return result;
                }

                if (message.MessageType == STUNMessageTypes.BindingErrorResponse)
                {
                    errorAttr =
                        message.Attributes.FirstOrDefault(p => p is STUNErrorCodeAttribute) as
                            STUNErrorCodeAttribute;

                    if (errorAttr == null)
                    {
                        result.QueryError = STUNQueryError.BadResponse;
                        return result;
                    }

                    result.QueryError = STUNQueryError.ServerError;
                    result.ServerError = errorAttr.Error;
                    result.ServerErrorPhrase = errorAttr.Phrase;
                    return result;
                }

                // the message type is wrong
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }

            message = new STUNMessage(STUNMessageTypes.BindingRequest, transID);
            message.Attributes.Add(new STUNChangeRequestAttribute(true, true));

            var testmsg = new STUNMessage(STUNMessageTypes.BindingRequest, null);
            testmsg.Parse(message.GetBytes());

            socket.SendTo(message.GetBytes(), server);

            responseBuffer = STUNUtils.Receive(socket, ReceiveTimeout);

            if (responseBuffer != null)
            {
                if (!message.TryParse(responseBuffer))
                {
                    result.QueryError = STUNQueryError.BadResponse;
                    return result;
                }

                if (!STUNUtils.ByteArrayCompare(message.TransactionID, transID))
                {
                    result.QueryError = STUNQueryError.BadTransactionID;
                    return result;
                }

                if (message.MessageType == STUNMessageTypes.BindingResponse)
                {
                    result.QueryError = STUNQueryError.Success;
                    result.NATType = STUNNATType.FullCone;
                    return result;
                }

                if (message.MessageType == STUNMessageTypes.BindingErrorResponse)
                {
                    errorAttr =
                        message.Attributes.FirstOrDefault(p => p is STUNErrorCodeAttribute) as
                            STUNErrorCodeAttribute;

                    if (errorAttr == null)
                    {
                        result.QueryError = STUNQueryError.BadResponse;
                        return result;
                    }

                    result.QueryError = STUNQueryError.ServerError;
                    result.ServerError = errorAttr.Error;
                    result.ServerErrorPhrase = errorAttr.Phrase;
                    return result;
                }

                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }

            // if user only wanted to know the NAT is open or not
            if (queryType == STUNQueryType.OpenNAT)
            {
                result.QueryError = STUNQueryError.Success;
                result.NATType = STUNNATType.Unspecified;
                return result;
            }

            // we now need changed-address attribute
            // because we send our request to this address instead of the first server address
            if (changedAddr == null)
            {
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }
            else
            {
                server = changedAddr.EndPoint;
            }

            message = new STUNMessage(STUNMessageTypes.BindingRequest, transID);
            socket.SendTo(message.GetBytes(), server);

            responseBuffer = STUNUtils.Receive(socket, ReceiveTimeout);

            if (responseBuffer == null)
            {
                result.QueryError = STUNQueryError.Timedout;
                return result;
            }

            if (!message.TryParse(responseBuffer))
            {
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }

            if (!STUNUtils.ByteArrayCompare(message.TransactionID, transID))
            {
                result.QueryError = STUNQueryError.BadTransactionID;
                return result;
            }

            errorAttr =
                message.Attributes.FirstOrDefault(p => p is STUNErrorCodeAttribute) as STUNErrorCodeAttribute;

            if (message.MessageType == STUNMessageTypes.BindingErrorResponse)
            {
                if (errorAttr == null)
                {
                    result.QueryError = STUNQueryError.BadResponse;
                    return result;
                }

                result.QueryError = STUNQueryError.ServerError;
                result.ServerError = errorAttr.Error;
                result.ServerErrorPhrase = errorAttr.Phrase;
                return result;
            }

            if (message.MessageType != STUNMessageTypes.BindingResponse)
            {
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }

            mappedAddressAttr = message.Attributes.FirstOrDefault(p => p is STUNMappedAddressAttribute)
                as STUNMappedAddressAttribute;

            if (mappedAddressAttr == null)
            {
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }

            if (!mappedAddressAttr.EndPoint.Equals(result.PublicEndPoint))
            {
                result.QueryError = STUNQueryError.Success;
                result.NATType = STUNNATType.Symmetric;
                result.PublicEndPoint = null;
                return result;
            }

            message = new STUNMessage(STUNMessageTypes.BindingRequest, transID);
            message.Attributes.Add(new STUNChangeRequestAttribute(false, true)); // change port but not ip

            socket.SendTo(message.GetBytes(), server);

            responseBuffer = STUNUtils.Receive(socket, ReceiveTimeout);

            if (responseBuffer == null)
            {
                result.QueryError = STUNQueryError.Success;
                result.NATType = STUNNATType.PortRestricted;
                return result;
            }

            if (!message.TryParse(responseBuffer))
            {
                result.QueryError = STUNQueryError.Timedout;
                return result;
            }

            if (!STUNUtils.ByteArrayCompare(message.TransactionID, transID))
            {
                result.QueryError = STUNQueryError.BadTransactionID;
                return result;
            }

            errorAttr = message.Attributes.FirstOrDefault(p => p is STUNErrorCodeAttribute)
                as STUNErrorCodeAttribute;

            if (message.MessageType == STUNMessageTypes.BindingErrorResponse)
            {
                if (errorAttr == null)
                {
                    result.QueryError = STUNQueryError.BadResponse;
                    return result;
                }

                result.QueryError = STUNQueryError.ServerError;
                result.ServerError = errorAttr.Error;
                result.ServerErrorPhrase = errorAttr.Phrase;
                return result;
            }

            if (message.MessageType != STUNMessageTypes.BindingResponse)
            {
                result.QueryError = STUNQueryError.BadResponse;
                return result;
            }

            result.QueryError = STUNQueryError.Success;
            result.NATType = STUNNATType.Restricted;
            return result;
        }
    }
}