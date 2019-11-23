using Faun.Components;
using Faun.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faun.Systems
{
    public abstract class GameSystem
    {
        protected EntityManager _entityManager;
        protected ComponentType _entitySet;

        protected EntityManager EntityManager { get => _entityManager; }

        public ComponentType EntitySet { get => _entitySet; }

        public GameSystem(EntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public abstract void Update(GameTime gameTime);
    }
}
