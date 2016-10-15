using Common.Modules.AntiCaptha;
using DataParser.Enums;
using DataSaver;
using DataSaver.Models;
using DevExpress.XtraGrid;
using FormulasCollection.Enums;
using FormulasCollection.Models;
using FormulasCollection.Realizations;
using SiteAccess.Access;
using SiteAccess.Enums;
using SiteAccess.Model.Bets;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ToolsPortable;

namespace DXApplication1.Pages
{
    public partial class SearchPage : Form
    {
        public EventHandler UpdateEvent;
        public EventHandler CalculatorCall;
        private LocalSaver _localSaver;
        private User _currentUser;
        private TwoOutComeCalculatorFormulas _calculatorFormulas;

        public int DefaultRate { get; set; }

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
            _localSaver = new LocalSaver();
            _calculatorFormulas = new TwoOutComeCalculatorFormulas();

            if (isSearchPage)
                InitSearchPage();
            else
                InitAccountingPage();
        }

        private void InitAccountingPage()
        {
            Text = "Учет";
            Icon = Icon.FromHandle(((Bitmap)imageList1.Images[3]).GetHicon());
        }

        private void InitSearchPage()
        {
            Text = "Поиск вилок";
            Icon = Icon.FromHandle(((Bitmap)imageList1.Images[2]).GetHicon());
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

        private void simpleButton3_Click(object sender,
            EventArgs e)
        {
            //Піннакл
            _currentUser = _localSaver.FindUser();
            if (_currentUser == null)
            {
                MessageBox.Show("Ваш користувач не налаштований правильно, будь-ласка перевірте настройки");
                return;
            }
            var fork = gridView1.GetFocusedRow() as Fork;
            PlacePinnacle(fork);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Марафон
            _currentUser = _localSaver.FindUser();
            if (_currentUser == null)
            {
                MessageBox.Show("Ваш користувач не налаштований правильно, будь-ласка перевірте настройки");
                return;
            }
            var fork = gridView1.GetFocusedRow() as Fork;
            PlaceMarathon(fork);
        }
        private void PlaceMarathon(Fork fork)
        {
            var marath = new MarathonAccess(new AntiGate(_currentUser.AntiGateCode));
            marath.Login(_currentUser.LoginMarathon, _currentUser.PasswordMarathon);

            var recomendedRates = _calculatorFormulas.GetRecommendedRates(DefaultRate,
              fork.CoefFirst.ConvertToDoubleOrNull(),
              fork.CoefSecond.ConvertToDoubleOrNull());
            var bet = new MarathonBet
            {
                Id = fork.MarathonEventId,
                Name = fork.Event,
                // ReSharper disable once PossibleInvalidOperationException
                Stake = recomendedRates.Item1.ConvertToDoubleOrNull().Value,
                AddData = $"{{\"sn\":\"{fork.sn}\"," +
                          $"\"mn\":\"{fork.mn}\"," +
                          $"\"ewc\":\"{fork.ewc}\"," +
                          $"\"cid\":{fork.cid}," +
                          $"\"prt\":\"{fork.prt}\"," +
                          $"\"ewf\":\"{fork.ewf}\"," +
                          $"\"epr\":\"{fork.epr}\"," +
                          "\"prices\"" +
                                $":{{\"0\":\"{fork.prices[0]}\"," +
                                    $"\"1\":\"{fork.prices[1]}\"," +
                                    $"\"2\":\"{fork.prices[2]}\"," +
                                    $"\"3\":\"{fork.prices[3]}\"," +
                                    $"\"4\":\"{fork.prices[4]}\"," + $"\"5\":\"{fork.prices[5]}\"}}}}"
            };
            marath.MakeBet(bet);
            if (fork.Type != ForkType.Saved)
            {
                fork.Type = ForkType.Saved;
                _localSaver.UpdateFork(fork);
            }
        }

        private void PlacePinnacle(Fork fork)
        {
            var pinn = new PinncaleAccess();
            pinn.Login(_currentUser.LoginPinnacle, _currentUser.PasswordPinnacle);

            var recomendedRates = _calculatorFormulas.GetRecommendedRates(DefaultRate,
                fork.CoefFirst.ConvertToDoubleOrNull(),
                fork.CoefSecond.ConvertToDoubleOrNull());
            var bet = new PinnacleBet
            {
                AcceptBetterLine = true,
                BetType = BetType.MONEYLINE,
                Eventid = Convert.ToInt64(fork.PinnacleEventId),
                Guid = Guid.NewGuid().ToString(),
                OddsFormat = OddsFormat.DECIMAL,
                LineId = Convert.ToInt64(fork.LineId),
                /*
                 * This represents the period of the match. For example, for soccer we have:
                 * 0 - Game
                 * 1 - 1st Half
                 * 2 - 2nd Half
                 */
                PeriodNumber = 0,
                WinRiskRate = WinRiskType.WIN,
                // ReSharper disable once PossibleInvalidOperationException
                Stake = recomendedRates.Item2.ConvertToDecimalOrNull().Value,
                SportId = (int)(SportType)Enum.Parse(typeof(SportType), fork.Sport, false)
            };
            pinn.MakeBet(bet);
            if (fork.Type != ForkType.Saved)
            {
                fork.Type = ForkType.Saved;
                _localSaver.UpdateFork(fork);
            }
        }

    }
}