using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a poison elementals corpse" )]
	public class PoisonElemental : BaseCreature
	{
		[Constructable]
		public PoisonElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a poison elemental";
			Body = 162;
			BaseSoundID = 263;

			SetStr( 426, 515 );
			SetDex( 166, 185 );
			SetInt( 361, 435 );

			SetHits( 256, 309 );

			SetDamage( 12, 18 );

			SetDamageType( ResistanceType.Physical, 10 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			
			
			
			SetResistance( ResistanceType.Magie, 40, 50 );

			//SetSkill( SkillName.EvalInt, 80.1, 95.0 );
			SetSkill( SkillName.ArtMagique, 80.1, 95.0 );
			SetSkill( SkillName.Concentration, 80.2, 120.0 );
			SetSkill( SkillName.Empoisonnement, 90.1, 100.0 );
			SetSkill( SkillName.Concentration, 85.2, 115.0 );
			SetSkill( SkillName.Tactiques, 80.1, 100.0 );
			SetSkill( SkillName.Anatomie, 70.1, 90.0 );

			VirtualArmor = 70;

			PackItem( new Nightshade( 4 ) );
			PackItem( new LesserPoisonPotion() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{ get{ return 0.75; } }

		public override int TreasureMapLevel{ get{ return 5; } }

		public PoisonElemental( Serial serial ) : base( serial )
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