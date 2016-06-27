using FormulasCollection.Realizations;
using System;
using System.Windows.Forms;
using ToolsPortable;

namespace DXApplication1.Pages
{
    public partial class CalculatorPage : Form
    {
        protected TwoOutComeCalculatorFormulas CalculatorFormulas;

        public bool ToClose { get; set; }
        public bool IsOpen { get; set; }
        public EventHandler RateChanging;
        public EventHandler UpdateForm;


        private void OnUpdateForm() => UpdateForm?.Invoke(null, null);

        protected virtual void OnURateChanging() => RateChanging?.Invoke(null, null);

        private Fork _fork;
        private Tuple<string, string> _recommendedRates;

        public Fork Fork
        {
            set
            {
                _fork = value;
                OnUpdateForm();
            }
        }

        public CalculatorPage()
        {
            InitializeComponent();
            InitializeEvents();
            CalculatorFormulas = new TwoOutComeCalculatorFormulas();
        }

        ~CalculatorPage()
        {
            DeInitializeEvents();
        }

        private void CalculatorPage_UpdateForm(object sender, EventArgs eventArgs)
        {
            DeInitializeEvents();


            lbMain.Text = _fork?.Event;
            lbType1.Text = _fork?.TypeFirst;
            lbCoef1.Text = _fork?.CoefFirst;
            lbType2.Text = _fork?.TypeSecond;
            lbCoef2.Text = _fork?.CoefSecond;
            _recommendedRates =
                CalculatorFormulas.GetRecommendedRates(
                    CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                        textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , lbCoef1.Text?.Trim().ConvertToDoubleOrNull(), lbCoef2.Text?.Trim().ConvertToDoubleOrNull());
            textEditRate1.Text = _recommendedRates.Item1;
            textEditRate2.Text = _recommendedRates.Item2;


            InitializeEvents();
        }

        private void CalculatorPage_Shown(object sender, System.EventArgs e) => IsOpen = true;


        public void InitializeEvents()
        {
            Closing += AccountingPage_Closing;
            Shown += CalculatorPage_Shown;
            UpdateForm += CalculatorPage_UpdateForm;
            RateChanging += AccountingPage_RateChanging;
            textEditRate2.EditValueChanging += textEditRate1_EditValueChanging;
            textEditRate1.EditValueChanging += textEditRate1_EditValueChanging;

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
            Shown -= CalculatorPage_Shown;
            RateChanging -= AccountingPage_RateChanging;
            UpdateForm -= CalculatorPage_UpdateForm;
            textEditRate2.EditValueChanging -= textEditRate1_EditValueChanging;
            textEditRate1.EditValueChanging -= textEditRate1_EditValueChanging;
        }

        private void AccountingPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsOpen = false;
            e.Cancel = !ToClose;
            if (!ToClose)
                Hide();
        }

        protected virtual void textEditRate1_EditValueChanging(object sender,
            DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var recomendedRates =
                CalculatorFormulas.GetRecommendedRates(
                    CalculatorFormulas.CalculateSummaryRate(textEditRate1.Text.Trim().ConvertToDoubleOrNull(),
                        textEditRate2.Text.Trim().ConvertToDoubleOrNull()).ConvertToDoubleOrNull()
                    , ConvertToRate(lbCoef1.Text),
                    ConvertToRate(lbCoef2.Text));

            textEditIncome1.Text = CalculatorFormulas.CalculateRate(
                ConvertToRate(textEditAllRate.Text),
                ConvertToRate(textEditRate1.Text),
                ConvertToRate(lbCoef1.Text)) +
                                   recomendedRates.Item1;
            textEditIncome2.Text = CalculatorFormulas.CalculateRate(
                ConvertToRate(textEditAllRate.Text),
                ConvertToRate(textEditAllRate.Text) - ConvertToRate(textEditRate1.Text),
                ConvertToRate(lbCoef2.Text)) +
                                   recomendedRates.Item2;

            OnURateChanging();
        }

        public static double? ConvertToRate(string value)
        {
            return value?.Trim().Replace(".", ",").ConvertToDoubleOrNull();
        }
    }
}