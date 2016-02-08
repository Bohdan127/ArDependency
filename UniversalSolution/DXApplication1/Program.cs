using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataColector.DefaultRealization;
using DataColector.Interfaces;
using DataParser.DefaultRealization;
using DataParser.Interfaces;
using DataSaver.DefaultRealization;
using DataSaver.Interfaces;
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
                   

            var containerMain = new WindsorContainer();
            containerMain.Register(Component.For<Form1>());
            containerMain.Register(Component.For<IDataColector>().ImplementedBy<DefaultDataColector>());
            containerMain.Register(Component.For<IDataMatch>().ImplementedBy<DefaultMatch>());
            containerMain.Register(Component.For<IDataParser>().ImplementedBy<DefaultDataParser>());
            containerMain.Register(Component.For<IDataSaver>().ImplementedBy<DefaultDataSaver>());

            // CREATE THE MAIN OBJECT AND INVOKE ITS METHOD(S) AS DESIRED.
            var mainThing = containerMain.Resolve<Form1>();

            Application.Run(mainThing);
        }
    }
}