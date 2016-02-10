using DataColector.Interfaces;
using DataParser.Interfaces;
using DataSaver.Interfaces;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using System;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace DXApplication1
{
    public partial class Form1 : XtraForm
    {
        private IDataColector _collector;
        private IDataParser _parser;
        private IDataSaver _saver;
        private Timer updateTimer;

        private string defUrl = @"https://ua.1xbet.com/LiveFeed/Get1x2?sportId=0&sports=&champId=0&tf=1000000&count=50&cnt=10&lng=ru&cfview=0";

        public Form1(IDataColector colector, IDataParser parser, IDataSaver saver)
        {
            _collector = colector;
            _parser = parser;
            _saver = saver;

            InitializeComponent();
            InitSkinGallery();


            updateTimer = new Timer(1000 * 60);
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            Shown += Form1_Shown;

            repositoryItemToggleSwitch1.Toggled += AutoUpdateChanged;
            barEditItem1.EditValue = false;
            barEditItem1.EditValueChanged += AutoUpdateChanged;
            repositoryItemTextEdit1.Appearance.Options.UseTextOptions = true;
            repositoryItemTextEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            barUpdateNow.ItemClick += barUpdateNow_ItemClick;
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            barUpdateNow_ItemClick(null, null);
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
            if (!backgroundWorker1.IsBusy)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    gridView1.SelectRows(0, gridView1.RowCount);
                    gridView1.DeleteSelectedRows();
                    gridControl1.DataSource = null;

                    backgroundWorker1.RunWorkerAsync();
                }));
            }
        }

        private void ChangeUpdateTime(object sender, EventArgs e)
        {
            if (updateTimer.Enabled)
            {
                updateTimer.Stop();
                barEditItem1.EditValue = false;
            }
            updateTimer.Interval = int.Parse(barEditItem2.EditValue.ToString()) * 1000 * 60;//60 - seconds in one minute, 1000 miliseconds at one second 
        }

        private void AutoUpdateChanged(object sender, EventArgs e)
        {
            if ((bool)barEditItem1.EditValue)
                updateTimer.Start();
            else
                updateTimer.Stop();
        }
    }
}