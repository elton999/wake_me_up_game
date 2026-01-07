using System.Collections.Generic;

namespace Game3D;

public class Levels
{
    private static int _currentLevel = 0;
    public static List<List<KeySequenceData>> Level;

    public static void NextLevel()
    {
        _currentLevel++;
        if (_currentLevel >= Level.Count)
            _currentLevel = 0;
    }

    public static List<KeySequenceData> GetCurrentLevel() => Level[_currentLevel];
}
