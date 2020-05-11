using System;
using KanbanProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestCreateTask()
        {
            DateTime dateTime = new DateTime();
            Column column = new Column("");
            Task res = column.createTask("", "ok", dateTime);//title length fail
            Assert.AreEqual(res, null);

            string s = new string('d', 330);
            Task res2 = column.createTask("ok", s, dateTime);//text length fail
            Assert.AreEqual(res2, null);

            string s1 = new string('d', 60);
            Task res3 = column.createTask(s1, "ok", dateTime);//title length fail
            Assert.AreEqual(res3, null);

            /* Guid res4 = column.createTask("ok", null, dateTime);//null fail
             Assert.AreEqual(res4, Guid.Empty);*/

            column.limTasks(1);
            Task res4 = column.createTask("ok", "ok", dateTime);//should work
            Assert.AreNotEqual(res4, null);

            Task res5 = column.createTask("ok", "ok", dateTime);//limit fail
            Assert.AreEqual(res5, null);
        }

        [TestMethod]
        public void TestRemoveTask()
        {
            Column column = new Column("");
            Task t = column.removeTask(column.createTask("sh", "ir", new DateTime()).getTaskid());
            Assert.AreNotEqual(t, null); //should work

            t = column.removeTask(Guid.NewGuid());
            Assert.AreEqual(t, null); //trying to delete a task that does not exist
        }
    }
}
