using DevExpress.XtraEditors;
using DXApplication1.Models;
using FormulasCollection.Interfaces;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using DataSaver.Models;
using License.Logic;

namespace DXApplication1
{
    public partial class XtraForm1 : XtraForm
    {
        #region Members

        //todo Later Make this parameter visible from UI with possibility to change
        public const string SettingsPath = "./";

        public const string SettingsFile = "Configuration.xml";

        private Filter _filter;
        private PageManager _pageManager;
        private string licenseKey = string.Empty;

        #endregion Members

        #region CTOR

        public XtraForm1(IForkFormulas forkFormulas)
        {

            InitializeComponent();

            IsMdiContainer = true;
            Closed += XtraForm1_Closed;
            Closing += XtraForm1_Closing;

            _pageManager = new PageManager(this, forkFormulas);

            DeserializeAll();
            PrepareData();

            //todo this is bad and illogic crutch
            _pageManager.GetFilterPage(_filter);//default preload filter page

            licenseKey = _filter.LicenseKey ?? string.Empty;
            //before payment will be with license
            //LicenseForm licenseForm = new LicenseForm();
            //if (!licenseForm.CheckInstance(licenseKey))
            //    licenseForm.ShowDialog();
            //if (!licenseForm.IsRegistered)
            //    Close();
            //licenseKey = licenseForm.LicenseKey;
        }

        #endregion CTOR

        #region Events

        /// <summary>
        /// Function with dead code for opening calculation after search form if calculator wasn't open before
        /// </summary>
        public async void OpenSearchForCalculator()
        {
            if (!_pageManager.GetCalculatorPage().IsOpen)
            {
                var openForm = await _pageManager.CreateCalculatorForm().ConfigureAwait(true);
                if (openForm.ShowDialog() != DialogResult.OK)
                    return;
                _pageManager.GetCalculatorPage(reload: true).Fork = openForm.SelectedEvent;
            }

            _pageManager.GetCalculatorPage().Hide();//if already shown right now
            _pageManager.GetCalculatorPage().Show();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _pageManager.GetSearchPage().Hide();//if already shown right now
            _pageManager.GetSearchPage().Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _pageManager.GetAccountPage().Hide();//if already shown right now
            _pageManager.GetAccountPage().Show();
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
            _pageManager.GetFilterPage(_filter).Hide();//if already shown right now
            _pageManager.GetFilterPage(_filter).Show();
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
            catch
            {
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
                _filter.LicenseKey = licenseKey;

                var writer = File.Create(SettingsPath + SettingsFile);

                new XmlSerializer(typeof(Filter)).Serialize(
                   writer, _filter);

                writer.Close();
            }
            catch
            {
                bRes = false;
            }

            return bRes;
        }

        #endregion Functions
    }
}