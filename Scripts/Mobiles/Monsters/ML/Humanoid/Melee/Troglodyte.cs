using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a troglodyte corpse" )]
	public class Troglodyte : BaseCreature
	{

		[Constructable]
		public Troglodyte() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) // NEED TO CHECK
		{
			Name = "a troglodyte";
            Body = 400;
			BaseSoundID = 0x59F; 

			SetStr( 148, 217 );
			SetDex( 91, 120 );
			SetInt( 51, 70 );

			SetHits( 302, 340 );

			SetDamage( 11, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Magie, 30, 40 );

			//SetSkill( SkillName.Anatomy, 70.5, 94.8 );
			SetSkill( SkillName.Concentration, 51.8, 65.0 );
			SetSkill( SkillName.Tactiques, 80.4, 94.7 );
			SetSkill( SkillName.Anatomie, 70.2, 93.5 );

			VirtualArmor = 28; // Don't know what it should be

			PackItem( new Bandage( 5 ) );  // How many?
			PackItem( new Ribs() );

		}

			public override bool CanHeal { get { return true; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );  // Need to verify
		}

		public Troglodyte( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
