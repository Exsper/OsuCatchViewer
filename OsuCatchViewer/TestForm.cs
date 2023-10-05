using osu.Game.Beatmaps;
using OsuCatchViewer.CatchAPI;

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
            IBeatmap beatmap = CatchBeatmapAPI.GetBeatmap("1.osu", new string[] { "EZ" });
            Console.Write(beatmap);
        }
    }
}
