using Interop.QBFC15;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.GetCustomerByName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Infrastructure.QBFC.Customer
{
    public class GetCustomerByNameQbfcMessage :
       IGetCustomerByNameQbfcMessage, IQbfcMessage<GetCustomerByNameRequest, GetCustomerByNameResponse>
    {
        public void BuildQueryRequest(GetCustomerByNameRequest request, IMsgSetRequest requestMsgSet)
        {
            ICustomerQuery customerQueryRq = requestMsgSet.AppendCustomerQueryRq();
            customerQueryRq.ORCustomerListQuery.CustomerListFilter
                .ORNameFilter.NameRangeFilter.FromName.SetValue(request.Name);

            customerQueryRq.ORCustomerListQuery.CustomerListFilter
                .ORNameFilter.NameRangeFilter.ToName.SetValue(request.Name);
        }

        public GetCustomerByNameResponse WalkQueryResponse(IMsgSetResponse responseMsgSet)
        {
            if (responseMsgSet == null) return default;
            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null) return default;

            if (responseList.Count > 1)
            {
                var temp = responseList.GetAt(1);
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

                            // temp
                            if (customerRetList.Count > 1)
                            {
                                var temp = customerRetList.GetAt(1);
                            }

                            if (customerRetList.Count == 1)
                            {
                                ICustomerRet customerRet = customerRetList.GetAt(0);
                                var item = WalkCustomerRet(customerRet);
                                return item;
                            }
                        }
                    }
                }
            }

            return default;
        }

        GetCustomerByNameResponse WalkCustomerRet(ICustomerRet customerRet)
        {
            if (customerRet == null) return default;

            var result = new GetCustomerByNameResponse
            {
                Id = customerRet.ListID?.GetValue(),
                Name = customerRet.Name?.GetValue(),
                CompanyName = customerRet.CompanyName?.GetValue(),
                Contact = customerRet.Contact?.GetValue(),
                Email = customerRet.Email?.GetValue(),
                Notes = customerRet.Notes?.GetValue(),
                Phone = customerRet.Phone?.GetValue(),
                CreatedAt = customerRet.TimeCreated.GetValue(),
                IsActive = customerRet.IsActive.GetValue(),
                EditSequence = customerRet.EditSequence.GetValue()
            };
            return result;
        }
    }
}
