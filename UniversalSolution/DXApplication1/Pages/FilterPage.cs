using System.Windows.Forms;

namespace DXApplication1.Pages
{
    public partial class FilterPage : Form
    {
        public FilterPage()
        {
            InitializeComponent();

            //default settings
            this.dataLayoutControl1.OptionsCustomizationForm.ShowLoadButton = false;
            this.dataLayoutControl1.OptionsCustomizationForm.ShowPropertyGrid = true;
            this.dataLayoutControl1.OptionsCustomizationForm.ShowSaveButton = false;
            this.dataLayoutControl1.OptionsSerialization.RestoreAppearanceItemCaption = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreAppearanceTabPage = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreGroupPadding = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreGroupSpacing = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreLayoutGroupAppearanceGroup = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreLayoutItemPadding = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreLayoutItemSpacing = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreRootGroupPadding = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreRootGroupSpacing = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreTabbedGroupPadding = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreTabbedGroupSpacing = true;
            this.dataLayoutControl1.OptionsSerialization.RestoreTextToControlDistance = true;
        }
    }
}
