/////////////////////////////////////////////////
//                                             //
// Automatically generated by the              //
// AddonGenerator script by Arya               //
//                                             //
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AG_5x8plotAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new AG_5x8plotAddonDeed();
			}
		}

		[ Constructable ]
		public AG_5x8plotAddon()
		{
			AddComponent( new AddonComponent( 13001 ), -1, 1, 0 );
			AddComponent( new AddonComponent( 13001 ), 0, 3, 0 );
			AddComponent( new AddonComponent( 13001 ), 0, 4, 0 );
			AddComponent( new AddonComponent( 13001 ), 2, 3, 0 );
			AddComponent( new AddonComponent( 13001 ), 2, 4, 0 );
			AddComponent( new AddonComponent( 13001 ), 2, 1, 0 );
			AddComponent( new AddonComponent( 13001 ), 2, 2, 0 );
			AddComponent( new AddonComponent( 13001 ), 2, -1, 0 );
			AddComponent( new AddonComponent( 13001 ), 2, 0, 0 );
			AddComponent( new AddonComponent( 13001 ), 1, -2, 0 );
			AddComponent( new AddonComponent( 13001 ), 2, -2, 0 );
			AddComponent( new AddonComponent( 13001 ), -1, -2, 0 );
			AddComponent( new AddonComponent( 13001 ), -1, -3, 0 );
			AddComponent( new AddonComponent( 13001 ), -2, -3, 0 );
			AddComponent( new AddonComponent( 13001 ), -2, -2, 0 );
			AddComponent( new AddonComponent( 13001 ), -2, 3, 0 );
			AddComponent( new AddonComponent( 13001 ), 0, 2, 0 );
			AddComponent( new AddonComponent( 13001 ), -2, 1, 0 );
			AddComponent( new AddonComponent( 13001 ), -2, 2, 0 );
			AddComponent( new AddonComponent( 13001 ), 1, 4, 0 );
			AddComponent( new AddonComponent( 13001 ), 1, 3, 0 );
			AddComponent( new AddonComponent( 13001 ), 1, 2, 0 );
			AddComponent( new AddonComponent( 13001 ), 1, 1, 0 );
			AddComponent( new AddonComponent( 13001 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 13001 ), 1, -1, 0 );
			AddComponent( new AddonComponent( 13001 ), 2, -3, 0 );
			AddComponent( new AddonComponent( 13001 ), 1, -3, 0 );
			AddComponent( new AddonComponent( 13001 ), -2, -1, 0 );
			AddComponent( new AddonComponent( 13001 ), -2, 0, 0 );
			AddComponent( new AddonComponent( 13001 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 13001 ), -1, -1, 0 );
			AddComponent( new AddonComponent( 13001 ), -1, 2, 0 );
			AddComponent( new AddonComponent( 13001 ), -2, 4, 0 );
			AddComponent( new AddonComponent( 13001 ), -1, 4, 0 );
			AddComponent( new AddonComponent( 13001 ), -1, 3, 0 );
			AddComponent( new AddonComponent( 13001 ), 0, -3, 0 );
			AddComponent( new AddonComponent( 13001 ), 0, -2, 0 );
			AddComponent( new AddonComponent( 13001 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 13001 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 13001 ), 0, 1, 0 );
			AddonComponent ac;
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -2, -3, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -1, -3, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -2, 3, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 13001 );
			AddComponent( ac, 1, -1, 0 );

		}

		public AG_5x8plotAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class AG_5x8plotAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new AG_5x8plotAddon();
			}
		}

		[Constructable]
		public AG_5x8plotAddonDeed()
		{
			Name = "AG_5x8plot";
		}

		public AG_5x8plotAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}