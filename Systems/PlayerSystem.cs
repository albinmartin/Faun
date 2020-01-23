using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faun.Components;
using Faun.Controls;
using Faun.Entities;
using Faun.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faun.Systems
{
    class PlayerSystem : GameSystem
    {
        InputManager _input;
        Camera _camera;

        public PlayerSystem(EntityManager entityManager, InputManager input, Camera camera)
            : base(entityManager)
        {
            _input = input;
            _entitySet = ComponentType.Player | ComponentType.Movement;
            _camera = camera;
        }

        public override void Update(GameTime gameTime)
        {
            List<Entity> players = _entityManager.GetEntities(_entitySet);

            // Input.
            if (players.Count > 0)
            {
                Keys[] keys = _input.GetKeysDown();
                Vector2 direction = Vector2.Zero;

                foreach (var key in keys)
                {
                    switch (key)
                    {
                        case Keys.A:
                            direction.X -= 1;
                            break;
                        case Keys.D:
                            direction.X += 1;
                            break;
                        case Keys.S:
                            direction.Y += 1;
                            break;
                        case Keys.W:
                            direction.Y -= 1;
                            break;
                        default:
                            break;
                    }
                }

                foreach(var player in players)
                {
                    // Update movement.
                    Movement m = (Movement)_entityManager.GetComponent(player, ComponentType.Movement);
                    m.Velocity += direction * m.Speed * (gameTime.ElapsedGameTime.Milliseconds/16.0f);

                    // Update camera. 
                    //TODO: Adjust for multiple players.
                    _camera.Update(m.Position, gameTime);
                }

                
            }

        }
    }
}
