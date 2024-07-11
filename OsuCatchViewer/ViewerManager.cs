using osu.Game.Beatmaps;
using osu.Game.Rulesets.Catch.Objects;
using OsuCatchViewer.CatchAPI;

namespace OsuCatchViewer
{
    public class ViewerManager
    {
        public string BeatmapFolder { get; set; }
        public string BeatmapPath { get; set; }
        public IBeatmap Beatmap { get; set; }
        public ReplayAPI.Mods BeatmapMods { get; set; }
        public List<PalpableCatchHitObject> CatchHitObjects { get; set; }
        public List<HasColorHitObject> HasColorHitObjects { get; set; }
        public List<HasColorHitObject> HasColorHitObjects1 { get; set; }
        public int catchedBananas1 = 0;
        public List<HasColorHitObject> HasColorHitObjects2 { get; set; }
        public int catchedBananas2 = 0;
        public List<HasColorHitObject> HasColorHitObjects3 { get; set; }
        public int catchedBananas3 = 0;
        public List<HasColorHitObject> NearbyHitObjects { get; set; }
        private List<List<ReplayAPI.ReplayFrame>?> ReplayFrames { get; set; }
        public List<List<ReplayAPI.ReplayFrame>> NearbyFrames { get; set; }
        public SongPlayer SongPlayer { get; set; }
        public int MaxSongTime { get; set; }
        public List<ReplayAPI.Replay?> CurrentReplays { get; set; }
        public OsuDbAPI.OsuDbFile OsuDbFile { get; set; }
        public int State_PlaybackFlow { get; set; }

        // 0=数据 1=观看 2=按键
        public int State_PlaybackMode { get; set; }

        private float state_PlaybackSpeed;
        public float State_PlaybackSpeed
        {
            get { return this.state_PlaybackSpeed; }
            set
            {
                state_PlaybackSpeed = value;
                MainForm.self?.UpdateSpeedRadio(value);
                SongPlayer.SetPlaybackSpeed(value);
            }
        }
        private float state_volume;
        public float State_Volume
        {
            get { return this.state_volume; }
            set
            {
                this.state_volume = value;
                this.SongPlayer.SetVolume(value);
            }
        }
        public int State_TimeRange { get; set; }
        public int ApproachTime { get; set; }
        private int CircleDiameter { get; set; }
        public float State_ARMul { get; set; }
        public int State_CurveSmoothness { get; set; }

        private byte state_ReplaySelected;
        public byte State_ReplaySelected
        {
            get { return this.state_ReplaySelected; }
            set
            {
                state_ReplaySelected = value;
                MainForm.self?.UpdateReplayRadio(value);
            }
        }


        public ViewerManager(OsuDbAPI.OsuDbFile osuDbFile, string filepath, bool isReplay, int modsWhenOnlyBeatmap = 0)
        {
            OsuDbFile = osuDbFile;
            SongPlayer = new SongPlayer();
            State_PlaybackFlow = 0;
            State_PlaybackMode = 1;
            MaxSongTime = 0;
            NearbyHitObjects = new List<HasColorHitObject>();
            ReplayFrames = new List<List<ReplayAPI.ReplayFrame>?>();
            NearbyFrames = new List<List<ReplayAPI.ReplayFrame>>();
            State_TimeRange = 100;
            State_CurveSmoothness = 50;
            State_PlaybackSpeed = 1.0f;
            State_ReplaySelected = 0;
            State_PlaybackMode = 0;
            // 从Y=0开始落下会显得突兀，可以往前再预留一段时间，默认给掉落时间×1.2
            State_ARMul = 1.2f;
            for (int i = 0; i < 7; i++)
            {
                ReplayFrames.Add(null);
                NearbyFrames.Add(new List<ReplayAPI.ReplayFrame>());
            }
            // 初始化replay槽位
            CurrentReplays = new List<ReplayAPI.Replay?>();
            for (int i = 0; i < 7; i++) CurrentReplays.Add(null);
            if (isReplay)
            {
                LoadReplay(filepath);
            }
            else
            {
                LoadBeatmap(filepath, modsWhenOnlyBeatmap);
            }
        }

        public void AfterLoad()
        {
            JumpTo(0);
            int FirstHitObjectTime = (int)CatchHitObjects[0].StartTime;
            SongPlayer.Start(Path.Combine(BeatmapFolder, Beatmap.Metadata.AudioFile));
            MaxSongTime = (int)this.SongPlayer.SongLength;
            JumpTo(FirstHitObjectTime - 1000);

            State_PlaybackFlow = 0;

            HasColorHitObjects1 = CatchBeatmapAPI.GetTipColors(this.CatchHitObjects, this.Beatmap, ref catchedBananas1, 1);
            HasColorHitObjects2 = CatchBeatmapAPI.GetTipColors(this.CatchHitObjects, this.Beatmap, ref catchedBananas2, 2);
            HasColorHitObjects3 = CatchBeatmapAPI.GetTipColors(this.CatchHitObjects, this.Beatmap, ref catchedBananas3, 3);

            HasColorHitObjects = HasColorHitObjects3;

            /*
            if (r.ReplayFrames.Count > 0)
            {
                MaxSongTime = r.ReplayFrames[r.ReplayFrames.Count - 1].Time;
            }
            else
            {
                MaxSongTime = 0;
            }
            */
        }

        public void LoadBeatmap(string beatmapPath, int mods = 0)
        {
            State_PlaybackSpeed = 1.0f;

            Beatmap = CatchBeatmapAPI.GetBeatmap(beatmapPath, mods);
            BeatmapFolder = beatmapPath.Substring(0, beatmapPath.LastIndexOf("\\"));
            BeatmapPath = beatmapPath;
            CatchHitObjects = CatchBeatmapAPI.GetPalpableObjects(Beatmap);
            BeatmapMods = (ReplayAPI.Mods)mods;

            float moddedAR = Beatmap.Difficulty.ApproachRate;
            ApproachTime = (int)((moddedAR < 5) ? 1800 - moddedAR * 120 : 1200 - (moddedAR - 5) * 150);
            float moddedCS = Beatmap.Difficulty.CircleSize;
            CircleDiameter = (int)(108.848 - moddedCS * 8.9646);

            AfterLoad();
        }

        public void LoadBeatmapFromReplay(string MapHash, int mods)
        {
            if (this.OsuDbFile == null)
            {
                throw new Exception("无法读取osu!.db");
            }
            string beatmapPath = "";
            foreach (OsuDbAPI.Beatmap dbBeatmap in this.OsuDbFile.Beatmaps)
            {
                if (dbBeatmap.Hash == MapHash)
                {
                    beatmapPath = MainForm.Path_Songs + dbBeatmap.FolderName + "\\" + dbBeatmap.OsuFile;
                    break;
                }
            }
            LoadBeatmap(beatmapPath, mods);
        }

        private bool IsConflictMod(ReplayAPI.Replay r1, ReplayAPI.Replay r2)
        {
            if (r1.Mods.HasFlag(ReplayAPI.Mods.Easy) ^ r2.Mods.HasFlag(ReplayAPI.Mods.Easy)) return true;
            if (r1.Mods.HasFlag(ReplayAPI.Mods.HardRock) ^ r2.Mods.HasFlag(ReplayAPI.Mods.HardRock)) return true;
            return false;
        }

        public void UnloadReplay(int slotIndex)
        {
            if (CurrentReplays[slotIndex] != null)
            {
                CurrentReplays[slotIndex].Dispose();
                CurrentReplays[slotIndex] = null;
                ReplayFrames[slotIndex] = null;
                NearbyFrames[slotIndex] = new List<ReplayAPI.ReplayFrame>();
            }
        }

        /// <summary>
        /// 根据当前谱面和replay，判断是否需要重新加载谱面
        /// </summary>
        private bool NeedReloadBeatmap(ReplayAPI.Replay r)
        {
            // 检查谱面是否相同
            if (Beatmap == null || Beatmap.BeatmapInfo.Hash != r.MapHash)
            {
                return true;
            }
            // 检查有无HR/EZ
            if (r.Mods.HasFlag(ReplayAPI.Mods.Easy) ^ BeatmapMods.HasFlag(ReplayAPI.Mods.Easy)) return true;
            if (r.Mods.HasFlag(ReplayAPI.Mods.HardRock) ^ BeatmapMods.HasFlag(ReplayAPI.Mods.HardRock)) return true;
            return false;
        }

        public void LoadReplay(string replayPath, int slotIndex = 0)
        {
            ReplayAPI.Replay r = new ReplayAPI.Replay(replayPath, true);
            if (CurrentReplays[slotIndex] != null)
            {
                CurrentReplays[slotIndex].Dispose();
            }
            CurrentReplays[slotIndex] = r;
            if (NeedReloadBeatmap(r))
            {
                LoadBeatmapFromReplay(r.MapHash, (int)r.Mods);
                bool deletedReplay = false;
                for (int i = 0; i < CurrentReplays.Count; i++)
                {
                    if (i != slotIndex && CurrentReplays[i] != null && IsConflictMod(r, CurrentReplays[i]))
                    {
                        UnloadReplay(i);
                        deletedReplay = true;
                    }
                }
                if (deletedReplay) MainForm.ErrorMessage("None/HR/EZ Mod的谱面不兼容，自动去除不兼容Mod的Replay");
            }
            else
            {
                AfterLoad();
            }
            ReplayFrames[State_ReplaySelected] = r.ReplayFrames;
        }


        private void Tick_Beatmap()
        {
            this.NearbyHitObjects = new List<HasColorHitObject>();
            float time = (float)this.SongPlayer.SongTime;
            int startIndex = this.HitObjectsLowerBound(time);
            int endIndex = this.HitObjectsUpperBound(time);
            for (int k = startIndex; k <= endIndex; k++)
            {
                if (k < 0)
                {
                    continue;
                }
                else if (k >= this.CatchHitObjects.Count)
                {
                    break;
                }
                this.NearbyHitObjects.Add(this.HasColorHitObjects[k]);
            }
        }

        private void Tick_Replay()
        {
            for (int j = 0; j < 7; j++)
            {
                if (this.ReplayFrames[j] != null)
                {
                    if (this.ReplayFrames[j].Count == 0)
                    {
                        MainForm.ErrorMessage("This replay contains no cursor data.");
                        this.ReplayFrames[j] = null;
                        continue;
                    }
                    // like the hitobjects, the replay frames are also in chronological order
                    // so we use more binary searches to efficiently get the index of the replay frame at a time
                    this.NearbyFrames[j] = new List<ReplayAPI.ReplayFrame>();
                    if (this.State_PlaybackMode == 0 || this.State_PlaybackMode == 2)
                    {
                        int lowIndex = this.BinarySearchReplayFrame(j, (int)(this.SongPlayer.SongTime) - this.State_TimeRange);
                        int highIndex = this.BinarySearchReplayFrame(j, (int)this.SongPlayer.SongTime) + 1;
                        for (int i = lowIndex; i <= highIndex; i++)
                        {
                            this.NearbyFrames[j].Add(this.ReplayFrames[j][i]);
                        }
                    }
                    else if (this.State_PlaybackMode == 1)
                    {
                        int nearestIndex = this.BinarySearchReplayFrame(j, (int)this.SongPlayer.SongTime);
                        this.NearbyFrames[j].Add(this.ReplayFrames[j][nearestIndex]);
                        if (nearestIndex + 1 < this.ReplayFrames[j].Count)
                        {
                            this.NearbyFrames[j].Add(this.ReplayFrames[j][nearestIndex + 1]);
                        }
                    }
                }
            }
        }

        private int BinarySearchReplayFrame(int replaynum, int target)
        {
            int high = this.ReplayFrames[replaynum].Count - 1;
            int low = 0;
            while (low <= high)
            {
                int mid = (high + low) / 2;
                if (mid == high || mid == low)
                {
                    return mid;
                }
                if (this.ReplayFrames[replaynum][mid].Time >= target)
                {
                    high = mid;
                }
                else
                {
                    low = mid;
                }
            }
            return 0;
        }

        private int HitObjectsLowerBound(float target)
        {
            int first = 0;
            int last = this.CatchHitObjects.Count;
            int count = last - first;
            while (count > 0)
            {
                int step = count / 2;
                int it = first + step;
                var hitObject = this.CatchHitObjects[it];
                float endTime = (float)hitObject.StartTime;
                float animationEnd = endTime + this.ApproachTime;
                if (animationEnd < target)
                {
                    first = ++it;
                    count -= step + 1;
                }
                else
                {
                    count = step;
                }
            }
            return first;
        }

        private int HitObjectsUpperBound(float target)
        {
            int first = 0;
            int last = this.CatchHitObjects.Count;
            int count = last - first;
            while (count > 0)
            {
                int step = count / 2;
                int it = first + step;
                float animationStart = (float)(this.CatchHitObjects[it].StartTime - this.ApproachTime * this.State_ARMul);
                if (!(target < animationStart))
                {
                    first = ++it;
                    count -= step + 1;
                }
                else
                {
                    count = step;
                }
            }
            return first;
        }

        private void JumpTo(long value)
        {
            if (this.SongPlayer == null) return;
            if (value < 0)
            {
                this.SongPlayer.JumpTo(0);
                this.State_PlaybackFlow = 0;
            }
            else if (value > this.MaxSongTime)
            {
                this.SongPlayer.Stop();
                this.State_PlaybackFlow = 0;
            }
            else
            {
                this.SongPlayer.JumpTo(value);
            }
        }

        public void SetSongTimePercent(float percent)
        {
            // for when timeline is clicked, sets the song time in ms from percentage into the song
            this.JumpTo((long)(percent * (float)this.MaxSongTime));
        }



        public void Tick()
        {
            Tick_Beatmap();
            Tick_Replay();
        }

        public void StopSongPlayer()
        {
            if (SongPlayer != null)
                SongPlayer.Stop();
        }

    }
}
