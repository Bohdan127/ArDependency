using DataParser.MY;
using DXApplication1.Pages;
using FormulasCollection.Interfaces;
using FormulasCollection.Realizations;
using System;
using System.Diagnostics.Contracts;
using System.Windows.Forms;

namespace DXApplication1.Models
{
    public class PageManager
    {
        #region Members

        private FilterPage _filterPage;
        private AccountingPage _accountingPage;
        private CalculatorPage _calculatorPage;
        private IForkFormulas _forkFormulas;
        private Form _defaultMdiParent;

        #endregion

        #region CTOR

        public PageManager(Form mdiParent, IForkFormulas forkFormulas)
        {
            _defaultMdiParent = mdiParent;
            _forkFormulas = forkFormulas;
        }

        #endregion

        #region Functions

        public void CloseAllPages()
        {
            _accountingPage = null;
            _calculatorPage = null;
            _filterPage = null;
        }

        public FilterPage GetFilterPage(Filter _filter, Form mdiParent = null, bool reload = false)
        {
            Contract.Requires(_filter != null);

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
                _calculatorPage = new CalculatorPage(new TwoOutComeCalculatorFormulas());
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
        #endregion

        #region Events
        private void AccountPage_CalculatorCall(object sender, EventArgs eventArgs)
        {
            Contract.Requires(sender != null);

            //  if(sender == null)
            //      return;

            if (sender is Fork)
            {
                GetCalculatorPage(reload: true).Fork = (Fork)sender;
                GetCalculatorPage().Show();
            }
        }

        private async void AccountPage_Update(object sender, EventArgs e)
        {
            ParsePinnacle.SportType type;

            if (_filterPage == null) return;

            Filter filter = _filterPage.Filter;

            if (filter.Basketball)
                type = ParsePinnacle.SportType.Basketball;
            else if (filter.Football)
                type = ParsePinnacle.SportType.Football;
            else if (filter.Hockey)
                type = ParsePinnacle.SportType.Hockey;
            else if (filter.Tennis)
                type = ParsePinnacle.SportType.Tennis;
            else if (filter.Volleyball)
                type = ParsePinnacle.SportType.Volleyball;

            else
                return;

            _accountingPage.MainGridControl.DataSource = await _forkFormulas.GetAllForksAsync(await new ParsePinnacle().GetNameTeamsAndDateAsync(type).ConfigureAwait(true)).ConfigureAwait(true);
            _accountingPage.Refresh();
        }
        #endregion
    }
}
