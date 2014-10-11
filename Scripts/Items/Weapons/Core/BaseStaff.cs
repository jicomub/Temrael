using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public abstract class BaseStaff : BaseBashing
	{
		public override int DefHitSound{ get{ return 0x233; } }
		public override int DefMissSound{ get{ return 0x239; } }

		public override WeaponType DefType{ get{ return WeaponType.Staff; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Bash2H; } }

		public BaseStaff( int itemID ) : base( itemID )
		{
            Layer = Layer.TwoHanded;
		}

		public BaseStaff( Serial serial ) : base( serial )
		{
            Layer = Layer.TwoHanded;
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
		}
	}
}