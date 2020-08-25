using WPFUndoRedo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http.Headers;
namespace WPFUndoRedo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShouldManageCommandHistory()
        {
            var t =  CommandWithUndoRedo.Create("test", Execute, Undo);
            t.Execute();
            t.History.Undo.Execute();          
        }

        public void Execute()
        {

        }
        public void Undo()
        {

        }
    }
}
