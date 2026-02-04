using Game3D.Interfaces;
using Microsoft.Xna.Framework.Media;
using UmbrellaToolsKit.Input;

namespace Game3D.Entities;

public class SongLevelEntity : UIEntity, ISetup
{
    private Song _song;
    private bool _isPlaying = false;

    public override void Start() => Setup();

    public void Setup()
    {
        _song = Scene.Content.Load<Song>(Path.SONGS_PATH + Levels.GetCurrentSong());
    }

    public void PlaySong()
    {
        MediaPlayer.Play(_song);
        MediaPlayer.IsRepeating = false;
        _isPlaying = true;
    }

    public void Stop()
    {
        _isPlaying = false;
        MediaPlayer.Stop();
    }

    public override void Update(float deltaTime)
    {
        if (GameStates.CurrentState != GameStates.State.PLAYING) return;

        if (KeyBoardHandler.KeyPressed("reset"))
        {
            Stop();
            PlaySong();
        }
        
        if (!_isPlaying)
        {
            PlaySong();
        }
    }
}