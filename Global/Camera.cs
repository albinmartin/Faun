using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faun.Global
{
    class Camera
    {
        Matrix _view;
        Matrix _projection;
        float _zDistance;
        float _viewportWidth;
        float _viewportHeight;
        Vector2 _origin;
        float _scrollSpeed;

        public Matrix Transform;
        public Matrix Projection { get => _projection; set => _projection = value; }
        public Matrix View { get => _view; set => _view = value; }
        public Vector2 Origin { get => _origin;}
        public Vector2 Position { get; set; }
        public Vector2 Focus { get; set; }

        public Camera(GraphicsDevice graphics)
        {
            _zDistance = -1;
            _scrollSpeed = 1.5f; //Faster than player movement yields a scroll movement cap due to focus point (player pos) will cap out. (vector focus - position)
            Position = Vector2.Zero;
            Focus = Vector2.Zero;

            // Create default spritebatch projection matrix
            Projection = Matrix.CreateOrthographic(graphics.Viewport.Width, graphics.Viewport.Height, 0, 1); // TEMP!
            View = Matrix.CreateTranslation(new Vector3(0,0,_zDistance));
            Transform = Matrix.Identity * Matrix.CreateTranslation(0, 0, 0); 
            _viewportWidth = graphics.Viewport.Width;
            _viewportHeight = graphics.Viewport.Height;
            _origin = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
        }

        public void Update(Vector2 position, GameTime gameTime)
        {
            Focus = position;
            Position += new Vector2((Focus.X - Position.X) * _scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,
                                    (Focus.Y - Position.Y) * _scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                        Matrix.CreateTranslation(_origin.X, _origin.Y, 0);
        }
    }
}
