using DataColector.Interfaces;
using DataParser.Interfaces;
using DataSaver.Interfaces;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using System;
using System.Threading;

namespace DXApplication1
{
    public partial class Form1 : XtraForm
    {
        private IDataColector _collector;
        private IDataParser _parser;
        private IDataSaver _saver;

        private string defUrl = @"https://ua.1xbet.com/LiveFeed/Get1x2?sportId=0&sports=&champId=0&tf=1000000&count=50&cnt=10&lng=ru&cfview=0";

        public Form1(IDataColector colector, IDataParser parser, IDataSaver saver)
        {
            _collector = colector;
            _parser = parser;
            _saver = saver;
            InitializeComponent();
            InitSkinGallery();

            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            this.Shown += Form1_Shown;
        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Thread.Sleep(10);
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            gridControl1.DataSource = _parser.ParseData(
                _collector.GetJsonArray(defUrl));
        }

        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //var dataTable = _saver.MakeTable(
            //    _parser.ParseData(
            //        _collector.GetJsonArray(defUrl)));
            gridControl1.DataSource = _parser.ParseData(
                _collector.GetJsonArray(defUrl));
            webBrowser1.Navigate("http://positivebet.com/");
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void barUpdateNow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.SelectRows(0, gridView1.RowCount);
            gridView1.DeleteSelectedRows();
            gridControl1.DataSource = null;
            backgroundWorker1.RunWorkerAsync();
        }
    }
}