using System.Drawing;
using System.Windows.Forms;

namespace OsuCatchViewer
{
    public class Timeline : Control
    {
        public float Value
        {
            get { return this.value; }
            set
            {
                if (value != this.value)
                {
                    this.Invalidate();
                    this.value = value;
                }
            }
        }
        public List<double>[] BananaShowerPercent
        {
            get { return this.bananaShowerPercent; } 
            set
            {
                if (value != this.bananaShowerPercent)
                {
                    this.Invalidate();
                    this.bananaShowerPercent = value;
                }
            }
        }

        private Pen pen;
        private Brush backgroundBrush;
        private Brush foregroundBrush;
        private Brush bananaBrush;
        private float value;
        private List<double>[] bananaShowerPercent;

        public Timeline() : base()
        {
            this.Value = 0;
            this.Width = 700;
            this.Height = 15;
            this.pen = new Pen(Color.Black);
            this.backgroundBrush = new SolidBrush(Color.LightGray);
            this.foregroundBrush = new SolidBrush(Color.Black);
            this.bananaBrush = new SolidBrush(Color.Yellow);
            this.bananaShowerPercent = new List<double>[2];
            this.Paint += Timeline_Paint;

        }

        private void Timeline_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(this.backgroundBrush, 0, this.Height * 2 / 5, this.Width, this.Height / 5);
            if (this.bananaShowerPercent[0] != null)
            {
                for (int i = 0; i < this.bananaShowerPercent[0].Count; i++)
                {
                    int x = (int)(this.bananaShowerPercent[0][i] * this.Width);
                    int width = (int)(this.bananaShowerPercent[1][i] * this.Width) - x;
                    e.Graphics.FillRectangle(this.bananaBrush, x, 0, width, this.Height);
                }
            }
            int q = (int)(this.Value * this.Width);
            e.Graphics.FillRectangle(this.foregroundBrush, q, 0, 5, this.Height);
        }
    }
}
