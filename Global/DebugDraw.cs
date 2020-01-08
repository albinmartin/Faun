using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faun.Global
{
    public static class DebugDraw
    {
        static SpriteBatch _spritebatch;
        static GraphicsDevice _graphics;
        static SpriteFont _font;
        static int _numPrints;

        static DebugDraw()
        {

        }

        public static void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            _graphics = graphics;
            _spritebatch = new SpriteBatch(graphics);
            _font = content.Load<SpriteFont>("fonts/defaultDebug");
            _numPrints = 0;
        }
        
        public static void Refresh()
        {
            _numPrints = 0;
        }

        public static void Print(string text)
        {
            _spritebatch.Begin();
            _spritebatch.DrawString(_font, text, new Vector2(5, _numPrints * _font.LineSpacing + 2), Color.White);
            _spritebatch.End();
            _numPrints++;
        }
    }
}
