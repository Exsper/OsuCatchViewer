using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using osu.Game.Tests.Beatmaps;
using OsuCatchViewer.CatchAPI;
using osu.Game.Beatmaps;

namespace OsuCatchViewer
{
    public partial class TestForm : Form
    {

        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            IBeatmap beatmap = CatchBeatmapAPI.GetBeatmap("1.osu", new string[] {"EZ"});
            Console.Write(beatmap);
        }
    }
}
