using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Engines.Quests
{	
	public class BaseObjective
	{		
		private BaseQuest m_Quest;	
		private int m_MaxProgress;
		private int m_CurProgress;
		private int m_Seconds;
		private bool m_Timed;
		
		public BaseQuest Quest
		{
			get{ return m_Quest; }
			set{ m_Quest = value; }
		}	
		
		public int MaxProgress
		{
			get{ return m_MaxProgress; }
			set{ m_MaxProgress = value; }
		}
		
		public int CurProgress
		{
			get{ return m_CurProgress; }
			set
			{ 
				m_CurProgress = value; 
				
				if ( Completed )
					OnCompleted();
					
				if ( m_CurProgress == -1 )
					OnFailed();
					
				if ( m_CurProgress < -1 )
					m_CurProgress = -1;
			}
		}
		
		public int Seconds
		{
			get{ return m_Seconds; }
			set
			{ 
				m_Seconds = value;
				
				if ( m_Seconds < 0 )
					m_Seconds = 0;
			}
		}
		
		public bool Timed
		{
			get{ return m_Timed; }
			set{ m_Timed = value; }
		}
		
		public bool Completed
		{
			get{ return CurProgress >= MaxProgress; }
		}
		
		public bool Failed
		{	
			get { return CurProgress == -1; }
		}
		
		public BaseObjective() : this( 1, 0 )
		{
		}
		
		public BaseObjective( int maxProgress ) : this( maxProgress, 0 )
		{
		}
		
		public BaseObjective( int maxProgress, int seconds )
		{				
			m_MaxProgress = maxProgress;
			m_Seconds = seconds;
			
			if ( seconds > 0 )
				Timed = true;
			else
				Timed = false;	
		}
		
		public virtual void Complete()
		{
			CurProgress = MaxProgress;
		}
		
		public virtual void Fail()
		{
			CurProgress = -1;
		}
		
		public virtual void OnAccept()
		{
		}
		
		public virtual void OnCompleted()
		{
		}
		
		public virtual void OnFailed()
		{
		}
		
		public virtual Type Type()
		{
			return null;
		}
		
		public virtual bool Update( object obj )
		{
			return false;
		}
		
		public virtual void UpdateTime()
		{
			if ( !Timed || Failed )
				return;
		
			if ( Seconds > 0 )
			{
				Seconds -= 1;
			}
			else if ( !Completed )
			{
				m_Quest.Owner.SendLocalizedMessage( 1072258 ); // You failed to complete an objective in time!
				
				Fail();
			}
		}
		
		public virtual void Serialize( GenericWriter writer )
		{
			writer.WriteEncodedInt( (int) 0 ); // version
			
			writer.Write( (int) m_CurProgress );
			writer.Write( (int) m_Seconds );
		}
		
		public virtual void Deserialize( GenericReader reader )
		{
			int version = reader.ReadEncodedInt();
			
			m_CurProgress = reader.ReadInt();
			m_Seconds = reader.ReadInt();
		}
	}
	
	public class SlayObjective : BaseObjective
	{
		private Type m_Creature;
		private string m_Name;	
		private Region m_Region;
	
		public Type Creature
		{ 
			get{ return m_Creature; } 
			set{ m_Creature = value; }
		}
		
		public string Name
		{ 
			get{ return m_Name; } 
			set{ m_Name = value; }	
		}		
		
		public Region Region
		{ 
			get{ return m_Region; } 
			set{ m_Region = value; }	
		}
		
		public SlayObjective( Type creature, string name, int amount ) : this( creature, name, amount, null )
		{
		}
		
		public SlayObjective( Type creature, string name, int amount, string region ) : this( creature, name, amount, region, 0 )
		{
		}
		
		public SlayObjective( Type creature, string name, int amount, int seconds ) : this( creature, name, amount, null, seconds )
		{
		}
		
		public SlayObjective( Type creature, string name, int amount, string region, int seconds ) : base( amount, seconds )
		{			
			m_Creature = creature;
			m_Name = name;
			
			if ( region != null )
			{
				m_Region = QuestHelper.FindRegion( region );

				if ( m_Region == null )
					Console.WriteLine( String.Format( "Invalid region name ('{0}') in '{1}' objective!", region, GetType() ) );
			}
		}
		
		public virtual void OnKill( Mobile killed )
		{
			if ( Completed )
				Quest.Owner.SendLocalizedMessage( 1075050 ); // You have killed all the required quest creatures of this type.
			else
				Quest.Owner.SendLocalizedMessage( 1075051, ( MaxProgress - CurProgress ).ToString() ); // You have killed a quest creature. ~1_val~ more left.
		}
		
		public virtual bool IsObjective( Mobile mob )
		{		
			if ( m_Creature == null )
				return false;
		
			if ( m_Creature.IsAssignableFrom( mob.GetType() ) )
			{
				if ( m_Region != null && !m_Region.Contains( mob.Location ) )
					return false;
					
				return true;
			}
			
			return false;
		}
		
		public override bool Update( object obj )
		{
			if ( obj is Mobile )
			{
				Mobile mob = (Mobile) obj;
			
				if ( IsObjective( mob ) )
				{						
					if ( !Completed )
						CurProgress += 1;
						
					OnKill( mob );					
					return true;
				}
			}
			
			return false;
		}
		
		public override Type Type()
		{
			return m_Creature;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}		
	}
	
	public class ObtainObjective : BaseObjective
	{
		private Type m_Obtain;
		private string m_Name;			
		private int m_Image;
		
		public Type Obtain
		{ 
			get{ return m_Obtain; } 
			set{ m_Obtain = value; }
		}
		
		public string Name
		{ 
			get{ return m_Name; } 
			set{ m_Name = value; }	
		}
		
		public int Image
		{ 
			get{ return m_Image; } 
			set{ m_Image = value; }	
		}
		
		public ObtainObjective( Type obtain, string name, int amount ) : this( obtain, name, amount, 0, 0 )
		{
		}
		
		public ObtainObjective( Type obtain, string name, int amount, int image ) : this( obtain, name, amount, image, 0 )
		{
		}
		
		public ObtainObjective( Type obtain, string name, int amount, int image, int seconds ) : base( amount, seconds )
		{			
			m_Obtain = obtain;
			m_Name = name;
			m_Image = image;
		}
		
		public override bool Update( object obj )
		{		
			if ( obj is Item )
			{
				Item obtained = (Item) obj;
				
				if ( IsObjective( obtained ) )
				{					
					if ( !obtained.QuestItem )
					{				
						CurProgress += obtained.Amount;
							
						obtained.QuestItem = true;
						Quest.Owner.SendLocalizedMessage( 1072353 ); // You set the item to Quest Item status
					}	
					else
					{
						CurProgress -= obtained.Amount;
							
						obtained.QuestItem = false;
						Quest.Owner.SendLocalizedMessage( 1072354 ); // You remove Quest Item status from the item
					}		
						
					return true;
				}
			}
			
			return false;
		}	
		
		public virtual bool IsObjective( Item item )
		{
			if ( m_Obtain == null )
				return false;
		
			if ( m_Obtain.IsAssignableFrom( item.GetType() ) )
				return true;
			
			return false;
		}
		
		public override Type Type()
		{
			return m_Obtain;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}		
	}
	
	public class DeliverObjective : BaseObjective
	{
		private Type m_Delivery;
		private String m_DeliveryName;
		
		private Type m_Destination;
		private String m_DestName;
	
		public Type Delivery
		{		
			get{ return m_Delivery; }
			set{ m_Delivery = value; }
		}	
		
		public String DeliveryName
		{		
			get{ return m_DeliveryName; }
			set{ m_DeliveryName = value; }
		}
		
		public Type Destination
		{		
			get{ return m_Destination; }
			set{ m_Destination = value; }
		}
		
		public String DestName
		{		
			get{ return m_DestName; }
			set{ m_DestName = value; }
		}
		
		public DeliverObjective( Type delivery, String deliveryName, int amount, Type destination, String destName ) : this( delivery, deliveryName, amount, destination, destName, 0 )
		{
		}
		
		public DeliverObjective( Type delivery, String deliveryName, int amount, Type destination, String destName, int seconds ) : base( amount, seconds )
		{					
			m_Delivery = delivery;
			m_DeliveryName = deliveryName;
			
			m_Destination = destination;
			m_DestName = destName;
		}
		
		public override void OnAccept()
		{
			if ( Quest.StartingItem != null )
			{
				Quest.StartingItem.QuestItem = true;
				return;
			}	
			
			int amount = MaxProgress;		
			
			while ( amount > 0 && !Failed )
			{			
				Item item = QuestHelper.Construct( m_Delivery ) as Item;
				
				if ( item == null )
				{
					Fail();
					break;
				}					
					
				if ( item.Stackable )
				{
					item.Amount = amount;
					amount = 1;
				}	
						
				if ( !Quest.Owner.PlaceInBackpack( item ) )
				{
					Quest.Owner.SendLocalizedMessage( 503200 ); // You do not have room in your backpack for this.
					Quest.Owner.SendLocalizedMessage( 1075574 ); // Could not create all the necessary items. Your quest has not advanced.
					
					Fail();
					
					break;
				}	
				else
					item.QuestItem = true;
						
				amount -= 1;
			}
			
			if ( Failed )
			{
				QuestHelper.DeleteItems( Quest.Owner, m_Delivery, MaxProgress - amount, false );
			
				Quest.RemoveQuest();	
			}
		}
		
		public override bool Update( object obj )
		{		
			if ( m_Delivery == null || m_Destination == null )
				return false;
				
			if ( Failed )
			{
				Quest.Owner.SendLocalizedMessage( 1074813 );  // You have failed to complete your delivery.
				return false;
			}
			
			if ( obj is BaseVendor )
			{
				if ( Quest.StartingItem != null )
				{
					Complete();					
					return true;
				}
				else if ( m_Destination.IsAssignableFrom( obj.GetType() ) )
				{					
					if ( MaxProgress < QuestHelper.CountQuestItems( Quest.Owner, Delivery ) )
					{
						Quest.Owner.SendLocalizedMessage( 1074813 );  // You have failed to complete your delivery.						
						Fail();
					}					
					else
						Complete();
					
					return true;
				}
			}
			
			return false;
		}
		
		public override Type Type()
		{
			return m_Delivery;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}		
	}
	
	public class EscortObjective : BaseObjective
	{
		private Region m_Region;
		private int m_Fame;
		private int m_Compassion;
	
		public Region Region
		{		
			get{ return m_Region; }
			set{ m_Region = value; }
		}
		
		public int Fame
		{
			get{ return m_Fame; }
			set{ m_Fame = value; }
		}

		public int Compassion
		{
			get { return m_Compassion; }
			set { m_Compassion = value; }
		}
		
		public EscortObjective( string region ) : this( region, 10, 200, 0 )
		{
		}
		
		public EscortObjective( string region, int fame ) : this( region, fame, 200 )
		{
		}

		public EscortObjective( string region, int fame, int compassion ) : this( region, fame, compassion, 0 )
		{
		}
		
		public EscortObjective( string region, int fame, int compassion, int seconds ) : base( 1, seconds )
		{
			m_Region = QuestHelper.FindRegion( region );
			m_Fame = fame;
			m_Compassion = compassion;

			if ( m_Region == null )
				Console.WriteLine( String.Format( "Invalid region name ('{0}') in '{1}' objective!", region, GetType() ) );
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}	
	}
	
	public class ApprenticeObjective : BaseObjective
	{
		private SkillName m_Skill;
		private Region m_Region;
		private object m_Enter;
		private object m_Leave;
		
		public SkillName Skill
		{
			get{ return m_Skill; }
			set{ m_Skill = value; }
		}
		
		public Region Region
		{
			get{ return m_Region; }
			set{ m_Region = value; }
		}
		
		public object Enter
		{
			get{ return m_Enter; }
			set{ m_Enter = value; }			
		}
		
		public object Leave
		{
			get{ return m_Leave; }
			set{ m_Leave = value; }			
		}
		
		public ApprenticeObjective( SkillName skill, int cap ) : this( skill, cap, null, null, null )
		{
		}
		
		public ApprenticeObjective( SkillName skill, int cap, string region, object enterRegion, object leaveRegion ) : base( cap )
		{
			m_Skill = skill;
		
			if ( region != null )
			{
				m_Region = QuestHelper.FindRegion( region );					
				m_Enter = enterRegion;	
				m_Leave = leaveRegion;
				
				if ( m_Region == null )
					Console.WriteLine( String.Format( "Invalid region name ('{0}') in '{1}' objective!", region, GetType() ) );
			}
		}
		
		public override bool Update( object obj )
		{
			if ( Completed )
				return false;
		
			if ( obj is Skill )
			{
				Skill skill = (Skill) obj;				
				
				if ( skill.SkillName != m_Skill )
					return false;				
				
				if ( Quest.Owner.Skills[ m_Skill ].Base >= MaxProgress )
				{
					Complete();
					return true;
				}
			}
			
			return false;
		}		
		
		public override void OnAccept()
		{
			Region region = Quest.Owner.Region;
			
			while ( region != null )
			{					
				if ( region is ApprenticeRegion )
					region.OnEnter( Quest.Owner );									
				
				region = region.Parent;
			}
		}
		
		public override void OnCompleted()
		{
			QuestHelper.RemoveAcceleratedSkillgain( Quest.Owner );
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}	
	}
}