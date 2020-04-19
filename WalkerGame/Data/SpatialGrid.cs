using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Svelto.ECS;
using WalkerGame.Metadata;

using NodeType = Svelto.ECS.EGID;

namespace WalkerGame.Data
{
    [Service]
    public class SpatialGrid
    {
        private const int CELL_SIZE = 32;
        private Dictionary<Point2, GridNode<NodeType>> nodes;

        [Inject]
        public SpatialGrid()
        {
            nodes = new Dictionary<Point2, GridNode<NodeType>>();
        }

        public void InsertEntity(NodeType entity, ref RectangleF bounds)
        {
            var p = new Point2(bounds.X / CELL_SIZE, bounds.Y / CELL_SIZE);
            var pEnd = new Point2((bounds.X + bounds.Width) / CELL_SIZE, (bounds.Y + bounds.Height) / CELL_SIZE);
            for (; p.X <= pEnd.X; p.X++)
            {
                for (; p.Y <= pEnd.Y; p.Y++)
                {
                    GridNode<NodeType> gridNode;
                    if (nodes.ContainsKey(p))
                    {
                        gridNode = nodes[p];
                    }
                    else
                    {
                        gridNode = new GridNode<NodeType>();
                        nodes.Add(p, gridNode);
                    }

                    gridNode.Insert(entity);
                }
            }
        }

        public void RemoveEntity(NodeType  entity, ref RectangleF bounds)
        {
            var p = new Point2(bounds.X / CELL_SIZE, bounds.Y / CELL_SIZE);
            var pEnd = new Point2((bounds.X + bounds.Width) / CELL_SIZE, (bounds.Y + bounds.Height) / CELL_SIZE);
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

        public void MoveEntity(NodeType  entity, ref RectangleF oldBounds, ref RectangleF newBounds)
        {
            var oldPoints = GetPointRange(ref oldBounds);
            var newPoints = GetPointRange(ref newBounds);

            if (oldPoints.p == newPoints.p && oldPoints.pEnd == newPoints.pEnd)
                return;
            
            RemoveEntity(entity, ref oldBounds);
            InsertEntity(entity, ref newBounds);
        }

        private (Point2 p, Point2 pEnd) GetPointRange(ref RectangleF bounds)
        {
            var p = new Point2(bounds.X / CELL_SIZE, bounds.Y / CELL_SIZE);
            var pEnd = new Point2((bounds.X + bounds.Width) / CELL_SIZE, (bounds.Y + bounds.Height) / CELL_SIZE);
            return (p, pEnd);
        }

        public void QueryRegion(SortedSet<NodeType> entityList, ref RectangleF region)
        {
            entityList.Clear();
            var p = new Point2(region.X / CELL_SIZE, region.Y / CELL_SIZE);
            var pEnd = new Point2((region.X + region.Width) / CELL_SIZE, (region.Y + region.Height) / CELL_SIZE);
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