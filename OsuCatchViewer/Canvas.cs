using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Catch.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using Color = OpenTK.Graphics.Color4;
using Rectangle = System.Drawing.Rectangle;

namespace OsuCatchViewer
{
    public class Canvas : OpenTK.GLControl
    {

        public static Color[] Color_Cursor = new Color[7] { Color.Red, Color.Cyan, Color.Lime, Color.Yellow, Color.Magenta, new Color(128, 128, 255, 255), Color.Honeydew };
        public ViewerManager viewerManager;

        public float CatcherAreaHeight { get; set; }

        private Texture2D? nodeTexture;
        private Texture2D? cursorTexture;
        private Texture2D? hitCircleTexture;
        private Texture2D? helpTexture;
        private Texture2D? DropTexture;
        private Texture2D? BananaTexture;

        public byte ShowHelp { get; set; }


        public Canvas()
            : base()
        {
            this.MakeCurrent();
            this.Paint += Canvas_Paint;
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            if (Keyboard.GetState().IsKeyDown(Key.H))
            {
                this.ShowHelp = 1;
            }
            else if (this.ShowHelp != 2)
            {
                this.ShowHelp = 0;
            }
            if (ShowHelp != 0 && this.helpTexture != null)
            {
                DrawHelp();
            }

            if (viewerManager != null)
            {
                this.Tick();

                this.DrawJudgementLine();
                this.Draw();

            }
            this.SwapBuffers();
            System.Threading.Thread.Sleep(16);
            this.Resize += Canvas_Resize;
        }

        private void Canvas_Resize(object? sender, EventArgs? e)
        {
            int w = this.Size.Width;
            int h = this.Size.Height;
            int x = 0;
            int y = 0;
            if (w * 3 / 4 > h)
            {
                w = h * 4 / 3;
                x = (this.Size.Width - w) / 2;
            }
            if (h * 4 / 3 > w)
            {
                h = w * 3 / 4;
                y = (this.Size.Height - h) / 2;
            }
            GL.Viewport(x, y, w, h);
        }

        public void Tick()
        {
            viewerManager.Tick();
            if (Keyboard.GetState().IsKeyDown(Key.D) || Keyboard.GetState().IsKeyDown(Key.Right))
            {
                viewerManager.State_PlaybackFlow = 2;
            }
            else if (Keyboard.GetState().IsKeyDown(Key.A) || Keyboard.GetState().IsKeyDown(Key.Left))
            {
                viewerManager.State_PlaybackFlow = 1;
            }
            else if (viewerManager.State_PlaybackFlow != 3)
            {
                viewerManager.State_PlaybackFlow = 0;
            }
            MainForm.self.SetPlayPause("Play");
            if (viewerManager.State_PlaybackFlow == 0)
            {
                viewerManager.SongPlayer.Pause();
            }
            else if (viewerManager.State_PlaybackFlow == 1)
            {
                viewerManager.SongPlayer.Pause();
                //this.songPlayer.JumpTo((long)(this.songPlayer.SongTime - (gameTime.ElapsedGameTime.Milliseconds * this.State_PlaybackSpeed)));
                viewerManager.SongPlayer.JumpTo((long)(viewerManager.SongPlayer.SongTime - ((1000.0 / 60.0) * viewerManager.State_PlaybackSpeed)));
            }
            else if (viewerManager.State_PlaybackFlow == 2)
            {
                viewerManager.SongPlayer.Play();
            }
            else if (viewerManager.State_PlaybackFlow == 3)
            {
                MainForm.self.SetPlayPause("Pause");
                viewerManager.SongPlayer.Play();
            }
            if (viewerManager.MaxSongTime != 0)
            {
                MainForm.self.SetTimelinePercent((float)viewerManager.SongPlayer.SongTime / (float)viewerManager.MaxSongTime);
            }
            MainForm.self.SetSongTimeLabel((int)viewerManager.SongPlayer.SongTime);
        }

        public void Init()
        {
            // 盘子区间
            this.CatcherAreaHeight = 384f - 350f;

            ShowHelp = 2;

            this.nodeTexture = this.TextureFromFile(MainForm.Path_Img_EditorNode);
            this.cursorTexture = this.TextureFromFile(MainForm.Path_Img_Cursor);
            this.hitCircleTexture = this.TextureFromFile(MainForm.Path_Img_Hitcircle);
            this.helpTexture = this.TextureFromFile(MainForm.Path_Img_Help);
            this.DropTexture = this.TextureFromFile(MainForm.Path_Img_Drop);
            this.BananaTexture = this.TextureFromFile(MainForm.Path_Img_Banana);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.AlphaTest);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            this.Canvas_Resize(this, null);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Vector2 border = new Vector2(4.0f, 3.0f) * 32.0f;
            GL.Ortho(-border.X, 512.0 + border.X, 384.0 + border.Y, -border.Y, 0.0, 1.0);
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        private Texture2D? TextureFromFile(string path)
        {
            try
            {
                return new Texture2D(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));
            }
            catch
            {
                MainForm.ErrorMessage(String.Format("Could not the load the image found at \"{0}\", please make sure it exists and is a valid .png image.\nRedownloading the .zip file will also restore lost images and update new ones.", path));
                return null;
            }
        }

        /*
        private void UnloadContent()
        {
            this.nodeTexture.Dispose();
            this.cursorTexture.Dispose();
            this.hitCircleTexture.Dispose();
            this.helpTexture.Dispose();
        }
        */



        private void MulFlipMatrix()
        {
            double[] flipMatrix = {
                1.0, 0.0, 0.0, 0.0,
                0.0, -1.0, 0.0, 384.0,
                0.0, 0.0, 1.0, 0.0,
                0.0, 0.0, 0.0, 1.0,
            };
            GL.MultTransposeMatrix(flipMatrix);
        }

        private void Draw_Mode_Data(List<List<ReplayAPI.ReplayFrame>> nearbyFrames, int replaySelectedIndex)
        {
            Vector2 currentPos = Vector2.Zero;
            Vector2 lastPos = new Vector2(-222, 0);
            for (int i = 0; i < nearbyFrames[replaySelectedIndex].Count; i++)
            {
                ReplayAPI.ReplayFrame currentFrame = nearbyFrames[replaySelectedIndex][i];
                float alpha = i / (float)nearbyFrames[replaySelectedIndex].Count;
                currentPos = new Vector2(currentFrame.X, currentFrame.Y);
                currentPos.Y = 384 - this.CatcherAreaHeight;
                if (lastPos.X != -222)
                {
                    this.DrawLine(lastPos, currentPos, new Color(1.0f, 0.0f, 0.0f, alpha));
                }
                Color nodeColor = Color.Gray;
                if (currentFrame.Keys.HasFlag(ReplayAPI.Keys.K1))
                {
                    nodeColor = Color.Cyan;
                }
                else if (currentFrame.Keys.HasFlag(ReplayAPI.Keys.K2))
                {
                    nodeColor = Color.Magenta;
                }
                else if (currentFrame.Keys.HasFlag(ReplayAPI.Keys.M1))
                {
                    nodeColor = Color.Lime;
                }
                else if (currentFrame.Keys.HasFlag(ReplayAPI.Keys.M2))
                {
                    nodeColor = Color.Yellow;
                }
                this.nodeTexture.Draw(currentPos - new Vector2(5, 5), Vector2.Zero, nodeColor);
                lastPos = currentPos;
            }
        }

        private void Draw_Mode_Press(List<List<ReplayAPI.ReplayFrame>> nearbyFrames, int replaySelectedIndex)
        {
            for (int i = 1; i < nearbyFrames[replaySelectedIndex].Count; i++)
            {
                var lastFrame = nearbyFrames[replaySelectedIndex][i - 1];
                lastFrame.Y = 384 - CatcherAreaHeight;
                var currentFrame = nearbyFrames[replaySelectedIndex][i];
                currentFrame.Y = 384 - CatcherAreaHeight;
                float alpha = i / (float)nearbyFrames[replaySelectedIndex].Count;
                var lastPos = new Vector2(lastFrame.X, lastFrame.Y);
                var currentPos = new Vector2(currentFrame.X, currentFrame.Y);
                this.DrawLine(lastPos, currentPos, new Color(1.0f, 0.0f, 0.0f, alpha));
                Color nodeColor = Color.Black;
                if (currentFrame.Keys.HasFlag(ReplayAPI.Keys.K1) && !lastFrame.Keys.HasFlag(ReplayAPI.Keys.K1))
                {
                    nodeColor = Color.Yellow;
                }
                else if (currentFrame.Keys.HasFlag(ReplayAPI.Keys.K2) && !lastFrame.Keys.HasFlag(ReplayAPI.Keys.K2))
                {
                    nodeColor = Color.Yellow;
                }
                else if (currentFrame.Keys.HasFlag(ReplayAPI.Keys.M1) && !lastFrame.Keys.HasFlag(ReplayAPI.Keys.M1))
                {
                    nodeColor = Color.Yellow;
                }
                else if (currentFrame.Keys.HasFlag(ReplayAPI.Keys.M2) && !lastFrame.Keys.HasFlag(ReplayAPI.Keys.M2))
                {
                    nodeColor = Color.Yellow;
                }
                else if (currentFrame.Keys != lastFrame.Keys && !currentFrame.Keys.HasFlag(ReplayAPI.Keys.K1) && !currentFrame.Keys.HasFlag(ReplayAPI.Keys.K2) && !currentFrame.Keys.HasFlag(ReplayAPI.Keys.M1) && !currentFrame.Keys.HasFlag(ReplayAPI.Keys.M2))
                {
                    nodeColor = Color.Gray;
                }
                if (nodeColor != Color.Black)
                {
                    this.nodeTexture.Draw(currentPos - new Vector2(5, 5), Vector2.Zero, nodeColor);
                }
            }
        }

        private void Draw_Mode_View(List<List<ReplayAPI.ReplayFrame>> nearbyFrames, double songTime, float circleSize)
        {
            for (int i = 0; i < 7; i++)
            {
                if (nearbyFrames[i] != null && nearbyFrames[i].Count >= 1)
                {
                    Vector2 cursorPos = this.GetInterpolatedFrame(i, nearbyFrames, songTime);
                    float diameter = 106.75f * (1.0f - 0.7f * (circleSize - 5) / 5);
                    cursorPos.Y = 384f - this.CatcherAreaHeight + diameter / 2 - 8 * (1.0f - 0.7f * (circleSize - 5) / 5);
                    this.cursorTexture.Draw(cursorPos, diameter, diameter, new Vector2(diameter * 0.5f), Canvas.Color_Cursor[i]);
                }
            }
        }

        private Vector2 GetInterpolatedFrame(int replayNum, List<List<ReplayAPI.ReplayFrame>> nearbyFrames, double songTime)
        {
            // gets the cursor position at a given time based on the replay data
            // if between two points, interpolate between
            Vector2 p1 = new Vector2(nearbyFrames[replayNum][0].X, nearbyFrames[replayNum][0].Y);
            Vector2 p2 = Vector2.Zero;
            int t1 = nearbyFrames[replayNum][0].Time;
            int t2 = t1 + 1;
            // check to make sure it is not the final replay frame in the replay
            if (nearbyFrames[replayNum].Count > 1)
            {
                p2.X = nearbyFrames[replayNum][1].X;
                p2.Y = nearbyFrames[replayNum][1].Y;
                t2 = nearbyFrames[replayNum][1].Time;
                // While I don't think there would ever be two replay frames at the same time,
                // this will prevent ever dividing by zero when calculating 'm'
                if (t1 == t2)
                {
                    t2++;
                }
            }
            // 't' is the percentage (from 0.0 to 1.0) of time completed from one point to other
            float t = ((float)songTime - t1) / (float)(t2 - t1);
            // Linearly interpolate between point 1 and point 2 based off the time percentage 'm'
            return new Vector2(p1.X + (p2.X - p1.X) * t, p1.Y + (p2.Y - p1.Y) * t);
        }


        public void Draw()
        {
            if (this.nodeTexture == null || this.cursorTexture == null) return;
            int circleDiameter = (int)(108.848 - viewerManager.Beatmap.Difficulty.CircleSize * 8.9646);
            float fruitSpeed = 384 / viewerManager.ApproachTime;
            for (int b = viewerManager.NearbyHitObjects.Count - 1; b >= 0; b--)
            {
                PalpableCatchHitObject hitObject = viewerManager.NearbyHitObjects[b];
                // the song time relative to the hitobject start time
                float diff = (float)(hitObject.StartTime - viewerManager.SongPlayer.SongTime);
                // 0=在顶端 1=在判定线上 >1=超过判定线
                float alpha = 1.0f;
                if (diff < viewerManager.ApproachTime * viewerManager.State_ARMul && diff > -(viewerManager.ApproachTime * (viewerManager.State_ARMul - 1)))
                {
                    alpha = 1 - (diff / (float)viewerManager.ApproachTime);
                    this.DrawHitcircle(hitObject, alpha, circleDiameter);
                }
            }
            if (viewerManager.State_PlaybackMode == 0)
            {
                Draw_Mode_Data(viewerManager.NearbyFrames, viewerManager.State_ReplaySelected);
            }
            else if (viewerManager.State_PlaybackMode == 2 && viewerManager.NearbyFrames.Count > 2)
            {
                Draw_Mode_Press(viewerManager.NearbyFrames, viewerManager.State_ReplaySelected);
            }
            else if (viewerManager.State_PlaybackMode == 1)
            {
                Draw_Mode_View(viewerManager.NearbyFrames, viewerManager.SongPlayer.SongTime, viewerManager.Beatmap.Difficulty.CircleSize);
            }
        }


        public void DrawHelp ()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(0.0, 1.0, 1.0, 0.0, 0.0, 1.0);
            helpTexture.Draw(Vector2.Zero, 1.0f, 1.0f, Vector2.Zero, Color.White);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
        }



        private void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            GL.Disable(EnableCap.Texture2D);
            GL.Color4(color);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(start.X, start.Y);
            GL.Vertex2(end.X, end.Y);
            GL.End();
            GL.Enable(EnableCap.Texture2D);
        }

        private void DrawHitcircle(PalpableCatchHitObject hitObject, float alpha, int circleDiameter)
        {
            Vector2 pos = new Vector2(hitObject.EffectiveX, 384 * alpha - this.CatcherAreaHeight);
            if (hitObject is TinyDroplet)
            {
                if (hitObject.HyperDash)
                    this.DrawHitcircle(DropTexture, pos, (int)(circleDiameter * hitObject.Scale / 2), new Color(1.0f, 0f, 0f, 1.0f));
                else
                    this.DrawHitcircle(DropTexture, pos, (int)(circleDiameter * hitObject.Scale / 2), new Color(1.0f, 1.0f, 1.0f, 1.0f));
            }
            else if (hitObject is Droplet)
            {
                if (hitObject.HyperDash)
                    this.DrawHitcircle(DropTexture, pos, (int)(circleDiameter * hitObject.Scale), new Color(1.0f, 0f, 0f, 1.0f));
                else
                    this.DrawHitcircle(DropTexture, pos, (int)(circleDiameter * hitObject.Scale), new Color(1.0f, 1.0f, 1.0f, 1.0f));
            }
            else if(hitObject is Fruit)
            {
                if (hitObject.HyperDash)
                    this.DrawHitcircle(hitCircleTexture, pos, circleDiameter, new Color(1.0f, 0f, 0f, 1.0f));
                else
                    this.DrawHitcircle(hitCircleTexture, pos, circleDiameter, new Color(1.0f, 1.0f, 1.0f, 1.0f));
            }
            else if (hitObject is Banana) this.DrawHitcircle(BananaTexture, pos, circleDiameter, new Color(1.0f, 1.0f, 0f, 1.0f));
        }

        private void DrawHitcircle(Texture2D texture, Vector2 pos, int diameter, Color color)
        {
            texture.Draw(pos, diameter, diameter, new Vector2(diameter * 0.5f), color);
        }

        private void DrawJudgementLine()
        {
            Vector2 rp0 = new Vector2(0, 384 - CatcherAreaHeight);
            Vector2 rp1 = new Vector2(512, 384 - CatcherAreaHeight);
            DrawLine(rp0, rp1, Color.White);
        }


        
    }
}
