using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Test_Sefer_Seyahat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=Test_Yolcu_Bilet;Integrated Security=True");

        void seferlistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLSEFERBILGI", bgl);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand cmd = new SqlCommand("Insert Into TBLYOLCUBILGI (AD, SOYAD, TELEFON, TC, CINSIYET, MAIL) Values (@p1, @p2, @p3, @p4, @p5, @p6)", bgl);
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", MskTel.Text);
            cmd.Parameters.AddWithValue("@p4", MskTC.Text);
            cmd.Parameters.AddWithValue("@p5", CmbCinsiyet.Text);
            cmd.Parameters.AddWithValue("@p6", TxtMail.Text);
            cmd.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Yolcu Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnKaptan_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand cmd = new SqlCommand("Insert Into TBLKAPTAN (KAPTANNO, ADSOYAD, TELEFON) Values (@p1, @p2, @p3)", bgl);
            cmd.Parameters.AddWithValue("@p1", TxtKaptanNo.Text);
            cmd.Parameters.AddWithValue("@p2", TxtKaptanAd.Text);
            cmd.Parameters.AddWithValue("@p3", MskKaptanTel.Text);
            cmd.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kaptan Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnSeferOlus_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand cmd = new SqlCommand("Insert Into TBLSEFERBILGI (KALKIS, VARIS, TARIH, SAAT, KAPTAN, FIYAT) Values (@p1, @p2, @p3, @p4, @p5, @p6)", bgl);
            cmd.Parameters.AddWithValue("@p1", TxtKalkis.Text);
            cmd.Parameters.AddWithValue("@p2", TxtVaris.Text);
            cmd.Parameters.AddWithValue("@p3", MskTarih.Text);
            cmd.Parameters.AddWithValue("@p4", MskSaat.Text);
            cmd.Parameters.AddWithValue("@p5", MskKaptan.Text);
            cmd.Parameters.AddWithValue("@p6", TxtFiyat.Text);
            cmd.ExecuteReader();
            bgl.Close();
            MessageBox.Show("Sefer Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            seferlistesi();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            seferlistesi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtRezSeferNo.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "1";
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "2";
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "3";
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "4";
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "5";
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "6";
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "7";
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "8";
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            TxtKoltukNo.Text = "9";
        }

        private void BtnRezYap_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand cmd = new SqlCommand("Select TC From TBLYOLCUBILGI Where TC = @p1", bgl);
            cmd.Parameters.AddWithValue("@p1", MskYolcuTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                if (MskYolcuTC.Text.Length == 11 && MskYolcuTC.Text != "")
                {
                    SqlCommand cmd2 = new SqlCommand("Insert Into TBLSEFERDETAY (SEFERNO, YOLCUTC, KOLTUK) Values (@p1, @p2, @p3)", bgl);
                    cmd2.Parameters.AddWithValue("@p1", TxtRezSeferNo.Text);
                    cmd2.Parameters.AddWithValue("@p2", MskYolcuTC.Text);
                    cmd2.Parameters.AddWithValue("@p3", TxtKoltukNo.Text);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Rezervasyon Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("11 Haneli T.C. Numaranız Eksik, Lütfen Tekrar Kontrol Ediniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            else
            {
                MessageBox.Show("Hatalı T.C Numarası veya Böyle Bir T.C. Numarası Yolcu Listesinde Kayıtlı Değil", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bgl.Close();           
        }
    }
}
