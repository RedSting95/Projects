namespace Telekocsi.Models
{
    public class Tariff
    {
        public static int Price { get; set; }

        public Tariff()
        {
            using (StreamReader sr = new StreamReader("Tariffconfig.txt"))
            {
                string p = sr.ReadLine();
                Price=Convert.ToInt32(p);
            }
        }


    }
}
