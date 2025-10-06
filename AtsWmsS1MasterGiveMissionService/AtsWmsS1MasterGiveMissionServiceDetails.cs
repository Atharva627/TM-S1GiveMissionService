using System.Collections.Concurrent;
using log4net;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using static AtsWmsS1MasterGiveMissionService.ats_tata_metallics_dbDataSet;
using AtsWmsS1MasterGiveMissionService.ats_tata_metallics_dbDataSetTableAdapters;
using System.Threading;
using System.Collections;

namespace AtsWmsS1MasterGiveMissionService
{
    public class FetchMissionParameters
    {
        public string missionType;
        public int checkID;
        public int palletInformationID;
        public int stationID;
    }

    class AtsWmsS1MasterGiveMissionServiceDetails
    {

        #region DataTables
        ats_wms_master_plc_connection_detailsDataTable ats_wms_master_plc_connection_detailsDataTableDT = null;
        ats_wms_outfeed_mission_runtime_detailsDataTable ats_wms_outfeed_mission_runtime_detailsDataTableDT = null;
        ats_wms_outfeed_mission_runtime_detailsDataTable ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject = null;
        ats_wms_outfeed_mission_runtime_detailsDataTable ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT = null;
        ats_wms_infeed_mission_runtime_detailsDataTable ats_wms_infeed_mission_runtime_detailsDataTableDT = null;
        ats_wms_infeed_mission_runtime_detailsDataTable ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT = null;
        ats_wms_transfer_pallet_mission_runtime_detailsDataTable ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT = null;
        ats_wms_transfer_pallet_mission_runtime_detailsDataTable ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT = null;
        ats_wms_master_decision_point_direction_detailsDataTable ats_wms_master_decision_point_direction_detailsDataTableDT = null;
        ats_wms_master_decision_point_direction_detailsDataTable ats_wms_master_decision_point_direction_detailsDataTableDataRequestDT = null;
        ats_wms_master_decision_point_direction_detailsDataTable ats_wms_master_decision_point_direction_detailsDataTableTaskCompletionDT = null;
        ats_wms_master_decision_point_direction_detailsDataTable ats_wms_master_decision_point_direction_detailsDataTableTaskConfirmationDT = null;
        ats_wms_master_pallet_informationDataTable ats_wms_master_pallet_informationDataTableDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableTemptDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTablePrevTRDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableRackPalletsDT = null;
        ats_wms_master_stacker_tag_detailsDataTable ats_wms_master_stacker_tag_detailsDataTableDT = null;
        ats_wms_master_stacker_tag_detailsDataTable ats_wms_master_stacker_tag_detailsDataTableSourceDT = null;
        ats_wms_master_stacker_tag_detailsDataTable ats_wms_master_stacker_tag_detailsDataTableDestinationDT = null;
        ats_wms_master_stacker_tag_detailsDataTable ats_wms_master_stacker_tag_detailsDataTableFeedbackDT = null;
        ats_wms_master_stacker_tag_detailsDataTable ats_wms_master_stacker_tag_detailsDataTableSourceDTFeedBack = null;
        ats_wms_master_stacker_tag_detailsDataTable ats_wms_master_stacker_tag_detailsDataTableDestinationDTFeedBack = null;
        ats_wms_master_rack_detailsDataTable ats_wms_master_rack_detailsDataTableDT = null;
        ats_wms_master_rack_detailsDataTable ats_wms_master_rack_detailsDataTablePrevTRDT = null;
        ats_wms_check_a1_mission_detailsDataTable ats_wms_check_a1_mission_detailsDataTableDT = null;
        ats_wms_transfer_pallet_mission_runtime_detailsDataTable ats_wms_transfer_pallet_mission_runtime_detailsDataTableAlarmTransferDT = null;
        ats_wms_loading_stations_tag_detailsDataTable ats_wms_loading_stations_tag_detailsDataTableDT = null;
        ats_wms_loading_stations_tag_detailsDataTable ats_wms_loading_stations_tag_detailsDataTableDTReworkReject = null;
        ats_wms_master_task_confirmation_tag_detailsDataTable ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR = null;

        #endregion

        #region TableAdapters
        ats_wms_master_plc_connection_detailsTableAdapter ats_wms_master_plc_connection_detailsTableAdapterInstance = new ats_wms_master_plc_connection_detailsTableAdapter();
        ats_wms_outfeed_mission_runtime_detailsTableAdapter ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance = new ats_wms_outfeed_mission_runtime_detailsTableAdapter();
        ats_wms_infeed_mission_runtime_detailsTableAdapter ats_wms_infeed_mission_runtime_detailsTableAdapterInstance = new ats_wms_infeed_mission_runtime_detailsTableAdapter();
        ats_wms_transfer_pallet_mission_runtime_detailsTableAdapter ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance = new ats_wms_transfer_pallet_mission_runtime_detailsTableAdapter();
        ats_wms_master_decision_point_direction_detailsTableAdapter ats_wms_master_decision_point_direction_detailsTableAdapterInstance = new ats_wms_master_decision_point_direction_detailsTableAdapter();
        ats_wms_master_pallet_informationTableAdapter ats_wms_master_pallet_informationTableAdapterInstance = new ats_wms_master_pallet_informationTableAdapter();
        ats_wms_master_position_detailsTableAdapter ats_wms_master_position_detailsTableAdapterInstance = new ats_wms_master_position_detailsTableAdapter();
        ats_wms_master_stacker_tag_detailsTableAdapter ats_wms_master_stacker_tag_detailsTableAdapterInstance = new ats_wms_master_stacker_tag_detailsTableAdapter();
        ats_wms_master_stacker_tag_detailsTableAdapter ats_wms_master_stacker_tag_detailsTableAdapterSourceInstance = new ats_wms_master_stacker_tag_detailsTableAdapter();
        ats_wms_master_stacker_tag_detailsTableAdapter ats_wms_master_stacker_tag_detailsTableAdapterSourceFeedBackInstance = new ats_wms_master_stacker_tag_detailsTableAdapter();
        ats_wms_master_stacker_tag_detailsTableAdapter ats_wms_master_stacker_tag_detailsTableAdapterDestinationFeedBackInstance = new ats_wms_master_stacker_tag_detailsTableAdapter();
        ats_wms_master_rack_detailsTableAdapter ats_wms_master_rack_detailsTableAdapterInstance = new ats_wms_master_rack_detailsTableAdapter();
        ats_wms_check_a1_mission_detailsTableAdapter ats_wms_check_a1_mission_detailsTableAdapterInstance = new ats_wms_check_a1_mission_detailsTableAdapter();
        ats_wms_transfer_pallet_mission_runtime_detailsTableAdapter ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterAlarmTransferInstance = new ats_wms_transfer_pallet_mission_runtime_detailsTableAdapter();
        ats_wms_loading_stations_tag_detailsTableAdapter ats_wms_loading_stations_tag_detailsTableAdapterInstance = new ats_wms_loading_stations_tag_detailsTableAdapter();
        ats_wms_master_task_confirmation_tag_detailsTableAdapter ats_wms_master_task_confirmation_tag_detailsTableAdapterInstance = new ats_wms_master_task_confirmation_tag_detailsTableAdapter();
        #endregion

        #region PLC PING VARIABLE   
        //private string IP_ADDRESS = System.Configuration.ConfigurationManager.AppSettings["IP_ADDRESS"]; //2
        private Ping pingSenderForThisConnection = null;
        private PingReply replyForThisConnection = null;
        private Boolean pingStatus = false;
        private int serverPingStatusCount = 0;
        #endregion

        #region KEPWARE VARIABLES

        /* Kepware variable*/

        OPCServer ConnectedOpc = new OPCServer();

        Array OPCItemIDs = Array.CreateInstance(typeof(string), 100);
        Array ItemServerHandles = Array.CreateInstance(typeof(Int32), 100);
        Array ItemServerErrors = Array.CreateInstance(typeof(Int32), 100);
        Array ClientHandles = Array.CreateInstance(typeof(Int32), 100);
        Array RequestedDataTypes = Array.CreateInstance(typeof(Int16), 100);
        Array AccessPaths = Array.CreateInstance(typeof(string), 100);
        Array ItemServerValues = Array.CreateInstance(typeof(string), 100);
        OPCGroup OpcGroupNames;
        //object yDIR;
        //object yDIR;
        object tDIR11;
        object yDIR11;
        // Connection string
        static string plcServerConnectionString = null;

        #endregion

        #region Global Variables
        static string className = "AtsWmsS1MasterGiveMissionServiceDetails";
        private static readonly ILog Log = LogManager.GetLogger(className);
        private System.Timers.Timer a1MasterGiveMissionTimer = null;

        private static ConcurrentQueue<FetchMissionParameters> missionQueue = new ConcurrentQueue<FetchMissionParameters>();
        private static bool isDispatcherRunning = false;

        int palletPresentOnStackerPickupPosition = 0;
        string palletCodeOnStackerPickupPosition = "";
        int areaId = 1;
        int stackerRightSide = 2;
        int stackerLeftSide = 1;
        public int stackerAreaSide = 0;
        public int stackerFloor = 0;
        public int stackerColumn = 0;
        int positionNumberInRack = 0;
        int sourcePositionTagType = 0;
        int destinationPositionTagType = 1;
        int feedbackTagType = 1;
        public int destinationPositionNumberInRack = 1;
        int infeedTaskType = 1;
        int outfeedTaskType = 2;
        int transferTaskType = 3;
        int alarmTaskType = 4;
        int racksideLeft = 1;
        int racksideRight = 2;
        int CS6LoadingPickSideColumn = 8;
        int CS5LoadingPickSideColumn = 14;
        int CS4LoadingPickSideColumn = 19;
        //int CS4LoadingPlaceSideColumn = CS4LoadingPickSideColumn - 1;
        int S4LoadingSideColumn = 0;
        int ReworkRejectSideColumn = 5;
        int tankColumn = 19;
        int startTargetFloorId = 1;
        int infeedAndOutfeedDepth = 1;
        int v1 = 0;
        int v2 = 0;
        int v3 = 0;
        int v4 = 0;
        int v5 = 0;
        int v6 = 0;
        int v7 = 0;
        int v8 = 0;
        int v9 = 0;
        int v10 = 0;
        int transferMissionCheckId = 6;
        string transferMissionType = "Transfer";
        string IP_ADDRESS = "";
        int STACKER_1_DATA_REQUEST = 5;
        int STACKER_1 = 1;

        string MISSION_PALLET_CODE = "ATS.WMS_STACKER_1.STACKER_1_MISSION_PALLET_CODE";


        #endregion

        public void startOperation()
        {
            try
            {
                Log.Debug("1");
                //Timer 
                a1MasterGiveMissionTimer = new System.Timers.Timer();
                //Running the function after 1 sec 
                a1MasterGiveMissionTimer.Interval = (1000);
                //After 1 sec timer will elapse and DataFetchDetailsOperation function will be called 
                a1MasterGiveMissionTimer.Elapsed += new System.Timers.ElapsedEventHandler(a1MasterGiveMissionOperation);
                //Timr autoreset flase
                a1MasterGiveMissionTimer.AutoReset = false;
                //starting the timer
                a1MasterGiveMissionTimer.Start();
                //starting Dispatcher
                Task.Run(() => StartMissionDispatcherAsync());

            }
            catch (Exception ex)
            {
                Log.Error("startOperation :: Exception Occure in a1MasterGiveMissionTimer" + ex.Message);
            }
        }

        private async Task StartMissionDispatcherAsync()
        {
            if (isDispatcherRunning) return;
            isDispatcherRunning = true;

            while (true)
            {
                try
                {
                    if (!missionQueue.IsEmpty && readTag("ATS.WMS_STACKER_1.STACKER_1_DATA_REQUEST") == "True")
                    {
                        FetchMissionParameters mission;
                        if (missionQueue.TryDequeue(out mission))
                        {
                            //Log.Debug($"Dispatching mission: {mission.missionType}, Type: {mission.taskType}");
                            //giveMissionToStacker(mission);
                            Log.Debug($"Dispatching missionType: {mission.missionType}, checkID: {mission.checkID}, palletInformationID: {mission.palletInformationID}, stationID: {mission.stationID}");
                            fetchMissionDetails(mission.missionType, mission.checkID, mission.palletInformationID, mission.stationID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Mission Dispatcher Error: " + ex.Message + " StackTrace: " + ex.StackTrace);
                }

                await Task.Delay(1000); // Poll every second
            }
        }

        public void a1MasterGiveMissionOperation(object sender, EventArgs args)
        {
            try
            {
                Log.Debug("2");
                try
                {
                    //Stopping the timer to start the below operation
                    a1MasterGiveMissionTimer.Stop();
                }
                catch (Exception ex)
                {
                    Log.Error("a1MasterGiveMissionOperation :: Exception occure while stopping the timer :: " + ex.Message + "StackTrace  :: " + ex.StackTrace);
                }

                try
                {
                    //Fetching PLC data from DB by sending PLC connection IP address

                    ats_wms_master_plc_connection_detailsDataTableDT = ats_wms_master_plc_connection_detailsTableAdapterInstance.GetData();
                    IP_ADDRESS = ats_wms_master_plc_connection_detailsDataTableDT[0].PLC_CONNECTION_IP_ADDRESS;
                    Log.Debug("2.1.1 :: IP_ADDRESS ::" + IP_ADDRESS);

                }
                catch (Exception ex)
                {
                    Log.Error("a1MasterGiveMissionOperation :: Exception Occure while reading machine datasource connection details from the database :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                }


                // Check PLC Ping Status
                try
                {
                    //Checking the PLC ping status by a method
                    pingStatus = checkPlcPingRequest();
                    Log.Debug("2.2");
                }
                catch (Exception ex)
                {
                    Log.Error("a1MasterGiveMissionOperation :: Exception while checking plc ping status :: " + ex.Message + " stactTrace :: " + ex.StackTrace);
                }

                if (pingStatus == true)
                //if (true)
                {
                    try
                    {
                        Log.Debug("3");
                        //checking if the PLC data from DB is retrived or not
                        if (ats_wms_master_plc_connection_detailsDataTableDT != null && ats_wms_master_plc_connection_detailsDataTableDT.Count != 0)
                        //if (true)
                        {
                            try
                            {
                                plcServerConnectionString = ats_wms_master_plc_connection_detailsDataTableDT[0].PLC_CONNECTION_URL;
                            }
                            catch (Exception ex)
                            {
                                Log.Error("a1MasterGiveMissionOperation :: Exception occured while getting plcServerConnectionString ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                            }
                            try
                            {
                                //Calling the connection method for PLC connection
                                OnConnectPLC();
                            }
                            catch (Exception ex)
                            {
                                Log.Error("a1MasterGiveMissionOperation :: Exception while connecting to plc :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                            }

                            try
                            {
                                // Check the PLC connected status
                                if (ConnectedOpc.ServerState.ToString().Equals("1"))
                                //if (true)
                                {
                                    Log.Debug("4");
                                    //Bussiness logic




                                    try
                                    {
                                        Log.Debug("4 : Check wheather the missions are in progress");
                                        Log.Debug("4A : Fetching Inprogress Outfeed Mission");
                                        try
                                        {
                                            //Fetching inprogress outfeed missions by sending area ID and mission status (to check if any outfeed mission is not running then infeed mission can be given)
                                            ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByAREA_IDAndOUTFEED_MISSION_STATUSAndSTACKER_ID(areaId, "IN_PROGRESS", 1);
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation :: Exception occure while fetching outfeed mission data :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                        }

                                        Log.Debug("4B : Fetching Inprogress Infeed Mission");
                                        try
                                        {
                                            //Fetching inprogress infeed missions by sending area ID and mission status (to check if any outfeed mission is not running then infeed mission can be given)
                                            ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByAREA_IDAndINFEED_MISSION_STATUSAndSTACKER_ID(areaId, "IN_PROGRESS", 1);
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation :: Exception occure while fetching infeed mission data :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                        }

                                        Log.Debug("4C : Fetching Inprogress Transfer Mission");
                                        try
                                        {
                                            //Fetching inprogress transfer missions by sending area ID and mission status (to check if any outfeed mission is not running then infeed mission can be given)
                                            //ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByTRANSFER_MISSION_STATUSAndAREA_IDAndSTACKER_ID("IN_PROGRESS", areaId, 1);
                                            ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByTRANSFER_MISSION_STATUSAndSTACKER_ID("IN_PROGRESS", 1);
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation :: Exception occure while fetching transfer mission data :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                        }
                                        Log.Debug("4D : Fetching Inprogress Alarm Mission");
                                        try
                                        {
                                            //Fetching inprogress alarm mission by sendingarea ID and mission status (to check if any alarm mission is not running )
                                            //ats_wms_tempreture_alarm_mission_runtime_detailsDataTableDT = ats_wms_tempreture_alarm_mission_runtime_detailsTableAdapterInstance.GetDataByAREA_IDAndALARM_MISSION_STATUS(areaId, "IN_PROGRESS");
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation::Exception occure while fetching Alarm mission data:: " + ex.Message + "StackTrace:: " + ex.StackTrace);
                                        }

                                        Log.Debug("5 : Checking inprogress mission count is 0 of infeed,outfeed,tranfer and alarm missions");
                                        try
                                        {
                                            //checking inprogress mission count is 0
                                            if (ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT != null && ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT.Count == 0 &&
                                                ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT.Count == 0 &&
                                                ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT != null && ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT.Count == 0)
                                            //&& ats_wms_tempreture_alarm_mission_runtime_detailsDataTableDT != null && ats_wms_tempreture_alarm_mission_runtime_detailsDataTableDT.Count == 0)
                                            {
                                                Log.Debug("6 : Fetching is checked misssion points");
                                                try
                                                {
                                                    //fetching is checked mission points, to check the unchecked mission points between filled pallet infeed, empty pallet outfeed, filled pallet outfeed and empty pallet infeed
                                                    ats_wms_check_a1_mission_detailsDataTableDT = ats_wms_check_a1_mission_detailsTableAdapterInstance.GetDataByIS_CHECKED(0);
                                                    if (ats_wms_check_a1_mission_detailsDataTableDT != null && ats_wms_check_a1_mission_detailsDataTableDT.Count > 0)
                                                    {
                                                        Log.Debug("7 : Check for each mission point");
                                                        //checking for each mission point
                                                        for (int i = 0; i < ats_wms_check_a1_mission_detailsDataTableDT.Count; i++)
                                                        {
                                                            Thread.Sleep(100);
                                                            try
                                                            {
                                                                Thread.Sleep(500);
                                                                Log.Debug("8 : ats_wms_check_a1_mission_detailsDataTableDT ID :: " + ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                //check for infeed mission
                                                                if (ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID == 4)
                                                                {
                                                                    Log.Debug("8A : Check for EMPTY mission");
                                                                    try
                                                                    {
                                                                        Log.Debug("EMPTY");
                                                                        checkStackerMissionRequest("EMPTY", ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(1, ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        Log.Error("giveMissionToStacker1Operation :: Exception occured while calling checkStackerMissionRequest and updating is check 1 for infeed mission ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                                    }
                                                                }
                                                                //check for outfeed mission
                                                                else if (ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID == 1)
                                                                {
                                                                    Log.Debug("8B : Check for INFEED mission");
                                                                    try
                                                                    {
                                                                        Log.Debug("INFEED");
                                                                        checkStackerMissionRequest("INFEED", ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(1, ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        Log.Error("giveMissionToStacker1Operation :: Exception occured while calling checkStackerMissionRequest and updating is check 1 for outfeed mission ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                                    }
                                                                }
                                                                //check for transfer missison 
                                                                else if (ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID == 2)
                                                                {
                                                                    Log.Debug("8C : Check for OUTFEED mission");
                                                                    try
                                                                    {
                                                                        Log.Debug("OUTFEED");
                                                                        checkStackerMissionRequest("OUTFEED", ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(1, ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        Log.Error("giveMissionToStacker1Operation :: Exception occured while calling checkStackerMissionRequest and updating is check 1 for transfer mission ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                                    }
                                                                }
                                                                //check for Empty missison 
                                                                else if (ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID == 3)
                                                                // else if (ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID == 4 || ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID == 3 || ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID == 5)
                                                                {
                                                                    Log.Debug("8C : Check for Transfer mission");
                                                                    try
                                                                    {
                                                                        Log.Debug("TRANSFER MISSION");
                                                                        checkStackerMissionRequest("TRANSFER", ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(1, ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        Log.Error("giveMissionToStacker1Operation :: Exception occured while calling checkStackerMissionRequest and updating is check 1 for transfer mission ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                                    }
                                                                }
                                                                //else if (ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID == 5)
                                                                //{
                                                                //    Log.Debug("8C : Check for REWORK/REJECT mission");
                                                                //    try
                                                                //    {
                                                                //        Log.Debug("REWORK/REJECT MISSION");
                                                                //        checkStackerMissionRequest("REWORK-REJECT", ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                //        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(1, ats_wms_check_a1_mission_detailsDataTableDT[i].CHECK_A1_MISSION_DETAILS_ID);
                                                                //    }
                                                                //    catch (Exception ex)
                                                                //    {
                                                                //        Log.Error("giveMissionToStacker1Operation :: Exception occured while calling checkStackerMissionRequest and updating is check 1 for transfer mission ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                                //    }

                                                                //}
                                                                break;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Log.Error("giveMissionToStacker1Operation :: Exception occured while checking mission deatils::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                            }
                                                        }

                                                        Log.Debug("9 : Updating Is_Check 0 to all");
                                                        try
                                                        {
                                                            ats_wms_check_a1_mission_detailsDataTableDT = ats_wms_check_a1_mission_detailsTableAdapterInstance.GetDataByIS_CHECKED(0);
                                                            if (ats_wms_check_a1_mission_detailsDataTableDT != null && ats_wms_check_a1_mission_detailsDataTableDT.Count == 0)
                                                            {
                                                                ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKED(0);
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Log.Error("giveMissionToStacker1Operation :: Exception occured while updating is check 0 to all::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Log.Error("giveMissionToStacker1Operation :: Exception occured while getting is checked  = 0 data ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation :: Exception occured while checking infeed and outfeed mission inprogress data ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error("giveMissionToStacker1Operation :: Exception occure while getting pallet present :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                    }





                                }
                                else
                                {
                                    //Reconnect to plc
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error("a1MasterGiveMissionOperation :: Exception occured while checking server state is 1 ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                            }
                        }
                        else
                        {
                            //Reconnect to plc, Check Ip address, url
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("a1MasterGiveMissionOperation :: Exception occured while checking PLC connection details ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {

                Log.Error("startOperation :: Exception occured while stopping timer :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
            }
            finally
            {
                try
                {
                    //Starting the timer again for the next iteration
                    a1MasterGiveMissionTimer.Start();
                }
                catch (Exception ex1)
                {
                    Log.Error("startOperation :: Exception occured while stopping timer :: " + ex1.Message + " stackTrace :: " + ex1.StackTrace);
                }
            }

        }

        public void checkStackerMissionRequest(string missionType, int checkId)
        {
            Log.Debug("10 : checkStackerMissionRequest");
            //this function is used to check(read) the stacker data request
            //Fetching stacker data request tag from DB by sneding Decision point name 
            //try
            //{
            //    ats_wms_master_decision_point_direction_detailsDataTableDT = ats_wms_master_decision_point_direction_detailsTableAdapterInstance.GetDataByDECISION_POINT_NAME("STACKER_1.STACKER_1_DATA_REQUEST");
            //}
            //catch (Exception ex1)
            //{
            //    Log.Error("startOperation :: Exception occured while while getting data request tag details from DB :: " + ex1.Message + " stackTrace :: " + ex1.StackTrace);
            //}
            for (;;)
            {
                Thread.Sleep(1100);
                try
                {
                    Log.Debug("11 : Stacker data requedt is 1");
                    //Checking if the data request is true or false(true=1 and false=0)

                    //if (ats_wms_master_decision_point_direction_detailsDataTableDT != null && ats_wms_master_decision_point_direction_detailsDataTableDT.Count > 0)
                    {
                        try
                        {
                            Log.Debug("checkStackerMissionRequest :: Found Data Request Tag :: waiting to become True");
                            //Checking if the data request is true or false(by reading the tag, True = data request is high, False = No data request from PLC)
                            //if (readTag(ats_wms_master_decision_point_direction_detailsDataTableDT[0].DECISION_POINT_DIRECTION_TAG_NAME).Equals("True"))
                            try
                            {
                                //ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR = ats_wms_master_task_confirmation_tag_detailsTableAdapterInstance.GetDataBySTACKER_IDAndTAG_TYPE(STACKER_1, STACKER_1_DATA_REQUEST);
                                if (readTag("ATS.WMS_STACKER_1.STACKER_1_DATA_REQUEST").Equals("True"))
                                //if (ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR != null && ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR.Count > 0)
                                {


                                    //if (readTag(ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR[0].TASK_CONFIRMATION_TAG_NAME).Equals("True"))
                                    //{
                                        try
                                        {
                                            Log.Debug("11A : Fetching inprogress outfeed mission");
                                            //Fetching inprogress outfeed missions by sending area ID and mission status (to check if any outfeed mission is not running then infeed mission can be given)
                                            ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByAREA_IDAndOUTFEED_MISSION_STATUSAndSTACKER_ID(areaId, "IN_PROGRESS", 1);
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation :: Exception occured while fetching outfeed mission data ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                        }
                                        try
                                        {
                                            Log.Debug("11B : Fetching inprogress infeed mission");
                                            //Fetching inprogress outfeed missions by sending area ID and mission status (to check if any outfeed mission is not running then infeed mission can be given)
                                            ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByAREA_IDAndINFEED_MISSION_STATUSAndSTACKER_ID(areaId, "IN_PROGRESS", 1);
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation :: Exception occured while fetching infeed mission data ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                        }
                                        try
                                        {
                                            Log.Debug("11C : Fetching inprogress transfer mission");
                                            //Fetching inprogress transfer missions by sending area ID and mission status (to check if any outfeed mission is not running then infeed mission can be given)
                                            ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByTRANSFER_MISSION_STATUSAndAREA_IDAndSTACKER_ID("IN_PROGRESS", areaId, 1);
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation :: Exception occured while fetching transfer mission data ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                        }
                                        try
                                        {
                                            Log.Debug("11d : Fetching inprogress alarm mission");
                                            //Fetching inprogress alarm missions by sending area ID and mission status (to check if any outfeed mission is not running then infeed mission can be given)
                                            //ats_wms_tempreture_alarm_mission_runtime_detailsDataTableInProgressDT = ats_wms_tempreture_alarm_mission_runtime_detailsTableAdapterInprogressInstance.GetDataByAREA_IDAndALARM_MISSION_STATUS(areaId, "IN_PROGRESS");
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("giveMissionToStacker1Operation :: Exception occured while fetching alarm mission data ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                        }
                                        Log.Debug("12 : Check if count is 0");
                                        Log.Debug("ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT.Count :: " + ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT.Count);
                                        Log.Debug("ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT.Count :: " + ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT.Count);
                                        Log.Debug("ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT.Count :: " + ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT.Count);
                                        // Log.Debug("ats_wms_tempreture_alarm_mission_runtime_detailsDataTableInProgressDT.Count :: " + ats_wms_tempreture_alarm_mission_runtime_detailsDataTableInProgressDT.Count);
                                        //Ckecking if the data is available in DB. if count is 0 means there is no outfeed mission currently running.
                                        if (ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableInProgressDT.Count == 0 &&
                                            ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT != null && ats_wms_infeed_mission_runtime_detailsDataTableInProgressDT.Count == 0 &&
                                            ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT != null && ats_wms_transfer_pallet_mission_runtime_detailsDataTableInProgressDT.Count == 0)
                                        // ats_wms_tempreture_alarm_mission_runtime_detailsDataTableInProgressDT != null && ats_wms_tempreture_alarm_mission_runtime_detailsDataTableInProgressDT.Count == 0)

                                        {
                                            Log.Debug("checkStackerMissionRequest::checkStackerMissionRequest :: Recieved stacker Data Request:: 1 ");
                                            //Calling method giveMissionToStacker by sending the pallet information ID
                                            Log.Debug("missionType :: " + missionType + " :: checkId :: " + checkId);
                                            checkPalletPresent(missionType, checkId);
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    //}
                                    //else
                                    //{
                                    //    break;
                                    //}
                                }
                                else
                                {
                                    Log.Debug("Data Request False");
                                }
                            }
                            catch (Exception ex)
                            {

                                int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                                string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                                Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking data request :: " + ex.Message
                                + " HResult :: " + hResult
                                + " COM Component Error :: " + comError
                                + " stackTrace :: " + ex.StackTrace);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception occured while reading stacker data request tag :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception occured while getting stacker data request :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                }
            }
        }



        public void checkPalletPresent(string missionType, int checkId)
        {
            Log.Debug("13 : If missionType is :: "  + missionType + " and checkId is :: " + checkId);
            if (missionType == "INFEED" && checkId == 1)
            {
                Log.Debug("13.1 : If missionType is INFEED and checkId is 1");
                int stationID = 0;



                // Assuming ats_wms_loading_stations_tag_detailsDataTableDT is already populated
                ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetData();

                const int VALID_PALLET_CODE_LENGTH = 4;
                bool reverse = false;

                // Process stations in both directions (forward first, then reverse)
                for (int iteration = 0; iteration < 2; iteration++)
                {
                    Log.Debug("13.2 :: Entering Forward & Reverse Direction For Loop for iteration = "+ iteration );
                    if (ats_wms_loading_stations_tag_detailsDataTableDT != null && ats_wms_loading_stations_tag_detailsDataTableDT.Rows.Count > 0)
                    {
                        int startIndex = reverse ? ats_wms_loading_stations_tag_detailsDataTableDT.Rows.Count - 1 : 0;
                        int endIndex = reverse ? -1 : ats_wms_loading_stations_tag_detailsDataTableDT.Rows.Count;
                        int step = reverse ? -1 : 1;

                        for (int i = startIndex; reverse ? i > endIndex : i < endIndex; i += step)
                        {
                            // Example operation: print row data
                            Console.WriteLine(string.Join(", ", ats_wms_loading_stations_tag_detailsDataTableDT.Rows[i].ItemArray));
                            Log.Debug("14.0 :: For loop indexing for Loading station F & R... i = "+i);
                            Thread.Sleep(100);
                            try
                            {
                                // Check if pallet is present at pickup position
                                string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].PICKUP_POSITION_PALLET_PRESENT_TAG);

                                if (palletPresentAtPickup.Equals("True"))
                                {
                                    Log.Debug("14 :: Pallet is Present at Pickup Position");
                                    try
                                    {
                                        // Get pallet code
                                        string palletCodeAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].PICKUP_POSITION_PALLET_CODE_TAG);

                                        if (palletCodeAtPickup.Length > VALID_PALLET_CODE_LENGTH)
                                        {
                                            //format pallet code
                                        }

                                        Log.Debug("15 :: Pallet Code is in Standard Format");
                                        if (palletCodeAtPickup.Length == VALID_PALLET_CODE_LENGTH && !string.IsNullOrEmpty(palletCodeAtPickup))
                                        {
                                            // Get pallet information from database
                                            ats_wms_master_pallet_informationDataTableDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeAtPickup);

                                            // Checking if the data is present in DB
                                            if (ats_wms_master_pallet_informationDataTableDT != null && ats_wms_master_pallet_informationDataTableDT.Count > 0)
                                            {
                                                // Check if infeed mission is already generated
                                                if (ats_wms_master_pallet_informationDataTableDT[0].IS_INFEED_MISSION_GENERATED == 1)
                                                {
                                                    try
                                                    {
                                                        // Get station ID and fetch mission details
                                                        stationID = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;
                                                        //Log.Debug("Call fetchMissionDetailsFunction  stationID :: "+ stationID);
                                                        //fetchMissionDetails(missionType, checkId, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID, stationID);
                                                        missionQueue.Enqueue(new FetchMissionParameters { missionType=missionType, checkID= checkId, palletInformationID=ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID,stationID= stationID });
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Log.Error("giveMissionToStacker1Operation :: Exception occured while calling fetchMissionDetails function ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                                    }
                                                }
                                                else
                                                {
                                                    Log.Debug("15.1 :: else :: Infeed mission not generated");
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                                        string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                                        Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                                        + " HResult :: " + hResult
                                        + " COM Component Error :: " + comError
                                        + " stackTrace :: " + ex.StackTrace);
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                                string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                                Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                                + " HResult :: " + hResult
                                + " COM Component Error :: " + comError
                                + " stackTrace :: " + ex.StackTrace);
                            }
                        }

                        // Toggle reverse for next iteration
                        reverse = !reverse;
                    }
                }

                //// Assuming ats_wms_loading_stations_tag_detailsDataTableDT is already populated
                //ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetData();

                //bool reverse = false;

                //for (int iteration = 0; iteration < 2; iteration++) // Example: iterate twice
                //{
                //    if (ats_wms_loading_stations_tag_detailsDataTableDT != null && ats_wms_loading_stations_tag_detailsDataTableDT.Rows.Count > 0)
                //    {
                //        if (reverse)
                //        {
                //            for (int i = ats_wms_loading_stations_tag_detailsDataTableDT.Rows.Count - 1; i >= 0; i--)
                //            {
                //                // Example operation: print row data
                //                Console.WriteLine(string.Join(", ", ats_wms_loading_stations_tag_detailsDataTableDT.Rows[i].ItemArray));

                //                Thread.Sleep(100);
                //                try
                //                {
                //                    string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].PICKUP_POSITION_PALLET_PRESENT_TAG);


                //                    if (palletPresentAtPickup.Equals("True"))
                //                    {
                //                        Log.Debug("14 :: Pallet is Present at Pickup Position");
                //                        try
                //                        {
                //                            string palletCodeAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].PICKUP_POSITION_PALLET_CODE_TAG);


                //                            if (palletCodeAtPickup.Length > 4)
                //                            {
                //                                //format pallet code
                //                            }
                //                            Log.Debug("15 :: Pallet Code is in Standard Format");
                //                            if (palletCodeAtPickup.Length == 4 && !string.IsNullOrEmpty(palletCodeAtPickup))
                //                            {
                //                                ats_wms_master_pallet_informationDataTableDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeAtPickup);

                //                                // Checking if the data is present in DB
                //                                if (ats_wms_master_pallet_informationDataTableDT != null && ats_wms_master_pallet_informationDataTableDT.Count > 0)
                //                                {
                //                                    if (ats_wms_master_pallet_informationDataTableDT[0].IS_INFEED_MISSION_GENERATED == 1)
                //                                    {


                //                                        try
                //                                        {
                //                                            stationID = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;

                //                                            Log.Debug("Call fetchMissionDetailsFunction");

                //                                            fetchMissionDetails(missionType, checkId, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID, stationID);
                //                                        }
                //                                        catch (Exception ex)
                //                                        {
                //                                            Log.Error("giveMissionToStacker1Operation :: Exception occured while calling fetchMissionDetails function ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                //                                        }
                //                                    }
                //                                    else
                //                                    {
                //                                        Log.Debug("15.1 :: else :: Infeed mission not generated");
                //                                    }
                //                                }
                //                            }
                //                        }
                //                        catch (Exception ex)
                //                        {

                //                            int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                //                            string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                //                            Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                //                            + " HResult :: " + hResult
                //                            + " COM Component Error :: " + comError
                //                            + " stackTrace :: " + ex.StackTrace);
                //                        }
                //                    }
                //                    else
                //                    {
                //                        continue;
                //                    }
                //                }
                //                catch (Exception ex)
                //                {

                //                    int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                //                    string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                //                    Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                //                    + " HResult :: " + hResult
                //                    + " COM Component Error :: " + comError
                //                    + " stackTrace :: " + ex.StackTrace);
                //                }


                //            }
                //        }
                //        else
                //        {
                //            for (int i = 0; i < ats_wms_loading_stations_tag_detailsDataTableDT.Rows.Count; i++)
                //            {
                //                // Example operation: print row data
                //                Console.WriteLine(string.Join(", ", ats_wms_loading_stations_tag_detailsDataTableDT.Rows[i].ItemArray));

                //                Thread.Sleep(100);
                //                try
                //                {
                //                    string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].PICKUP_POSITION_PALLET_PRESENT_TAG);


                //                    if (palletPresentAtPickup.Equals("True"))
                //                    {
                //                        Log.Debug("14 :: Pallet is Present at Pickup Position");
                //                        try
                //                        {
                //                            string palletCodeAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].PICKUP_POSITION_PALLET_CODE_TAG);


                //                            if (palletCodeAtPickup.Length > 4)
                //                            {
                //                                //format pallet code
                //                            }
                //                            Log.Debug("15 :: Pallet Code is in Standard Format");
                //                            if (palletCodeAtPickup.Length == 4 && !string.IsNullOrEmpty(palletCodeAtPickup))
                //                            {
                //                                ats_wms_master_pallet_informationDataTableDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeAtPickup);

                //                                // Checking if the data is present in DB
                //                                if (ats_wms_master_pallet_informationDataTableDT != null && ats_wms_master_pallet_informationDataTableDT.Count > 0)
                //                                {
                //                                    if (ats_wms_master_pallet_informationDataTableDT[0].IS_INFEED_MISSION_GENERATED == 1)
                //                                    {


                //                                        try
                //                                        {
                //                                            stationID = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;

                //                                            Log.Debug("Call fetchMissionDetailsFunction");

                //                                            fetchMissionDetails(missionType, checkId, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID, stationID);
                //                                        }
                //                                        catch (Exception ex)
                //                                        {
                //                                            Log.Error("giveMissionToStacker1Operation :: Exception occured while calling fetchMissionDetails function ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                //                                        }
                //                                    }
                //                                    else
                //                                    {
                //                                        Log.Debug("15.1 :: else :: Infeed mission not generated");
                //                                    }
                //                                }
                //                            }
                //                        }
                //                        catch (Exception ex)
                //                        {

                //                            int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                //                            string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                //                            Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                //                            + " HResult :: " + hResult
                //                            + " COM Component Error :: " + comError
                //                            + " stackTrace :: " + ex.StackTrace);
                //                        }
                //                    }
                //                    else
                //                    {
                //                        continue;
                //                    }
                //                }
                //                catch (Exception ex)
                //                {

                //                    int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                //                    string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                //                    Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                //                    + " HResult :: " + hResult
                //                    + " COM Component Error :: " + comError
                //                    + " stackTrace :: " + ex.StackTrace);
                //                }

                //            }
                //        }

                //        reverse = !reverse; // Toggle reverse for next iteration
                //    }
                //}



                //ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetData();



                //if (ats_wms_loading_stations_tag_detailsDataTableDT != null && ats_wms_loading_stations_tag_detailsDataTableDT.Count > 0)
                //{
                //    for (int i = 0; i < ats_wms_loading_stations_tag_detailsDataTableDT.Count; i++)
                //    {
                //        Thread.Sleep(100);
                //        try
                //        {
                //            string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].PICKUP_POSITION_PALLET_PRESENT_TAG);


                //            if (palletPresentAtPickup.Equals("True"))
                //            {
                //                Log.Debug("14 :: Pallet is Present at Pickup Position");
                //                try
                //                {
                //                    string palletCodeAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].PICKUP_POSITION_PALLET_CODE_TAG);


                //                    if (palletCodeAtPickup.Length > 4)
                //                    {
                //                        //format pallet code
                //                    }
                //                    Log.Debug("15 :: Pallet Code is in Standard Format");
                //                    if (palletCodeAtPickup.Length == 4 && !string.IsNullOrEmpty(palletCodeAtPickup))
                //                    {
                //                        ats_wms_master_pallet_informationDataTableDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeAtPickup);

                //                        // Checking if the data is present in DB
                //                        if (ats_wms_master_pallet_informationDataTableDT != null && ats_wms_master_pallet_informationDataTableDT.Count > 0)
                //                        {
                //                            if (ats_wms_master_pallet_informationDataTableDT[0].IS_INFEED_MISSION_GENERATED == 1)
                //                            {


                //                                try
                //                                {
                //                                    stationID = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;

                //                                    Log.Debug("Call fetchMissionDetailsFunction");

                //                                    fetchMissionDetails(missionType, checkId, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID, stationID);
                //                                }
                //                                catch (Exception ex)
                //                                {
                //                                    Log.Error("giveMissionToStacker1Operation :: Exception occured while calling fetchMissionDetails function ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                //                                }
                //                            }
                //                            else
                //                            {
                //                                Log.Debug("15.1 :: else :: Infeed mission not generated");
                //                            }
                //                        }
                //                    }
                //                }
                //                catch (Exception ex)
                //                {

                //                    int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                //                    string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                //                    Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                //                    + " HResult :: " + hResult
                //                    + " COM Component Error :: " + comError
                //                    + " stackTrace :: " + ex.StackTrace);
                //                }
                //            }
                //            else
                //            {
                //                continue;
                //            }
                //        }
                //        catch (Exception ex)
                //        {

                //            int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                //            string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                //            Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                //            + " HResult :: " + hResult
                //            + " COM Component Error :: " + comError
                //            + " stackTrace :: " + ex.StackTrace);
                //        }
                //    }
                //}

            }
            // Currently Outfeed Not Needed.
            //else if (missionType == "OUTFEED" && checkId == 2)
            //{
            //    Log.Debug("14 : If missionType is OUTFEED and checkId is 4");


            //    ats_wms_outfeed_mission_runtime_detailsDataTableDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByAREA_IDAndOUTFEED_MISSION_STATUS(areaId, "READY");

            //    if (ats_wms_outfeed_mission_runtime_detailsDataTableDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableDT.Count > 0)
            //    {
            //        Log.Debug("Outfeed Mission ID :: " + ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].OUTFEED_MISSION_ID);
            //        {
            //            //Checking if the Pallet is Present (1 - pallet is present and 0- pallet is not present)
            //            Log.Debug("checking palletPresentAtProductionSide");
            //            //string palletPresentAtProductionSide = "";


            //            try
            //            {
            //                string palletPresentAtProductionSide = checkPalletPresenceAtEmptyPlacePosition();


            //                {

            //                    ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetData();

            //                    if (ats_wms_loading_stations_tag_detailsDataTableDT != null && ats_wms_loading_stations_tag_detailsDataTableDT.Count > 0)
            //                    {
            //                        for (int i = 0; i < ats_wms_loading_stations_tag_detailsDataTableDT.Count; i++)
            //                        {
            //                            string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_PALLET_PRESENT_TAG);


            //                            if (palletPresentAtPickup.Equals("False"))
            //                            {

            //                                int station_id = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;
            //                                //Pallet is already present on the unloading conveyor(1:means true && 0:means false)
            //                                Log.Debug("Pallet is already present on the unloading conveyor");
            //                                if (palletPresentAtPickup.Equals("False"))
            //                                {
            //                                    ats_wms_master_position_detailsDataTableRackPalletsDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_IDLessThanAndRACK_IDAndPOSITION_IS_EMPTYOrPOSITION_IS_ALLOCATED(ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].POSITION_ID, ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].RACK_ID, 0, 1);


            //                                    if (ats_wms_master_position_detailsDataTableRackPalletsDT != null && ats_wms_master_position_detailsDataTableRackPalletsDT.Count == 0)
            //                                    {
            //                                        Log.Debug("checkinng palletPresentAtProductionSide :: false");
            //                                        fetchMissionDetails(missionType, checkId, ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].PALLET_INFORMATION_ID, station_id);
            //                                    }
            //                                }


            //                            }
            //                            else
            //                            {
            //                                continue;
            //                            }
            //                        }
            //                    }

            //                }





            //            }
            //            catch (Exception ex)
            //            {

            //                Log.Error("giveMissionToStacker1Operation :: Exception occured while checking pallet present at empty unload position::" + ex.Message + " StackTrace:: " + ex.StackTrace);
            //            }
            //        }
            //    }
            //}
            else if (missionType == "EMPTY" && checkId == 4)
            {
                Log.Debug("14 : If missionType is EMPTY and checkId is 4");


                ats_wms_outfeed_mission_runtime_detailsDataTableDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByAREA_IDAndOUTFEED_MISSION_STATUSAndSTACKER_ID(areaId, "READY", 1);

                if (ats_wms_outfeed_mission_runtime_detailsDataTableDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableDT.Count > 0)
                {
                    Log.Debug("14.1 :: Outfeed Mission ID :: " + ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].OUTFEED_MISSION_ID);
                    {
                        //Checking if the Pallet is Present (1 - pallet is present and 0- pallet is not present)
                        Log.Debug("14.2 :: checking palletPresentAtCoreshooterSide");
                        //string palletPresentAtProductionSide = "";


                        try
                        {
                            Log.Debug("14.2 :: checking palletPresentAtCoreshooterSide");
                            //string palletPresentAtProductionSide = checkPalletPresenceAtEmptyPlacePosition();
                            ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetData();

                            Log.Debug("14.2.1 :: checking palletPresentAtCoreshooterSide");


                            ArrayList listname = new ArrayList { 1, 2 };

                            for (int p = 0; p < listname.Count; p++)
                            {
                                int value = (int)listname[p];

                                switch (value)
                                {

                                case 1:

                                Log.Debug("  For Rework/Reject");

                                ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByPALLET_STATUS_IDAndOUTFEED_MISSION_STATUSAndSTACKER_ID(4, 7, "READY", 1);


                                Log.Debug("14.2.2 :: checking palletPresentAtCoreshooterSide");
                                if (ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject != null && ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject.Count > 0)
                                {
                                    Log.Debug("14.2.3 :: checking palletPresentAtCoreshooterSide");
                                    ats_wms_loading_stations_tag_detailsDataTableDTReworkReject = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetDataBySTATION_IDBetween(4, 5);

                                    Log.Debug("14.2.4 :: checking palletPresentAtCoreshooterSide");
                                    ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsDataTableDTReworkReject;
                                }


                                {
                                    Log.Debug("14.2.5 :: checking palletPresentAtCoreshooterSide");
                                    if (ats_wms_loading_stations_tag_detailsDataTableDT != null && ats_wms_loading_stations_tag_detailsDataTableDT.Count > 0)
                                    {
                                        Log.Debug("14.2.6 :: checking palletPresentAtCoreshooterSide");
                                        for (int i = 0; i < ats_wms_loading_stations_tag_detailsDataTableDT.Count; i++)
                                        {
                                            Thread.Sleep(200);
                                            //string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_PALLET_PRESENT_TAG);
                                            try
                                            {
                                                Log.Debug("14.2.7 :: checking palletPresentAtCoreshooterSide");
                                                string DropPositionIsready = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_IS_READY_TAG);


                                                Log.Debug("14.2.8 :: checking palletPresentAtCoreshooterSide");
                                                if (DropPositionIsready.Equals("True"))
                                                {

                                                    Log.Debug("14.2.9  :: checking palletPresentAtCoreshooterSide");
                                                    int station_id = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;
                                                    //Pallet is already present on the unloading conveyor(1:means true && 0:means false)
                                                    Log.Debug("14.3 :: Pallet is not present on the  coreshooter drop position :: " + station_id);
                                                    if (DropPositionIsready.Equals("True"))
                                                    {
                                                        Log.Debug("14.4 :: Pallet is not present on the  coreshooter drop position");
                                                        ats_wms_master_position_detailsDataTableRackPalletsDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_IDLessThanAndRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATED(ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject[0].POSITION_ID, ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject[0].RACK_ID, 0, 1);


                                                        if (ats_wms_master_position_detailsDataTableRackPalletsDT != null && ats_wms_master_position_detailsDataTableRackPalletsDT.Count == 0)
                                                        {
                                                            Log.Debug("14.5 :: checkinng palletPresentAtProductionSide :: false");
                                                          fetchMissionDetails(missionType, checkId, ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject[0].PALLET_INFORMATION_ID, station_id);
                                                        }
                                                    }


                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }
                                            catch (Exception ex)
                                            {


                                                int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                                                string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                                                Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                                                + " HResult :: " + hResult
                                                + " COM Component Error :: " + comError
                                                + " stackTrace :: " + ex.StackTrace);
                                            }
                                        }
                                    }

                                }

                                break;


                                case 2:

                                Log.Debug("For EMPTY");



                                var ats_wms_outfeed_mission_runtime_detailsDataTableDTEmptyOutfeed = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByPALLET_STATUS_IDAndOUTFEED_MISSION_STATUSAndSTACKER_ID(3, 3, "READY", 1);

                                Log.Debug("14.2.2 :: checking palletPresentAtCoreshooterSide");
                                if (ats_wms_outfeed_mission_runtime_detailsDataTableDTEmptyOutfeed != null && ats_wms_outfeed_mission_runtime_detailsDataTableDTEmptyOutfeed.Count > 0)
                                {
                                    Log.Debug("14.2.3 :: checking palletPresentAtCoreshooterSide");
                                   var  ats_wms_loading_stations_tag_detailsDataTableDTCCM = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetDataBySTATION_IDBetween(1,3);

                                    Log.Debug("14.2.4 :: checking palletPresentAtCoreshooterSide");
                                    ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsDataTableDTCCM;
                                }


                                {
                                    Log.Debug("14.2.5 :: checking palletPresentAtCoreshooterSide");
                                    if (ats_wms_loading_stations_tag_detailsDataTableDT != null && ats_wms_loading_stations_tag_detailsDataTableDT.Count > 0)
                                    {
                                        Log.Debug("14.2.6 :: checking palletPresentAtCoreshooterSide");
                                        for (int i = 0; i < ats_wms_loading_stations_tag_detailsDataTableDT.Count; i++)
                                        {
                                            Thread.Sleep(200);
                                            //string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_PALLET_PRESENT_TAG);
                                            try
                                            {
                                                Log.Debug("14.2.7 :: checking palletPresentAtCoreshooterSide");
                                                string DropPositionIsready = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_IS_READY_TAG);


                                                Log.Debug("14.2.8 :: checking palletPresentAtCoreshooterSide");
                                                if (DropPositionIsready.Equals("True"))
                                                {

                                                    Log.Debug("14.2.9  :: checking palletPresentAtCoreshooterSide");
                                                    int station_id = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;
                                                    //Pallet is already present on the unloading conveyor(1:means true && 0:means false)
                                                    Log.Debug("14.3 :: Pallet is not present on the  coreshooter drop position :: " + station_id);
                                                    if (DropPositionIsready.Equals("True"))
                                                    {
                                                        Log.Debug("14.4 :: Pallet is not present on the  coreshooter drop position");
                                                        ats_wms_master_position_detailsDataTableRackPalletsDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_IDLessThanAndRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATED(ats_wms_outfeed_mission_runtime_detailsDataTableDTEmptyOutfeed[0].POSITION_ID, ats_wms_outfeed_mission_runtime_detailsDataTableDTEmptyOutfeed[0].RACK_ID, 0, 1);


                                                        if (ats_wms_master_position_detailsDataTableRackPalletsDT != null && ats_wms_master_position_detailsDataTableRackPalletsDT.Count == 0)
                                                        {
                                                            Log.Debug("14.5 :: checkinng palletPresentAtProductionSide :: false");
                                                            fetchMissionDetails(missionType, checkId, ats_wms_outfeed_mission_runtime_detailsDataTableDTEmptyOutfeed[0].PALLET_INFORMATION_ID, station_id);
                                                        }
                                                    }


                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }
                                            catch (Exception ex)
                                            {


                                                int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                                                string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                                                Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                                                + " HResult :: " + hResult
                                                + " COM Component Error :: " + comError
                                                + " stackTrace :: " + ex.StackTrace);
                                            }
                                        }
                                    }

                                }
                                break;
                                default:
                                break;

                                }

                            }
                         
                                  
                           

                            //Log.Debug("14.2.2 :: checking palletPresentAtCoreshooterSide");
                            //if (ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject != null && ats_wms_outfeed_mission_runtime_detailsDataTableDTReworkReject.Count > 0)
                            //{
                            //    Log.Debug("14.2.3 :: checking palletPresentAtCoreshooterSide");
                            //    ats_wms_loading_stations_tag_detailsDataTableDTReworkReject = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetDataBySTATION_IDBetween(4, 5);

                            //    Log.Debug("14.2.4 :: checking palletPresentAtCoreshooterSide");
                            //    ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsDataTableDTReworkReject;
                            //}


                            //{
                            //    Log.Debug("14.2.5 :: checking palletPresentAtCoreshooterSide");
                            //    if (ats_wms_loading_stations_tag_detailsDataTableDT != null && ats_wms_loading_stations_tag_detailsDataTableDT.Count > 0)
                            //    {
                            //        Log.Debug("14.2.6 :: checking palletPresentAtCoreshooterSide");
                            //        for (int i = 0; i < ats_wms_loading_stations_tag_detailsDataTableDT.Count; i++)
                            //        {
                            //            Thread.Sleep(200);
                            //            //string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_PALLET_PRESENT_TAG);
                            //            try
                            //            {
                            //                Log.Debug("14.2.7 :: checking palletPresentAtCoreshooterSide");
                            //                string DropPositionIsready = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_IS_READY_TAG);


                            //                Log.Debug("14.2.8 :: checking palletPresentAtCoreshooterSide");
                            //                if (DropPositionIsready.Equals("True"))
                            //                {

                            //                    Log.Debug("14.2.9  :: checking palletPresentAtCoreshooterSide");
                            //                    int station_id = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;
                            //                    //Pallet is already present on the unloading conveyor(1:means true && 0:means false)
                            //                    Log.Debug("14.3 :: Pallet is not present on the  coreshooter drop position :: " + station_id);
                            //                    if (DropPositionIsready.Equals("True"))
                            //                    {
                            //                        Log.Debug("14.4 :: Pallet is not present on the  coreshooter drop position");
                            //                        ats_wms_master_position_detailsDataTableRackPalletsDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_IDLessThanAndRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATED(ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].POSITION_ID, ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].RACK_ID, 0, 1);


                            //                        if (ats_wms_master_position_detailsDataTableRackPalletsDT != null && ats_wms_master_position_detailsDataTableRackPalletsDT.Count == 0)
                            //                        {
                            //                            Log.Debug("14.5 :: checkinng palletPresentAtProductionSide :: false");
                            //                            fetchMissionDetails(missionType, checkId, ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].PALLET_INFORMATION_ID, station_id);
                            //                        }
                            //                    }


                            //                }
                            //                else
                            //                {
                            //                    continue;
                            //                }
                            //            }
                            //            catch (Exception ex)
                            //            {


                            //                int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                            //                string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                            //                Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
                            //                + " HResult :: " + hResult
                            //                + " COM Component Error :: " + comError
                            //                + " stackTrace :: " + ex.StackTrace);
                            //            }
                            //        }
                            //    }

                            //}





                        }
                        catch (Exception ex)
                        {

                            Log.Error("giveMissionToStacker1Operation :: Exception occured while checking pallet present at empty unload position::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                        }
                    }
                }
            }
            //else if (missionType == "REWORK-REJECT" && checkId == 5)
            //{
            //    Log.Debug("14 : If missionType is REWORK-REJECT and checkId is 5");


            //    ats_wms_outfeed_mission_runtime_detailsDataTableDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByAREA_IDAndOUTFEED_MISSION_STATUSAndSTACKER_ID(areaId, "READY", 1);

            //    if (ats_wms_outfeed_mission_runtime_detailsDataTableDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableDT.Count > 0)
            //    {
            //        Log.Debug("14.1 :: Outfeed Mission ID :: " + ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].OUTFEED_MISSION_ID);
            //        {
            //            //Checking if the Pallet is Present (1 - pallet is present and 0- pallet is not present)
            //            Log.Debug("14.2 :: checking palletPresentAtCoreshooterSide");
            //            //string palletPresentAtProductionSide = "";


            //            try
            //            {
            //                //string palletPresentAtProductionSide = checkPalletPresenceAtEmptyPlacePosition();


            //                {

            //                    ats_wms_loading_stations_tag_detailsDataTableDT = ats_wms_loading_stations_tag_detailsTableAdapterInstance.GetData();

            //                    if (ats_wms_loading_stations_tag_detailsDataTableDT != null && ats_wms_loading_stations_tag_detailsDataTableDT.Count > 0)
            //                    {
            //                        for (int i = 0; i < ats_wms_loading_stations_tag_detailsDataTableDT.Count; i++)
            //                        {
            //                            Thread.Sleep(200);
            //                            //string palletPresentAtPickup = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_PALLET_PRESENT_TAG);
            //                            try
            //                            {
            //                                string DropPositionIsready = readTag(ats_wms_loading_stations_tag_detailsDataTableDT[i].DROP_POSITION_IS_READY_TAG);



            //                                if (DropPositionIsready.Equals("True"))
            //                                {

            //                                    int station_id = ats_wms_loading_stations_tag_detailsDataTableDT[i].STATION_ID;
            //                                    //Pallet is already present on the unloading conveyor(1:means true && 0:means false)
            //                                    Log.Debug("14.3 :: Pallet is not present on the  coreshooter drop position :: " + station_id);
            //                                    if (DropPositionIsready.Equals("True"))
            //                                    {
            //                                        Log.Debug("14.4 :: Pallet is not present on the  coreshooter drop position");
            //                                        ats_wms_master_position_detailsDataTableRackPalletsDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_IDLessThanAndRACK_IDAndPOSITION_IS_EMPTYOrPOSITION_IS_ALLOCATED(ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].POSITION_ID, ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].RACK_ID, 0, 1);


            //                                        if (ats_wms_master_position_detailsDataTableRackPalletsDT != null && ats_wms_master_position_detailsDataTableRackPalletsDT.Count == 0)
            //                                        {
            //                                            Log.Debug("14.5 :: checkinng palletPresentAtProductionSide :: false");
            //                                            fetchMissionDetails(missionType, checkId, ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].PALLET_INFORMATION_ID, station_id);
            //                                        }
            //                                    }


            //                                }
            //                                else
            //                                {
            //                                    continue;
            //                                }
            //                            }
            //                            catch (Exception ex)
            //                            {


            //                                int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
            //                                string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

            //                                Log.Error("GiveMissionToStacker :: checkStackerMissionRequest :: Exception while checking palletPresentAtPickup  :: " + ex.Message
            //                                + " HResult :: " + hResult
            //                                + " COM Component Error :: " + comError
            //                                + " stackTrace :: " + ex.StackTrace);
            //                            }
            //                        }
            //                    }

            //                }





            //            }
            //            catch (Exception ex)
            //            {

            //                Log.Error("giveMissionToStacker1Operation :: Exception occured while checking pallet present at empty unload position::" + ex.Message + " StackTrace:: " + ex.StackTrace);
            //            }
            //        }
            //    }
            //}

            else if (missionType == "TRANSFER" && checkId == 3)
            {
                Log.Debug("15 : If missionType is TRANSFER and checkId is 6");
                ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByTRANSFER_MISSION_STATUSAndSTACKER_ID("READY", 1);

                if (ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT != null && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT.Count > 0)
                {
                    Log.Debug("transfer Mission ID :: " + ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].TRANSFER_PALLET_MISSION_RUNTIME_DETAILS_ID);
                    for (int i = 0; i < ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT.Count; i++)
                    {
                        Thread.Sleep(100);
                        ats_wms_master_position_detailsDataTableTemptDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_ID(ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PREVIOUS_POSITION_ID);

                        Log.Debug("Pallet previous position ID :: " + ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PREVIOUS_POSITION_ID);
                        Log.Debug("Pallet previous rack ID :: " + ats_wms_master_position_detailsDataTableTemptDT[0].RACK_ID);
                        // checking if the position is above the missioned pallet posiotion has no pallet or position is not allocated

                        try
                        {
                            if (ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PREVIOUS_POSITION_ID % 2 == 0)
                            {
                                try
                                {
                                    ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_IDLessThanAndRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATED(ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PREVIOUS_POSITION_ID, ats_wms_master_position_detailsDataTableTemptDT[0].RACK_ID, 0, 1);
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("giveMissionToStacker1Operation :: Exception occured while getting mission rack details ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                                }

                                Log.Debug("Pallet count in rack before this pallet :: " + ats_wms_master_position_detailsDataTableDT.Count);
                                //Checking the count should be 0
                                if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count == 0)
                                {
                                    Log.Debug("checking palletPresentAtDispatchSide :: false");     //checking palletPresentAtDispatchSide is 0 i.e false
                                    fetchMissionDetails(missionType, checkId, ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PALLET_INFORMTION_ID, 0);
                                }



                            }
                            else if (ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PREVIOUS_POSITION_ID % 2 != 0 && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].FLOOR_ID == 6 && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PREVIOUS_POSITION_ID >= 261)
                            {
                                Log.Debug("Wrong");
                                break;
                            }
                            else if (ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PREVIOUS_POSITION_ID % 2 != 0)
                            {

                                Log.Debug("checking palletPresentAtDispatchSide :: false");     //checking palletPresentAtDispatchSide is 0 i.e false
                                fetchMissionDetails(missionType, checkId, ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[i].PALLET_INFORMTION_ID, 0);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error("giveMissionToStacker1Operation :: Exception occured while check Pallet Present function of Transfer Mission::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                        }

                    }
                }
            }

        }



        public void fetchMissionDetails(string missionType, int checkId, int palletInformationId, int stationID)
        {
            Log.Debug("In fetchMissionDetails");
            if (missionType == "INFEED")
            {
                if (true)
                {
                    Log.Debug("16 : missionType :: " + missionType);
                    try
                    {
                        ats_wms_infeed_mission_runtime_detailsDataTableDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByPALLET_INFORMATION_IDAndINFEED_MISSION_STATUSAndAREA_IDAndSTACKER_ID(palletInformationId, "READY", areaId, 1);

                        if (ats_wms_infeed_mission_runtime_detailsDataTableDT != null && ats_wms_infeed_mission_runtime_detailsDataTableDT.Count > 0)
                        {
                            Log.Debug("16.1 ");
                            ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(1, checkId);
                            try
                            {
                                PositionActiveDetails positionActiveDetailsInstance = new PositionActiveDetails();

                                if (positionActiveDetailsInstance.isFloorInfeedActive(ats_wms_infeed_mission_runtime_detailsDataTableDT[0].FLOOR_ID, areaId) &&
                                    positionActiveDetailsInstance.isPositionActive(ats_wms_infeed_mission_runtime_detailsDataTableDT[0].POSITION_ID) &&
                                    positionActiveDetailsInstance.isRackActive(ats_wms_infeed_mission_runtime_detailsDataTableDT[0].RACK_ID))
                                {
                                    Log.Debug("16.2 ");
                                    try
                                    {
                                        MissionParametersDetails missionParametersDetailsInstance = new MissionParametersDetails();
                                        missionParametersDetailsInstance.missionId = ats_wms_infeed_mission_runtime_detailsDataTableDT[0].INFEED_MISSION_ID;
                                        missionParametersDetailsInstance.taskType = infeedTaskType;
                                        missionParametersDetailsInstance.targetColumn = ats_wms_infeed_mission_runtime_detailsDataTableDT[0].RACK_COLUMN;
                                        missionParametersDetailsInstance.targetFloor = ats_wms_infeed_mission_runtime_detailsDataTableDT[0].FLOOR_ID;
                                        missionParametersDetailsInstance.targetDepthOfLine = ats_wms_infeed_mission_runtime_detailsDataTableDT[0].POSITION_NUMBER_IN_RACK;

                                        if (ats_wms_infeed_mission_runtime_detailsDataTableDT[0].RACK_SIDE == "L")
                                        {
                                            missionParametersDetailsInstance.targetLine = racksideLeft;
                                        }
                                        else if (ats_wms_infeed_mission_runtime_detailsDataTableDT[0].RACK_SIDE == "R")
                                        {
                                            missionParametersDetailsInstance.targetLine = racksideLeft;
                                        }
                                        missionParametersDetailsInstance.startFloor = startTargetFloorId;
                                        missionParametersDetailsInstance.startDepthOfLine = infeedAndOutfeedDepth;
                                        //if (ats_wms_infeed_mission_runtime_detailsDataTableDT[0].CORESHOP == "")

                                        //    missionParametersDetailsInstance.startLine = racksideLeft;
                                        if (ats_wms_infeed_mission_runtime_detailsDataTableDT[0].CORESHOP == "CORE_SHOOTER-6" || ats_wms_infeed_mission_runtime_detailsDataTableDT[0].CORESHOP == "CORE_SHOOTER-5" ||
                                  ats_wms_infeed_mission_runtime_detailsDataTableDT[0].CORESHOP == "CORE_SHOOTER-4")

                                            //missionParametersDetailsInstance.startLine = racksideLeft;
                                            missionParametersDetailsInstance.startLine = racksideRight;
                                        else
                                        {
                                            //missionParametersDetailsInstance.startLine = racksideLeft;
                                            missionParametersDetailsInstance.startLine = racksideRight;
                                        }

                                        if (stationID == 1)
                                        {
                                            missionParametersDetailsInstance.startColumn = CS6LoadingPickSideColumn;
                                        }
                                        else if (stationID == 2)
                                        {
                                            missionParametersDetailsInstance.startColumn = CS5LoadingPickSideColumn;
                                        }
                                        else if (stationID == 3)
                                        {
                                            missionParametersDetailsInstance.startColumn = CS4LoadingPickSideColumn;
                                        }
                                        else if (stationID == 4 || stationID == 5)
                                        {
                                            missionParametersDetailsInstance.startColumn = ReworkRejectSideColumn;
                                        }

                                        if (!ats_wms_infeed_mission_runtime_detailsDataTableDT[0].IsPALLET_CODENull())
                                        {
                                            missionParametersDetailsInstance.palletCode = ats_wms_infeed_mission_runtime_detailsDataTableDT[0].PALLET_CODE;
                                        }

                                        try
                                        {
                                            //missionQueue.Enqueue(missionParametersDetailsInstance);
                                            //Log.Debug($"Mission enqueued: {missionParametersDetailsInstance.missionId}, Type: {missionParametersDetailsInstance.taskType}");
                                            giveMissionToStacker(missionParametersDetailsInstance);
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("Error while enqueuing mission: " + ex.Message + " StackTrace: " + ex.StackTrace);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error("startOperation ::    Exception occured while setting infeed mission values :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error("startOperation :: Exception occured while checking positon active status :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                            }
                        }
                        else
                        {
                            ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(0, checkId);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("startOperation :: Exception occured while stopping timer :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                    }
                }
            }
            else if (missionType == "EMPTY")
            {
                Log.Debug("17 : missionType :: " + missionType);
                try
                {
                    ats_wms_outfeed_mission_runtime_detailsDataTableDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByPALLET_INFORMATION_IDAndOUTFEED_MISSION_STATUSAndAREA_IDAndSTACKER_ID(palletInformationId, "READY", areaId, 1);

                    if (ats_wms_outfeed_mission_runtime_detailsDataTableDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableDT.Count > 0)
                    {
                        Log.Debug("17.1");
                        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(1, checkId);
                        try
                        {
                            PositionActiveDetails positionActiveDetailsInstance = new PositionActiveDetails();

                            if (positionActiveDetailsInstance.isFloorOutfeedActive(ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].FLOOR_ID, areaId) &&
                                positionActiveDetailsInstance.isPositionActive(ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].POSITION_ID) &&
                                positionActiveDetailsInstance.isRackActive(ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].RACK_ID))
                            {
                                Log.Debug("17.2");
                                try
                                {
                                    //fetch rack column

                                    ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_ID(ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].RACK_ID);



                                    MissionParametersDetails missionParametersDetailsInstance = new MissionParametersDetails();
                                    missionParametersDetailsInstance.missionId = ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].OUTFEED_MISSION_ID;
                                    missionParametersDetailsInstance.taskType = outfeedTaskType;
                                    missionParametersDetailsInstance.startColumn = ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN;
                                    missionParametersDetailsInstance.startFloor = ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].FLOOR_ID;
                                    //if (ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].RACK_SIDE == "L")
                                    //{
                                    missionParametersDetailsInstance.startLine = racksideLeft;
                                    //}
                                    //else if (ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].RACK_SIDE == "R")
                                    //{
                                    //    missionParametersDetailsInstance.startLine = racksideRight;
                                    //}
                                    Log.Debug("17.3 POSITION_NUMBER_IN_RACK :: " + ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].POSITION_NUMBER_IN_RACK);
                                    missionParametersDetailsInstance.startDepthOfLine = ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].POSITION_NUMBER_IN_RACK;

                                    //if(ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].POSITION_ID % 2 == 0)
                                    //{
                                    //    missionParametersDetailsInstance.startDepthOfLine = STACKER_1_D_2;
                                    //}
                                    //else if (ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].POSITION_ID % 2 == 1)
                                    //{
                                    //    missionParametersDetailsInstance.startDepthOfLine = STACKER_1_D_1;
                                    //}

                                    if (stationID == 3)
                                    {
                                        missionParametersDetailsInstance.targetColumn = CS4LoadingPickSideColumn - 1;
                                    }
                                    if (stationID == 2)
                                    {
                                        missionParametersDetailsInstance.targetColumn = CS5LoadingPickSideColumn - 1;
                                    }
                                    if (stationID == 1)
                                    {
                                        missionParametersDetailsInstance.targetColumn = CS6LoadingPickSideColumn - 1;
                                    }
                                    if (stationID == 4 || stationID == 5)
                                    {
                                        missionParametersDetailsInstance.targetColumn = ReworkRejectSideColumn;
                                    }

                                    Log.Debug("missionParametersDetailsInstance.targetSide* :; " + stackerRightSide);
                                    missionParametersDetailsInstance.targetLine = stackerRightSide;
                                    Log.Debug("missionParametersDetailsInstance.targetLine :; " + missionParametersDetailsInstance.targetLine);

                                    missionParametersDetailsInstance.targetFloor = startTargetFloorId;
                                    Log.Debug("missionParametersDetailsInstance.targetFloor :; " + missionParametersDetailsInstance.targetFloor);
                                    missionParametersDetailsInstance.targetDepthOfLine = infeedAndOutfeedDepth;
                                    Log.Debug("missionParametersDetailsInstance.targetDepth :; " + missionParametersDetailsInstance.targetDepthOfLine);


                                    if (!ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].IsPALLET_CODENull())
                                    {
                                        missionParametersDetailsInstance.palletCode = ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].PALLET_CODE;
                                    }

                                    try
                                    {
                                        //missionQueue.Enqueue(missionParametersDetailsInstance);
                                        //Log.Debug($"Mission enqueued: {missionParametersDetailsInstance.missionId}, Type: {missionParametersDetailsInstance.taskType}");
                                        Log.Debug("Going to give mission to stacker");
                                        giveMissionToStacker(missionParametersDetailsInstance);
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error("Error while enqueuing mission: " + ex.Message + " StackTrace: " + ex.StackTrace);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("startOperation :: Exception occured while setting outfeed mission values :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error("startOperation :: Exception occured while checking position details :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                        }
                    }
                    else
                    {
                        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(0, checkId);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("startOperation :: Exception occured while stopping timer :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                }
            }
            else if (missionType == "TRANSFER")
            {
                Log.Debug("18 : missionType :: " + missionType);
                try
                {
                    ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByPALLET_INFORMATION_IDAndTRANSFER_MISSION_STATUSAndAREA_ID(palletInformationId, "READY", areaId);

                    if (ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT != null && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT.Count > 0)
                    {
                        Log.Debug("18.1");
                        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(1, checkId);
                        try
                        {
                            PositionActiveDetails positionActiveDetailsInstance = new PositionActiveDetails();

                            if (positionActiveDetailsInstance.isFloorInfeedActive(ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].FLOOR_ID, areaId) &&
                                positionActiveDetailsInstance.isPositionActive(ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].TRANSFER_POSITION_ID) &&
                                positionActiveDetailsInstance.isRackActive(ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].RACK_ID))
                            {
                                Log.Debug("18.2");
                                try
                                {
                                    // fetch rack column

                                    ats_wms_master_position_detailsDataTablePrevTRDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_ID(ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].PREVIOUS_POSITION_ID);
                                    ats_wms_master_rack_detailsDataTablePrevTRDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_ID(ats_wms_master_position_detailsDataTablePrevTRDT[0].RACK_ID);
                                    var ats_wms_master_position_detailsDataTableTarget = ats_wms_master_position_detailsTableAdapterInstance.GetDataByPOSITION_ID(ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].TRANSFER_POSITION_ID);



                                    MissionParametersDetails missionParametersDetailsInstance = new MissionParametersDetails();
                                    missionParametersDetailsInstance.missionId = ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].TRANSFER_PALLET_MISSION_RUNTIME_DETAILS_ID;
                                    missionParametersDetailsInstance.taskType = transferTaskType;
                                    missionParametersDetailsInstance.startColumn = ats_wms_master_rack_detailsDataTablePrevTRDT[0].RACK_COLUMN;
                                    missionParametersDetailsInstance.startFloor = ats_wms_master_position_detailsDataTablePrevTRDT[0].FLOOR_ID;


                                    if (ats_wms_master_rack_detailsDataTablePrevTRDT[0].S1_RACK_SIDE == "L")
                                    {
                                        missionParametersDetailsInstance.startLine = racksideLeft;
                                    }
                                    else if (ats_wms_master_rack_detailsDataTablePrevTRDT[0].S2_RACK_SIDE == "R")
                                    {
                                        missionParametersDetailsInstance.startLine = racksideLeft;
                                    }
                                    if (ats_wms_master_position_detailsDataTablePrevTRDT[0].POSITION_ID % 2 == 0)
                                    {
                                        missionParametersDetailsInstance.startDepthOfLine = 2;
                                    }
                                    else if (ats_wms_master_position_detailsDataTablePrevTRDT[0].POSITION_ID % 2 != 0)
                                    {
                                        missionParametersDetailsInstance.startDepthOfLine = 1;
                                    }
                                    //missionParametersDetailsInstance.startDepthOfLine = ats_wms_master_position_detailsDataTablePrevTRDT[0].ST1_POSITION_NUMBER_IN_RACK;
                                    missionParametersDetailsInstance.targetColumn = ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].RACK_COLUMN;
                                    if (ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].RACK_SIDE == "L")
                                    {
                                        missionParametersDetailsInstance.targetLine = racksideLeft;
                                    }
                                    else if (ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].RACK_SIDE == "R")
                                    {
                                        missionParametersDetailsInstance.targetLine = racksideLeft;
                                    }
                                    missionParametersDetailsInstance.targetFloor = ats_wms_master_position_detailsDataTableTarget[0].FLOOR_ID;

                                    if (ats_wms_master_position_detailsDataTableTarget[0].POSITION_ID % 2 == 0)
                                    {
                                        missionParametersDetailsInstance.targetDepthOfLine = 2;
                                    }
                                    else if (ats_wms_master_position_detailsDataTableTarget[0].POSITION_ID % 2 != 0)
                                    {
                                        missionParametersDetailsInstance.targetDepthOfLine = 1;
                                    }
                                    //missionParametersDetailsInstance.targetFloor = ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].FLOOR_ID;
                                    //missionParametersDetailsInstance.targetDepthOfLine = ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].POSITION_NUMBER_IN_RACK;


                                    if (!ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].IsPALLET_CODENull())
                                    {
                                        missionParametersDetailsInstance.palletCode = ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].PALLET_CODE;
                                    }

                                    try
                                    {
                                        //missionQueue.Enqueue(missionParametersDetailsInstance);
                                        //Log.Debug($"Mission enqueued: {missionParametersDetailsInstance.missionId}, Type: {missionParametersDetailsInstance.taskType}");
                                        giveMissionToStacker(missionParametersDetailsInstance);
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error("Error while enqueuing mission: " + ex.Message + " StackTrace: " + ex.StackTrace);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("startOperation :: Exception occured while setting transfer mission values :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error("startOperation :: Exception occured while checking position details :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                        }
                    }
                    else
                    {
                        ats_wms_check_a1_mission_detailsTableAdapterInstance.UpdateIS_CHECKEDWhereCHECK_A1_MISSION_DETAILS_ID(0, checkId);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("startOperation :: Exception occured while stopping timer :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                }
            }

        }


        public void giveMissionToStacker(MissionParametersDetails missionParametersDetailsInstance)
        {
            //ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR = ats_wms_master_task_confirmation_tag_detailsTableAdapterInstance.GetDataBySTACKER_IDAndTAG_TYPE(STACKER_1, STACKER_1_DATA_REQUEST);
            if (readTag("ATS.WMS_STACKER_1.STACKER_1_DATA_REQUEST").Equals("True"))
            //if (ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR != null && ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR.Count > 0)
            {
                Log.Debug("tag Name :: " + ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR[0].TASK_CONFIRMATION_TAG_NAME);
                //if (readTag(ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR[0].TASK_CONFIRMATION_TAG_NAME).Equals("True"))
                ////if (readTag("ATS.WMS_STACKER_1.STACKER_1_DATA_REQUEST").Equals("True"))
                //{
                    //getting source position tags
                    Log.Debug("20 : Getiing source position tags");
                    try
                    {

                        //Fetching data from DB by sending tag type as source tag
                        ats_wms_master_stacker_tag_detailsDataTableSourceDT = ats_wms_master_stacker_tag_detailsTableAdapterInstance.GetDataByTAG_TYPEAndAREA_ID(sourcePositionTagType, areaId);

                        //checking if data is available in Database
                        if (ats_wms_master_stacker_tag_detailsDataTableSourceDT != null && ats_wms_master_stacker_tag_detailsDataTableSourceDT.Count > 0)
                        {
                            Log.Debug("giveMissionToStacker :: Found Source Tags");
                            //Multiple source tag will receive so Complaring each row and witing the value in to plc tag for respecive type of source tag
                            for (int i = 0; i < ats_wms_master_stacker_tag_detailsDataTableSourceDT.Count; i++)
                            {
                                Thread.Sleep(100);
                                //Comparing if the tag description matched name FLOOR to write the source floor in plc tag
                                if (ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_DESC.Equals("FLOOR"))
                                {
                                    //Writting the source floor into plc tag(source floor is default)
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME, missionParametersDetailsInstance.startFloor.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME + " Source FLOOR :: " + missionParametersDetailsInstance.startFloor.ToString());
                                }

                                //Comparing if the tag description matched name Column to write the source Column in plc tag
                                else if (ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_DESC.Equals("COLUMN"))
                                {
                                    //Writeing the source Column into plc tag(source Column is default)
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME, missionParametersDetailsInstance.startColumn.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME + " Source Column :: " + missionParametersDetailsInstance.startColumn.ToString());
                                }

                                //Comparing if the tag description matched name direction to write the source side of ASRS in plc tag
                                else if (ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_DESC.Equals("DIRECTION")) // Left/Right
                                {
                                    //Writting the source side of ASRS into plc tag(source side is default)
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME, missionParametersDetailsInstance.startLine.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME + " Source Direction :: " + missionParametersDetailsInstance.startLine.ToString());
                                }

                                //Comparing if the tag description matched name position number in rack to write the source position in plc tag
                                else if (ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_DESC.Equals("POSITION NUMBER IN RACK"))
                                {
                                    //Writting the source position into plc tag(source position is default)
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME, missionParametersDetailsInstance.startDepthOfLine.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME + "Source Position Number In Rack :: " + missionParametersDetailsInstance.startDepthOfLine.ToString());
                                }
                            }
                        }
                        else
                        {
                            Log.Debug("GiveStackerMission :: giveMissionToStacker :: Stacker source position details is not found in the database.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("startOperation :: giveMissionToStacker :: Exception occured while getting Source Tags for tag type :: " + sourcePositionTagType + " " + ex.Message + " stackTrace :: " + ex.StackTrace);
                    }


                    //getting destination position tags
                    Log.Debug("21 : Getiing destination position tags");
                    try
                    {
                        //Fetching data from DB by sending tag type as destination tag
                        ats_wms_master_stacker_tag_detailsDataTableDestinationDT = ats_wms_master_stacker_tag_detailsTableAdapterInstance.GetDataByTAG_TYPEAndAREA_ID(destinationPositionTagType, areaId);

                        //Checking if the data is availble in Database
                        if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT != null && ats_wms_master_stacker_tag_detailsDataTableDestinationDT.Count > 0)
                        {
                            Log.Debug("giveMissionToStacker :: Found Destination Tags");
                            //Multiple destination tag will receive so Complaring each row and witing the value in to plc tag for respecive type of destination tag fetching from infeed mission table in DB
                            for (int j = 0; j < ats_wms_master_stacker_tag_detailsDataTableDestinationDT.Count; j++)
                            {
                                Thread.Sleep(100);
                                //Comparing if the tag description matched name FLOOR to write the destination floor in plc tag
                                if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("FLOOR"))
                                {
                                    //Writting the destination floor into plc tag (fetching from infeed mission table)
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, missionParametersDetailsInstance.targetFloor.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination FLOOR :: " + missionParametersDetailsInstance.targetFloor.ToString());
                                }

                                //Comparing if the tag description matched name Column to write the source destination in plc tag
                                else if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("COLUMN"))
                                {
                                    //Writting the destination column into plc tag (fetching from infeed mission table)
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, missionParametersDetailsInstance.targetColumn.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination Column :: " + missionParametersDetailsInstance.targetColumn.ToString());
                                }

                                //Comparing if the tag description matched name direction to write the destination side of ASRS in plc tag
                                else if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("DIRECTION"))
                                {
                                    //Writeing the destination side of ASRS into plc tag(fetching from infeed mission table)
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, missionParametersDetailsInstance.targetLine.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination Direction :: " + missionParametersDetailsInstance.targetLine.ToString());
                                }

                                //Comparing if the tag description matched name direction to write the destination side of ASRS in plc tag
                                else if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("POSITION NUMBER IN RACK"))
                                {
                                    //Writeing the destination side of ASRS into plc tag
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, missionParametersDetailsInstance.targetDepthOfLine.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination position in rack :: " + missionParametersDetailsInstance.targetDepthOfLine.ToString());
                                    destinationPositionNumberInRack = missionParametersDetailsInstance.targetDepthOfLine;
                                }

                                //Comparing if the tag description matched name position number in rack to write the destination position in plc tag
                                else if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("STACKER INFEED MISSION ID"))
                                {
                                    //Writting the source position into plc tag(fetching from infeed mission table)
                                    writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, missionParametersDetailsInstance.missionId.ToString());
                                    Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination stacker Mission id :: " + missionParametersDetailsInstance.missionId.ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("startOperation :: Exception occured while getting Destination Tags for tag type :: " + destinationPositionTagType + " " + ex.Message + " stackTrace :: " + ex.StackTrace);
                    }


                    //writting pallet code in PLC tag
                    try
                    {
                        Log.Debug("giveMissionToStacker :: Writing pallet code in PLC Tag");
                        //checking if the pallet code is not null
                        {

                            writeTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_PALLET_CODE", missionParametersDetailsInstance.palletCode);
                            Log.Debug("ATS.WMS_STACKER_1.STACKER_1_MISSION_PALLET_CODE ::" + missionParametersDetailsInstance.palletCode);

                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While writting pallet code in PLC tag. " + ex.Message + " stackTrace :: " + ex.StackTrace);
                    }




                    try
                    {
                        //Fetching tag details from DB by sending Decision Point Name to get task type  

                        ats_wms_master_decision_point_direction_detailsDataTableDT = ats_wms_master_decision_point_direction_detailsTableAdapterInstance.GetDataByDECISION_POINT_NAME("STACKER_1.STACKER_1_TASK_TYPE");

                        //Checking if the data is availble in DB
                        if (ats_wms_master_decision_point_direction_detailsDataTableDT != null && ats_wms_master_decision_point_direction_detailsDataTableDT.Count > 0)
                        {
                            Log.Debug("giveMissionToStacker :: Writing task type as :: " + missionParametersDetailsInstance.taskType.ToString());
                            //Writing task type in PLC tag . write task type as infeed task type not default type
                            writeTag(ats_wms_master_decision_point_direction_detailsDataTableDT[0].DECISION_POINT_DIRECTION_TAG_NAME, missionParametersDetailsInstance.taskType.ToString());

                            //Log.Debug("GiveStackerMission :: giveMissionToStacker :: Give pallet infeed task value (1) to the PLC in tag :: " + master_decision_point_direction_detailsDataTableDT[0].DECISION_POINT_DIRECTION_TAG_NAME
                            //+ " value :: " + master_decision_point_direction_detailsDataTableDT[0].DECISION_POINT_DIRECTION_TAG_DEFAULT_VALUE);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("startOperation :: Exception occured while getting task type :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                    }


                    try
                    {
                        //Changing status of mission to IN_PROGRESS in DB
                        String timeNow = DateTime.Now.TimeOfDay.ToString();
                        TimeSpan currentTimeNow = TimeSpan.Parse(timeNow);

                        String currentDate = "";
                        currentDate = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                        if (missionParametersDetailsInstance.taskType == 1)
                        {
                            try
                            {
                                Log.Debug("giveMissionToStacker :: Updating Mission Status to IN_PROGRESS");
                                //Updating the mission status by sending infeed mission ID
                                ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.UpdateINFEED_MISSION_STATUSAndINFEED_MISSION_START_DATETIMEWhereINFEED_MISSION_ID("IN_PROGRESS", (currentDate + " " + currentTimeNow), missionParametersDetailsInstance.missionId);
                                Log.Debug("GiveStackerMission :: giveMissionToStacker :: Status Updated to IN_PROGRESS for infeed mission ID :: " + ats_wms_infeed_mission_runtime_detailsDataTableDT[0].INFEED_MISSION_ID);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While updating the status of the infeed mission to IN_PROGRESS. " + ex.Message + " stackTrace :: " + ex.StackTrace);
                            }
                        }
                        else if (missionParametersDetailsInstance.taskType == 2)
                        {
                            try
                            {
                                //Updating the mission status by sending outfeed mission ID
                                ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.UpdateOUTFEED_MISSION_STATUSAndOUTFEED_MISSION_START_DATETIMEWhereOUTFEED_MISSION_ID((currentDate + " " + currentTimeNow), "IN_PROGRESS", missionParametersDetailsInstance.missionId);

                                Log.Debug("GiveStackerMission :: giveMissionToStacker :: Status Updated to IN_PROGRESS for outfeed mission ID :: " + ats_wms_outfeed_mission_runtime_detailsDataTableDT[0].OUTFEED_MISSION_ID);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While updating the status of the mission to IN_PROGRESS. " + ex.Message + " stackTrace :: " + ex.StackTrace);
                            }
                        }
                        else if (missionParametersDetailsInstance.taskType == 3)
                        {
                            try
                            {
                                //Updating the mission status by sending transfer mission ID
                                ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.UpdateTRANSFER_MISSION_START_DATETIMEAndTRANSFER_MISSION_STATUSWhereTRANSFER_PALLET_MISSION_RUNTIME_DETAILS_ID((currentDate + " " + currentTimeNow), "IN_PROGRESS", missionParametersDetailsInstance.missionId);

                                Log.Debug("GiveStackerMission :: giveMissionToStacker :: Status Updated to IN_PROGRESS for outfeed mission ID :: " + ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT[0].TRANSFER_PALLET_MISSION_RUNTIME_DETAILS_ID);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While updating the status of the mission to IN_PROGRESS. " + ex.Message + " stackTrace :: " + ex.StackTrace);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While updating mission status to inprogress. " + ex.Message + " stackTrace :: " + ex.StackTrace);
                    }


                    for (;;)
                    {
                        Thread.Sleep(1000);
                        //reading stacker data request from the plc tag
                        try
                        {

                            Log.Debug("giveMissionToStacker :: Found Data Request Tag :: waiting to become false");
                            try
                            {
                                //Log.Debug("ATS.WMS_STACKER_1.STACKER_1_DATA_REQUEST :: " + readTag("ATS.WMS_STACKER_1.STACKER_1_DATA_REQUEST"));
                                Log.Debug("ATS.WMS_STACKER_1.STACKER_1_DATA_REQUEST :: " + readTag(ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR[0].TASK_CONFIRMATION_TAG_NAME));
                            }
                            catch (Exception ex)
                            {

                                Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While Getting Stacker Data Request." + ex.Message + " stackTrace :: " + ex.StackTrace);
                            }
                            //Reading and checking of the plc tag value(true = data request high and false = no data request)
                            //if (readTag("ATS.WMS_STACKER_1.STACKER_1_DATA_REQUEST").Equals("False"))
                            if (readTag(ats_wms_master_task_confirmation_tag_detailsDataTableDTStackerDR[0].TASK_CONFIRMATION_TAG_NAME).Equals("False"))
                            {
                                try
                                {
                                    Log.Debug("giveMissionToStacker :: going in ReadMissionFeedbackDetails");
                                    //Calling a method ReadMissionFeedbackDetails by sending infeed mission data
                                    ReadMissionFeedbackDetails(missionParametersDetailsInstance.missionId, missionParametersDetailsInstance.taskType);
                                    Log.Debug("Tags Flushed");
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While Getting ReadMissionFeedbackDetails" + ex.Message + " stackTrace :: " + ex.StackTrace);
                                }

                                Log.Debug("ReadMissionFeedbackDetails done");
                                positionNumberInRack = 0;
                                break;
                            }
                            else
                            {
                                Log.Debug("Data Request did not become False");
                            }

                        }
                        catch (Exception ex)
                        {
                            Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While Getting Stacker Data Request." + ex.Message + " stackTrace :: " + ex.StackTrace);
                        }
                    }
                //}
                //else
                //{
                //    Log.Debug("Data Request not True while giving Mission");
                //}
            }
            else
            {
                //Log.Debug("Data Request Tag is incorrect in Table");
                Log.Debug("Data Request not True while giving Mission");
            }

        }


        //public void ReadMissionFeedbackDetails(int missionId, int taskType)
        //{


        //    // if match flush the data
        //    for (;;)
        //    {
        //        Thread.Sleep(1000);
        //        try
        //        {
        //            int value = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_TASK_NO"));
        //            if (value == (missionId))
        //            {

        //                Thread.Sleep(2000);
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_START_COLUMN", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_START_FLOOR", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_START_LINE", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_START_POSITION_NUMBER_IN_RACK", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_COLUMN", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_FLOOR", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_LINE", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_POSITION_NUMBER_IN_RACK", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_ID", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_TASK_TYPE", "0");
        //                writeTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_PALLET_CODE", "0");
        //                break;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("ReadMissionFeedbackDetails ::  Exception while reading feedback task number" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //        }
        //    }
        //}

        public void ReadMissionFeedbackDetails(int missionId, int taskType)
        {
            for (;;)
            {
                Thread.Sleep(1000);
                try
                {
                    int value = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_TASK_NO"));
                    if (value == missionId)
                    {
                        Thread.Sleep(1000);

                        // Writing values
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_START_COLUMN", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_START_FLOOR", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_START_LINE", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_START_POSITION_NUMBER_IN_RACK", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_COLUMN", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_FLOOR", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_LINE", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_POSITION_NUMBER_IN_RACK", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_ID", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_TASK_TYPE", "0");
                        //writeTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_PALLET_CODE", "0");
                        Log.Debug("20 : Getiing source position tags");
                        try
                        {

                            //Fetching data from DB by sending tag type as source tag
                            ats_wms_master_stacker_tag_detailsDataTableSourceDT = ats_wms_master_stacker_tag_detailsTableAdapterInstance.GetDataByTAG_TYPEAndAREA_ID(sourcePositionTagType, areaId);

                            //checking if data is available in Database
                            if (ats_wms_master_stacker_tag_detailsDataTableSourceDT != null && ats_wms_master_stacker_tag_detailsDataTableSourceDT.Count > 0)
                            {
                                Log.Debug("giveMissionToStacker :: Found Source Tags");
                                //Multiple source tag will receive so Complaring each row and witing the value in to plc tag for respecive type of source tag
                                for (int i = 0; i < ats_wms_master_stacker_tag_detailsDataTableSourceDT.Count; i++)
                                {
                                    Thread.Sleep(100);
                                    //Comparing if the tag description matched name FLOOR to write the source floor in plc tag
                                    if (ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_DESC.Equals("FLOOR"))
                                    {
                                        //Writting the source floor into plc tag(source floor is default)
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME + " Source FLOOR :: " + "0");
                                    }

                                    //Comparing if the tag description matched name Column to write the source Column in plc tag
                                    else if (ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_DESC.Equals("COLUMN"))
                                    {
                                        //Writeing the source Column into plc tag(source Column is default)
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME + " Source Column :: " + "0");
                                    }

                                    //Comparing if the tag description matched name direction to write the source side of ASRS in plc tag
                                    else if (ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_DESC.Equals("DIRECTION")) // Left/Right
                                    {
                                        //Writting the source side of ASRS into plc tag(source side is default)
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME + " Source Direction :: " + "0");
                                    }

                                    //Comparing if the tag description matched name position number in rack to write the source position in plc tag
                                    else if (ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_DESC.Equals("POSITION NUMBER IN RACK"))
                                    {
                                        //Writting the source position into plc tag(source position is default)
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableSourceDT[i].STACKER_TAG_NAME + "Source Position Number In Rack :: " + "0");
                                    }
                                }
                            }
                            else
                            {
                                Log.Debug("GiveStackerMission :: giveMissionToStacker :: Stacker source position details is not found in the database.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error("startOperation :: giveMissionToStacker :: Exception occured while getting Source Tags for tag type :: " + sourcePositionTagType + " " + ex.Message + " stackTrace :: " + ex.StackTrace);
                        }


                        //getting destination position tags
                        Log.Debug("21 : Getiing destination position tags");
                        try
                        {
                            //Fetching data from DB by sending tag type as destination tag
                            ats_wms_master_stacker_tag_detailsDataTableDestinationDT = ats_wms_master_stacker_tag_detailsTableAdapterInstance.GetDataByTAG_TYPEAndAREA_ID(destinationPositionTagType, areaId);

                            //Checking if the data is availble in Database
                            if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT != null && ats_wms_master_stacker_tag_detailsDataTableDestinationDT.Count > 0)
                            {
                                Log.Debug("giveMissionToStacker :: Found Destination Tags");
                                //Multiple destination tag will receive so Complaring each row and witing the value in to plc tag for respecive type of destination tag fetching from infeed mission table in DB
                                for (int j = 0; j < ats_wms_master_stacker_tag_detailsDataTableDestinationDT.Count; j++)
                                {
                                    Thread.Sleep(100);
                                    //Comparing if the tag description matched name FLOOR to write the destination floor in plc tag
                                    if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("FLOOR"))
                                    {
                                        //Writting the destination floor into plc tag (fetching from infeed mission table)
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination FLOOR :: " + "0");
                                    }

                                    //Comparing if the tag description matched name Column to write the source destination in plc tag
                                    else if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("COLUMN"))
                                    {
                                        //Writting the destination column into plc tag (fetching from infeed mission table)
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination Column :: " + "0");
                                    }

                                    //Comparing if the tag description matched name direction to write the destination side of ASRS in plc tag
                                    else if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("DIRECTION"))
                                    {
                                        //Writeing the destination side of ASRS into plc tag(fetching from infeed mission table)
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination Direction :: " + "0");
                                    }

                                    //Comparing if the tag description matched name direction to write the destination side of ASRS in plc tag
                                    else if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("POSITION NUMBER IN RACK"))
                                    {
                                        //Writeing the destination side of ASRS into plc tag
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination position in rack :: " + "0");
                                        //destinationPositionNumberInRack = missionParametersDetailsInstance.targetDepthOfLine;
                                    }

                                    //Comparing if the tag description matched name position number in rack to write the destination position in plc tag
                                    else if (ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_DESC.Equals("STACKER INFEED MISSION ID"))
                                    {
                                        //Writting the source position into plc tag(fetching from infeed mission table)
                                        writeTag(ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME, "0");
                                        Log.Debug("GiveStackerMission :: giveMissionToStacker :: Tag Name :: " + ats_wms_master_stacker_tag_detailsDataTableDestinationDT[j].STACKER_TAG_NAME + " Destination stacker Mission id :: " + "0");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error("startOperation :: Exception occured while getting Destination Tags for tag type :: " + destinationPositionTagType + " " + ex.Message + " stackTrace :: " + ex.StackTrace);
                        }


                        //writting pallet code in PLC tag
                        try
                        {
                            Log.Debug("giveMissionToStacker :: Writing pallet code in PLC Tag");
                            //checking if the pallet code is not null
                            {
                                writeTag("ATS.WMS_STACKER_1.STACKER_1_TASK_TYPE", "0");
                                Log.Debug("ATS.WMS_STACKER_1.STACKER_1_TASK_TYPE ::" + "0");
                                writeTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_PALLET_CODE", "0");
                                Log.Debug("ATS.WMS_STACKER_1.STACKER_1_MISSION_PALLET_CODE ::" + "0");

                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error("GiveStackerMission :: giveMissionToStacker :: Error While writting pallet code in PLC tag. " + ex.Message + " stackTrace :: " + ex.StackTrace);
                        }

                        // Checking values
                        if (
                            readTag("ATS.WMS_STACKER_1.STACKER_1_START_COLUMN") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_START_FLOOR") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_START_LINE") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_START_POSITION_NUMBER_IN_RACK") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_COLUMN") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_FLOOR") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_LINE") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_POSITION_NUMBER_IN_RACK") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_ID") == "0" &&
                            readTag("ATS.WMS_STACKER_1.STACKER_1_TASK_TYPE") == "0" &&
                            readTag(MISSION_PALLET_CODE) == "0"
                        )
                        {
                            break; // Exit the loop if all values are zero
                        }


                    }
                }
                catch (Exception ex)
                {
                    //Log.Error("ReadMissionFeedbackDetails :: Exception while reading feedback task number: " + ex.Message + " Stacktrace:: " + ex.StackTrace);
                    // Continue the loop despite the error

                    int hResult = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                    string comError = (ex is System.Runtime.InteropServices.COMException) ? ((System.Runtime.InteropServices.COMException)ex).ErrorCode.ToString() : "No COM error";

                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading feedback task number :: " + ex.Message
                    + " HResult :: " + hResult
                    + " COM Component Error :: " + comError
                    + " stackTrace :: " + ex.StackTrace);
                }
            }
        }

        //public void ReadMissionFeedbackDetails(int missionId, int taskType)
        //{
        //    //This function is used to compare and flush the tags if shared tags to PLC and IT are same
        //    try
        //    {
        //        //fetching the source feedback tags from DB (the PLC tags which contains mission the data shared by IT )
        //        ats_wms_master_stacker_tag_detailsDataTableFeedbackDT = ats_wms_master_stacker_tag_detailsTableAdapterInstance.GetDataByTAG_TYPEAndAREA_ID(feedbackTagType, areaId);


        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("ReadMissionFeedbackDetails :: Exception while read feed back data from stacker tag details" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //    }
        //    Log.Debug("Fetched source feedback tags from the DB");

        //    //if match flush the data
        //    for (;;)
        //    {
        //        Thread.Sleep(1000);
        //        try
        //        {



        //            try
        //            {
        //                //Erasing start column tag data
        //                Log.Debug("1.Reading Feedback Column Tag");
        //                //string startFBColumn = readTag("ATS.WMS_STACKER_1.STACKER_1_FB_START_COLUMN");
        //                //Log.Debug("startFBColumn :: " + startFBColumn);
        //                int value1 = 0;
        //                try
        //                {
        //                value1 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_START_COLUMN"));
        //                Log.Debug("1.1: reading Column  tag :: value1::" + value1);

        //                }
        //                catch (Exception ex)
        //                {

        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading FB column" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v1 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_START_COLUMN"));
        //                //Log.Debug("v1::" + v1);
        //                Log.Debug("FBVALUE 1 :: " + value1 + "" + "VALUE ::" + v1);

        //                if (v1 != 0 && value1 != 0 && v1 == value1)
        //                {

        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_START_COLUMN", "0");

        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing Column Tag");

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading column" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //            }



        //            try
        //            {
        //                //Erasing start floor tag data
        //                Log.Debug("2.Reading Feedback floor Tag");
        //                int value2 = 0;
        //                try
        //                {
        //                value2 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_START_FLOOR"));
        //                Log.Debug("2.1: reading Feedback floor tag :: value2::" + value2);

        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading FB floor" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v2 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_START_FLOOR"));
        //                // Log.Debug("v2::" + v2);
        //                Log.Debug("FBVALUE floor :: " + value2 + "" + "VALUE ::" + v2);

        //                if (v2 != 0 && value2 != 0 && v2 == value2)
        //                {

        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_START_FLOOR", "0");
        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing Floor Tag");


        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading floor" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //            }


        //            try
        //            {
        //                //Erasing start Direction data
        //                Log.Debug("3.Reading Direction Column Tag");
        //                int value3 = 0;
        //                try
        //                {
        //                value3 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_START_LINE"));
        //                Log.Debug("3.1: reading Column  tag :: value3::" + value3);

        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading FB side/line " + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v3 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_START_LINE"));
        //                //Log.Debug("v3::" + v3);
        //                Log.Debug("FBVALUE 3 :: " + value3 + "" + "VALUE ::" + v3);


        //                if (v3 != 0 && value3 != 0 && v3 == value3)
        //                {


        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_START_LINE", "0");
        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing Direction Tag");


        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading side" + ex.Message + " Stacktrace:: " + ex.StackTrace);

        //            }

        //            try
        //            {
        //                //Erasing start Rack data
        //                Log.Debug("4.Reading Rack Column Tag");
        //                int value4 = 0;

        //                try
        //                {
        //                value4 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_START_POSITION_NUMBER_IN_RACK"));
        //                Log.Debug("4.1: reading Column  tag :: value3::" + value4);

        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading FB rack" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v4 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_START_POSITION_NUMBER_IN_RACK"));
        //                // Log.Debug("v4::" + v4);
        //                Log.Debug("FBVALUE 4 :: " + value4 + "" + "VALUE ::" + v4);
        //                if (v4 != 0 && value4 != 0 && v4 == value4)
        //                {

        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_START_POSITION_NUMBER_IN_RACK", "0");
        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing Rack Tag");


        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading rack" + ex.Message + " Stacktrace:: " + ex.StackTrace);

        //            }
        //            try
        //            {
        //                //Erasing Destination column tag data
        //                Log.Debug("5.Reading Feedback Column Tag");
        //                int value5 = 0;
        //                try
        //                {
        //                value5 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_TARGET_COLUMN"));
        //                Log.Debug("5.1: reading Column  tag :: value5::" + value5);

        //                }
        //                catch (Exception ex)
        //                {

        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading target FB column" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v5 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_COLUMN"));
        //                //Log.Debug("v5::" + v5);
        //                Log.Debug("FBVALUE 5 :: " + value5 + "" + "VALUE ::" + v5);
        //                if (v5 != 0 && value5 != 0 && v5 == value5)
        //                {

        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_COLUMN", "0");

        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing feedback Column Tag");


        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading target column" + ex.Message + " Stacktrace:: " + ex.StackTrace);

        //            }

        //            try
        //            {
        //                //Erasing Destination floor tag data
        //                Log.Debug("6.Reading Feedback floor Tag");
        //                int value6 = 0;
        //                try
        //                {
        //                value6 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_TARGET_FLOOR"));
        //                Log.Debug("6.1: reading Feedback floor tag :: value6::" + value6);

        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading FB target floor" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v6 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_FLOOR"));
        //                // Log.Debug("v6::" + v6);
        //                Log.Debug("FBVALUE 6 :: " + value6 + "" + "VALUE ::" + v6);

        //                if (v6 != 0 && value6 != 0 && v6 == value6)
        //                {

        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_FLOOR", "0");
        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing feedback Floor Tag");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading target floor" + ex.Message + " Stacktrace:: " + ex.StackTrace);

        //            }

        //            try
        //            {
        //                //Erasing Destination Direction data
        //                Log.Debug("7.Reading Direction line Tag");
        //                int value7 = 0;
        //                try
        //                {
        //                value7 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_TARGET_LINE"));
        //                Log.Debug("7.1: reading Direction  tag :: value7::" + value7);

        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading FB line" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v7 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_LINE"));
        //                //Log.Debug("v7::" + v7);
        //                Log.Debug("FBVALUE 7 :: " + value7 + "" + "VALUE ::" + v7);
        //                if (v7 != 0 && value7 != 0 && v7 == value7)
        //                {

        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_LINE", "0");
        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing feedback Direction Tag");

        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading line" + ex.Message + " Stacktrace:: " + ex.StackTrace);

        //            }

        //            try
        //            {
        //                //Erasing Destination Rack data
        //                Log.Debug("Reading Rack target position Tag");
        //                int value8 = 0;
        //                try
        //                {
        //                value8 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_TARGET_POSITION_NUMBER_IN_RACK"));
        //                Log.Debug("8.1: reading Rack tag :: value8::" + value8);

        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading target FB column" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v8 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_POSITION_NUMBER_IN_RACK"));
        //                //Log.Debug("v8::" + v8);
        //                Log.Debug("FBVALUE 8 :: " + value8 + "" + "VALUE ::" + v8);


        //                if (v8 != 0 && value8 != 0 && v8 == value8)
        //                {

        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_POSITION_NUMBER_IN_RACK", "0");
        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing feedback rack Tag");


        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading target column" + ex.Message + " Stacktrace:: " + ex.StackTrace);

        //            }


        //            try
        //            {
        //                //Erasing feedback task no data
        //                Log.Debug("Reading feedback task no Tag");
        //                int value9 = 0;
        //                try
        //                {
        //                value9 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_TASK_NO"));
        //                Log.Debug("9.1: reading Fedback Task No tag :: value9::" + value9);

        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("ReadMissionFeedbackDetails :: Exception while reading target mission id" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v9 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_ID"));
        //                //Log.Debug("v9 ::" + v9);
        //                Log.Debug("FBVALUE 9 :: " + value9 + "" + "VALUE ::" + v9);
        //                if (v9 != 0 && value9 != 0 && v9 == value9)
        //                {

        //                    writeTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_ID", "0");
        //                    Log.Debug("ReadMissionFeedBackDetails::Erasing feedback task no Tag");


        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading target mission id" + ex.Message + " Stacktrace:: " + ex.StackTrace);

        //            }



        //            try
        //            {
        //                int value10 = 0;
        //                try
        //                {
        //                value10 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_FB_TASK_TYPE"));
        //                Log.Debug("value10::" + value10);

        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("ReadMissionFeedbackDetails ::  Exception while writing pallet code and task type" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                }
        //                int v10 = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TASK_TYPE"));
        //                //Log.Debug("v10 ::" + v10);
        //                Log.Debug("FBVALUE 10 :: " + value10 + "" + "VALUE ::" + v10);
        //                if (v10 != 0 && value10 != 0 && v10 == value10)
        //                {
        //                    try
        //                    {
        //                        //Erasing mission pallet code  and task type to 0

        //                        writeTag("ATS.WMS_STACKER_1.STACKER_1_TASK_TYPE", "0");
        //                        writeTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_PALLET_CODE", "0");
        //                        Log.Debug("All Tags Data Erase");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Log.Error("ReadMissionFeedbackDetails ::  Exception while writing pallet code and task type" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //                    }

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("ReadMissionFeedbackDetails :: Exception while reading target task type" + ex.Message + " Stacktrace:: " + ex.StackTrace);

        //            }




        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("ReadMissionFeedbackDetails :: Exception while read destination back data from stacker tag details" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //        }
        //        try
        //        {
        //            int startColumn = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_START_COLUMN"));

        //            int startFloor = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_START_FLOOR"));

        //            int startLine = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_START_LINE"));

        //            int startDepth = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_START_POSITION_NUMBER_IN_RACK"));

        //            int targetColumn = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_COLUMN"));

        //            int targetFloor = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_FLOOR"));

        //            int targetLine = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_LINE"));

        //            int targetDepth = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TARGET_POSITION_NUMBER_IN_RACK"));

        //            int missionID = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_MISSION_ID"));

        //            int task_Type = Convert.ToInt32(readTag("ATS.WMS_STACKER_1.STACKER_1_TASK_TYPE"));


        //            if (startColumn == 0 && startFloor == 0 && startLine == 0 && startDepth == 0 && targetColumn == 0 && targetFloor == 0 && targetLine == 0 && targetDepth == 0 && missionID == 0 && task_Type == 0)
        //            {
        //                Log.Debug("FeedBack value and source/target values matching , braking the loop");

        //                break;
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            Log.Error("ReadMissionFeedbackDetails :: Exception while read destination back data from stacker tag details" + ex.Message + " Stacktrace:: " + ex.StackTrace);
        //        }




        //    }

        //}

        //public Tuple<string, string> GetPalletPresentAndPalletCode()
        //{
        //    string palletPresent1 = "";
        //    string palletCode1 = "";

        //    try
        //    {
        //        palletPresent1 = readTag("ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_PRESENT");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Exception occured reading data in ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_PRESENT ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //    }

        //    if (palletPresent1.Equals("True"))
        //    {
        //        try
        //        {
        //            if (palletPresent1.Equals("True"))
        //            {
        //                try
        //                {
        //                    palletCode1 = readTag("ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_CODE").Trim();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("Exception occured reading data in ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_CODE ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("Exception occured while getting stacker tag details for Pallet code  ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //        }

        //        return Tuple.Create(palletPresent1, palletCode1);
        //    }
        //    else if (palletPresent1.Equals("False"))
        //    {
        //        string palletPresent2 = "";
        //        string palletCode2 = "";

        //        try
        //        {
        //            palletPresent2 = readTag("ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_PRESENT");
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("Exception occured reading data in ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_PRESENT ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //        }

        //        if (palletPresent2.Equals("True"))
        //        {
        //            try
        //            {
        //                if (palletPresent2.Equals("True"))
        //                {
        //                    try
        //                    {
        //                        palletCode2 = readTag("ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_CODE").Trim();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Log.Error("Exception occured reading data in ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_CODE ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("Exception occured while getting stacker tag details for Pallet code  ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //            }

        //            return Tuple.Create(palletPresent2, palletCode2);
        //        }
        //        else if (palletPresent2.Equals("False"))
        //        {
        //            string palletPresent3 = "";
        //            string palletCode3 = "";

        //            try
        //            {
        //                palletPresent3 = readTag("ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_PRESENT");
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("Exception occured reading data in ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_PRESENT ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //            }

        //            if (palletPresent3.Equals("True"))
        //            {
        //                try
        //                {
        //                    if (palletPresent3.Equals("True"))
        //                    {
        //                        try
        //                        {
        //                            palletCode3 = readTag("ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_CODE").Trim();
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Log.Error("Exception occured reading data in ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_CODE ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("Exception occured while getting stacker tag details for Pallet code  ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
        //                }

        //                return Tuple.Create(palletPresent3, palletCode3);
        //            }
        //        }
        //    }


        //    return Tuple.Create("", "");
        //}



        //public Tuple<string, string> GetPalletPresentAndPalletCode()
        //{
        //    string palletPresent1 = "";
        //    string palletCode1 = "";
        //    string palletPresent2 = "";
        //    string palletCode2 = "";
        //    string palletPresent3 = "";
        //    string palletCode3 = "";

        //    try
        //    {
        //        palletPresent1 = readTag("ATS.WMS_STACKER_1.CS_4_PICKUP_POSITION_PALLET_PRESENT");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Exception occurred reading data in ATS.WMS_STACKER_1.CS_4_PICKUP_POSITION_PALLET_PRESENT :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //    }

        //    if (palletPresent1.Equals("True"))
        //    {
        //        try
        //        {
        //            palletCode1 = readTag("ATS.WMS_STACKER_1.CS_4_PICKUP_POSITION_PALLET_CODE").Trim();
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("Exception occurred reading data in ATS.WMS_STACKER_1.AREA_1_LOADING_STATION_PALLET_CODE :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //        }
        //        return Tuple.Create(palletPresent1, palletCode1);
        //    }

        //    try
        //    {
        //        palletPresent2 = readTag("ATS.WMS_STACKER_1.CS_5_PICKUP_POSITION_PALLET_PRESENT");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Exception occurred reading data in ATS.WMS_STACKER_1.CS_5_PICKUP_POSITION_PALLET_PRESENT :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //    }

        //    if (palletPresent2.Equals("True"))
        //    {
        //        try
        //        {
        //            palletCode2 = readTag("ATS.WMS_STACKER_1.CS_5_PICKUP_POSITION_PALLET_CODE").Trim();
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("Exception occurred reading data in ATS.WMS_STACKER_2.STACKER_2_LOADING_STATION_PALLET_CODE :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //        }
        //        return Tuple.Create(palletPresent2, palletCode2);
        //    }

        //    try
        //    {
        //        palletPresent3 = readTag("ATS.WMS_STACKER_1.CS_6_PICKUP_POSITION_PALLET_PRESENT");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Exception occurred reading data in ATS.WMS_STACKER_1.CS_6_PICKUP_POSITION_PALLET_PRESENT :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //    }

        //    if (palletPresent3.Equals("True"))
        //    {
        //        try
        //        {
        //            palletCode3 = readTag("ATS.WMS_STACKER_1.CS_6_PICKUP_POSITION_PALLET_CODE").Trim();
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("Exception occurred reading data in ATS.WMS_STACKER_3.STACKER_3_LOADING_STATION_PALLET_CODE :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //        }
        //        return Tuple.Create(palletPresent3, palletCode3);
        //    }

        //    return Tuple.Create("", "");
        //}



        //public string checkPalletPresenceAtEmptyPlacePosition()
        //{
        //    string palletPresentCS6Empty = "";
        //    string palletPresentCS5Empty = "";
        //    string palletPresentCS4Empty = "";

        //    try
        //    {
        //        palletPresentCS6Empty = readTag("ATS.WMS_STACKER_1.CS_6_PICKUP_POSITION_PALLET_PRESENT");
        //        if (palletPresentCS6Empty.Equals("False"))
        //        {
        //            return palletPresentCS6Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Exception occurred reading data in ATS.WMS_STACKER_1.CS_4_PICKUP_POSITION_PALLET_PRESENT :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //    }

        //    try
        //    {
        //        palletPresentCS5Empty = readTag("ATS.WMS_STACKER_1.CS_5_PICKUP_POSITION_PALLET_PRESENT");
        //        if (palletPresentCS5Empty.Equals("False"))
        //        {
        //            return palletPresentCS5Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Exception occurred reading data in ATS.WMS_STACKER_1.CS_4_PICKUP_POSITION_PALLET_PRESENT :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //    }

        //    try
        //    {
        //        palletPresentCS4Empty = readTag("ATS.WMS_STACKER_1.CS_4_PICKUP_POSITION_PALLET_PRESENT");
        //        if (palletPresentCS4Empty.Equals("False"))
        //        {
        //            return palletPresentCS4Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("Exception occurred reading data in ATS.WMS_STACKER_1.CS_4_PICKUP_POSITION_PALLET_PRESENT :: " + ex.Message + " StackTrace:: " + ex.StackTrace);
        //    }

        //    return "True"; // Return "False" if no pallet is present in any area
        //}




        #region Ping funcationality

        public Boolean checkPlcPingRequest()
        {
            //Log.Debug("IprodPLCMachineXmlGenOperation :: Inside checkServerPingRequest");

            try
            {
                try
                {
                    pingSenderForThisConnection = new Ping();
                    replyForThisConnection = pingSenderForThisConnection.Send(IP_ADDRESS);
                }
                catch (Exception ex)
                {
                    Log.Error("checkPlcPingRequest :: for IP :: " + IP_ADDRESS + " Exception occured while sending ping request :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                    replyForThisConnection = null;
                }

                if (replyForThisConnection != null && replyForThisConnection.Status == IPStatus.Success)
                {
                    //Log.Debug("checkPlcPingRequest :: for IP :: " + IP_ADDRESS + " Ping success :: " + replyForThisConnection.Status.ToString());
                    return true;
                }
                else
                {
                    //Log.Debug("checkPlcPingRequest :: for IP :: " + IP_ADDRESS + " Ping failed. ");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("checkPlcPingRequest :: for IP :: " + IP_ADDRESS + " Exception while checking ping request :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region Read and Write PLC tag

        [HandleProcessCorruptedStateExceptions]
        //public string readTag(string tagName)
        //{

        //    try
        //    {
        //        //Log.Debug("IprodPLCCommunicationOperation :: Inside readTag.");

        //        // Set PLC tag
        //        OPCItemIDs.SetValue(tagName, 1);
        //        //Log.Debug("readTag :: Plc tag is configured for plc group.");

        //        // remove all group
        //        ConnectedOpc.OPCGroups.RemoveAll();
        //        //Log.Debug("readTag :: Remove all group.");

        //        // Kepware configuration                
        //        OpcGroupNames = ConnectedOpc.OPCGroups.Add("AtsWmsS1MasterGiveMissionServiceDetailsGroup");
        //        OpcGroupNames.DeadBand = 0;
        //        OpcGroupNames.UpdateRate = 100;
        //        OpcGroupNames.IsSubscribed = true;
        //        OpcGroupNames.IsActive = true;
        //        OpcGroupNames.OPCItems.AddItems(1, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);
        //        //Log.Debug("readTag :: Kepware properties configuration is complete.");

        //        // Read tag
        //        OpcGroupNames.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, 1, ref
        //           ItemServerHandles, out ItemServerValues, out ItemServerErrors, out yDIR, out yDIR);

        //        //Log.Debug("readTag ::  tag name :: " + tagName + " tag value :: " + Convert.ToString(ItemServerValues.GetValue(1)));

        //        if (Convert.ToString(ItemServerValues.GetValue(1)).Equals("True"))
        //        {
        //            Log.Debug("readTag :: Found and Return True");
        //            return "True";
        //        }
        //        else if (Convert.ToString(ItemServerValues.GetValue(1)).Equals("False"))
        //        {
        //            Log.Debug("readTag :: Found and Return False");
        //            return "False";
        //        }
        //        else
        //        {
        //            Log.Debug("readTag :: Found read value :: " + (ItemServerValues.GetValue(1)));
        //            return Convert.ToString(ItemServerValues.GetValue(1));

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("readTag :: Exception while reading plc tag :: " + tagName + " :: " + ex.Message);
        //        //OnConnectPLC();
        //    }

        //    Log.Debug("readTag :: Return False.. retun null.");

        //    return "False";
        //}


        public string readTag(string tagName)
        {

            try
            {
                //Log.Debug("IprodPLCCommunicationOperation :: Inside readTag.");

                // Set PLC tag
                OPCItemIDs.SetValue(tagName, 1);
                //Log.Debug("readTag :: Plc tag is configured for plc group.");

                // remove all group
                ConnectedOpc.OPCGroups.RemoveAll();
                //Log.Debug("readTag :: Remove all group.");

                // Kepware configuration                
                OpcGroupNames = ConnectedOpc.OPCGroups.Add("AtsWmsS1MasterGiveMissionServiceDetailsGroup");
                OpcGroupNames.DeadBand = 0;
                OpcGroupNames.UpdateRate = 500;
                OpcGroupNames.IsSubscribed = true;
                OpcGroupNames.IsActive = true;
                OpcGroupNames.OPCItems.AddItems(1, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);
                //Log.Debug("readTag :: Kepware properties configuration is complete.");

                // Read tag
                OpcGroupNames.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, 1, ref
                   ItemServerHandles, out ItemServerValues, out ItemServerErrors, out yDIR11, out yDIR11);

                //Log.Debug("readTag ::  tag name :: " + tagName + " tag value :: " + Convert.ToString(ItemServerValues.GetValue(1)));

                if (Convert.ToString(ItemServerValues.GetValue(1)).Equals("True"))
                {
                    //Log.Debug("readTag :: Found and Return True");
                    return "True";
                }
                else if (Convert.ToString(ItemServerValues.GetValue(1)).Equals("False"))
                {
                    //Log.Debug("readTag :: Found and Return False");
                    return "False";
                }
                else
                {
                    return Convert.ToString(ItemServerValues.GetValue(1));
                }

            }
            catch (Exception ex)
            {
                Log.Error("readTag :: Exception while reading plc tag :: " + tagName + " :: " + ex.Message);
            }

            Log.Debug("readTag :: Return False.. retun null.");

            return null;
        }

        [HandleProcessCorruptedStateExceptions]
        public Boolean writeTag(string tagName, string tagValue)
        {

            try
            {
                Log.Debug("IprodGiveMissionToStacker :: Inside writeTag.");

                // Set PLC tag
                OPCItemIDs.SetValue(tagName, 1);
                //Log.Debug("writeTag :: Plc tag is configured for plc group.");

                // remove all group
                ConnectedOpc.OPCGroups.RemoveAll();
                //Log.Debug("writeTag :: Remove all group.");

                // Kepware configuration                  
                OpcGroupNames = ConnectedOpc.OPCGroups.Add("AtsWmsS1MasterGiveMissionServiceDetailsGroup");
                OpcGroupNames.DeadBand = 0;
                OpcGroupNames.UpdateRate = 500;
                OpcGroupNames.IsSubscribed = true;
                OpcGroupNames.IsActive = true;
                OpcGroupNames.OPCItems.AddItems(1, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);
                //Log.Debug("writeTag :: Kepware properties configuration is complete.");

                // read plc tags
                OpcGroupNames.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, 1, ref
                   ItemServerHandles, out ItemServerValues, out ItemServerErrors, out yDIR11, out yDIR11);

                // Add tag value
                ItemServerValues.SetValue(tagValue, 1);

                // Write tag
                OpcGroupNames.SyncWrite(1, ref ItemServerHandles, ref ItemServerValues, out ItemServerErrors);

                return true;

            }
            catch (Exception ex)
            {
                Log.Error("writeTag :: Exception while writing mission data in the plc tag :: " + tagName + " :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                OnConnectPLC();
            }

            return false;

        }

        #endregion

        #region Connect and Disconnect PLC

        private void OnConnectPLC()
        {

            Log.Debug("OnConnectPLC :: inside OnConnectPLC");

            try
            {
                // Connection url
                if (!((ConnectedOpc.ServerState.ToString()).Equals("1")))
                {
                    ConnectedOpc.Connect(plcServerConnectionString, "");
                    Log.Debug("OnConnectPLC :: PLC connection successful and OPC server state is :: " + ConnectedOpc.ServerState.ToString());
                }
                else
                {
                    Log.Debug("OnConnectPLC :: Already connected with the plc.");
                }

            }
            catch (Exception ex)
            {
                Log.Error("OnConnectPLC :: Exception while connecting to plc :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
            }
        }

        private void OnDisconnectPLC()
        {
            Log.Debug("inside OnDisconnectPLC");

            try
            {
                ConnectedOpc.Disconnect();
                Log.Debug("OnDisconnectPLC :: Connection with the plc is disconnected.");
            }
            catch (Exception ex)
            {
                Log.Error("OnDisconnectPLC :: Exception while disconnecting to plc :: " + ex.Message);
            }

        }


        #endregion
    }
}


