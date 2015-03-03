using System;
using System.Collections.Generic;
using System.Text;

namespace elementary1
{
    class sendung
    {
        string _sDate = "";
        public string sDate
        {
            get { return _sDate; }
        }
        string _sTime = "";
        public string sTime
        {
            get { return _sTime; }
        }
        public string sDatetime;
        string _sStaffel = "";
        public string sStaffel
        {
            get { return _sStaffel; }
        }
        string _sEpisode = "";
        public string sEpisode { get { return _sEpisode; } }
        string _sTitel = "";
        public string sTitel { get { return _sTitel; } }

        public sendung(string sIn)
        {
            //0    0    1    1    2
            //0    5    0    5    0
            //S01E16_20150103_0725_Die Falle
            _sDate = sIn.Substring(7, 8);
            _sTime = sIn.Substring(16, 4);
            _sStaffel = sIn.Substring(0, 3);
            _sEpisode = sIn.Substring(3, 3);
            _sTitel = sIn.Substring(21);
            sDatetime = _sDate + "_" + sTime;
        }
    }
    class sendetermine
    {
        public List<sendung> sendungen = new List<sendung>();
        public sendetermine()
        {
            foreach(string s in termine)
                sendungen.Add(new sendung(s));
        }
        #region SendeTermine
        string[] termine = new string[]{
            //kabel eins
            "S01E16_20150103_0725_Die Falle",
            "S01E16_20150227_2115_Die Falle",
            "S03E08_20150227_2015_Schichtende",
            "S03E07_20150222_0725_Die Muskatnuss-Verbindung",
            "S01E15_20150222_0625_Die Prüfung",
            "S01E15_20150220_2115_Die Prüfung",
            "S03E07_20150220_2015_Die Muskatnuss-Verbindung",
            "S03E06_20150215_0720_Terra Pericolosa",
            "S01E14_20150215_0620_Der Schlussfolgerer",
            "S01E14_20150213_2115_Der Schlussfolgerer",
            "S03E06_20150213_2015_Terra Pericolosa",
            "S01E13_20150208_0650_Für das Allgemeinwohl",
            "S01E13_20150206_2115_Für das Allgemeinwohl",
            "S03E05_20150206_2015_Watsons Buch",
            "S03E04_20150201_0715_Bella",
            "S01E12_20150201_0625_M.",
            "S01E12_20150130_2115_M.",
            "S03E04_20150130_2015_Bella",
            "S03E03_20150125_0710_Wer gewinnt, stirbt",
            "S01E11_20150125_0615_Eine ganz normale Familie",
            "S01E11_20150123_2115_Eine ganz normale Familie",
            "S03E03_20150123_2015_Wer gewinnt, stirbt",
            "S03E02_20150118_0715_Die fünf orangenen Perlen",
            "S01E10_20150118_0620_Der Leviathan",
            "S01E10_20150116_2115_Der Leviathan",
            "S03E02_20150116_2015_Die fünf orangenen Perlen",
            "S03E01_20150111_0720_Das perfekte Verbrechen",
            "S01E09_20150111_0620_Chinesische Medizin",
            "S01E09_20150109_2115_Chinesische Medizin",
            "S03E01_20150109_2015_Das perfekte Verbrechen",
            "S01E08_20150104_0700_Rätselhafte Bombe",
            "S01E07_20150104_0605_Mittel und Wege",
            "S01E08_20150102_2115_Rätselhafte Bombe",
            "S01E07_20150102_2015_Mittel und Wege",
            "S01E06_20141221_1115_Spuren im Sand",
            "S02E24_20141221_1015_Das große Experiment",
            "S01E06_20141219_2115_Spuren im Sand",
            "S02E24_20141219_2015_Das große Experiment",
            "S02E23_20141214_1115_Spione",
            "S01E05_20141214_1020_Todesengel",
            "S01E05_20141212_2115_Todesengel",
            "S02E23_20141212_2015_Spione",
            "S02E22_20141207_1120_Die Liste",
            "S01E04_20141207_1020_Konkurrenzkampf",
            "S01E04_20141205_2115_Konkurrenzkampf",
            "S02E22_20141205_2015_Die Liste",
            "S02E21_20141130_1110_Gefahr aus der Luft",
            "S01E03_20141130_1015_Der Ballonmann",
            "S01E03_20141128_2115_Der Ballonmann",
            "S02E21_20141128_2015_Gefahr aus der Luft",
            "S02E20_20141123_1115_Tödliches Pulver",
            "S01E02_20141123_1015_Während du schliefst",
            "S01E02_20141121_2115_Während du schliefst",
            "S02E20_20141121_2015_Tödliches Pulver",
            "S02E19_20141116_1155_Bissspuren",
            "S01E01_20141116_1055_Ein aussichtsloser Fall",
            "S01E01_20141114_2115_Ein aussichtsloser Fall",
            "S02E19_20141114_2015_Bissspuren",
            "S02E18_20141109_1110_Der Hund",
            "S02E17_20141109_1010_Zwei Ohren zu viel",
            "S02E18_20141107_2115_Der Hund",
            "S02E17_20141107_2015_Zwei Ohren zu viel",
            "S02E15_20141102_1115_Im Rampenlicht",
            "S02E16_20141102_1020_Kampfhähne",
            "S02E16_20141031_2120_Kampfhähne",
            "S02E15_20141031_2015_Im Rampenlicht",
            "S02E14_20141026_0845_Der Nanotyrannus",
            "S02E13_20141026_0750_Kopflos",
            "S02E14_20141024_2115_Der Nanotyrannus",
            "S02E13_20141024_2015_Kopflos",
            "S02E12_20141019_0900_Geliebte Feindin",
            "S02E11_20141019_0805_Im Namen der Opfer",
            "S02E12_20141017_2115_Geliebte Feindin",
            "S02E11_20141017_2015_Im Namen der Opfer",
            "S02E10_20141012_0900_Pulverfass",
            "S02E09_20141012_0800_Alles Lüge?",
            "S02E10_20141010_2120_Pulverfass",
            "S02E09_20141010_2015_Alles Lüge?",
            "S02E08_20140928_0855_Blut ist dicker",
            "S02E07_20140928_0800_Unberechenbar",
            "S02E08_20140926_2115_Blut ist dicker",
            "S02E07_20140926_2015_Unberechenbar",
            "S02E06_20140921_0825_Ein unnatürliches Arrangement",
            "S02E05_20140921_0730_Schwarzes Herz",
            "S02E06_20140919_2115_Ein unnatürliches Arrangement",
            "S02E05_20140919_2015_Schwarzes Herz",
            "S02E03_20140914_0900_Wir sind Everyone",
            "S02E04_20140912_2115_Späte Bestrafung",
            "S02E03_20140912_2015_Wir sind Everyone",
            "S02E02_20140907_0855_Nach X auflösen",
            "S02E01_20140907_0755_London",
            "S02E02_20140905_2115_Nach X auflösen",
            "S02E01_20140905_2015_London",
            "S01E23_20140831_0650_Irene",
            "S01E24_20140830_0255_Moriarty",
            "S01E24_20140829_2320_Moriarty",
            "S01E23_20140829_2215_Irene",
            "S01E22_20140824_0745_Fragen und Antworten",
            "S01E21_20140823_0255_Ein Schritt näher",
            "S01E22_20140822_2315_Fragen und Antworten",
            "S01E21_20140822_2215_Ein Schritt näher",
            "S01E19_20140817_0810_Stadt im Dunkeln",
            "S01E20_20140816_0255_Haus in Flammen",
            "S01E20_20140815_2315_Haus in Flammen",
            "S01E19_20140815_2215_Stadt im Dunkeln",
            "S01E18_20140810_0640_Die Frau mit den Blumen",
            "S01E18_20140808_2220_Die Frau mit den Blumen",
            "S01E17_20140803_0800_Möglichkeit Zwei",
            "S01E17_20140801_2215_Möglichkeit Zwei",
            "S01E16_20140727_0755_Die Falle",
            "S01E16_20140725_2220_Die Falle",
            "S01E15_20140720_0800_Die Prüfung",
            "S01E15_20140718_2215_Die Prüfung",
            "S01E14_20140713_0745_Der Schlussfolgerer",
            "S01E14_20140711_2215_Der Schlussfolgerer",
            "S01E13_20140607_0205_Für das Allgemeinwohl",
            "S01E13_20140606_2215_Für das Allgemeinwohl",
            "S01E12_20140531_0200_M.",
            "S01E12_20140530_2215_M.",
            "S01E11_20140524_0200_Eine ganz normale Familie",
            //AXN
            "S01E11_20141025_2325_Eine ganz normale Familie",
            "S01E12_20141026_0010_M.",
            "S01E11_20141028_2350_Eine ganz normale Familie",
            "S01E12_20141029_0035_M.",
            "S01E13_20141029_2015_Für das Allgemeinwohl",
            "S01E14_20141029_2100_Der Schlussfolgerer",
            "S01E13_20141031_0115_Für das Allgemeinwohl",
            "S01E14_20141031_0200_Der Schlussfolgerer",
            "S01E13_20141102_0245_Für das Allgemeinwohl",
            "S01E14_20141102_0330_Der Schlussfolgerer",
            "S01E13_20141104_2350_Für das Allgemeinwohl",
            "S01E14_20141105_0035_Der Schlussfolgerer",
            "S01E15_20141105_2015_Die Prüfung",
            "S01E16_20141105_2100_Die Falle",
            "S01E15_20141107_0055_Die Prüfung",
            "S01E16_20141107_0140_Die Falle",
            "S01E15_20141108_2355_Die Prüfung",
            "S01E16_20141109_0040_Die Falle",
            "S01E15_20141111_2350_Die Prüfung",
            "S01E16_20141112_0035_Die Falle",
            "S01E17_20141112_2015_Möglichkeit Zwei",
            "S01E18_20141112_2100_Die Frau mit den Blumen",
            "S01E17_20141114_0050_Möglichkeit Zwei",
            "S01E18_20141114_0135_Die Frau mit den Blumen",
            "S01E17_20141115_2355_Möglichkeit Zwei",
            "S01E18_20141116_0040_Die Frau mit den Blumen",
            "S01E17_20141118_2350_Möglichkeit Zwei",
            "S01E18_20141119_0035_Die Frau mit den Blumen",
            "S01E19_20141119_2015_Stadt im Dunkeln",
            "S01E20_20141119_2100_Haus in Flammen",
            "S01E19_20141121_0100_Stadt im Dunkeln",
            "S01E20_20141121_0145_Haus in Flammen",
            "S01E19_20141123_0020_Stadt im Dunkeln",
            "S01E20_20141123_0100_Haus in Flammen",
            "S01E19_20141125_2350_Stadt im Dunkeln",
            "S01E20_20141126_0035_Haus in Flammen",
            "S01E21_20141126_2015_Ein Schritt näher",
            "S01E22_20141126_2100_Fragen und Antworten",
            "S01E21_20141128_0045_Ein Schritt näher",
            "S01E22_20141128_0130_Fragen und Antworten",
            "S01E21_20141130_0055_Ein Schritt näher",
            "S01E22_20141130_0140_Fragen und Antworten",
            "S01E21_20141202_2350_Ein Schritt näher",
            "S01E22_20141203_0035_Fragen und Antworten",
            "S01E23_20141203_2015_Irene",
            "S01E24_20141203_2100_Moriarty",
            "S01E23_20141205_0110_Irene",
            "S01E24_20141205_0155_Moriarty",
            "S01E23_20141206_2350_Irene",
            "S01E24_20141207_0035_Moriarty",
            "S01E23_20141209_2350_Irene",
            "S01E24_20141210_0035_Moriarty",
            "S01E01_20141210_2015_Ein aussichtsloser Fall",
            "S01E02_20141210_2100_Während du schliefst",
            "S01E01_20141212_0045_Ein aussichtsloser Fall",
            "S1E02_20141212_0135_Während du schliefst",
            "S01E01_20141214_0020_Ein aussichtsloser Fall",
            "S01E02_20141214_0105_Während du schliefst",
            "S01E01_20141216_2350_Ein aussichtsloser Fall",
            "S01E02_20141217_0035_Während du schliefst",
            "S01E03_20141217_2015_Der Ballonmann",
            "S01E04_20141217_2100_Konkurrenzkampf",
            "S01E03_20141219_0005_Der Ballonmann",
            "S01E04_20141219_0045_Konkurrenzkampf",
            "S01E03_20141220_2355_Der Ballonmann",
            "S01E04_20141221_0040_Konkurrenzkampf",
            "S01E03_20141223_2350_Der Ballonmann",
            "S01E04_20141224_0035_Konkurrenzkampf",
            "S01E05_20150107_2015_Todesengel",
            "S01E06_20150107_2100_Spuren im Sand",
            "S01E05_20150109_0045_Todesengel",
            "S01E06_20150109_0130_Spuren im Sand",
            "S01E05_20150111_0025_Todesengel",
            "S01E06_20150111_0110_Spuren im Sand",
            "S01E05_20150114_0220_Todesengel",
            "S01E05_20150114_0305_Todesengel",
            "S01E07_20150114_2015_Mittel und Wege",
            "S01E08_20150114_2100_Rätselhafte Bombe",
            "S01E07_20150115_2305_Mittel und Wege",
            "S01E08_20150115_2350_Rätselhafte Bombe",
            "S01E07_20150118_0035_Mittel und Wege",
            "S01E08_20150118_0120_Rätselhafte Bombe",
            "S01E07_20150121_0220_Mittel und Wege",
            "S01E08_20150121_0305_Rätselhafte Bombe",
            "S01E09_20150121_2015_Chinesische Medizin",
            "S01E10_20150121_2100_Der Leviathan",
            "S1E09_20150123_0050_Chinesische Medizin",
            "S01E10_20150123_0135_Der Leviathan",
            "S01E09_20150124_2335_Chinesische Medizin",
            "S01E10_20150125_0020_Der Leviathan",
            "S01E09_20150128_0220_Chinesische Medizin",
            "S01E10_20150128_0305_Der Leviathan",
            "S01E11_20150128_2015_Eine ganz normale Familie",
            "S01E12_20150128_2100_M.",
            "S01E11_20150130_0050_Eine ganz normale Familie",
            "S01E12_20150130_0135_M.",
            "S01E11_20150131_2330_Eine ganz normale Familie",
            "S01E12_20150201_0010_M.",
            "S01E11_20150204_0220_Eine ganz normale Familie",
            "S01E12_20150204_0305_M.",
            "S01E13_20150204_2015_Für das Allgemeinwohl",
            "S01E14_20150204_2100_Der Schlussfolgerer",
            "S01E13_20150206_0050_Für das Allgemeinwohl",
            "S01E14_20150206_0135_Der Schlussfolgerer",
            "S01E13_20150208_0005_Für das Allgemeinwohl",
            "S01E14_20150208_0050_Der Schlussfolgerer",
            "S01E13_20150210_0100_Für das Allgemeinwohl",
            "S01E14_20150210_0145_Der Schlussfolgerer",
            "S01E15_20150211_2015_Die Prüfung",
            "S01E16_20150211_2100_Die Falle",
            "S01E15_20150213_0220_Die Prüfung",
            "S01E16_20150213_0305_Die Falle",
            "S01E15_20150215_0040_Die Prüfung",
            "S01E16_20150215_0125_Die Falle",
            "S01E15_20150217_0045_Die Prüfung",
            "S01E15_20150217_0130_Die Prüfung",
            "S01E17_20150218_2015_Möglichkeit Zwei",
            "S01E18_20150218_2100_Die Frau mit den Blumen",
            "S01E17_20150220_0215_Möglichkeit Zwei",
            "S01E18_20150220_0300_Die Frau mit den Blumen",

        };
        #endregion
    }
}
