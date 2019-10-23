using System;
using System.Linq;
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
            Assert.Single(navTree.Children().First(x => x.Id == "root-child-b").Children()
                .First(x => x.Id == "2-child-of-b"));
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


        [Fact]
        public void CanGetNodeDescendentsViaDescendentsStaticExtensionMethod()
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

            var firstChild = navTree.GetChild("root-child-a");
            Assert.NotNull(firstChild);
            
            var firstChildDescendents = firstChild.Descendents();
            Assert.Equal(3, firstChildDescendents.Count());
 
            var secondChild = navTree.GetChild("root-child-b");
            Assert.NotNull(secondChild);
            
            var secondChildDescendents = secondChild.Descendents();
            Assert.Equal(4, secondChildDescendents.Count());

            var lastChild = navTree.Descendents().Where(x => x.Id == "0-child-of-2b");
            Assert.NotNull(lastChild);

        }


        [Fact]
        public void CanGetParentsViaBreadthFirstTraversal()
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
            
            var lastChild = navTree.Descendents().First(x => x.Id == "0-child-of-2b");
            Assert.NotNull(lastChild);

            var parents = lastChild.ParentBreadthFirstTraversal().ToArray();
            Assert.Equal(4, parents.Count());
            
            Assert.Equal("0-child-of-2b", parents[0].Id);
            Assert.Equal("2-child-of-b", parents[1].Id);
            Assert.Equal("root-child-b", parents[2].Id);
            Assert.Equal("root", parents[3].Id);

        }

        [Fact]
        public static void ToPathReturnsValidPath()
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
             
             var lastChild = navTree.Descendents().First(x => x.Id == "0-child-of-2b");
             Assert.NotNull(lastChild);

             var path = lastChild.ToPath();
             Assert.Equal("/root/root-child-b/2-child-of-b/0-child-of-2b", path);
           
        }
    }
}