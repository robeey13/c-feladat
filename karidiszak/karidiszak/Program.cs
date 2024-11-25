using System.Text;

namespace karidiszak
{
    class NapiMunka
    {
        public static int KeszultDb { get; private set; }
        public int Nap { get; private set; }
        public int HarangKesz { get; private set; }
        public int HarangEladott { get; private set; }
        public int AngyalkaKesz { get; private set; }
        public int AngyalkaEladott { get; private set; }
        public int FenyofaKesz { get; private set; }
        public int FenyofaEladott { get; private set; }

        public NapiMunka(string sor)
        {
            string[] s = sor.Split(';');
            Nap = Convert.ToInt32(s[0]);
            HarangKesz = Convert.ToInt32(s[1]);
            HarangEladott = Convert.ToInt32(s[2]);
            AngyalkaKesz = Convert.ToInt32(s[3]);
            AngyalkaEladott = Convert.ToInt32(s[4]);
            FenyofaKesz = Convert.ToInt32(s[5]);
            FenyofaEladott = Convert.ToInt32(s[6]);

            NapiMunka.KeszultDb += HarangKesz + AngyalkaKesz + FenyofaKesz;
        }

        public int NapiBevetel()
        {
            return -(HarangEladott * 1000 + AngyalkaEladott * 1350 + FenyofaEladott * 1500);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("diszek.txt", encoding: Encoding.UTF8);
            string line;
            List<NapiMunka> melo = new List<NapiMunka>();
            while((line = sr.ReadLine()) != null) 
            {
                   melo.Add(new NapiMunka(line));
            }

            int sum = 0;
            foreach (NapiMunka item in melo) {
                sum += item.AngyalkaKesz + item.FenyofaKesz + item.HarangKesz;
            }
            Console.WriteLine("Összesen " + sum + " dísz készült.");

            bool disznelkuli = false;
            foreach (NapiMunka item in melo) {
                if (item.AngyalkaKesz + item.FenyofaKesz + item.HarangKesz == 0)
                {
                    disznelkuli = true;
                    break;
                }
            }

            if (disznelkuli)
            {
                Console.WriteLine("Volt dísznélküli nap");
            }
            else
            {
                Console.WriteLine("Nem volt dísznélküli");
            }



            int bekertSzam;

            do { 
            bekertSzam = Int32.Parse(Console.ReadLine());
            } while ( !(bekertSzam >= 1 && bekertSzam <= 40));
            Console.WriteLine("Ez jó!");

            int angyalka = 0;
            int fenyofa = 0;
            int harang = 0;

            int nap = 1;

            foreach (NapiMunka item in melo)
            {
                angyalka += item.AngyalkaKesz;
                fenyofa += item.FenyofaKesz;
                harang += item.HarangKesz;

                angyalka += item.AngyalkaEladott;
                fenyofa += item.FenyofaEladott;
                harang += item.HarangEladott;
                if (nap == bekertSzam) 
                {
                    break;
                }
                nap++;
            }

            Console.WriteLine($"Az adott napon: \n angyalka - {angyalka} \n fenyofa - {fenyofa} \n harang - {harang}");

        }
    }
}
