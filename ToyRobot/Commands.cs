using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot
{
    public class Commands
    {
        bool firstCommand = true;
        int x = 0;
        int y = 0;
        readonly int yLimit = 5;
        readonly int xLimit = 5;
        string direction = string.Empty;
        List<string> possibleDirections = new List<string> { "north", "south", "east", "west" };

        public string RunCommand(string command)
        {
            var commandArr = command.ToLower().Split(' ');
            if (firstCommand)
            {
                if (commandArr[0] == "place")
                {
                    if (IsValidPlace(commandArr, firstCommand))
                    {
                        firstCommand = !firstCommand;
                    }
                }
                else
                {
                    return "First command must be PLACE";
                }
            }

            switch (commandArr[0])
            {
                case "place":
                    if (IsValidPlace(commandArr, firstCommand))
                    {
                        SetPlace(commandArr);
                    }
                    else
                    {
                        return "Command ignored";
                    }
                    break;
                case "report":
                    return GetReport();
                case "move":
                    SetMove();
                    break;
                case "left":
                    SetLeft();
                    break;
                case "right":
                    SetRight();
                    break;
                default:
                    return "Command ignored";
            }

            return string.Empty;

        }

        private void SetLeft()
        {
            switch (direction)
            {
                case "north":
                    direction = "west";
                    break;
                case "south":
                    direction = "east";
                    break;
                case "east":
                    direction = "north";
                    break;
                case "west":
                    direction = "south";
                    break;
            }
        }

        private void SetRight()
        {
            switch (direction)
            {
                case "north":
                    direction = "east";
                    break;
                case "south":
                    direction = "west";
                    break;
                case "east":
                    direction = "south";
                    break;
                case "west":
                    direction = "north";
                    break;
            }
        }

        private void SetMove()
        {
            switch (direction)
            {
                case "north":
                    if (y < yLimit)
                    {
                        y++;
                    }
                    break;
                case "south":
                    if (y > 0)
                    {
                        y--;
                    }
                    break;
                case "east":
                    if (x < xLimit)
                    {
                        x++;
                    }
                    break;
                case "west":
                    if (x > 0)
                    {
                        x--;
                    }
                    break;
            }
        }

        private string GetReport()
        {
            return string.Format("{0},{1},{2}", x, y, direction.ToUpper());
        }

        private bool IsValidPlace(string[] commandArr, bool firstCommand = false)
        {
            if (commandArr.Length < 2) return false;

            string[] pos = commandArr[1].Split(",");

            int tempX = 0;
            var tempY = 0;
            if (!int.TryParse(pos[0], out tempX)) return false;
            if (!int.TryParse(pos[1], out tempY)) return false;

            if (firstCommand && pos.Length < 3) return false;

            if (pos.Length == 3)
            {
                if (!possibleDirections.Contains(pos[2])) return false;
            }

            if (tempX >= 0 && tempX <= xLimit && tempY >= 0 && tempY <= yLimit)
            {
                return true;
            }

            return false;

        }

        private void SetPlace(string[] commandArr)
        {
            if (!IsValidPlace(commandArr)) return;

            string[] pos = commandArr[1].Split(",");

            var tempX = int.Parse(pos[0]);
            var tempY = int.Parse(pos[1]);

            if (tempX >= 0 && tempX <= xLimit && tempY >= 0 && tempY <= yLimit)
            {
                x = tempX;
                y = tempY;

                if (pos.Length == 3)
                {
                    direction = pos[2];
                }

            }

        }
    }
}
