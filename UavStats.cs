using System;
using System.Xml;
using System.IO;
using MissionPlanner;
using System.Text;
using System.Collections.Generic;
using MissionPlanner.ArduPilot;

namespace MissionPlanner
{
    public class UAVStats
    {

        //string literal name of model
        public string uav;
        //firmware used px4/ardupilot varient and type E.G ArduPlane
        public string firmware;
        //display in list for selection
        public int addToList;
        //systemID of mav connected.
        public int sysid;

        //avionics batteries
        public List<double> avionicsBatteryLow = new List<double>();
        public List<double> avionicsBatteryMed = new List<double>();
        public List<double> avionicsBatteryHigh = new List<double>();
        public List<double> avionicsAH = new List<double>();
        //flight batteries
        public List<double> flightBatteryLow = new List<double>();
        public List<double> flightBatteryMed = new List<double>();
        public List<double> flightBatteryHigh = new List<double>();
        public List<double> flightAH = new List<double>();

        //Gas monitoring
        public int hasGas;
        public double lowGas;
        public double medGas;
        public double highGas;
        public double maxCHT; //max cylinder head temp 
        public double minCHT; //min cylinder head temp for takeoff



        //FW takeoff chars
        public int isFW;
        public double takeoffpitch;
        public double maxgradient;
        public double crtUpFW;
        public double crtdwnFW;
        public double flightSpeedMFW;
        public double climbWattsFW;
        public double cruiseWattsFW;
        public double missionAltFW;
        public double resumeAltFW;
        public double LandAltFW;

        //VTOL
        public int isVTOL;
        public double transitionDist;
        public double deTransitionDist;
        public double vtolTKoffAlt;
        public double vtolLandAlt;
        public double crtUpVTOL;
        public double crtdwnVTOL;
        public double flightSpeedMVTOL;
        public double climbWattsVTOL;
        public double missionAltVTOL;
        public double resumeAltVTOL;

        //model has predefined approach path
        public int runwayautoprompt; //auto prompt setting up a landing after survey grid screen
        public int landwp; //final waypoint in the landing sequence
        public double landLoitAlt; //altitude for loiter point in approach path
        public double landMedPtAlt;
        //button access
        public int gpslanding; //set landing at takeoff point
        public int runway; // has access to landing strip
        public int resumeMission;  //can pause and resume mission

        //warnings but not limits
        public double maxMissionDist;
        public double maxFlightTime;

        //add ons
        public int hasTerrainFollow;
        public int hasChute;
        public int hasGenerator;

        public string setStats(float sysid)
        {
            int i = 0;
            foreach (var uav in MainV2.instance.UAVs.Values)
            {
                i++;
                if (uav.sysid == sysid)
                {

                    MainV2.CurrentUAV = uav;
                    if (MainV2.CurrentUAV.firmware == "ArduPlane")
                    {
                        MainV2.comPort.MAV.cs.firmware = Firmwares.ArduPlane;

                    }
                    if (MainV2.CurrentUAV.firmware == "ArduCopter")
                    {
                        MainV2.comPort.MAV.cs.firmware = Firmwares.ArduCopter2;

                    }
                    if (MainV2.CurrentUAV.firmware == "ArduRover")
                    {
                        MainV2.comPort.MAV.cs.firmware = Firmwares.ArduRover;

                    }
                    if (MainV2.CurrentUAV.firmware == "ArduTracker")
                    {
                        MainV2.comPort.MAV.cs.firmware = Firmwares.ArduTracker;
                    }

                    return MainV2.CurrentUAV.uav.ToString();
                }

            }
            return null;
            //return stats;
        }
        public void setStatsbyName(string name)
        {
            foreach (var uav in MainV2.instance.UAVs.Values)
            {

                if (uav.uav == name)
                {

                    MainV2.CurrentUAV = uav;

                    if (MainV2.CurrentUAV.firmware == "ArduPlane")
                    {
                        MainV2.comPort.MAV.cs.firmware = Firmwares.ArduPlane;
                    }
                    if (MainV2.CurrentUAV.firmware == "ArduCopter2")
                    {
                        MainV2.comPort.MAV.cs.firmware = Firmwares.ArduCopter2;
                    }
                    if (MainV2.CurrentUAV.firmware == "ArduRover")
                    {
                        MainV2.comPort.MAV.cs.firmware = Firmwares.ArduRover;
                    }
                    if (MainV2.CurrentUAV.firmware == "ArduTracker")
                    {
                        MainV2.comPort.MAV.cs.firmware = Firmwares.ArduTracker;
                    }

                }

            }
            

        }


        public void load_uavs(string filename)
        {

            try
            {
                using (XmlTextReader xmlreader = new XmlTextReader(filename))
                {
                    while (xmlreader.Read())
                    {
                        xmlreader.MoveToElement();
                        try
                        {
                            switch (xmlreader.Name)
                            {
                                case "Model":
                                    {
                                        UAVStats UAV = new UAVStats();

                                        while (xmlreader.Read())
                                        {
                                            bool dobreak = false;
                                            xmlreader.MoveToElement();
                                            switch (xmlreader.Name)
                                            {
                                                case "uav":
                                                    UAV.uav = xmlreader.ReadString();
                                                    break;
                                                case "firmware":
                                                    UAV.firmware = xmlreader.ReadString();
                                                    break;
                                                case "addToList":
                                                    UAV.addToList = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "sysid":
                                                    UAV.sysid = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "avionicsBatteryHigh":
                                                    UAV.avionicsBatteryHigh.Add(double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US")));
                                                    break;
                                                case "avionicsBatteryMed":
                                                    UAV.avionicsBatteryMed.Add(double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US")));
                                                    break;
                                                case "avionicsBatteryLow":
                                                    UAV.avionicsBatteryLow.Add(double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US")));
                                                    break;
                                                case "avionicsAH":
                                                    UAV.avionicsAH.Add(double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US")));
                                                    break;
                                                case "flightBatteryHigh":
                                                    UAV.flightBatteryHigh.Add(double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US")));
                                                    break;
                                                case "flightBatteryMed":
                                                    UAV.flightBatteryMed.Add(double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US")));
                                                    break;
                                                case "flightBatteryLow":
                                                    UAV.flightBatteryLow.Add(double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US")));
                                                    break;
                                                case "flightAH":
                                                    UAV.flightAH.Add(double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US")));
                                                    break;
                                                case "hasGas":
                                                    UAV.hasGas = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "lowGas":
                                                    UAV.lowGas = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "medGas":
                                                    UAV.medGas = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "highGas":
                                                    UAV.highGas = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "maxCHT":
                                                    UAV.maxCHT = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "minCHT":
                                                    UAV.minCHT = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "isFW":
                                                    UAV.isFW = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "takeoffpitch":
                                                    UAV.takeoffpitch = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "maxgradient":
                                                    UAV.maxgradient = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "crtUpFW":
                                                    UAV.crtUpFW = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "crtdwnFW":
                                                    UAV.crtdwnFW = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "flightSpeedMFW":
                                                    UAV.flightSpeedMFW = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "climbWattsFW":
                                                    UAV.climbWattsFW = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "cruiseWattsFW":
                                                    UAV.cruiseWattsFW = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "missionAltFW":
                                                    UAV.missionAltFW = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "resumeAltFW":
                                                    UAV.resumeAltFW = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "isVTOL":
                                                    UAV.isVTOL = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "transitionDist":
                                                    UAV.transitionDist = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "deTransitionDist":
                                                    UAV.deTransitionDist = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "vtolTKoffAlt":
                                                    UAV.vtolTKoffAlt = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "vtolLandAlt":
                                                    UAV.vtolLandAlt = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "crtUpVTOL":
                                                    UAV.crtUpVTOL = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "crtdwnVTOL":
                                                    UAV.crtdwnVTOL = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "flightSpeedMVTOL":
                                                    UAV.flightSpeedMVTOL = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "climbWattsVTOL":
                                                    UAV.climbWattsVTOL = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "missionAltVTOL":
                                                    UAV.missionAltVTOL = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "resumeAltVTOL":
                                                    UAV.resumeAltVTOL = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "runwayautoprompt":
                                                    UAV.runwayautoprompt = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "landwp":
                                                    UAV.landwp = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "landLoitAlt":
                                                    UAV.landLoitAlt = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "landMedPtAlt":
                                                    UAV.landMedPtAlt = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "gpslanding":
                                                    UAV.gpslanding = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "runway":
                                                    UAV.runway = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "resumeMission":
                                                    UAV.resumeMission = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "maxMissionDist":
                                                    UAV.maxMissionDist = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "maxFlightTime":
                                                    UAV.maxFlightTime = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "hasTerrainFollow":
                                                    UAV.hasTerrainFollow = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "hasChute":
                                                    UAV.hasChute = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "hasGenerator":
                                                    UAV.hasGenerator = int.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                    break;
                                                case "Model":
                                                    MainV2.instance.UAVs[UAV.uav] = UAV;
                                                    dobreak = true;
                                                    break;
                                            }
                                            if (dobreak)
                                                break;
                                        }
                                        string temp = xmlreader.ReadString();
                                    }
                                    break;
                                case "Config":
                                    break;
                                case "xml":
                                    break;
                                default:
                                    if (xmlreader.Name == "") // line feeds
                                        break;
                                    //config[xmlreader.Name] = xmlreader.ReadString();
                                    break;
                            }
                        }
                        catch (Exception ee) { Console.WriteLine(ee.Message); } // silent fail on bad entry
                    }
                }
            }

            catch (Exception ex) { Console.WriteLine(ex.ToString()); } // bad config file


            foreach (var uav in MainV2.instance.UAVs.Values)
            {

                if (uav.uav != null)
                {

                    if (!MainV2._connectionControl.cmb_uav.Items.Contains(uav.uav) && uav.addToList == 1)
                        MainV2._connectionControl.cmb_uav.Items.Add(uav.uav);
                }

            }
        }
    }

}