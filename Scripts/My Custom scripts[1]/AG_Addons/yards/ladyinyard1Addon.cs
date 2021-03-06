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
	public class AG_ladyinyard1Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new AG_ladyinyard1AddonDeed();
			}
		}

		[ Constructable ]
		public AG_ladyinyard1Addon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 1, 2, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, -1, -1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 1, 1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 1, 0, 1 );
			ac = new AddonComponent( 4786 );
			AddComponent( ac, 0, 0, 1 );
			ac = new AddonComponent( 4973 );
			ac.Hue = 1150;
			AddComponent( ac, 3, 0, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 2, 2, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, -2, -1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 1, -1, 1 );
			ac = new AddonComponent( 6815 );
			AddComponent( ac, 0, 2, 2 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, -1, 1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, -2, 2, 1 );
			ac = new AddonComponent( 3149 );
			AddComponent( ac, 1, 2, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, -2, 1, 1 );
			ac = new AddonComponent( 7835 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 2, 1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, -1, 0, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 2, -1, 1 );
			ac = new AddonComponent( 7836 );
			AddComponent( ac, -1, 0, 1 );
			ac = new AddonComponent( 3203 );
			AddComponent( ac, -1, 2, 2 );
			ac = new AddonComponent( 6811 );
			AddComponent( ac, 0, 2, 2 );
			ac = new AddonComponent( 6811 );
			AddComponent( ac, -2, 2, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, -1, 2, 1 );
			ac = new AddonComponent( 3223 );
			AddComponent( ac, -2, 1, 1 );
			ac = new AddonComponent( 4785 );
			AddComponent( ac, 0, 1, 1 );
			ac = new AddonComponent( 4973 );
			ac.Hue = 1150;
			AddComponent( ac, 1, 2, 1 );
			ac = new AddonComponent( 6378 );
			AddComponent( ac, 1, 2, 1 );
			ac = new AddonComponent( 6378 );
			AddComponent( ac, 3, 0, 1 );
			ac = new AddonComponent( 4783 );
			ac.Hue = 1150;
			AddComponent( ac, 2, 0, 1 );
			ac = new AddonComponent( 4969 );
			ac.Hue = 1150;
			AddComponent( ac, 0, 2, 1 );
			ac = new AddonComponent( 4962 );
			ac.Hue = 1150;
			AddComponent( ac, 2, -1, 1 );
			ac = new AddonComponent( 6378 );
			AddComponent( ac, 2, 1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 0, 0, 1 );
			ac = new AddonComponent( 3149 );
			AddComponent( ac, -1, 2, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, -2, 0, 1 );
			ac = new AddonComponent( 6811 );
			AddComponent( ac, 3, 0, 1 );
			ac = new AddonComponent( 4784 );
			ac.Hue = 1150;
			AddComponent( ac, 1, 1, 1 );
			ac = new AddonComponent( 6811 );
			AddComponent( ac, 1, -1, 3 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 3, 2, 1 );
			ac = new AddonComponent( 6583 );
			ac.Hue = 1150;
			AddComponent( ac, 3, 1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 3, 0, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 0, 1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 2, 0, 1 );
			ac = new AddonComponent( 4967 );
			ac.Hue = 1150;
			AddComponent( ac, 3, 2, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 3, -1, 1 );
			ac = new AddonComponent( 6583 );
			ac.Hue = 1150;
			AddComponent( ac, 2, 2, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 0, 2, 1 );
			ac = new AddonComponent( 3149 );
			AddComponent( ac, -1, -1, 1 );
			ac = new AddonComponent( 3149 );
			AddComponent( ac, 3, 0, 3 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 3, 1, 1 );
			ac = new AddonComponent( 6013 );
			AddComponent( ac, 0, -1, 1 );
			ac = new AddonComponent( 4782 );
			ac.Hue = 1150;
			AddComponent( ac, 2, 1, 1 );
			ac = new AddonComponent( 6378 );
			AddComponent( ac, 2, -1, 1 );
			ac = new AddonComponent( 4967 );
			ac.Hue = 1150;
			AddComponent( ac, 3, -1, 1 );
			ac = new AddonComponent( 3149 );
			AddComponent( ac, 3, 1, 1 );
			ac = new AddonComponent( 6378 );
			AddComponent( ac, 3, 2, 3 );

		}

		public AG_ladyinyard1Addon( Serial serial ) : base( serial )
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

	public class AG_ladyinyard1AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new AG_ladyinyard1Addon();
			}
		}

		[Constructable]
		public AG_ladyinyard1AddonDeed()
		{
			Name = "AG_ladyinyard1";
		}

		public AG_ladyinyard1AddonDeed( Serial serial ) : base( serial )
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