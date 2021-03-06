﻿using Faun.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faun.Components
{
    public enum Direction { Left = 0, Right = 1, Up = 2, Down = 3}

    public class Movement : Component
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public Direction Facing { get; set; }
        

        public Movement(Entity entity) 
            :base(entity)
        {
            this._type = ComponentType.Movement;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Speed = 1.0f;
        }

        public Movement(Entity entity, Vector2 position)
            : base(entity)
        {
            this._type = ComponentType.Movement;
            Position = position;
            Velocity = Vector2.Zero;
        }

        public override void OnShutdown()
        {
        }

        public override void OnStartup()
        {
        }
    }
}
