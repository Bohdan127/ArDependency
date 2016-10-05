using DataSaver;
using DevExpress.XtraGrid;
using FormulasCollection.Enums;
using FormulasCollection.Models;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class SearchPage : Form
    {
        public EventHandler UpdateEvent;
        public EventHandler CalculatorCall;
        private LocalSaver _localSaver;

        public bool ToClose { get; set; }
        public GridControl MainGridControl => gridControl1;

        public virtual void OnUpdate() => UpdateEvent?.Invoke(null, null);

        public virtual void OnCalculatorCall() => CalculatorCall?.Invoke(gridView1.GetFocusedRow(), null);

        public SearchPage(bool isSearchPage = true)
        {
            InitializeComponent();
            Closing += SearchPage_Closing;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.WorkerSupportsCancellation = true;
            repositoryItemCheckEditSave.EditValueChanging += RepositoryItemCheckEditSave_EditValueChanging;
            _localSaver = new LocalSaver();

            if (isSearchPage)
                InitSearchPage();
            else
                InitAccountingPage();
        }

        private void InitAccountingPage()
        {
            Text = "Учет";
            Icon = Icon.FromHandle(((Bitmap)imageList1.Images[3]).GetHicon());
            repositoryItemCheckEditSave.ValueChecked = ForkType.Merged;
            repositoryItemCheckEditSave.ValueUnchecked = ForkType.Current;
            repositoryItemCheckEditSave.ValueGrayed = ForkType.Saved;
        }

        private void InitSearchPage()
        {
            Text = "Поиск вилок";
            Icon = Icon.FromHandle(((Bitmap)imageList1.Images[2]).GetHicon());
            repositoryItemCheckEditSave.ValueChecked = ForkType.Merged;
            repositoryItemCheckEditSave.ValueUnchecked = ForkType.Current;
        }

        private void RepositoryItemCheckEditSave_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.OldValue == null || e.OldValue == e.NewValue) return;

            var currRow = gridView1.GetFocusedRow() as Fork;

            if (currRow == null) return;

            if (e.OldValue as ForkType? == ForkType.Saved)
            {
                _localSaver.DeleteFork(currRow);
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
            else if (e.NewValue as ForkType? == ForkType.Current
                  || e.NewValue as ForkType? == ForkType.Merged)
            {
                currRow.Type = (ForkType)e.NewValue;
                _localSaver.UpdateFork(currRow);
            }
        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (var i = progressBarControl1.Properties.Minimum; i <= progressBarControl1.Properties.Maximum; i++)
            {
                Thread.Sleep(5);
                progressBarControl1.Invoke(new MethodInvoker(delegate { progressBarControl1.EditValue = i; }));
            }
        }

        protected virtual void SearchPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EndLoading();
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