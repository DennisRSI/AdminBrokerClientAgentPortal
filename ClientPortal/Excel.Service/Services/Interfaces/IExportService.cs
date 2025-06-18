using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendorImport.Service.Models;
using VendorImport.Service.Models.ViewModels;

namespace Excel.Service.Services.Interfaces
{
    public interface IExportService
    {
        Task<(bool isSuccess, string message)> ImportFile(IFormFile file);
        VendorResults<InventoryReservationModel> VendorInventory { get; set; }
        VendorResults<MerchantInventoryReservationModel> VendorMerchant { get; set; }
        VendorResults<AdjustmentModel> VendorAdjustment { get; set; }
        VendorResults<InventoryReservationModel> BookingInventory { get; set; }
        VendorResults<MerchantInventoryReservationModel> BookingMerchant { get; set; }
        VendorResults<AdjustmentModel> BookingAdjustment { get; set; }
        //    VendorResults<AdjustmentModel> vendorAdjustment = null,
        //    VendorResults<InventoryReservationModel> bookingInventory = null,
        //    VendorResults<MerchantInventoryReservationModel> bookingMerchant = null,
        //    VendorResults<AdjustmentModel> bookingAdjustment = null);
        //Task<(bool isSuccess, string message)> ExportFile(
        //    VendorResults<InventoryReservationModel> vendorInventory = null,
        //    VendorResults<MerchantInventoryReservationModel> vendorMerchant = null,
        //    VendorResults<AdjustmentModel> vendorAdjustment = null,
        //    VendorResults<InventoryReservationModel> bookingInventory = null,
        //    VendorResults<MerchantInventoryReservationModel> bookingMerchant = null,
        //    VendorResults<AdjustmentModel> bookingAdjustment = null);
    }
}
