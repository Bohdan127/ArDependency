using DXApplication1.DB;
using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class FilterPage : Form
    {
        public DataSet1 DataSet
        {
            get { return this.dataSet1; }
        }

        public FilterPage()
        {
            InitializeComponent();
        }

        public bool Close { get; set; }

        public FilterPage(DataSet1 dataSet1)
            : this()
        {
            this.dataSet1 = dataSet1;
            InitializeEvents();
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
    }
}
