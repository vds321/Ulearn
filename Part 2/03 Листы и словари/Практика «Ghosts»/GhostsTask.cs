using System;
using System.Text;

namespace hashes
{
	public class GhostsTask : IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, IMagic
	{
		Vector vector = new Vector(1.0, 2.0);
		Segment segment = new Segment(new Vector(1.1, 2.3), new Vector(2.3, 3.2));
		Cat cat = new Cat("Kitty", "Cat1", DateTime.Now);
		Robot robot = new Robot("Robot1");
		Document document;
		private byte[] Bytes = { 1, 2, 3, 4 };
		public GhostsTask()
		{
			document = new Document("Doc1", Encoding.UTF8, Bytes);
		}
		public void DoMagic()
		{
			vector.Add(new Vector(1, 0));
			segment.End.Add(new Vector(1.1, 1.1));
			Bytes[0] = 0;
			Robot.BatteryCapacity += 20;
			cat.Rename("Pussy1");
		}
		Vector IFactory<Vector>.Create()
		{
			return vector;
		}

		Segment IFactory<Segment>.Create()
		{
			return segment;
		}

		Document IFactory<Document>.Create()
		{
			return document;
		}

		Cat IFactory<Cat>.Create()
		{
			return cat;
		}

		Robot IFactory<Robot>.Create()
		{
			return robot;
		}
	}
}