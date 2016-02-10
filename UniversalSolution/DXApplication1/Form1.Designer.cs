namespace DXApplication1
{
    partial class Form1
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
            DevExpress.XtraBars.Ribbon.ReduceOperation reduceOperation1 = new DevExpress.XtraBars.Ribbon.ReduceOperation();
            this.stylesRibbonPageGroup1 = new DevExpress.XtraSpreadsheet.UI.StylesRibbonPageGroup();
            this.barUpdateNow = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemToggleSwitch1 = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.appMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.iExit = new DevExpress.XtraBars.BarButtonItem();
            this.iHelp = new DevExpress.XtraBars.BarButtonItem();
            this.iAbout = new DevExpress.XtraBars.BarButtonItem();
            this.siStatus = new DevExpress.XtraBars.BarStaticItem();
            this.siInfo = new DevExpress.XtraBars.BarStaticItem();
            this.rgbiSkins = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.fileRibbonPage1 = new DevExpress.XtraSpreadsheet.UI.FileRibbonPage();
            this.homeRibbonPage1 = new DevExpress.XtraSpreadsheet.UI.HomeRibbonPage();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.defaultMatchBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.iDataMatchBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.iDataParserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iDataMatchBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.defaultMatchBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSportname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpp1Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpp2Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colP1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colP2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colX1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colI2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colX2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFora1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFora2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colII = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultMatchBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iDataMatchBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iDataParserBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iDataMatchBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultMatchBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stylesRibbonPageGroup1
            // 
            this.stylesRibbonPageGroup1.ItemLinks.Add(this.barUpdateNow);
            this.stylesRibbonPageGroup1.ItemLinks.Add(this.barEditItem1);
            this.stylesRibbonPageGroup1.ItemLinks.Add(this.barEditItem2);
            this.stylesRibbonPageGroup1.Name = "stylesRibbonPageGroup1";
            this.stylesRibbonPageGroup1.Text = "Обновления";
            // 
            // barUpdateNow
            // 
            this.barUpdateNow.Caption = "Обновить сейчас";
            this.barUpdateNow.Id = 273;
            this.barUpdateNow.ImageUri.Uri = "Refresh";
            this.barUpdateNow.Name = "barUpdateNow";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "Автообновления          ";
            this.barEditItem1.Edit = this.repositoryItemToggleSwitch1;
            this.barEditItem1.EditHeight = 24;
            this.barEditItem1.EditWidth = 70;
            this.barEditItem1.Id = 278;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemToggleSwitch1
            // 
            this.repositoryItemToggleSwitch1.AutoHeight = false;
            this.repositoryItemToggleSwitch1.Name = "repositoryItemToggleSwitch1";
            this.repositoryItemToggleSwitch1.OffText = "Off";
            this.repositoryItemToggleSwitch1.OnText = "On";
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "Обновление каждые(мин)";
            this.barEditItem2.Edit = this.repositoryItemTextEdit1;
            this.barEditItem2.Id = 324;
            this.barEditItem2.Name = "barEditItem2";
            this.barEditItem2.EditValueChanged += new System.EventHandler(this.ChangeUpdateTime);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Mask.EditMask = "[1-9]";
            this.repositoryItemTextEdit1.Mask.IgnoreMaskBlank = false;
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEdit1.Mask.ShowPlaceHolders = false;
            this.repositoryItemTextEdit1.MaxLength = 1;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.NullText = "1";
            // 
            // appMenu
            // 
            this.appMenu.Name = "appMenu";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 673);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1100, 27);
            // 
            // ribbonControl
            // 
            this.ribbonControl.ApplicationButtonDropDownControl = this.appMenu;
            this.ribbonControl.ApplicationButtonText = null;
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.iExit,
            this.iHelp,
            this.iAbout,
            this.siStatus,
            this.siInfo,
            this.rgbiSkins,
            this.barUpdateNow,
            this.barEditItem1,
            this.barEditItem2});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 325;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.PageHeaderItemLinks.Add(this.iAbout);
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.fileRibbonPage1,
            this.homeRibbonPage1,
            this.ribbonPage1});
            this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemToggleSwitch1,
            this.repositoryItemTextEdit1});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl.Size = new System.Drawing.Size(1100, 141);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.Toolbar.ItemLinks.Add(this.iHelp);
            // 
            // iExit
            // 
            this.iExit.Caption = "Exit";
            this.iExit.Description = "Closes this program after prompting you to save unsaved data.";
            this.iExit.Hint = "Closes this program after prompting you to save unsaved data";
            this.iExit.Id = 20;
            this.iExit.ImageIndex = 6;
            this.iExit.LargeImageIndex = 6;
            this.iExit.Name = "iExit";
            // 
            // iHelp
            // 
            this.iHelp.Caption = "Help";
            this.iHelp.Description = "Start the program help system.";
            this.iHelp.Hint = "Start the program help system";
            this.iHelp.Id = 22;
            this.iHelp.ImageIndex = 7;
            this.iHelp.LargeImageIndex = 7;
            this.iHelp.Name = "iHelp";
            // 
            // iAbout
            // 
            this.iAbout.Caption = "About";
            this.iAbout.Description = "Displays general program information.";
            this.iAbout.Hint = "Displays general program information";
            this.iAbout.Id = 24;
            this.iAbout.ImageIndex = 8;
            this.iAbout.LargeImageIndex = 8;
            this.iAbout.Name = "iAbout";
            // 
            // siStatus
            // 
            this.siStatus.Caption = "Some Status Info";
            this.siStatus.Id = 31;
            this.siStatus.Name = "siStatus";
            this.siStatus.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // siInfo
            // 
            this.siInfo.Caption = "Some Info";
            this.siInfo.Id = 32;
            this.siInfo.Name = "siInfo";
            this.siInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // rgbiSkins
            // 
            this.rgbiSkins.Caption = "Skins";
            // 
            // 
            // 
            this.rgbiSkins.Gallery.AllowHoverImages = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rgbiSkins.Gallery.ColumnCount = 4;
            this.rgbiSkins.Gallery.FixedHoverImageSize = false;
            this.rgbiSkins.Gallery.ImageSize = new System.Drawing.Size(32, 17);
            this.rgbiSkins.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
            this.rgbiSkins.Gallery.RowCount = 4;
            this.rgbiSkins.Id = 60;
            this.rgbiSkins.Name = "rgbiSkins";
            // 
            // fileRibbonPage1
            // 
            this.fileRibbonPage1.Name = "fileRibbonPage1";
            this.fileRibbonPage1.Text = "ValeoBet";
            // 
            // homeRibbonPage1
            // 
            this.homeRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.stylesRibbonPageGroup1});
            this.homeRibbonPage1.Name = "homeRibbonPage1";
            reduceOperation1.Behavior = DevExpress.XtraBars.Ribbon.ReduceOperationBehavior.UntilAvailable;
            reduceOperation1.Group = this.stylesRibbonPageGroup1;
            reduceOperation1.ItemLinkIndex = 2;
            reduceOperation1.ItemLinksCount = 0;
            reduceOperation1.Operation = DevExpress.XtraBars.Ribbon.ReduceOperationType.Gallery;
            this.homeRibbonPage1.ReduceOperations.Add(reduceOperation1);
            this.homeRibbonPage1.Text = "Настройки";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Учет";
            // 
            // defaultMatchBindingSource1
            // 
            this.defaultMatchBindingSource1.DataSource = typeof(DataParser.DefaultRealization.DefaultMatch);
            // 
            // iDataMatchBindingSource1
            // 
            this.iDataMatchBindingSource1.DataSource = typeof(DataParser.Interfaces.IDataMatch);
            // 
            // iDataParserBindingSource
            // 
            this.iDataParserBindingSource.DataSource = typeof(DataParser.Interfaces.IDataParser);
            // 
            // iDataMatchBindingSource
            // 
            this.iDataMatchBindingSource.DataSource = typeof(DataParser.Interfaces.IDataMatch);
            // 
            // defaultMatchBindingSource
            // 
            this.defaultMatchBindingSource.DataSource = typeof(DataParser.DefaultRealization.DefaultMatch);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.defaultMatchBindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.ribbonControl;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1100, 228);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colSportname,
            this.colChamp,
            this.colOpp1Name,
            this.colOpp2Name,
            this.colP1,
            this.colX,
            this.colP2,
            this.colX1,
            this.colI2,
            this.colX2,
            this.colFora1,
            this.colI,
            this.colFora2,
            this.colII,
            this.colTotal,
            this.colB,
            this.colM});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            // 
            // colSportname
            // 
            this.colSportname.FieldName = "Sportname";
            this.colSportname.Name = "colSportname";
            this.colSportname.Visible = true;
            this.colSportname.VisibleIndex = 1;
            // 
            // colChamp
            // 
            this.colChamp.FieldName = "Champ";
            this.colChamp.Name = "colChamp";
            this.colChamp.Visible = true;
            this.colChamp.VisibleIndex = 2;
            // 
            // colOpp1Name
            // 
            this.colOpp1Name.FieldName = "Opp1Name";
            this.colOpp1Name.Name = "colOpp1Name";
            this.colOpp1Name.Visible = true;
            this.colOpp1Name.VisibleIndex = 3;
            // 
            // colOpp2Name
            // 
            this.colOpp2Name.FieldName = "Opp2Name";
            this.colOpp2Name.Name = "colOpp2Name";
            this.colOpp2Name.Visible = true;
            this.colOpp2Name.VisibleIndex = 4;
            // 
            // colP1
            // 
            this.colP1.FieldName = "P1";
            this.colP1.Name = "colP1";
            this.colP1.Visible = true;
            this.colP1.VisibleIndex = 5;
            // 
            // colX
            // 
            this.colX.FieldName = "X";
            this.colX.Name = "colX";
            this.colX.Visible = true;
            this.colX.VisibleIndex = 6;
            // 
            // colP2
            // 
            this.colP2.FieldName = "P2";
            this.colP2.Name = "colP2";
            this.colP2.Visible = true;
            this.colP2.VisibleIndex = 7;
            // 
            // colX1
            // 
            this.colX1.FieldName = "X1";
            this.colX1.Name = "colX1";
            this.colX1.Visible = true;
            this.colX1.VisibleIndex = 8;
            // 
            // colI2
            // 
            this.colI2.FieldName = "I2";
            this.colI2.Name = "colI2";
            this.colI2.Visible = true;
            this.colI2.VisibleIndex = 9;
            // 
            // colX2
            // 
            this.colX2.FieldName = "X2";
            this.colX2.Name = "colX2";
            this.colX2.Visible = true;
            this.colX2.VisibleIndex = 10;
            // 
            // colFora1
            // 
            this.colFora1.FieldName = "Fora1";
            this.colFora1.Name = "colFora1";
            this.colFora1.Visible = true;
            this.colFora1.VisibleIndex = 11;
            // 
            // colI
            // 
            this.colI.FieldName = "I";
            this.colI.Name = "colI";
            this.colI.Visible = true;
            this.colI.VisibleIndex = 12;
            // 
            // colFora2
            // 
            this.colFora2.FieldName = "Fora2";
            this.colFora2.Name = "colFora2";
            this.colFora2.Visible = true;
            this.colFora2.VisibleIndex = 13;
            // 
            // colII
            // 
            this.colII.FieldName = "II";
            this.colII.Name = "colII";
            this.colII.Visible = true;
            this.colII.VisibleIndex = 14;
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 15;
            // 
            // colB
            // 
            this.colB.FieldName = "B";
            this.colB.Name = "colB";
            this.colB.Visible = true;
            this.colB.VisibleIndex = 16;
            // 
            // colM
            // 
            this.colM.FieldName = "M";
            this.colM.Name = "colM";
            this.colM.Visible = true;
            this.colM.VisibleIndex = 17;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1100, 300);
            this.webBrowser1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 141);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webBrowser1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1100, 532);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ribbonControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultMatchBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iDataMatchBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iDataParserBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iDataMatchBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultMatchBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.ApplicationMenu appMenu;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private System.Windows.Forms.BindingSource iDataMatchBindingSource;
        private System.Windows.Forms.BindingSource iDataParserBindingSource;
        private System.Windows.Forms.BindingSource iDataMatchBindingSource1;
        private System.Windows.Forms.BindingSource defaultMatchBindingSource1;
        private System.Windows.Forms.BindingSource defaultMatchBindingSource;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.BarButtonItem iExit;
        private DevExpress.XtraBars.BarButtonItem iHelp;
        private DevExpress.XtraBars.BarButtonItem iAbout;
        private DevExpress.XtraBars.BarStaticItem siStatus;
        private DevExpress.XtraBars.BarStaticItem siInfo;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
        private DevExpress.XtraBars.BarButtonItem barUpdateNow;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch repositoryItemToggleSwitch1;
        private DevExpress.XtraSpreadsheet.UI.FileRibbonPage fileRibbonPage1;
        private DevExpress.XtraSpreadsheet.UI.HomeRibbonPage homeRibbonPage1;
        private DevExpress.XtraSpreadsheet.UI.StylesRibbonPageGroup stylesRibbonPageGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colSportname;
        private DevExpress.XtraGrid.Columns.GridColumn colChamp;
        private DevExpress.XtraGrid.Columns.GridColumn colOpp1Name;
        private DevExpress.XtraGrid.Columns.GridColumn colOpp2Name;
        private DevExpress.XtraGrid.Columns.GridColumn colP1;
        private DevExpress.XtraGrid.Columns.GridColumn colX;
        private DevExpress.XtraGrid.Columns.GridColumn colP2;
        private DevExpress.XtraGrid.Columns.GridColumn colX1;
        private DevExpress.XtraGrid.Columns.GridColumn colI2;
        private DevExpress.XtraGrid.Columns.GridColumn colX2;
        private DevExpress.XtraGrid.Columns.GridColumn colFora1;
        private DevExpress.XtraGrid.Columns.GridColumn colI;
        private DevExpress.XtraGrid.Columns.GridColumn colFora2;
        private DevExpress.XtraGrid.Columns.GridColumn colII;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colB;
        private DevExpress.XtraGrid.Columns.GridColumn colM;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}
