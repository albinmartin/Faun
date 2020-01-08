using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Faun.GameState;
using Faun.Controls;
using Microsoft.Xna.Framework.Content;
using Faun.Global;

namespace Faun
{
    public class GameMain : Game
    {
        GameStateManager _stateManager;
        InputManager _input;
        GraphicsDeviceManager _graphicsDeviceManager;

        public GameMain()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            DebugDraw.Initialize(Content, _graphicsDeviceManager.GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: Implement service provider?
            
            // Init input.
            _input = new InputManager();
            _input.Initialize();

            // Create menu and world.
            _stateManager = new GameStateManager(Content, _graphicsDeviceManager.GraphicsDevice, _input);
            _stateManager.CreateMainMenu();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _stateManager.Update(gameTime);
            _input.Update();
            DebugDraw.Refresh();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _stateManager.Draw(gameTime);
            base.Draw(gameTime);
        }

    }
}
