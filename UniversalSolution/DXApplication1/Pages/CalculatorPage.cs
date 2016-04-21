using FormulasCollection;
using System;
using System.Windows.Forms;
using Tools;

namespace DXApplication1.Pages
{
    public partial class CalculatorPage : Form
    {
        protected ICalculatorFormulas CalculatorFormulas;

        public bool Close { get; set; }
        public bool IsOpen { get; set; }
        public EventHandler RateChanging;
        protected virtual void OnURateChanging() => RateChanging?.Invoke(null, null);

        public CalculatorPage(ICalculatorFormulas _calculatorFormulas)
        {
            InitializeComponent();
            InitializeEvents();
            Shown += CalculatorPage_Shown;
            CalculatorFormulas = _calculatorFormulas;
        }

        private void CalculatorPage_Shown(object sender, System.EventArgs e)
        {
            IsOpen = true;
        }

        public void InitializeEvents()
        {
            Closing += AccountingPage_Closing;
            RateChanging += AccountingPage_RateChanging;
        }

        private void AccountingPage_RateChanging(object sender, EventArgs e)
        {
            textEditAllIncome.Text = CalculatorFormulas.CalculateSummaryIncome(
                    textEditIncome1.Text,
                    textEditIncome2.Text
                ).ToString();
            textEditAllRate.Text = CalculatorFormulas.CalculateSummaryRate(
                    textEditRate1.Text.ConvertToDouble(),
                    textEditRate2.Text.ConvertToDouble()
                ).ToString();
        }

        public void DeInitializeEvents()
        {
            Closing -= AccountingPage_Closing;
            RateChanging -= AccountingPage_RateChanging;
        }

        private void AccountingPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsOpen = false;
            e.Cancel = !Close;
            if (!Close)
                Hide();
        }

        protected virtual void textEditRate1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            textEditIncome1.Text = CalculatorFormulas.CalculateIncome(lbCoef1.Text.ConvertToDouble(), textEditRate1.Text.ConvertToDouble()).ToString();
            OnURateChanging();
        }

        protected virtual void textEditRate2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            textEditIncome2.Text = CalculatorFormulas.CalculateIncome(lbCoef2.Text.ConvertToDouble(), textEditRate2.Text.ConvertToDouble()).ToString();
            OnURateChanging();
        }
    }
}
