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
            Assert.Null(navTree.Parent);
            Assert.Equal(2, navTree.Children().Count());
            Assert.Equal(3, navTree.Children().First(x => x.Id == "root-child-a").Children().Count());
            Assert.Equal(3, navTree.Children().First(x => x.Id == "root-child-b").Children().Count());
            Assert.Single(navTree.Children().First(x => x.Id == "root-child-b").Children().First(x => x.Id == "2-child-of-b"));
        }

        [Fact]
        public void CanLocateChildNodeById()
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


             var firstChild = navTree.GetChild("root-child-b");
             Assert.NotNull(firstChild);
             Assert.Equal("root", firstChild.Parent.Id);

             var secondChild = firstChild.GetChild("2-child-of-b");
             Assert.NotNull(secondChild);
             Assert.Equal("root-child-b", secondChild.Parent.Id);

             var thirdChild = secondChild.GetChild("0-child-of-2b");
             Assert.NotNull(thirdChild);
             Assert.Equal("2-child-of-b", thirdChild.Parent.Id);
        }
    }
}