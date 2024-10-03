using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit.Animation3D;

namespace Game3D
{
    public class Game1 : Game
    {
        const int SCREENWIDTH = 1024, SCREENHEIGHT = 768;
        GraphicsDeviceManager graphics;
        GraphicsDevice gpu;
        SpriteBatch spriteBatch;
        SpriteFont font;
        static public int screenW, screenH;

        Rectangle desktopRect;
        Rectangle screenRect;

        RenderTarget2D MainTarget;

        Matrix world = Matrix.CreateTranslation(0, 0, 0);
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 4, 20), new Vector3(0, 3, 0), new Vector3(0, 1, 0));
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), SCREENWIDTH / SCREENHEIGHT, 0.01f, 100f);

        Vector3 lightPosition = new Vector3(2, 2, 2);

        UmbrellaToolsKit.Animation3D.Mesh mesh;
        UmbrellaToolsKit.Animation3D.Model model;

        Pose restPose;
        float playbackTime;
        int currentClip;
        int CurrentClip
        {
            get
            {
                if (currentClip > 0)
                    return (currentClip % (mesh.Clips.Length));
                return currentClip = 0;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            int desktop_width = SCREENWIDTH;
            int desktop_height = SCREENHEIGHT;

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
            MainTarget = new RenderTarget2D(gpu, SCREENWIDTH, SCREENHEIGHT, false, pp.BackBufferFormat, DepthFormat.Depth24);
            screenH = MainTarget.Height;
            screenW = MainTarget.Width;
            desktopRect = new Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight);
            screenRect = new Rectangle(0, 0, screenW, screenH);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("BasicFont");
            mesh = Content.Load<Mesh>("Woman");

            model = new UmbrellaToolsKit.Animation3D.Model(mesh, GraphicsDevice);
            model.SetTexture(Content.Load<Texture2D>("WomanTex"));
            model.SetLightPosition(lightPosition);
            model.SetEffect(Content.Load<Effect>("DiffuseLighting"));

            restPose = mesh.Skeleton.GetRestPose();
        }

        int currentBone = 0;
        bool debugMode = false;
        float animationSpeed = 1f;

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            playbackTime = mesh.Clips[CurrentClip].Sample(restPose, playbackTime + ((float)gameTime.ElapsedGameTime.TotalSeconds * animationSpeed));

            model.DebugMode(debugMode, currentBone);

            base.Update(gameTime);
        }
        private float angle = 0;

        protected override void Draw(GameTime gameTime)
        {
            angle += 0.8f;
            if (angle > 360.0f)
                angle -= 360.0f;

            GraphicsDevice.Clear(debugMode ? Color.Black : Color.Gray);

            model.SetWorld(world);

            model.Draw(GraphicsDevice, projection, view);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Use (up, down) to change the animation", Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, $"Animation: {mesh.Clips[CurrentClip].mName}", Vector2.UnitY * 15, Color.White);
            spriteBatch.DrawString(font, $"Debug Mode (Press F1): status: {debugMode} Current Bone: {(currentBone < mesh.JointsIndexs.Length && currentBone >= 0 ? mesh.JointsIndexs[currentBone] : currentBone)} {(currentBone < mesh.JointsIndexs.Length && currentBone >= 0 ? mesh.Skeleton.GetJoinName(mesh.JointsIndexs[currentBone]) : currentBone)} (left, right)", Vector2.UnitY * 30, Color.White);
            spriteBatch.DrawString(font, $"FPS: {1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds}", Vector2.UnitY * 45, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}