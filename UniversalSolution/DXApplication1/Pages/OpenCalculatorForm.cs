using FormulasCollection.Realizations;
using System;
using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class OpenCalculatorForm : Form
    {
        public Fork SelectedEvent { get; set; }

        public OpenCalculatorForm()
        {
            InitializeComponent();

            //todo maybe this one we should call after load data to GridView
            if (gridView1.RowCount < 0)
                buttonOpen.Enabled = false;
            else
                SelectedEvent = gridView1.GetFocusedRow() as Fork;
            //disable while we not choose some row from GridView
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            //todo make search here
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textEdit1.Text = string.Empty;
            textEdit2.Text = string.Empty;
            textEdit3.Text = string.Empty;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                buttonOpen.Enabled = true;
                SelectedEvent = gridView1.GetFocusedRow() as Fork;
            }
        }
    }
}
