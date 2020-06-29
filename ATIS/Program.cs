using System;
using System.Collections.Generic;
using System.Text;

namespace ATIS.Ui
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            using (new UWPApp.App())
            {
                var app = new ATIS.Ui.App();
                app.InitializeComponent();
                app.Run();
            }


        }
    }
}
