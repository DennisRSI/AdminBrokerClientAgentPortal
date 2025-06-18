using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientPortal.Controllers.APIs
{
    [Route("api/[controller]")]
    public class ARNController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IExportService _importService;

        public ARNController(IWebHostEnvironment hostingEnvironment, IExportService importService)
        {
            _hostingEnvironment = hostingEnvironment;
            _importService = importService;
        }

        /*// GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<controller>
        [HttpPost("excelimport")]
        public async Task<(bool isSuccess, string message)> ExcelImport()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            (bool isSuccess, string message) model = (false, "Not Implemented");

            try
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (file.Length > 0)
                {
                    model = await _importService.ImportFile(file);

                    folderName = "Parsed";
                    string sWebRootFolder = _hostingEnvironment.WebRootPath;
                    newPath = Path.Combine(sWebRootFolder, folderName);

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    string sFileName = $@"ARNParsed_{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString().PadLeft(2, '0')}{DateTime.Now.Day.ToString().PadLeft(2, '0')}.xlsx";

                    //string URL = string.Format("https://consol.travnow.com/{0}/{1}", folderName, sFileName);
                    string URL = string.Format("/{0}/{1}", folderName, sFileName); //https:/localhost:44396
                    FileInfo file1 = new FileInfo(Path.Combine(newPath, sFileName));
                    var memory = new MemoryStream();

                    using (var fs = new FileStream(Path.Combine(newPath, sFileName), FileMode.Create, FileAccess.Write))
                    {
                        IWorkbook workbook;
                        workbook = new XSSFWorkbook();

                        #region Good Inventory Vendor Sheet Archive

                        ISheet excelSheet1 = workbook.CreateSheet("Good Inventory Archive");
                        IRow row = excelSheet1.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Affiliate");
                        row.CreateCell(1).SetCellValue("Site");
                        row.CreateCell(2).SetCellValue("Confirmation");
                        row.CreateCell(3).SetCellValue("Property");
                        row.CreateCell(4).SetCellValue("Guest");
                        row.CreateCell(5).SetCellValue("CheckOut");
                        row.CreateCell(6).SetCellValue("Booked");
                        row.CreateCell(7).SetCellValue("Res Status");
                        row.CreateCell(8).SetCellValue("Room Nights");
                        row.CreateCell(9).SetCellValue("Flat Amount");
                        row.CreateCell(10).SetCellValue("Booked Room Revenue");
                        row.CreateCell(11).SetCellValue("Comm Rcvd");
                        row.CreateCell(12).SetCellValue("Comm Proc Fee");
                        row.CreateCell(13).SetCellValue("Collection Expense");
                        row.CreateCell(14).SetCellValue("ARN Call Center Fee");
                        row.CreateCell(15).SetCellValue("Net Comm");
                        row.CreateCell(16).SetCellValue("Sub Aff Comm");
                        row.CreateCell(17).SetCellValue("Net Comm After Sub Affil");
                        row.CreateCell(18).SetCellValue("CID");
                        row.CreateCell(19).SetCellValue("ReservationId");
                        row.CreateCell(20).SetCellValue("RegistrationId");
                        row.CreateCell(21).SetCellValue("Registration Name");

                        int ct = 1;

                        foreach (var vi in _importService.VendorInventory.AddedItems)
                        {
                            row = excelSheet1.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue($"{vi.AffiliateName} - {vi.AffiliateId}");
                            row.CreateCell(1).SetCellValue($"{vi.SiteId} - {vi.SiteName}");
                            row.CreateCell(2).SetCellValue(vi.Confirmation);
                            row.CreateCell(3).SetCellValue(vi.Property);
                            row.CreateCell(4).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(5).SetCellValue(vi.CheckoutDate.ToShortDateString());
                            row.CreateCell(6).SetCellValue(vi.BookedDate.ToShortDateString());
                            row.CreateCell(7).SetCellValue(vi.ReservationStatus);
                            row.CreateCell(8).SetCellValue(vi.RoomNights);
                            row.CreateCell(9).SetCellValue((double)vi.FlatAmount);
                            row.CreateCell(10).SetCellValue((double)vi.BookedRoomRevenue);
                            row.CreateCell(11).SetCellValue((double)vi.CommissionReceived);
                            row.CreateCell(12).SetCellValue((double)vi.ComissionProcessingFee);
                            row.CreateCell(13).SetCellValue((double)vi.CollectionExpense);
                            row.CreateCell(14).SetCellValue((double)vi.ARNCallCenterFee);
                            row.CreateCell(15).SetCellValue((double)vi.NetCommission);
                            row.CreateCell(16).SetCellValue((double)vi.SubAffiliateCommission);
                            row.CreateCell(17).SetCellValue((double)vi.NetCommissionAfterSubAffiliate);
                            row.CreateCell(18).SetCellValue(vi.CID);
                            row.CreateCell(19).SetCellValue(vi.ReservationId);
                            row.CreateCell(20).SetCellValue(vi.RegistrationId);
                            row.CreateCell(21).SetCellValue(vi.RegistrationName);

                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Good Inventory Booking Reservations

                        ISheet excelSheet2 = workbook.CreateSheet("Good Inventory Brokers");
                        row = excelSheet2.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Affiliate");
                        row.CreateCell(1).SetCellValue("Site");
                        row.CreateCell(2).SetCellValue("Confirmation");
                        row.CreateCell(3).SetCellValue("Property");
                        row.CreateCell(4).SetCellValue("Guest");
                        row.CreateCell(5).SetCellValue("CheckOut");
                        row.CreateCell(6).SetCellValue("Booked");
                        row.CreateCell(7).SetCellValue("Res Status");
                        row.CreateCell(8).SetCellValue("Room Nights");
                        row.CreateCell(9).SetCellValue("Flat Amount");
                        row.CreateCell(10).SetCellValue("Booked Room Revenue");
                        row.CreateCell(11).SetCellValue("Comm Rcvd");
                        row.CreateCell(12).SetCellValue("Comm Proc Fee");
                        row.CreateCell(13).SetCellValue("Collection Expense");
                        row.CreateCell(14).SetCellValue("ARN Call Center Fee");
                        row.CreateCell(15).SetCellValue("Net Comm");
                        row.CreateCell(16).SetCellValue("Sub Aff Comm");
                        row.CreateCell(17).SetCellValue("Net Comm After Sub Affil");
                        row.CreateCell(18).SetCellValue("CID");
                        row.CreateCell(19).SetCellValue("ReservationId");
                        row.CreateCell(20).SetCellValue("RegistrationId");
                        row.CreateCell(21).SetCellValue("Registration Name");

                        ct = 1;

                        foreach (var vi in _importService.BookingInventory.AddedItems)
                        {
                            row = excelSheet2.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue($"{vi.AffiliateName} - {vi.AffiliateId}");
                            row.CreateCell(1).SetCellValue($"{vi.SiteId} - {vi.SiteName}");
                            row.CreateCell(2).SetCellValue(vi.Confirmation);
                            row.CreateCell(3).SetCellValue(vi.Property);
                            row.CreateCell(4).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(5).SetCellValue(vi.CheckoutDate.ToShortDateString());
                            row.CreateCell(6).SetCellValue(vi.BookedDate.ToShortDateString());
                            row.CreateCell(7).SetCellValue(vi.ReservationStatus);
                            row.CreateCell(8).SetCellValue(vi.RoomNights);
                            row.CreateCell(9).SetCellValue((double)vi.FlatAmount);
                            row.CreateCell(10).SetCellValue((double)vi.BookedRoomRevenue);
                            row.CreateCell(11).SetCellValue((double)vi.CommissionReceived);
                            row.CreateCell(12).SetCellValue((double)vi.ComissionProcessingFee);
                            row.CreateCell(13).SetCellValue((double)vi.CollectionExpense);
                            row.CreateCell(14).SetCellValue((double)vi.ARNCallCenterFee);
                            row.CreateCell(15).SetCellValue((double)vi.NetCommission);
                            row.CreateCell(16).SetCellValue((double)vi.SubAffiliateCommission);
                            row.CreateCell(17).SetCellValue((double)vi.NetCommissionAfterSubAffiliate);
                            row.CreateCell(18).SetCellValue(vi.CID);
                            row.CreateCell(19).SetCellValue(vi.ReservationId);
                            row.CreateCell(20).SetCellValue(vi.RegistrationId);
                            row.CreateCell(21).SetCellValue(vi.RegistrationName);

                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Good Merchant Archive
                        ISheet excelSheet3 = workbook.CreateSheet("Good Merchant Archive");
                        row = excelSheet3.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Affiliate");
                        row.CreateCell(1).SetCellValue("Site");
                        row.CreateCell(2).SetCellValue("Confirmation");
                        row.CreateCell(3).SetCellValue("Property");
                        row.CreateCell(4).SetCellValue("Guest");
                        row.CreateCell(5).SetCellValue("CheckOut");
                        row.CreateCell(6).SetCellValue("Booked");
                        row.CreateCell(7).SetCellValue("Res Status");
                        row.CreateCell(8).SetCellValue("Room Nights");
                        row.CreateCell(9).SetCellValue("Flat Amount");
                        row.CreateCell(10).SetCellValue("Room Revenue");
                        row.CreateCell(11).SetCellValue("Gross Sale w/Tax");
                        row.CreateCell(12).SetCellValue("Cost Of Hotel w/Tax");
                        row.CreateCell(13).SetCellValue("Credit Card Processing Fee");
                        row.CreateCell(14).SetCellValue("Net Profit");
                        row.CreateCell(15).SetCellValue("Affil. Comm %");
                        row.CreateCell(16).SetCellValue("ARN Trans Fee");
                        row.CreateCell(17).SetCellValue("ARN Call Center Fee");
                        row.CreateCell(18).SetCellValue("Net Comm");
                        row.CreateCell(19).SetCellValue("Sub Aff Comm");
                        row.CreateCell(20).SetCellValue("Net Comm After Sub Affil");
                        row.CreateCell(21).SetCellValue("CID");
                        row.CreateCell(22).SetCellValue("ReservationId");
                        row.CreateCell(23).SetCellValue("RegistrationId");
                        row.CreateCell(24).SetCellValue("Retistration Name");

                        ct = 1;

                        foreach (var vi in _importService.VendorMerchant.AddedItems)
                        {
                            row = excelSheet3.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue($"{vi.AffiliateName} - {vi.AffiliateId}");
                            row.CreateCell(1).SetCellValue($"{vi.SiteId} - {vi.SiteName}");
                            row.CreateCell(2).SetCellValue(vi.Confirmation);
                            row.CreateCell(3).SetCellValue(vi.Property);
                            row.CreateCell(4).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(5).SetCellValue(vi.CheckoutDate.ToShortDateString());
                            row.CreateCell(6).SetCellValue(vi.BookedDate.ToShortDateString());
                            row.CreateCell(7).SetCellValue(vi.ReservationStatus);
                            row.CreateCell(8).SetCellValue(vi.RoomNights);
                            row.CreateCell(9).SetCellValue((double)vi.FlatAmount);
                            row.CreateCell(10).SetCellValue((double)vi.RoomRevenue);
                            row.CreateCell(11).SetCellValue((double)vi.GrossSaleWithTax);
                            row.CreateCell(12).SetCellValue((double)vi.CostOfHotelWithTax);
                            row.CreateCell(13).SetCellValue((double)vi.CardProcessingFees);
                            row.CreateCell(14).SetCellValue((double)vi.NetProfit);
                            row.CreateCell(15).SetCellValue((double)vi.AffiliteCommissionPercentage);
                            row.CreateCell(16).SetCellValue((double)vi.ARNTransactionFee);
                            row.CreateCell(17).SetCellValue((double)vi.ARNCallCenterFee);
                            row.CreateCell(18).SetCellValue((double)vi.NetCommission);
                            row.CreateCell(19).SetCellValue((double)vi.SubAffiliateCommission);
                            row.CreateCell(20).SetCellValue((double)vi.NetCommissionAfterSubAffiliate);
                            row.CreateCell(21).SetCellValue(vi.CID);
                            row.CreateCell(22).SetCellValue(vi.ReservationId);
                            row.CreateCell(23).SetCellValue(vi.RegistrationId);
                            row.CreateCell(24).SetCellValue(vi.RegistrationName);

                        }

                        // workbook.Write(fs);

                        #endregion

                        #region Good Merchant Brokers

                        ISheet excelSheet4 = workbook.CreateSheet("Good Merchant Brokers");
                        row = excelSheet4.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Affiliate");
                        row.CreateCell(1).SetCellValue("Site");
                        row.CreateCell(2).SetCellValue("Confirmation");
                        row.CreateCell(3).SetCellValue("Property");
                        row.CreateCell(4).SetCellValue("Guest");
                        row.CreateCell(5).SetCellValue("CheckOut");
                        row.CreateCell(6).SetCellValue("Booked");
                        row.CreateCell(7).SetCellValue("Res Status");
                        row.CreateCell(8).SetCellValue("Room Nights");
                        row.CreateCell(9).SetCellValue("Flat Amount");
                        row.CreateCell(10).SetCellValue("Room Revenue");
                        row.CreateCell(11).SetCellValue("Gross Sale w/Tax");
                        row.CreateCell(12).SetCellValue("Cost Of Hotel w/Tax");
                        row.CreateCell(13).SetCellValue("Credit Card Processing Fee");
                        row.CreateCell(14).SetCellValue("Net Profit");
                        row.CreateCell(15).SetCellValue("Affil. Comm %");
                        row.CreateCell(16).SetCellValue("ARN Trans Fee");
                        row.CreateCell(17).SetCellValue("ARN Call Center Fee");
                        row.CreateCell(18).SetCellValue("Net Comm");
                        row.CreateCell(19).SetCellValue("Sub Aff Comm");
                        row.CreateCell(20).SetCellValue("Net Comm After Sub Affil");
                        row.CreateCell(21).SetCellValue("CID");
                        row.CreateCell(22).SetCellValue("ReservationId");
                        row.CreateCell(23).SetCellValue("RegistrationId");
                        row.CreateCell(24).SetCellValue("Retistration Name");

                        ct = 1;

                        foreach (var vi in _importService.BookingMerchant.AddedItems)
                        {
                            row = excelSheet4.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue($"{vi.AffiliateName} - {vi.AffiliateId}");
                            row.CreateCell(1).SetCellValue($"{vi.SiteId} - {vi.SiteName}");
                            row.CreateCell(2).SetCellValue(vi.Confirmation);
                            row.CreateCell(3).SetCellValue(vi.Property);
                            row.CreateCell(4).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(5).SetCellValue(vi.CheckoutDate.ToShortDateString());
                            row.CreateCell(6).SetCellValue(vi.BookedDate.ToShortDateString());
                            row.CreateCell(7).SetCellValue(vi.ReservationStatus);
                            row.CreateCell(8).SetCellValue(vi.RoomNights);
                            row.CreateCell(9).SetCellValue((double)vi.FlatAmount);
                            row.CreateCell(10).SetCellValue((double)vi.RoomRevenue);
                            row.CreateCell(11).SetCellValue((double)vi.GrossSaleWithTax);
                            row.CreateCell(12).SetCellValue((double)vi.CostOfHotelWithTax);
                            row.CreateCell(13).SetCellValue((double)vi.CardProcessingFees);
                            row.CreateCell(14).SetCellValue((double)vi.NetProfit);
                            row.CreateCell(15).SetCellValue((double)vi.AffiliteCommissionPercentage);
                            row.CreateCell(16).SetCellValue((double)vi.ARNTransactionFee);
                            row.CreateCell(17).SetCellValue((double)vi.ARNCallCenterFee);
                            row.CreateCell(18).SetCellValue((double)vi.NetCommission);
                            row.CreateCell(19).SetCellValue((double)vi.SubAffiliateCommission);
                            row.CreateCell(20).SetCellValue((double)vi.NetCommissionAfterSubAffiliate);
                            row.CreateCell(21).SetCellValue(vi.CID);
                            row.CreateCell(22).SetCellValue(vi.ReservationId);
                            row.CreateCell(23).SetCellValue(vi.RegistrationId);
                            row.CreateCell(24).SetCellValue(vi.RegistrationName);

                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Good Adjustments Archive

                        ISheet excelSheet5 = workbook.CreateSheet("Good Adjustments Archive");
                        row = excelSheet5.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Confirmation");
                        row.CreateCell(1).SetCellValue("Property");
                        row.CreateCell(2).SetCellValue("Guest");
                        row.CreateCell(3).SetCellValue("Comm Adj$");
                        row.CreateCell(4).SetCellValue("Notes");

                        ct = 1;

                        foreach (var vi in _importService.VendorAdjustment.AddedItems)
                        {
                            row = excelSheet5.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue(vi.Confirmation);
                            row.CreateCell(1).SetCellValue(vi.Property);
                            row.CreateCell(2).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(3).SetCellValue((double)vi.CommissionAdjustment);
                            row.CreateCell(5).SetCellValue(vi.Notes);


                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Good Adjustments Brokers

                        ISheet excelSheet6 = workbook.CreateSheet("Good Adjustments Brokers");
                        row = excelSheet6.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Confirmation");
                        row.CreateCell(1).SetCellValue("Property");
                        row.CreateCell(2).SetCellValue("Guest");
                        row.CreateCell(3).SetCellValue("Comm Adj$");
                        row.CreateCell(4).SetCellValue("Notes");

                        ct = 1;

                        foreach (var vi in _importService.BookingAdjustment.AddedItems)
                        {
                            row = excelSheet6.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue(vi.Confirmation);
                            row.CreateCell(1).SetCellValue(vi.Property);
                            row.CreateCell(2).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(3).SetCellValue((double)vi.CommissionAdjustment);
                            row.CreateCell(5).SetCellValue(vi.Notes);


                        }

                        //workbook.Write(fs);

                        #endregion

                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        #region Bad Inventory Archive

                        ISheet excelSheet7 = workbook.CreateSheet("Bad Inventory Archive");
                        row = excelSheet7.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Affiliate");
                        row.CreateCell(1).SetCellValue("Site");
                        row.CreateCell(2).SetCellValue("Confirmation");
                        row.CreateCell(3).SetCellValue("Property");
                        row.CreateCell(4).SetCellValue("Guest");
                        row.CreateCell(5).SetCellValue("CheckOut");
                        row.CreateCell(6).SetCellValue("Booked");
                        row.CreateCell(7).SetCellValue("Res Status");
                        row.CreateCell(8).SetCellValue("Room Nights");
                        row.CreateCell(9).SetCellValue("Flat Amount");
                        row.CreateCell(10).SetCellValue("Booked Room Revenue");
                        row.CreateCell(11).SetCellValue("Comm Rcvd");
                        row.CreateCell(12).SetCellValue("Comm Proc Fee");
                        row.CreateCell(13).SetCellValue("Collection Expense");
                        row.CreateCell(14).SetCellValue("ARN Call Center Fee");
                        row.CreateCell(15).SetCellValue("Net Comm");
                        row.CreateCell(16).SetCellValue("Sub Aff Comm");
                        row.CreateCell(17).SetCellValue("Net Comm After Sub Affil");
                        row.CreateCell(18).SetCellValue("CID");
                        row.CreateCell(19).SetCellValue("ReservationId");
                        row.CreateCell(20).SetCellValue("RegistrationId");
                        row.CreateCell(21).SetCellValue("Registration Name");
                        row.CreateCell(22).SetCellValue("Outcome");

                        ct = 1;

                        foreach (var vi in _importService.VendorInventory.RejectedItems)
                        {
                            row = excelSheet7.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue($"{vi.AffiliateName} - {vi.AffiliateId}");
                            row.CreateCell(1).SetCellValue($"{vi.SiteId} - {vi.SiteName}");
                            row.CreateCell(2).SetCellValue(vi.Confirmation);
                            row.CreateCell(3).SetCellValue(vi.Property);
                            row.CreateCell(4).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(5).SetCellValue(vi.CheckoutDate.ToShortDateString());
                            row.CreateCell(6).SetCellValue(vi.BookedDate.ToShortDateString());
                            row.CreateCell(7).SetCellValue(vi.ReservationStatus);
                            row.CreateCell(8).SetCellValue(vi.RoomNights);
                            row.CreateCell(9).SetCellValue((double)vi.FlatAmount);
                            row.CreateCell(10).SetCellValue((double)vi.BookedRoomRevenue);
                            row.CreateCell(11).SetCellValue((double)vi.CommissionReceived);
                            row.CreateCell(12).SetCellValue((double)vi.ComissionProcessingFee);
                            row.CreateCell(13).SetCellValue((double)vi.CollectionExpense);
                            row.CreateCell(14).SetCellValue((double)vi.ARNCallCenterFee);
                            row.CreateCell(15).SetCellValue((double)vi.NetCommission);
                            row.CreateCell(16).SetCellValue((double)vi.SubAffiliateCommission);
                            row.CreateCell(17).SetCellValue((double)vi.NetCommissionAfterSubAffiliate);
                            row.CreateCell(18).SetCellValue(vi.CID);
                            row.CreateCell(19).SetCellValue(vi.ReservationId);
                            row.CreateCell(20).SetCellValue(vi.RegistrationId);
                            row.CreateCell(21).SetCellValue(vi.RegistrationName);
                            row.CreateCell(21).SetCellValue(vi.RegistrationName);
                            row.CreateCell(22).SetCellValue(vi.Message);

                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Bad Inventory Booking Reservations

                        ISheet excelSheet8 = workbook.CreateSheet("Bad Inventory Brokers");
                        row = excelSheet8.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Affiliate");
                        row.CreateCell(1).SetCellValue("Site");
                        row.CreateCell(2).SetCellValue("Confirmation");
                        row.CreateCell(3).SetCellValue("Property");
                        row.CreateCell(4).SetCellValue("Guest");
                        row.CreateCell(5).SetCellValue("CheckOut");
                        row.CreateCell(6).SetCellValue("Booked");
                        row.CreateCell(7).SetCellValue("Res Status");
                        row.CreateCell(8).SetCellValue("Room Nights");
                        row.CreateCell(9).SetCellValue("Flat Amount");
                        row.CreateCell(10).SetCellValue("Booked Room Revenue");
                        row.CreateCell(11).SetCellValue("Comm Rcvd");
                        row.CreateCell(12).SetCellValue("Comm Proc Fee");
                        row.CreateCell(13).SetCellValue("Collection Expense");
                        row.CreateCell(14).SetCellValue("ARN Call Center Fee");
                        row.CreateCell(15).SetCellValue("Net Comm");
                        row.CreateCell(16).SetCellValue("Sub Aff Comm");
                        row.CreateCell(17).SetCellValue("Net Comm After Sub Affil");
                        row.CreateCell(18).SetCellValue("CID");
                        row.CreateCell(19).SetCellValue("ReservationId");
                        row.CreateCell(20).SetCellValue("RegistrationId");
                        row.CreateCell(21).SetCellValue("Registration Name");
                        row.CreateCell(22).SetCellValue("Outcome");

                        ct = 1;

                        foreach (var vi in _importService.BookingInventory.RejectedItems)
                        {
                            row = excelSheet8.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue($"{vi.AffiliateName} - {vi.AffiliateId}");
                            row.CreateCell(1).SetCellValue($"{vi.SiteId} - {vi.SiteName}");
                            row.CreateCell(2).SetCellValue(vi.Confirmation);
                            row.CreateCell(3).SetCellValue(vi.Property);
                            row.CreateCell(4).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(5).SetCellValue(vi.CheckoutDate.ToShortDateString());
                            row.CreateCell(6).SetCellValue(vi.BookedDate.ToShortDateString());
                            row.CreateCell(7).SetCellValue(vi.ReservationStatus);
                            row.CreateCell(8).SetCellValue(vi.RoomNights);
                            row.CreateCell(9).SetCellValue((double)vi.FlatAmount);
                            row.CreateCell(10).SetCellValue((double)vi.BookedRoomRevenue);
                            row.CreateCell(11).SetCellValue((double)vi.CommissionReceived);
                            row.CreateCell(12).SetCellValue((double)vi.ComissionProcessingFee);
                            row.CreateCell(13).SetCellValue((double)vi.CollectionExpense);
                            row.CreateCell(14).SetCellValue((double)vi.ARNCallCenterFee);
                            row.CreateCell(15).SetCellValue((double)vi.NetCommission);
                            row.CreateCell(16).SetCellValue((double)vi.SubAffiliateCommission);
                            row.CreateCell(17).SetCellValue((double)vi.NetCommissionAfterSubAffiliate);
                            row.CreateCell(18).SetCellValue(vi.CID);
                            row.CreateCell(19).SetCellValue(vi.ReservationId);
                            row.CreateCell(20).SetCellValue(vi.RegistrationId);
                            row.CreateCell(21).SetCellValue(vi.RegistrationName);
                            row.CreateCell(22).SetCellValue(vi.Message);

                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Bad Merchant Archive

                        ISheet excelSheet9 = workbook.CreateSheet("Bad Merchant Archive");
                        row = excelSheet9.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Affiliate");
                        row.CreateCell(1).SetCellValue("Site");
                        row.CreateCell(2).SetCellValue("Confirmation");
                        row.CreateCell(3).SetCellValue("Property");
                        row.CreateCell(4).SetCellValue("Guest");
                        row.CreateCell(5).SetCellValue("CheckOut");
                        row.CreateCell(6).SetCellValue("Booked");
                        row.CreateCell(7).SetCellValue("Res Status");
                        row.CreateCell(8).SetCellValue("Room Nights");
                        row.CreateCell(9).SetCellValue("Flat Amount");
                        row.CreateCell(10).SetCellValue("Room Revenue");
                        row.CreateCell(11).SetCellValue("Gross Sale w/Tax");
                        row.CreateCell(12).SetCellValue("Cost Of Hotel w/Tax");
                        row.CreateCell(13).SetCellValue("Credit Card Processing Fee");
                        row.CreateCell(14).SetCellValue("Net Profit");
                        row.CreateCell(15).SetCellValue("Affil. Comm %");
                        row.CreateCell(16).SetCellValue("ARN Trans Fee");
                        row.CreateCell(17).SetCellValue("ARN Call Center Fee");
                        row.CreateCell(18).SetCellValue("Net Comm");
                        row.CreateCell(19).SetCellValue("Sub Aff Comm");
                        row.CreateCell(20).SetCellValue("Net Comm After Sub Affil");
                        row.CreateCell(21).SetCellValue("CID");
                        row.CreateCell(22).SetCellValue("ReservationId");
                        row.CreateCell(23).SetCellValue("RegistrationId");
                        row.CreateCell(24).SetCellValue("Retistration Name");
                        row.CreateCell(25).SetCellValue("Outcome");

                        ct = 1;

                        foreach (var vi in _importService.VendorMerchant.RejectedItems)
                        {
                            row = excelSheet9.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue($"{vi.AffiliateName} - {vi.AffiliateId}");
                            row.CreateCell(1).SetCellValue($"{vi.SiteId} - {vi.SiteName}");
                            row.CreateCell(2).SetCellValue(vi.Confirmation);
                            row.CreateCell(3).SetCellValue(vi.Property);
                            row.CreateCell(4).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(5).SetCellValue(vi.CheckoutDate.ToShortDateString());
                            row.CreateCell(6).SetCellValue(vi.BookedDate.ToShortDateString());
                            row.CreateCell(7).SetCellValue(vi.ReservationStatus);
                            row.CreateCell(8).SetCellValue(vi.RoomNights);
                            row.CreateCell(9).SetCellValue((double)vi.FlatAmount);
                            row.CreateCell(10).SetCellValue((double)vi.RoomRevenue);
                            row.CreateCell(11).SetCellValue((double)vi.GrossSaleWithTax);
                            row.CreateCell(12).SetCellValue((double)vi.CostOfHotelWithTax);
                            row.CreateCell(13).SetCellValue((double)vi.CardProcessingFees);
                            row.CreateCell(14).SetCellValue((double)vi.NetProfit);
                            row.CreateCell(15).SetCellValue((double)vi.AffiliteCommissionPercentage);
                            row.CreateCell(16).SetCellValue((double)vi.ARNTransactionFee);
                            row.CreateCell(17).SetCellValue((double)vi.ARNCallCenterFee);
                            row.CreateCell(18).SetCellValue((double)vi.NetCommission);
                            row.CreateCell(19).SetCellValue((double)vi.SubAffiliateCommission);
                            row.CreateCell(20).SetCellValue((double)vi.NetCommissionAfterSubAffiliate);
                            row.CreateCell(21).SetCellValue(vi.CID);
                            row.CreateCell(22).SetCellValue(vi.ReservationId);
                            row.CreateCell(23).SetCellValue(vi.RegistrationId);
                            row.CreateCell(24).SetCellValue(vi.RegistrationName);
                            row.CreateCell(25).SetCellValue(vi.Message);

                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Bad Merchant Brokers

                        ISheet excelSheet10 = workbook.CreateSheet("Bad Merchant Brokers");
                        row = excelSheet10.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Affiliate");
                        row.CreateCell(1).SetCellValue("Site");
                        row.CreateCell(2).SetCellValue("Confirmation");
                        row.CreateCell(3).SetCellValue("Property");
                        row.CreateCell(4).SetCellValue("Guest");
                        row.CreateCell(5).SetCellValue("CheckOut");
                        row.CreateCell(6).SetCellValue("Booked");
                        row.CreateCell(7).SetCellValue("Res Status");
                        row.CreateCell(8).SetCellValue("Room Nights");
                        row.CreateCell(9).SetCellValue("Flat Amount");
                        row.CreateCell(10).SetCellValue("Room Revenue");
                        row.CreateCell(11).SetCellValue("Gross Sale w/Tax");
                        row.CreateCell(12).SetCellValue("Cost Of Hotel w/Tax");
                        row.CreateCell(13).SetCellValue("Credit Card Processing Fee");
                        row.CreateCell(14).SetCellValue("Net Profit");
                        row.CreateCell(15).SetCellValue("Affil. Comm %");
                        row.CreateCell(16).SetCellValue("ARN Trans Fee");
                        row.CreateCell(17).SetCellValue("ARN Call Center Fee");
                        row.CreateCell(18).SetCellValue("Net Comm");
                        row.CreateCell(19).SetCellValue("Sub Aff Comm");
                        row.CreateCell(20).SetCellValue("Net Comm After Sub Affil");
                        row.CreateCell(21).SetCellValue("CID");
                        row.CreateCell(22).SetCellValue("ReservationId");
                        row.CreateCell(23).SetCellValue("RegistrationId");
                        row.CreateCell(24).SetCellValue("Retistration Name");
                        row.CreateCell(25).SetCellValue("Outcome");

                        ct = 1;

                        foreach (var vi in _importService.BookingMerchant.RejectedItems)
                        {
                            row = excelSheet10.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue($"{vi.AffiliateName} - {vi.AffiliateId}");
                            row.CreateCell(1).SetCellValue($"{vi.SiteId} - {vi.SiteName}");
                            row.CreateCell(2).SetCellValue(vi.Confirmation);
                            row.CreateCell(3).SetCellValue(vi.Property);
                            row.CreateCell(4).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(5).SetCellValue(vi.CheckoutDate.ToShortDateString());
                            row.CreateCell(6).SetCellValue(vi.BookedDate.ToShortDateString());
                            row.CreateCell(7).SetCellValue(vi.ReservationStatus);
                            row.CreateCell(8).SetCellValue(vi.RoomNights);
                            row.CreateCell(9).SetCellValue((double)vi.FlatAmount);
                            row.CreateCell(10).SetCellValue((double)vi.RoomRevenue);
                            row.CreateCell(11).SetCellValue((double)vi.GrossSaleWithTax);
                            row.CreateCell(12).SetCellValue((double)vi.CostOfHotelWithTax);
                            row.CreateCell(13).SetCellValue((double)vi.CardProcessingFees);
                            row.CreateCell(14).SetCellValue((double)vi.NetProfit);
                            row.CreateCell(15).SetCellValue((double)vi.AffiliteCommissionPercentage);
                            row.CreateCell(16).SetCellValue((double)vi.ARNTransactionFee);
                            row.CreateCell(17).SetCellValue((double)vi.ARNCallCenterFee);
                            row.CreateCell(18).SetCellValue((double)vi.NetCommission);
                            row.CreateCell(19).SetCellValue((double)vi.SubAffiliateCommission);
                            row.CreateCell(20).SetCellValue((double)vi.NetCommissionAfterSubAffiliate);
                            row.CreateCell(21).SetCellValue(vi.CID);
                            row.CreateCell(22).SetCellValue(vi.ReservationId);
                            row.CreateCell(23).SetCellValue(vi.RegistrationId);
                            row.CreateCell(24).SetCellValue(vi.RegistrationName);
                            row.CreateCell(25).SetCellValue(vi.Message);

                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Bad Adjustments Archive

                        ISheet excelSheet11 = workbook.CreateSheet("Bad Adjustments Archive");
                        row = excelSheet11.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Confirmation");
                        row.CreateCell(1).SetCellValue("Property");
                        row.CreateCell(2).SetCellValue("Guest");
                        row.CreateCell(3).SetCellValue("Comm Adj$");
                        row.CreateCell(4).SetCellValue("Notes");
                        row.CreateCell(5).SetCellValue("Outcome");

                        ct = 1;

                        foreach (var vi in _importService.VendorAdjustment.RejectedItems)
                        {
                            row = excelSheet11.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue(vi.Confirmation);
                            row.CreateCell(1).SetCellValue(vi.Property);
                            row.CreateCell(2).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(3).SetCellValue((double)vi.CommissionAdjustment);
                            row.CreateCell(4).SetCellValue(vi.Notes);
                            row.CreateCell(5).SetCellValue(vi.Message);

                        }

                        //workbook.Write(fs);

                        #endregion

                        #region Bad Adjustments Brokers

                        ISheet excelSheet12 = workbook.CreateSheet("Bad Adjustments Brokers");
                        row = excelSheet12.CreateRow(0);

                        row.CreateCell(0).SetCellValue("Confirmation");
                        row.CreateCell(1).SetCellValue("Property");
                        row.CreateCell(2).SetCellValue("Guest");
                        row.CreateCell(3).SetCellValue("Comm Adj$");
                        row.CreateCell(4).SetCellValue("Notes");
                        row.CreateCell(5).SetCellValue("Outcome");

                        ct = 1;

                        foreach (var vi in _importService.BookingAdjustment.RejectedItems)
                        {
                            row = excelSheet12.CreateRow(ct);
                            ct++;

                            row.CreateCell(0).SetCellValue(vi.Confirmation);
                            row.CreateCell(1).SetCellValue(vi.Property);
                            row.CreateCell(2).SetCellValue($"{vi.GuestFirstName} {vi.GuestLastName}");
                            row.CreateCell(3).SetCellValue((double)vi.CommissionAdjustment);
                            row.CreateCell(5).SetCellValue(vi.Notes);


                        }

                        //workbook.Write(fs);

                        #endregion


                        workbook.Write(fs);

                        model.message = model.message.Replace("[[URL]]", URL);
                        //model.message = "Success";
                        //model.isSuccess = true;
                    }

                    //using (var stream = new FileStream(Path.Combine(newPath, sFileName), FileMode.Open))
                    //{
                    //    await stream.CopyToAsync(memory);
                    //}
                    //memory.Position = 0;

                    //return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

                    
                }
            }
            catch (Exception ex)
            {
                model.isSuccess = false;
                model.message = ex.Message;
            }

            
            return model;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
