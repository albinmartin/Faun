﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faun.Components;

namespace Faun.Entities
{
    public class Entity
    {
        // TODO: Could change _components to array with index map for performance on many components.
        private List<Component> _components;
        private ComponentType _componentMask;

        public List<Component> Components { get => _components; }
        public ComponentType ComponentMask { get => _componentMask;}

        public Entity()
        {
            _components = new List<Component>();
            _componentMask = 0;
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);
            _componentMask = _componentMask | component.ComponentType;
        }

        public void Startup()
        {
            foreach(var component in _components)
            {
                component.OnStartup();
            }
        }

        public void Shutdown()
        {
            foreach(var component in _components)
            {
                component.OnShutdown();
            }
        }

        public Component GetComponent (ComponentType type)
        {
            // Check if entity has component with componentMask.
            if ((_componentMask & type) == type)
            {
                // Find component.
                foreach (var component in _components)
                {
                    if (component.ComponentType == type)
                    {
                        return component;
                    }
                }
            }

            return null;
        }

        public bool HasComponent(ComponentType type)
        {
            return (_componentMask & type) == type;
        }
    }
}
