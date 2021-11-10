using Interop.QBFC15;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Application.Interfaces.QBFC
{
    public interface IQbfcMessage<TResponse>
    {
        void BuildQueryRequest(IMsgSetRequest requestMsgSet);
        TResponse WalkQueryResponse(IMsgSetResponse responseMsgSet);
    }
}
