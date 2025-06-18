using Booking.Service.Services._Interfaces;
using Excel.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorImport.Service.Interfaces;
using VendorImport.Service.Models;
using VendorImport.Service.Models.ViewModels;

namespace Excel.Service.Services
{
    public class ExportService :IExportService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private List<AdjustmentModel> _adjustments = new List<AdjustmentModel>();
        private List<InventoryReservationModel> _inventoryReservations = new List<InventoryReservationModel>();
        private List<MerchantInventoryReservationModel> _merchantInventoryReservations = new List<MerchantInventoryReservationModel>();
        private readonly IVendorImportService _vendorImportSvc;
        private readonly IBookingService _bookingSvc;

        public ExportService(IWebHostEnvironment hostingEnvironment, IVendorImportService vendorImportSvc, IBookingService bookingSvc)
        {
            _hostingEnvironment = hostingEnvironment;
            _vendorImportSvc = vendorImportSvc;
            _bookingSvc = bookingSvc;
        }

        private async Task<bool> ParseInventoryRow(IRow row, int cellCount)
        {
            InventoryReservationModel model = new InventoryReservationModel();

            for (int j = row.FirstCellNum; j < cellCount; j++)
            {
                if (row.GetCell(j) != null)
                {
                    switch (j)
                    {
                        case 0:
                            model.AffiliateString = row.GetCell(j).ToString();
                            break;
                        case 1:
                            model.SiteString = row.GetCell(j).ToString();
                            break;
                        case 2:
                            model.Confirmation = row.GetCell(j).ToString();
                            break;
                        case 3:
                            model.Property = row.GetCell(j).ToString();
                            break;
                        case 4:
                            model.GuestString = row.GetCell(j).ToString();
                            break;
                        case 5:
                            DateTime dt = new DateTime();
                            if (DateTime.TryParse(row.GetCell(j).ToString(), out dt))
                                model.CheckoutDate = dt;
                            break;
                        case 6:
                            DateTime dt1 = new DateTime();
                            if (DateTime.TryParse(row.GetCell(j).ToString(), out dt1))
                                model.BookedDate = dt1;
                            break;
                        case 7:
                            model.ReservationStatus = row.GetCell(j).ToString();
                            break;
                        case 8:
                            int roomNights = 0;
                            if (int.TryParse(row.GetCell(j).ToString(), out roomNights))
                                model.RoomNights = roomNights;
                            break;
                        case 9:
                            decimal flatAmt = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out flatAmt))
                                model.FlatAmount = flatAmt;
                            break;
                        case 10:
                            decimal revenu = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out revenu))
                                model.RoomRevenue = revenu;
                            break;
                        case 11:
                            decimal commmissionRecieved = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out commmissionRecieved))
                                model.CommissionReceived = commmissionRecieved;
                            break;
                        case 12:
                            decimal commissionProcessingFee = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out commissionProcessingFee))
                                model.ComissionProcessingFee = commissionProcessingFee;
                            break;
                        case 13:
                            decimal collectionExpense = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out collectionExpense))
                                model.CollectionExpense = collectionExpense;
                            break;
                        case 14:
                            decimal callCenterFee = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out callCenterFee))
                                model.ARNCallCenterFee = callCenterFee;
                            break;
                        case 15:
                            decimal netCommission = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out netCommission))
                                model.NetCommission = netCommission;
                            break;
                        case 16:
                            decimal subAffiliateCommission;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out subAffiliateCommission))
                                model.SubAffiliateCommission = subAffiliateCommission;
                            break;
                        case 17:
                            decimal netCommissionAfterSubAffiliate;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out netCommissionAfterSubAffiliate))
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
            MerchantInventoryReservationModel model = new MerchantInventoryReservationModel();

            for (int j = row.FirstCellNum; j < cellCount; j++)
            {
                if (row.GetCell(j) != null)
                {
                    switch (j)
                    {
                        case 0:
                            model.AffiliateString = row.GetCell(j).ToString();
                            break;
                        case 1:
                            model.SiteString = row.GetCell(j).ToString();
                            break;
                        case 2:
                            model.Confirmation = row.GetCell(j).ToString();
                            break;
                        case 3:
                            model.Property = row.GetCell(j).ToString();
                            break;
                        case 4:
                            model.GuestString = row.GetCell(j).ToString();
                            break;
                        case 5:
                            DateTime dt = new DateTime();
                            if (DateTime.TryParse(row.GetCell(j).ToString(), out dt))
                                model.CheckoutDate = dt;
                            break;
                        case 6:
                            DateTime dt1 = new DateTime();
                            if (DateTime.TryParse(row.GetCell(j).ToString(), out dt1))
                                model.BookedDate = dt1;
                            break;
                        case 7:
                            model.ReservationStatus = row.GetCell(j).ToString();
                            break;
                        case 8:
                            int roomNights = 0;
                            if (int.TryParse(row.GetCell(j).ToString(), out roomNights))
                                model.RoomNights = roomNights;
                            break;
                        case 9:
                            decimal flatAmt = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out flatAmt))
                                model.FlatAmount = flatAmt;
                            break;
                        case 10:
                            decimal revenu = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out revenu))
                                model.RoomRevenue = revenu;
                            break;
                        case 11:
                            decimal grossSalesTax = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out grossSalesTax))
                                model.GrossSaleWithTax = grossSalesTax;
                            break;
                        case 12:
                            decimal costOfHotelWithTax = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out costOfHotelWithTax))
                                model.CostOfHotelWithTax = costOfHotelWithTax;
                            break;
                        case 13:
                            decimal cardProcessingFee = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out cardProcessingFee))
                                model.CardProcessingFees = cardProcessingFee;
                            break;
                        case 14:
                            decimal netProfit = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out netProfit))
                                model.NetProfit = netProfit;
                            break;
                        case 15:
                            decimal affiliteCommissionPercentage = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out affiliteCommissionPercentage))
                                model.AffiliteCommissionPercentage = affiliteCommissionPercentage;
                            break;
                        case 16:
                            decimal arnTransferFee;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out arnTransferFee))
                                model.ARNTransactionFee = arnTransferFee;
                            break;
                        case 17:
                            decimal arnCallCenterFee;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out arnCallCenterFee))
                                model.ARNCallCenterFee = arnCallCenterFee;
                            break;
                        case 18:
                            decimal netCommission = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out netCommission))
                                model.NetCommission = netCommission;
                            break;
                        case 19:
                            decimal subAffiliateCommission = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out subAffiliateCommission))
                                model.SubAffiliateCommission = subAffiliateCommission;
                            break;
                        case 20:
                            decimal netCommissionAfterSubAffiliate = 0;
                            if (decimal.TryParse(row.GetCell(j).ToString(), out netCommissionAfterSubAffiliate))
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
            AdjustmentModel model = new AdjustmentModel();
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
                            model.GuestString = row.GetCell(j).ToString();
                            break;
                        case 3:
                            decimal commissionAdjustment = 0;
                            if(decimal.TryParse(row.GetCell(j).ToString(), out commissionAdjustment))
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

        public VendorResults<InventoryReservationModel> VendorInventory { get; set; } = new VendorResults<InventoryReservationModel>();
        public VendorResults<InventoryReservationModel> BookingInventory { get; set; } = new VendorResults<InventoryReservationModel>();
        public VendorResults<MerchantInventoryReservationModel> VendorMerchant { get; set; } = new VendorResults<MerchantInventoryReservationModel>();
        public VendorResults<MerchantInventoryReservationModel> BookingMerchant { get; set; } = new VendorResults<MerchantInventoryReservationModel>();
        public VendorResults<AdjustmentModel> VendorAdjustment { get; set; } = new VendorResults<AdjustmentModel>();
        public VendorResults<AdjustmentModel> BookingAdjustment { get; set; } = new VendorResults<AdjustmentModel>();


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
                //VendorResults<InventoryReservationModel> vendorInventoryModel = null;
                //VendorResults<InventoryReservationModel> bookingInventoryModel = null;

                //VendorResults<MerchantInventoryReservationModel> vendorMerchantModel = null;
                //VendorResults<MerchantInventoryReservationModel> bookingMerchantModel = null;

                //VendorResults<AdjustmentModel> vendorAdjustmentModel = null;
                //VendorResults<AdjustmentModel> bookingAdjustmentModel = null;


                if (_inventoryReservations.Count > 0)
                {
                    VendorInventory = await _vendorImportSvc.AddInventoryReservationsAsync(_inventoryReservations);
                    BookingInventory = await _bookingSvc.AddInventoriesPaidData(_inventoryReservations);
                }

                if (_merchantInventoryReservations.Count > 0)
                {
                    VendorMerchant = await _vendorImportSvc.AddMerchantInventoryReservationsAsync(_merchantInventoryReservations);
                    BookingMerchant = await _bookingSvc.AddMerchantsPaidData(_merchantInventoryReservations);
                }

                if (_adjustments.Count > 0)
                {
                    VendorAdjustment = await _vendorImportSvc.AddAdjustmentsAsync(_adjustments);
                    BookingAdjustment = await _bookingSvc.VerifyAdjustments(_adjustments);
                }

                //(bool isSuccess, string message) returnMsg = await ExportFile(vendorInventoryModel, vendorMerchantModel, vendorAdjustmentModel, bookingInventoryModel, bookingMerchantModel, bookingAdjustmentModel);

                model.message += $"<div class=\"row\"><div class=\"col-md-4\">Status:</div><div class=\"col-md-8\">Complete <a href='[[URL]]' target=\"_blank\">Download Excel Import Report</a></div></div>";
            }
            catch (Exception ex)
            {
                model.isSuccess = false;
                model.message = ex.Message;
            }

            return model;
        }

        /*public async Task<(IActionResult)> ExportFile(
            VendorResults<InventoryReservationModel> vendorInventory = null, 
            VendorResults<MerchantInventoryReservationModel> vendorMerchant = null, 
            VendorResults<AdjustmentModel> vendorAdjustment = null, 
            VendorResults<InventoryReservationModel> bookingInventory = null, 
            VendorResults<MerchantInventoryReservationModel> bookingMerchant = null, 
            VendorResults<AdjustmentModel> bookingAdjustment = null)
        {

            (bool isSuccess, string message) model = (false, "Not implemented");

            try
            {
                


            }
            catch (Exception ex)
            {
                model = (false, ex.Message);
                
            }

            return model;
        }*/
    }
}
