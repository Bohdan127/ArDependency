using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class CalculatorPage : Form
    {
        public bool Close { get; set; }
        public bool IsOpen { get; set; }

        public CalculatorPage()
        {
            InitializeComponent();
            InitializeEvents();
            Shown += CalculatorPage_Shown;
        }

        private void CalculatorPage_Shown(object sender, System.EventArgs e)
        {
            IsOpen = true;
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
            IsOpen = false;
            e.Cancel = !Close;
            if (!Close)
                Hide();
        }
    }
}
