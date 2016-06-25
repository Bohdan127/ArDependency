using System;
using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class AccountingPage : Form
    {
        public EventHandler UpdateEvent;
        public EventHandler CalculatorCall;
        public bool ToClose { get; set; }

        protected virtual void OnUpdate() => UpdateEvent?.Invoke(null, null);

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
            e.Cancel = !ToClose;
            if (!ToClose)
                Hide();
        }

        protected virtual void bUpdate_Click(object sender, System.EventArgs e) => OnUpdate();

        protected virtual void simpleButton1_Click(object sender, EventArgs e) => OnCalculatorCall();
    }
}