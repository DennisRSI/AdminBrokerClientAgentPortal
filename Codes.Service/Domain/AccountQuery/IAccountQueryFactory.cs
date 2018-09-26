namespace Codes.Service.Domain
{
    public interface IAccountQueryFactory
    {
        IAccountQuery GetAccountQuery(int brokerId, int agentId, int clientId);
    }
}
