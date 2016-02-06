using DevExpress.LookAndFeel;
using System;
using System.Windows.Forms;

namespace DXApplication1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            //var container = new WindsorContainer();
            //container.Register(Component.For<Form1>());
            //container.Register(Component.For<IDataColector>().ImplementedBy<DefaultDataColector>());
            //container.Register(Component.For<IDataParser>().ImplementedBy<DefaultDataParser>());
            //container.Register(Component.For<IDataSaver>().ImplementedBy<DefaultDataSaver>());

            //// CREATE THE MAIN OBJECT AND INVOKE ITS METHOD(S) AS DESIRED.
            //var mainThing = container.Resolve<Form1>();

            Application.Run(new Form1());
        }
    }
}