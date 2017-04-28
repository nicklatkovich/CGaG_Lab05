using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CGaG_Lab05 {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        Color BackColor = new Color(30, 30, 30);

        public MainThread( ) {
            Graphics = new GraphicsDeviceManager(this);
            Window.Title = "CGaG Lab 5 by NickLatkovich";
            base.IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize( ) {
            // TODO: Initialization logic

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

            base.Draw(Time);
        }
    }
}
