using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
    public interface IBloodCreature
    {
    }
	[CorpseName( "a bloodworm corpse" )]
    public class BloodWorm : BaseCreature, IBloodCreature
	{
		[Constructable]
		public BloodWorm() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Bloodworm";
			Body = 287;

			SetStr( 401, 473 );
			SetDex( 80 );
			SetInt( 18, 19 );

			SetHits( 374, 422 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 50, 52 );
			SetResistance( ResistanceType.Fire, 42, 50 );
			SetResistance( ResistanceType.Cold, 29, 31 );
			SetResistance( ResistanceType.Poison, 69, 75 );
			SetResistance( ResistanceType.Energy, 26, 35 );

			SetSkill( SkillName.MagicResist, 35 );
			SetSkill( SkillName.Tactics, 100 );
			SetSkill( SkillName.Wrestling, 100 );
            SetSkill( SkillName.SpiritSpeak, 120 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override int GetIdleSound() { return 1503; } 
		public override int GetAngerSound() { return 1500; } 
		public override int GetHurtSound() { return 1502; } 
		public override int GetDeathSound()	{ return 1501; }

		public BloodWorm( Serial serial ) : base( serial )
		{
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
	}
}