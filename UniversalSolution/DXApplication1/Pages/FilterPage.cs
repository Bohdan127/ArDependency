using DXApplication1.Models;
using System.Windows.Forms;
using Tools;

namespace DXApplication1.Pages
{
    public partial class FilterPage : Form
    {
        public Filter Filter { get; private set; }

        public bool Close { get; set; }

        public FilterPage()
        {
            InitializeComponent();
        }

        public FilterPage(Filter filter)
            : this()
        {
            Filter = filter;
            FirstBind();
            InitializeEvents();
        }

        protected virtual void FirstBind()
        {
            lock (Filter)
            {
                minTextEdit.EditValue = Filter.Min;
                maxTextEdit.EditValue = Filter.Max;
                marathonBetToggleSwitch.EditValue = Filter.MarathonBet;
                pinnacleSportsToggleSwitch.EditValue = Filter.PinnacleSports;
                footballToggleSwitch.EditValue = Filter.Football;
                basketballToggleSwitch.EditValue = Filter.Basketball;
                volleyballToggleSwitch.EditValue = Filter.Volleyball;
                hockeyToggleSwitch.EditValue = Filter.Hockey;
                tennisToggleSwitch.EditValue = Filter.Tennis;
                fasterDateTimePicker.EditValue = Filter.FaterThen;//???
                longerDateTimePicker.EditValue = Filter.LongerThen;//???
                outCome2ToggleSwitch.EditValue = Filter.OutCome2;
                outcome3ToggleSwitch.EditValue = Filter.OutCome3;
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
            lock (Filter)
            {
                Filter.Min = e.NewValue.ConvertToIntOrNull();
            }
        }

        private void Max_Changing(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.Max = e.NewValue.ConvertToIntOrNull();
            }
        }

        private void MarathonBet_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.MarathonBet = !Filter.MarathonBet;
            }
        }

        private void PinnacleSports_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.PinnacleSports = !Filter.PinnacleSports;
            }
        }

        private void Football_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.Football = !Filter.Football;
            }
        }

        private void Basketball_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.Basketball = !Filter.Basketball;
            }
        }

        private void Volleyball_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.Volleyball = !Filter.Volleyball;
            }
        }

        private void Hockey_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.Hockey = !Filter.Hockey;
            }
        }

        private void Tennis_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.Tennis = !Filter.Hockey;
            }
        }

        private void Faster_Changing(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.FaterThen = DBTools.ConvertToDateTime(e.NewValue);
            }
        }

        private void Later_Changing(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.LongerThen = DBTools.ConvertToDateTime(e.NewValue);
            }
        }

        private void OutCome2_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.OutCome2 = !Filter.OutCome2;
            }
        }

        private void OutCome3_Toggled(object sender, System.EventArgs e)
        {
            lock (Filter)
            {
                Filter.OutCome3 = !Filter.OutCome3;
            }
        }
    }
}
