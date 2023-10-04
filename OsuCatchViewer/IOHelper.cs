using System;
using System.IO;
using OsuMemoryDataProvider;
using Microsoft.Win32;
using OsuMemoryDataProvider.OsuMemoryModels;
using OsuMemoryDataProvider.OsuMemoryModels.Direct;
using osu.Game.Screens.Edit;
using SevenZip.Compression.LZ;

namespace OsuCatchViewer
{
    public class IOHelper
    {
        private static readonly StructuredOsuMemoryReader pioStructuredReader = StructuredOsuMemoryReader.Instance;
        private static readonly OsuBaseAddresses osuBaseAddresses = new();
        private static readonly object pioReaderLock = new();


        private static T ReadClassProperty<T>(object readObj, string propName, T defaultValue = default) where T : class
        {
            lock (pioReaderLock)
            {
                if (pioStructuredReader.TryReadProperty(readObj, propName, out var readResult))
                    return (T)readResult;
            }

            return defaultValue;
        }

        private static string ReadString(object readObj, string propName)
            => ReadClassProperty<string>(readObj, propName);

        public static string GetCurrentBeatmap(string songsFolder)
        {
            string path;
            try
            {
                string songs = songsFolder;

                if (string.IsNullOrEmpty(songs))
                {
                    throw new Exception(
                        @"Can't fetch current in-game beatmap, because there is no Songs path specified in Preferences.");
                }

                string folder = ReadString(osuBaseAddresses.Beatmap, nameof(CurrentBeatmap.FolderName));
                string filename = ReadString(osuBaseAddresses.Beatmap, nameof(CurrentBeatmap.OsuFileName));

                if (string.IsNullOrEmpty(folder))
                {
                    throw new Exception(@"Can't fetch the folder name of the current in-game beatmap.");
                }

                if (string.IsNullOrEmpty(filename))
                {
                    throw new Exception(@"Can't fetch the file name of the current in-game beatmap.");
                }

                path = Path.Combine(songs, folder, filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }

            return path;
        }


    }
}