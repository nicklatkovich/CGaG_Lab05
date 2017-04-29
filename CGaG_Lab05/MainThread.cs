using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CGaG_Lab05 {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        Matrix WorldMatrix;
        Matrix ViewMatrix;
        Matrix ProjectionMatrix;
        BasicEffect Effect;

        float AxesLight = 0.4f;

        Color[ ] AxesColors;

        Color BackColor = new Color(30, 30, 30);

        public MainThread( ) {
            AxesColors = new Color[3] {
                new Color(AxesLight, 0f, 0f),
                new Color(0f, AxesLight, 0f),
                new Color(0f, 0f, AxesLight),
            };
            Graphics = new GraphicsDeviceManager(this);
            Window.Title = "CGaG Lab 5 by NickLatkovich";
            base.IsMouseVisible = true;
            Content.RootDirectory = "Content";

        }

        protected override void Initialize( ) {
            // TODO: Initialization logic
            WorldMatrix = Matrix.Identity;
            ViewMatrix = Matrix.CreateLookAt(new Vector3(10f, 10f, 10f), Vector3.Zero, Vector3.Up);

            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height, 1.0f, 100.0f);

            Effect = new BasicEffect(Graphics.GraphicsDevice);

            Effect.World = WorldMatrix;
            Effect.View = ViewMatrix;
            Effect.Projection = ProjectionMatrix;
            Effect.VertexColorEnabled = true;

            base.Initialize( );
        }

        protected override void LoadContent( ) {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Use this.Content to load game content
        }

        protected override void UnloadContent( ) {
            // TODO: Unload any non ContentManager content
        }

        protected override void Update(GameTime Time) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState( ).IsKeyDown(Keys.Escape))
                Exit( );

            // TODO: Update logic

            base.Update(Time);
        }

        protected override void Draw(GameTime Time) {
            GraphicsDevice.Clear(this.BackColor);

            // TODO: Drawing code
            RasterizerState rasterizerState1 = new RasterizerState( );
            rasterizerState1.CullMode = CullMode.None;
            Graphics.GraphicsDevice.RasterizerState = rasterizerState1;
            foreach (EffectPass pass in Effect.CurrentTechnique.Passes) {
                pass.Apply( );
                this.DrawLineList(new VertexPositionColor[6] {
                    new VertexPositionColor(new Vector3(-1024f, 0f, 0f), AxesColors[0]),
                    new VertexPositionColor(new Vector3(1024f, 0f, 0f), AxesColors[0]),
                    new VertexPositionColor(new Vector3(0f, 1024f, 0f), AxesColors[1]),
                    new VertexPositionColor(new Vector3(0f, -1024f, 0f), AxesColors[1]),
                    new VertexPositionColor(new Vector3(0f, 0f, 1024f), AxesColors[2]),
                    new VertexPositionColor(new Vector3(0f, 0f, -1024f), AxesColors[2]),
                });
                this.DrawLineTriangle(new Vector3(0f, -2f, 0f), new Vector3(2f, 1f, 0f), new Vector3(-2f, 1f, 0f), Color.Blue);
            }

            base.Draw(Time);
        }
    }
}
