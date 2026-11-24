namespace AkilliEvSimulatoru;

public class Cihaz
{
    public string Kimlik { get; }
    public string Ad { get; }
    public CihazTuru Tur { get; }
    public bool AcikMi { get; private set; }

    public Cihaz(string kimlik, string ad, CihazTuru tur)
    {
        Kimlik = kimlik;
        Ad = ad;
        Tur = tur;
        AcikMi = false;
    }

    public void Ac()
    {
        AcikMi = true;
    }

    public void Kapat()
    {
        AcikMi = false;
    }

    public void BilgileriGoster()
    {
        string durum = AcikMi ? "Açık" : "Kapalı";
        Console.WriteLine($"[{Kimlik}] {Ad} | Tür: {Tur} | Durum: {durum}");
    }
}