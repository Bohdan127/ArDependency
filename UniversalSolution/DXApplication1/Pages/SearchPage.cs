using DevExpress.XtraGrid;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Tools;

namespace DXApplication1.Pages
{
    public partial class SearchPage : Form
    {
        public EventHandler Update;
        public EventHandler CalculatorCall;
        public bool Close { get; set; }
        public GridControl MainGridControl => gridControl1;
        private List<Fork> dataSource;

        protected virtual void OnUpdate() => Update?.Invoke(null, null);

        protected virtual void OnCalculatorCall() => CalculatorCall?.Invoke(gridView1.GetFocusedRow(), null);

        public SearchPage()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = progressBarControl1.Properties.Minimum; i < progressBarControl1.Properties.Maximum; i++)
            {
                Thread.Sleep(10);
                progressBarControl1.Invoke(new MethodInvoker(delegate { progressBarControl1.EditValue = i; }));
            }
        }

        protected virtual void AccountingPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !Close;
            if (!Close)
                Hide();
        }

        protected virtual void bUpdate_Click(object sender, System.EventArgs e)
        {
            OnUpdate();
        }

        protected virtual void simpleButton1_Click(object sender, EventArgs e) => OnCalculatorCall();

        private List<Fork> MakeSearch(string eventCriteria, string typeFirstCriteria, string typeSecondCriteria)
        {
            var resList = new List<Fork>();
            resList.AddRange(dataSource);

            if (eventCriteria.IsNotBlank())
                resList = resList.Where(f => f.Event.Contains(eventCriteria)).ToList();
            if (typeFirstCriteria.IsNotBlank())
                resList = resList.Where(f => f.TypeFirst.Contains(typeFirstCriteria)).ToList();
            if (typeSecondCriteria.IsNotBlank())
                resList = resList.Where(f => f.TypeSecond.Contains(typeSecondCriteria)).ToList();

            return resList;
        }

        public void StartLoading()
        {
            backgroundWorker1.RunWorkerAsync();
        }

        public void EndLoading()
        {
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            progressBarControl1.Invoke(new MethodInvoker(
                delegate { progressBarControl1.EditValue = progressBarControl1.Properties.Minimum; }));
        }
    }
}
