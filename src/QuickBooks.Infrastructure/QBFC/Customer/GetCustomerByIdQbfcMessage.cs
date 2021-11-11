using Interop.QBFC15;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.GetCustomerById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Infrastructure.QBFC.Customer
{
    public class GetCustomerByIdQbfcMessage : 
        IGetCustomerByIdQbfcMessage, IQbfcMessage<GetCustomerByIdRequest,GetCustomerByIdResponse>
    {
        public void BuildQueryRequest(GetCustomerByIdRequest request, IMsgSetRequest requestMsgSet)
        {
            ICustomerQuery CustomerQueryRq = requestMsgSet.AppendCustomerQueryRq();
            //Set attributes
            //Set field value for metaData
            //CustomerQueryRq.metaData.SetValue(ENmetaData.mdNoMetaData);
            //Set field value for iterator
            //CustomerQueryRq.iterator.SetValue(ENiterator.itContinue);
            CustomerQueryRq.ORCustomerListQuery.ListIDList.Add(request.Id);
        }

        public GetCustomerByIdResponse WalkQueryResponse(IMsgSetResponse responseMsgSet)
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
                            if(customerRetList.Count > 1)
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

        GetCustomerByIdResponse WalkCustomerRet(ICustomerRet customerRet)
        {
            if (customerRet == null) return default;

            var result = new GetCustomerByIdResponse();

            //Get value of ListID
            string listID7497 = customerRet.ListID.GetValue();
            result.Id = listID7497;

            //Get value of Name
            string name7501 = customerRet.Name.GetValue();
            result.Name = name7501;

            //Get value of Phone
            if (customerRet.Phone != null)
            {
                string phone7558 = (string)customerRet.Phone.GetValue();
                result.Phone = phone7558;
            }
            return result;
        }
    }
}
