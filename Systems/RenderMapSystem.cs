using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faun.Entities;
using Faun.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Faun.Systems
{
    class RenderMapSystem : DrawSystem
    {
        enum Tileset { Grass = 0 }

        SpriteBatch _spriteBatch;
        Texture2D[] _mapSheets;
        Rectangle _mapSize;
        Camera _camera;
        int _tileSize;

        public RenderMapSystem(EntityManager entityManager, ContentManager content, GraphicsDevice graphics, Camera camera)
            : base(entityManager, content, graphics)
        {
            // Configure spritebatch.
            _spriteBatch = new SpriteBatch(graphics);

            // Load sheets.
            _mapSheets = new Texture2D[Enum.GetNames(typeof(Tileset)).Length];
            _mapSheets[(int)Tileset.Grass] = Content.Load<Texture2D>("landscape/grass_light_fall");
            
            // Map config.
            _mapSize = new Rectangle(0,0,4,4);
            _tileSize = _mapSheets[(int)Tileset.Grass].Width;

            _camera = camera;
        }

        public override void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, _camera.Transform);
            for (int x = 0; x < _mapSize.Width; x++)
            {
                for (int y = 0; y < _mapSize.Height; y++)
                {
                    //_spriteBatch.Draw(_mapSheets[(int)Tileset.Grass], new Vector2(x * _tileSize, y * _tileSize), new Rectangle(0,0, _tileSize, _tileSize), Color.White);
                    _spriteBatch.Draw(_mapSheets[(int)Tileset.Grass], new Vector2(x * _tileSize, y * _tileSize), new Rectangle(0, 0, _tileSize, _tileSize), Color.White,0, new Vector2(0.5f, 0.5f), 1, SpriteEffects.None, 0);
                }
            }
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
