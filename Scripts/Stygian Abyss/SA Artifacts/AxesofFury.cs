using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	// Based off a DoubleAxe
	[FlipableAttribute( 0x8FD, 0x4068 )]
	public class AxesofFury : DualShortAxes
	{
        public override int LabelNumber { get { return 1113517; } }///Axes of Fury

        //public override int BasePhysicalResistance { get { return 100; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 14; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 33; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 35; } }
		public override int OldSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

		[Constructable]
		public AxesofFury() : base( 0x8FD )
		{
			Weight = 4.0;
            Hue = 33;
           
            WeaponAttributes.HitLowerDefend = 40;
            WeaponAttributes.HitFireball = 45;

            Attributes.BonusDex = 5;
            Attributes.DefendChance = 15;
            Attributes.AttackChance = 20;
            Attributes.WeaponSpeed = 30;
            Attributes.WeaponDamage = 45;
		}

		public AxesofFury( Serial serial ) : base( serial )
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