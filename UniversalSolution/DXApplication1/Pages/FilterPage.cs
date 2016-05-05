using DevExpress.XtraEditors.Controls;
using DXApplication1.Models;
using System;
using System.ComponentModel;
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

            //New Requirements: parser for this two site only
            Filter.MarathonBet = true;
            Filter.PinnacleSports = true;

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
                fasterDateTimePicker.EditValue = Filter.FaterThen;
                longerDateTimePicker.EditValue = Filter.LongerThen;
                outCome2ToggleSwitch.EditValue = Filter.OutCome2;
                outcome3ToggleSwitch.EditValue = Filter.OutCome3;
                textEditUserLogin.EditValue = Filter.UserName;
                textEditUserPass.EditValue = Filter.UserPass;
                textEditAutoUpdate.EditValue = Filter.AutoUpdateTime;
                spinEditRate.EditValue = Filter.DefaultRate;
            }
        }

        public void InitializeEvents()
        {
            Closing += FilterPage_Closing;
            fasterDateTimePicker.EditValueChanging += Faster_Changing;
            maxTextEdit.EditValueChanging += Max_Changing;
            minTextEdit.EditValueChanging += Min_Changing;
            outCome2ToggleSwitch.Toggled += OutCome2_Toggled;
            outcome3ToggleSwitch.Toggled += OutCome3_Toggled;
            pinnacleSportsToggleSwitch.Toggled += PinnacleSports_Toggled;
            marathonBetToggleSwitch.Toggled += MarathonBet_Toggled;
            basketballToggleSwitch.Toggled += Basketball_Toggled;
            footballToggleSwitch.Toggled += Football_Toggled;
            longerDateTimePicker.EditValueChanging += Later_Changing;
            volleyballToggleSwitch.Toggled += Volleyball_Toggled;
            tennisToggleSwitch.Toggled += Tennis_Toggled;
            hockeyToggleSwitch.Toggled += Hockey_Toggled;
            textEditUserLogin.EditValueChanging += TextEditUserLogin_EditValueChanging;
            textEditUserPass.EditValueChanging += TextEditUserPass_EditValueChanging;
            textEditAutoUpdate.EditValueChanging += TextEditAutoUpdate_EditValueChanging;
            spinEditRate.EditValueChanging += SpinEditRate_EditValueChanging;
        }

        public void DeInitializeEvents()
        {
            Closing -= FilterPage_Closing;
            fasterDateTimePicker.EditValueChanging -= Faster_Changing;
            maxTextEdit.EditValueChanging -= Max_Changing;
            minTextEdit.EditValueChanging -= Min_Changing;
            outCome2ToggleSwitch.Toggled -= OutCome2_Toggled;
            outcome3ToggleSwitch.Toggled -= OutCome3_Toggled;
            pinnacleSportsToggleSwitch.Toggled -= PinnacleSports_Toggled;
            marathonBetToggleSwitch.Toggled -= MarathonBet_Toggled;
            basketballToggleSwitch.Toggled -= Basketball_Toggled;
            footballToggleSwitch.Toggled -= Football_Toggled;
            longerDateTimePicker.EditValueChanging -= Later_Changing;
            volleyballToggleSwitch.Toggled -= Volleyball_Toggled;
            tennisToggleSwitch.Toggled -= Tennis_Toggled;
            hockeyToggleSwitch.Toggled -= Hockey_Toggled;
            textEditUserLogin.EditValueChanging -= TextEditUserLogin_EditValueChanging;
            textEditUserPass.EditValueChanging -= TextEditUserPass_EditValueChanging;
            textEditAutoUpdate.EditValueChanging -= TextEditAutoUpdate_EditValueChanging;
            spinEditRate.EditValueChanging -= SpinEditRate_EditValueChanging;
        }

        private void SpinEditRate_EditValueChanging(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                if ((e.NewValue?.ToString().EndsWith(".") ?? false) || (e.NewValue?.ToString().EndsWith(",") ?? false))
                    Filter.DefaultRate = e.NewValue.ToString().TrimEnd(new[] { '.', ',' }).ConvertToIntOrNull();
                else
                    e.NewValue.ConvertToIntOrNull();
            }
        }

        private void TextEditAutoUpdate_EditValueChanging(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.AutoUpdateTime = e.NewValue.ConvertToIntOrNull();
            }
        }

        private void TextEditUserPass_EditValueChanging(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.UserPass = e.NewValue?.ToString();
            }
        }

        private void TextEditUserLogin_EditValueChanging(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.UserName = e.NewValue?.ToString();
            }
        }

        private void FilterPage_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = !Close;
            if (!Close)
                Hide();
        }

        private void Min_Changing(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.Min = e.NewValue.ConvertToIntOrNull();
            }
        }

        private void Max_Changing(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.Max = e.NewValue.ConvertToIntOrNull();
            }
        }

        private void MarathonBet_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.MarathonBet = marathonBetToggleSwitch.EditValue.ConvertToBool();
            }
        }

        private void PinnacleSports_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.PinnacleSports = pinnacleSportsToggleSwitch.EditValue.ConvertToBool();
            }
        }

        private void Football_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Football = footballToggleSwitch.EditValue.ConvertToBool();
            }
        }

        private void Basketball_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Basketball = basketballToggleSwitch.EditValue.ConvertToBool();
            }
        }

        private void Volleyball_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Volleyball = volleyballToggleSwitch.EditValue.ConvertToBool();
            }
        }

        private void Hockey_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Hockey = hockeyToggleSwitch.EditValue.ConvertToBool();
            }
        }

        private void Tennis_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Tennis = tennisToggleSwitch.EditValue.ConvertToBool();
            }
        }

        private void Faster_Changing(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.FaterThen = DBTools.ConvertToDateTime(e.NewValue);
            }
        }

        private void Later_Changing(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.LongerThen = DBTools.ConvertToDateTime(e.NewValue);
            }
        }

        private void OutCome2_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.OutCome2 = !Filter.OutCome2;
            }
        }

        private void OutCome3_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.OutCome3 = !Filter.OutCome3;
            }
        }
    }
}
