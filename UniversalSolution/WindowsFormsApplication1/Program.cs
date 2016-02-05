using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataColector;
using DataParser;
using DataSaver;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new WindsorContainer();
            container.Register(Component.For<Form1>());
            container.Register(Component.For<IDataColector>().ImplementedBy<DefaultDataColector>());
            container.Register(Component.For<IDataParser>().ImplementedBy<DefaultDataParser>());
            container.Register(Component.For<IDataSaver>().ImplementedBy<DefaultDataSaver>());

            // CREATE THE MAIN OBJECT AND INVOKE ITS METHOD(S) AS DESIRED.    
            Application.Run(container.Resolve<Form1>());
        }
    }
}
