using DevExpress.XtraGrid;
using System;
using System.Threading;
using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class SearchPage : Form
    {
        public EventHandler UpdateEvent;
        public EventHandler CalculatorCall;
        public bool ToClose { get; set; }
        public GridControl MainGridControl => gridControl1;

        protected virtual void OnUpdate() => UpdateEvent?.Invoke(null, null);

        protected virtual void OnCalculatorCall() => CalculatorCall?.Invoke(gridView1.GetFocusedRow(), null);

        public SearchPage()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = progressBarControl1.Properties.Minimum; i <= progressBarControl1.Properties.Maximum; i++)
            {
                Thread.Sleep(5);
                progressBarControl1.Invoke(new MethodInvoker(delegate { progressBarControl1.EditValue = i; }));
            }
        }

        protected virtual void AccountingPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ToClose;
            if (!ToClose)
                Hide();
        }

        protected virtual void bUpdate_Click(object sender, System.EventArgs e)
        {
            OnUpdate();
        }

        protected virtual void simpleButton1_Click(object sender, EventArgs e) => OnCalculatorCall();

        public void StartLoading()
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        public void EndLoading()
        {
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
        }
    }
}