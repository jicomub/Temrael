using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells
{
	public class StupiditeSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Stupidite", "Rel Wis",
				SpellCircle.Second,
				212,
				9031,
				Reagent.Ginseng,
				Reagent.Nightshade
            );

        public override int RequiredAptitudeValue { get { return 3; } }
        public override Aptitude[] RequiredAptitude { get { return new Aptitude[] { Aptitude.Thaumaturgie }; } }

        public StupiditeSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

                SpellHelper.AddStatCurse(Caster, m, StatType.Int, GetDurationForSpell(1));

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;

				m.FixedParticles( 0x3779, 10, 15, 5004, EffectLayer.Head );
				m.PlaySound( 0x1E4 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
            private StupiditeSpell m_Owner;

            public InternalTarget(StupiditeSpell owner)
                : base(12, false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}