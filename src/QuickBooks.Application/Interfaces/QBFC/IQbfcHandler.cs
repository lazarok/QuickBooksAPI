namespace QuickBooks.Application.Interfaces.QBFC
{
    public interface IQbfcHandler
    {
        TResponse Execute<TResponse>(IQbfcMessage<TResponse> qbfcMessage);
    }
}
