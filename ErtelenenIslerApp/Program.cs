using ErtelenenIslerApp.Base;
using ErtelenenIslerApp.Data;
using ErtelenenIslerApp.Data.Repository;
using System;
using System.Threading;

namespace ErtelenenIslerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            VeritabaniIslemleri veritabaniIslemleri = new VeritabaniIslemleri();

            int musteriNumarasi = 15000000;

            RPersonnelEntity personnelRepository = new RPersonnelEntity();
            PersonnelEntity personnelEntity = personnelRepository.Get(musteriNumarasi);

            CalistirmaMotoru.KomutCalistir("MuhasebeModulu", "MaasYatir", personnelEntity);

            CalistirmaMotoru.KomutCalistir("MuhasebeModulu", "YillikUcretTahsilEt", personnelEntity);

            CalistirmaMotoru.BekleyenIslemleriGerceklestir();
        }
    }

    public class CalistirmaMotoru
    {

        public static object[] KomutCalistir(string modülSınıfAdı, string methodAdı, PersonnelEntity personnelEntity)
        {
            MuhasebeModulu muhasebeModulu = new MuhasebeModulu();
            muhasebeModulu.MaasYatir(personnelEntity);

            throw new NotImplementedException();
        }

        public static void BekleyenIslemleriGerceklestir()
        {
            // ?
        }
    }

    public class MuhasebeModulu : BaseDB
    {
        public void MaasYatir(PersonnelEntity personnelEntity)
        {
            try
            {
                using (var session = SessionFactory.GetFactory().OpenSession())
                {
                    personnelEntity.PersonnelCode = "DUA.LIPA";
                    personnelEntity.PersonnelDesc = "Dua Lipa";
                    personnelEntity.CustomerNumber = 15000000;
                    personnelEntity.AccountCode = "DL15000000";
                    personnelEntity.AccountDesc = "Maaş";
                    personnelEntity.Salary = 1000000000;
                    personnelEntity.Currency = "$";
                    personnelEntity.IsPaid = true;

                    session.Save(personnelEntity);
                    session.Flush();

                    Console.WriteLine(string.Format("{0} numaralı müşterinin maaşı yatırıldı.", personnelEntity.CustomerNumber));
                }

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {

            }
        }

        private void YillikUcretTahsilEt(int musteriNumarasi)
        {
            try
            {
                RPersonnelEntity personnelRepository = new RPersonnelEntity();
                PersonnelEntity personnelEntity = personnelRepository.Get(musteriNumarasi);

                if (personnelEntity != null && personnelEntity.Salary > 0)
                {
                    using (var session = SessionFactory.GetFactory().OpenSession())
                    {
                        personnelEntity.Salary = 0;
                        session.Update(personnelEntity);
                        session.Flush();

                        Console.WriteLine("{0} numaralı müşteriden yıllık kart ücreti tahsil edildi.", musteriNumarasi);
                    }
                }

                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {

            }
        }

        private void OtomatikOdemeleriGerceklestir(int musteriNumarasi)
        {
            // gerekli işlemler gerçekleştirilir.
            Console.WriteLine("{0} numaralı müşterinin otomatik ödemeleri gerçekleştirildi.", musteriNumarasi);
        }
    }

    public class VeritabaniIslemleri
    {
    }
}
