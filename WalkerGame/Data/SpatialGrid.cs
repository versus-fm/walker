using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WalkerGame.Metadata;

namespace WalkerGame.Data
{
    [Service]
    public class SpatialGrid
    {
        private const int CELL_SIZE = 32;
        private Dictionary<Point, GridNode> nodes;

        private SpatialGrid()
        {
            nodes = new Dictionary<Point, GridNode>();
        }

        public void InsertEntity(ulong entity, ref Rectangle bounds)
        {
            var p = new Point(bounds.X / CELL_SIZE, bounds.Y / CELL_SIZE);
            var pEnd = new Point((bounds.X + bounds.Width) / CELL_SIZE, (bounds.Y + bounds.Height) / CELL_SIZE);
            for (; p.X <= pEnd.X; p.X++)
            {
                for (; p.Y <= pEnd.Y; p.Y++)
                {
                    GridNode gridNode;
                    if (nodes.ContainsKey(p))
                    {
                        gridNode = nodes[p];
                    }
                    else
                    {
                        gridNode = new GridNode();
                        nodes.Add(p, gridNode);
                    }

                    gridNode.Insert(entity);
                }
            }
        }

        public void RemoveEntity(ulong entity, ref Rectangle bounds)
        {
            var p = new Point(bounds.X / CELL_SIZE, bounds.Y / CELL_SIZE);
            var pEnd = new Point((bounds.X + bounds.Width) / CELL_SIZE, (bounds.Y + bounds.Height) / CELL_SIZE);
            for (; p.X <= pEnd.X; p.X++)
            {
                for (; p.Y <= pEnd.Y; p.Y++)
                {
                    if (nodes.TryGetValue(p, out var node))
                    {
                        node.Remove(entity);
                    }
                }
            }
        }

        public void MoveEntity(ulong entity, ref Rectangle oldBounds, ref Rectangle newBounds)
        {
            var oldPoints = GetPointRange(ref oldBounds);
            var newPoints = GetPointRange(ref newBounds);

            if (oldPoints.p == newPoints.p && oldPoints.pEnd == newPoints.pEnd)
                return;
            
            RemoveEntity(entity, ref oldBounds);
            InsertEntity(entity, ref newBounds);
        }

        private (Point p, Point pEnd) GetPointRange(ref Rectangle bounds)
        {
            var p = new Point(bounds.X / CELL_SIZE, bounds.Y / CELL_SIZE);
            var pEnd = new Point((bounds.X + bounds.Width) / CELL_SIZE, (bounds.Y + bounds.Height) / CELL_SIZE);
            return (p, pEnd);
        }

        public void QueryRegion(SortedSet<ulong> entityList, ref Rectangle region)
        {
            entityList.Clear();
            var p = new Point(region.X / CELL_SIZE, region.Y / CELL_SIZE);
            var pEnd = new Point((region.X + region.Width) / CELL_SIZE, (region.Y + region.Height) / CELL_SIZE);
            for (; p.X <= pEnd.X; p.X++)
            {
                for (; p.Y <= pEnd.Y; p.Y++)
                {
                    if (nodes.TryGetValue(p, out var node))
                    {
                        node.Merge(entityList);
                    }
                }
            }
        }
    }
}