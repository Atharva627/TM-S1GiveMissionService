using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AtsWmsS1MasterGiveMissionService
{
    public partial class AtsWmsS1MasterGiveMissionService : ServiceBase
    {
        static string className = "AtsWmsS1MasterGiveMissionService";
        private static readonly ILog Log = LogManager.GetLogger(className);
        public AtsWmsS1MasterGiveMissionService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Log.Debug("OnStart :: AtsWmsS1MasterGiveMissionService in OnStart....");

                try
                {
                    XmlConfigurator.Configure();
                    try
                    {
                        AtsWmsBatteryA1MasterGiveMissionServiceTaskThread();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("OnStart :: Exception occured while AtsWmsBatteryA1MasterGiveMissionServiceTaskThread  threads task :: " + ex.Message);
                    }
                    Log.Debug("OnStart :: AtsWmsBatteryA1MasterGiveMissionServiceTaskThread in OnStart ends..!!");

                    //XmlConfigurator.Configure();
                    //Thread staThread = new Thread(new ThreadStart(AtsWmsS1MasterGiveMissionServiceTaskThread));
                    //staThread.SetApartmentState(ApartmentState.STA);
                    //staThread.Start();
                    //Log.Debug("OnStart :: AtsWmsDispatchOrderServiceTaskThread in OnStart ends..!!");
                }
                catch (Exception ex)
                {
                    Log.Error("OnStart :: Exception occured in OnStart :: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Error("OnStart :: Exception occured in OnStart :: " + ex.Message);
            }
        }

        public async void AtsWmsBatteryA1MasterGiveMissionServiceTaskThread()
        {
            await Task.Run(() =>
            {
                try
                {
                    Log.Debug("1 AtsWmsS1MasterGiveMissionService");
                    AtsWmsS1MasterGiveMissionServiceDetails AtsWmsS1MasterGiveMissionServiceDetailsInstance = new AtsWmsS1MasterGiveMissionServiceDetails();
                    AtsWmsS1MasterGiveMissionServiceDetailsInstance.startOperation();
                }
                catch (Exception ex)
                {
                    Log.Error("TestService :: Exception in AtsWmsBatteryA1MasterGiveMissionServiceTaskThread :: " + ex.Message);
                }

            });
        }
        //public void AtsWmsS1MasterGiveMissionServiceTaskThread()
        //{
        //    try
        //    {
        //        AtsWmsS1MasterGiveMissionServiceDetails AtsWmsS1MasterGiveMissionServiceDetailsInstance = new AtsWmsS1MasterGiveMissionServiceDetails();
        //        AtsWmsS1MasterGiveMissionServiceDetailsInstance.startOperation();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("TestService :: Exception in AtsWmsS1MasterGiveMissionServiceTaskThread :: " + ex.Message);
        //    }
        //}

        protected override void OnStop()
        {
            try
            {
                Log.Debug("OnStop :: AtsWmsS1MasterGiveMissionService in OnStop ends..!!");
            }
            catch (Exception ex)
            {
                Log.Error("OnStop :: Exception occured in OnStop :: " + ex.Message);
            }
        }
    }
}
