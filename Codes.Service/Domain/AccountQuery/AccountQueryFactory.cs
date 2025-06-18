using Codes1.Service.Data;

namespace Codes1.Service.Domain
{
    public class AccountQueryFactory : IAccountQueryFactory
    {
        private readonly Codes1DbContext _context;

        public AccountQueryFactory(Codes1DbContext context)
        {
            _context = context;
        }

        public IAccountQuery GetAccountQuery(int brokerId, int agentId, int clientId)
        {
            if (agentId > 0)
            {
                return new AccountQueryAgent(_context, agentId);
            }

            if (clientId > 0)
            {
                return new AccountQueryClient(_context, clientId);
            }

            if (brokerId > 0)
            {
                return new AccountQueryBroker(_context, brokerId);
            }

            return new AccountQueryAdmin(_context);
        }
    }
}
