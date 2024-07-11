using osu.Game.Rulesets.Catch.Objects;
using Color = OpenTK.Graphics.Color4;

namespace OsuCatchViewer.CatchAPI
{

    public class HasColorHitObject
    {
        private PalpableCatchHitObject HitObject { get; set; }
        private Color Color { get; set; }
        public HasColorHitObject(PalpableCatchHitObject hitObject, Color color)
        {
            this.HitObject = hitObject;
            this.Color = color;
        }

        public PalpableCatchHitObject GetHitObject() { return this.HitObject; }

        public Color GetColor() { return this.Color; }
    }

    public class BananaGroup
    {
        public int FirstBananaIndex { get; set; }
        public PalpableCatchHitObject StartObj { get; set; }
        public PalpableCatchHitObject EndObj { get; set; }
        public List<PalpableCatchHitObject> hasTipBananas { get; set; }

        public double CATCHER_BASE_SIZE { get; set; }
        public double ALLOWED_CATCH_RANGE { get; set; }
        public double calculateScale { get; set; }
        public double catchWidth { get; set; }
        public double halfCatcherWidth { get; set; }
        public double BASE_DASH_SPEED { get; set; }
        public double BASE_WALK_SPEED { get; set; }

        public BananaGroup(int firstBananaIndex)
        {
            hasTipBananas = new List<PalpableCatchHitObject>();
            FirstBananaIndex = firstBananaIndex;
        }

        public void AddBanana(PalpableCatchHitObject banana)
        {
            hasTipBananas.Add(banana);
        }

        /// <summary>
        /// 有向最长路径算法
        /// </summary>
        public List<Color> LongestPath(double CircleSize, ref int catchBananaCount, int difficultyLevel)
        {
            this.CATCHER_BASE_SIZE = 106.75;
            this.ALLOWED_CATCH_RANGE = 0.8;
            this.calculateScale = 1.0 - 0.7 * (CircleSize - 5.0) / 5.0;
            this.catchWidth = CATCHER_BASE_SIZE * Math.Abs(calculateScale) * ALLOWED_CATCH_RANGE;
            this.halfCatcherWidth = catchWidth / 2.0;
            this.halfCatcherWidth /= ALLOWED_CATCH_RANGE;    // ???
            this.BASE_DASH_SPEED = 1.0;
            this.BASE_WALK_SPEED = 0.5;

            // 计算最长路径
            // 设开始节点为StartObj，结束节点为EndObj
            // 香蕉索引为1-n，开始节点索引为0，结束节点索引为n+1，一共n+2项
            int n = hasTipBananas.Count;
            // 权重
            double[][] G = new double[n + 2][];
            int[] choice = new int[n + 2];
            double[] dp = new double[n + 2];
            int[] needdash = new int[n + 2];
            // 初始化
            for (int ni = 0; ni <= n + 1; ni++)
            {
                G[ni] = new double[n + 2];
                for (int nj = 0; nj < n + 2; nj++)
                {
                    G[ni][nj] = -1;
                }
                choice[ni] = -1;
                dp[ni] = -1;
                needdash[ni] = -1;
            }
            // 开始
            // 如果开始有果子，则权重为1.5，必须接到
            if (StartObj == null)
            {
                for (int ni = 1; ni <= n + 1; ni++) G[0][ni] = 1;
            }
            else
            {
                for (int ni = 1; ni <= n; ni++)
                {
                    G[0][ni] = CatchEffectiveness(StartObj, hasTipBananas[ni - 1], difficultyLevel) * 1.5;
                }
                G[0][n + 1] = (EndObj != null) ? CatchEffectiveness(StartObj, EndObj, difficultyLevel) * 1.5 : 1;
            }
            // 结束
            // 如果后续有果子，则权重为1.5，必须接到
            if (EndObj == null)
            {
                for (int ni = 0; ni <= n; ni++) G[ni][n + 1] = 1;
            }
            else
            {
                G[0][n + 1] = (StartObj != null) ? CatchEffectiveness(StartObj, EndObj, difficultyLevel) * 1.5 : 1;
                for (int ni = 1; ni <= n; ni++)
                {
                    G[ni][n + 1] = CatchEffectiveness(hasTipBananas[ni - 1], EndObj, difficultyLevel) * 1.5;
                }
            }
            // 香蕉间
            for (int ni = 1; ni <= n; ni++)
            {
                for (int nj = ni + 1; nj <= n; nj++)
                {
                    G[ni][nj] = CatchEffectiveness(hasTipBananas[ni - 1], hasTipBananas[nj - 1], difficultyLevel);
                }
            }

            double maxDP = DP(0, n, ref dp, ref G, ref choice, ref needdash);

            List<Color> colors = new List<Color>(n);
            for (int ni = 0; ni < n; ni++)
            {
                colors.Add(Color.Yellow);
            }
            // 最优节点涂上白色，需加速涂粉色
            int nk = 0;
            while (nk <= (n + 1) && (choice[nk]) != -1)
            {
                nk = choice[nk];
                if ((nk - 1) < n)
                {
                    catchBananaCount += 1;
                    if (needdash[nk] > 0)
                        colors[nk - 1] = new Color(255, 128, 128, 255);
                    else
                        colors[nk - 1] = new Color(255, 255, 255, 255);
                }
            }

            // Console.WriteLine("total " + catchBananaCount + " bananas catched.");
            return colors;
        }

        /// <summary>
        /// TODO: 权重算法
        /// </summary>
        /// <param name="before"></param> 后一个物件
        /// <param name="after"></param> 前一个物件
        /// <returns></returns>
        private double CatchEffectiveness(PalpableCatchHitObject before, PalpableCatchHitObject after, int difficultyLevel)
        {
            // if 香蕉间的距离<=盘子速度*间隔时间 + 1/2 * 盘子大小  路径赋值1
            // else if 香蕉间的距离<=盘子速度*间隔时间 + 盘子大小  路径赋值 (盘子速度*间隔时间 + 盘子大小 - 香蕉间的距离) / (1/2 * 盘子大小)
            // else if 香蕉间的距离>盘子速度*间隔时间 + 盘子大小  断路
            double bspace = Math.Abs(before.EffectiveX - after.EffectiveX);
            double btime = after.StartTime - before.StartTime - 1000 / 60 / 4;
            if (bspace <= btime * this.BASE_WALK_SPEED + this.halfCatcherWidth) return 1;
            else if (bspace <= btime * this.BASE_WALK_SPEED + this.catchWidth) return ((btime * this.BASE_WALK_SPEED + this.catchWidth - bspace) / this.halfCatcherWidth) / 2 + 0.5;
            else if (difficultyLevel >= 2 && bspace <= btime * this.BASE_DASH_SPEED + this.halfCatcherWidth) return 0.5;
            else if (difficultyLevel >= 3 && bspace <= btime * this.BASE_DASH_SPEED + this.catchWidth) return (btime * this.BASE_DASH_SPEED + this.catchWidth - bspace) / this.halfCatcherWidth / 2;
            else return -1;
        }

        private double DP(int ni, int n, ref double[] dp, ref double[][] G, ref int[] choice, ref int[] needdash)
        {
            if (dp[ni] > 0) return dp[ni];
            for (int nj = ni + 1; nj <= n + 1; nj++)
            {
                if (G[ni][nj] > 0)
                {
                    double temp = DP(nj, n, ref dp, ref G, ref choice, ref needdash) + G[ni][nj];
                    if (temp > dp[ni])
                    {
                        dp[ni] = temp;
                        choice[ni] = nj;
                        needdash[ni] = (G[ni][nj] <= 0.5) ? 1 : 0;
                    }
                }
            }
            return dp[ni];
        }
    }

    public static class BananaTipBuilder
    {
        public static List<BananaGroup> GetBananaGroups(List<PalpableCatchHitObject> palpableCatchHitObjects)
        {
            List<BananaGroup> bananaGroups = new List<BananaGroup>();
            for (int i = 0; i < palpableCatchHitObjects.Count; i++)
            {
                if (palpableCatchHitObjects[i] is Banana)
                {
                    if (i <= 0)
                    {
                        BananaGroup temp = new BananaGroup(i);
                        temp.StartObj = null;
                        temp.AddBanana(palpableCatchHitObjects[i]);
                        bananaGroups.Add(temp);
                    }
                    else if (palpableCatchHitObjects[i - 1] is not Banana)
                    {
                        BananaGroup temp = new BananaGroup(i);
                        temp.StartObj = palpableCatchHitObjects[i - 1];
                        temp.AddBanana(palpableCatchHitObjects[i]);
                        bananaGroups.Add(temp);
                    }
                    else
                    {
                        bananaGroups.Last().AddBanana(palpableCatchHitObjects[i]);
                    }
                    if (i + 1 >= palpableCatchHitObjects.Count)
                    {
                        bananaGroups.Last().EndObj = null;
                    }
                }
                else
                {
                    if (i > 0 && palpableCatchHitObjects[i - 1] is Banana)
                    {
                        bananaGroups.Last().EndObj = palpableCatchHitObjects[i];
                    }
                }
            }
            return bananaGroups;
        }
    }
}
