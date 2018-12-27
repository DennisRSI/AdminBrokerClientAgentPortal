using Codes.Service.Data;

namespace Codes.Service.Domain
{
    public class AccountQueryFactory : IAccountQueryFactory
    {
        private readonly CodesDbContext _context;

        public AccountQueryFactory(CodesDbContext context)
        {
            _context = context;
        }

        public IAccountQuery GetAccountQuery(int brokerId, int agentId, int clientId)
        {
            if (brokerId > 0)
            {
                return new AccountQueryBroker(_context, brokerId);
            }

            if (agentId > 0)
            {
                return new AccountQueryAgent(_context, agentId);
            }

            if (clientId > 0)
            {
                return new AccountQueryClient(_context, clientId);
            }

            return new AccountQueryAdmin(_context);
        }
    }
}
