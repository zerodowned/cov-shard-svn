using System;
using System.Collections;

namespace Server.Spells.Spellweaving
{
	public class ArcaneEmpowermentSpell : ArcanistSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
                "Arcane Empowerment", "Aslavdra",
				-1
			);
			
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }

        public override double RequiredSkill { get { return 24.0; } }
        public override int RequiredMana { get { return 50; } }

        public ArcaneEmpowermentSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( m_Table.ContainsKey( Caster ) )
			{				
				Caster.SendLocalizedMessage( 501775 ); // This spell is already in effect.
			}
			else if ( CheckSequence() )
			{
				Caster.PlaySound( 0x5C1 );
				
				int level = GetFocusLevel( Caster );
				double skill = Caster.Skills[ SkillName.Spellweaving ].Value;
				
				TimeSpan duration = TimeSpan.FromSeconds( 15 + (int) ( skill / 24 ) + level * 2 );
				int bonus = (int) Math.Floor( skill / 12 ) + level * 5;

				m_Table[ Caster ] = new EmpowermentInfo( Caster, duration, bonus, level );

				BuffInfo.AddBuff( Caster, new BuffInfo( BuffIcon.ArcaneEmpowerment, 1031616, 1075808, duration, Caster, new TextDefinition( String.Format( "{0}\t10", bonus.ToString() ) ) ) );
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();
		
		public static int GetSpellBonus( Mobile m, bool playerVsPlayer )
		{
			EmpowermentInfo info = m_Table[ m ] as EmpowermentInfo;
			
			if ( info != null )
				return info.Bonus + ( playerVsPlayer ? info.Focus : 0 );
			
			return 0;
		}
		
		public static void AddHealBonus( Mobile m, ref int toHeal )
		{
			EmpowermentInfo info = m_Table[ m ] as EmpowermentInfo;
			
			if ( info != null )
				toHeal = (int) Math.Floor( ( 1 + ( 10 + info.Bonus ) / 100.0 ) * toHeal );
		}
		
		public static void RemoveBonus( Mobile m )
		{
			EmpowermentInfo info = m_Table[ m ] as EmpowermentInfo;
			
			if ( info != null && info.Timer != null )
				info.Timer.Stop();			
				
			m_Table.Remove( m );
		}

		private class EmpowermentInfo
		{
			public int Bonus;			
			public int Focus;
			public ExpireTimer Timer;

			public EmpowermentInfo( Mobile caster, TimeSpan duration, int bonus, int focus )
			{
				Bonus = bonus;
				Focus = focus;

				Timer = new ExpireTimer( caster, duration );
				Timer.Start();
			}
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Mobile;

			public ExpireTimer( Mobile m, TimeSpan delay ) : base( delay )
			{
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				m_Mobile.PlaySound( 0x5C2 );
				m_Table.Remove( m_Mobile );
			}
		}
	}
}