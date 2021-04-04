namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveToDirection(robot, width - 2, Direction.Right);
            MoveToDirection(robot, height - 2, Direction.Down);
        }
        public static void MoveToDirection(Robot robot, int coordinat, Direction direction)
        {
            for (int x = 1; x < coordinat; x++) robot.MoveTo(direction);
        }
    }
}