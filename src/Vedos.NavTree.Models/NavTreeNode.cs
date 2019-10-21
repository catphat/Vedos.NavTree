using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Vedos.NavTree.Models
{
    public class NavTreeNode : IEnumerable<NavTreeNode>
    {
        private readonly Dictionary<string, NavTreeNode> _children = new Dictionary<string, NavTreeNode>();

        public readonly string Id;
        public NavTreeNode Parent { get; private set; }

        public NavTreeNode(string id)
        {
            Id = id;
        }

        public NavTreeNode GetChild(string id)
        {
            return _children[id];
        }

        public IEnumerable<NavTreeNode> Children()
        {
            return _children.Select(x => x.Value);
        }

        public void Add(NavTreeNode node)
        {
            node.Parent?._children.Remove(node.Id);
            node.Parent = this;
            _children.Add(node.Id, node);
        }

        public IEnumerator<NavTreeNode> GetEnumerator()
        {
            return _children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}