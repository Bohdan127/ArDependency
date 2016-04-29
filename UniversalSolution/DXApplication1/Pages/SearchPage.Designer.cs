namespace DXApplication1.Pages
{
    partial class SearchPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchPage));
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDateAndTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSportType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTeams = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookmaker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.colCoef1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookmaker2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCoef2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.progressBarControl1);
            this.dataLayoutControl1.Controls.Add(this.simpleButton1);
            this.dataLayoutControl1.Controls.Add(this.gridControl1);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(643, 425);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(24, 363);
            this.progressBarControl1.MinimumSize = new System.Drawing.Size(30, 38);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(464, 38);
            this.progressBarControl1.StyleController = this.dataLayoutControl1;
            this.progressBarControl1.TabIndex = 12;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(492, 363);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(127, 38);
            this.simpleButton1.StyleController = this.dataLayoutControl1;
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "Калькулятор";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(24, 42);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(595, 317);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDateAndTime,
            this.colSportType,
            this.colTeams,
            this.colBookmaker,
            this.colRate1,
            this.colCoef1,
            this.colBookmaker2,
            this.colRate2,
            this.colCoef2,
            this.colProfit});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colDateAndTime
            // 
            this.colDateAndTime.Caption = "Дата и время события";
            this.colDateAndTime.FieldName = "DateAndTime";
            this.colDateAndTime.Name = "colDateAndTime";
            this.colDateAndTime.OptionsColumn.ReadOnly = true;
            this.colDateAndTime.Visible = true;
            this.colDateAndTime.VisibleIndex = 0;
            // 
            // colSportType
            // 
            this.colSportType.Caption = "Вид спорта";
            this.colSportType.FieldName = "SportType";
            this.colSportType.Name = "colSportType";
            this.colSportType.OptionsColumn.ReadOnly = true;
            this.colSportType.Visible = true;
            this.colSportType.VisibleIndex = 1;
            // 
            // colTeams
            // 
            this.colTeams.Caption = "Команды и лига";
            this.colTeams.FieldName = "Teams";
            this.colTeams.Name = "colTeams";
            this.colTeams.OptionsColumn.ReadOnly = true;
            this.colTeams.Visible = true;
            this.colTeams.VisibleIndex = 2;
            // 
            // colBookmaker
            // 
            this.colBookmaker.Caption = "Букмекер 1";
            this.colBookmaker.FieldName = "BookmakerFirst";
            this.colBookmaker.Name = "colBookmaker";
            this.colBookmaker.OptionsColumn.ReadOnly = true;
            this.colBookmaker.Visible = true;
            this.colBookmaker.VisibleIndex = 3;
            // 
            // colRate1
            // 
            this.colRate1.Caption = "Тип ставки 1";
            this.colRate1.FieldName = "RateFirst";
            this.colRate1.Name = "colRate1";
            this.colRate1.OptionsColumn.ReadOnly = true;
            this.colRate1.Visible = true;
            this.colRate1.VisibleIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(643, 425);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem9});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(623, 405);
            this.layoutControlGroup3.Text = "Данные";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton1;
            this.layoutControlItem3.Location = new System.Drawing.Point(468, 321);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(131, 42);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(599, 321);
            this.layoutControlItem1.Text = "Грид с данными из сайтов";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.progressBarControl1;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 321);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(468, 42);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CalculateSheet_32x32.png");
            this.imageList1.Images.SetKeyName(1, "CalculateNow_32x32.png");
            // 
            // colCoef1
            // 
            this.colCoef1.Caption = "Коэффициент 1";
            this.colCoef1.FieldName = "CoefFirst";
            this.colCoef1.Name = "colCoef1";
            this.colCoef1.Visible = true;
            this.colCoef1.VisibleIndex = 5;
            // 
            // colBookmaker2
            // 
            this.colBookmaker2.Caption = "Букмекер 2";
            this.colBookmaker2.FieldName = "BookmakerSecond";
            this.colBookmaker2.Name = "colBookmaker2";
            this.colBookmaker2.Visible = true;
            this.colBookmaker2.VisibleIndex = 6;
            // 
            // colRate2
            // 
            this.colRate2.Caption = "Тип ставки 2";
            this.colRate2.FieldName = "RateSecond";
            this.colRate2.Name = "colRate2";
            this.colRate2.Visible = true;
            this.colRate2.VisibleIndex = 7;
            // 
            // colCoef2
            // 
            this.colCoef2.Caption = "Коэффициент 2";
            this.colCoef2.FieldName = "CoefSecond";
            this.colCoef2.Name = "colCoef2";
            this.colCoef2.Visible = true;
            this.colCoef2.VisibleIndex = 8;
            // 
            // colProfit
            // 
            this.colProfit.Caption = "Прибыль";
            this.colProfit.FieldName = "Profit";
            this.colProfit.Name = "colProfit";
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 9;
            // 
            // SearchPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 425);
            this.Controls.Add(this.dataLayoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchPage";
            this.Text = "Учет";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colDateAndTime;
        private DevExpress.XtraGrid.Columns.GridColumn colTeams;
        private DevExpress.XtraGrid.Columns.GridColumn colSportType;
        private DevExpress.XtraGrid.Columns.GridColumn colBookmaker;
        private DevExpress.XtraGrid.Columns.GridColumn colRate1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraGrid.Columns.GridColumn colCoef1;
        private DevExpress.XtraGrid.Columns.GridColumn colBookmaker2;
        private DevExpress.XtraGrid.Columns.GridColumn colRate2;
        private DevExpress.XtraGrid.Columns.GridColumn colCoef2;
        private DevExpress.XtraGrid.Columns.GridColumn colProfit;
    }
}