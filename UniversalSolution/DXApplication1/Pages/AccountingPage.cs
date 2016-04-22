using DevExpress.XtraGrid;
using System;
using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class AccountingPage : Form
    {
        public EventHandler Update;
        public EventHandler CalculatorCall;
        public bool Close { get; set; }
        public GridControl MainGridControl => gridControl1;
        protected virtual void OnUpdate() => Update?.Invoke(null, null);
        protected virtual void OnCalculatorCall() => CalculatorCall?.Invoke(gridView1.GetFocusedRow(), null);

        public AccountingPage()
        {
            InitializeComponent();
            InitializeEvents();
        }

        public void InitializeEvents()
        {
            Closing += AccountingPage_Closing;
        }

        public void DeInitializeEvents()
        {
            Closing -= AccountingPage_Closing;
        }

        protected virtual void AccountingPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !Close;
            if (!Close)
                Hide();
        }

        protected virtual void bUpdate_Click(object sender, System.EventArgs e) => OnUpdate();

        protected virtual void simpleButton1_Click(object sender, EventArgs e) => OnCalculatorCall();
    }
}
