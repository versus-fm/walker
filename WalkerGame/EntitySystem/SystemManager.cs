using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WalkerGame.Logic;
using WalkerGame.Metadata;
using WalkerGame.Reflection;
using NotImplementedException = System.NotImplementedException;

namespace WalkerGame.EntitySystem
{
    [GamePart("systems")]
    public class SystemManager : UpdateTarget, DrawTarget, PostConstruct
    {
        private readonly ObjectGraph objectGraph;
        private List<ISystem> systems;

        [Inject]
        public SystemManager(ObjectGraph objectGraph)
        {
            this.objectGraph = objectGraph;
            systems = new List<ISystem>();
        }

        public void Update(GameTime gameTime)
        {
            systems.ForEach(x => x.Update(gameTime));
        }

        public void Draw(GameTime gameTime)
        {
            systems.ForEach(x => x.Draw(gameTime));
        }

        public void Post()
        {
            objectGraph.DoOnAttribute<SystemAttribute>((attribute, type) =>
            {
                if (type.Implements<ISystem>())
                {
                    var system = (ISystem) objectGraph.Construct(type);
                    if (attribute.RunBefore != null)
                    {
                        var i = 0;
                        for (; i < systems.Count; i++)
                        {
                            if (systems[i].GetType() == attribute.RunBefore)
                            {
                                break;
                            }
                        }
                        systems.Insert(i, system);
                    }
                    else
                    {
                        systems.Add(system);
                    }
                }
            });
        }
    }
}