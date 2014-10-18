using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Factions;

namespace Server
{
	public class SpeedInfo
	{
		// Should we use the new method of speeds?
		private static bool Enabled = true;

		private double m_ActiveSpeed;
		private double m_PassiveSpeed;
		private Type[] m_Types;

		public double ActiveSpeed
		{
			get{ return m_ActiveSpeed; }
			set{ m_ActiveSpeed = value; }
		}

		public double PassiveSpeed
		{
			get{ return m_PassiveSpeed; }
			set{ m_PassiveSpeed = value; }
		}

		public Type[] Types
		{
			get{ return m_Types; }
			set{ m_Types = value; }
		}

		public SpeedInfo( double activeSpeed, double passiveSpeed, Type[] types )
		{
			m_ActiveSpeed = activeSpeed;
			m_PassiveSpeed = passiveSpeed;
			m_Types = types;
		}

		public static bool Contains( object obj )
		{
			if ( !Enabled )
				return false;

			if ( m_Table == null )
				LoadTable();

			SpeedInfo sp = (SpeedInfo)m_Table[obj.GetType()];

			return ( sp != null );
		}

		public static bool GetSpeeds( object obj, ref double activeSpeed, ref double passiveSpeed )
		{
			if ( !Enabled )
				return false;

			if ( m_Table == null )
				LoadTable();

			SpeedInfo sp = (SpeedInfo)m_Table[obj.GetType()];

            /*if (sp == null)
            {
                return false;
            }*/

			/*activeSpeed = sp.ActiveSpeed;
			passiveSpeed = sp.PassiveSpeed;*/

            activeSpeed = 0.215;
            passiveSpeed = 0.50;

			return true;
		}

		private static void LoadTable()
		{
			m_Table = new Hashtable();

			for ( int i = 0; i < m_Speeds.Length; ++i )
			{
				SpeedInfo info = m_Speeds[i];
				Type[] types = info.Types;

				for ( int j = 0; j < types.Length; ++j )
					m_Table[types[j]] = info;
			}
		}

		private static Hashtable m_Table;

		private static SpeedInfo[] m_Speeds = new SpeedInfo[]
			{
                ///* Slow */
                //new SpeedInfo( 0.3, 0.6, new Type[]
                //{
                //    typeof( Zombie )
                //} ),
                ///* Fast */
                //new SpeedInfo( 0.2, 0.4, new Type[]
                //{
                //    //Ancien slow
                //    typeof( ArcticOgreLord ),	typeof( BogThing ),
                //    typeof( Bogle ),			typeof( BoneKnight ),		typeof( EarthElemental ),
                //    typeof( Ettin ),			typeof( FrostOoze ),		typeof( FrostTroll ),
                //    typeof( GazerLarva ),		typeof( Ghoul ),			typeof( Golem ),
                //    typeof( HeadlessOne ),		typeof( Mummy ),
                //    typeof( Ogre ),				typeof( OgreLord ),			typeof( PlagueBeast ),
                //    typeof( Quagmire ),			typeof( Rat ),				typeof( RottingCorpse ),
                //    typeof( Skeleton ),			typeof( Slime ),
                //    typeof( Walrus ),			typeof( RestlessSoul ),
                //    typeof( CrystalElemental ),	typeof( DarknightCreeper ),	typeof( MoundOfMaggots ),
                //    typeof( Juggernaut ),		typeof( Yamandon ),

                //    //Ancien medium
                //    typeof( ToxicElemental ),	typeof( AgapiteElemental ),	typeof( Alligator ),
                //    typeof( AncientLich ),		typeof( Betrayer ),			typeof( Bird ),
                //    typeof( BlackBear ),
                //    typeof( BloodElemental ),	typeof( Boar ),				typeof( Bogling ),
                //    typeof( BoneMagi ),			typeof( Brigand ),			typeof( BronzeElemental ),
                //    typeof( BrownBear ),		typeof( Bull ),				typeof( BullFrog ),
                //    typeof( Cat ),				typeof( Centaur ),			typeof( ChaosDaemon ),
                //    typeof( Chicken ),			typeof( GolemController ),	typeof( CopperElemental ),
                //    typeof( CopperElemental ),	typeof( Cow ),
                //    typeof( Cyclops ),			typeof( Daemon ),			typeof( DeepSeaSerpent ),
                //    typeof( DinosaureDesert ),	typeof( DireWolf ),			typeof( Dog ),
                //    typeof( Dolphin ),			typeof( Dragon ),			typeof( Drake ),
                //    typeof( DullCopperElemental ), typeof( Eagle ),			typeof( ElderGazer ),
                //    typeof( EvilMage ),			typeof( EvilMageLord ),		typeof( Executioner ),
                //    typeof( Savage ),			typeof( FireElemental ),	typeof( FireGargoyle ),
                //    typeof( DinosaureForet ),	typeof( DinosaureVolcan ),
                //    typeof( FrostSpider ),		typeof( Gargoyle ),			typeof( Gazer ),
                //    typeof( GiantRat ),			typeof( GiantSerpent ),
                //    typeof( GiantSpider ),		typeof( GiantToad ),		typeof( Goat ),
                //    typeof( GoldenElemental ),	typeof( Gorilla ),			typeof( GreatHart ),
                //    typeof( GreyWolf ),			typeof( GrizzlyBear ),		typeof( Guardian ),
                //    typeof( Harpy ),			typeof( HellHound ),
                //    typeof( Hind ),				typeof( HordeMinion ),		typeof( Horse ),
                //    typeof( Horse ),			typeof( IceElemental ),		typeof( IceFiend ),
                //    typeof( Imp ),				
                //    typeof( Kirin ),			typeof( Kraken ),	        
					
                //    typeof( Lizardman ),		typeof( Mongbat ),
                //    typeof( StrongMongbat ),	typeof( MountainGoat ),		typeof( Orc ),
                //    typeof( OrcBomber ),		typeof( OrcBrute ),			typeof( OrcCaptain ),
                //    typeof( OrcishLord ),		typeof( OrcishMage ),		typeof( PackHorse ),
                //    typeof( Panther ),			typeof( Pig ),
                //    typeof( PlagueSpawn ),		typeof( PolarBear ),		typeof( Rabbit ),
                //    typeof( Ratman ),			typeof( RatmanArcher ),		typeof( RatmanMage ),
                //    typeof( Bouc ),
                //    typeof( Chocobo ),		typeof( Scorpion ),			typeof( SeaSerpent ),
                //    typeof( SerpentineDragon ),	typeof( Shade ),			typeof( ShadowIronElemental ),
                //    typeof( ShadowWisp ),		typeof( ShadowWyrm ),		typeof( Sheep ),
                //    typeof( SkeletalDragon ),	typeof( SkeletalMage ),
                //    typeof( MontureMortVivante ),	typeof( Snake ),
                //    typeof( SpectralArmour ),	typeof( Spectre ),
                //    typeof( StoneGargoyle ),	typeof( StoneHarpy ),		typeof( DragonMarais ),
                //    typeof( DragonMaraisApprivoise ), typeof( SwampTentacle ),	typeof( TerathanAvenger ),
                //    typeof( TerathanDrone ),	typeof( TerathanMatriarch ), typeof( TerathanWarrior ),
                //    typeof( TimberWolf ),		typeof( Titan ),			typeof( Troll ),
                //    typeof( Licorne ),			typeof( ValoriteElemental ), typeof( VeriteElemental ),
                //    typeof( WaterElemental ),	typeof( WhippingVine ),
                //    typeof( WhiteWolf ),		typeof( Wraith ),			typeof( Wyvern ),
                //    typeof( KhaldunZealot ),	typeof( KhaldunSummoner ),	typeof( ChocoboSauvage ),
                //    typeof( LichLord ),			typeof( SkeletalKnight ),	typeof( SummonedDaemon ),
                //    typeof( SummonedEarthElemental ),	typeof( SummonedWaterElemental ), typeof( SummonedFireElemental ),
                //    typeof( MeerWarrior ),		typeof( MeerEternal ),		typeof( MeerMage ),
                //    typeof( MeerCaptain ),		typeof( JukaLord ),			typeof( JukaMage ),
                //    typeof( JukaWarrior ),		typeof( AbysmalHorror ),	typeof( BoneDemon ),
                //    typeof( Devourer ),			typeof( FleshGolem ),		typeof( Gibberling ),
                //    typeof( GoreFiend ),		typeof( Impaler ),			typeof( PatchworkSkeleton ),
                //    typeof( Ravager ),			typeof( ShadowKnight ),		typeof( SkitteringHopper ),
                //    typeof( Treefellow ),		typeof( VampireBat ),		typeof( WailingBanshee ),
                //    typeof( WandererOfTheVoid ),	typeof( Cursed ),
                //    typeof( ShadowFiend ),
                //    typeof( SpectralArmour ),	typeof( ArcaneDaemon ),
                //    typeof( Doppleganger ),		typeof( EnslavedGargoyle ), typeof( ExodusMinion ),
                //    typeof( ExodusOverseer ),	typeof( GargoyleDestroyer ),	typeof( GargoyleEnforcer ),
                //    typeof( Moloch ),			typeof( BakeKitsune ),		typeof( DeathwatchScarabeeHatchling ),
                //    typeof( Kappa ),			typeof( KazeKemono ),		typeof( DeathwatchScarabee ),
                //    typeof( TsukiWolf ),		typeof( YomotsuElder ),		typeof( YomotsuPriest ),
                //    typeof( YomotsuWarrior ),	typeof( RevenantLion ),		typeof( Oni ),
                //    typeof( Scarabee ),

                //    typeof( AirElemental ),
                //    typeof( AncientWyrm ),		typeof( Balron ),			typeof( BladeSpirits ),
                //    typeof( DreadSpider ),		typeof( Efreet ),			typeof( EtherealWarrior ),
                //    typeof( Lich ),				typeof( Cauchemar ),		typeof( OphidianArchmage ),
                //    typeof( OphidianMage ),		typeof( OphidianWarrior ),	typeof( OphidianMatriarch ),
                //    typeof( OphidianKnight ),	typeof( PoisonElemental ),	typeof( Revenant ),
                //    typeof( SandVortex ),		typeof( SavageRider ),		typeof( SavageShaman ),
                //    typeof( SnowElemental ),	typeof( WhiteWyrm ),		typeof( Wisp ),
                //    typeof( DemonKnight ),		typeof( GiantBlackWidow ),	typeof( SummonedAirElemental ),
                //    typeof( Griffon ),			typeof( LadyOfTheSnow ),
                //    typeof( RaiJu ),			typeof( Ronin ),			typeof( RuneScarabee )
                //} ),
                ///* Very Fast */
                //new SpeedInfo( 0.175, 0.350, new Type[]
                //{
                //    typeof( EnergyVortex ),
                //    typeof( EliteNinja ),			typeof( Pixie ),		
                //    typeof( VorpalBunny ),		typeof( FleshRenderer ),	typeof( KhaldunRevenant ),
                //    typeof( FactionDragoon ),	typeof( FactionKnight ),	typeof( FactionPaladin ),
                //    typeof( FactionHenchman ),	typeof( FactionMercenary ),	typeof( FactionNecromancer ),
                //    typeof( FactionSorceress ),	typeof( FactionWizard ),	typeof( FactionBerserker ),
                //    typeof( FactionPaladin ),	typeof( Leviathan ),		typeof( FireScarabee ),
                //    typeof( FanDancer )		
                //} ),
                ///* Medium */
                //new SpeedInfo( 0.25, 0.5, new Type[]
                //{
                //} )
			};
	}
}