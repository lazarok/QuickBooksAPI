using Interop.QBFC15;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.GetAllCustomer;
using System.Collections.Generic;

namespace QuickBooks.Infrastructure.QBFC.Customer
{
    public class GetAllCustomerQbfcMessage : IGetAllCustomerQbfcMessage, IQbfcMessage<GetAllCustomerRequest,IEnumerable<GetAllCustomerResponse>>
    {
        public void BuildQueryRequest(GetAllCustomerRequest request, IMsgSetRequest requestMsgSet)
        {
            ICustomerQuery customerQueryRq = requestMsgSet.AppendCustomerQueryRq();         
        }

        public IEnumerable<GetAllCustomerResponse> WalkQueryResponse(IMsgSetResponse responseMsgSet)
        {
            if (responseMsgSet == null) yield break;
            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null) yield break;

            if(responseList.Count > 1)
            {
                var temp = responseList.GetAt(2);
            }

            //if we sent only one request, there is only one response, we'll walk the list for this sample
            for (int i = 0; i < responseList.Count; i++)
            {
                IResponse response = responseList.GetAt(i);
                //check the status code of the response, 0=ok, >0 is warning
                if (response.StatusCode >= 0)
                {
                    //the request-specific response is in the details, make sure we have some
                    if (response.Detail != null)
                    {
                        //make sure the response is the type we're expecting
                        ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                        if (responseType == ENResponseType.rtCustomerQueryRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            ICustomerRetList customerRetList = (ICustomerRetList)response.Detail;
                            for (int j = 0; j < customerRetList.Count; j++)
                            {
                                ICustomerRet customerRet = customerRetList.GetAt(j);
                                var item = WalkCustomerRet(customerRet);
                                if (item != default) yield return item;
                            }                           
                        }
                    }
                }
            }
            yield break;
        }

        GetAllCustomerResponse WalkCustomerRet(ICustomerRet customerRet)
        {
            if (customerRet == null) return default;

            var result = new GetAllCustomerResponse
            {
                Id = customerRet.ListID.GetValue(),
                Name = customerRet.Name.GetValue()
            };
            if (customerRet.CompanyName != null)
            {
                string companyName = customerRet.CompanyName.GetValue();
                result.CompanyName = companyName;
            }                 
            result.EditSequence = customerRet.EditSequence.GetValue();           
            return result;
        }

    }
}
