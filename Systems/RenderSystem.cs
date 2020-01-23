using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faun.Entities;
using Faun.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Faun.Global;

namespace Faun.Systems
{
    class RenderSystem : DrawSystem
    {
        SpriteBatch _spriteBatch;
        Texture2D _spritesheet;
        Rectangle[,] _spriteParts;
        Camera _camera;

        public RenderSystem(EntityManager entityManager, ContentManager content, GraphicsDevice graphics, Camera camera)
            :base(entityManager, content, graphics)
        {
            // Configure spritebatch.
            _spriteBatch = new SpriteBatch(graphics);
            _entitySet = ComponentType.Sprite;

            // Load spritesheets & fonts.
            _spritesheet = Content.Load<Texture2D>("entities/spritesheet_goat_v1");
            
            // Create source rectangles.
            int spritesPerRow = 16;
            _spriteParts = new Rectangle[1,spritesPerRow];
            int width = _spritesheet.Bounds.Width;
            int height = _spritesheet.Bounds.Height;

            for(int i = 0; i<spritesPerRow; i++)
            {
                _spriteParts[(int)SpriteType.Goatman, i] = new Rectangle(i*(width/spritesPerRow), 0, width / spritesPerRow, height);
            }

            // Camera
            _camera = camera;
        }

        public override void Draw()
        {
            // Collect entities.
            List<Entity> sprites = _entityManager.GetEntities(EntitySet);
            
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null,null,null,null, _camera.Transform);
            foreach (var entity in sprites)
            {
                Vector2 pos = ((Movement)_entityManager.GetComponent(entity, ComponentType.Movement)).Position;

                // Get texture from spritesheet. Check if animated and pick correct frame.
                Rectangle sourceRect;
                Animation animation = (Animation)_entityManager.GetComponent(entity, ComponentType.Animation);
                if (animation != null)
                {
                    sourceRect = _spriteParts[GetSpriteTextureIndex(entity), animation.CurrentFrame + ((int)animation.SpriteLoop)*animation.LoopLenght];
                }
                else
                {
                    sourceRect = _spriteParts[GetSpriteTextureIndex(entity), 0];
                }

                // Calculate draw order using layer depth. Depth = Y coordinate in screenspace, hence we transform postion to screen.
                // Dividing by viewport scales value from 0 to 1.
                // TODO: Origin on sprites. Cull sprites that is offscreen.
                float layerDepth = 1.0f-Vector3.Transform(new Vector3(pos, 0.0f), _camera.Transform).Y/Graphics.Viewport.Height;
                // Draw sprite.
                // Args: sprite, position, source rect, color, rotation, origin, scale, effect, layerDepth.
                _spriteBatch.Draw(_spritesheet, pos, sourceRect, Color.White, 0, new Vector2(sourceRect.Width/2,sourceRect.Height/2), 1, SpriteEffects.None, layerDepth);
            }
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            Draw();
        }

        // Get texture index in the spritesheet for this sprite.
        private int GetSpriteTextureIndex(Entity entity)
        {
            return ((Sprite)_entityManager.GetComponent(entity, ComponentType.Sprite)).TextureIndex;
        }
    }
}
