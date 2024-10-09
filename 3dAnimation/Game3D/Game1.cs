using Game3D.Entities;
using Game3D.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Input;
using Microsoft.Xna.Framework.Input;

namespace Game3D;
public class Game1 : Game
{
    public const int SCREEN_WIDTH = 1024, SCREEN_HEIGHT = 768;
    public static int ScreenW, ScreenH;
    public Color BackgroundColor => new Color(24, 20, 37, 255);

    private GraphicsDeviceManager _graphics;
    private GraphicsDevice _gpu;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Scene _scene;

    Rectangle desktopRect;
    Rectangle screenRect;

    public RenderTarget2D MainTarget;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        int desktop_width = SCREEN_WIDTH;
        int desktop_height = SCREEN_HEIGHT;

        _graphics.PreferredBackBufferWidth = desktop_width;
        _graphics.PreferredBackBufferHeight = desktop_height;
        _graphics.IsFullScreen = false;
        _graphics.PreferredDepthStencilFormat = DepthFormat.None;
        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        _graphics.ApplyChanges();
        Window.Position = new Point(30, 60);
        _gpu = GraphicsDevice;

        PresentationParameters pp = _gpu.PresentationParameters;
        _spriteBatch = new SpriteBatch(_gpu);
        MainTarget = new RenderTarget2D(_gpu, SCREEN_WIDTH, SCREEN_HEIGHT, false, pp.BackBufferFormat, DepthFormat.Depth24);
        ScreenH = MainTarget.Height;
        ScreenW = MainTarget.Width;
        desktopRect = new Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight);
        screenRect = new Rectangle(0, 0, ScreenW, ScreenH);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _scene = new Scene(_gpu, Content);
        _scene.Camera.SetPosition(new Vector3(14.5f, 6.8f, 7.0f));
        _scene.Camera.UpdateTarget(new Vector3(0.0f, 0.0f, 3.0f));

        _scene.AddEntity3d(new HouseEntity());
        _scene.AddEntity3d(new NpcEntity());

        _scene.AddUI(new CharBoardUI());
        _scene.AddUI(new SequenceTimeLineUI());

        _font = Content.Load<SpriteFont>(Path.FONT_PATH);

        KeyBoardHandler.AddInput("up", Keys.Up);
        KeyBoardHandler.AddInput("reset", Keys.F1);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyBoardHandler.SetInputData();
        _scene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        _scene.Draw(_spriteBatch);

        _spriteBatch.Begin();
        _spriteBatch.DrawString(_font, $"FPS: {(int)(1.0d / gameTime.ElapsedGameTime.TotalSeconds)}", Vector2.UnitY * 10, Color.White);
        _spriteBatch.DrawString(_font, $"Camera Position: {_scene.Camera.Position}", Vector2.UnitY * 25, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}