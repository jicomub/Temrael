﻿using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.First
{
    public class GuerisonCelesteMiracle : ReligiousSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Guerison Celeste", "",
                SpellCircle.Third,
                17,
                9061
            );

        public override int RequiredAptitudeValue { get { return 4; } }
        public override Aptitude[] RequiredAptitude { get { return new Aptitude[] { Aptitude.Benedictions }; } }

        public GuerisonCelesteMiracle(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (m.IsDeadBondedPet)
            {
                Caster.SendLocalizedMessage(1060177); // You cannot heal a creature that is already dead!
            }
            else if (m is BaseCreature && ((BaseCreature)m).IsAnimatedDead)
            {
                Caster.SendLocalizedMessage(1061654); // You cannot heal that which is not alive.
            }
            else if (m.Poisoned)
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, (Caster == m) ? 1005000 : 1010398);
            }
            else if (CheckBSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                double toHeal;

                toHeal = Caster.Skills[SkillName.Miracles].Value * 0.35;
                toHeal += Utility.Random(1, 5);

                toHeal = SpellHelper.AdjustValue(Caster, toHeal, Aptitude.FaveurDivine);

                m.Heal((int)toHeal);

                m.FixedParticles(0x376A, 9, 32, 5005, EffectLayer.Waist);
                m.PlaySound(0x1F2);
            }

            FinishSequence();
        }

        public class InternalTarget : Target
        {
            private GuerisonCelesteMiracle m_Owner;

            public InternalTarget(GuerisonCelesteMiracle owner)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}