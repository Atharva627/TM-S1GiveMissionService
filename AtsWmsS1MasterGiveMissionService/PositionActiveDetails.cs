using AtsWmsS1MasterGiveMissionService.ats_tata_metallics_dbDataSetTableAdapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AtsWmsS1MasterGiveMissionService.ats_tata_metallics_dbDataSet;

namespace AtsWmsS1MasterGiveMissionService
{
    class PositionActiveDetails
    {
        static string className = "PositionActiveDetails";
        private static readonly ILog Log = LogManager.GetLogger(className);

        #region DataTables
        ats_wms_mapping_floor_area_detailsDataTable ats_wms_mapping_floor_area_detailsDataTableDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableDT = null;
        ats_wms_master_rack_detailsDataTable ats_wms_master_rack_detailsDataTableDT = null;
        #endregion

        #region TableAdapters
        ats_wms_mapping_floor_area_detailsTableAdapter ats_wms_mapping_floor_area_detailsTableAdapterInstance = new ats_wms_mapping_floor_area_detailsTableAdapter();
        ats_wms_master_position_detailsTableAdapter ats_wms_master_position_detailsTableAdapterInstance = new ats_wms_master_position_detailsTableAdapter();
        ats_wms_master_rack_detailsTableAdapter ats_wms_master_rack_detailsTableAdapterInstance = new ats_wms_master_rack_detailsTableAdapter();
        #endregion


        public Boolean isFloorInfeedActive(int floorId, int areaId)
        {
            Log.Debug("In isFloorInfeedActive");

            try
            {
                ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByAREA_IDAndFLOOR_IDAndINFEED_IS_ACTIVE(areaId, floorId, 1);

                if (ats_wms_mapping_floor_area_detailsDataTableDT != null && ats_wms_mapping_floor_area_detailsDataTableDT.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("atsWmsA2GiveMissionOperation :: Exception while checking infeed is active :: " + ex.Message + " stactTrace :: " + ex.StackTrace);
            }
            return false;
        }


        public Boolean isFloorOutfeedActive(int floorId, int areaId)
        {
            Log.Debug("In isFloorOutfeedActive");

            try
            {
                ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByAREA_IDAndFLOOR_IDAndOUTFEED_IS_ACTIVE(areaId, floorId, 1);

                if (ats_wms_mapping_floor_area_detailsDataTableDT != null && ats_wms_mapping_floor_area_detailsDataTableDT.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Log.Error("atsWmsA2GiveMissionOperation :: Exception while checking outfeed is active :: " + ex.Message + " stactTrace :: " + ex.StackTrace);
            }
            return false;
        }


        public Boolean isPositionActive(int positionId)
        {
            Log.Debug("IN isPositionActive");

            try
            {
                ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_IDAndPOSITION_IS_ACTIVE(positionId, 1);
                if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("atsWmsA2GiveMissionOperation :: Exception while infeed is active :: " + ex.Message + " stactTrace :: " + ex.StackTrace);
            }
            return false;
        }



        public Boolean isRackActive(int rackId)
        {
            Log.Debug("IN isPositionActive");

            try
            {
                ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(rackId, 1);
                if (ats_wms_master_rack_detailsDataTableDT != null && ats_wms_master_rack_detailsDataTableDT.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("atsWmsA2GiveMissionOperation :: Exception while infeed is active :: " + ex.Message + " stactTrace :: " + ex.StackTrace);
            }
            return false;
        }
    }
}
