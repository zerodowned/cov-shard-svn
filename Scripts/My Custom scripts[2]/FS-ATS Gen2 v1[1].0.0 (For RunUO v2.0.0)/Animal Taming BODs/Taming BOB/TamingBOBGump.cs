using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Prompts;

namespace Server.Engines.BulkOrders
{
	public class TamingBOBGump : Gump
	{
		private PlayerMobile m_From;
		private TamingBulkOrderBook m_Book;
		private ArrayList m_List;

		private int m_Page;

		private const int LabelColor = 0x7FFF;

		public Item Reconstruct( object obj )
		{
			Item item = null;

			if ( obj is TamingBOBLargeEntry )
				item = ((TamingBOBLargeEntry)obj).Reconstruct();
			else if ( obj is TamingBOBSmallEntry )
				item = ((TamingBOBSmallEntry)obj).Reconstruct();

			return item;
		}

		public bool CheckFilter( object obj )
		{
			if ( obj is TamingBOBLargeEntry )
			{
				TamingBOBLargeEntry e = (TamingBOBLargeEntry)obj;

				return CheckFilter( e.AmountMax, true );
			}
			else if ( obj is TamingBOBSmallEntry )
			{
				TamingBOBSmallEntry e = (TamingBOBSmallEntry)obj;

				return CheckFilter( e.AmountMax, false );
			}

			return false;
		}

		public bool CheckFilter( int amountMax, bool isLarge )
		{
			TamingBOBFilter f = ( m_From.UseOwnFilter ? m_From.TamingBOBFilter : m_Book.Filter );

			if ( f.IsDefault )
				return true;

			if ( f.Quantity == 1 && amountMax != 10 )
				return false;
			else if ( f.Quantity == 2 && amountMax != 15 )
				return false;
			else if ( f.Quantity == 3 && amountMax != 20 )
				return false;

			if ( f.Type == 1 && isLarge )
				return false;
			else if ( f.Type == 2 && !isLarge )
				return false;

			return true;
		}

		public int GetIndexForPage( int page )
		{
			int index = 0;

			while ( page-- > 0 )
				index += GetCountForIndex( index );

			return index;
		}

		public int GetCountForIndex( int index )
		{
			int slots = 0;
			int count = 0;

			ArrayList list = m_List;

			for ( int i = index; i >= 0 && i < list.Count; ++i )
			{
				object obj = list[i];

				if ( CheckFilter( obj ) )
				{
					int add;

					if ( obj is TamingBOBLargeEntry )
						add = ((TamingBOBLargeEntry)obj).Entries.Length;
					else
						add = 1;

					if ( (slots + add) > 10 )
						break;

					slots += add;
				}

				++count;
			}

			return count;
		}

		public object GetMaterialName( Type Type )
		{
			return "";
		}

		public TamingBOBGump( PlayerMobile from, TamingBulkOrderBook book ) : this( from, book, 0, null )
		{
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			int index = info.ButtonID;

			switch ( index )
			{
				case 0: // EXIT
				{
					break;
				}
				case 1: // Set Filter
				{
					m_From.SendGump( new TamingBOBFilterGump( m_From, m_Book ) );

					break;
				}
				case 2: // Previous page
				{
					if ( m_Page > 0 )
						m_From.SendGump( new TamingBOBGump( m_From, m_Book, m_Page - 1, m_List ) );

					return;
				}
				case 3: // Next page
				{
					if ( GetIndexForPage( m_Page + 1 ) < m_List.Count )
						m_From.SendGump( new TamingBOBGump( m_From, m_Book, m_Page + 1, m_List ) );

					break;
				}
				case 4: // Price all
				{
					if ( m_Book.IsChildOf( m_From.Backpack ) )
					{
						m_From.Prompt = new SetPricePrompt( m_Book, null, m_Page, m_List );
						m_From.SendMessage( "Type in a price for all deeds in the book:" );
					}

					break;
				}
				default:
				{
					bool canDrop = m_Book.IsChildOf( m_From.Backpack );
					bool canPrice = canDrop || (m_Book.RootParent is PlayerVendor);

					index -= 5;

					int type = index % 2;
					index /= 2;

					if ( index < 0 || index >= m_List.Count )
						break;

					object obj = m_List[index];

					if ( !m_Book.Entries.Contains( obj ) )
					{
						m_From.SendLocalizedMessage( 1062382 ); // The deed selected is not available.
						break;
					}

					if ( type == 0 ) // Drop
					{
						if ( m_Book.IsChildOf( m_From.Backpack ) )
						{
							Item item = Reconstruct( obj );

							if ( item != null )
							{
								m_From.AddToBackpack( item );
								m_From.SendLocalizedMessage( 1045152 ); // The bulk order deed has been placed in your backpack.

								m_Book.Entries.Remove( obj );
								m_Book.InvalidateProperties();

								if ( m_Book.Entries.Count > 0 )
									m_From.SendGump( new TamingBOBGump( m_From, m_Book, 0, null ) );
								else
									m_From.SendLocalizedMessage( 1062381 ); // The book is empty.
							}
							else
							{
								m_From.SendMessage( "Internal error. The bulk order deed could not be reconstructed." );
							}
						}
					}
					else // Set Price | Buy
					{
						if ( m_Book.IsChildOf( m_From.Backpack ) )
						{
							m_From.Prompt = new SetPricePrompt( m_Book, obj, m_Page, m_List );
							m_From.SendLocalizedMessage( 1062383 ); // Type in a price for the deed:
						}
						else if ( m_Book.RootParent is PlayerVendor )
						{
							PlayerVendor pv = (PlayerVendor)m_Book.RootParent;

							VendorItem vi = pv.GetVendorItem( m_Book );

							int price = 0;

							if ( vi != null && !vi.IsForSale )
							{
								if ( obj is TamingBOBLargeEntry )
									price = ((TamingBOBLargeEntry)obj).Price;
								else if ( obj is TamingBOBSmallEntry )
									price = ((TamingBOBSmallEntry)obj).Price;
							}

							if ( price == 0 )
								m_From.SendLocalizedMessage( 1062382 ); // The deed selected is not available.
							else
								m_From.SendMessage( "offline" );
								m_From.SendGump( new TamingBODBuyGump( m_From, m_Book, obj, price ) );
						}
					}

					break;
				}
			}
		}

		private class SetPricePrompt : Prompt
		{
			private TamingBulkOrderBook m_Book;
			private object m_Object;
			private int m_Page;
			private ArrayList m_List;

			public SetPricePrompt( TamingBulkOrderBook book, object obj, int page, ArrayList list )
			{
				m_Book = book;
				m_Object = obj;
				m_Page = page;
				m_List = list;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( m_Object != null && !m_Book.Entries.Contains( m_Object ) )
				{
					from.SendLocalizedMessage( 1062382 ); // The deed selected is not available.
					return;
				}

				int price = Utility.ToInt32( text );

				if ( price < 0 || price > 250000000 )
				{
					from.SendLocalizedMessage( 1062390 ); // The price you requested is outrageous!
				}
				else if ( m_Object == null )
				{
					for ( int i = 0; i < m_List.Count; ++i )
					{
						object obj = m_List[i];

						if ( !m_Book.Entries.Contains( obj ) )
							continue;

						if ( obj is TamingBOBLargeEntry )
							((TamingBOBLargeEntry)obj).Price = price;
						else if ( obj is TamingBOBSmallEntry )
							((TamingBOBSmallEntry)obj).Price = price;
					}

					from.SendMessage( "Deed prices set." );

					if ( from is PlayerMobile )
						from.SendGump( new TamingBOBGump( (PlayerMobile)from, m_Book, m_Page, m_List ) );
				}
				else if ( m_Object is TamingBOBLargeEntry )
				{
					((TamingBOBLargeEntry)m_Object).Price = price;

					from.SendLocalizedMessage( 1062384 ); // Deed price set.

					if ( from is PlayerMobile )
						from.SendGump( new TamingBOBGump( (PlayerMobile)from, m_Book, m_Page, m_List ) );
				}
				else if ( m_Object is TamingBOBSmallEntry )
				{
					((TamingBOBSmallEntry)m_Object).Price = price;

					from.SendLocalizedMessage( 1062384 ); // Deed price set.

					if ( from is PlayerMobile )
						from.SendGump( new TamingBOBGump( (PlayerMobile)from, m_Book, m_Page, m_List ) );
				}
			}
		}

		public TamingBOBGump( PlayerMobile from, TamingBulkOrderBook book, int page, ArrayList list ) : base( 12, 24 )
		{
			from.CloseGump( typeof( TamingBOBGump ) );
			from.CloseGump( typeof( TamingBOBFilterGump ) );

			m_From = from;
			m_Book = book;
			m_Page = page;

			if ( list == null )
			{
				list = new ArrayList( book.Entries.Count );

				for ( int i = 0; i < book.Entries.Count; ++i )
				{
					object obj = book.Entries[i];

					if ( CheckFilter( obj ) )
						list.Add( obj );
				}
			}

			m_List = list;

			int index = GetIndexForPage( page );
			int count = GetCountForIndex( index );

			int tableIndex = 0;

			PlayerVendor pv = book.RootParent as PlayerVendor;

			bool canDrop = book.IsChildOf( from.Backpack );
			bool canBuy = ( pv != null );
			bool canPrice = ( canDrop || canBuy );

			if ( canBuy )
			{
				VendorItem vi = pv.GetVendorItem( book );

				canBuy = ( vi != null && !vi.IsForSale );
			}

			int width = 600;

			if ( !canPrice )
				width = 516;

			X = (624 - width) / 2;

			AddPage( 0 );

			AddBackground( 10, 10, width, 439, 5054 );
			AddImageTiled( 18, 20, width - 17, 420, 2624 );

			if ( canPrice )
			{
				AddImageTiled( 573, 64, 24, 352, 200 );
				AddImageTiled( 493, 64, 78, 352, 1416 );
			}

			if ( canDrop )
				AddImageTiled( 24, 64, 32, 352, 1416 );

			AddImageTiled( 58, 64, 36, 352, 200 );
			AddImageTiled( 96, 64, 133, 352, 1416 );
			//AddImage( 231, 100, 5549 );
			AddImageTiled( 415, 64, 76, 352, 200 );

			for ( int i = index; i < (index + count) && i >= 0 && i < list.Count; ++i )
			{
				object obj = list[i];

				if ( !CheckFilter( obj ) )
					continue;

				AddImageTiled( 24, 94 + (tableIndex * 32), canPrice ? 573 : 489, 2, 2624 );

				if ( obj is TamingBOBLargeEntry )
					tableIndex += ((TamingBOBLargeEntry)obj).Entries.Length;
				else if ( obj is TamingBOBSmallEntry )
					++tableIndex;
			}

			AddAlphaRegion( 18, 20, width - 17, 420 );
			AddImage( 5, 5, 10460 );
			AddImage( width - 15, 5, 10460 );
			AddImage( 5, 424, 10460 );
			AddImage( width - 15, 424, 10460 );

			AddHtmlLocalized( canPrice ? 266 : 224, 32, 200, 32, 1062220, LabelColor, false, false ); // Bulk Order Book
			AddHtmlLocalized( 63, 64, 200, 32, 1062213, LabelColor, false, false ); // Type
			AddLabel( 147, 64, 1149, @"Animal" );
			AddHtmlLocalized( 429, 64, 200, 32, 1062217, LabelColor, false, false ); // Amount

			AddButton( 35, 32, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 70, 32, 200, 32, 1062476, LabelColor, false, false ); // Set Filter

			TamingBOBFilter f = ( from.UseOwnFilter ? from.TamingBOBFilter : book.Filter );

			if ( f.IsDefault )
				AddHtmlLocalized( canPrice ? 470 : 386, 32, 120, 32, 1062475, 16927, false, false ); // Using No Filter
			else if ( from.UseOwnFilter )
				AddHtmlLocalized( canPrice ? 470 : 386, 32, 120, 32, 1062451, 16927, false, false ); // Using Your Filter
			else
				AddHtmlLocalized( canPrice ? 470 : 386, 32, 120, 32, 1062230, 16927, false, false ); // Using Book Filter

			AddButton( 375, 416, 4017, 4018, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 410, 416, 120, 20, 1011441, LabelColor, false, false ); // EXIT

			if ( canDrop )
				AddHtmlLocalized( 26, 64, 50, 32, 1062212, LabelColor, false, false ); // Drop

			if ( canPrice )
			{
				AddHtmlLocalized( 516, 64, 200, 32, 1062218, LabelColor, false, false ); // Price

				if ( canBuy )
				{
					AddHtmlLocalized( 576, 64, 200, 32, 1062219, LabelColor, false, false ); // Buy
				}
				else
				{
					AddHtmlLocalized( 576, 64, 200, 32, 1062227, LabelColor, false, false ); // Set

					AddButton( 450, 416, 4005, 4007, 4, GumpButtonType.Reply, 0 );
					AddHtml( 485, 416, 120, 20, "<BASEFONT COLOR=#FFFFFF>Price all</FONT>", false, false );
				}
			}

			tableIndex = 0;

			if ( page > 0 )
			{
				AddButton( 75, 416, 4014, 4016, 2, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 110, 416, 150, 20, 1011067, LabelColor, false, false ); // Previous page
			}

			if ( GetIndexForPage( page + 1 ) < list.Count )
			{
				AddButton( 225, 416, 4005, 4007, 3, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 260, 416, 150, 20, 1011066, LabelColor, false, false ); // Next page
			}

			for ( int i = index; i < (index + count) && i >= 0 && i < list.Count; ++i )
			{
				object obj = list[i];

				if ( !CheckFilter( obj ) )
					continue;

				if ( obj is TamingBOBLargeEntry )
				{
					TamingBOBLargeEntry e = (TamingBOBLargeEntry)obj;

					int y = 96 + (tableIndex * 32);

					if ( canDrop )
						AddButton( 35, y + 2, 5602, 5606, 5 + (i * 2), GumpButtonType.Reply, 0 );

					if ( canDrop || (canBuy && e.Price > 0) )
					{
						AddButton( 579, y + 2, 2117, 2118, 6 + (i * 2), GumpButtonType.Reply, 0 );
						AddLabel( 495, y, 1152, e.Price.ToString() );
					}

					AddHtmlLocalized( 61, y, 50, 32, 1062225, LabelColor, false, false ); // Large

					for ( int j = 0; j < e.Entries.Length; ++j )
					{
						TamingBOBLargeSubEntry sub = e.Entries[j];

						string s = sub.AnimalName;

						int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

						if( capsbreak > -1 )
						{
							string secondhalf = s.Substring( capsbreak );
 							string firsthalf = s.Substring(0, capsbreak );

							string newname = firsthalf + " " + secondhalf;

							AddLabel( 103, y, 1149, newname.ToString() );
						}
						else
						{
							AddLabel( 103, y, 1149, sub.AnimalName.ToString() );
						}

						object name = GetMaterialName( sub.Type );

						if ( name is int )
							AddHtmlLocalized( 316, y, 100, 20, (int)name, LabelColor, false, false );
						else if ( name is string )
							AddLabel( 316, y, 1152, (string)name );

						AddLabel( 421, y, 1152, String.Format( "{0} / {1}", sub.AmountCur, e.AmountMax ) );

						++tableIndex;
						y += 32;
					}
				}
				else if ( obj is TamingBOBSmallEntry )
				{
					TamingBOBSmallEntry e = (TamingBOBSmallEntry)obj;

					int y = 96 + (tableIndex++ * 32);

					if ( canDrop )
						AddButton( 35, y + 2, 5602, 5606, 5 + (i * 2), GumpButtonType.Reply, 0 );

					if ( canDrop || (canBuy && e.Price > 0) )
					{
						AddButton( 579, y + 2, 2117, 2118, 6 + (i * 2), GumpButtonType.Reply, 0 );
						AddLabel( 495, y, 1152, e.Price.ToString() );
					}

					AddHtmlLocalized( 61, y, 50, 32, 1062224, LabelColor, false, false ); // Small

					string s = e.AnimalName;

					int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

					if( capsbreak > -1 )
					{
						string secondhalf = s.Substring( capsbreak );
 						string firsthalf = s.Substring(0, capsbreak );

						string newname = firsthalf + " " + secondhalf;

						AddLabel( 103, y, 1149, newname.ToString() );
					}
					else
					{
						AddLabel( 103, y, 1149, e.AnimalName.ToString() );
					}

					object name = GetMaterialName( e.Type );

					if ( name is int )
						AddHtmlLocalized( 316, y, 100, 20, (int)name, LabelColor, false, false );
					else if ( name is string )
						AddLabel( 316, y, 1152, (string)name );

					AddLabel( 421, y, 1152, String.Format( "{0} / {1}", e.AmountCur, e.AmountMax ) );
				}
			}
		}
	}
}