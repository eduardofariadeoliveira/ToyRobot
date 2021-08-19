using NUnit.Framework;
using ToyRobot;

namespace ToyRobotTDD
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_CommandBePLACE_When_FirstCommand()
        {
            var commandClass = new Commands();
            var command = string.Empty;
            var ret = string.Empty;

            command = "MOVE";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("First command must be PLACE", ret);

            command = "LEFT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("First command must be PLACE", ret);

            command = "RIGHT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("First command must be PLACE", ret);

            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("First command must be PLACE", ret);

            command = "PLACE 0,0,NORTH";
            ret = commandClass.RunCommand(command);
            Assert.IsEmpty(ret);
        }

        [Test]
        public void Should_DisplayInitialPosition_When_SetPlaceThenCallReport()
        {
            var commandClass = new Commands();
            var command = string.Empty;
            var ret = string.Empty;

            command = "PLACE 0,0,NORTH";
            commandClass.RunCommand(command);

            command = "REPORT";
            ret = commandClass.RunCommand(command);

            Assert.AreEqual("0,0,NORTH", ret);
        }

        [Test]
        public void Should_PreventFalling_When_ReachTheTableLimits()
        {
            var commandClass = new Commands();
            var command = string.Empty;
            var ret = string.Empty;

            command = "PLACE 0,4,NORTH";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("0,5,NORTH", ret);

            command = "PLACE 0,0,SOUTH";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("0,0,SOUTH", ret);

            command = "PLACE 4,1,EAST";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("5,1,EAST", ret);

            command = "PLACE 2,5,WEST";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("0,5,WEST", ret);
        }

        [Test]
        public void Should_MoveNextPosition_When_CommandMoveAndDependingOnDirection()
        {
            var commandClass = new Commands();
            var command = string.Empty;
            var ret = string.Empty;

            command = "PLACE 1,1,NORTH";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("1,2,NORTH", ret);
        }

        [Test]
        public void Should_Ignore_When_PlaceAreOutOfTableLimits()
        {
            var commandClass = new Commands();
            var command = string.Empty;
            var ret = string.Empty;

            command = "PLACE 7,0,NORTH";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("First command must be PLACE", ret);

            command = "PLACE -1,0,NORTH";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("First command must be PLACE", ret);

            command = "PLACE 0,0,NORTH";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("0,1,NORTH", ret);
        }

        [Test]
        public void Should_TurnTheRobot_When_UseRightOrLeft()
        {
            var commandClass = new Commands();
            var command = string.Empty;
            var ret = string.Empty;

            command = "PLACE 1,1,NORTH";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("1,2,NORTH", ret);

            command = "RIGHT";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("2,2,EAST", ret);

            command = "RIGHT";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("2,0,SOUTH", ret);

            command = "LEFT";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "LEFT";
            commandClass.RunCommand(command);
            command = "MOVE";
            commandClass.RunCommand(command);
            command = "REPORT";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("3,1,NORTH", ret);
        }

        [Test]
        public void Should_Discard_When_InvalidCommandAndParameters()
        {
            var commandClass = new Commands();
            var command = string.Empty;
            var ret = string.Empty;

            command = "PLACE A,1,NORTH";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("Command ignored", ret);

            command = "PLACE 0,0,NORTH";
            ret = commandClass.RunCommand(command);
            command = "JUMP";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("Command ignored", ret);

            command = "PLACE 0,0,HALF";
            ret = commandClass.RunCommand(command);
            command = "JUMP";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("Command ignored", ret);
        }

        [Test]
        public void Should_Ignore_When_FirstPLACEhasNoDirection()
        {
            var commandClass = new Commands();
            var command = string.Empty;
            var ret = string.Empty;

            command = "PLACE 1,1";
            ret = commandClass.RunCommand(command);
            Assert.AreEqual("Command ignored", ret);
        }

    }
}