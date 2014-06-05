using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MindBlastScroll : SpellScroll
	{
		[Constructable]
		public MindBlastScroll() : this( 1 )
		{
		}

		[Constructable]
		public MindBlastScroll( int amount ) : base( 37, 0x1F51, amount )
		{
            Name = "Illusion: Lobotomie";
		}

		public MindBlastScroll( Serial serial ) : base( serial )
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

            Name = "Illusion: Lobotomie";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new MindBlastScroll( amount ), amount );
		}*/
	}
}