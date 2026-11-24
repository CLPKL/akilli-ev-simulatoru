using AkilliEvSimulatoru;

class Program
{
    static void Main(string[] args)
    {
        List<Cihaz> cihazlar = new List<Cihaz>
        {
            new Cihaz("1", "Salon Lambası", CihazTuru.Lamba),
            new Cihaz("2", "Salon Kliması", CihazTuru.Klima),
            new Cihaz("3", "Ana Güvenlik Paneli", CihazTuru.GuvenlikSistemi)
        };

        bool devam = true;

        while (devam)
        {
            Console.WriteLine("\n=== AKILLI EV MENÜSÜ ===");
            Console.WriteLine("1 - Cihazları Listele");
            Console.WriteLine("2 - Yeni Cihaz Ekle");
            Console.WriteLine("3 - Cihaz Aç");
            Console.WriteLine("4 - Cihaz Kapat");
            Console.WriteLine("0 - Çıkış");
            Console.Write("Seçim: ");

            string? secim = Console.ReadLine()?.Trim();

            switch (secim)
            {
                case "1":
                    CihazlariListele(cihazlar);
                    break;

                case "2":
                    CihazEkle(cihazlar);
                    break;

                case "3":
                    CihazDurumDegistir(cihazlar, ac: true);
                    break;

                case "4":
                    CihazDurumDegistir(cihazlar, ac: false);
                    break;

                case "0":
                    devam = false;
                    break;

                default:
                    Console.WriteLine("Hata: Geçersiz menü seçimi.");
                    break;
            }
        }
    }

    static void CihazlariListele(List<Cihaz> cihazlar)
    {
        if (cihazlar.Count == 0)
        {
            Console.WriteLine("Listelenecek cihaz bulunamadı.");
            return;
        }

        Console.WriteLine("\n--- Kayıtlı Cihazlar ---");
        foreach (Cihaz cihaz in cihazlar)
        {
            cihaz.BilgileriGoster();
        }
    }

    static void CihazEkle(List<Cihaz> cihazlar)
    {
        Console.Write("Cihaz Kimliği: ");
        string kimlik = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(kimlik))
        {
            Console.WriteLine("Hata: Kimlik boş bırakılamaz.");
            return;
        }

        bool ayniKimlikVarMi = cihazlar.Any(c => c.Kimlik.Equals(kimlik, StringComparison.OrdinalIgnoreCase));
        if (ayniKimlikVarMi)
        {
            Console.WriteLine($"Hata: '{kimlik}' kimliğine sahip bir cihaz zaten mevcut!");
            return;
        }

        Console.Write("Cihaz Adı: ");
        string ad = (Console.ReadLine() ?? "").Trim();

        Console.WriteLine("Cihaz Türü Seçin:");
        Console.WriteLine("1 - Lamba");
        Console.WriteLine("2 - Klima");
        Console.WriteLine("3 - GuvenlikSistemi");
        Console.Write("Tür seçimi: ");
        string? turSecim = Console.ReadLine()?.Trim();

        CihazTuru tur = turSecim switch
        {
            "1" => CihazTuru.Lamba,
            "2" => CihazTuru.Klima,
            "3" => CihazTuru.GuvenlikSistemi,
            _ => (CihazTuru)(-1)
        };

        if (!Enum.IsDefined(typeof(CihazTuru), tur))
        {
            Console.WriteLine("Hata: Geçersiz cihaz türü.");
            return;
        }

        cihazlar.Add(new Cihaz(kimlik, ad, tur));
        Console.WriteLine("Cihaz başarıyla eklendi (başlangıç durumu: Kapalı).");
    }

    static void CihazDurumDegistir(List<Cihaz> cihazlar, bool ac)
    {
        Console.Write("İşlem yapılacak Cihaz Kimliği: ");
        string kimlik = (Console.ReadLine() ?? "").Trim();

        Cihaz? cihaz = cihazlar.FirstOrDefault(c => c.Kimlik.Equals(kimlik, StringComparison.OrdinalIgnoreCase));

        if (cihaz == null)
        {
            Console.WriteLine($"Hata: '{kimlik}' kimliğine sahip bir cihaz bulunamadı.");
            return;
        }

        if (ac)
        {
            cihaz.Ac();
            Console.WriteLine($"'{cihaz.Ad}' açıldı.");
        }
        else
        {
            cihaz.Kapat();
            Console.WriteLine($"'{cihaz.Ad}' kapatıldı.");
        }
    }
}
