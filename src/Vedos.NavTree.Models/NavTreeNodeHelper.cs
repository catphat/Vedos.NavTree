using System.Collections.Generic;
using System.Linq;

namespace Vedos.NavTree.Models
{
    public static class NavTreeNodeHelper
    {
        public static IEnumerable<NavTreeNode> BreadthFirstTraversal(this NavTreeNode root)
        {
            var nodes = new Queue<NavTreeNode>(new[] {root});
            while (nodes.Any())
            {
                var node = nodes.Dequeue();
                yield return node;
                foreach (var cn in node.Children()) nodes.Enqueue(cn);
            }
        }

        public static IEnumerable<NavTreeNode> Descendents(this NavTreeNode root)
        {
            return root.BreadthFirstTraversal().Where(x => x.Id != root.Id);
        }
    }
}