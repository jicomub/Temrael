using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
    public class InstinctCharnelSpell : ReligiousSpell
    {
        public static Hashtable m_InstinctCharnelTable = new Hashtable();
        public static Hashtable m_Timers = new Hashtable();

        private static SpellInfo m_Info = new SpellInfo(
                "Instinct Charnel", "Toki Impa",
                SpellCircle.Fifth,
                212,
                9041
            );

        public override int RequiredAptitudeValue { get { return 4; } }
        public override Aptitude[] RequiredAptitude { get { return new Aptitude[] { Aptitude.Monial }; } }

        public InstinctCharnelSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public override bool DelayedDamage { get { return false; } }

        public void Target(BaseCreature m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (!m.Summoned && !m.Controlled && m.ControlMaster != Caster && m.SummonMaster != Caster)
            {
                Caster.SendMessage("Vous ne pouvez cibler que les cr�atures que vous contr�lez.");
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, m);

                StopTimer(m);

                TimeSpan duration = GetDurationForSpell(1);

                m_InstinctCharnelTable[m] = 0.05 + ((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) / 800); //5 � 30% 

                Timer t = new InstinctCharnelTimer(m, DateTime.Now + duration);
                m_Timers[m] = t;
                t.Start();

                m.FixedParticles(2339, 10, 20, 5013, 1441, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                m.FixedParticles(8902, 10, 20, 5013, 1441, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(654);
            }

            FinishSequence();
        }

        public static void StopTimer(Mobile m)
        {
            Timer t = (Timer)m_Timers[m];

            if (t != null)
            {
                t.Stop();
                m_Timers.Remove(m);
                m_InstinctCharnelTable.Remove(m);

                m.FixedParticles(2339, 10, 20, 5013, 1441, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                m.FixedParticles(8902, 10, 20, 5013, 1441, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(654);
            }
        }

        public class InstinctCharnelTimer : Timer
        {
            private Mobile m_target;
            private DateTime endtime;

            public InstinctCharnelTimer(Mobile target, DateTime end)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(2))
            {
                m_target = target;
                endtime = end;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if ((DateTime.Now >= endtime && InstinctCharnelSpell.m_InstinctCharnelTable.Contains(m_target)) || m_target == null || m_target.Deleted || !m_target.Alive)
                {
                    InstinctCharnelSpell.m_InstinctCharnelTable.Remove(m_target);
                    InstinctCharnelSpell.m_Timers.Remove(m_target);

                    m_target.FixedParticles(2339, 10, 20, 5013, 1441, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                    m_target.FixedParticles(8902, 10, 20, 5013, 1441, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                    m_target.PlaySound(654);

                    Stop();
                }
            }
        }

        private class InternalTarget : Target
        {
            private InstinctCharnelSpell m_Owner;

            public InternalTarget(InstinctCharnelSpell owner)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseCreature)
                {
                    m_Owner.Target((BaseCreature)o);
                }
                else
                    from.SendMessage("Vous ne pouvez cibler que des cr�atures !");
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}