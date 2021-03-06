﻿using System;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("Cadavre d'orc")]
    public class Orc : BaseCreature
    {
        [Constructable]
        public Orc()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Orc";
            Body = 17;
            BaseSoundID = 433;

            PlayersAreEnemies = true;
            Direction = Direction.West;

            SetStr(100);
            SetDex(60);
            SetInt(10);

            SetHits(150);
            SetMana(10);
            SetStam(120);
            SetArme(4, 8, 40);

            SetResistance(ResistanceType.Physical, 40);
            SetResistance(ResistanceType.Magical, 0);

            SetSkill(SkillName.ArmureNaturelle, 40);
            SetSkill(SkillName.Tactiques, 52);
            SetSkill(SkillName.Epee, 50);
            SetSkill(SkillName.Detection, 60);
            SetSkill(SkillName.CoupCritique, 18);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Junk);
            AddLoot(LootPack.UtilityItems);
            AddLoot(LootPack.Food);
        }

        public override int Bones { get { return 2; } }
        public override BoneType BoneType { get { return BoneType.Nordique; } }

        public Orc(Serial serial)
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