using System;
using Server.Spells;

namespace Server.Engines.Combat
{
    public class Damage
    {
        private Damage() { }

        public static readonly Damage instance = new Damage();
        
        #region Degats Physiques
        public double DegatsPhysiquesReduits(Mobile atk, Mobile def, double dmg)
        {
            return DegatsPhysiquesReduits(atk, def, dmg, 0);
        }

        public double DegatsPhysiquesReduits(Mobile atk, Mobile def, double dmg, double incpen)
        {
            double basear = def.PhysicalResistance > 75 ? 75 : def.PhysicalResistance;
            double basepen = GetBonus(atk.Skills[SkillName.Penetration].Value, 0.3, 5);
            double reducedar = ReduceValue(basear, basepen);
            reducedar = ReduceValue(reducedar, incpen);

            return Reduction(dmg, reducedar + def.ArmureNaturelle);
        }
        #endregion

        #region Degats Magiques
        public void AppliquerDegatsMagiques(Mobile def, double dmg)
        {
            double reducedDmg = Reduction(dmg, def.MagicResistance);

            def.Damage((int)reducedDmg);
        }

        const double DPSBASE = 6.45;
        const short NbTotalCercles = 10; // Il y a pr�sentement 10 cercles dans le syst�me de magie.
        const double ScalingCategorie = 0.5;// Bonus qui fait la diff�rence entre un spell de cercle 1, et de cercle 10, pour les d�g�ts.


        public double GetDegatsMagiques(Mobile atk, SkillName branche, short cercle, TimeSpan tempsCast)
        {
            return BaseDegatsMagiques(cercle, tempsCast) * (1 + Spell.GetSpellScaling(atk, branche));
        }

        private double BaseDegatsMagiques(short cercle, TimeSpan tempsCast)
        {
            // Calcul de la fiche Excel.
            return (((((ScalingCategorie / NbTotalCercles) * cercle) + 1) * DPSBASE) * tempsCast.Seconds);
        }
        #endregion

        private double Reduction(double dmg, double resist)
        {
            resist = resist / 100;

            return dmg * (1 - resist);
        }

        protected double GetBonus(double value, double scalar, double offset)
        {
            double bonus = value * scalar;

            if (value >= 100)
                bonus += offset;

            return bonus / 100;
        }

        protected double ReduceValue(double value, double factor)
        {
            return value * (1 - factor);
        }

        protected double IncreasedValue(double value, double factor)
        {
            return value * (1 + factor);
        }

        protected double IncValueDimReturn(double value, double factor)
        {
            if (value >= 1) return 1;
            if (value < 0) value = 0;

            return (1 - value) * factor + value;
        }
    }
}