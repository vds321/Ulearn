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
			if (Game.KeyPressed == Keys.Up && y - 1 >= 0)
            {
                diggerCommand.DeltaY--;
            }
            if (Game.KeyPressed == Keys.Down && y + 1 < Game.MapHeight)
            {
                diggerCommand.DeltaY++;
            }
            if (Game.KeyPressed == Keys.Right && x + 1 < Game.MapWidth)
            {
                diggerCommand.DeltaX++;
            }
            if (Game.KeyPressed == Keys.Left && x - 1 >= 0)
            {
                diggerCommand.DeltaX--;
            }            return diggerCommand;

        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
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
            return 1;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }
}