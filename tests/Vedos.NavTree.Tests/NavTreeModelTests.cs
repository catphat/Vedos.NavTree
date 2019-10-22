using System;
using System.Linq;
using Vedos.NavTree.Models;
using Xunit;

namespace Vedos.NavTree.Tests
{
    public class NavTreeModelTests
    {
        [Fact]
        public void CanInstantiateNavTree()
        {
            var navTree = new NavTreeNode(id: "root");
            Assert.Equal("root", navTree.Id);
        }

        [Fact]
        public void CanInstantiateNavTreeWithChildNodes()
        {
            var navTree = new NavTreeNode("root")
            {
                new NavTreeNode("root-child-a")
                {
                    new NavTreeNode("0-child-of-a"),
                    new NavTreeNode("1-child-of-a"),
                    new NavTreeNode("2-child-of-a"),
                },
                new NavTreeNode("root-child-b")
                {
                    new NavTreeNode("0-child-of-b"),
                    new NavTreeNode("1-child-of-b"),
                    new NavTreeNode("2-child-of-b")
                    {
                        new NavTreeNode("0-child-of-2b")
                    }
                }
            };
            
            Assert.True(navTree.Any());
            Assert.Equal(2, navTree.Children().Count());
            Assert.Equal(3, navTree.Children().First(x => x.Id == "root-child-a").Children().Count());
            Assert.Equal(3, navTree.Children().First(x => x.Id == "root-child-b").Children().Count());
            Assert.Single(navTree.Children().First(x => x.Id == "root-child-b").Children().First(x => x.Id == "2-child-of-b"));
        }
    }
}