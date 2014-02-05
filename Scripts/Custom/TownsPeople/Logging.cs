using System;
using System.IO;
using Server;
using Server.Accounting;
using Server.Mobiles;

namespace Server.Commands
{
	public class TownspersonLogging
	{
		private static StreamWriter m_Output;

		public static StreamWriter Output
		{
			get{ return m_Output; } 
		}

		public static void Initialize()
		{

            if ( !Directory.Exists( "Backups" ) )
				Directory.CreateDirectory( "Backups" );
			if ( !Directory.Exists( "Backups/Logs" ) )
				Directory.CreateDirectory( "Backups/Logs" );

			string directory = "Backups/Logs/Townsperson";

			if ( !Directory.Exists( directory ) )
				Directory.CreateDirectory( directory );

			try
			{
                DateTime now = DateTime.Now;
                string today = String.Format("{0}-{1}-{2}, {3}", now.Year, now.Month, now.Day, now.DayOfWeek);
				m_Output = new StreamWriter( Path.Combine( directory, String.Format( "{0}.log", today) ), true );
				
				m_Output.AutoFlush = true;

				m_Output.WriteLine( "##############################" );
				m_Output.WriteLine( "Log started on {0}", DateTime.Now );
				m_Output.WriteLine( "Townsperson Logging set to {0}", Townsperson.Logging.ToString() );
				m_Output.WriteLine();
			}
			catch
			{
			}
		}

		public static object Format( object o )
		{
			if ( o is Mobile )
			{
				Mobile m = (Mobile)o;

				if ( m.Account == null )
					return String.Format( "{0} (no account)", m );
				else
					return String.Format( "{0} ('{1}')", m, ((Account)m.Account).Username );
			}
			else if ( o is Item )
			{
				Item item = (Item)o;

				return String.Format( "0x{0:X} ({1})", item.Serial.Value, item.GetType().Name );
			}

			return o;
		}

		public static void WriteLine( Mobile from, string format, params object[] args )
		{
			WriteLine( from, String.Format( format, args ) );
		}

		public static void WriteLine( Mobile from, string text )
		{
			try
			{
				m_Output.WriteLine( "{0}: {1}: {2}", DateTime.Now.ToShortTimeString(), TownspersonLogging.Format( from ), text );

				string path = Core.BaseDirectory;
                AppendPath( ref path, "Backups" );
				AppendPath( ref path, "Logs" );
				AppendPath( ref path, "Townsperson" );
				path = Path.Combine( path, String.Format( "{0}.log", DateTime.Now.ToLongDateString() ) );

				using ( StreamWriter sw = new StreamWriter( path, true ) )
					sw.WriteLine( "{0}: {1}: {2}", DateTime.Now, TownspersonLogging.Format( from ), text );
			}
			catch
			{
			}
		}

		public static void AppendPath( ref string path, string toAppend )
		{
			path = Path.Combine( path, toAppend );

			if ( !Directory.Exists( path ) )
				Directory.CreateDirectory( path );
		}
	}
}