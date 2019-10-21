using System;
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
    }
}