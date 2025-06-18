using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorImport.Service.Data;
using VendorImport.Service.Interfaces;
using VendorImport.Service.Models;
using VendorImport.Service.Models.ViewModels;

namespace VendorImport.Service
{
    public class VendorImportService : IVendorImportService
    {
        private readonly VendorDbContext _context;

        public VendorImportService(VendorDbContext context)
        {
            _context = context;
        }

        public async Task<VendorResult<AdjustmentModel>> AddAdjustmentAsync(AdjustmentModel model)
        {
            VendorResult<AdjustmentModel> returnObj = new VendorResult<AdjustmentModel>();

            try
            {
                if (_context.Adjustments.Count(c => c.Confirmation == model.Confirmation) < 1)
                {
                    await _context.Adjustments.AddAsync(model);
                    await _context.SaveChangesAsync();
                    returnObj.Item = model;

                    returnObj.Message = "Success";
                    returnObj.IsSuccess = true;
                }
                else
                {
                    returnObj.Item = model;
                    returnObj.Message = "Already added";
                    returnObj.IsSuccess = true;
                }

            }
            catch(Exception ex)
            {
                if (returnObj == null)
                    returnObj = new VendorResult<AdjustmentModel>();

                returnObj.IsSuccess = false;
                returnObj.Message = ex.Message;
            }

            return returnObj;
        }

        public async Task<VendorResults<AdjustmentModel>> AddAdjustmentsAsync(List<AdjustmentModel> model)
        {
            VendorResults<AdjustmentModel> returnObj = new VendorResults<AdjustmentModel>();

            try
            {
                returnObj.RejectedItems = (from a in _context.Adjustments
                                           join m in model on a.Confirmation equals m.Confirmation
                                           select a).ToList();

                returnObj.RejectedItems.All(c => { c.Message = "Already In Table"; return true; });

                List<AdjustmentModel> addModel = (from m in model
                                            join a in _context.Adjustments on m.Confirmation equals a.Confirmation into gj
                                            from subset in gj.DefaultIfEmpty()
                                            where subset == null
                                            select m).ToList();

                if (addModel.Count > 0)
                {
                    await _context.Adjustments.AddRangeAsync(addModel);
                    await _context.SaveChangesAsync();
                    returnObj.AddedItems = addModel;
                }

                returnObj.Message = "Success";
                returnObj.IsSuccess = true;
            }
            catch (Exception ex)
            {
                if (returnObj == null)
                    returnObj = new VendorResults<AdjustmentModel>();

                returnObj.IsSuccess = false;
                returnObj.Message = ex.Message;
            }

            return returnObj;
        }

        public async Task<VendorResult<InventoryReservationModel>> AddInventoryReservationAsync(InventoryReservationModel model)
        {
            VendorResult<InventoryReservationModel> returnObj = new VendorResult<InventoryReservationModel>();

            try
            {
                if (_context.InventoryReservations.Count(c => c.Confirmation == model.Confirmation) < 1)
                {
                    await _context.InventoryReservations.AddAsync(model);
                    await _context.SaveChangesAsync();
                    returnObj.Item = model;

                    returnObj.Message = "Success";
                    returnObj.IsSuccess = true;
                }
                else
                {
                    returnObj.Item = model;
                    returnObj.Message = "Already added";
                    returnObj.IsSuccess = true;
                }

            }
            catch (Exception ex)
            {
                if (returnObj == null)
                    returnObj = new VendorResult<InventoryReservationModel>();

                returnObj.IsSuccess = false;
                returnObj.Message = ex.Message;
            }

            return returnObj;
        }

        public async Task<VendorResults<InventoryReservationModel>> AddInventoryReservationsAsync(List<InventoryReservationModel> model)
        {
            VendorResults<InventoryReservationModel> returnObj = new VendorResults<InventoryReservationModel>();

            try
            {
                returnObj.RejectedItems = (from a in _context.InventoryReservations
                                           join m in model on a.Confirmation equals m.Confirmation
                                           select a).ToList();

                returnObj.RejectedItems.All(c => { c.Message = "Already In Table"; return true; });

                List<InventoryReservationModel> addModel = (from m in model
                                                  join a in _context.InventoryReservations on m.Confirmation equals a.Confirmation into gj
                                                  from subset in gj.DefaultIfEmpty()
                                                  where subset == null
                                                  select m).ToList();
                if (addModel.Count > 0)
                {
                    await _context.InventoryReservations.AddRangeAsync(addModel);
                    await _context.SaveChangesAsync();

                    returnObj.AddedItems = addModel;
                }

                returnObj.Message = "Success";
                returnObj.IsSuccess = true;
            }
            catch (Exception ex)
            {
                if (returnObj == null)
                    returnObj = new VendorResults<InventoryReservationModel>();

                returnObj.IsSuccess = false;
                returnObj.Message = ex.Message;
            }

            return returnObj;
        }

        public async Task<VendorResult<MerchantInventoryReservationModel>> AddMerchantInventoryReservationAsync(MerchantInventoryReservationModel model)
        {
            VendorResult<MerchantInventoryReservationModel> returnObj = new VendorResult<MerchantInventoryReservationModel>();

            try
            {
                if (_context.MerchantInventoryReservations.Count(c => c.Confirmation == model.Confirmation) < 1)
                {
                    await _context.MerchantInventoryReservations.AddAsync(model);
                    await _context.SaveChangesAsync();
                    returnObj.Item = model;

                    returnObj.Message = "Success";
                    returnObj.IsSuccess = true;
                }
                else
                {
                    returnObj.Item = model;
                    returnObj.Message = "Already added";
                    returnObj.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                if (returnObj == null)
                    returnObj = new VendorResult<MerchantInventoryReservationModel>();

                returnObj.IsSuccess = false;
                returnObj.Message = ex.Message;
            }

            return returnObj;
        }

        public async Task<VendorResults<MerchantInventoryReservationModel>> AddMerchantInventoryReservationsAsync(List<MerchantInventoryReservationModel> model)
        {
            VendorResults<MerchantInventoryReservationModel> returnObj = new VendorResults<MerchantInventoryReservationModel>();

            try
            {
                returnObj.RejectedItems = (from a in _context.MerchantInventoryReservations
                                           join m in model on a.Confirmation equals m.Confirmation
                                           select a).ToList();

                returnObj.RejectedItems.All(c => { c.Message = "Already In Table"; return true; });

                List<MerchantInventoryReservationModel> addModel = (from m in model
                                                            join a in _context.MerchantInventoryReservations on m.Confirmation equals a.Confirmation into gj
                                                            from subset in gj.DefaultIfEmpty()
                                                            where subset == null
                                                            select m).ToList();
                if (addModel.Count > 0)
                {
                    await _context.MerchantInventoryReservations.AddRangeAsync(addModel);
                    await _context.SaveChangesAsync();

                    returnObj.AddedItems = addModel;
                }

                returnObj.Message = "Success";
                returnObj.IsSuccess = true;
            }
            catch (Exception ex)
            {
                if (returnObj == null)
                    returnObj = new VendorResults<MerchantInventoryReservationModel>();

                returnObj.IsSuccess = false;
                returnObj.Message = ex.Message;
            }

            return returnObj;
        }
    }
}
