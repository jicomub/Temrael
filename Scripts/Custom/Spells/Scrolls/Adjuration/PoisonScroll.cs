using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PoisonScroll : SpellScroll
	{
		[Constructable]
		public PoisonScroll() : this( 1 )
		{
		}

		[Constructable]
		public PoisonScroll( int amount ) : base( 20, 0x1F40, amount )
		{
            Name = "Adjuration: Poison";
		}

		public PoisonScroll( Serial serial ) : base( serial )
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

            Name = "Adjuration: Poison";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new PoisonScroll( amount ), amount );
		}*/
	}
}