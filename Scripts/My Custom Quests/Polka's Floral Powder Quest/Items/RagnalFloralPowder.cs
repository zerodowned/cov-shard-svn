using System;
using Server;

namespace Server.Items
{
	public class RagnalFloralPowder : Item
	{
		[Constructable]
		public RagnalFloralPowder() : base( 4157 )
		{
			Name = "Ragnal Floral Powder";
			Weight = 1.0;
			Hue = 1646;
		}
		
		public RagnalFloralPowder( Serial serial ) : base( serial )
		{
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