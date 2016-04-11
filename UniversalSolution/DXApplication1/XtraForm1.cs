using DXApplication1.Pages;

namespace DXApplication1
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var filterPage = new FilterPage();
            filterPage.MdiParent = this;
            filterPage.Show();
        }
    }
}