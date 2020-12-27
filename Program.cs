using System;
using System.Windows.Forms;

namespace GalsPassHolder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);
            }
            //catch (System.Security.SecurityException) { } //interesting that with min permmissions, unable to tie this handler in
            //catch (MethodAccessException) { }
            catch (Exception ex)
            {
                DealWithException(ex);
            }

            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandedException);
            }
            catch (System.Security.SecurityException) { } //interesting that with current permmissions, unable to tie this handler in
            catch (MethodAccessException) { }
            catch (Exception ex)
            {
                DealWithException(ex);
            }

            FrmMain frm = null;
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                frm = new FrmMain();
                Application.Run(frm);
            }
            catch (Exception ex)
            {
                DealWithException(ex);
            }
            finally
            {
                if (frm != null)
                    frm.Dispose(); //why does the code analyizer recommend this?
            }
        }

        static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs args)
        {
            DealWithException(args.Exception);
        }

        static void UnhandedException(object sender, System.UnhandledExceptionEventArgs args)
        {

            DealWithException((Exception)args.ExceptionObject);
        }

        static void DealWithException(Exception ex)
        {
            
            try
            {
                var nl = Environment.NewLine;
                var text = "==================================" + nl + DateTime.Now.ToString(GalFormFunctions.inv) + nl + nl + ConvertExceptionToTextRecursive(ex) + nl + nl;
                var fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\galErrors.txt";
                MessageBox.Show(text + nl + nl + "Attempting to save to: " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.IO.File.AppendAllText(fileName, text);
            }
            catch (Exception ex2)
            {
                ErrorHandlerPanic(ex2);
            }
        }

        static string ConvertExceptionToTextRecursive(Exception ex, int depth = 0)
        {
            var text = new System.Text.StringBuilder();
            var nl = Environment.NewLine;
            string name = "", message = "", source = "", stackTrace = "", targetSite = "", targetSiteToString = "";
            try
            {
                message = ex.Message;
                source = ex.Source;
                stackTrace = ex.StackTrace;
                if (ex.TargetSite != null)
                {
                    targetSite = ex.TargetSite.Name;
                    targetSiteToString = ex.TargetSite.ToString();
                }
                name = ex.GetType().FullName;
            }
            catch (Exception ex2)
            {
                ErrorHandlerPanic(ex2);
            }
            text.Append("Type: " + name + nl + "Message: " + message + nl + "Source: " + source + nl + "Stacktrace:" + nl + stackTrace + nl + nl);
            if (!String.IsNullOrWhiteSpace(targetSite))
                text.Append("targetsite:" + nl + "name: " + targetSite + nl + "toString(): " + targetSiteToString + nl);

            try
            {
                if (ex.InnerException != null && depth <= 5)
                    text.Append(nl + "--------------------------------------" + nl + nl + ConvertExceptionToTextRecursive(ex, depth+1));
                else
                    text.Append(nl + "exceeded depth limit");
            }
            catch (Exception ex2)
            {
                ErrorHandlerPanic(ex2);
            }

            return text.ToString();
        }

        static void ErrorHandlerPanic(Exception ex, String location = "")
        {
            String msg = "Error handler Panic: \n";
            if (!String.IsNullOrWhiteSpace(location))
                msg += location + "\n";
            msg += ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace;
            MessageBox.Show(msg);
        }
    }
}
