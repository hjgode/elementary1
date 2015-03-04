using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace elementary1
{
    class dateinamen
    {
        public class datei
        {
            public string sDate { get; set; }
            public string sTime { get; set; }
            public string sNameOriginal { get; set; }
            string _sNameNew = "";
            public string sNameNew { get { return _sNameNew; } set { _sNameNew = value; } }
            public string sDatetime { get; set; }
            
            public string sDatetimePlusFive { get; set; } //some recordings start 5 minutes befor event
            // not found: S00E00_20150130_2009_-_kabel_eins_HD_-_Elementary.eit
            public string sDatetimePlusSix { get; set; } //some recordings start 6 minutes befor event

            public string sDatetimePlusThree { get; set; } //some recordings start 3 minutes befor event

            public datei(string sIn)
            {
                sNameOriginal = sIn;
                int offset=0;
                //0    0    1    1    2
                //0    5    0    5    0
                //S01E09_20150128_0220_-_AXN_Action_-_Elementary.eit
                if(sIn.StartsWith("S0"))
                    offset=7;
                sDate = sIn.Substring(offset, 8);
                sTime = sIn.Substring(offset + 9, 4);
                sDatetime=sDate+"_"+sTime;
                //PlusFive calculation
                DateTime dt = new DateTime(int.Parse(sDatetime.Substring(0, 4)), int.Parse(sDatetime.Substring(4, 2)), int.Parse(sDatetime.Substring(6, 2)),
                    int.Parse(sTime.Substring(0,2)), int.Parse(sTime.Substring(2,2)),0);
                DateTime dtPlusFive = dt + new TimeSpan(0, 5, 0);
                string sTimePlusFive = dtPlusFive.Hour.ToString("00") + dtPlusFive.Minute.ToString("00");
                sDatetimePlusFive = dtPlusFive.Year.ToString("0000") + dtPlusFive.Month.ToString("00") + dtPlusFive.Day.ToString("00") + "_" + sTimePlusFive;
                //PlusSix
                DateTime dtPlusSix = dt + new TimeSpan(0, 6, 0);
                string sTimePlusSix = dtPlusSix.Hour.ToString("00") + dtPlusSix.Minute.ToString("00");
                sDatetimePlusSix = dtPlusSix.Year.ToString("0000") + dtPlusSix.Month.ToString("00") + dtPlusSix.Day.ToString("00") + "_" + sTimePlusSix;
                //PlusThree
                DateTime dtPlusThree = dt + new TimeSpan(0, 3, 0);
                string sTimePlusThree = dtPlusThree.Hour.ToString("00") + dtPlusThree.Minute.ToString("00");
                sDatetimePlusThree = dtPlusThree.Year.ToString("0000") + dtPlusThree.Month.ToString("00") + dtPlusThree.Day.ToString("00") + "_" + sTimePlusThree;
            }
        }
        public List<datei> dateien = new List<datei>();
        public dateinamen()
        {
            foreach (string s in dateinamenliste)
                //if (s.StartsWith("S00") || !s.StartsWith("S0")) //process only unknown events
                    dateien.Add(new datei(s));
            dateien.Sort(delegate(datei x, datei y)
            {
                return x.sDatetime.CompareTo(y.sDatetime);
            });

        }
        public string readFromDir(){
            string sPath="";
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                dateien.Clear();
                DirectoryInfo di = new DirectoryInfo(fbd.SelectedPath);
                sPath = fbd.SelectedPath;
                if (!sPath.EndsWith("\\"))
                    sPath += "\\";
                FileInfo[] files = di.GetFiles("*.eit");
                foreach (FileInfo fi in files)
                {
                    //fixnames.fixRecordingFilename(sPath + fi.Name);
                    dateien.Add(new datei(fi.Name));
                }
            }
            else
                MessageBox.Show("Getting files canceled");
            return sPath;
        }

        public static void renameFileSet(string sFullNameOrg, string sNameNew)
        {
            string sPath=Path.GetDirectoryName(sFullNameOrg);
            string sBaseNameOrg = Path.GetFileNameWithoutExtension(sFullNameOrg);
            string sBaseNameNew = Path.GetFileNameWithoutExtension(sNameNew);

            if(!sPath.EndsWith("\\"))
                sPath+="\\";
            
            DirectoryInfo di = new DirectoryInfo(sPath);
            FileInfo[] filesOrg = di.GetFiles(sBaseNameOrg + ".*");
            foreach (FileInfo fiOld in filesOrg)
            {
                string sExtCurrent = getMyExtension(fiOld.Name);// Path.GetExtension(fiOld.Name);
                string oldName = fiOld.Name;
                string newName = sBaseNameNew + sExtCurrent;
#if !DEBUG
                System.Diagnostics.Debug.WriteLine("File.Move("+sPath + oldName+", "+sPath + newName+")\r\n");
#else
                File.Move(sPath + oldName, sPath + newName);
#endif
            }
        }

        static string getMyExtension(string s)
        {
            int pos = s.IndexOf(".");
            string sBase = s.Substring(pos);
            return sBase;
        }

        /// <summary>
        /// return file name for event at date/time
        /// </summary>
        /// <param name="sSearch">20150201_0230
        /// yyyyMMdd_HHmm</param>
        /// <returns></returns>
        public datei newNameByDateTime(string sSearch)
        {
            datei _datei = null;
            string date=sSearch.Substring(0,8);
            string time=sSearch.Substring(9,4);
            foreach (datei d in dateien)
            {
                if (d.sDatetime == sSearch || d.sDatetimePlusFive == sSearch || d.sDatetimePlusSix == sSearch || d.sDatetimePlusThree == sSearch)
                    return d;
            }
            return _datei;
        }

        #region Dateinamen
        string[] dateinamenliste = new string[]{
            "S01E09_20150128_0220_-_AXN_Action_-_Elementary.eit",
            "S01E10_20150128_0305_-_AXN_Action_-_Elementary.eit",
            "S01E10_20150128_0305_-_AXN_Action_-_Elementary_fixed.eit",
            "S01E11_20150128_2010_-_AXN_Action_-_Elementary.eit",
            "S01E12_20150128_2055_-_AXN_Action_-_Elementary.eit",
            "S01E11_20150130_0045_-_AXN_Action_-_Elementary.eit",
            "S01E12_20150130_0130_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150130_2009_-_kabel_eins_HD_-_Elementary.eit",
            "S00E00_20150130_2112_-_kabel_eins_HD_-_Elementary.eit",
            "S01E12_20150201_0005_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150201_0620_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150201_0711_-_kabel_eins_-_Elementary.eit",
            "S01E11_20150204_0215_-_AXN_Action_-_Elementary.eit",
            "S01E12_20150204_0300_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150204_2010_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150204_2055_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150206_0045_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150206_0130_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150206_2009_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150206_2112_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150208_0000_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150208_0045_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150208_0653_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150210_0055_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150210_0140_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150211_2010_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150211_2055_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150213_0215_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150213_0300_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150213_2009_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150213_2109_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150215_0035_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150215_0120_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150215_0615_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150215_0712_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150217_0040_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150217_0125_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150218_2010_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150218_2055_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150220_0210_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150220_0255_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150220_2009_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150220_2112_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150221_2340_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150222_0025_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150222_0621_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150222_0718_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150224_0035_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150224_0115_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150225_2010_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150225_2055_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150227_0215_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150227_0300_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150227_2009_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150227_2112_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150130_2009_-_kabel_eins_HD_-_Elementary.eit",
            "S00E00_20150130_2112_-_kabel_eins_HD_-_Elementary.eit",
            "S00E00_20150201_0620_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150201_0711_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150204_2010_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150204_2055_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150206_0045_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150206_0130_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150206_2009_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150206_2112_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150208_0000_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150208_0045_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150208_0653_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150210_0055_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150210_0140_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150211_2010_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150211_2055_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150213_0215_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150213_0300_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150213_2009_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150213_2109_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150215_0035_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150215_0120_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150215_0615_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150215_0712_-_kabel_eins_-_Elementary.eit",
            "S00E00_20150217_0040_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150217_0125_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150218_2010_-_AXN_Action_-_Elementary.eit",
            "S00E00_20150218_2055_-_AXN_Action_-_Elementary.eit",
            "S01E09_20150128_0220_-_AXN_Action_-_Elementary.eit",
            "S01E10_20150128_0305_-_AXN_Action_-_Elementary.eit",
            "S01E10_20150128_0305_-_AXN_Action_-_Elementary_fixed.eit",
            "S01E11_20150128_2010_-_AXN_Action_-_Elementary.eit",
            "S01E11_20150130_0045_-_AXN_Action_-_Elementary.eit",
            "S01E11_20150204_0215_-_AXN_Action_-_Elementary.eit",
            "S01E12_20150128_2055_-_AXN_Action_-_Elementary.eit",
            "S01E12_20150130_0130_-_AXN_Action_-_Elementary.eit",
            "S01E12_20150201_0005_-_AXN_Action_-_Elementary.eit",
            "S01E12_20150204_0300_-_AXN_Action_-_Elementary.eit",
        };
        #endregion
    }
}
