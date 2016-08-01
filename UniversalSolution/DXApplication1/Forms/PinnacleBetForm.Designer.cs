namespace DXApplication1.Forms
{
    partial class PinnacleBetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PinnacleBetForm));
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.textEditBet = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemBet = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditBet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.simpleButtonCancel);
            this.dataLayoutControl1.Controls.Add(this.simpleButtonOk);
            this.dataLayoutControl1.Controls.Add(this.textEditBet);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(202, 72);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemBet,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(202, 72);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // textEditBet
            // 
            this.textEditBet.Location = new System.Drawing.Point(97, 12);
            this.textEditBet.Name = "textEditBet";
            this.textEditBet.Size = new System.Drawing.Size(93, 20);
            this.textEditBet.StyleController = this.dataLayoutControl1;
            this.textEditBet.TabIndex = 4;
            // 
            // layoutControlItemBet
            // 
            this.layoutControlItemBet.Control = this.textEditBet;
            this.layoutControlItemBet.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemBet.Name = "layoutControlItemBet";
            this.layoutControlItemBet.Size = new System.Drawing.Size(182, 24);
            this.layoutControlItemBet.Text = "Цена ставки ($)";
            this.layoutControlItemBet.TextSize = new System.Drawing.Size(81, 13);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonOk.Image")));
            this.simpleButtonOk.Location = new System.Drawing.Point(12, 36);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(84, 22);
            this.simpleButtonOk.StyleController = this.dataLayoutControl1;
            this.simpleButtonOk.TabIndex = 5;
            this.simpleButtonOk.Text = "Поставить";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonOk;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(88, 28);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonCancel.Image")));
            this.simpleButtonCancel.Location = new System.Drawing.Point(100, 36);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(90, 22);
            this.simpleButtonCancel.StyleController = this.dataLayoutControl1;
            this.simpleButtonCancel.TabIndex = 6;
            this.simpleButtonCancel.Text = "Отменить";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonCancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(88, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(94, 28);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // PinnacleBetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 72);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "PinnacleBetForm";
            this.Text = "Pinnacle Ставка";
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditBet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraEditors.TextEdit textEditBet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}