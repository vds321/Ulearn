using System;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            var stepsCount = Math.Max((width - 2) / (height - 2), (height - 2) / (width - 2));
            while (true)
            {
                if (width > height)
                {
                    MoveToDirection(robot, stepsCount, Direction.Right);
                    if (robot.Finished) break;
                    robot.MoveTo(Direction.Down);
                } 
                if (width < height)
                {
                    MoveToDirection(robot, stepsCount, Direction.Down);
                    if (robot.Finished) break;
                    robot.MoveTo(Direction.Right);
                } 
            }
        }
		
        private static void MoveToDirection(Robot robot, int stepscount, Direction direction)
        {
            for (int i = 0; i < stepscount; i++)
            {
                robot.MoveTo(direction);
            }
        }
    }
}