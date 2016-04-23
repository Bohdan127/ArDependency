using FormulasCollection.Realizations;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class OpenCalculatorForm : Form
    {
        public Fork SelectedEvent { get; set; }
        private List<Fork> dataSource;

        public OpenCalculatorForm()
        {
            InitializeComponent();

            dataSource = new List<Fork>();
        }

        public OpenCalculatorForm(List<Fork> dataList)
            : this()
        {
            dataSource.AddRange(dataList);
            gridControl1.DataSource = dataList;

            if (gridView1.RowCount < 0)
                buttonOpen.Enabled = false;
            else
            {
                gridView1.FocusedRowHandle = 0;
                SelectedEvent = gridView1.GetFocusedRow() as Fork;
            }
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
