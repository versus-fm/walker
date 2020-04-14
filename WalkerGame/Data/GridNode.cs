using System.Collections.Generic;

namespace WalkerGame.Data
{
    public class GridNode
    {
        private SortedSet<ulong> entities;

        public GridNode()
        {
            entities = new SortedSet<ulong>();
        }

        public bool Empty()
        {
            return entities.Count == 0;
        }

        public bool Insert(ulong entity)
        {
            return entities.Add(entity);
        }

        public bool Remove(ulong entity)
        {
            return entities.Remove(entity);
        }

        public SortedSet<ulong> GetEntities()
        {
            return entities;
        }

        public void Merge(SortedSet<ulong> entityList)
        {
            entityList.UnionWith(entities);
        }
    }
}