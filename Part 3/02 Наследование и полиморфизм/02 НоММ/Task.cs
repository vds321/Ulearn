namespace Inheritance.MapObjects
{
    public interface IOwnered
    {
        int Owner { get; set; }
    }

    public interface IArmed
    {
        Army Army { get; set; }
    }

    public interface ITakeTreasure
    {
        Treasure Treasure { get; set; }
    }

    public class Dwelling : IOwnered
    {
        public int Owner { get; set; }
    }

    public class Mine : IOwnered, IArmed, ITakeTreasure
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Creeps : IArmed, ITakeTreasure
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Wolves : IArmed
    {
        public Army Army { get; set; }
    }

    public class ResourcePile : ITakeTreasure
    {
        public Treasure Treasure { get; set; }
    }

    public static class Interaction
    {
        public static void Make(Player player, object mapObject)
        {
            if (mapObject is IArmed armed && !player.CanBeat(armed.Army)) player.Die();
            else
            {
                if (mapObject is ITakeTreasure treasure) player.Consume(treasure.Treasure);
                if (mapObject is IOwnered ownered) ownered.Owner = player.Id;
            }
        }
    }
}
