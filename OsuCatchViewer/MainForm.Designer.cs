namespace OsuCatchViewer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            playBtn = new Button();
            dataBtn = new Button();
            timeWindowBar = new TrackBar();
            label1 = new Label();
            timeWindowLabel = new Label();
            panel1 = new Panel();
            speed400Radio = new RadioButton();
            label2 = new Label();
            speed200Radio = new RadioButton();
            speed150Radio = new RadioButton();
            speed100Radio = new RadioButton();
            speed050Radio = new RadioButton();
            speed025Radio = new RadioButton();
            speed075Radio = new RadioButton();
            viewBtn = new Button();
            panel2 = new Panel();
            replay7Radio = new RadioButton();
            label3 = new Label();
            replay6Radio = new RadioButton();
            replay5Radio = new RadioButton();
            replay4Radio = new RadioButton();
            replay2Radio = new RadioButton();
            replay1Radio = new RadioButton();
            replay3Radio = new RadioButton();
            unloadBtn = new Button();
            songTimeLabel = new Label();
            volumeBar = new TrackBar();
            label4 = new Label();
            volumeBarLabel = new Label();
            cursorColorPanel = new Panel();
            openFileDialog = new OpenFileDialog();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            OpenBeatmapToolStripMenuItem = new ToolStripMenuItem();
            获取osu当前谱面ToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            openSettingsFileToolStripMenuItem = new ToolStripMenuItem();
            reloadToolStripMenuItem = new ToolStripMenuItem();
            quickLoadToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            onscreenHelpToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            coloredPanel = new Panel();
            label5 = new Label();
            pressBtn = new Button();
            timeline = new Timeline();
            panel3 = new Panel();
            EZRadioButton = new RadioButton();
            HRRadioButton = new RadioButton();
            NoneRadioButton = new RadioButton();
            label6 = new Label();
            Canvas = new Canvas();
            panel4 = new Panel();
            BananaDifficulty1_1RadioButton = new RadioButton();
            BananaDifficulty1_2RadioButton = new RadioButton();
            BananaDifficulty1_3RadioButton = new RadioButton();
            BananaDifficulty3RadioButton = new RadioButton();
            BananaDifficulty2RadioButton = new RadioButton();
            BananaDifficulty1RadioButton = new RadioButton();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)timeWindowBar).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            menuStrip1.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // playBtn
            // 
            playBtn.Anchor = AnchorStyles.Bottom;
            playBtn.Location = new Point(6, 749);
            playBtn.Margin = new Padding(4);
            playBtn.Name = "playBtn";
            playBtn.Size = new Size(147, 58);
            playBtn.TabIndex = 2;
            playBtn.Text = "播放";
            playBtn.UseVisualStyleBackColor = true;
            playBtn.Click += playBtn_Click;
            // 
            // dataBtn
            // 
            dataBtn.Location = new Point(293, 31);
            dataBtn.Margin = new Padding(4);
            dataBtn.Name = "dataBtn";
            dataBtn.Size = new Size(161, 48);
            dataBtn.TabIndex = 5;
            dataBtn.Text = "观察按键时长";
            dataBtn.UseVisualStyleBackColor = true;
            dataBtn.Click += dataBtn_Click;
            // 
            // timeWindowBar
            // 
            timeWindowBar.Anchor = AnchorStyles.Bottom;
            timeWindowBar.LargeChange = 2;
            timeWindowBar.Location = new Point(185, 749);
            timeWindowBar.Margin = new Padding(4);
            timeWindowBar.Maximum = 20;
            timeWindowBar.Minimum = 2;
            timeWindowBar.Name = "timeWindowBar";
            timeWindowBar.Size = new Size(753, 45);
            timeWindowBar.TabIndex = 7;
            timeWindowBar.Value = 10;
            timeWindowBar.Scroll += timeWindowBar_Scroll;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Location = new Point(961, 749);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 8;
            label1.Text = "回放长度";
            // 
            // timeWindowLabel
            // 
            timeWindowLabel.Anchor = AnchorStyles.Bottom;
            timeWindowLabel.AutoSize = true;
            timeWindowLabel.Location = new Point(1025, 749);
            timeWindowLabel.Margin = new Padding(4, 0, 4, 0);
            timeWindowLabel.Name = "timeWindowLabel";
            timeWindowLabel.Size = new Size(50, 17);
            timeWindowLabel.TabIndex = 9;
            timeWindowLabel.Text = "100 ms";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left;
            panel1.Controls.Add(speed400Radio);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(speed200Radio);
            panel1.Controls.Add(speed150Radio);
            panel1.Controls.Add(speed100Radio);
            panel1.Controls.Add(speed050Radio);
            panel1.Controls.Add(speed025Radio);
            panel1.Controls.Add(speed075Radio);
            panel1.Location = new Point(10, 113);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(107, 265);
            panel1.TabIndex = 10;
            // 
            // speed400Radio
            // 
            speed400Radio.AutoSize = true;
            speed400Radio.Location = new Point(1, 230);
            speed400Radio.Margin = new Padding(4);
            speed400Radio.Name = "speed400Radio";
            speed400Radio.Size = new Size(60, 21);
            speed400Radio.TabIndex = 18;
            speed400Radio.Text = "4.00 x";
            speed400Radio.UseVisualStyleBackColor = true;
            speed400Radio.CheckedChanged += speed400Radio_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(-4, 7);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 12;
            label2.Text = "播放速度";
            // 
            // speed200Radio
            // 
            speed200Radio.AutoSize = true;
            speed200Radio.Location = new Point(1, 200);
            speed200Radio.Margin = new Padding(4);
            speed200Radio.Name = "speed200Radio";
            speed200Radio.Size = new Size(60, 21);
            speed200Radio.TabIndex = 17;
            speed200Radio.Text = "2.00 x";
            speed200Radio.UseVisualStyleBackColor = true;
            speed200Radio.CheckedChanged += speed200Radio_CheckedChanged;
            // 
            // speed150Radio
            // 
            speed150Radio.AutoSize = true;
            speed150Radio.Location = new Point(1, 170);
            speed150Radio.Margin = new Padding(4);
            speed150Radio.Name = "speed150Radio";
            speed150Radio.Size = new Size(88, 21);
            speed150Radio.TabIndex = 16;
            speed150Radio.Text = "1.50 x (DT)";
            speed150Radio.UseVisualStyleBackColor = true;
            speed150Radio.CheckedChanged += speed150Radio_CheckedChanged;
            // 
            // speed100Radio
            // 
            speed100Radio.AutoSize = true;
            speed100Radio.Checked = true;
            speed100Radio.Location = new Point(1, 140);
            speed100Radio.Margin = new Padding(4);
            speed100Radio.Name = "speed100Radio";
            speed100Radio.Size = new Size(60, 21);
            speed100Radio.TabIndex = 15;
            speed100Radio.TabStop = true;
            speed100Radio.Text = "1.00 x";
            speed100Radio.UseVisualStyleBackColor = true;
            speed100Radio.CheckedChanged += speed100Radio_CheckedChanged;
            // 
            // speed050Radio
            // 
            speed050Radio.AutoSize = true;
            speed050Radio.Location = new Point(1, 79);
            speed050Radio.Margin = new Padding(4);
            speed050Radio.Name = "speed050Radio";
            speed050Radio.Size = new Size(60, 21);
            speed050Radio.TabIndex = 14;
            speed050Radio.Text = "0.50 x";
            speed050Radio.UseVisualStyleBackColor = true;
            speed050Radio.CheckedChanged += speed050Radio_CheckedChanged;
            // 
            // speed025Radio
            // 
            speed025Radio.AutoSize = true;
            speed025Radio.Location = new Point(1, 50);
            speed025Radio.Margin = new Padding(4);
            speed025Radio.Name = "speed025Radio";
            speed025Radio.Size = new Size(60, 21);
            speed025Radio.TabIndex = 14;
            speed025Radio.Text = "0.25 x";
            speed025Radio.UseVisualStyleBackColor = true;
            speed025Radio.CheckedChanged += speed025Radio_CheckedChanged;
            // 
            // speed075Radio
            // 
            speed075Radio.AutoSize = true;
            speed075Radio.Location = new Point(1, 110);
            speed075Radio.Margin = new Padding(4);
            speed075Radio.Name = "speed075Radio";
            speed075Radio.Size = new Size(88, 21);
            speed075Radio.TabIndex = 13;
            speed075Radio.Text = "0.75 x (HT)";
            speed075Radio.UseVisualStyleBackColor = true;
            speed075Radio.CheckedChanged += speed075Radio_CheckedChanged;
            // 
            // viewBtn
            // 
            viewBtn.Location = new Point(125, 31);
            viewBtn.Margin = new Padding(4);
            viewBtn.Name = "viewBtn";
            viewBtn.Size = new Size(161, 48);
            viewBtn.TabIndex = 12;
            viewBtn.Text = "观看模式";
            viewBtn.Click += viewBtn_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Left;
            panel2.Controls.Add(replay7Radio);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(replay6Radio);
            panel2.Controls.Add(replay5Radio);
            panel2.Controls.Add(replay4Radio);
            panel2.Controls.Add(replay2Radio);
            panel2.Controls.Add(replay1Radio);
            panel2.Controls.Add(replay3Radio);
            panel2.Location = new Point(8, 425);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(107, 265);
            panel2.TabIndex = 19;
            // 
            // replay7Radio
            // 
            replay7Radio.AutoSize = true;
            replay7Radio.Location = new Point(1, 230);
            replay7Radio.Margin = new Padding(4);
            replay7Radio.Name = "replay7Radio";
            replay7Radio.Size = new Size(38, 21);
            replay7Radio.TabIndex = 18;
            replay7Radio.Text = "白";
            replay7Radio.UseVisualStyleBackColor = true;
            replay7Radio.CheckedChanged += replay7Radio_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(-1, 14);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 12;
            label3.Text = "多人回放";
            // 
            // replay6Radio
            // 
            replay6Radio.AutoSize = true;
            replay6Radio.Location = new Point(1, 200);
            replay6Radio.Margin = new Padding(4);
            replay6Radio.Name = "replay6Radio";
            replay6Radio.RightToLeft = RightToLeft.No;
            replay6Radio.Size = new Size(38, 21);
            replay6Radio.TabIndex = 17;
            replay6Radio.Text = "紫";
            replay6Radio.UseVisualStyleBackColor = true;
            replay6Radio.CheckedChanged += replay6Radio_CheckedChanged;
            // 
            // replay5Radio
            // 
            replay5Radio.AutoSize = true;
            replay5Radio.Location = new Point(1, 170);
            replay5Radio.Margin = new Padding(4);
            replay5Radio.Name = "replay5Radio";
            replay5Radio.Size = new Size(38, 21);
            replay5Radio.TabIndex = 16;
            replay5Radio.Text = "粉";
            replay5Radio.UseVisualStyleBackColor = true;
            replay5Radio.CheckedChanged += replay5Radio_CheckedChanged;
            // 
            // replay4Radio
            // 
            replay4Radio.AutoSize = true;
            replay4Radio.Location = new Point(1, 140);
            replay4Radio.Margin = new Padding(4);
            replay4Radio.Name = "replay4Radio";
            replay4Radio.Size = new Size(38, 21);
            replay4Radio.TabIndex = 15;
            replay4Radio.Text = "黄";
            replay4Radio.UseVisualStyleBackColor = true;
            replay4Radio.CheckedChanged += replay4Radio_CheckedChanged;
            // 
            // replay2Radio
            // 
            replay2Radio.AutoSize = true;
            replay2Radio.Location = new Point(1, 79);
            replay2Radio.Margin = new Padding(4);
            replay2Radio.Name = "replay2Radio";
            replay2Radio.Size = new Size(38, 21);
            replay2Radio.TabIndex = 14;
            replay2Radio.Text = "蓝";
            replay2Radio.UseVisualStyleBackColor = true;
            replay2Radio.CheckedChanged += replay2Radio_CheckedChanged;
            // 
            // replay1Radio
            // 
            replay1Radio.AutoSize = true;
            replay1Radio.Checked = true;
            replay1Radio.Location = new Point(1, 50);
            replay1Radio.Margin = new Padding(4);
            replay1Radio.Name = "replay1Radio";
            replay1Radio.Size = new Size(38, 21);
            replay1Radio.TabIndex = 14;
            replay1Radio.TabStop = true;
            replay1Radio.Text = "红";
            replay1Radio.UseVisualStyleBackColor = true;
            replay1Radio.CheckedChanged += replay1Radio_CheckedChanged;
            // 
            // replay3Radio
            // 
            replay3Radio.AutoSize = true;
            replay3Radio.Location = new Point(1, 110);
            replay3Radio.Margin = new Padding(4);
            replay3Radio.Name = "replay3Radio";
            replay3Radio.Size = new Size(38, 21);
            replay3Radio.TabIndex = 13;
            replay3Radio.Text = "绿";
            replay3Radio.UseVisualStyleBackColor = true;
            replay3Radio.CheckedChanged += replay3Radio_CheckedChanged;
            // 
            // unloadBtn
            // 
            unloadBtn.Anchor = AnchorStyles.Left;
            unloadBtn.Location = new Point(21, 701);
            unloadBtn.Margin = new Padding(4);
            unloadBtn.Name = "unloadBtn";
            unloadBtn.Size = new Size(88, 30);
            unloadBtn.TabIndex = 20;
            unloadBtn.Text = "删除选中回放";
            unloadBtn.UseVisualStyleBackColor = true;
            unloadBtn.Click += unloadBtn_Click;
            // 
            // songTimeLabel
            // 
            songTimeLabel.Anchor = AnchorStyles.Bottom;
            songTimeLabel.AutoSize = true;
            songTimeLabel.Location = new Point(1063, 704);
            songTimeLabel.Margin = new Padding(4, 0, 4, 0);
            songTimeLabel.Name = "songTimeLabel";
            songTimeLabel.Size = new Size(36, 17);
            songTimeLabel.TabIndex = 21;
            songTimeLabel.Text = "0 ms";
            // 
            // volumeBar
            // 
            volumeBar.Anchor = AnchorStyles.Right;
            volumeBar.Location = new Point(1063, 141);
            volumeBar.Margin = new Padding(4);
            volumeBar.Name = "volumeBar";
            volumeBar.Orientation = Orientation.Vertical;
            volumeBar.Size = new Size(45, 98);
            volumeBar.TabIndex = 22;
            volumeBar.TickStyle = TickStyle.Both;
            volumeBar.Value = 10;
            volumeBar.Scroll += volumeBar_Scroll;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(1031, 120);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 23;
            label4.Text = "音量：";
            // 
            // volumeBarLabel
            // 
            volumeBarLabel.Anchor = AnchorStyles.Right;
            volumeBarLabel.AutoSize = true;
            volumeBarLabel.Location = new Point(1083, 120);
            volumeBarLabel.Margin = new Padding(4, 0, 4, 0);
            volumeBarLabel.Name = "volumeBarLabel";
            volumeBarLabel.Size = new Size(44, 17);
            volumeBarLabel.TabIndex = 24;
            volumeBarLabel.Text = "100 %";
            volumeBarLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cursorColorPanel
            // 
            cursorColorPanel.BackColor = Color.Red;
            cursorColorPanel.BorderStyle = BorderStyle.Fixed3D;
            cursorColorPanel.Location = new Point(629, 33);
            cursorColorPanel.Margin = new Padding(4);
            cursorColorPanel.Name = "cursorColorPanel";
            cursorColorPanel.Size = new Size(41, 46);
            cursorColorPanel.TabIndex = 25;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, OpenBeatmapToolStripMenuItem, 获取osu当前谱面ToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(44, 21);
            fileToolStripMenuItem.Text = "文件";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeyDisplayString = "(Ctrl + O)";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(202, 22);
            openToolStripMenuItem.Text = "打开Replay";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // OpenBeatmapToolStripMenuItem
            // 
            OpenBeatmapToolStripMenuItem.Name = "OpenBeatmapToolStripMenuItem";
            OpenBeatmapToolStripMenuItem.Size = new Size(202, 22);
            OpenBeatmapToolStripMenuItem.Text = "仅打开谱面";
            OpenBeatmapToolStripMenuItem.Click += OpenBeatmapToolStripMenuItem_Click;
            // 
            // 获取osu当前谱面ToolStripMenuItem
            // 
            获取osu当前谱面ToolStripMenuItem.Name = "获取osu当前谱面ToolStripMenuItem";
            获取osu当前谱面ToolStripMenuItem.Size = new Size(202, 22);
            获取osu当前谱面ToolStripMenuItem.Text = "获取osu!当前谱面";
            获取osu当前谱面ToolStripMenuItem.Click += 获取osu当前谱面ToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openSettingsFileToolStripMenuItem, reloadToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(44, 21);
            settingsToolStripMenuItem.Text = "设置";
            // 
            // openSettingsFileToolStripMenuItem
            // 
            openSettingsFileToolStripMenuItem.Name = "openSettingsFileToolStripMenuItem";
            openSettingsFileToolStripMenuItem.Size = new Size(148, 22);
            openSettingsFileToolStripMenuItem.Text = "打开配置文件";
            openSettingsFileToolStripMenuItem.Click += openSettingsFileToolStripMenuItem_Click;
            // 
            // reloadToolStripMenuItem
            // 
            reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            reloadToolStripMenuItem.Size = new Size(148, 22);
            reloadToolStripMenuItem.Text = "重新加载";
            reloadToolStripMenuItem.Click += reloadToolStripMenuItem_Click;
            // 
            // quickLoadToolStripMenuItem
            // 
            quickLoadToolStripMenuItem.Name = "quickLoadToolStripMenuItem";
            quickLoadToolStripMenuItem.Size = new Size(68, 21);
            quickLoadToolStripMenuItem.Text = "快速读取";
            quickLoadToolStripMenuItem.DropDownOpening += quickLoadToolStripMenuItem_DropDownOpening;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { onscreenHelpToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 21);
            helpToolStripMenuItem.Text = "帮助";
            // 
            // onscreenHelpToolStripMenuItem
            // 
            onscreenHelpToolStripMenuItem.Name = "onscreenHelpToolStripMenuItem";
            onscreenHelpToolStripMenuItem.Size = new Size(145, 22);
            onscreenHelpToolStripMenuItem.Text = "帮助贴图 (H)";
            onscreenHelpToolStripMenuItem.Click += onscreenHelpCtrlHToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.MenuBar;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, quickLoadToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(1148, 27);
            menuStrip1.TabIndex = 27;
            menuStrip1.Text = "menuStrip1";
            // 
            // coloredPanel
            // 
            coloredPanel.BackColor = Color.Red;
            coloredPanel.BorderStyle = BorderStyle.Fixed3D;
            coloredPanel.Location = new Point(629, 33);
            coloredPanel.Margin = new Padding(4);
            coloredPanel.Name = "coloredPanel";
            coloredPanel.Size = new Size(41, 46);
            coloredPanel.TabIndex = 25;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(679, 33);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(0, 17);
            label5.TabIndex = 26;
            // 
            // pressBtn
            // 
            pressBtn.Location = new Point(461, 31);
            pressBtn.Margin = new Padding(4);
            pressBtn.Name = "pressBtn";
            pressBtn.Size = new Size(161, 48);
            pressBtn.TabIndex = 31;
            pressBtn.Text = "观察按键按下";
            pressBtn.UseVisualStyleBackColor = true;
            pressBtn.Click += pressBtn_Click;
            // 
            // timeline
            // 
            timeline.Anchor = AnchorStyles.Bottom;
            timeline.BananaShowerPercent = new List<double>[]
    {
    null,
    null
    };
            timeline.Location = new Point(129, 701);
            timeline.Margin = new Padding(4);
            timeline.Name = "timeline";
            timeline.RightToLeft = RightToLeft.No;
            timeline.Size = new Size(888, 20);
            timeline.TabIndex = 1;
            timeline.Value = 0F;
            timeline.MouseClick += timeline_MouseClick;
            // 
            // panel3
            // 
            panel3.Controls.Add(EZRadioButton);
            panel3.Controls.Add(HRRadioButton);
            panel3.Controls.Add(NoneRadioButton);
            panel3.Controls.Add(label6);
            panel3.Location = new Point(698, 31);
            panel3.Name = "panel3";
            panel3.Size = new Size(437, 48);
            panel3.TabIndex = 32;
            // 
            // EZRadioButton
            // 
            EZRadioButton.AutoSize = true;
            EZRadioButton.Location = new Point(200, 12);
            EZRadioButton.Name = "EZRadioButton";
            EZRadioButton.Size = new Size(40, 21);
            EZRadioButton.TabIndex = 33;
            EZRadioButton.Text = "EZ";
            EZRadioButton.UseVisualStyleBackColor = true;
            EZRadioButton.CheckedChanged += EZRadioButton_CheckedChanged;
            // 
            // HRRadioButton
            // 
            HRRadioButton.AutoSize = true;
            HRRadioButton.Location = new Point(142, 12);
            HRRadioButton.Name = "HRRadioButton";
            HRRadioButton.Size = new Size(43, 21);
            HRRadioButton.TabIndex = 2;
            HRRadioButton.Text = "HR";
            HRRadioButton.UseVisualStyleBackColor = true;
            HRRadioButton.CheckedChanged += HRRadioButton_CheckedChanged;
            // 
            // NoneRadioButton
            // 
            NoneRadioButton.AutoSize = true;
            NoneRadioButton.Checked = true;
            NoneRadioButton.Location = new Point(78, 12);
            NoneRadioButton.Name = "NoneRadioButton";
            NoneRadioButton.Size = new Size(58, 21);
            NoneRadioButton.TabIndex = 1;
            NoneRadioButton.TabStop = true;
            NoneRadioButton.Text = "None";
            NoneRadioButton.UseVisualStyleBackColor = true;
            NoneRadioButton.CheckedChanged += NoneRadioButton_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 14);
            label6.Name = "label6";
            label6.Size = new Size(36, 17);
            label6.TabIndex = 0;
            label6.Text = "Mod";
            // 
            // Canvas
            // 
            Canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Canvas.BackColor = Color.Black;
            Canvas.CatcherAreaHeight = 0F;
            Canvas.Location = new Point(129, 113);
            Canvas.Margin = new Padding(4, 4, 4, 4);
            Canvas.Name = "Canvas";
            Canvas.ShowHelp = 0;
            Canvas.Size = new Size(888, 564);
            Canvas.TabIndex = 33;
            Canvas.VSync = false;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Right;
            panel4.Controls.Add(BananaDifficulty1_1RadioButton);
            panel4.Controls.Add(BananaDifficulty1_2RadioButton);
            panel4.Controls.Add(BananaDifficulty1_3RadioButton);
            panel4.Controls.Add(BananaDifficulty3RadioButton);
            panel4.Controls.Add(BananaDifficulty2RadioButton);
            panel4.Controls.Add(BananaDifficulty1RadioButton);
            panel4.Controls.Add(label7);
            panel4.Location = new Point(1031, 246);
            panel4.Name = "panel4";
            panel4.Size = new Size(104, 196);
            panel4.TabIndex = 34;
            // 
            // BananaDifficulty1_1RadioButton
            // 
            BananaDifficulty1_1RadioButton.AutoSize = true;
            BananaDifficulty1_1RadioButton.Location = new Point(15, 37);
            BananaDifficulty1_1RadioButton.Name = "BananaDifficulty1_1RadioButton";
            BananaDifficulty1_1RadioButton.Size = new Size(86, 21);
            BananaDifficulty1_1RadioButton.TabIndex = 6;
            BananaDifficulty1_1RadioButton.Text = "放手 (未知)";
            BananaDifficulty1_1RadioButton.UseVisualStyleBackColor = true;
            BananaDifficulty1_1RadioButton.CheckedChanged += BananaDifficulty1_1RadioButton_CheckedChanged;
            // 
            // BananaDifficulty1_2RadioButton
            // 
            BananaDifficulty1_2RadioButton.AutoSize = true;
            BananaDifficulty1_2RadioButton.Location = new Point(15, 64);
            BananaDifficulty1_2RadioButton.Name = "BananaDifficulty1_2RadioButton";
            BananaDifficulty1_2RadioButton.Size = new Size(86, 21);
            BananaDifficulty1_2RadioButton.TabIndex = 5;
            BananaDifficulty1_2RadioButton.Text = "睡觉 (未知)";
            BananaDifficulty1_2RadioButton.UseVisualStyleBackColor = true;
            BananaDifficulty1_2RadioButton.CheckedChanged += BananaDifficulty1_2RadioButton_CheckedChanged;
            // 
            // BananaDifficulty1_3RadioButton
            // 
            BananaDifficulty1_3RadioButton.AutoSize = true;
            BananaDifficulty1_3RadioButton.Location = new Point(15, 91);
            BananaDifficulty1_3RadioButton.Name = "BananaDifficulty1_3RadioButton";
            BananaDifficulty1_3RadioButton.Size = new Size(86, 21);
            BananaDifficulty1_3RadioButton.TabIndex = 4;
            BananaDifficulty1_3RadioButton.Text = "轻松 (未知)";
            BananaDifficulty1_3RadioButton.UseVisualStyleBackColor = true;
            BananaDifficulty1_3RadioButton.CheckedChanged += BananaDifficulty1_3RadioButton_CheckedChanged;
            // 
            // BananaDifficulty3RadioButton
            // 
            BananaDifficulty3RadioButton.AutoSize = true;
            BananaDifficulty3RadioButton.Checked = true;
            BananaDifficulty3RadioButton.Location = new Point(15, 172);
            BananaDifficulty3RadioButton.Name = "BananaDifficulty3RadioButton";
            BananaDifficulty3RadioButton.Size = new Size(86, 21);
            BananaDifficulty3RadioButton.TabIndex = 3;
            BananaDifficulty3RadioButton.TabStop = true;
            BananaDifficulty3RadioButton.Text = "较难 (未知)";
            BananaDifficulty3RadioButton.UseVisualStyleBackColor = true;
            BananaDifficulty3RadioButton.CheckedChanged += BananaDifficulty3RadioButton_CheckedChanged;
            // 
            // BananaDifficulty2RadioButton
            // 
            BananaDifficulty2RadioButton.AutoSize = true;
            BananaDifficulty2RadioButton.Location = new Point(15, 145);
            BananaDifficulty2RadioButton.Name = "BananaDifficulty2RadioButton";
            BananaDifficulty2RadioButton.Size = new Size(86, 21);
            BananaDifficulty2RadioButton.TabIndex = 2;
            BananaDifficulty2RadioButton.Text = "一般 (未知)";
            BananaDifficulty2RadioButton.UseVisualStyleBackColor = true;
            BananaDifficulty2RadioButton.CheckedChanged += BananaDifficulty2RadioButton_CheckedChanged;
            // 
            // BananaDifficulty1RadioButton
            // 
            BananaDifficulty1RadioButton.AutoSize = true;
            BananaDifficulty1RadioButton.Location = new Point(15, 118);
            BananaDifficulty1RadioButton.Name = "BananaDifficulty1RadioButton";
            BananaDifficulty1RadioButton.Size = new Size(86, 21);
            BananaDifficulty1RadioButton.TabIndex = 1;
            BananaDifficulty1RadioButton.Text = "简单 (未知)";
            BananaDifficulty1RadioButton.UseVisualStyleBackColor = true;
            BananaDifficulty1RadioButton.CheckedChanged += BananaDifficulty1RadioButton_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(0, 9);
            label7.Name = "label7";
            label7.Size = new Size(56, 17);
            label7.TabIndex = 0;
            label7.Text = "香蕉路线";
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1148, 864);
            Controls.Add(panel4);
            Controls.Add(Canvas);
            Controls.Add(panel3);
            Controls.Add(pressBtn);
            Controls.Add(label5);
            Controls.Add(coloredPanel);
            Controls.Add(cursorColorPanel);
            Controls.Add(volumeBarLabel);
            Controls.Add(label4);
            Controls.Add(volumeBar);
            Controls.Add(songTimeLabel);
            Controls.Add(unloadBtn);
            Controls.Add(panel2);
            Controls.Add(viewBtn);
            Controls.Add(timeline);
            Controls.Add(panel1);
            Controls.Add(timeWindowLabel);
            Controls.Add(label1);
            Controls.Add(timeWindowBar);
            Controls.Add(dataBtn);
            Controls.Add(playBtn);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "osu! Catch Previewer";
            FormClosing += MainForm_FormClosing;
            Load += Main_Load;
            DragDrop += Main_DragDrop;
            DragEnter += Main_DragEnter;
            ((System.ComponentModel.ISupportInitialize)timeWindowBar).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Timeline timeline;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button dataBtn;
        private System.Windows.Forms.TrackBar timeWindowBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timeWindowLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton speed075Radio;
        private System.Windows.Forms.RadioButton speed400Radio;
        private System.Windows.Forms.RadioButton speed200Radio;
        private System.Windows.Forms.RadioButton speed150Radio;
        private System.Windows.Forms.RadioButton speed100Radio;
        private System.Windows.Forms.RadioButton speed050Radio;
        private System.Windows.Forms.RadioButton speed025Radio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button viewBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton replay7Radio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton replay6Radio;
        private System.Windows.Forms.RadioButton replay5Radio;
        private System.Windows.Forms.RadioButton replay4Radio;
        private System.Windows.Forms.RadioButton replay2Radio;
        private System.Windows.Forms.RadioButton replay1Radio;
        private System.Windows.Forms.RadioButton replay3Radio;
        private System.Windows.Forms.Button unloadBtn;
        private System.Windows.Forms.Label songTimeLabel;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label volumeBarLabel;
        private System.Windows.Forms.Panel cursorColorPanel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSettingsFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem onscreenHelpToolStripMenuItem;
        private System.Windows.Forms.Panel coloredPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button pressBtn;
        private ToolStripMenuItem OpenBeatmapToolStripMenuItem;
        private Panel panel3;
        private RadioButton NoneRadioButton;
        private Label label6;
        private RadioButton EZRadioButton;
        private RadioButton HRRadioButton;
        private ToolStripMenuItem 获取osu当前谱面ToolStripMenuItem;
        private Canvas Canvas;
        private Panel panel4;
        private Label label7;
        public RadioButton BananaDifficulty3RadioButton;
        public RadioButton BananaDifficulty2RadioButton;
        public RadioButton BananaDifficulty1RadioButton;
        public RadioButton BananaDifficulty1_1RadioButton;
        public RadioButton BananaDifficulty1_2RadioButton;
        public RadioButton BananaDifficulty1_3RadioButton;
    }
}

