using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendorImport.Service.Models;
using VendorImport.Service.Models.ViewModels;

namespace VendorImport.Service.Interfaces
{
    public interface IVendorImportService
    {
        Task<VendorResults<AdjustmentModel>> AddAdjustmentsAsync(List<AdjustmentModel> model);
        Task<VendorResult<AdjustmentModel>> AddAdjustmentAsync(AdjustmentModel model);
        Task<VendorResults<MerchantInventoryReservationModel>> AddMerchantInventoryReservationsAsync(List<MerchantInventoryReservationModel> model);
        Task<VendorResult<MerchantInventoryReservationModel>> AddMerchantInventoryReservationAsync(MerchantInventoryReservationModel model);
        Task<VendorResults<InventoryReservationModel>> AddInventoryReservationsAsync(List<InventoryReservationModel> model);
        Task<VendorResult<InventoryReservationModel>> AddInventoryReservationAsync(InventoryReservationModel model);

    }
}
