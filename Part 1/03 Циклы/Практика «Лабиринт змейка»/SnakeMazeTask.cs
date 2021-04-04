namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (true)
            {
                MoveToDirection(robot, width - 4, Direction.Right);
                MoveToDirection(robot, 1, Direction.Down);
                MoveToDirection(robot, width - 4, Direction.Left);
                if (robot.Finished) break;
                MoveToDirection(robot, 1, Direction.Down);
            }
        }

        private static void MoveToDirection(Robot robot, int x, Direction direction)
        {
            for (int i = 0; i <= x; i++)
            {
                robot.MoveTo(direction);
            }
        }
    }
}