using DevExpress.XtraEditors;
using DXApplication1.Models;
using DXApplication1.Pages;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DXApplication1
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {

        #region Members
        //todo Later Make this parameter visible from UI with possibility to change
        public const string SettingsPath = "./";
        public const string SettingsFile = "DataSet.xml";

        private FilterPage _filterPage;
        private AccountingPage _accountingPage;
        private CalculatorPage _calculatorPage;
        private Filter _filter;
        #endregion

        #region CTOR
        public XtraForm1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.Closed += XtraForm1_Closed;
            this.Closing += XtraForm1_Closing;

            DeserializeAll();
            PrepareData();
        }
        #endregion

        #region Events

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_calculatorPage == null)
            {
                _calculatorPage = new CalculatorPage();
                _calculatorPage.MdiParent = this;
                _calculatorPage.Close = false;
            }
            var openForm = new OpenCalculatorForm();

            if (!_calculatorPage.IsOpen)
            {
                if (openForm.ShowDialog() != DialogResult.OK)
                    return;
            }

            _calculatorPage.Hide();//if already shown right now
            _calculatorPage.Show();

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_accountingPage == null)
            {
                _accountingPage = new AccountingPage();
                _accountingPage.MdiParent = this;
                _accountingPage.Close = false;
                _accountingPage.Update += AccountPage_Update;
            }
            else
            {
                _accountingPage.Hide();
            }
            _accountingPage.Show();
        }

        private void AccountPage_Update(object sender, EventArgs e)
        {
            //todo Call here all async logic for getting new Data from Parser  with parameters defined in _filter
        }

        private void XtraForm1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = XtraMessageBox.Show(
                "Действительно хотите выйти?",
                "Выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
                       == DialogResult.No;

            if (_filterPage != null)
                _filterPage.Close = !e.Cancel;

            if (_accountingPage != null)
                _accountingPage.Close = !e.Cancel;
        }
        private void XtraForm1_Closed(object sender, System.EventArgs e)
        {
            SerializeAll();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_filterPage == null)
            {
                _filterPage = new FilterPage(_filter);
                _filterPage.MdiParent = this;
                _filterPage.Close = false;
            }
            else
            {
                _filterPage.Hide();
            }
            _filterPage.Show();
        }
        #endregion

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
            var writer = File.Create(SettingsPath + SettingsFile);

            //todo looks like this method is deficiency
            //MergeDataSet();

            new XmlSerializer(typeof(Filter)).Serialize(
               writer, _filter);

            writer.Close();
            return bRes;
        }
        #endregion


    }
}