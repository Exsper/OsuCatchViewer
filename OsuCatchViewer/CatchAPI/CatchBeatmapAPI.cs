using osu.Framework.Audio.Track;
using osu.Framework.Graphics.Textures;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.Formats;
using osu.Game.IO;
using osu.Game.Rulesets;
using osu.Game.Rulesets.Catch;
using osu.Game.Rulesets.Catch.Objects;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects;
using osu.Game.Skinning;
using Color = OpenTK.Graphics.Color4;

namespace OsuCatchViewer.CatchAPI
{
    public class ProcessorWorkingBeatmap : WorkingBeatmap
    {
        private readonly Beatmap beatmap;



        /// <summary>
        /// Constructs a new <see cref="ProcessorWorkingBeatmap"/> from a .osu file.
        /// </summary>
        /// <param name="file">The .osu file.</param>
        /// <param name="beatmapId">An optional beatmap ID (for cases where .osu file doesn't have one).</param>
        public ProcessorWorkingBeatmap(string file, int? beatmapId = null)
            : this(readFromFile(file), beatmapId)
        {
        }

        private ProcessorWorkingBeatmap(Beatmap beatmap, int? beatmapId = null)
    : base(beatmap.BeatmapInfo, null)
        {
            this.beatmap = beatmap;

            //beatmap.BeatmapInfo.Ruleset = LegacyHelper.GetRulesetFromLegacyID(beatmap.BeatmapInfo.Ruleset.OnlineID).RulesetInfo;

            //if (beatmapId.HasValue)
            //    beatmap.BeatmapInfo.OnlineID = beatmapId.Value;
        }

        private static Beatmap readFromFile(string filename)
        {
            using (var stream = File.OpenRead(filename))
            using (var reader = new LineBufferedReader(stream))
                return Decoder.GetDecoder<Beatmap>(reader).Decode(reader);
        }

        protected override IBeatmap GetBeatmap() => beatmap;
        public override Texture GetBackground() => null;
        protected override Track GetBeatmapTrack() => null;
        protected override ISkin GetSkin() => null;
        public override Stream GetStream(string storagePath) => null;
    }

    public class CatchBeatmapAPI
    {
        public static Ruleset catchRulest => new CatchRuleset();

        static Mod[] GetMods(string[] Mods, Ruleset ruleset)
        {
            if (Mods == null)
                return Array.Empty<Mod>();

            List<Mod> availableMods = ruleset.CreateAllMods().ToList();
            List<Mod> mods = new List<Mod>();

            foreach (var modString in Mods)
            {
                Mod newMod = availableMods.FirstOrDefault(m => string.Equals(m.Acronym, modString, StringComparison.CurrentCultureIgnoreCase)) ?? throw new ArgumentException($"Invalid mod provided: {modString}");
                mods.Add(newMod);
            }

            return mods.ToArray();
        }

        public static string[] GetModsString(int raw_mods)
        {
            List<string> modsArr = new List<string>();
            if ((raw_mods & 1) > 0) modsArr.Add("NF");
            if ((raw_mods & 2) > 0) modsArr.Add("EZ");
            if ((raw_mods & 4) > 0) modsArr.Add("TD");
            if ((raw_mods & 8) > 0) modsArr.Add("HD");
            if ((raw_mods & 16) > 0) modsArr.Add("HR");
            if ((raw_mods & 32) > 0) modsArr.Add("SD");
            if ((raw_mods & 64) > 0) modsArr.Add("DT");
            if ((raw_mods & 128) > 0) modsArr.Add("Relax");
            if ((raw_mods & 256) > 0) modsArr.Add("HT");
            if ((raw_mods & 512) > 0) { modsArr.Add("NC"); modsArr.Remove("DT"); }
            if ((raw_mods & 1024) > 0) modsArr.Add("FL");
            if ((raw_mods & 2048) > 0) modsArr.Add("Auto");
            if ((raw_mods & 4096) > 0) modsArr.Add("SO");
            if ((raw_mods & 8192) > 0) modsArr.Add("AP");
            if ((raw_mods & 16384) > 0) { modsArr.Add("PF"); modsArr.Remove("SD"); };
            if ((raw_mods & 32768) > 0) modsArr.Add("4K");
            if ((raw_mods & 65536) > 0) modsArr.Add("5K");
            if ((raw_mods & 131072) > 0) modsArr.Add("6K");
            if ((raw_mods & 262144) > 0) modsArr.Add("7K");
            if ((raw_mods & 524288) > 0) modsArr.Add("8K");
            if ((raw_mods & 1048576) > 0) modsArr.Add("FI");
            if ((raw_mods & 2097152) > 0) modsArr.Add("RD");
            if ((raw_mods & 4194304) > 0) modsArr.Add("Cinema");
            if ((raw_mods & 8388608) > 0) modsArr.Add("Target");
            if ((raw_mods & 16777216) > 0) modsArr.Add("9K");
            if ((raw_mods & 33554432) > 0) modsArr.Add("KeyCoop");
            if ((raw_mods & 67108864) > 0) modsArr.Add("1K");
            if ((raw_mods & 134217728) > 0) modsArr.Add("3K");
            if ((raw_mods & 268435456) > 0) modsArr.Add("2K");
            if ((raw_mods & 536870912) > 0) modsArr.Add("ScoreV2");
            if ((raw_mods & 1073741824) > 0) modsArr.Add("MR");
            return modsArr.ToArray();
        }

        public static IBeatmap Execute(string filename, Mod[] mods)
        {
            Ruleset ruleset = catchRulest;
            ProcessorWorkingBeatmap workingBeatmap = new ProcessorWorkingBeatmap(filename);
            IBeatmap playableBeatmap = workingBeatmap.GetPlayableBeatmap(ruleset.RulesetInfo, mods);
            if (playableBeatmap == null) throw new Exception("该谱面有错误或无法游玩接水果模式。");
            return playableBeatmap;
        }

        public static IBeatmap GetBeatmap(string filename, string[] mods)
        {
            Ruleset ruleset = catchRulest;
            return Execute(filename, GetMods(mods, ruleset));
        }

        public static IBeatmap GetBeatmap(string filename, int mods)
        {
            Ruleset ruleset = catchRulest;
            return Execute(filename, GetMods(GetModsString(mods), ruleset));
        }

        public static List<PalpableCatchHitObject> GetPalpableObjects(IBeatmap beatmap)
        {
            List<PalpableCatchHitObject> palpableObjects = new List<PalpableCatchHitObject>();

            foreach (var currentObject in beatmap.HitObjects)
            {
                if (currentObject is Fruit fruitObject)
                    palpableObjects.Add(fruitObject);

                else if (currentObject is JuiceStream)
                {
                    foreach (var juice in currentObject.NestedHitObjects)
                    {
                        if (juice is PalpableCatchHitObject palpableObject)
                            palpableObjects.Add(palpableObject);
                    }
                }
                else if (currentObject is BananaShower)
                {
                    foreach (var banana in currentObject.NestedHitObjects)
                    {
                        if (banana is PalpableCatchHitObject palpableObject)
                            palpableObjects.Add(palpableObject);
                    }
                }
            }

            palpableObjects.Sort((h1, h2) => h1.StartTime.CompareTo(h2.StartTime));

            return palpableObjects;
        }

        public static List<HasColorHitObject> GetTipColors(List<PalpableCatchHitObject> palpableObjects, IBeatmap beatmap, ref int catchedBananas, int difficultyLevel)
        {
            Color[] _allColors = new Color[palpableObjects.Count];
            for (int i = 0; i < palpableObjects.Count; i++)
            {
                _allColors[i] = Color.Yellow;
            }

            List<Color> allColors = _allColors.ToList();

            // 标记路径
            List<BananaGroup> bananaGroups = BananaTipBuilder.GetBananaGroups(palpableObjects);

            catchedBananas = 0;

            foreach (var bananaGroup in bananaGroups)
            {
                List<Color> colors = bananaGroup.LongestPath(beatmap.Difficulty.CircleSize, ref catchedBananas, difficultyLevel);
                int startIndex = bananaGroup.FirstBananaIndex;
                int endIndex = startIndex + bananaGroup.hasTipBananas.Count;
                for (int i = startIndex; i < endIndex; i++)
                {
                    allColors[i] = colors[i - startIndex];
                }
            }
            List<HasColorHitObject> hasColorHitObject = new List<HasColorHitObject>();
            for (int i = 0; i < palpableObjects.Count; i++)
            {
                hasColorHitObject.Add(new HasColorHitObject(palpableObjects[i], allColors[i]));
            }
            return hasColorHitObject;
        }


        public static string GetBeatmapTitle(IBeatmap beatmap)
        {
            // TODO
            return beatmap.BeatmapInfo.Metadata.Title;
        }

        public static BeatmapDifficulty GetBeatmapDifficulty(IBeatmap beatmap)
        {
            // mod加成后的四维
            return beatmap.Difficulty;
        }

        /// <summary>
        /// 获取谱面内各香蕉段的起止时间的百分比
        /// </summary>
        /// <param name="beatmap"></param> 谱面
        /// <param name="songTime"></param> 音频总时长
        /// <returns></returns>
        public static List<double>[] BananaShowerTime(IBeatmap beatmap, double songTime)
        {
            List<double>[] bananaShowerTime = new List<double>[2];
            bananaShowerTime[0] = new List<double>();
            bananaShowerTime[1] = new List<double>();
            foreach (var currentObject in beatmap.HitObjects)
            {
                if (currentObject is BananaShower)
                {
                    bananaShowerTime[0].Add(currentObject.StartTime / songTime);
                    bananaShowerTime[1].Add(currentObject.GetEndTime() / songTime);
                }
            }
            return bananaShowerTime;
        }
    }
}
