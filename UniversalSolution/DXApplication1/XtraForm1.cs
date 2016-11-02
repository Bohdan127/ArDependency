using DataSaver.Models;
using DevExpress.XtraEditors;
using DXApplication1.Models;
using License.Logic;
using NLog;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DXApplication1
{
    public partial class XtraForm1 : XtraForm
    {
        #region Members

        // ReSharper disable once InconsistentNaming
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public const string SettingsPath = "./";
        public const string SettingsFile = "Configuration.xml";

        private Filter _filter;
        private PageManager _pageManager;
        private string licenseKey = string.Empty;

        #endregion Members

        #region CTOR

        public XtraForm1()
        {
            InitializeComponent();

            IsMdiContainer = true;
            Closed += XtraForm1_Closed;
            Closing += XtraForm1_Closing;

            _pageManager = new PageManager(this);
            DeserializeAll();
            PrepareData();

            _pageManager.GetFilterPage(_filter);

            var licenseForm = new LicenseForm();
            if (!licenseForm.CheckInstance(_filter.LicenseKey))
                licenseForm.ShowDialog();
            if (!licenseForm.IsRegistered)
                Close();
            _filter.LicenseKey = licenseForm.LicenseKey;
        }
        #endregion CTOR

        #region Events

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var page = _pageManager.GetSearchPage();
            page.Hide();//if already shown right now
            page.Show();
            page.OnUpdate();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var page = _pageManager.GetAccountPage();
            page.Hide();//if already shown right now
            page.Show();
            page.OnUpdate();
        }

        private void XtraForm1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = XtraMessageBox.Show(
                "Действительно хотите выйти?",
                "Выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
                       == DialogResult.No;

            if (!e.Cancel)
                _pageManager.CloseAllPages();
        }

        private void XtraForm1_Closed(object sender, System.EventArgs e)
        {
            SerializeAll();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var page = _pageManager.GetFilterPage(_filter);
            page.Hide();//if already shown right now
            page.Show();
        }

        #endregion Events

        #region Functions

        private void PrepareData()
        {
            if (_filter == null)
                _filter = new Filter();
        }

        /// <summary>
        /// Save changes to XML output file
        /// </summary>
        /// <returns></returns>
        protected bool DeserializeAll()
        {
            //todo Show Message if bRes == False that can't read old data
            var bRes = true;
            try
            {
                _filter = (Filter)new XmlSerializer(typeof(Filter))
                    .Deserialize(new StreamReader(SettingsPath + SettingsFile));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                bRes = false;
            }

            bRes &= _filter != null;

            return bRes;
        }

        /// <summary>
        /// Read previous data from XML
        /// </summary>
        /// <returns></returns>
        protected bool SerializeAll()
        {
            //todo Show Message if bRes == False that can't read old data
            var bRes = true;
            try
            {
                var writer = File.Create(SettingsPath + SettingsFile);

                new XmlSerializer(typeof(Filter)).Serialize(
                   writer, _filter);

                writer.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                bRes = false;
            }

            return bRes;
        }

        #endregion Functions
    }
}