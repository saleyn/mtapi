using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace TestApiClientUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Form1();
            Application.ThreadException += delegate(object sender, ThreadExceptionEventArgs e)
            {
              var frame = new StackFrame(1, true);
              var m = frame.GetMethod();
              var from = m.DeclaringType.FullName;
              var file = Path.GetFileName(frame.GetFileName());
              var line = frame.GetFileLineNumber();
              var msg  = e.Exception.Message;
              MessageBox.Show(mainForm.Owner, $"Error in {file}:{line}:{from}\n${msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs e)
            {
              var frame = new StackFrame(1, true);
              var m = frame.GetMethod();
              var from = m.DeclaringType.FullName;
              var file = Path.GetFileName(frame.GetFileName());
              var line = frame.GetFileLineNumber();
              var msg  = (e.ExceptionObject as Exception)?.Message ?? "";
              MessageBox.Show(mainForm.Owner, $"Error in {file}:{line}:{from}:\n${msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            Application.Run(mainForm);
        }
    }
}
