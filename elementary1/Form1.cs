using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace elementary1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            start();
        }

        void start()
        {
            sendetermine _sendeTermine = new sendetermine();
            dataGridView1.DataSource = _sendeTermine.sendungen;
            dateinamen dateiNamen = new dateinamen();
            dataGridView2.DataSource = dateiNamen.dateien;
            foreach (sendung _sendung in _sendeTermine.sendungen)
            {
                string sNew=dateiNamen.newNameByDateTime(_sendung.sDatetime);
                if (sNew != "")
                {
                    textBox1.Text += _sendung.sStaffel + _sendung.sEpisode + "_" + sNew+"\r\n";
                    System.Diagnostics.Debug.WriteLine(sNew);
                }
            }
        }
    }
}
