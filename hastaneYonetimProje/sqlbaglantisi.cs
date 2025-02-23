using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace hastaneYonetimProje
{
    internal class sqlbaglantisi 
        /*SQL bağlantısını bir çok formda kullanacağımız için ve her defasında connection eklemek uzun olacağı için 
         sqlbaglantisi adında bir sınıf ve baglanti metoduyla işimizi kolaylaştırdık.*/
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=Hasan\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True;");
            baglan.Open();
            return baglan;


        
        
        }


    }
}
