using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Game3D;

public class Levels
{
    private static int _currentLevel = 0;
    private static SongEvents.Song _songEvents;
    public static List<List<KeySequenceData>> Level;
    public static List<string> Songs = new List<string>();

    public static bool IsTheLastLevel => _currentLevel == Level.Count - 1;

    public static void NextLevel()
    {
        LoadSongEvents();
        _currentLevel++;
        if (_currentLevel >= Level.Count)
            _currentLevel = 0;
    }

    public static string GetCurrentSong()
    {
        LoadSongEvents();
        return Songs[_currentLevel];
    }

    public static List<KeySequenceData> GetCurrentLevel()
    {
        LoadSongEvents();
        return Level[_currentLevel];
    }

    private static void LoadSongEvents()
    {
        if (_songEvents != null) return;

        _songEvents = SongEvents.Song.LoadJson(Path.SONG_EVENTS_PATH);
        Level = new List<List<KeySequenceData>>();
        foreach (var songEvent in _songEvents.RhythmEditor)
        {
            var eventList = new List<KeySequenceData>();
            foreach (var eventItem in songEvent.Events)
            {
                eventList.Add(new KeySequenceData() { Time = eventItem.Timer, KeyDirection = (DirectionKey)eventItem.Arrow });
            }
            Level.Add(eventList);
            Songs.Add(songEvent.Song.Split(".")[0]);
        }
    }
}
