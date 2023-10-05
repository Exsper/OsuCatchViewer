using OsuCatchViewer.CatchAPI;
using System.Diagnostics;


namespace OsuCatchViewer
{
    public partial class MainForm : Form
    {
        public OsuDbAPI.OsuDbFile OsuDbFile { get; set; }

        public static string Path_Settings = "settings.txt";
        public static string Path_OsuDb = "";
        public static string Path_Songs = "";
        public static string Path_Replays = "";
        public static string Path_Img_EditorNode = @"img/editornode.png";
        public static string Path_Img_Cursor = @"img/fruit-ryuuta.png";
        public static string Path_Img_Hitcircle = @"img/fruit-apple.png";
        public static string Path_Img_Drop = @"img/fruit-drop.png";
        public static string Path_Img_Banana = @"img/fruit-bananas.png";
        public static string Path_Img_Help = @"img/help.png";
        public static MainForm self;


        public MainForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            MainForm.self = this;

            InitializeComponent();
            this.Stopwatch = new Stopwatch();
            this.Stopwatch.Start();
            Application.Idle += Application_Idle;
        }

        private Stopwatch Stopwatch;

        private void Application_Idle(object sender, EventArgs e)
        {
            this.Stopwatch.Stop();
            if (this.Stopwatch.ElapsedMilliseconds > 2)
                this.Canvas.Invalidate();
            this.Stopwatch.Restart();
        }

        public void SetSettings(string[] settings)
        {
            MainForm.Path_OsuDb = settings[0];
            MainForm.Path_Songs = settings[1];
            MainForm.Path_Replays = settings[2];
            if (Directory.Exists(MainForm.Path_Songs))
            {
                if (MainForm.Path_Songs[MainForm.Path_Songs.Length - 1] != '\\')
                {
                    MainForm.Path_Songs += '\\';
                }
                if (File.Exists(MainForm.Path_OsuDb))
                {
                    try
                    {
                        this.OsuDbFile = new OsuDbAPI.OsuDbFile(MainForm.Path_OsuDb);
                    }
                    catch
                    {
                        MainForm.ErrorMessage("无法载入osu!.db。尝试备份后删除该文件，打开osu程序让osu重建缓存。");
                    }
                }
                else
                {
                    MainForm.ErrorMessage("osu!.db文件不存在，无法显示谱面，将只展示replay。");
                }
            }
            else
            {
                MainForm.ErrorMessage("谱面不存在，无法显示谱面，将只展示replay。");
            }
            if (Directory.Exists(MainForm.Path_Replays))
            {
                if (MainForm.Path_Replays[MainForm.Path_Replays.Length - 1] != '\\')
                {
                    MainForm.Path_Replays += '\\';
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text += " build " + Program.BUILD_DATE;
            this.Canvas.Init();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O))
            {
                this.Open();
                return true;
            }
            else if (keyData == Keys.S)
            {
                this.playBtn_Click(null, null);
            }
            else if (keyData == Keys.Q)
            {
                this.viewBtn_Click(null, null);
            }
            else if (keyData == Keys.W)
            {
                this.dataBtn_Click(null, null);
            }
            else if (keyData == Keys.E)
            {
                this.pressBtn_Click(null, null);
            }
            else if (keyData == Keys.D1)
            {
                this.replay1Radio.Checked = true;
            }
            else if (keyData == Keys.D2)
            {
                this.replay2Radio.Checked = true;
            }
            else if (keyData == Keys.D3)
            {
                this.replay3Radio.Checked = true;
            }
            else if (keyData == Keys.D4)
            {
                this.replay4Radio.Checked = true;
            }
            else if (keyData == Keys.D5)
            {
                this.replay5Radio.Checked = true;
            }
            else if (keyData == Keys.D6)
            {
                this.replay6Radio.Checked = true;
            }
            else if (keyData == Keys.D7)
            {
                this.replay7Radio.Checked = true;
            }
            else if (keyData == Keys.Z)
            {
                this.speed025Radio.Checked = true;
            }
            else if (keyData == Keys.X)
            {
                this.speed050Radio.Checked = true;
            }
            else if (keyData == Keys.C)
            {
                this.speed075Radio.Checked = true;
            }
            else if (keyData == Keys.V)
            {
                this.speed100Radio.Checked = true;
            }
            else if (keyData == Keys.B)
            {
                this.speed150Radio.Checked = true;
            }
            else if (keyData == Keys.N)
            {
                this.speed200Radio.Checked = true;
            }
            else if (keyData == Keys.M)
            {
                this.speed400Radio.Checked = true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            this.Open(files[0]);
        }

        public static void ErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error");
        }

        public void Open()
        {
            if (this.openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                this.Open(this.openFileDialog.FileName);
            }
        }


        public void Open(string path)
        {
            try
            {
                string ext = path.Substring(path.LastIndexOf(".") + 1);
                if (String.Equals(ext, "osr", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (this.Canvas.viewerManager == null) this.Canvas.viewerManager = new ViewerManager(OsuDbFile, path, true);
                    else this.Canvas.viewerManager.LoadReplay(path, this.Canvas.viewerManager.State_ReplaySelected);
                }
                else if (String.Equals(ext, "osu", StringComparison.CurrentCultureIgnoreCase))
                {
                    int mods = 0;
                    if (HRRadioButton.Checked) mods = (1 << 4);
                    else if (EZRadioButton.Checked) mods = (1 << 1);
                    if (this.Canvas.viewerManager == null) this.Canvas.viewerManager = new ViewerManager(OsuDbFile, path, false, mods);
                    else this.Canvas.viewerManager.LoadBeatmap(path, mods);
                }
                this.UpdateTitle();
                this.volumeBar_Scroll(null, null);
                this.Canvas.ShowHelp = 0;

                List<double>[] bananaShowerPercent = CatchBeatmapAPI.BananaShowerTime(this.Canvas.viewerManager.Beatmap, this.Canvas.viewerManager.MaxSongTime);
                timeline.BananaShowerPercent = bananaShowerPercent;
            }
            catch (Exception e)
            {
                MainForm.ErrorMessage("读取文件出错\n附加信息：" + e.Message);
            }
        }



        public void UpdateTitle()
        {
            this.Text = this.Canvas.viewerManager.Beatmap.BeatmapInfo.ToString();
            int mods = (int)this.Canvas.viewerManager.BeatmapMods;
            if (mods > 0)
            {
                this.Text += " - " + string.Join("+", CatchBeatmapAPI.GetModsString(mods));
            }
            if ((mods & 2) > 0) EZRadioButton.Checked = true;
            else if ((mods & 16) > 0) HRRadioButton.Checked = true;
            else NoneRadioButton.Checked = true;

            if (this.Canvas.viewerManager.CurrentReplays[0] == null) replay1Radio.Text = "红(*)";
            else replay1Radio.Text = "红(" + this.Canvas.viewerManager.CurrentReplays[0].PlayerName + ")";
            if (this.Canvas.viewerManager.CurrentReplays[1] == null) replay2Radio.Text = "蓝(*)";
            else replay2Radio.Text = "蓝(" + this.Canvas.viewerManager.CurrentReplays[1].PlayerName + ")";
            if (this.Canvas.viewerManager.CurrentReplays[2] == null) replay3Radio.Text = "绿(*)";
            else replay3Radio.Text = "绿(" + this.Canvas.viewerManager.CurrentReplays[2].PlayerName + ")";
            if (this.Canvas.viewerManager.CurrentReplays[3] == null) replay4Radio.Text = "黄(*)";
            else replay4Radio.Text = "黄(" + this.Canvas.viewerManager.CurrentReplays[3].PlayerName + ")";
            if (this.Canvas.viewerManager.CurrentReplays[4] == null) replay5Radio.Text = "粉(*)";
            else replay5Radio.Text = "粉(" + this.Canvas.viewerManager.CurrentReplays[4].PlayerName + ")";
            if (this.Canvas.viewerManager.CurrentReplays[5] == null) replay6Radio.Text = "紫(*)";
            else replay6Radio.Text = "紫(" + this.Canvas.viewerManager.CurrentReplays[5].PlayerName + ")";
            if (this.Canvas.viewerManager.CurrentReplays[6] == null) replay7Radio.Text = "白(*)";
            else replay7Radio.Text = "白(" + this.Canvas.viewerManager.CurrentReplays[6].PlayerName + ")";

            OpenTK.Graphics.Color4 c = Canvas.Color_Cursor[this.Canvas.viewerManager.State_ReplaySelected];
            this.cursorColorPanel.BackColor = System.Drawing.Color.FromArgb((int)(255 * c.R), (int)(255 * c.G), (int)(255 * c.B));
        }

        public RadioButton GetReplayRadioBtn(int n)
        {
            if (n == 0)
            {
                return this.replay1Radio;
            }
            else if (n == 1)
            {
                return this.replay2Radio;
            }
            else if (n == 2)
            {
                return this.replay3Radio;
            }
            else if (n == 3)
            {
                return this.replay4Radio;
            }
            else if (n == 4)
            {
                return this.replay5Radio;
            }
            else if (n == 5)
            {
                return this.replay6Radio;
            }
            else if (n == 6)
            {
                return this.replay7Radio;
            }
            else
            {
                return null;
            }
        }

        private void timeline_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Canvas.viewerManager.SetSongTimePercent(e.X / (float)this.timeline.Width);
        }

        public void SetTimelinePercent(float percent)
        {
            if (percent < 0)
            {
                percent = 0;
            }
            else if (percent > 1)
            {
                percent = 1;
            }
            this.timeline.Value = percent;
        }

        public void SetSongTimeLabel(int ms)
        {
            this.songTimeLabel.Text = ms + " ms";
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            if (this.Canvas.viewerManager == null) return;
            if (this.playBtn.Text == "Play")
            {
                this.Canvas.viewerManager.State_PlaybackFlow = 3;
            }
            else
            {
                this.Canvas.viewerManager.State_PlaybackFlow = 0;
            }

        }

        public void SetPlayPause(string value)
        {
            if (this.playBtn.Text != value)
            {
                this.playBtn.Text = value;
            }
        }

        private void viewBtn_Click(object sender, EventArgs e)
        {
            this.Canvas.viewerManager.State_PlaybackMode = 1;
        }

        private void dataBtn_Click(object sender, EventArgs e)
        {
            this.Canvas.viewerManager.State_PlaybackMode = 0;
        }

        private void pressBtn_Click(object sender, EventArgs e)
        {
            this.Canvas.viewerManager.State_PlaybackMode = 2;
        }

        private void timeWindowBar_Scroll(object sender, EventArgs e)
        {
            this.Canvas.viewerManager.State_TimeRange = this.timeWindowBar.Value * 10;
            this.timeWindowLabel.Text = this.Canvas.viewerManager.State_TimeRange + " ms";
        }

        private void volumeBar_Scroll(object sender, EventArgs e)
        {
            this.Canvas.viewerManager.State_Volume = this.volumeBar.Value * 0.1f;
            this.volumeBarLabel.Text = String.Format("{0} %", this.volumeBar.Value * 10);
        }

        private void speed025Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateSpeedRadio();
        }

        private void speed050Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateSpeedRadio();
        }

        private void speed075Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateSpeedRadio();
        }

        private void speed100Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateSpeedRadio();
        }

        private void speed150Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateSpeedRadio();
        }

        private void speed200Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateSpeedRadio();
        }

        private void speed400Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateSpeedRadio();
        }

        public void UpdateSpeedRadio()
        {
            if (this.speed025Radio.Checked)
            {
                this.Canvas.viewerManager.State_PlaybackSpeed = 0.25f;
            }
            else if (this.speed050Radio.Checked)
            {
                this.Canvas.viewerManager.State_PlaybackSpeed = 0.50f;
            }
            else if (this.speed075Radio.Checked)
            {
                this.Canvas.viewerManager.State_PlaybackSpeed = 0.75f;
            }
            else if (this.speed100Radio.Checked)
            {
                this.Canvas.viewerManager.State_PlaybackSpeed = 1.00f;
            }
            else if (this.speed150Radio.Checked)
            {
                this.Canvas.viewerManager.State_PlaybackSpeed = 1.50f;
            }
            else if (this.speed200Radio.Checked)
            {
                this.Canvas.viewerManager.State_PlaybackSpeed = 2.00f;
            }
            else if (this.speed400Radio.Checked)
            {
                this.Canvas.viewerManager.State_PlaybackSpeed = 4.00f;
            }
        }

        public void UpdateSpeedRadio(float value)
        {
            if (value == 0.25f)
            {
                this.speed025Radio.Checked = true;
            }
            else if (value == 0.50f)
            {
                this.speed050Radio.Checked = true;
            }
            else if (value == 0.75f)
            {
                this.speed075Radio.Checked = true;
            }
            else if (value == 1.00f)
            {
                this.speed100Radio.Checked = true;
            }
            else if (value == 1.50f)
            {
                this.speed150Radio.Checked = true;
            }
            else if (value == 2.00f)
            {
                this.speed200Radio.Checked = true;
            }
            else if (value == 4.00f)
            {
                this.speed400Radio.Checked = true;
            }

        }

        private void replay1Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateReplayRadio();
        }

        private void replay2Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateReplayRadio();
        }

        private void replay3Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateReplayRadio();
        }

        private void replay4Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateReplayRadio();
        }

        private void replay5Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateReplayRadio();
        }

        private void replay6Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateReplayRadio();
        }

        private void replay7Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateReplayRadio();
        }

        public void UpdateReplayRadio()
        {
            for (byte i = 0; i < 7; i++)
            {
                if (this.GetReplayRadioBtn(i).Checked)
                {
                    if (this.Canvas.viewerManager != null)
                    {
                        this.Canvas.viewerManager.State_ReplaySelected = i;
                        break;
                    }
                }
            }
            this.UpdateTitle();
        }

        public void UpdateReplayRadio(byte value)
        {
            this.GetReplayRadioBtn(value).Checked = true;
            this.coloredPanel.BackColor = System.Drawing.Color.FromArgb(Canvas.Color_Cursor[value].ToArgb());
        }

        private void unloadBtn_Click(object sender, EventArgs e)
        {
            if (this.Canvas.viewerManager.CurrentReplays[this.Canvas.viewerManager.State_ReplaySelected] != null)
            {
                this.Canvas.viewerManager.CurrentReplays[this.Canvas.viewerManager.State_ReplaySelected].Dispose();
                this.Canvas.viewerManager.CurrentReplays[this.Canvas.viewerManager.State_ReplaySelected] = null;
                this.Canvas.viewerManager.UnloadReplay(this.Canvas.viewerManager.State_ReplaySelected);
                this.UpdateTitle();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Open();
        }

        private void OpenBeatmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Open();

        }

        private void openSettingsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.OpenSettings();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.SetSettings(Program.LoadSettings());
            }
            catch
            {
                MainForm.ErrorMessage("The settings file was deleted. A new one will be created when you relaunch the program.");
            }
        }

        private void quickLoadToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.quickLoadToolStripMenuItem.DropDownItems.Clear();
            if (Directory.Exists(MainForm.Path_Replays))
            {
                string[] files = Directory.GetFiles(MainForm.Path_Replays, "*.osr", SearchOption.TopDirectoryOnly);
                ToolStripMenuItem parentItem = this.quickLoadToolStripMenuItem;
                int groupBy = 30;
                int maxItems = files.Length;
                int counter = 0;
                foreach (string fullpath in files)
                {
                    if (maxItems > groupBy && counter % groupBy == 0)
                    {
                        parentItem = new ToolStripMenuItem(String.Format("{0} to {1}", counter + 1, Math.Min(counter + groupBy, maxItems)));
                        this.quickLoadToolStripMenuItem.DropDownItems.Add(parentItem);
                    }
                    string[] split = fullpath.Split('\\');
                    ToolStripMenuItem item = new ToolStripMenuItem(split[split.Length - 1]);
                    item.Click += item_Click;
                    parentItem.DropDownItems.Add(item);
                    counter++;
                }
            }
            else
            {
                MainForm.ErrorMessage("You do not have a replay folder specified. You can change this in the settings file.");
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            this.quickLoadToolStripMenuItem.DropDownItems.Clear();
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                this.Open(Path.Combine(MainForm.Path_Replays, item.Text));
            }
        }

        private void onscreenHelpCtrlHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Canvas.ShowHelp = 2;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Canvas.viewerManager != null) this.Canvas.viewerManager.StopSongPlayer();
        }

        public void GetCurrentBeatmap()
        {
            string path;
            try
            {
                path = IOHelper.GetCurrentBeatmap(Path_Songs);
                Open(path);
            }
            catch (Exception ex)
            {
                ErrorMessage("自动获取谱面失败，请手动选择谱面。\n错误信息：" + ex.Message);
            }
        }

        private void 获取osu当前谱面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentBeatmap();
        }

        private void ReloadBeatmapWithMod()
        {
            Open(this.Canvas.viewerManager.BeatmapPath);
        }

        private void NoneRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ReloadBeatmapWithMod();
        }

        private void HRRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ReloadBeatmapWithMod();
        }

        private void EZRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ReloadBeatmapWithMod();
        }
    }
}
