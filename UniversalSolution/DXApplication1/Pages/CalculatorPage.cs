using FormulasCollection.Models;
using FormulasCollection.Realizations;
using System;
using System.Windows.Forms;
using ToolsPortable;

namespace DXApplication1.Pages
{
    public partial class CalculatorPage : Form
    {
        private readonly TwoOutComeCalculatorFormulas _calculatorFormulas;

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
            _calculatorFormulas = new TwoOutComeCalculatorFormulas();
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



            _recommendedRates = _calculatorFormulas.GetRecommendedRates(
                    _fork?.Profit,
                     lbCoef1.Text.ConvertToDoubleOrNull(),
                     lbCoef2.Text.ConvertToDoubleOrNull());
            textEditRate1.Text = _recommendedRates.Item1;
            textEditRate2.Text = _recommendedRates.Item2;
            OnURateChanging();

            InitializeEvents();
        }

        private void CalculatorPage_Shown(object sender, System.EventArgs e) => IsOpen = true;


        public void InitializeEvents()
        {
            Closing += AccountingPage_Closing;
            Shown += CalculatorPage_Shown; UpdateForm += CalculatorPage_UpdateForm;
            RateChanging += AccountingPage_RateChanging;
            textEditRate2.EditValueChanged += TextEditRate2_EditValueChanged;
            textEditRate1.EditValueChanged += TextEditRate2_EditValueChanged;

        }

        private void AccountingPage_RateChanging(object sender, EventArgs e)
        {
            //Update all inserted rate
            textEditAllRate.Text = _calculatorFormulas.CalculateSummaryRate(
                textEditRate1.Text.ConvertToDoubleOrNull(),
                textEditRate2.Text.ConvertToDoubleOrNull())?.ToString();

            //Get first Income
            textEditIncome1.Text = _calculatorFormulas.CalculateRate(
                 textEditAllRate.Text.ConvertToDoubleOrNull(),
                 textEditRate1.Text.ConvertToDoubleOrNull(),
                 lbCoef1.Text.ConvertToDoubleOrNull());

            //Get second Income
            textEditIncome2.Text = _calculatorFormulas.CalculateRate(
                 textEditAllRate.Text.ConvertToDoubleOrNull(),
                 textEditAllRate.Text.ConvertToDoubleOrNull() - textEditRate1.Text.ConvertToDoubleOrNull(),
                 lbCoef2.Text.ConvertToDoubleOrNull());

            //Get clear income for first one
            textEditIncome3.Text = _calculatorFormulas.CalculateClearRate(
                 textEditRate2.Text.ConvertToDoubleOrNull(),
                 textEditIncome1.Text.ConvertToDoubleOrNull());

            //get clear income for second one
            textEditIncome4.Text = _calculatorFormulas.CalculateClearRate(
                 textEditRate1.Text.ConvertToDoubleOrNull(),
                 textEditIncome2.Text.ConvertToDoubleOrNull());

            //get summary income
            textEditAllIncome.Text = _calculatorFormulas.CalculateSummaryIncome(
                textEditIncome3.Text.ConvertToDoubleOrNull(),
                textEditIncome4.Text.ConvertToDoubleOrNull());
        }

        public void DeInitializeEvents()
        {
            Closing -= AccountingPage_Closing;
            Shown -= CalculatorPage_Shown;
            RateChanging -= AccountingPage_RateChanging;
            UpdateForm -= CalculatorPage_UpdateForm;
            textEditRate2.EditValueChanged -= TextEditRate2_EditValueChanged;
            textEditRate1.EditValueChanged -= TextEditRate2_EditValueChanged;
        }

        private void AccountingPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsOpen = false;
            e.Cancel = !ToClose;
            if (!ToClose)
                Hide();
        }

        protected virtual void TextEditRate2_EditValueChanged(object sender, EventArgs e)
        {
            OnURateChanging();
        }
    }
}