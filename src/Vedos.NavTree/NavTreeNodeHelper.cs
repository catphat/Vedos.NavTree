using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vedos.NavTree
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

        public static IEnumerable<NavTreeNode> ParentBreadthFirstTraversal(this NavTreeNode node)
        {
            var nodes = new Queue<NavTreeNode>(new[] {node});
            while (nodes.Any())
            {
                var n = nodes.Dequeue();
                yield return n;
                if (n.Parent != null)
                {
                    nodes.Enqueue(n.Parent);
                }
            }
        }
        
        public static IEnumerable<NavTreeNode> Descendents(this NavTreeNode root)
        {
            return root.BreadthFirstTraversal().Where(x => x.Id != root.Id);
        }

    }
}