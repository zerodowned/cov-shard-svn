// Scripted by Karmageddon
using System;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public class SpiderQueenStone : Item
	{
		[Constructable]
		public SpiderQueenStone() : base( 0x117C )
		{
			Movable=false;
			Name="Stone of the Spider Queen";
			Hue = 1150;
		}

        public SpiderQueenStone(Serial serial)
            : base(serial)
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;
	
			if ( pack != null && pack.ConsumeTotal( typeof( MissMuffetScroll ), 1 ) )
			{
				Map map = from.Map;

                BaseCreature arachnid = new SpiderQueen();				

				Point3D loc = from.Location;
				bool validLocation = false;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = from.X + Utility.Random( 3 ) - 1;
					int y = from.Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

                arachnid.MoveToWorld(loc, map);

                arachnid.Combatant = from;
			}
			else
			{
				from.SendMessage(0x22,"You need Miss Muffets Scroll to use that.");
			
			}

		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}