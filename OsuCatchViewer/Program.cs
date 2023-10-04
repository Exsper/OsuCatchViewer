using Microsoft.Win32;
using OsuCatchViewer;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OsuCatchViewer
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        public const string BUILD_DATE = "2023-10-03 (modified from ReplayViewer)";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Main2();
            //Application.Run(new TestForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Crash !");
            try
            {
                string d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                File.WriteAllText("crash.log", d + "\r\n" + e.ExceptionObject.ToString());
            }
            catch
            {
                Console.WriteLine("An exception occured while writing crash.log");
            }
        }
        private static string getOsuPath()
        {
            using (RegistryKey osureg = Registry.ClassesRoot.OpenSubKey("osu\\DefaultIcon"))
            {
                if (osureg != null)
                {
                    string osukey = osureg.GetValue(null).ToString();
                    string osupath = osukey.Remove(0, 1);
                    osupath = osupath.Remove(osupath.Length - 11);
                    return osupath;
                }
                else return "";
            }
        }

        private static void Main2()
        {
            SetProcessDPIAware();
            string[] settings = new string[3];
            if (File.Exists(MainForm.Path_Settings))
            {
                settings = Program.LoadSettings();
            }
            else
            {
                string osupath = getOsuPath();
                if (osupath == "") osupath = @"C:\osu!\";
                settings = new string[]
                {
                    @"# 每行以#开头的为注释",
                    @"# 请勿更改设置顺序",
                    @"",
                    @"# osu!.db路径",
                    osupath + @"osu!.db",
                    @"",
                    @"# songs文件夹路径",
                    osupath + @"songs\",
                    @"",
                    @"# replay文件夹路径",
                    osupath + @"replays\"
                };
                File.WriteAllLines(MainForm.Path_Settings, settings);
                DialogResult reply = MessageBox.Show("已自动创建osu!相关路径配置文件，是否停止程序并立刻检查其内容？", "已创建配置文件", MessageBoxButtons.YesNo);
                if (reply == DialogResult.Yes)
                {
                    Program.OpenSettings();
                    return;
                }
                else
                {
                    settings = new string[3];
                    settings[0] = osupath + @"osu!.db";
                    settings[1] = osupath + @"songs\";
                    settings[2] = osupath + @"replays\";
                }
            }
            using (MainForm form = new MainForm())
            {
                form.SetSettings(settings);
                Application.Run(form);
            }
            //form.Canvas = new Canvas(form.GetPictureBoxHandle(), form);
            //form.Canvas.Run();
        }

        public static void OpenSettings()
        {
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo(MainForm.Path_Settings) { UseShellExecute = true });
            }
            catch (Exception e)
            {
                MainForm.ErrorMessage(e.Message);
            }
        }

        public static string[] LoadSettings()
        {
            string[] settings = new string[3];
            string[] lines = File.ReadAllLines(MainForm.Path_Settings);
            int n = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (n >= settings.Length)
                {
                    break;
                }
                if (lines[i].Length > 0 && lines[i][0] != '#')
                {
                    settings[n] = lines[i];
                    n++;
                }
            }
            return settings;
        }
    }
}
