using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{

	public class PigStatue2 : Item
	{
		[Constructable]
		public PigStatue2() : this( null )
		{
		}

		[Constructable]
		public PigStatue2 ( string name ) : base ( 8449 )
		{
			Name = "Three Little Pigs Statue";
			Hue = 1366;
		}

        public PigStatue2(Serial serial)
            : base(serial)
		{
		}

      		
		public override void Serialize ( GenericWriter writer)
		{
			base.Serialize ( writer );

			writer.Write ( (int) 0);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize ( reader );

			int version = reader.ReadInt();
		}
	}
}
