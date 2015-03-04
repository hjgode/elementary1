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
        bool bInit = true;
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

            string sPath = dateiNamen.readFromDir();

            dataGridView2.DataSource = dateiNamen.dateien;
            foreach (sendung _sendung in _sendeTermine.sendungen)
            {
                dateinamen.datei dNew=dateiNamen.newNameByDateTime(_sendung.sDatetime);
                if (dNew != null)
                {
                    dNew.sNameNew = _sendung.sStaffel + _sendung.sEpisode + "_" + _sendung._sDatetime + "_" + _sendung.sTitleNormalized + ".eit";
                    textBox1.Text += dNew.sNameOriginal + "->" + dNew.sNameNew + "\r\n";
                    System.Diagnostics.Debug.WriteLine(dNew.sNameOriginal);
                }
            }
            foreach (dateinamen.datei d in dateiNamen.dateien)
            {
                if (d.sNameNew == "")
                {
                    textBox1.Text += "not found: " + d.sNameOriginal + "\r\n";
                }
                else
                {
                    if (sPath != "")
                    {
                        dateinamen.renameFileSet(sPath + d.sNameOriginal, d.sNameNew);
                        textBox1.Text += "renaming: " + d.sNameOriginal + " to " + d.sNameNew + "\r\n";
                    }
                }
            }
            bInit = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;
            string value="";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                value = row.Cells[0].Value.ToString();                
            }

            dataGridView2.ClearSelection();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value.ToString() == value)
                {
                    dataGridView2.FirstDisplayedCell = row.Cells[0];
                    row.Selected = true;
                    break;
                }
            }
        }
    }
}
