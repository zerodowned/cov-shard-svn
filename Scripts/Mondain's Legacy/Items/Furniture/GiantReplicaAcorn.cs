using System;

namespace Server.Items
{
	[Furniture]
	public class GiantReplicaAcorn : CraftableFurniture
	{
		public override int LabelNumber{ get{ return 1072889; } } // giant replica acorn
		
		[Constructable]
		public GiantReplicaAcorn() : base( 0x2D4A )
		{
			Weight = 1.0;
		}

		public GiantReplicaAcorn( Serial serial ) : base( serial )
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
