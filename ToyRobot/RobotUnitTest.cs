using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToyRobot
{
    [TestClass]
    public class RobotUnitTest
    {
        private Robot Robot;

        public RobotUnitTest()
        {
            Robot = new Robot();
        }

        [TestMethod]
        public void ShouldReturnFalseIfFirstCommandIsNull()
        {
            //arrange
            string command = null;

            //action
            var response = Robot.HandleCommand(command);

            //assert
            Assert.IsFalse(response);
        }

        [TestMethod]
        public void ShouldReturnFalseIfFirstCommandIsEmpty()
        {
            //arrange
            string command = string.Empty;

            //action
            var response = Robot.HandleCommand(command);

            //assert
            Assert.IsFalse(response);
        }

        [TestMethod]
        public void ShouldReturnFalseIfFirstCommandIsNotValid()
        {
            //arrange
            string command = "EAST 0,0,NORTH";

            //action
            var response = Robot.HandleCommand(command);

            //assert
            Assert.IsFalse(response);
        }

        [DataTestMethod]
        [DataRow("PLA 0,0,NORTH")]
        [DataRow("PLA 0,0,NORTHDS")]
        public void ShouldReturnFalseIfFirstCommandIsNotValidPlaceCommand(string command)
        {
            //arrange

            //action
            var response = Robot.HandleCommand(command);

            //assert
            Assert.IsFalse(response);
        }

        [TestMethod]
        public void ShouldReturnTrueIfFirstCommandIsValidPlaceCommand()
        {
            //arrange
            string command = "PLACE 0,0,NORTH";

            //action
            var response = Robot.HandleCommand(command);

            //assert
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void ShouldReturnFalseIfFirstCommandIsOutOfBounderies()
        {
            //arrange
            string command = "PLACE 9,1,NORTH";

            //action
            var response = Robot.HandleCommand(command);

            //assert
            Assert.IsFalse(response);
        }

        [TestMethod]
        public void ShouldReturnTrueIfFirstCommandIsNotOutOfBounderies()
        {
            //arrange
            string command = "PLACE 1,1,NORTH";

            //action
            var response = Robot.HandleCommand(command);

            //assert
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void ShouldReturnTrueIfSecondCommandIsValid()
        {
            //arrange
            string firstCommand = "PLACE 1,1,NORTH";
            string secondCommand = "MOVE";

            //action
            var firstReponse = Robot.HandleCommand(firstCommand);

            var secondReponse = Robot.HandleCommand(secondCommand);

            //assert
            Assert.IsTrue(firstReponse);
            Assert.IsTrue(secondReponse);
        }

        [TestMethod]
        public void ShouldReturnValidLogOutPutForMove()
        {
            //arrange
            Robot rb = new();

            string firstCommand = "PLACE 0,0,NORTH";
            string secondCommand = "MOVE";
            string thirdCommand = "REPORT";

            string expectedOutput = "Output: 0,1,NORTH";

            //action
            var firstReponse = rb.HandleCommand(firstCommand);

            var secondReponse = rb.HandleCommand(secondCommand);

            var thirdReponse = rb.HandleCommand(thirdCommand);

            //assert
            Assert.IsTrue(firstReponse);
            Assert.IsTrue(secondReponse);
            Assert.IsTrue(thirdReponse);

            Assert.AreEqual(rb.LogOutPut, expectedOutput);
        }

        [TestMethod]
        public void ShouldReturnValidLogOutPutForRightForLeft()
        {
            //arrange
            Robot rb = new();

            string firstCommand = "PLACE 0,0,NORTH";
            string secondCommand = "LEFT";
            string thirdCommand = "REPORT";

            string expectedOutput = "Output: 0,0,WEST";

            //action
            var firstReponse = rb.HandleCommand(firstCommand);

            var secondReponse = rb.HandleCommand(secondCommand);

            var thirdReponse = rb.HandleCommand(thirdCommand);

            //assert
            Assert.IsTrue(firstReponse);
            Assert.IsFalse(secondReponse);
            Assert.IsTrue(thirdReponse);

            Assert.AreEqual(rb.LogOutPut, expectedOutput);
        }

        [TestMethod]
        public void ShouldReturnValidLogOutPutForRightForRight()
        {
            //arrange
            Robot rb = new();

            string firstCommand = "PLACE 0,0,NORTH";
            string secondCommand = "RIGHT";
            string thirdCommand = "REPORT";

            string expectedOutput = "Output: 0,0,EAST";

            //action
            var firstReponse = rb.HandleCommand(firstCommand);

            var secondReponse = rb.HandleCommand(secondCommand);

            var thirdReponse = rb.HandleCommand(thirdCommand);

            //assert
            Assert.IsTrue(firstReponse);
            Assert.IsTrue(secondReponse);
            Assert.IsTrue(thirdReponse);

            Assert.AreEqual(rb.LogOutPut, expectedOutput);
        }

        [TestMethod]
        public void ShouldReturnValidLogOutForTestCase3()
        {
            //arrange
            Robot rb = new();

            string firstCommand = "PLACE 1,2,EAST";

            string expectedOutput = "Output: 3,3,NORTH";

            //action
            rb.HandleCommand(firstCommand);

            rb.HandleCommand("MOVE");
            rb.HandleCommand("MOVE");
            rb.HandleCommand("LEFT");
            rb.HandleCommand("MOVE");
            rb.HandleCommand("REPORT");

            Assert.AreEqual(rb.LogOutPut, expectedOutput);
        }

        [TestMethod]
        public void ShouldReturnValidLogOutForTestCase4()
        {
            //arrange
            Robot rb = new();

            string firstCommand = "PLACE 1,2,EAST";

            string expectedOutput = "Output: 3,2,NORTH";

            //action
            rb.HandleCommand(firstCommand);

            rb.HandleCommand("MOVE");
            rb.HandleCommand("LEFT");
            rb.HandleCommand("MOVE");
            rb.HandleCommand("PLACE 3,1");
            rb.HandleCommand("MOVE");
            rb.HandleCommand("REPORT");

            Assert.AreEqual(rb.LogOutPut, expectedOutput);
        }
    }
}
