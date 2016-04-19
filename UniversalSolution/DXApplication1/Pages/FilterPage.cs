using DXApplication1.Models;
using System.Windows.Forms;
using Tools;

namespace DXApplication1.Pages
{
    public partial class FilterPage : Form
    {
        private Filter _filter;

        public bool Close { get; set; }

        public FilterPage()
        {
            InitializeComponent();
        }

        public FilterPage(Filter filter)
            : this()
        {
            _filter = filter;
            FirstBind();
            InitializeEvents();
        }

        protected virtual void FirstBind()
        {
            lock (_filter)
            {
                minTextEdit.EditValue = _filter.Min;
                maxTextEdit.EditValue = _filter.Max;
                marathonBetToggleSwitch.EditValue = _filter.MarathonBet;
                pinnacleSportsToggleSwitch.EditValue = _filter.PinnacleSports;
                footballToggleSwitch.EditValue = _filter.Football;
                basketballToggleSwitch.EditValue = _filter.Basketball;
                volleyballToggleSwitch.EditValue = _filter.Volleyball;
                hockeyToggleSwitch.EditValue = _filter.Hockey;
                tennisToggleSwitch.EditValue = _filter.Tennis;
                fasterDateTimePicker.EditValue = _filter.FaterThen;//???
                longerDateTimePicker.EditValue = _filter.LongerThen;//???
                outCome2ToggleSwitch.EditValue = _filter.OutCome2;
                outcome3ToggleSwitch.EditValue = _filter.OutCome3;
            }
        }

        public void InitializeEvents()
        {
            this.Closing += FilterPage_Closing;
        }

        public void DeInitializeEvents()
        {
            this.Closing -= FilterPage_Closing;
        }

        private void FilterPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !Close;
            if (!Close)
                Hide();
        }

        private void Min_Changing(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            lock (_filter)
            {
                _filter.Min = e.NewValue.ConvertToIntOrNull();
            }
        }

        private void Max_Changing(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            lock (_filter)
            {
                _filter.Max = e.NewValue.ConvertToIntOrNull();
            }
        }

        private void MarathonBet_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.MarathonBet = !_filter.MarathonBet;
            }
        }

        private void PinnacleSports_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.PinnacleSports = !_filter.PinnacleSports;
            }
        }

        private void Football_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.Football = !_filter.Football;
            }
        }

        private void Basketball_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.Basketball = !_filter.Basketball;
            }
        }

        private void Volleyball_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.Volleyball = !_filter.Volleyball;
            }
        }

        private void Hockey_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.Hockey = !_filter.Hockey;
            }
        }

        private void Tennis_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.Tennis = !_filter.Hockey;
            }
        }

        private void Faster_Changing(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            lock (_filter)
            {
                _filter.FaterThen = DBTools.ConvertToDateTime(e.NewValue);
            }
        }

        private void Later_Changing(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            lock (_filter)
            {
                _filter.LongerThen = DBTools.ConvertToDateTime(e.NewValue);
            }
        }

        private void OutCome2_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.OutCome2 = !_filter.OutCome2;
            }
        }

        private void OutCome3_Toggled(object sender, System.EventArgs e)
        {
            lock (_filter)
            {
                _filter.OutCome3 = !_filter.OutCome3;
            }
        }
    }
}
