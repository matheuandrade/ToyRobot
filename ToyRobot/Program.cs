using ToyRobot;

Robot Robot = new();

while(true)
{
    var input = Console.ReadLine();

    if (input != null)
    {
        Robot.HandleCommand(input);
    }
}