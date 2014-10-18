using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class RecallScroll : SpellScroll
	{
		[Constructable]
		public RecallScroll() : this( 1 )
		{
		}

		[Constructable]
		public RecallScroll( int amount ) : base( RecallSpell.m_SpellID, 0x1F4C, amount )
		{
            Name = "Illusion: Rappel";
		}

		public RecallScroll( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            Name = "Illusion: Rappel";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new RecallScroll( amount ), amount );
		}*/
	}
}