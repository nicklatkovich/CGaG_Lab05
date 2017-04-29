using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CGaG_Lab05 {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        Matrix WorldMatrix;
        Matrix ViewMatrix;
        Matrix ProjectionMatrix;
        BasicEffect Effect;

        float AxesLight = 0.2f;

        VertexPositionColor[ ] Points;
        short[ ] Indices = new short[ ] {
            // base
            0, 1,
            1, 2,
            2, 0,
            // side
            0, 3,
            1, 4,
            2, 5,
            // top
            3, 4,
            4, 5,
            5, 3,
        };
        Vector3 SphereCameraPosition = new Vector3(10f, 315f, 45f);

        Color[ ] AxesColors;
        Color PyramidColor = Color.Blue;

        Color BackColor = new Color(30, 30, 30);

        public MainThread( ) {
            AxesColors = new Color[ ] {
                new Color(AxesLight, 0f, 0f),
                new Color(0f, AxesLight, 0f),
                new Color(0f, 0f, AxesLight),
            };
            {
                float baseSize = 4f;
                float baseHeight = -2f;
                float baseDx = baseSize * (float)Math.Cos(MathHelper.ToRadians(30f));
                float baseDy = baseSize / 2f;
                float topSize = 2f;
                float topHeight = 2f;
                float topDx = topSize * (float)Math.Cos(MathHelper.ToRadians(30f));
                float topDy = topSize / 2f;
                Points = new VertexPositionColor[ ] {
                    new VertexPositionColor(new Vector3(0f, baseHeight, -baseSize), PyramidColor),
                    new VertexPositionColor(new Vector3(baseDx, baseHeight, baseDy), PyramidColor),
                    new VertexPositionColor(new Vector3(-baseDx, baseHeight, baseDy), PyramidColor),
                    new VertexPositionColor(new Vector3(0f, topHeight, -topSize), PyramidColor),
                    new VertexPositionColor(new Vector3(topDx, topHeight, topDy), PyramidColor),
                    new VertexPositionColor(new Vector3(-topDx, topHeight, topDy), PyramidColor),
                };
            }
            Graphics = new GraphicsDeviceManager(this);
            Window.Title = "CGaG Lab 5 by NickLatkovich";
            base.IsMouseVisible = true;
            Content.RootDirectory = "Content";

        }

        protected override void Initialize( ) {
            // TODO: Initialization logic
            WorldMatrix = Matrix.Identity;
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
            Effect.View = Matrix.CreateLookAt(SphereCameraPosition.SphereToCart( ), Vector3.Zero, Vector3.Up);

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
                this.DrawLineList(Points, Indices);}

            base.Draw(Time);
        }
    }
}
