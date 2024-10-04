using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D
{
    public class Game1 : Game
    {
        const int SCREEN_WIDTH = 1024, SCREEN_HEIGHT = 768;
        static public int screenW, screenH;

        GraphicsDeviceManager graphics;
        GraphicsDevice gpu;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Rectangle desktopRect;
        Rectangle screenRect;

        RenderTarget2D MainTarget;

        Scene scene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        Entity entity = new Entity();
        protected override void Initialize()
        {
            int desktop_width = SCREEN_WIDTH;
            int desktop_height = SCREEN_HEIGHT;

            graphics.PreferredBackBufferWidth = desktop_width;
            graphics.PreferredBackBufferHeight = desktop_height;
            graphics.IsFullScreen = false;
            graphics.PreferredDepthStencilFormat = DepthFormat.None;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphics.ApplyChanges();
            Window.Position = new Point(30, 60);
            gpu = GraphicsDevice;

            PresentationParameters pp = gpu.PresentationParameters;
            spriteBatch = new SpriteBatch(gpu);
            MainTarget = new RenderTarget2D(gpu, SCREEN_WIDTH, SCREEN_HEIGHT, false, pp.BackBufferFormat, DepthFormat.Depth24);
            screenH = MainTarget.Height;
            screenW = MainTarget.Width;
            desktopRect = new Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight);
            screenRect = new Rectangle(0, 0, screenW, screenH);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            scene = new Scene(gpu, Content);
            entity.Model = Content.Load<Model>("models/house");
            entity.Texture = Content.Load<Texture2D>("textures/default_texture");
            scene.AddEntity(entity);

            font = Content.Load<SpriteFont>("BasicFont");
        }

        protected override void Update(GameTime gameTime)
        {
            scene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            scene.Draw();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, $"FPS: {1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds}", Vector2.UnitY * 10, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}