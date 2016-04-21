using DevExpress.XtraGrid;
using System;
using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class AccountingPage : Form
    {
        public EventHandler Update;
        public bool Close { get; set; }
        public GridControl MainGridControl => gridControl1;
        protected virtual void OnUpdate() => Update?.Invoke(null, null);

        public AccountingPage()
        {
            InitializeComponent();
            InitializeEvents();
        }

        public void InitializeEvents()
        {
            this.Closing += AccountingPage_Closing;
        }

        public void DeInitializeEvents()
        {
            this.Closing -= AccountingPage_Closing;
        }

        private void AccountingPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !Close;
            if (!Close)
                Hide();
        }

        private void bUpdate_Click(object sender, System.EventArgs e)
        {
            OnUpdate();
        }
    }
}
