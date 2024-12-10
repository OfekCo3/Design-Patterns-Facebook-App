using System;
using System.Windows.Forms;
using FacebookWrapper;

namespace BasicFacebookFeatures
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Features description + UML diagrams:
        /// https://docs.google.com/document/d/1JbcL19Ocogqeg9kWPLgeQC8ZNlj9XJ6htP016hQlz5c/edit?usp=sharing
        /// </summary>
        [STAThread]
        static void Main()
        {
            Clipboard.SetText("design.patterns20cc");
            FacebookService.s_UseForamttedToStrings = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}