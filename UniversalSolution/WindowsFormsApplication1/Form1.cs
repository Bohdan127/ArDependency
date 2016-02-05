using DataColector;
using DataParser;
using DataSaver;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private IDataColector col;
        private IDataParser par;
        private IDataSaver sav;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(IDataColector _col, IDataParser _par, IDataSaver _sav)
            : this()
        {
            col = _col;
            par = _par;
            sav = _sav;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = col.Test;
            label2.Text = par.Test;
            label3.Text = sav.Test;
        }
    }
}
