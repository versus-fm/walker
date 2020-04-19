using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WalkerGame.Metadata;
using WalkerGame.Reflection;

namespace WalkerGame.Logic
{
    [Service]
    public class PartManager : PostConstruct
    {
        private readonly ObjectGraph objectGraph;

        private List<DrawTarget> drawParts;
        private List<UpdateTarget> updateParts;
        private List<ContentTarget> contentParts;
        private List<PostUpdateTarget> postUpdateTargets;
        private List<PreUpdateTarget> preUpdateTargets;
        private List<ReadyTarget> readyTargets;

        [Inject]
        public PartManager(ObjectGraph objectGraph)
        {
            this.objectGraph = objectGraph;
            
            drawParts = new List<DrawTarget>();
            updateParts = new List<UpdateTarget>();
            contentParts = new List<ContentTarget>();
            postUpdateTargets = new List<PostUpdateTarget>();
            preUpdateTargets = new List<PreUpdateTarget>();
            readyTargets = new List<ReadyTarget>();
        }
        public void Load()
        {
            contentParts.ForEach(x => x.Load());
        }

        public void Update(GameTime gameTime)
        {
            updateParts.ForEach(x => x.Update(gameTime));
        }

        public void Draw(GameTime gameTime)
        {
            drawParts.ForEach(x => x.Draw(gameTime));
        }

        public void PreUpdate(GameTime gameTime)
        {
            preUpdateTargets.ForEach(x => x.PreUpdate(gameTime));
        }

        public void PostUpdate(GameTime gameTime)
        {
            postUpdateTargets.ForEach(x => x.PostUpdate(gameTime));
        }

        public void Ready()
        {
            readyTargets.ForEach(x => x.ServiceReady());
        }
        
        public void Post()
        {
            objectGraph.DoOnAttribute<GamePartAttribute>((attribute, type) =>
            {
                if (type.Implements<PartTarget>())
                {
                    var serviceType = attribute.ForType ?? type;
                    var target = objectGraph.Get(serviceType);
                    if (type.Implements<DrawTarget>())
                    {
                        drawParts.Add((DrawTarget)target);
                    }
                    if (type.Implements<UpdateTarget>())
                    {
                        updateParts.Add((UpdateTarget)target);
                    }
                    if (type.Implements<ContentTarget>())
                    {
                        contentParts.Add((ContentTarget)target);
                    }
                    if (type.Implements<PostUpdateTarget>())
                    {
                        postUpdateTargets.Add((PostUpdateTarget)target);
                    }
                    if (type.Implements<PreUpdateTarget>())
                    {
                        preUpdateTargets.Add((PreUpdateTarget)target);
                    }
                    if (type.Implements<ReadyTarget>())
                    {
                        readyTargets.Add((ReadyTarget)target);
                    }
                }
            });
        }
    }
}