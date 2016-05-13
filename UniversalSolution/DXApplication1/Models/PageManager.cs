using DataSaver.Models;
using DXApplication1.Pages;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1.Models
{
    public class PageManager
    {
        #region Members

        private FilterPage _filterPage;
        private AccountingPage _accountingPage;
        private SearchPage _searchPage;
        private CalculatorPage _calculatorPage;
        private Form _defaultMdiParent;
        private OpenCalculatorForm _openCalculatorForm;
        public DataManager DataManager { get; set; }
        private Timer timer;

        #endregion Members

        #region CTOR

        public PageManager(Form mdiParent, IForkFormulas forkFormulas)
        {
            _defaultMdiParent = mdiParent;
            DataManager = new DataManager(forkFormulas);
            timer = new Timer();
            timer.Interval = 20 * 1000;//default for 20 seconds
            timer.Tick += Timer_Tick;
        }

        #endregion CTOR

        #region Functions

        public void CloseAllPages()
        {
            _accountingPage = null;
            _calculatorPage = null;
            _filterPage = null;
        }

        /// <summary>
        /// This Control always reloaded in each call.
        /// </summary>
        public async Task<OpenCalculatorForm> CreateCalculatorForm(bool useMdiParent = false, Form mdiParent = null, bool loadData = true)
        {
            if (loadData) _openCalculatorForm = new OpenCalculatorForm(await DataManager.
                 GetForksForAllSportsAsync(_filterPage?.Filter).ConfigureAwait(true));
            else
                _openCalculatorForm = new OpenCalculatorForm();

            _openCalculatorForm.MdiParent = useMdiParent ? mdiParent ?? _defaultMdiParent : null;

            return _openCalculatorForm;
        }

        public FilterPage GetFilterPage(Filter _filter, Form mdiParent = null, bool reload = false)
        {
            if (_filter == null) _filter = new Filter();

            if (_filterPage == null)
            {
                _filterPage = new FilterPage(_filter);
                _filterPage.MdiParent = mdiParent ?? _defaultMdiParent;
                _filterPage.Close = false;


            }
            return _filterPage;
        }

        public CalculatorPage GetCalculatorPage(Form mdiParent = null, bool reload = false)
        {
            if (_calculatorPage == null)
            {
                _calculatorPage = new CalculatorPage(new TwoOutComeCalculatorFormulas(), _filterPage?.Filter);
                _calculatorPage.MdiParent = mdiParent ?? _defaultMdiParent;
                _calculatorPage.Close = false;
            }
            return _calculatorPage;
        }

        public AccountingPage GetAccountPage(Form mdiParent = null, bool reload = false)
        {
            if (_accountingPage == null || reload)
            {
                _accountingPage = new AccountingPage();
                _accountingPage.MdiParent = mdiParent ?? _defaultMdiParent;
                _accountingPage.Close = false;
                _accountingPage.Update += AccountPage_Update;
                _accountingPage.CalculatorCall += AccountPage_CalculatorCall;
            }
            return _accountingPage;
        }

        public SearchPage GetSearchPage(Form mdiParent = null, bool reload = false)
        {
            if (_searchPage == null || reload)
            {
                _searchPage = new SearchPage();
                _searchPage.MdiParent = mdiParent ?? _defaultMdiParent;
                _searchPage.Close = false;
                _searchPage.Update += SearchPage_Update;
                _searchPage.CalculatorCall += AccountPage_CalculatorCall;//can be the same as for account page
            }

            if (!timer.Enabled)
            {
                //todo just for first time, but this is bad way
                SearchPage_Update(null, null);
                if (_filterPage.Filter.AutoUpdateTime != null)
                    timer.Interval = _filterPage.Filter.AutoUpdateTime.Value * 1000;
                timer.Start();
            }

            return _searchPage;
        }

        #endregion Functions

        #region Events

        private void Timer_Tick(object sender, EventArgs e)
        {
            SearchPage_Update(sender, e);
        }

        private void AccountPage_Update(object sender, EventArgs e)
        {

        }

        private async void AccountPage_CalculatorCall(object sender, EventArgs eventArgs)
        {
            if (sender is Fork)
            {
                GetCalculatorPage(reload: true).Fork = (Fork)sender;
                GetCalculatorPage().Show();
            }
        }

        private async void SearchPage_Update(object sender, EventArgs e)
        {
            _searchPage.StartLoading();
            var resList = await DataManager.
                GetForksForAllSportsAsync(_filterPage?.Filter).ConfigureAwait(true);
            _searchPage.MainGridControl.DataSource = null;
            _searchPage.MainGridControl.DataSource = resList;
            _searchPage.MainGridControl.Refresh();
            _searchPage.EndLoading();
        }

        #endregion Events
    }
}