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
	public class AG_windowboxeast2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new AG_windowboxeast2AddonDeed();
			}
		}

		[ Constructable ]
		public AG_windowboxeast2Addon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 3151 );
			AddComponent( ac, 0, 0, 1 );
			ac = new AddonComponent( 3206 );
			AddComponent( ac, 0, 0, 8 );
			ac = new AddonComponent( 3223 );
			AddComponent( ac, 0, 0, 2 );
			ac = new AddonComponent( 2825 );
			ac.Name = "Window box";
			AddComponent( ac, 0, 0, 0 );

		}

		public AG_windowboxeast2Addon( Serial serial ) : base( serial )
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

	public class AG_windowboxeast2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new AG_windowboxeast2Addon();
			}
		}

		[Constructable]
		public AG_windowboxeast2AddonDeed()
		{
			Name = "AG_windowboxeast2";
		}

		public AG_windowboxeast2AddonDeed( Serial serial ) : base( serial )
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