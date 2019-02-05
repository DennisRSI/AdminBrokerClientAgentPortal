using Excel.Service.Models._ViewModel;
using Excel.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Excel.Service.Services
{
    public class ExportService : IExportService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private List<AdjustmentViewModel> _adjustments = new List<AdjustmentViewModel>();
        private List<InventoryReservationViewModel> _inventoryReservations = new List<InventoryReservationViewModel>();
        private List<MerchantInventoryReservationViewModel> _merchantInventoryReservations = new List<MerchantInventoryReservationViewModel>();

        public ExportService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private async Task<bool> ParseInventoryRow(IRow row, int cellCount)
        {
            InventoryReservationViewModel model = new InventoryReservationViewModel();

            for (int j = row.FirstCellNum; j < cellCount; j++)
            {
                if (row.GetCell(j) != null)
                {
                    switch (j)
                    {
                        case 0:
                            model.Affiliate = row.GetCell(j).ToString();
                            break;
                        case 1:
                            model.Site = row.GetCell(j).ToString();
                            break;
                        case 2:
                            model.Confirmation = row.GetCell(j).ToString();
                            break;
                        case 3:
                            model.Property = row.GetCell(j).ToString();
                            break;
                        case 4:
                            model.Guest = row.GetCell(j).ToString();
                            break;
                        case 5:
                            DateTime dt = new DateTime();
                            DateTime.TryParse(row.GetCell(j).ToString(), out dt);
                            model.CheckOut = dt;
                            break;
                        case 6:
                            DateTime dt1 = new DateTime();
                            DateTime.TryParse(row.GetCell(j).ToString(), out dt1);
                            model.Booked = dt1;
                            break;
                        case 7:
                            model.ReservationStatus = row.GetCell(j).ToString();
                            break;
                        case 8:
                            int roomNights = 0;
                            int.TryParse(row.GetCell(j).ToString(), out roomNights);
                            model.RooomNights = roomNights;
                            break;
                        case 9:
                            decimal flatAmt = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out flatAmt);
                            model.FlatAmount = flatAmt;
                            break;
                        case 10:
                            decimal revenu = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out revenu);
                            model.RoomRevenue = revenu;
                            break;
                        case 11:
                            decimal commmissionRecieved = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out commmissionRecieved);
                            model.CommissionReceived = commmissionRecieved;
                            break;
                        case 12:
                            decimal commissionProcessingFee = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out commissionProcessingFee);
                            model.CommissionProcessingFee = commissionProcessingFee;
                            break;
                        case 13:
                            decimal collectionExpense = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out collectionExpense);
                            model.CollectionExpense = collectionExpense;
                            break;
                        case 14:
                            decimal callCenterFee = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out callCenterFee);
                            model.ARNCallCenterFee = callCenterFee;
                            break;
                        case 15:
                            decimal netCommission = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out netCommission);
                            model.NetCommission = netCommission;
                            break;
                        case 16:
                            decimal subAffiliateCommission;
                            decimal.TryParse(row.GetCell(j).ToString(), out subAffiliateCommission);
                            model.SubAffiliateCommission = subAffiliateCommission;
                            break;
                        case 17:
                            decimal netCommissionAfterSubAffiliate;
                            decimal.TryParse(row.GetCell(j).ToString(), out netCommissionAfterSubAffiliate);
                            model.NetCommissionAfterSubAffiliate = netCommissionAfterSubAffiliate;
                            break;
                        case 18:
                            model.CID = row.GetCell(j).ToString();
                            break;
                        case 19:
                            model.ReservationId = row.GetCell(j).ToString();
                            break;
                        case 20:
                            model.RegistrationId = row.GetCell(j).ToString();
                            break;
                        case 21:
                            model.RegistrationName = row.GetCell(j).ToString();
                            break;
                    }
                }
            }

            _inventoryReservations.Add(model);

            return await Task.FromResult<bool>(true);
        }

        private async Task<bool> ParseMerchantInventoryRow(IRow row, int cellCount)
        {
            MerchantInventoryReservationViewModel model = new MerchantInventoryReservationViewModel();

            for (int j = row.FirstCellNum; j < cellCount; j++)
            {
                if (row.GetCell(j) != null)
                {
                    switch (j)
                    {
                        case 0:
                            model.Affiliate = row.GetCell(j).ToString();
                            break;
                        case 1:
                            model.Site = row.GetCell(j).ToString();
                            break;
                        case 2:
                            model.Confirmation = row.GetCell(j).ToString();
                            break;
                        case 3:
                            model.Property = row.GetCell(j).ToString();
                            break;
                        case 4:
                            model.Guest = row.GetCell(j).ToString();
                            break;
                        case 5:
                            DateTime dt = new DateTime();
                            DateTime.TryParse(row.GetCell(j).ToString(), out dt);
                            model.CheckOut = dt;
                            break;
                        case 6:
                            DateTime dt1 = new DateTime();
                            DateTime.TryParse(row.GetCell(j).ToString(), out dt1);
                            model.Booked = dt1;
                            break;
                        case 7:
                            model.ReservationStatus = row.GetCell(j).ToString();
                            break;
                        case 8:
                            int roomNights = 0;
                            int.TryParse(row.GetCell(j).ToString(), out roomNights);
                            model.RooomNights = roomNights;
                            break;
                        case 9:
                            decimal flatAmt = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out flatAmt);
                            model.FlatAmount = flatAmt;
                            break;
                        case 10:
                            decimal revenu = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out revenu);
                            model.RoomRevenue = revenu;
                            break;
                        case 11:
                            decimal grossSalesTax = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out grossSalesTax);
                            model.GrossSaleWithTax = grossSalesTax;
                            break;
                        case 12:
                            decimal costOfHotelWithTax = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out costOfHotelWithTax);
                            model.CostOfHotelWithTax = costOfHotelWithTax;
                            break;
                        case 13:
                            decimal cardProcessingFee = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out cardProcessingFee);
                            model.CreditCardProcessingFee = cardProcessingFee;
                            break;
                        case 14:
                            decimal netProfit = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out netProfit);
                            model.NetProfit = netProfit;
                            break;
                        case 15:
                            float affiliteCommissionPercentage = 0;
                            float.TryParse(row.GetCell(j).ToString(), out affiliteCommissionPercentage);
                            model.AffiliateCommissionPercentage = affiliteCommissionPercentage;
                            break;
                        case 16:
                            decimal arnTransferFee;
                            decimal.TryParse(row.GetCell(j).ToString(), out arnTransferFee);
                            model.ARNTransferFee = arnTransferFee;
                            break;
                        case 17:
                            decimal arnCallCenterFee;
                            decimal.TryParse(row.GetCell(j).ToString(), out arnCallCenterFee);
                            model.ARNCallCenterFee = arnCallCenterFee;
                            break;
                        case 18:
                            decimal netCommission = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out netCommission);
                            model.NetCommission = netCommission;
                            break;
                        case 19:
                            decimal subAffiliateCommission = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out subAffiliateCommission);
                            model.SubAffiliateCommission = subAffiliateCommission;
                            break;
                        case 20:
                            decimal netCommissionAfterSubAffiliate = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out netCommissionAfterSubAffiliate);
                            model.NetCommissionAfterSubAffiliate = netCommissionAfterSubAffiliate;
                            break;
                        case 21:
                            model.CID = row.GetCell(j).ToString();
                            break;
                        case 22:
                            model.ReservationId = row.GetCell(j).ToString();
                            break;
                        case 23:
                            model.RegistrationId = row.GetCell(j).ToString();
                            break;
                        case 24:
                            model.RegistrationName = row.GetCell(j).ToString();
                            break;
                    }
                }
            }

            _merchantInventoryReservations.Add(model);

            return await Task.FromResult<bool>(true);
        }

        private async Task<bool> ParseAdjustmentRow(IRow row, int cellCount)
        {
            AdjustmentViewModel model = new AdjustmentViewModel();
            for (int j = row.FirstCellNum; j < cellCount; j++)
            {
                if (row.GetCell(j) != null)
                {

                    switch (j)
                    {
                        case 0:
                            model.Confirmation = row.GetCell(j).ToString();
                            break;
                        case 1:
                            model.Property = row.GetCell(j).ToString();
                            break;
                        case 2:
                            model.Guest = row.GetCell(j).ToString();
                            break;
                        case 3:
                            decimal commissionAdjustment = 0;
                            decimal.TryParse(row.GetCell(j).ToString(), out commissionAdjustment);
                            model.CommissionAdjustment = commissionAdjustment;
                            break;
                        case 4:
                            model.Notes = row.GetCell(j).ToString();
                            break;
                    }
                }
            }

            _adjustments.Add(model);

            return await Task.FromResult<bool>(true);
        }

        private async Task<bool> ParseInventoryTable()
        {
            throw new NotImplementedException();
        }

        public async Task<(bool isSuccess, string message)> ImportFile(IFormFile file)
        {
            StringBuilder sb = new StringBuilder();
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            string sFileExtension = Path.GetExtension(file.FileName).ToLower();
            ISheet sheet;
            string fullPath = Path.Combine(newPath, file.FileName);

            (bool isSuccess, string message) model = (false, "Not Implemented");

            try
            {
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    int sheetCount = 0;
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats 
                        sheetCount = hssfwb.NumberOfSheets;

                        for (int k = 0; k < sheetCount; k++)
                        {
                            sheet = hssfwb.GetSheetAt(k); //get first sheet from workbook  
                            IRow headerRow = sheet.GetRow(0); //Get Header Row
                            int cellCount = headerRow.LastCellNum;

                            int rowStartCount = 3;

                            if (k == 2)
                                rowStartCount = 2;


                            for (int i = rowStartCount; i <= sheet.LastRowNum; i++) //Read Excel File
                            {
                                IRow row = sheet.GetRow(i);
                                if (row == null) continue;
                                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                                bool e = k == 0 ? await ParseInventoryRow(row, cellCount) : k == 1 ? await ParseMerchantInventoryRow(row, cellCount) : k == 2 ? await ParseAdjustmentRow(row, cellCount) : false;
                                //sb.AppendLine("</tr>");
                            }
                        }

                        model.isSuccess = true;
                        model.message = $"<div class=\"row\"><div class=\"col-md-4\">Inventory Reservations Processed:</div><div class=\"col-md-8\">{_inventoryReservations.Count}</div></div>";
                        model.message += $"<div class=\"row\"><div class=\"col-md-4\">Merchant Inventories Processed:</div><div class=\"col-md-8\">{_merchantInventoryReservations.Count}</div></div>";
                        model.message += $"<div class=\"row\"><div class=\"col-md-4\">Adjustments Processed:</div><div class=\"col-md-8\">{_adjustments.Count}</div></div>";
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheetCount = hssfwb.NumberOfSheets;

                        for (int k = 0; k < sheetCount; k++)
                        {
                            sheet = hssfwb.GetSheetAt(k); //get first sheet from workbook  
                            IRow headerRow = sheet.GetRow(0); //Get Header Row
                            int cellCount = headerRow.LastCellNum;

                            int rowStartCount = 3;

                            if (k == 2)
                                rowStartCount = 2;

                            for (int i = rowStartCount; i <= sheet.LastRowNum; i++) //Read Excel File
                            {
                                IRow row = sheet.GetRow(i);
                                if (row == null) continue;
                                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                                bool e = k == 0 ? await ParseInventoryRow(row, cellCount) : k == 1 ? await ParseMerchantInventoryRow(row, cellCount) : k == 2 ? await ParseAdjustmentRow(row, cellCount) : false;
                                //sb.AppendLine("</tr>");
                            }
                        }

                        model.isSuccess = true;
                        model.message = $"<div class=\"row\"><div class=\"col-md-4\">Inventory Reservations Processed:</div><div class=\"col-md-8\">{_inventoryReservations.Count}</div></div>";
                        model.message += $"<div class=\"row\"><div class=\"col-md-4\">Merchant Inventories Processed:</div><div class=\"col-md-8\">{_merchantInventoryReservations.Count}</div></div>";
                        model.message += $"<div class=\"row\"><div class=\"col-md-4\">Adjustments Processed:</div><div class=\"col-md-8\">{_adjustments.Count}</div></div>";
                    }
                }
            }
            catch (Exception ex)
            {
                model.isSuccess = false;
                model.message = ex.Message;
            }

            return model;
        }
    }
}
