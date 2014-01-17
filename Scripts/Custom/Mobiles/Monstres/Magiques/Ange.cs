﻿using System;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
    [CorpseName("Corps d'Ange")]
    public class Ange : BaseCreature
    {
        public override double DispelDifficulty { get { return 125.0; } }
        public override double DispelFocus { get { return 45.0; } }

        [Constructable]
        public Ange()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Ange";
            Body = 123;

            SetStr(476, 505);
            SetDex(76, 95);
            SetInt(301, 325);

            SetHits(400, 800);

            SetDamage(20, 40);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 50);
            SetResistance(ResistanceType.Contondant, 30, 50);
            SetResistance(ResistanceType.Tranchant, 30, 50);
            SetResistance(ResistanceType.Perforant, 30, 50);
            SetResistance(ResistanceType.Magie, 30, 50);

            //SetSkill( SkillName.EvalInt, 70.1, 80.0 );
            SetSkill(SkillName.ArtMagique, 70.1, 80.0);
            SetSkill(SkillName.Concentration, 85.1, 95.0);
            SetSkill(SkillName.Tactiques, 70.1, 80.0);
            SetSkill(SkillName.ArmePoing, 60.1, 80.0);

            Fame = 15000;
            Karma = -15000;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average, 2);
            //AddLoot(LootPack.MedScrolls, 2);
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override double AttackSpeed { get { return 3.0; } }
        public override bool CanRummageCorpses { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }
        //public override int TreasureMapLevel { get { return 4; } }
        public override int Meat { get { return 1; } }
        public override int Bones { get { return 8; } }
        public override int Hides { get { return 6; } }
        public override HideType HideType { get { return HideType.Magique; } }
        public override BoneType BoneType { get { return BoneType.Magique; } }

        public Ange(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
