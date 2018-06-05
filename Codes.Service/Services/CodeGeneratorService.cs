using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Codes.Service.Services
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        private readonly CodesDbContext _context;

        public CodeGeneratorService(CodesDbContext context)
        {
            _context = context;
        }

        public void GenerateCodes(CodeGeneratorOptions options)
        {
            string output = String.Empty;

            _context.Codes.FromSql("EXECUTE dbo.GenerateCodes {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}",
    
                options.Prefix,             // @LettersStart VARCHAR(50) = '',
                options.Suffix,             // @LettersEnd VARCHAR(50) = '',
                options.Increment,          // @IncrementBy INT = 3,
                options.BrokerId,           // @BrokerId INT,
                options.ClientId,           // @ClientId INT,
                options.CampaignId,         // @CampaignId INT,
                "Virtual",                  // @CodeType VARCHAR(50) = 'Virtual',
                "530",                      // @Issuer VARCHAR(100),
                options.PackageId,          // @PackageId INT = 0,
                options.Padding,            // @PaddingNumber tinyint = 5,
                "0",                        // @PaddingValue CHAR = '0',
                options.StartNumber,        // @StartNumber INT = 0,
                options.EndNumber,          // @EndNumber INT = 50,
                options.FaceValue,          // @HotelPoints INT = 1000,
                0,                          // @CondoRewards INT = 0,
                options.ActivationsPerCode, // @NumberOfUses INT = 1,
                0,                          // @ChargeAmount MONEY = 0,
                options.StartDate,          // @StartDate DATETIME = NULL,
                options.EndDate,            // @EndDate DATETIME = NULL,
                1,                          // @VerifyEmail BIT = 1,
                output                      // @Message VARCHAR(1000) OUTPUT

            ).ToList();
        }
    }
}
