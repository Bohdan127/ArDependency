using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class AccountingPage : Form
    {
        public bool Close { get; set; }

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
            //todo Update Grid here
        }
    }
}
