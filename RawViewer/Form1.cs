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
        private string mParamFilename = "param.csv";

        private string[] mFilenames = null;

        byte[]   mData08bit = null;
        ushort[] mData16bit = null;

        Bitmap mBmp = null;

        private int mWidth  = 0;
        private int mHeight = 0;

        private int mIdxShow = 0;

        private int mNumBitShift = 0;

        #region Functions without member variables.

        //---------------------------------------------------------------------
        public int ByteToBmp08bpp(ref Bitmap dst, byte[] src, int width, int height)
        {

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
                else
                {
                    tempByte = (byte)tempUshort;
                }
            }

            return 0;
        }

        #endregion

        #region Functions with member variables.
        public int SetImageSize(int width, int height)
        {
            if ((width <= 0) || (height <= 0)) return -1;

            int NumOfPixcels = width * height;

            this.mData08bit = new   byte[NumOfPixcels];
            this.mData16bit = new ushort[NumOfPixcels];

            this.mWidth  = width;
            this.mHeight = height;

            this.pictureBox1.Width  = this.mWidth;
            this.pictureBox1.Height = this.mHeight;

            return 0;
        }
        #endregion

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
                // パラメータファイル読み込み.
                System.IO.StreamReader sr = new System.IO.StreamReader(this.mParamFilename);

                string strLine = "";
                string[] strSplit = null;

                strLine = sr.ReadLine();
                strSplit = strLine.Split(',');
                int tempWidth = int.Parse(strSplit[1]);

                strLine = sr.ReadLine();
                strSplit = strLine.Split(',');
                int tempHeight = int.Parse(strSplit[1]);

                sr.Close();

                this.SetImageSize(tempWidth, tempHeight);

                this.mFilenames = this.openFileDialog1.FileNames;

                ReadRawFromFile(this.mData16bit, this.mFilenames[this.mIdxShow], this.mWidth, this.mHeight);

                this.mIdxShow = 0;

                this.pictureBox1.Invalidate();
            }
        }

        //---------------------------------------------------------------------
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //
        }
    }
}
