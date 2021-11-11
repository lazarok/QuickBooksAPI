using Interop.QBFC15;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.AddCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Infrastructure.QBFC.Customer
{
    class AddCustomerQbfcMessage : IAddCustomerQbfcMessage, IQbfcMessage<AddCustomerRequest, AddCustomerResponse>
    {
        public void BuildQueryRequest(AddCustomerRequest request, IMsgSetRequest requestMsgSet)
        {
            ICustomerAdd customerAddRq = requestMsgSet.AppendCustomerAddRq();

            customerAddRq.Name.SetValue(request.Name);
            customerAddRq.CompanyName.SetValue(request.CompanyName);
            customerAddRq.Phone.SetValue(request.Phone);
            customerAddRq.Email.SetValue(request.Email);
            customerAddRq.Contact.SetValue(request.Contact);
            customerAddRq.Notes.SetValue(request.Notes);
            customerAddRq.IsActive.SetValue(true);
        }

        public AddCustomerResponse WalkQueryResponse(IMsgSetResponse responseMsgSet)
        {
            if (responseMsgSet == null) return default;
            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null) return default;
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
                        if (responseType == ENResponseType.rtCustomerAddRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            ICustomerRet customerRet = (ICustomerRet)response.Detail;
                            var item = WalkCustomerRet(customerRet);
                            return item;
                        }
                    }
                }
            }

            return default;
        }

        private AddCustomerResponse WalkCustomerRet(ICustomerRet customerRet)
        {
            if (customerRet == null) return default;

            var result = new AddCustomerResponse
            {
                Id = customerRet.ListID?.GetValue(),
                Name = customerRet.Name?.GetValue(),
                CompanyName = customerRet.CompanyName?.GetValue(),
                Contact = customerRet.Contact?.GetValue(),
                Email = customerRet.Email?.GetValue(),
                Notes = customerRet.Notes?.GetValue(),
                Phone = customerRet.Phone?.GetValue(),
                CreatedAt = customerRet.TimeCreated.GetValue(),
                IsActive = customerRet.IsActive.GetValue()
            };

            return result;
        }
    }
}
