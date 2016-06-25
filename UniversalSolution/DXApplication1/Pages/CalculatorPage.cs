using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ToolsPortable;

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
        private List<string> recommendedRates = new List<string>();
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
            recommendedRates = CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(), textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                , lbCoef1.Text.Trim().ConvertToDoubleOrNull(), lbCoef2.Text.Trim().ConvertToDoubleOrNull());
            textEditRate1.Text = recommendedRates[0];
            textEditRate2.Text = recommendedRates[1];
        }

        public void InitializeEvents()
        {
            Closing += AccountingPage_Closing;
            RateChanging += AccountingPage_RateChanging;
        }

        private void AccountingPage_RateChanging(object sender, EventArgs e)
        {
            textEditAllIncome.Text = CalculatorFormulas.CalculateSummaryIncome(
                textEditIncome1.Text.Trim(),
                textEditIncome2.Text.Trim()
                );
            textEditAllRate.Text = CalculatorFormulas.CalculateSummaryRate(
                textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                textEditRate2.Text.Trim().ConvertToDoubleOrNull()
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
            OnURateChanging();

            try
            {
                textEditIncome1.Text = (CalculatorFormulas.CalculateRate(
                    textEditAllRate.Text.ConvertToDoubleOrNull(),
                    textEditRate1.Text.ConvertToDoubleOrNull(),
                    lbCoef1.Text.Trim().ConvertToDoubleOrNull()).ToString() +
                    (CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                    textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text.Trim().Replace(".", ",").ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().Replace(".", ",").ConvertToDoubleOrNull()))[0]);
            }
            catch (Exception ex)
            {
                textEditIncome1.Text = (CalculatorFormulas.CalculateRate(
                    textEditAllRate.Text.ConvertToDoubleOrNull(),
                    textEditRate1.Text.ConvertToDoubleOrNull(),
                    lbCoef1.Text.Trim().ConvertToDoubleOrNull()).ToString() +
                    (CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                    textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text.Trim().Replace(",", ".").ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().Replace(",", ".").ConvertToDoubleOrNull()))[0]);
            }

            try
            {
                textEditIncome2.Text = (CalculatorFormulas.CalculateRate(
                    textEditAllRate.Text.ConvertToDoubleOrNull(),
                    (textEditAllRate.Text.ConvertToDoubleOrNull() - textEditRate1.Text.ConvertToDoubleOrNull()),
                    lbCoef2.Text.Trim().ConvertToDoubleOrNull()).ToString() +
                    (CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                    textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text.Trim().Replace(".", ",").ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().Replace(".", ",").ConvertToDoubleOrNull()))[1]);
            }
            catch (Exception ex)
            {
                textEditIncome2.Text = (CalculatorFormulas.CalculateRate(
                    textEditAllRate.Text.ConvertToDoubleOrNull(),
                    (textEditAllRate.Text.ConvertToDoubleOrNull() - textEditRate1.Text.ConvertToDoubleOrNull()),
                    lbCoef2.Text.Trim().ConvertToDoubleOrNull()).ToString() +
                    (CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                    textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text.Trim().Replace(",", ".").ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().Replace(",", ".").ConvertToDoubleOrNull()))[1]);
            }
        }

        protected virtual void textEditRate2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            OnURateChanging();

            try
            {
                textEditIncome2.Text = (CalculatorFormulas.CalculateRate(
                    textEditAllRate.Text.ConvertToDoubleOrNull(),
                    textEditRate2.Text.ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().ConvertToDoubleOrNull()).ToString() +
                    (CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                    textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text.Trim().Replace(".", ",").ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().Replace(".", ",").ConvertToDoubleOrNull()))[1]);
            }
            catch (Exception ex)
            {
                textEditIncome2.Text = (CalculatorFormulas.CalculateRate(
                    textEditAllRate.Text.ConvertToDoubleOrNull(),
                    textEditRate2.Text.ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().ConvertToDoubleOrNull()).ToString() +
                    (CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                    textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text.Trim().Replace(",", ".").ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().Replace(",", ".").ConvertToDoubleOrNull()))[1]);
            }

            try
            {
                textEditIncome1.Text = (CalculatorFormulas.CalculateRate(textEditAllRate.Text.ConvertToDoubleOrNull(),
                    (textEditAllRate.Text.ConvertToDoubleOrNull() - textEditRate2.Text.ConvertToDoubleOrNull()),
                    lbCoef1.Text.Trim().ConvertToDoubleOrNull()).ToString() +
                    (CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                    textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text.Trim().Replace(".", ",").ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().Replace(".", ",").ConvertToDoubleOrNull()))[0]);
            }
            catch (Exception ex)
            {
                textEditIncome1.Text = (CalculatorFormulas.CalculateRate(textEditAllRate.Text.ConvertToDoubleOrNull(),
                    (textEditAllRate.Text.ConvertToDoubleOrNull() - textEditRate2.Text.ConvertToDoubleOrNull()),
                    lbCoef1.Text.Trim().ConvertToDoubleOrNull()).ToString() +
                    (CalculatorFormulas.GetRecommendedRates(CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                    textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text.Trim().Replace(",", ".").ConvertToDoubleOrNull(),
                    lbCoef2.Text.Trim().Replace(",", ".").ConvertToDoubleOrNull()))[0]);
            }
        }
    }
}