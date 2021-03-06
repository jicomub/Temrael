﻿using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class GuerisonMiraculeuseMiracleScroll : SpellScroll
    {
        [Constructable]
        public GuerisonMiraculeuseMiracleScroll()
            : this(1)
        {
        }

        [Constructable]
        public GuerisonMiraculeuseMiracleScroll(int amount)
            : base(2004, 0x227B, amount)
        {
            Name = "Guérison Miraculeuse";
        }

        public GuerisonMiraculeuseMiracleScroll(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}