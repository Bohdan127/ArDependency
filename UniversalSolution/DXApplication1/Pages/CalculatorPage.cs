using DataSaver.Models;
using DXApplication1.Models;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System;
using System.Globalization;
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
        private Fork _fork;
        public Fork Fork { set { _fork = value; UpdateForm(); } }//todo check what is true way!!!!

        public CalculatorPage(ICalculatorFormulas calculatorFormulas)
        {
            InitializeComponent();
            InitializeEvents();
            Shown += CalculatorPage_Shown;
            CalculatorFormulas = calculatorFormulas;
        }

        private void CalculatorPage_Shown(object sender, System.EventArgs e) => IsOpen = true;

        private void UpdateForm()
        {
            lbMain.Text = _fork?.Event;
            lbType1.Text = _fork?.TypeFirst;
            lbCoef1.Text = _fork?.CoefFirst;
            lbType2.Text = _fork?.TypeSecond;
            lbCoef2.Text = _fork?.CoefSecond;
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
                );
            textEditAllRate.Text = CalculatorFormulas.CalculateSummaryRate(
                textEditRate1.Text.ConvertToDoubleOrNull(),
                textEditRate2.Text.ConvertToDoubleOrNull()
                );
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
            textEditIncome1.Text = CalculatorFormulas.CalculateRate(
                textEditAllRate.Text.ConvertToDoubleOrNull(),
                textEditRate1.Text.ConvertToDoubleOrNull(),
                lbCoef1.Text.ConvertToDoubleOrNull());
            textEditIncome2.Text = (
                (textEditAllRate.Text.ConvertToDoubleOrNull() - textEditIncome1.Text.ConvertToDoubleOrNull()) * 
                lbCoef2.Text.ConvertToDoubleOrNull() - textEditAllRate.Text.ConvertToDoubleOrNull()).ToString();
            OnURateChanging();
        }

        protected virtual void textEditRate2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            textEditIncome2.Text = CalculatorFormulas.CalculateRate(
                textEditAllRate.Text.ConvertToDoubleOrNull(),
                textEditRate2.Text.ConvertToDoubleOrNull(), 
                lbCoef2.Text.ConvertToDoubleOrNull());
            textEditIncome1.Text = ((textEditAllRate.Text.ConvertToDoubleOrNull() - textEditIncome2.Text.ConvertToDoubleOrNull()) *
                lbCoef1.Text.ConvertToDoubleOrNull() - textEditAllRate.Text.ConvertToDoubleOrNull()).ToString();
            OnURateChanging();
        }
    }
}
