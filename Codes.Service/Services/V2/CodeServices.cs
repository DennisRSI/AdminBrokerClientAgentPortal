using Codes.Service.ViewModels.V2;
using Codes1.Service.Data;
using Codes.Service.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Booking.Service.Data;
using System.Data;
using Codes1.Service.Interfaces.V2;

namespace Codes1.Service.Services.V2
{
    public class CodeServices : ICodeServices
    {
        private readonly Codes1DbContext _context;
        private readonly BookingDbContext _bookingContext;
        public CodeServices(Codes1DbContext context, BookingDbContext bookingContext)
        {
            _context = context;
            _bookingContext = bookingContext;
        }

        public async Task<Codes1.Service.ViewModels.CommissionResultViewModel> ReportCommissionAsync(DateTime checkoutStartDate, DateTime checkoutEndDate, 
            int startRowIndex = 0, int numberOfRows = 0, int? brokerId = null, int? agentId = null, int? clientId = null, string paymentStatus = null)
        {
            if (brokerId == 0)
                brokerId = null;

            if (agentId == 0)
                agentId = null;

            if (clientId == 0)
                clientId = null;

            Codes1.Service.ViewModels.CommissionResultViewModel model = new Codes1.Service.ViewModels.CommissionResultViewModel
            {
                QueryType = "broker",
                CheckoutStartDate = checkoutStartDate,
                CheckoutEndDate = checkoutEndDate,
                Tables = new List<Codes1.Service.ViewModels.CommissionResultTableViewModel>()
            };

            try
            {
                var sql = @"SELECT brk.BrokerFirstName, brk.BrokerLastName, brk.CompanyName as BrokerCompanyName, b.BookingId, b.InternetPrice, b.YouPayPrice, 
                            c.MemberSavings, c.CompanyBucketCommission, 
                            COALESCE(cl.ContactFirstName, '') as ClientFirstName, 
                                COALESCE(cl.ContactLastName, '') as ClientLastName, 
                                COALESCE(cl.CompanyName, '') as ClientCompanyName,
                            COALESCE(ag.AgentFirstName, '') as PrimaryAgentFirstName, 
                                COALESCE(ag.AgentLastName, '') as PrimaryAgentLastName, 
                                COALESCE(ag.CompanyName, '') as PrimaryAgentCompanyName , 
                            COALESCE(ag1.AgentFirstName, '') as ClientAgentFirstName, 
                                COALESCE(ag1.AgentLastName, '') as ClientAgentLastName, 
                                COALESCE(ag1.CompanyName, '') as ClientAgentCompanyName,
                            b.InternetPrice, c.MemberSavings, c.CompanyBucketCommission, c.ClientCommission, c.AgentBucketCommission
                            FROM BookingCommissions c INNER JOIN 
                            RSIBookings.dbo.Bookings b on c.BookingId = b.BookingId INNER JOIN
                            BookingCommissionBreakdowns br1 on c.BookingId = br1.BookingId and br1.PersonType = 'Broker' INNER JOIN
                            Brokers brk on br1.Id = brk.BrokerId LEFT OUTER JOIN 
                            BookingCommissionBreakdowns br2 on c.BookingId = br2.BookingId and br2.PersonType = 'Client' LEFT OUTER JOIN 
                            Clients cl on br2.Id = cl.ClientId LEFT OUTER JOIN
                            BookingCommissionBreakdowns br3 on c.BookingId = br3.BookingId and br3.PersonType = 'Primary Agent' LEFT OUTER JOIN 
                            Agents ag on br3.Id = ag.AgentId LEFT OUTER JOIN
                            BookingCommissionBreakdowns br4 on c.BookingId = br4.BookingId and br4.PersonType = 'Client Agent' LEFT OUTER JOIN 
                            Agents ag1 on br4.Id = ag1.AgentId";

                var sqlWhere = @" WHERE c.MemberSavings > 0 AND b.Status = 'CONFIRMED'";

                int id = 0;

                if (agentId.GetValueOrDefault(0) > 0  || clientId.GetValueOrDefault(0) > 0 || brokerId.GetValueOrDefault(0) > 0)
                {
                    if (agentId > 0)
                    {
                        sql += @" INNER JOIN BookingCommissionBreakdowns br5 on c.BookingId = br5.BookingId";

                        sqlWhere += $" AND br5.Id = {agentId} AND (br5.PersonType = 'Agent' OR br.PersonType = 'Client Agent' OR br.PersonType = 'Primary Agent')";
                        id = agentId.GetValueOrDefault(0);
                    }
                    else if (clientId > 0)
                    {
                        sqlWhere += $" AND br2.Id = {clientId} AND (br2.PersonType = 'Client')";
                        id = clientId.GetValueOrDefault(0);
                    }
                    else if (brokerId > 0)
                    {
                        sqlWhere += $" AND br1.Id = {brokerId} AND (br1.PersonType = 'Broker')";
                        id = brokerId.GetValueOrDefault(0);
                    }
                }
                
                sql += sqlWhere;

                /*
                 * var results = _context.BookingCommissions.FromSql(sql)
                    .Include(i => i.BookingCommissionBreakdowns)
                    .Select(s => new Codes1.Service.ViewModels.CommissionResultItemViewModel
                    {
                        AccountName = "Broker",
                        CommissionEarned = s.CompanyBucketCommission,
                     CommissionType = "Broker Bucket",
                      FirstName = s.("BrokerCompanyName"]
                    });*/

                /*if (results.Count() > 0)
                {
                    Codes1.Service.ViewModels.CommissionResultTableViewModel result = new Codes1.Service.ViewModels.CommissionResultTableViewModel
                    {
                        ReportGroupName = "Client",
                        AccountType = "Broker",
                        Items = new List<Codes1.Service.ViewModels.CommissionResultItemViewModel>(),
                        TotalInternetPrice = 0,
                        TotalYouPayPrice = 0,
                        TotalMemberSavings = 0,
                        TotalCommissionEarned = 0
                    };

                    model.Tables.Add(result);

                    Codes1.Service.ViewModels.CommissionResultItemViewModel company = null;

                    foreach(var row in results)
                    {
                        company = row;
                    }
                }*/
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public async Task<DashboardViewModel> GetDashboardAsync(int brokerId = 0, int clientId = 0, int agentId = 0)
        {
            DashboardViewModel model = new DashboardViewModel();

            try
            {
                var cards = _context.Codes.Include(cp => cp.Campaign).Where(w => w.Issuer == "530");
                var sql = @"SELECT * FROM BookingCommissions c INNER JOIN 
                        RSIBookings.dbo.Bookings b on c.BookingId = b.BookingId
                        INNER JOIN BookingCommissionBreakdowns br1 on c.BookingId = br1.BookingId and br1.PersonType = 'Broker'";

                var sqlWhere = @" WHERE c.MemberSavings > 0 AND b.Status = 'CONFIRMED'";

                int id = 0;

                if (agentId > 0 || clientId > 0 || brokerId > 0)
                {
                    sql += @" INNER JOIN BookingCommissionBreakdowns br on c.BookingId = br.BookingId";

                    if (agentId > 0)
                    {
                        sql += $" AND br.Id = {agentId} AND (br.PersonType = 'Agent' OR br.PersonType = 'Client Agent' OR br.PersonType = 'Primary Agent')";
                        id = agentId;
                    }
                    else if (clientId > 0)
                    {
                        sql += $" AND br.Id = {clientId} AND (br.PersonType = 'Client')";
                        id = clientId;
                    }
                    else if (brokerId > 0)
                    {
                        sql += $" AND br.Id = {brokerId} AND (br.PersonType = 'Broker')";
                        id = brokerId;
                    }

                }
                

                sql += sqlWhere;

                //int id = agentId > 0 ? agentId : clientId > 0 ? clientId : brokerId > 0 ? brokerId : 0;
                var commissions = _context.BookingCommissions.FromSqlRaw(sql);
                                   
                if (agentId > 0)
                {
                    model.PhysicalCardsPurchased = await cards.Where(c => c.Campaign.CampaignType == "Physical" && c.Campaign.CampaignAgents.Any(s => s.AgentId == agentId)).CountAsync();
                    model.VirtualCardsPurchased = await cards.Where(c => c.Campaign.CampaignType == "Virtual" && c.Campaign.CampaignAgents.Any(s => s.AgentId == agentId)).CountAsync();

                    model.PhysicalCardsActivated = await (from c in cards
                                                    join a in _context.CodeActivities on c.CodeId equals a.CodeId
                                                    where c.Campaign.CampaignType == "Physical" && c.Campaign.CampaignAgents.Any(a => a.AgentId == agentId)
                                                    select c).CountAsync();

                    model.VirtualCardsActivated = await (from c in cards
                                                    join a in _context.CodeActivities on c.CodeId equals a.CodeId
                                                    where c.Campaign.CampaignType == "Virtual" && c.Campaign.CampaignAgents.Any(a => a.AgentId == agentId)
                                                    select c).CountAsync();
                    model.TotalCommissionOwedToCompay = await commissions.SumAsync(x => x.AgentBucketCommission);
                    model.TotalCommissionOwedToPerson = await commissions.Where(w => w.BrokerPaidDate == null).SelectMany(l => l.BookingCommissionBreakdowns)
                        .Where(w => w.Id == agentId && 
                        (w.PersonType == "Agent" || w.PersonType == "Primary Agent" || w.PersonType == "Client Agent"))
                        .SumAsync(s => s.Commission);
                }
                else if (clientId > 0)
                {
                    model.PhysicalCardsPurchased = await cards.Where(c => c.Campaign.CampaignType == "Physical" && c.Campaign.ClientId == clientId).CountAsync();
                    model.VirtualCardsPurchased = await cards.Where(c => c.Campaign.CampaignType == "Virtual" && c.Campaign.ClientId == clientId).CountAsync();

                    model.PhysicalCardsActivated = await (from c in cards
                                                    join a in _context.CodeActivities on c.CodeId equals a.CodeId
                                                    where c.Campaign.CampaignType == "Physical" && c.Campaign.ClientId == clientId
                                                    select c).CountAsync();

                    model.VirtualCardsActivated = await (from c in cards
                                                   join a in _context.CodeActivities on c.CodeId equals a.CodeId
                                                   where c.Campaign.CampaignType == "Virtual" && c.Campaign.ClientId == clientId
                                                   select c).CountAsync();
                    model.TotalCommissionOwedToPerson = await commissions.Where(w => w.BrokerPaidDate == null).SelectMany(l => l.BookingCommissionBreakdowns)
                        .Where(w => w.Id == clientId &&
                        w.PersonType == "Client")
                        .SumAsync(s => s.Commission);

                    model.TotalCommissionOwedToCompay = model.TotalCommissionOwedToPerson;
                }
                else if(brokerId > 0)
                {
                    model.PhysicalCardsPurchased = await cards.Where(c => c.Campaign.CampaignType == "Physical" && c.IssuerReference == brokerId.ToString()).CountAsync();
                    model.VirtualCardsPurchased = await cards.Where(c => c.Campaign.CampaignType == "Virtual" && c.IssuerReference == brokerId.ToString()).CountAsync();

                    model.PhysicalCardsActivated = await (from c in cards
                                                    join a in _context.CodeActivities on c.CodeId equals a.CodeId
                                                    where c.Campaign.CampaignType == "Physical" && c.IssuerReference == brokerId.ToString()
                                                    select c).CountAsync();

                    model.VirtualCardsActivated = await (from c in cards
                                                   join a in _context.CodeActivities on c.CodeId equals a.CodeId
                                                   where c.Campaign.CampaignType == "Virtual" && c.IssuerReference == brokerId.ToString()
                                                   select c).CountAsync();

                    model.TotalCommissionOwedToCompay = await commissions.Where(w => w.BrokerPaidDate == null).SumAsync(x => x.CompanyBucketCommission);
                    model.TotalCommissionOwedToPerson = await commissions.Where(w => w.BrokerPaidDate == null).SelectMany(l => l.BookingCommissionBreakdowns)
                        .Where(w => w.Id == brokerId &&
                        w.PersonType == "Broker")
                        .SumAsync(s => s.Commission);

                    model.HotelCommissionOwedToCompany = await commissions.Where(w => w.BrokerPaidDate == null && w.BookingType == "Hotel").SelectMany(l => l.BookingCommissionBreakdowns).SumAsync(s => s.Commission);
                    model.HotelCommissionPaidToCompany = await commissions.Where(w => w.BrokerPaidDate != null && w.BookingType == "Hotel").SelectMany(l => l.BookingCommissionBreakdowns).SumAsync(s => s.Commission);
                    model.CondoCommissionOwedToCompany = await commissions.Where(w => w.BrokerPaidDate == null && w.BookingType == "Condo").SelectMany(l => l.BookingCommissionBreakdowns).SumAsync(s => s.Commission);
                    model.CondoCommissionPaidToCompany = await commissions.Where(w => w.BrokerPaidDate != null && w.BookingType == "Condo").SelectMany(l => l.BookingCommissionBreakdowns).SumAsync(s => s.Commission);
                }
                else
                {
                    model.PhysicalCardsPurchased = await cards.Where(c => c.Campaign.CampaignType == "Physical").CountAsync();
                    model.VirtualCardsPurchased = await cards.Where(c => c.Campaign.CampaignType == "Virtual").CountAsync();

                    model.PhysicalCardsActivated = await (from c in cards
                                                    join a in _context.CodeActivities on c.CodeId equals a.CodeId
                                                    where c.Campaign.CampaignType == "Physical"
                                                    select c).CountAsync();

                    model.VirtualCardsActivated = await (from c in cards
                                                   join a in _context.CodeActivities on c.CodeId equals a.CodeId
                                                   where c.Campaign.CampaignType == "Virtual"
                                                   select c).CountAsync();

                    model.TotalCommissionOwedToCompay = await commissions.SumAsync(x => x.CompanyBucketCommission);
                    model.TotalCommissionOwedToPerson = await commissions.Where(w => w.BrokerPaidDate == null).SelectMany(l => l.BookingCommissionBreakdowns)
                        .Where(w => w.Id == agentId &&
                        w.PersonType == "Broker")
                        .SumAsync(s => s.Commission);
                }

                model.TotalMemberSavings =  await commissions.Where(w => w.BrokerPaidDate == null).SumAsync(s => s.MemberSavings);
                //model.TotalCommissionPaid = await commissions.Where(w => w.BrokerPaidDate != null).SelectMany(l => l.BookingCommissionBreakdowns).SumAsync(s => s.Commission);
                
               
                model.CondoSavings = await commissions.Where(w => w.BrokerPaidDate == null && w.BookingType == "Condo").SumAsync(s => s.MemberSavings);
                model.HotelSavings = await commissions.Where(w => w.BrokerPaidDate == null && w.BookingType == "Hotel").SumAsync(s => s.MemberSavings);
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }
    }
}
