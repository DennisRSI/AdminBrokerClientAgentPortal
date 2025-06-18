using Codes1.Service.Data;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codes1.Service.Services
{
    public class CodeGenerator1Service : ICodeGenerator1Service
    {
        private readonly Codes1DbContext _context;

        public CodeGenerator1Service(Codes1DbContext context)
        {
            _context = context;
        }

        public async Task<(bool isSuccess, string message)> CheckAvailableCodes(int startNumber, int numberOfCodes, int skipAmount, string preAlpha, string postAlpha = "", int padding = 1)
        {
            (bool isSuccess, string message) returnObj = (false, "Not Implemented");

            try
            {
                List<string> potentialCodes = new();
                
                if (postAlpha == null)
                    postAlpha = "";

                int currentNumber = startNumber;
                for(int i = 1; i <= numberOfCodes; i++)
                {
                    potentialCodes.Add(preAlpha + currentNumber.ToString().PadLeft(padding) + postAlpha);
                    currentNumber += skipAmount;

                }

                if (await _context.Codes.AnyAsync(a => potentialCodes.Contains(a.Code)))
                    returnObj = (false, "Code(s) not created. Full/Partial code range in use");
                else
                    returnObj = (true, "Success");
            }
            catch (Exception ex)
            {
                returnObj = (false, ex.Message);
            }

            return returnObj;
        }

        public void GenerateCodes(CodeGeneratorOptions options)
        {
            string output = String.Empty;

            _context.Codes.FromSqlRaw("EXECUTE dbo.GenerateCodes {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}",
    
                options.Prefix,             // @LettersStart VARCHAR(50) = '',
                options.Suffix,             // @LettersEnd VARCHAR(50) = '',
                options.Increment,          // @IncrementBy INT = 3,
                options.BrokerId,           // @BrokerId INT,
                options.ClientId,           // @ClientId INT,
                options.CampaignId,         // @CampaignId INT,
                options.CampaignType,       // @CodeType VARCHAR(50) = 'Virtual',
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
