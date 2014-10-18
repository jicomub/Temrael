using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class CurseWeaponScroll : SpellScroll
	{
		[Constructable]
		public CurseWeaponScroll() : this( 1 )
		{
		}

		[Constructable]
		public CurseWeaponScroll( int amount ) : base( CurseWeaponSpell.m_SpellID, 0x2263, amount )
		{
            Name = "N�cromancie: Maudire";
		}

		public CurseWeaponScroll( Serial serial ) : base( serial )
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

            Name = "N�cromancie: Maudire";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new CurseWeaponScroll( amount ), amount );
		}*/
	}
}