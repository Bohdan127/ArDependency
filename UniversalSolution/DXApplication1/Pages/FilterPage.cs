using DataSaver;
using DataSaver.Models;
using DevExpress.XtraEditors.Controls;
using FormulasCollection.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using ToolsPortable;

namespace DXApplication1.Pages
{
    public partial class FilterPage : Form
    {
        public Filter Filter { get; private set; }

        public LocalSaver LocalSaver { get; private set; }

        private string _userId;

        public bool ToClose { get; set; }

        public EventHandler FilterUpdated;

        public FilterPage(Filter filter)
        {
            InitializeComponent(); LocalSaver = new LocalSaver();
            FilterUpdated += (sender, args) => { if (Filter != null) LocalSaver.UpdateFilter(Filter); };
            Filter = filter;
            FirstBind();
            UserBind();
            InitializeEvents();
        }

        private void UserBind()
        {
            var user = LocalSaver.FindUser();
            _userId = user?.Id;
            textEditLoginPinnacle.Text = user?.LoginPinnacle;
            textEditPasswordPinnacle.Text = user?.PasswordPinnacle;
            textEditLoginMarathon.Text = user?.LoginMarathon;
            textEditPasswordMarathon.Text = user?.PasswordMarathon;
            textEditAntiGateCode.Text = user?.AntiGateCode;
        }

        protected void FirstBind()
        {
            lock (Filter)
            {
                minTextEdit.EditValue = Filter.Min;
                maxTextEdit.EditValue = Filter.Max;
                footballToggleSwitch.EditValue = Filter.Football;
                basketballToggleSwitch.EditValue = Filter.Basketball;
                volleyballToggleSwitch.EditValue = Filter.Volleyball;
                hockeyToggleSwitch.EditValue = Filter.Hockey;
                tennisToggleSwitch.EditValue = Filter.Tennis;
                fasterDateTimePicker.EditValue = Filter.FaterThen;
                longerDateTimePicker.EditValue = Filter.LongerThen;
                textEditAutoUpdate.EditValue = Filter.AutoUpdateTime;
                textEditMinMarBet.EditValue = Filter.MinMarBet;
                textEditMinPinBet.EditValue = Filter.MinPinBet;
            }
        }

        public void InitializeEvents()
        {
            Closing += FilterPage_Closing;
            fasterDateTimePicker.EditValueChanging += Faster_Changing;
            maxTextEdit.EditValueChanging += Max_Changing;
            minTextEdit.EditValueChanging += Min_Changing;
            basketballToggleSwitch.Toggled += Basketball_Toggled;
            footballToggleSwitch.Toggled += Football_Toggled;
            longerDateTimePicker.EditValueChanging += Later_Changing;
            volleyballToggleSwitch.Toggled += Volleyball_Toggled;
            tennisToggleSwitch.Toggled += Tennis_Toggled;
            hockeyToggleSwitch.Toggled += Hockey_Toggled;
            textEditAutoUpdate.EditValueChanging += TextEditAutoUpdate_EditValueChanging;
            textEditAutoUpdate.EditValueChanging += TextEditAutoUpdate_EditValueChanging;
            textEditAutoUpdate.EditValueChanging += TextEditAutoUpdate_EditValueChanging;
            textEditLoginPinnacle.EditValueChanged += User_EditValueChanged;
            textEditPasswordPinnacle.EditValueChanged += User_EditValueChanged;
            textEditLoginMarathon.EditValueChanged += User_EditValueChanged;
            textEditPasswordMarathon.EditValueChanged += User_EditValueChanged;
            textEditAntiGateCode.EditValueChanged += User_EditValueChanged;
            textEditMinMarBet.EditValueChanging += TextEditMinMarBet_EditValueChanging;
            textEditMinPinBet.EditValueChanging += TextEditMinPinBet_EditValueChanging;
        }


        public void DeInitializeEvents()
        {
            Closing -= FilterPage_Closing;
            fasterDateTimePicker.EditValueChanging -= Faster_Changing;
            maxTextEdit.EditValueChanging -= Max_Changing;
            minTextEdit.EditValueChanging -= Min_Changing;
            basketballToggleSwitch.Toggled -= Basketball_Toggled;
            footballToggleSwitch.Toggled -= Football_Toggled;
            longerDateTimePicker.EditValueChanging -= Later_Changing;
            volleyballToggleSwitch.Toggled -= Volleyball_Toggled;
            tennisToggleSwitch.Toggled -= Tennis_Toggled;
            hockeyToggleSwitch.Toggled -= Hockey_Toggled;
            textEditLoginPinnacle.EditValueChanged -= User_EditValueChanged;
            textEditPasswordPinnacle.EditValueChanged -= User_EditValueChanged;
            textEditLoginMarathon.EditValueChanged -= User_EditValueChanged;
            textEditPasswordMarathon.EditValueChanged -= User_EditValueChanged;
            textEditAntiGateCode.EditValueChanging -= User_EditValueChanged;
            textEditMinMarBet.EditValueChanging -= TextEditMinMarBet_EditValueChanging;
            textEditMinPinBet.EditValueChanging -= TextEditMinPinBet_EditValueChanging;
        }

        private void TextEditMinPinBet_EditValueChanging(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.MinPinBet = e.NewValue.ConvertToDecimalOrNull();
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void TextEditMinMarBet_EditValueChanging(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.MinMarBet = e.NewValue.ConvertToDecimalOrNull();
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void User_EditValueChanged(object sender, EventArgs e)
        {
            var user = new User
            {
                Id = _userId,
                LoginPinnacle = textEditLoginPinnacle.Text,
                PasswordPinnacle = textEditPasswordPinnacle.Text,
                LoginMarathon = textEditLoginMarathon.Text,
                PasswordMarathon = textEditPasswordMarathon.Text,
                AntiGateCode = textEditAntiGateCode.Text
            };
            LocalSaver.UpdateUser(user);
        }

        private void TextEditAutoUpdate_EditValueChanging(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.AutoUpdateTime = e.NewValue.ConvertToIntOrNull();
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void FilterPage_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = !ToClose;
            if (!ToClose)
                Hide();
        }

        private void Min_Changing(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.Min = e.NewValue.ConvertToDecimalOrNull();
                e.Cancel = Filter.Min != null
                           && Filter.Max != null
                           && ((Filter.Max.Value != 0
                                && Filter.Max < Filter.Min)
                               || Filter.Min.Value < 0);
                if (!e.Cancel) FilterUpdated?.Invoke(sender, e);
            }
        }

        private void Max_Changing(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                Filter.Max = e.NewValue.ConvertToDecimalOrNull();
                e.Cancel = Filter.Min != null
                           && Filter.Max != null
                           && ((Filter.Min.Value != 0
                                && Filter.Min > Filter.Max)
                               || Filter.Max.Value < 0);if (!e.Cancel) FilterUpdated?.Invoke(sender, e);
            }
        }

        private void Football_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Football = footballToggleSwitch.EditValue.ConvertToBool();
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void Basketball_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Basketball = basketballToggleSwitch.EditValue.ConvertToBool();
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void Volleyball_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Volleyball = volleyballToggleSwitch.EditValue.ConvertToBool();
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void Hockey_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Hockey = hockeyToggleSwitch.EditValue.ConvertToBool();
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void Tennis_Toggled(object sender, EventArgs e)
        {
            lock (Filter)
            {
                Filter.Tennis = tennisToggleSwitch.EditValue.ConvertToBool();
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void Faster_Changing(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                DateTime dateValue;
                if (e.NewValue == null)
                    Filter.FaterThen = null;
                else if (DateTime.TryParse(e.NewValue?.ToString(), out dateValue))
                {
                    Filter.FaterThen = dateValue;
                }
                FilterUpdated?.Invoke(sender, e);
            }
        }

        private void Later_Changing(object sender, ChangingEventArgs e)
        {
            lock (Filter)
            {
                DateTime dateValue;
                if (e.NewValue == null)
                    Filter.LongerThen = null;
                else if (DateTime.TryParse(e.NewValue?.ToString(), out dateValue))
                {
                    Filter.LongerThen = dateValue;
                }
                FilterUpdated?.Invoke(sender, e);
            }
        }
    }
}