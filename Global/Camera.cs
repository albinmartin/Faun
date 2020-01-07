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

        public Matrix Transform;
        public Matrix Projection { get => _projection; set => _projection = value; }
        public Matrix View { get => _view; set => _view = value; }
        public Vector2 Origin { get => _origin;}

        public Camera(GraphicsDevice graphics)
        {
            _zDistance = -1;
            Projection = Matrix.CreateOrthographic(1, 1, 0, 1);
            View = Matrix.CreateTranslation(new Vector3(0,0,_zDistance));
            Transform = Matrix.Identity * Matrix.CreateTranslation(0, 0, 0); 
            _viewportWidth = graphics.Viewport.Width;
            _viewportHeight = graphics.Viewport.Height;
            _origin = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
        }

        public void Update(Vector2 position)
        {
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-position.X, -position.Y, 0) *
                        Matrix.CreateTranslation(_origin.X, _origin.Y, 0);
        }
    }
}
