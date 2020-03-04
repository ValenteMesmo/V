using Microsoft.Xna.Framework;
using MonoGameFacade.Core;
using System;
using System.Collections.Generic;

namespace MonogameFacade.Core
{
    public class QuadTree
    {
        private readonly Node Root;
        public static Pool<Node> NodePool = new Pool<Node>(() => new Node());

        public QuadTree(Rectangle bounds, int maxDepth, int maxChildren)
        {
            var node = NodePool.Obtain();
            node.Bounds = bounds;
            node.Depth = 0;
            node.MaxDepth = maxDepth;
            node.MaxChildren = maxChildren;
            Root = node;
        }

        public void Add(Collider item)
        {
            Root.Add(item);
        }

        public void AddRange(IEnumerable<Collider> itens)
        {
            foreach (var item in itens)
                Root.Add(item);
        }

        public Collider[] Get(Collider item)
        {
            return Root.Get(item);
        }

        public void Clear()
        {
            Root.Clear();
        }

        internal void DrawDebug()
        {
            //Root.DrawDebug();
        }
    }

    public class Node
    {
        public Rectangle Bounds;
        public int Depth;
        public int MaxDepth;
        public int MaxChildren;
        private List<Node> Nodes = new List<Node>();
        private List<Collider> Children = new List<Collider>();
        private List<Collider> StuckChildren = new List<Collider>();
        private const int TOP_LEFT = 0;
        private const int TOP_RIGHT = 1;
        private const int BOTTOM_LEFT = 2;
        private const int BOTTOM_RIGHT = 3;

        public void Add(Collider item)
        {
            if (Nodes.Count > 0)
            {
                var index = FindIndex(item);
                var node = Nodes[index];

                if (item.Area.X >= node.Bounds.X
                    && item.Area.X + item.Width <= node.Bounds.X + node.Bounds.Width
                    && item.Area.Y >= node.Bounds.Y
                    && item.Area.Y + item.Height <= node.Bounds.Y + node.Bounds.Height)
                    Nodes[index].Add(item);
                else
                    StuckChildren.Add(item);

                return;
            }

            Children.Add(item);

            if (Depth < MaxDepth && Children.Count > MaxChildren)
            {
                Subdivide();

                for (int i = 0; i < Children.Count; i++)
                    Add(Children[i]);

                Children.Clear();
            }
        }

        public void Clear()
        {
            StuckChildren.Clear();
            Children.Clear();            

            if (Nodes.Count == 0)
                return;

            for (var i = 0; i < Nodes.Count; i++)
            {
                QuadTree.NodePool.Free(Nodes[i]);
                Nodes[i].Clear();
            }

            Nodes.Clear();
        }

        private Collider[] GetAllContent(List<Collider> Out = null)
        {
            if (Out == null)
                Out = new List<Collider>();

            if (Nodes.Count > 0)
            {
                for (var i = 0; i < Nodes.Count; i++)
                {
                    Nodes[i].GetAllContent(Out);
                }
            }
            Out.AddRange(StuckChildren);
            Out.AddRange(Children);

            return Out.ToArray();
        }

        public Collider[] Get(Collider item)
        {
            var Out = new List<Collider>();

            if (Nodes.Count > 0)
            {
                var index = FindIndex(item);
                var node = Nodes[index];

                if (item.Area.X >= node.Bounds.X
                    && item.Area.X + item.Width <= node.Bounds.X + node.Bounds.Width
                    && item.Area.Y >= node.Bounds.Y
                    && item.Area.Y + item.Height <= node.Bounds.Y + node.Bounds.Height)
                {
                    Out.AddRange(Nodes[index].Get(item));
                }
                else
                {
                    //Part of the item are overlapping multiple child nodes. For each of the overlapping nodes, return all containing objects.
                    if (item.Area.X <= Nodes[TOP_RIGHT].Bounds.X)
                    {
                        if (item.Area.Y <= Nodes[BOTTOM_LEFT].Bounds.Y)
                            Out.AddRange(Nodes[TOP_LEFT].GetAllContent());

                        if (item.Area.Y + item.Height > Nodes[BOTTOM_LEFT].Bounds.Y)
                            Out.AddRange(Nodes[BOTTOM_LEFT].GetAllContent());
                    }

                    if (item.Area.X + item.Width > Nodes[TOP_RIGHT].Bounds.X)
                    {//position+width bigger than middle x
                        if (item.Area.Y <= Nodes[BOTTOM_RIGHT].Bounds.Y)
                            Out.AddRange(Nodes[TOP_RIGHT].GetAllContent());

                        if (item.Area.Y + item.Height > Nodes[BOTTOM_RIGHT].Bounds.Y)
                            Out.AddRange(Nodes[BOTTOM_RIGHT].GetAllContent());
                    }
                }
            }

            Out.AddRange(StuckChildren);
            Out.AddRange(Children);

            return Out.ToArray();
        }

        private int FindIndex(Collider item)
        {
            var b = Bounds;
            var left = (item.Area.X > b.X + b.Width / 2) ? false : true;
            var top = (item.Area.Y > b.Y + b.Height / 2) ? false : true;

            //top left
            var index = TOP_LEFT;
            if (left)
            {
                //left side
                if (!top)
                {
                    //bottom left
                    index = BOTTOM_LEFT;
                }
            }
            else
            {
                //right side
                if (top)
                {
                    //top right
                    index = TOP_RIGHT;
                }
                else
                {
                    //bottom right
                    index = BOTTOM_RIGHT;
                }
            }

            return index;
        }

        private void Subdivide()
        {
            var depth = Depth + 1;

            var bx = Bounds.X;
            var by = Bounds.Y;

            //floor the values
            var b_w_h = (Bounds.Width / 2); //todo: Math.floor?
            var b_h_h = (Bounds.Height / 2);
            var bx_b_w_h = bx + b_w_h;
            var by_b_h_h = by + b_h_h;

            //TOP_LEFT
            {
                var node = QuadTree.NodePool.Obtain();
                node.Bounds.X = bx;
                node.Bounds.Y = by;
                node.Bounds.Width = b_w_h;
                node.Bounds.Height = b_h_h;
                node.Depth = depth;
                node.MaxDepth = MaxDepth;
                node.MaxChildren = MaxChildren;

                Nodes.Add(node);
            }

            //TOP_RIGHT
            {
                var node = QuadTree.NodePool.Obtain();
                node.Bounds.X = bx_b_w_h;
                node.Bounds.Y = by;
                node.Bounds.Width = b_w_h;
                node.Bounds.Height = b_h_h;
                node.Depth = depth;
                node.MaxDepth = MaxDepth;
                node.MaxChildren = MaxChildren;

                Nodes.Add(node);
            }

            //BOTTOM_LEFT
            {
                var node = QuadTree.NodePool.Obtain();
                node.Bounds.X = bx;
                node.Bounds.Y = by_b_h_h;
                node.Bounds.Width = b_w_h;
                node.Bounds.Height = b_h_h;
                node.Depth = depth;
                node.MaxDepth = MaxDepth;
                node.MaxChildren = MaxChildren;

                Nodes.Add(node);
            }

            //BOTTOM_RIGHT      
            {
                var node = QuadTree.NodePool.Obtain();
                node.Bounds.X = bx_b_w_h;
                node.Bounds.Y = by_b_h_h;
                node.Bounds.Width = b_w_h;
                node.Bounds.Height = b_h_h;
                node.Depth = depth;
                node.MaxDepth = MaxDepth;
                node.MaxChildren = MaxChildren;

                Nodes.Add(node);
            }
          
        }

        //internal void DrawDebug()
        //{
        //    Game1.RectanglesToRender.Enqueue(Bounds);

        //    for (int i = 0; i < Nodes.Count; i++)
        //        Nodes[i].DrawDebug();

        //    var currentContent = GetAllContent();

        //    for (int i = 0; i < currentContent.Length; i++)
        //        Game1.RectanglesToRender.Enqueue(currentContent[i].AsRectangle());
        //}
    }
}
