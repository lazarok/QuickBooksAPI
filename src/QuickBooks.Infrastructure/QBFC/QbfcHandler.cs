using Interop.QBFC15;
using QuickBooks.Application;
using QuickBooks.Application.Interfaces.QBFC;
using System;

namespace QuickBooks.Infrastructure.QBFC
{
    public class QbfcHandler : IQbfcHandler
    {
        public TResponse Execute<TRequest,TResponse>(
            TRequest request, 
            IQbfcMessage<TRequest,TResponse> qbfcMessage)
        {
            bool sessionBegun = false;
            bool connectionOpen = false;
            QBSessionManager sessionManager = null;

            try
            {
                //Create the session Manager object
                sessionManager = new QBSessionManager();

                //Create the message set request object to hold our request
                IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
                requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                qbfcMessage.BuildQueryRequest(request, requestMsgSet);

                //Connect to QuickBooks and begin a session
                sessionManager.OpenConnection("", "QuickBooks API");
                connectionOpen = true;
                sessionManager.BeginSession("", ENOpenMode.omDontCare);
                sessionBegun = true;

                //Send the request and get the response from QuickBooks
                IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

                //End the session and close the connection to QuickBooks
                sessionManager.EndSession();
                sessionBegun = false;
                sessionManager.CloseConnection();
                connectionOpen = false;

                var response = qbfcMessage.WalkQueryResponse(responseMsgSet);
                return response;
            }
            catch (Exception e)
            {
                if (sessionBegun)
                {
                    sessionManager.EndSession();
                }
                if (connectionOpen)
                {
                    sessionManager.CloseConnection();
                }
                throw;
            }
        }
    }
}
