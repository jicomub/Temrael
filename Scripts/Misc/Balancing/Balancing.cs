﻿using System;
using System.IO;

namespace Server.Misc.Balancing
{
    public delegate void DeathEventHandler( DeathEventArgs e );

    public class Balancing
    {
        private DateTime start;
        private DateTime end;
        private int tempsEcoule;
        private int hprestantTotal;
        private bool done;
        private TestMobile loser;
        private TestMobile mob1, mob2;
        private int combats, gains1, gains2;
        private event DeathEventHandler Death;
        private string fileLocation;

        public static void Initialize()
        {
            if (!Core.Balancing)
                return;

            #region Tests des différentes armes, plaque seulement.
            TestMobile mob1 = new MobilePlaqueEpeeRapide();
            TestMobile mob2 = new MobilePlaqueEpeeLente();
            new Balancing(mob1, mob2, "PLAQUE - 2H Rapide vs 2H Lente.txt");

            mob1 = new MobilePlaqueEpeeLenteBouclier();
            mob2 = new MobilePlaqueEpeeLente();
            new Balancing(mob1, mob2, "PLAQUE - 1H Lente vs 2H Lente.txt");

            mob1 = new MobilePlaqueEpeeLenteBouclier();
            mob2 = new MobilePlaqueEpeeRapide();
            new Balancing(mob1, mob2, "PLAQUE - 1H Lente vs 2H Rapide.txt");

            mob1 = new MobilePlaqueEpeeRapideBouclier();
            mob2 = new MobilePlaqueEpeeLente();
            new Balancing(mob1, mob2, "PLAQUE - 1H Rapide vs 2H Lente.txt");

            mob1 = new MobilePlaqueEpeeRapideBouclier();
            mob2 = new MobilePlaqueEpeeRapide();
            new Balancing(mob1, mob2, "PLAQUE - 1H Rapide vs 2H Rapide.txt");

            mob1 = new MobilePlaqueEpeeRapideBouclier();
            mob2 = new MobilePlaqueEpeeLenteBouclier();
            new Balancing(mob1, mob2, "PLAQUE - 1H Rapide vs 1H Lente.txt");
            #endregion

            #region Tests des différentes armes, cuir seulement.
            mob1 = new MobileCuirEpeeRapide();
            mob2 = new MobileCuirEpeeLente();
            new Balancing(mob1, mob2, "CUIR - 2H Rapide vs 2H Lente.txt");

            mob1 = new MobileCuirEpeeLenteBouclier();
            mob2 = new MobileCuirEpeeLente();
            new Balancing(mob1, mob2, "CUIR - 1H Lente vs 2H Lente.txt");

            mob1 = new MobileCuirEpeeLenteBouclier();
            mob2 = new MobileCuirEpeeRapide();
            new Balancing(mob1, mob2, "CUIR - 1H Lente vs 2H Rapide.txt");

            mob1 = new MobileCuirEpeeRapideBouclier();
            mob2 = new MobileCuirEpeeLente();
            new Balancing(mob1, mob2, "CUIR - 1H Rapide vs 2H Lente.txt");

            mob1 = new MobileCuirEpeeRapideBouclier();
            mob2 = new MobileCuirEpeeRapide();
            new Balancing(mob1, mob2, "CUIR - 1H Rapide vs 2H Rapide.txt");

            mob1 = new MobileCuirEpeeRapideBouclier();
            mob2 = new MobileCuirEpeeLenteBouclier();
            new Balancing(mob1, mob2, "CUIR - 1H Rapide vs 1H Lente.txt");
            #endregion
        }

        public Balancing(TestMobile m1, TestMobile m2)
        {
            m1.Balance = this;
            m2.Balance = this;
            mob1 = m1;
            mob2 = m2;
            fileLocation = null;

            Death += new DeathEventHandler(EventSink_Death);

            StartCombat();
            new FinishTimer(this).Start();
        }

        public Balancing(TestMobile m1, TestMobile m2, string loc) : this(m1, m2)
        {
            fileLocation = loc;   
        }

        public void InvokeDeath(DeathEventArgs e)
        {
            if (Death != null)
                Death(e);
        }

        public void StartCombat()
        {
            done = false;
            Heal(mob1);
            Heal(mob2);
            mob1.Map = mob2.Map = Map.Felucca;
            mob1.Location = new Point3D(4059, 66, 0);
            mob2.Location = new Point3D(4060, 66, 0);
            mob1.Combatant = mob2;
            mob2.Combatant = mob1;
            start = DateTime.Now;
        }

        public void Heal(Mobile m)
        {
            m.Hits = m.HitsMax;
            m.Stam = m.StamMax;
            m.Mana = m.ManaMax;
        }

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(String.Format(format, args));
        }

        public void WriteLine(string text)
        {
            if (fileLocation == null)
            {
                Console.WriteLine(text);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(fileLocation, false))
                    sw.WriteLine(text);
            }
        }

        public void EventSink_Death(DeathEventArgs e)
        {
            TestMobile from = e.Mobile;
            loser = from;
            end = DateTime.Now;

            done = true;
        }

        public void Loop()
        {
            ReportFight();
            StartCombat();
        }

        public void ReportFight()
        {
            int hitsleft = 0;
            string gagnant = "";
            combats++;
            if(loser == mob1)
            {
                gains2++;
                hitsleft = mob2.Hits;
                gagnant = mob2.Name;
            }
            else if(loser == mob2)
            {
                gains1++;
                hitsleft = mob1.Hits;
                gagnant = mob1.Name;
            }
            TimeSpan duration = end - start;
            tempsEcoule += (int)duration.TotalSeconds;
            hprestantTotal += hitsleft;

            string s;
            string durText = String.Format("{0}:{1}:{2}", duration.Hours, duration.Minutes, duration.Seconds);

            s = "-----------------------------\r\n";
            s += mob1.Name + " CONTRE " + mob2.Name + "\r\n";
            s += String.Format("\tCombat {1}\tDurée: {0}.\r\n\tGagnant: combattant {2}.\r\n\tVie restante: {3}.", 
               durText, combats, gagnant, hitsleft);
            s += String.Format("\tRésultats préliminaires:\r\n\t\tCombattant {2} : {0} ({4}%).\r\n\t\tCombattant {3} : {1} ({5}%).", 
                gains1, gains2, mob1.Name, mob2.Name, gains1 * 100 /combats, gains2 * 100 /combats);
            s += "\r\n\t\tTemps moyen: " + (tempsEcoule / combats);
            s += "\r\n\t\tHp restant moyen: " + (hprestantTotal / combats);

            WriteLine(s);
        }

        public class FinishTimer : Timer
        {
            private Balancing balance;

            public FinishTimer(Balancing b) : base(new TimeSpan(0,0,1), new TimeSpan(0,0,1))
            {
                balance = b;
            }

            protected override void OnTick()
            {
                if (balance.done)
                {
                    balance.done = false;
                    balance.mob1.Combatant = null;
                    balance.mob2.Combatant = null;
                    balance.Loop();
                }
            }
        }
    }

	public class DeathEventArgs : EventArgs
	{
		private TestMobile m_Mobile;

		public TestMobile Mobile{ get{ return m_Mobile; } }

        public DeathEventArgs(TestMobile mobile)
        {
            m_Mobile = mobile;
        }
	}
}