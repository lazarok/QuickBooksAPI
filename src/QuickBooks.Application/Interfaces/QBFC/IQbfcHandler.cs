namespace QuickBooks.Application.Interfaces.QBFC
{
    public interface IQbfcHandler
    {
        TResponse Execute<TRequest,TResponse>(TRequest request, IQbfcMessage<TRequest,TResponse> qbfcMessage);
    }
}
