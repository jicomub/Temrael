using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells
{
	public class HorrificBeastSpell : TransformationSpell
	{
        public static int spellID { get { return 0; } } // TOCHANGE

        private static int s_ManaCost = 50;
        private static SkillName s_SkillForCast = SkillName.ArtMagique;
        private static int s_MinSkillForCast = 50;
        private static TimeSpan s_Duree = TimeSpan.FromSeconds(1);

		private static SpellInfo m_Info = new SpellInfo(
				"B�te Horrifique", "Rel Xen Vas Bal",
				SpellCircle.Fifth,
				203,
				9031,
                s_ManaCost,
                s_Duree,
                s_SkillForCast,
                s_MinSkillForCast,
                false,
				Reagent.BatWing,
				Reagent.DaemonBlood
            );

        public override int Body { get { return 73; } }
        public override int StrOffset { get { return 10; } }
        public override int DexOffset { get { return 20; } }

		public HorrificBeastSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void PlayEffect( Mobile m )
		{
			m.PlaySound( 0x165 );
			m.FixedParticles( 0x3728, 1, 13, 9918, 92, 3, EffectLayer.Head );
		}
	}
}