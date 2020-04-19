using System.Collections.Generic;

namespace WalkerGame.Data
{
    public class GridNode<T> where T : struct
    {
        private SortedSet<T> entities;

        public GridNode()
        {
            entities = new SortedSet<T>();
        }

        public bool Empty()
        {
            return entities.Count == 0;
        }

        public bool Insert(T entity)
        {
            return entities.Add(entity);
        }

        public bool Remove(T entity)
        {
            return entities.Remove(entity);
        }

        public SortedSet<T> GetEntities()
        {
            return entities;
        }

        public void Merge(SortedSet<T> entityList)
        {
            entityList.UnionWith(entities);
        }
    }
}