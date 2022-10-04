namespace ToyRobot
{
    public class Robot
    {
        public Robot() { }

        private const string NORTH = "NORTH";
        private const string SOUTH = "SOUTH";
        private const string EAST = "EAST";
        private const string WEST = "WEST";

        private const string LEFT = "LEFT";
        private const string RIGHT = "RIGHT";

        private const string MOVE = "MOVE";
        private const string PLACE = "PLACE";
        private const string REPORT = "REPORT";

        private const int XSize = 6;
        private const int YSize = 6;

        private int X { get; set; }
        private int Y { get; set; }
        private string FirstCommand { get; set; }
        private string[] FirstCommandLine { get; set; }
        private string CurrentDirection { get; set; }
        public string LogOutPut { get; set; }

        public bool HandleCommand(string command)
        {
            try
            {
                if (command == null)
                    throw new ArgumentNullException(command, "invalid command");

                string[] commandSanitized = SanitizeCommand(command);

                if(string.IsNullOrEmpty(this.FirstCommand))
                {
                    if (commandSanitized[0] != PLACE)
                        return false;
                }

                bool response = false;

                switch (commandSanitized[0])
                {
                    case PLACE:
                        if(IsValidPlaceCommand(commandSanitized))
                        {
                            int placeX = Convert.ToInt32(commandSanitized[1]);
                            int placeY = Convert.ToInt32(commandSanitized[2]);
                            string placeDirection = commandSanitized.Length > 3 ? commandSanitized[3] : string.Empty;

                            response = Place(placeX, placeY, placeDirection);
                            this.FirstCommandLine = commandSanitized;
                            this.FirstCommand = commandSanitized[0];
                        }
                        break;

                    case LEFT:
                        int leftX = Convert.ToInt32(this.FirstCommandLine[1]);
                        int leftY = Convert.ToInt32(this.FirstCommandLine[2]);

                        response = Left(leftX, leftY);
                        break;

                    case RIGHT:
                        int rightX = Convert.ToInt32(this.FirstCommandLine[1]);
                        int rightY = Convert.ToInt32(this.FirstCommandLine[2]);

                        response = Right(rightX, rightY);
                        break;

                    case MOVE:
                        int moveX = Convert.ToInt32(this.FirstCommandLine[1]);
                        int moveY = Convert.ToInt32(this.FirstCommandLine[2]);

                        response = Move(moveX, moveY);
                        break;

                    case REPORT:
                        this.LogOutPut = $"Output: {this.X},{this.Y},{this.CurrentDirection}";
                        Console.WriteLine(this.LogOutPut);
                        response = true;
                        break;
                }

                return response;
            }
            catch
            {
                return false;
            }            
        }

        private static bool IsValidPlaceCommand(string[] command)
        {
            try
            {
                string placeCommand = command[0];
                int x = Convert.ToInt32(command[1]);
                int y = Convert.ToInt32(command[2]);

                if (command.Length > 4)
                {
                    string movement = command[3];

                    if (string.IsNullOrEmpty(movement))
                        return false;

                    if (movement == NORTH || movement == EAST || movement == WEST || movement == SOUTH)
                        return true;
                }
                else
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private void RotateRobotLeft()
        {
            if (this.CurrentDirection == NORTH)
                this.CurrentDirection = WEST;
            else if (this.CurrentDirection == WEST)
                this.CurrentDirection = SOUTH;
            else if (this.CurrentDirection == SOUTH)
                this.CurrentDirection = EAST;
            else
                this.CurrentDirection = NORTH;
        }
        private void RotateRobotRight()
        {
            if (this.CurrentDirection == NORTH)
                this.CurrentDirection = EAST;
            else if (this.CurrentDirection == EAST)
                this.CurrentDirection = SOUTH;
            else if (this.CurrentDirection == SOUTH)
                this.CurrentDirection = WEST;
            else
                this.CurrentDirection = NORTH;
        }

        private bool Left(int x, int y)
        {
            int newX = x - 1;
            int newY = y - 1;

            RotateRobotLeft();

            if (IsMovementIsInsideOfBounderies(newX, newY))
                return true;

            return false;
        }

        private bool Right(int x, int y)
        {
            int newX = x + 1;
            int newY = y + 1;

            RotateRobotRight();

            if (IsMovementIsInsideOfBounderies(newX, newY))
                return true;

            return false;
        }

        private bool Move(int x, int y)
        {
            if (IsMovementIsInsideOfBounderies(x, y))
            {
                switch (CurrentDirection)
                {
                    case NORTH:
                        this.Y++;
                        break;

                    case SOUTH:
                        this.Y--;
                        break;

                    case EAST:
                        this.X++;
                        break;

                    case WEST:
                        this.X--;
                        break;
                }

                return true;
            }

            return false;
        }

        private bool Place(int x, int y, string direction)
        {
            if (IsMovementIsInsideOfBounderies(x, y))
            {
                this.X = x;
                this.Y = y;

                if(!string.IsNullOrEmpty(direction))
                    this.CurrentDirection = direction;

                return true;
            }

            return false;
        }

        private static bool IsMovementIsInsideOfBounderies(int x, int y)
        {
            if (x >= 0 && x <= XSize && y >= 0 && y <= YSize)
                return true;

            return false;
        }

        private static string[] SanitizeCommand(string command)
        {
            try
            {
                return command.Split(new string[] { " ", "," }, StringSplitOptions.None);
            }
            catch
            {
                throw new ArgumentNullException("invalid command");
            }
        }

    }
}
