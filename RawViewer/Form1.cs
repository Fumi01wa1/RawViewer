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

        int mIdxShow = 0;

        //---------------------------------------------------------------------
        public int UshortToByte(byte[] dst, ushort[] src, int width, int height, int bitshift)
        {
            int PixlNum = width * height;

            for (int n = 0; n < PixlNum; n++)
            {
                ushort tempUshort = (ushort)(src[n] >> bitshift);

                byte tempByte = 0;

                if (tempUshort > byte.MaxValue)
                {
                    tempByte = byte.MaxValue;
                }
                else {
                    tempByte = (byte)tempUshort; 
                }
            }

            return 0;
        }

        //---------------------------------------------------------------------
        public int ReadRawFromFile(ushort[] dst, string src_filename, int width, int height)
        {
            int NumOfPixls = width * height;

            byte[] tempByte = new byte[NumOfPixls*2];

            System.IO.FileStream fs = new System.IO.FileStream(src_filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            fs.Read(tempByte, 0, NumOfPixls*2);

            fs.Close();

            for (int n = 0; n < NumOfPixls; n++)
            {
                dst[n] = System.BitConverter.ToUInt16(tempByte, 2 * n);
            }

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

                this.mIdxShow = 0;

                ReadRawFromFile(this.mData, this.mFilenames[this.mIdxShow], this.mWidth, this.mHeight);
            }
        }
    }
}
