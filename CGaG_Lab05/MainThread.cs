using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CGaG_Lab05 {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        enum DrawingStyle {
            Lines,
            Edges,
            ColorBases,
            ColorFaces,
        }
        DrawingStyle Style = DrawingStyle.Edges;
        
        BasicEffect Effect;

        float AxesLight = 0.7f;

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
        Tuple<short, short, short>[ ] Faces = new Tuple<short, short, short>[ ] {
            new Tuple<short, short, short>(0, 2, 1),
            new Tuple<short, short, short>(0, 1, 3),
            new Tuple<short, short, short>(1, 2, 5),
            new Tuple<short, short, short>(2, 0, 3),
            new Tuple<short, short, short>(3, 4, 5),
        };
        Tuple<short, short>[ ] nearFaces = new Tuple<short, short>[ ] {
            new Tuple<short, short>(0, 1),
            new Tuple<short, short>(0, 2),
            new Tuple<short, short>(0, 3),
            
            new Tuple<short, short>(1, 3),
            new Tuple<short, short>(1, 2),
            new Tuple<short, short>(2, 3),

            new Tuple<short, short>(4, 1),
            new Tuple<short, short>(4, 2),
            new Tuple<short, short>(4, 3),
        };
        Vector3 SphereCameraPosition = new Vector3(10f, 315f, 45f);

        Color[ ] AxesColors;
        Color PyramidColor = Color.Black;

        Color BackColor = new Color(1f, 1f, 1f);

        public MainThread( ) {
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnWindowResized;
            AxesColors = new Color[ ] {
                new Color(1f, AxesLight, AxesLight),
                new Color(AxesLight, 1f, AxesLight),
                new Color(AxesLight, AxesLight, 1f),
            };
            {
                float baseSize = 3f;
                float baseHeight = -2f;
                float baseDx = baseSize * (float)Math.Cos(MathHelper.ToRadians(30f));
                float baseDy = baseSize / 2f;
                float topSize = 1f;
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
            Graphics.PreparingDeviceSettings += SetMultiSampling;
            //Graphics.PreferMultiSampling = true;
            Window.Title = "CGaG Lab 5 by NickLatkovich";
            base.IsMouseVisible = true;
            Content.RootDirectory = "Content";

        }

        private void OnWindowResized(Object sender, EventArgs e) {
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            Graphics.ApplyChanges( );
        }

        private void SetMultiSampling(Object sender, PreparingDeviceSettingsEventArgs e) {
            e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 4;
        }

        protected override void Initialize( ) {
            // TODO: Initialization logic
            Effect = new BasicEffect(Graphics.GraphicsDevice);
            Effect.World = Matrix.Identity;
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

            KeyboardState keyboard = Keyboard.GetState( );

            if (keyboard.IsKeyDown(Keys.Escape)) {
                Exit( );
            }

            // TODO: Update logic
            SphereCameraPosition.Y +=
                (keyboard.IsKeyDown(Keys.Left) ? 1 : 0) -
                (keyboard.IsKeyDown(Keys.Right) ? 1 : 0);
            SphereCameraPosition.Z +=
                (keyboard.IsKeyDown(Keys.Up) ? 1 : 0) -
                (keyboard.IsKeyDown(Keys.Down) ? 1 : 0);
            SimpleUtils.Median(ref SphereCameraPosition.Z, -89f, 89f);
            Effect.View = Matrix.CreateLookAt(SphereCameraPosition.SphereToCart( ), Vector3.Zero, Vector3.Up);
            if (Graphics.PreferredBackBufferWidth > Graphics.PreferredBackBufferHeight) {
                Effect.Projection = Matrix.CreateOrthographic(SphereCameraPosition.X * Graphics.PreferredBackBufferWidth / Graphics.PreferredBackBufferHeight, SphereCameraPosition.X, 0.1f, 100.0f);
            } else {
                Effect.Projection = Matrix.CreateOrthographic(SphereCameraPosition.X, SphereCameraPosition.X * Graphics.PreferredBackBufferHeight / Graphics.PreferredBackBufferWidth, 0.1f, 100.0f);
            }

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
                switch (Style) {
                case DrawingStyle.Lines:
                    this.DrawLineList(Points, Indices);
                    break;
                case DrawingStyle.Edges:
                    bool[ ] facesVisible = new bool[Faces.Length];
                    for (uint i = 0; i < facesVisible.Length; i++) {
                        Vector3 V1 = Points[Faces[i].Item2].Position - Points[Faces[i].Item1].Position;
                        Vector3 V2 = Points[Faces[i].Item3].Position - Points[Faces[i].Item1].Position;
                        Vector3 normal = Vector3.Cross(V2, V1);
                        Vector3 toCam = SimpleUtils.SphereToCart(SphereCameraPosition);
                        facesVisible[i] = (float)Math.Abs(Math.Acos(Vector3.Dot(normal, toCam) / (normal.Length( ) * toCam.Length( )))) < MathHelper.ToRadians(90f);
                    }
                    bool[ ] visibleLines = new bool[nearFaces.Length];
                    for (uint i = 0; i < nearFaces.Length; i++) {
                        visibleLines[i] = facesVisible[nearFaces[i].Item1] || facesVisible[nearFaces[i].Item2];
                    }
                    this.DrawLineList(Points, Indices, visibleLines);
                    break;
                }
            }

            base.Draw(Time);
        }
    }
}
