using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    class Player : ICreature
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
            return conflictedObject is Sack;
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
    class Terrain : ICreature
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
    class Sack : ICreature
    {
        public int countOfFall = 0;
        public bool isSackFalling = false;
        public CreatureCommand Act(int x, int y)
        {

            if (y + 1 < Game.MapHeight && (Game.Map[x, y + 1] == null || (Game.Map[x, y + 1] is Player && isSackFalling)))
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
    class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player) Game.Scores += 10;
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
}