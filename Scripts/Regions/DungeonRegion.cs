using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Spells;
using Server.Spells.Druid;

namespace Server.Regions
{
	public class DungeonRegion : BaseRegion
	{
		public override bool YoungProtected { get { return false; } }

		private Point3D m_EntranceLocation;
		private Map m_EntranceMap;

		public Point3D EntranceLocation{ get{ return m_EntranceLocation; } set{ m_EntranceLocation = value; } }
		public Map EntranceMap{ get{ return m_EntranceMap; } set{ m_EntranceMap = value; } }

		public DungeonRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
			XmlElement entrEl = xml["entrance"];

			Map entrMap = map;
			ReadMap( entrEl, "map", ref entrMap, false );

			if ( ReadPoint3D( entrEl, entrMap, ref m_EntranceLocation, false ) )
				m_EntranceMap = entrMap;
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void OnEnter( Mobile m )
		{
			if ( m is PlayerMobile && ((PlayerMobile)m).Young )
				m.SendGump( new YoungDungeonWarning() );
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			global = LightCycle.DungeonLevel;
		}

		public override bool CanUseStuckMenu( Mobile m )
		{
			if ( this.Map == Map.Felucca )
				return false;

			return base.CanUseStuckMenu( m );
		}

        public override bool OnBeginSpellCast(Mobile m, ISpell s)///////////////////////////////////////////////
        {
            if (( this.Map == Map.Felucca && s is EnchantedGroveSpell) && m.AccessLevel == AccessLevel.Player)
            {
                m.SendLocalizedMessage(501802); // Thy spell doth not appear to work...

                return false;
            }

            return base.OnBeginSpellCast(m, s);
        }
	}
}