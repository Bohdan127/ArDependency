using DataColector.DefaultRealization;
using DataParser;
using DataParser.DefaultRealization;
using DataSaver.DefaultRealization;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace DXApplication1
{
    public partial class Form1 : XtraForm
    {
        #region Fields       

        private DefaultDataColector _collector;
        private DefaultDataParser _parser;
        private DefaultDataSaver _saver;
        private Timer _updateTimer;
        private bool isAutoUpdate = false;
        private bool isDeleteOldData = false;
        private bool isColoredNewData = false;
        private List<Office> _sites;
        private string defUrl = @"https://ua.1xbet.com/LiveFeed/Get1x2?sportId=0&sports=&champId=0&tf=1000000&count=50&cnt=10&lng=ru&cfview=0";

        #endregion

        #region CTOR

        public Form1()
        {
#if DEBUG
            var start = DateTime.Now;
#endif
            _parser = new DefaultDataParser();
            _sites = new List<Office>();

            InitializeComponent();
            InitSkinGallery();

            DefaultData();
            DefaultEvents();
#if DEBUG
            MessageBox.Show((DateTime.Now - start).Milliseconds.ToString());
#endif                                                 
        }

        #endregion

        #region Function

        /// <summary>
        /// Set default data for some fields
        /// </summary>
        public void DefaultData()
        {
            _updateTimer = new Timer(1000 * 60);
            repositoryItemTextEdit2.Appearance.Options.UseTextOptions = true;
            repositoryItemTextEdit2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            barAutoUpdate.EditValue = false;

            GetPreviousState();
        }

        /// <summary>
        /// Load previous program state from XML
        /// </summary>
        private void GetPreviousState()
        {
            //todo доставати дані із файла
        }

        /// <summary>
        /// Set default event for some fields
        /// </summary>
        public void DefaultEvents()
        {
            _updateTimer.Elapsed += UpdateTimer_Elapsed;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            Shown += Form1_Shown;
            FormClosing += Form1_FormClosing;
            switchAutoUpdate.Toggled += AutoUpdateChanged;
            barUpdateNow.ItemClick += barUpdateNow_ItemClick;
            barUpdateTime.EditValueChanged += ChangeUpdateTime;
            switchColorNewData.Toggled += SwitchColorNewData_Toggled;
            switchDeleteOldData.Toggled += SwitchDeleteOldData_Toggled;
            switchUa1xetCom.Toggled += SwitchUa1xetCom_Toggled;
            switchOlimpKz.Toggled += SwitchOlimpKz_Toggled;
            switchFonbetCom.Toggled += SwitchFonbetCom_Toggled;
            switchWilliamHillCom.Toggled += SwitchWilliamHillCom_Toggled;
            switchRu10BetCom.Toggled += SwitchRu10BetCom_Toggled;
        }

        /// <summary>
        /// Update only changed data, do this only when we not delete old data
        /// </summary>
        private void UpdateGrid()
        {
            if (!isDeleteOldData)
            {
                List<GenericMatch> currentDataList = (List<GenericMatch>)gridControl1.DataSource;

                var newDataList = _parser.GetDataForSomeSites(_sites);
                foreach (var dataMatch in newDataList)
                {
                    var currMatch = currentDataList.FirstOrDefault(
                                d => d.Id == dataMatch.Id);
                    if (currMatch == null)
                    {
                        currentDataList.Add(dataMatch);
                    }
                    else
                    {
                        CheckDiff(currMatch, dataMatch);
                    }
                }
            }
        }

        /// <summary>
        /// Check if it is any diff between matches and color this diff
        /// </summary>
        /// <param name="match"></param>
        /// <param name="dataMatch"></param>
        private void CheckDiff(GenericMatch match, GenericMatch dataMatch)
        {
            if (match.BetType != dataMatch.BetType)
            {
                match.BetType = dataMatch.BetType;
            }
        }

        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        #endregion

        #region Events

        private void SwitchRu10BetCom_Toggled(object sender, EventArgs e)
        {
            if (!_sites.Any(s => s == Office.ru10betCom))
                _sites.Add(Office.ru10betCom);
            else
                _sites.RemoveAll(s => s == Office.ru10betCom);
        }

        private void SwitchWilliamHillCom_Toggled(object sender, EventArgs e)
        {
            if (!_sites.Any(s => s == Office.williamhillCom))
                _sites.Add(Office.williamhillCom);
            else
                _sites.RemoveAll(s => s == Office.williamhillCom);
        }

        private void SwitchFonbetCom_Toggled(object sender, EventArgs e)
        {
            if (!_sites.Any(s => s == Office.fonbetCom))
                _sites.Add(Office.fonbetCom);
            else
                _sites.RemoveAll(s => s == Office.fonbetCom);
        }

        private void SwitchOlimpKz_Toggled(object sender, EventArgs e)
        {
            if (!_sites.Any(s => s == Office.olimpKz))
                _sites.Add(Office.olimpKz);
            else
                _sites.RemoveAll(s => s == Office.olimpKz);
        }

        private void SwitchUa1xetCom_Toggled(object sender, EventArgs e)
        {
            if (!_sites.Any(s => s == Office.ua1xetCom))
                _sites.Add(Office.ua1xetCom);
            else
                _sites.RemoveAll(s => s == Office.ua1xetCom);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //todo питати чи юзер зоче закрити
            //todo щберігати стан в xml
        }

        private void SwitchDeleteOldData_Toggled(object sender, EventArgs e)
        {
            isDeleteOldData = !isDeleteOldData;
        }

        private void SwitchColorNewData_Toggled(object sender, EventArgs e)
        {
            isColoredNewData = !isColoredNewData;
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
            if (isDeleteOldData)
                gridControl1.DataSource = _parser.GetDataForSomeSites(_sites);
            else
                UpdateGrid();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            gridControl1.DataSource = _parser.GetDataForSomeSites(_sites);
            webBrowser1.Navigate("http://positivebet.com/");
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void barUpdateNow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
#if DEBUG
            var start = DateTime.Now;
#endif
            if (!backgroundWorker1.IsBusy)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    if (isDeleteOldData)
                    {
                        gridView1.SelectRows(0, gridView1.RowCount);
                        gridView1.DeleteSelectedRows();
                        gridControl1.DataSource = null;
                    }

                    backgroundWorker1.RunWorkerAsync();
                }));
            }
#if DEBUG
            MessageBox.Show((DateTime.Now - start).Milliseconds.ToString());
#endif
        }

        private void ChangeUpdateTime(object sender, EventArgs e)
        {
            if (_updateTimer.Enabled)
            {
                _updateTimer.Stop();
                barAutoUpdate.EditValue = false;
            }
            _updateTimer.Interval = int.Parse(barUpdateTime.EditValue.ToString()) * 1000 * 60;//60 - seconds in one minute, 1000 miliseconds at one second 
        }

        private void AutoUpdateChanged(object sender, EventArgs e)
        {
            isAutoUpdate = !isAutoUpdate;

            if (isAutoUpdate)
                _updateTimer.Start();
            else
                _updateTimer.Stop();
        }

        #endregion

    }
}