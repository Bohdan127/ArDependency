using DevExpress.XtraEditors;
using DXApplication1.DB;
using DXApplication1.Pages;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DXApplication1
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        //todo Later Make this parameter visible from UI with possibility to change
        public const string SettingsPath = "./";
        public const string SettingsFile = "DataSet.xml";

        private DataSet1 _dataSet1;
        private FilterPage _filterPage;


        public XtraForm1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            _dataSet1 = new DataSet1();
            var row = _dataSet1.Filter.NewFilterRow();
            row.Min = 1;
            row.Max = 3;
            _dataSet1.Filter.Rows.Add(row);
            DeserializeAll();
            this.Closed += XtraForm1_Closed;
            this.Closing += XtraForm1_Closing;
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
        }

        private void XtraForm1_Closed(object sender, System.EventArgs e)
        {
            SerializeAll();
        }

        private bool DeserializeAll()
        {
            //todo Show Message if bRes == False that can't read old data
            var bRes = true;

            _dataSet1 = (DataSet1)new XmlSerializer(typeof(DataSet1))
                .Deserialize(new StreamReader(SettingsPath + SettingsFile));
            bRes = _dataSet1 != null;

            return bRes;
        }

        private bool SerializeAll()
        {
            //todo Show Message if bRes == False that can't read old data
            var bRes = true;
            var writer = File.Create(SettingsPath + SettingsFile);

            new XmlSerializer(typeof(DataSet1)).Serialize(
               writer, _dataSet1);

            writer.Close();
            return bRes;
        }


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_filterPage == null)
            {
                _filterPage = new FilterPage(_dataSet1);
                _filterPage.MdiParent = this;
                _filterPage.Close = false;
            }
            _filterPage.Show();
        }
    }
}