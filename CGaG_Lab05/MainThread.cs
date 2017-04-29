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

        Vector3[ ] Points;

        Color[ ] AxesColors;

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
                Points = new Vector3[ ] {
                    new Vector3(0f, baseHeight, -baseSize),
                    new Vector3(baseDx, baseHeight, baseDy),
                    new Vector3(-baseDx, baseHeight, baseDy),
                    new Vector3(0f, topHeight, -topSize),
                    new Vector3(topDx, topHeight, topDy),
                    new Vector3(-topDx, topHeight, topDy),
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
                {
                    Color cl = Color.Blue;
                    VertexPositionColor[ ] vertexes = new VertexPositionColor[ ] {
                        // base
                        new VertexPositionColor(Points[0], cl),
                        new VertexPositionColor(Points[1], cl),

                        new VertexPositionColor(Points[1], cl),
                        new VertexPositionColor(Points[2], cl),

                        new VertexPositionColor(Points[2], cl),
                        new VertexPositionColor(Points[0], cl),

                        // side
                        new VertexPositionColor(Points[0], cl),
                        new VertexPositionColor(Points[3], cl),

                        new VertexPositionColor(Points[1], cl),
                        new VertexPositionColor(Points[4], cl),

                        new VertexPositionColor(Points[2], cl),
                        new VertexPositionColor(Points[5], cl),

                        // top
                        new VertexPositionColor(Points[3], cl),
                        new VertexPositionColor(Points[4], cl),

                        new VertexPositionColor(Points[4], cl),
                        new VertexPositionColor(Points[5], cl),

                        new VertexPositionColor(Points[5], cl),
                        new VertexPositionColor(Points[3], cl),

                    };
                    this.DrawLineList(vertexes);
                }
            }

            base.Draw(Time);
        }
    }
}
