
//Scripted by Mimi and Kila Ventru
using System;
using Server;
using Server.Network;
using System.Collections;

namespace Server.Items
{
	public class PracticalPigsScroll : Item
	{
        [Constructable]
		public PracticalPigsScroll()
        {
			ItemID = 5357;
            Weight = 1.0;
			Name = "Three Little Pigs, Practical Pig's Scroll";
			Hue = 1103;
				
        }

		public override void OnDoubleClick(Mobile from)
        {
			Item n = from.Backpack.FindItemByType(typeof(FiferandFiddlerpigsScroll));
            if (n != null)
            {
				from.AddToBackpack(new ThreePigsCombinedScroll());
                n.Delete();
                Delete();
            }
            else
            {
                from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "You need Fifer and Fiddler pigs Combined Scroll to combine with Practical pigs", from.NetState);
            }
		}

        public PracticalPigsScroll(Serial serial)
            : base(serial)
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