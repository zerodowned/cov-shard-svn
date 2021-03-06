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
	public class AG_daisyvaseAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new AG_daisyvaseAddonDeed();
			}
		}

		[ Constructable ]
		public AG_daisyvaseAddon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 6378 );
			ac.Name = "Whispering Rose";
			AddComponent( ac, 2, 1, 29 );
			ac = new AddonComponent( 3151 );
			AddComponent( ac, 1, 0, 12 );
			ac = new AddonComponent( 3171 );
			AddComponent( ac, 1, 0, 14 );
			ac = new AddonComponent( 3198 );
			AddComponent( ac, 1, 0, 24 );
			ac = new AddonComponent( 2887 );
			ac.Hue = 1150;
			AddComponent( ac, 1, 0, 13 );
			ac = new AddonComponent( 3170 );
			AddComponent( ac, 1, 0, 18 );
			ac = new AddonComponent( 6378 );
			ac.Name = "Whispering Rose";
			AddComponent( ac, 1, 0, 24 );
			ac = new AddonComponent( 3234 );
			AddComponent( ac, 0, 0, 11 );
			ac = new AddonComponent( 6809 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 3232 );
			AddComponent( ac, 2, 0, 22 );

		}

		public AG_daisyvaseAddon( Serial serial ) : base( serial )
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

	public class AG_daisyvaseAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new AG_daisyvaseAddon();
			}
		}

		[Constructable]
		public AG_daisyvaseAddonDeed()
		{
			Name = "AG_daisyvase";
		}

		public AG_daisyvaseAddonDeed( Serial serial ) : base( serial )
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