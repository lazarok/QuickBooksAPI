using Interop.QBFC15;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.UpdateCustomer;

namespace QuickBooks.Infrastructure.QBFC.Customer
{
    public class UpdateCustomerQbfcMessage : IUpdateCustomerQbfcMessage, IQbfcMessage<UpdateCustomerRequest, UpdateCustomerResponse>
    {
        public void BuildQueryRequest(UpdateCustomerRequest request, IMsgSetRequest requestMsgSet)
        {
            ICustomerMod customerModRq = requestMsgSet.AppendCustomerModRq();
            customerModRq.ListID.SetValue(request.Id);
            customerModRq.EditSequence.SetValue(request.EditSequence);

            if (request.CompanyName != default)
                customerModRq.CompanyName.SetValue(request.CompanyName);

            if (request.Contact != default)
                customerModRq.Contact.SetValue(request.Contact);

            if (request.Email != default)
                customerModRq.Email.SetValue(request.Email);

            if (request.IsActive != default)
                customerModRq.IsActive.SetValue(request.IsActive.Value);

            if (request.Name != default)
                customerModRq.Name.SetValue(request.Name);

            if (request.Notes != default)
                customerModRq.Notes.SetValue(request.Notes);

            if (request.Phone != default)
                customerModRq.Phone.SetValue(request.Phone);
        }

        public UpdateCustomerResponse WalkQueryResponse(IMsgSetResponse responseMsgSet)
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
                        if (responseType == ENResponseType.rtCustomerModRs)
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

        private UpdateCustomerResponse WalkCustomerRet(ICustomerRet customerRet)
        {
            if (customerRet == null) return default;

            var result = new UpdateCustomerResponse
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
