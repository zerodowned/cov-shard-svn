using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Accounting;

namespace Server.Mobiles
{
[CorpseName( "Trinaree's Corpse" )]
	public class TrinsicLibrarian : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public TrinsicLibrarian()
		{
			Name = "Trinaree";
						Title = "Trinsic Librarian";
			Body = 0x191;
			CantWalk = true;
			Hue = Utility.RandomSkinHue();


			AddItem( new Server.Items.ClothNinjaJacket( 1105 ) );
			AddItem( new Server.Items.Skirt( 1105 ) );
			AddItem( new Server.Items.FeatheredHat( 1105 ) );



			ThighBoots ThighBoots = new ThighBoots();
			ThighBoots.Hue = 1372;
			AddItem( ThighBoots );




						int hairHue = 1372;
			
			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new LongHair( hairHue ) ); break;
			} 
			Blessed = true;
			
		}




		public TrinsicLibrarian( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
					list.Add( new TrinsicLibrarianEntry( from, this ) );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public class TrinsicLibrarianEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;

			public TrinsicLibrarianEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{


						  if( !( m_Mobile is PlayerMobile ) )
					return;

				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( TrinsicLibrarian ) ) )
					{
						mobile.SendGump( new TrinsicLibrarianGump( mobile ));

					}
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
					Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
				if( dropped is LittleRedRiddingHoodStory )
			{
				if( dropped.Amount!=1)
				{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "AH! You have done it by the gods! Many blessings be upon you! Here is your reward as promissed. I thank for all your hard work!", mobile.NetState );
					return false;
				}

					dropped.Delete();
					mobile.SendGump( new TrinsicLibrarianGump2(m) );
					
 			if( 1 > Utility.RandomDouble() ) // 1 = 100% = chance to drop 

		switch ( Utility.Random( 4 ))
					{

							case 0: mobile.AddToBackpack( new RedRidingHoodsShroudedHood() ); break;
							case 1: mobile.AddToBackpack( new WolfsSkinHood() ); break;
							case 2: mobile.AddToBackpack( new GrandmasEarrings() ); break;
							case 3: mobile.AddToBackpack( new LittleRedRiddingHoodNovel() ); break;



					}
				
					return true;
         		}
				else if ( dropped is LittleRedRiddingHoodStory )
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "This is not the story I seek!", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
				


				


