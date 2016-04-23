using DataParser;
using DataParser.DefaultRealization;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using License.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using Timer = System.Timers.Timer;

namespace DXApplication1
{
    public partial class Form1 : XtraForm
    {
        #region Fields

        private DefaultDataParser _parser;
        private Timer _updateTimer;
        private bool isAutoUpdate = false;
        private bool isDeleteOldData = false;
        private bool isColoredNewData = false;
        private List<Office> _sites;
        private string licenseKey = string.Empty;
        private const string outFileName = @"lastChanges.xml";

        #endregion Fields

        #region CTOR

        public Form1()
        {
            _parser = new DefaultDataParser();
            _sites = new List<Office>();

            InitSkinGallery();

            DefaultData();

            LicenseForm licenseForm = new LicenseForm();
            if (!licenseForm.CheckInstance(licenseKey))
                licenseForm.ShowDialog();
            if (!licenseForm.IsRegistered)
                Close();

            DefaultEvents();

            ////дефолт бо неробить ця ф-ціональність іще
            barDeleteOldData.EditValue = true;
            isDeleteOldData = true;
            barColorNewData.EditValue = false;
            barNewColor.Enabled = false;
            barColorNewData.Enabled = false;
            barDeleteOldData.Enabled = false;
        }

        #endregion CTOR

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
            using (XmlReader reader = XmlReader.Create(outFileName))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "UpdateTime":
                                barUpdateTime.EditValue = reader.ReadString();
                                break;

                            case "AutoUpdate":
                                if (reader.ReadString() == "True")
                                {
                                    barAutoUpdate.EditValue = true;
                                    AutoUpdateChanged(null, null);
                                }
                                break;

                            case "DeleteOldData":
                                if (reader.ReadString() == "True")
                                {
                                    barDeleteOldData.EditValue = true;
                                    isDeleteOldData = true;
                                }
                                break;

                            case "ColoredNewData":
                                if (reader.ReadString() == "True")
                                {
                                    barColorNewData.EditValue = true;
                                    isColoredNewData = true;
                                }
                                break;

                            case "NewColor":
                                barNewColor.EditValue = Color.FromArgb(Convert.ToInt32(reader.ReadString()));
                                break;

                            case "ua1xetCom":
                                if (reader.ReadString() == "True")
                                {
                                    barua1xetCom.EditValue = true;
                                    SwitchUa1xetCom_Toggled(null, null);
                                }
                                break;

                            case "olimpKz":
                                if (reader.ReadString() == "True")
                                {
                                    barolimpKz.EditValue = true;
                                    SwitchOlimpKz_Toggled(null, null);
                                }
                                break;

                            case "fonbetCom":
                                if (reader.ReadString() == "True")
                                {
                                    barfonbetCom.EditValue = true;
                                    SwitchFonbetCom_Toggled(null, null);
                                }
                                break;

                            case "williamhillCom":
                                if (reader.ReadString() == "True")
                                {
                                    barwilliamhillCom.EditValue = true;
                                    SwitchWilliamHillCom_Toggled(null, null);
                                }
                                break;

                            case "ru10betCom":
                                if (reader.ReadString() == "True")
                                {
                                    barru10betCom.EditValue = true;
                                    SwitchRu10BetCom_Toggled(null, null);
                                }
                                break;

                            case "LicenseKey":
                                licenseKey = reader.ReadString();
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
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

                if (currentDataList == null || currentDataList.Count == 0)
                {
                    gridControl1.DataSource = newDataList;
                    return;
                }

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    gridView1.FocusedRowHandle = i;
                    var currMatch = newDataList.FirstOrDefault(
                                d => d.Id == currentDataList[i].Id);
                    if (currMatch != null)
                    {
                        CheckDiff(currentDataList[i], currMatch, i);
                        newDataList.Remove(currMatch);
                    }
                    //todo пофарбувати тут всі нові
                    currentDataList.AddRange(newDataList);
                }
            }
        }

        /// <summary>
        /// Draw selected column with color selected in menu for barNewColor  for focused row   if isColoredNewData set to True
        /// </summary>
        /// <param name="colName">Name of column for painting</param>
        protected virtual void DrawSomeColumnInFocusedRow(string colName)
        {
            if (isColoredNewData)
                gridView1.Columns[colName].AppearanceCell.BackColor = (Color)barNewColor.EditValue;
        }

        /// <summary>
        /// Check if it is any diff between matches and color this diff
        /// </summary>
        /// <param name="match"></param>
        /// <param name="dataMatch"></param>
        private void CheckDiff(GenericMatch match, GenericMatch dataMatch, int rowHanle)
        {
            if (match.Champ != dataMatch.Champ)
            {
                DrawSomeColumnInFocusedRow(colChamp.FieldName);
                match.Champ = dataMatch.Champ;
            }
            if (match.ForkPercent != dataMatch.ForkPercent)
            {
                DrawSomeColumnInFocusedRow(colForkPercent.FieldName);
                match.ForkPercent = dataMatch.ForkPercent;
            }
            if (match.GameTime != dataMatch.GameTime)
            {
                DrawSomeColumnInFocusedRow(colGameTime.FieldName);
                match.GameTime = dataMatch.GameTime;
            }
            if (match.Id != dataMatch.Id)
            {
                DrawSomeColumnInFocusedRow(colId.FieldName);
                match.Id = dataMatch.Id;
            }
            if (match.Indicator1 != dataMatch.Indicator1)
            {
                DrawSomeColumnInFocusedRow(colIndicator1.FieldName);
                match.Indicator1 = dataMatch.Indicator1;
            }
            if (match.Indicator2 != dataMatch.Indicator2)
            {
                DrawSomeColumnInFocusedRow(colIndicator2.FieldName);
                match.Indicator2 = dataMatch.Indicator2;
            }
            if (match.Indicator3 != dataMatch.Indicator3)
            {
                DrawSomeColumnInFocusedRow(colIndicator3.FieldName);
                match.Indicator3 = dataMatch.Indicator3;
            }
            if (match.Office != dataMatch.Office)
            {
                DrawSomeColumnInFocusedRow(colOffice.FieldName);
                match.Office = dataMatch.Office;
            }
            if (match.OfficeBetType != dataMatch.OfficeBetType)
            {
                DrawSomeColumnInFocusedRow(colBetType.FieldName);
                match.OfficeBetType = dataMatch.OfficeBetType;
            }
            if (match.SportName != dataMatch.SportName)
            {
                DrawSomeColumnInFocusedRow(colSportname.FieldName);
                match.SportName = dataMatch.SportName;
            }
            if (match.Time != dataMatch.Time)
            {
                DrawSomeColumnInFocusedRow(colTime.FieldName);
                match.Time = dataMatch.Time;
            }
            if (match.Сoefficient != dataMatch.Сoefficient)
            {
                //DrawSomeColumnInFocusedRow(coe);
                //todo ми нафік не має цієї колонки треба додати
                match.Сoefficient = dataMatch.Сoefficient;
            }
        }

        private void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        /// <summary>
        /// Create new XML file with name outFileName and write all current settings
        /// </summary>
        private void WriteSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            using (XmlWriter writer = XmlWriter.Create(outFileName, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("MainForm");

                writer.WriteElementString("UpdateTime", barUpdateTime.EditValue?.ToString());
                writer.WriteElementString("AutoUpdate", isAutoUpdate.ToString());
                writer.WriteElementString("DeleteOldData", isDeleteOldData.ToString());
                writer.WriteElementString("ColoredNewData", isColoredNewData.ToString());
                writer.WriteElementString("NewColor", ((Color)barNewColor.EditValue).ToArgb().ToString());
                writer.WriteElementString("ua1xetCom", barua1xetCom.EditValue?.ToString());
                writer.WriteElementString("olimpKz", barolimpKz.EditValue?.ToString());
                writer.WriteElementString("fonbetCom", barfonbetCom.EditValue?.ToString());
                writer.WriteElementString("williamhillCom", barwilliamhillCom.EditValue?.ToString());
                writer.WriteElementString("ru10betCom", barru10betCom.EditValue?.ToString());
                writer.WriteElementString("LicenseKey", licenseKey);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        #endregion Function

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
            //todo щберігати стан в xml

            if (MessageBox.Show("Желаете выйти?", "Выход", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                WriteSettings();
            }
            else
                e.Cancel = true;
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
            gridView1.RefreshData();//refresh view for load new data
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            gridControl1.DataSource = _parser.GetDataForSomeSites(_sites);
            webBrowser1.Navigate("http://positivebet.com/");
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void barUpdateNow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    if (isDeleteOldData)//delete old data
                    {
                        gridView1.SelectRows(0, gridView1.RowCount);
                        gridView1.DeleteSelectedRows();
                        gridControl1.DataSource = null;
                    }
                    //load new data
                    backgroundWorker1.RunWorkerAsync();
                }));
            }
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

        #endregion Events
    }
}