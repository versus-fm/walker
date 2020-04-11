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

        [Inject]
        public PartManager(ObjectGraph objectGraph)
        {
            this.objectGraph = objectGraph;
            
            drawParts = new List<DrawTarget>();
            updateParts = new List<UpdateTarget>();
            contentParts = new List<ContentTarget>();
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
        public void Post()
        {
            objectGraph.DoOnAttribute<GamePartAttribute>((attribute, type) =>
            {
                if (type.Implements<PartTarget>())
                {
                    var target = objectGraph.Construct(type);
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
                }
            });
        }
    }
}