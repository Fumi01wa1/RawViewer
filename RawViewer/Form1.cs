using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RawViewer
{
    public partial class Form1 : Form
    {
        string[] mFilenames = null;
        ushort[] mData = null;
        string mParamFilename = "param.csv";

        int mWidth  = 0;
        int mHeight = 0;

        //---------------------------------------------------------------------
        public int UshortToByte(byte[] dst, ushort[] src, int width, int height, int bitshift)
        {

            return 0;
        }

        //---------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();

            this.openFileDialog1.Filter = "RAW Files(*.raw)|*.raw|ALL Files(*.*)|*.*";

            this.Text = Application.ProductName;
        }

        //---------------------------------------------------------------------
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //---------------------------------------------------------------------
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(this.mParamFilename);

                string strLine = "";
                string[] strSplit = null;

                strLine = sr.ReadLine();
                strSplit = strLine.Split(',');
                this.mWidth = int.Parse(strSplit[1]);

                strLine = sr.ReadLine();
                strSplit = strLine.Split(',');
                this.mHeight = int.Parse(strSplit[1]);

                sr.Close();

                int pixlNum = this.mWidth * this.mHeight;

                this.mData = new ushort[pixlNum];

                this.mFilenames = this.openFileDialog1.FileNames;
            }
        }
    }
}
