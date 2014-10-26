using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class ParalyzeScroll : SpellScroll
	{
		[Constructable]
		public ParalyzeScroll() : this( 1 )
		{
		}

		[Constructable]
		public ParalyzeScroll( int amount ) : base( ParalyzeSpell.m_SpellID, 0x1F52, amount )
		{
            Name = "Altération: Paralysie";
		}
		
		public ParalyzeScroll( Serial serial ) : base( serial )
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

            Name = "Altération: Paralysie";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new ParalyzeScroll( amount ), amount );
		}*/
	}
}