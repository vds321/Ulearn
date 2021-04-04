using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    internal class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand diggerCommand = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0
            };
            if (Game.KeyPressed == Keys.Up && y - 1 >= 0 && !(Game.Map[x, y - 1] is Sack))
            {
                diggerCommand.DeltaY--;
            }
            if (Game.KeyPressed == Keys.Down && y + 1 < Game.MapHeight && !(Game.Map[x, y + 1] is Sack))
            {
                diggerCommand.DeltaY++;
            }
            if (Game.KeyPressed == Keys.Right && x + 1 < Game.MapWidth && !(Game.Map[x + 1, y] is Sack))
            {
                diggerCommand.DeltaX++;
            }
            if (Game.KeyPressed == Keys.Left && x - 1 >= 0 && !(Game.Map[x - 1, y] is Sack))
            {
                diggerCommand.DeltaX--;
            }
            return diggerCommand;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    internal class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    internal class Sack : ICreature
    {
        public int countOfFall = 0;
        public bool isSackFalling = false;

        public CreatureCommand Act(int x, int y)
        {
            if (y + 1 < Game.MapHeight && (Game.Map[x, y + 1] == null || ((Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster) && isSackFalling)))
            {
                countOfFall++;
                isSackFalling = true;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
            }
            if (countOfFall > 1) return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            countOfFall = 0;
            isSackFalling = false;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    internal class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
            }
            if (conflictedObject is Monster) return true;
            return true;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    internal class Monster : ICreature
    {
        public int PlayerX;
        public int PlayerY;
        public int moveX;
        public int moveY;

        public CreatureCommand Act(int x, int y)
        {
            if (IsPlayerExist())
            {
                var dx = MovementMonsters(x, y)[0];
                var dy = MovementMonsters(x, y)[1];
                if (x + dx >= 0 && x + dx < Game.MapWidth && (Game.Map[x + dx, y] == null
                    || Game.Map[x + dx, y] is Gold || Game.Map[x + dx, y] is Player) && !(Game.Map[x + dx, y] is Monster))
                {
                    return new CreatureCommand() { DeltaX = dx, DeltaY = 0 };
                }
                if (y + dy >= 0 && y + dy < Game.MapHeight && (Game.Map[x, y + dy] == null
                    || Game.Map[x, y + dy] is Gold || Game.Map[x, y + dy] is Player) && !(Game.Map[x, y + dy] is Monster))
                {
                    return new CreatureCommand() { DeltaX = 0, DeltaY = dy };
                }
                return new CreatureCommand();
            }
            else return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return -1;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }

        private bool IsPlayerExist()
        {
            for (int i = 0; i < Game.MapWidth; i++)
            {
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    if (Game.Map[i, j] is Player)
                    {
                        PlayerX = i;
                        PlayerY = j;
                        return true;
                    }
                }
            }
            return false;
        }

        private int[] MovementMonsters(int x, int y)
        {
            if (PlayerX == x)
            {
                if (PlayerY < y) moveY = -1;
                else if (PlayerY > y) moveY = 1;
            }
            else if (PlayerY == y)
            {
                if (PlayerX < x) moveX = -1;
                else if (PlayerX > x) moveX = 1;
            }
            else
            {
                if (PlayerX > x) moveX = 1;
                else if (PlayerX < x) moveX = -1;
            }
            return new int[] { moveX, moveY };
        }
    }
}