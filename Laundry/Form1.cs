using System.Data;
using System.Data.SqlClient;

namespace Laundry
{
    public partial class Form1 : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataSet ds;
        String nama = "";

        Koneksi konn = new Koneksi();
        public Form1()
        {
            InitializeComponent();
            Form1_load();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void Form1_load()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from Tbl_Pelanggan", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_PELANGGAN");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_PELANGGAN";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void cbxjenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxjenis.Text == "Pakaian")
            {
                txtharga.Text = "7000";
            }
            else if (cbxjenis.Text == "Karpet")
            {
                txtharga.Text = "10000";
            }
            else if (cbxjenis.Text == "Selimut")
            {
                txtharga.Text = "12000";
            }
            else if (cbxjenis.Text == "Boneka")
            {
                txtharga.Text = "3000";
            }
        }

        // tombol proses untuk harga sesuai jenis barang yg ingin dicuci beserta kualitas cucian
        private void btnproses_Click_1(object sender, EventArgs e)
        {
            // int hasil;
            int c = 5000;
            int hasil = int.Parse(txtharga.Text) * int.Parse(txtberat.Text);

            txttotal.Text = System.Convert.ToString(hasil);
            if (radioButton2.Checked == true)
            {
                txttotal.Text = System.Convert.ToString(hasil + c);
            }

        }

        // menghitung uang kembalian 
        private void button4_Click_1(object sender, EventArgs e)
        {
            int kembalian = int.Parse(txtuangplgn.Text) - int.Parse(txttotal.Text);

            txtkembalian.Text = System.Convert.ToString(kembalian);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            if (txtnama.Text.Trim() == "" || txtberat.Text.Trim() == "" || cbxjenis.Text.Trim() == "" || txttotal.Text.Trim() == "" || dateTimePicker1.Text.Trim() == "")
            {
                MessageBox.Show("Mohon isi semua kolom");
            }
            else

            {
                SqlConnection conn = konn.GetConn();
                try
                {
                    cmd = new SqlCommand("INSERT INTO Tbl_Pelanggan VALUES ('" + txtnama.Text + "', '" + txtberat.Text + "Kg', '" + cbxjenis.Text + "','" + txttotal.Text + "','" + dateTimePicker1.Text + "')", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert data berhasil");
                    Form1_load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //fungsi menghapus inputan yg diberikan
        private void button2_Click_1(object sender, EventArgs e)
        {
            txtnama.Text = "";
            txtberat.Text = "";
            txtharga.Text = "";
            txttotal.Text = "";
            cbxjenis.Text = "";
            txtuangplgn.Text = "";
            txtkembalian.Text = "";
            dateTimePicker1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }


        // menghapus data pelanggan
        private void button1_Click(object sender, EventArgs e)
        {
            if (nama.Trim() == "")
            {
                MessageBox.Show("Silahkan Pilih data yang ingin dihapus");
            }
            else
            {
                //simpan data

                SqlConnection conn = konn.GetConn();
                try
                {
                    cmd = new SqlCommand("DELETE FROM TBL_PELANGGAN WHERE Nama ='" + nama + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("HAPUS DATA BERHASIL");
                    Form1_load();
                }

                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }


        // untuk cari pelanggan
        private void CariPelanggan()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from Tbl_Pelanggan where Nama like '%" + textBox1.Text + "%' or Jenis like '%" + textBox1.Text + "%'", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_PELANGGAN");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_Pelanggan";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            nama = row.Cells["Nama"].Value.ToString();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CariPelanggan();
        }
    }
}