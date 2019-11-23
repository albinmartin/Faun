using Faun.Controls;
using Faun.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faun.Components
{
    class Player : Component
    {
        public Player(Entity entity) 
            :base(entity)
        {
            _type = ComponentType.Player;
        }

        public override void OnShutdown()
        {
        }

        public override void OnStartup()
        {
        }
    }
}
