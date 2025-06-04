using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Repair_of_quipment_Kremlakova.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Repair_of_quipment_Kremlakova
{
    public partial class AE_Employee : Form
    {
        private SqlConnection sqlConnection = null;
        private DataSet dataSet = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private bool newRowAdding = false;
        private SqlCommandBuilder sqlBuilder = null;
        private DataTable dt = new DataTable();
        private DataTable dt2 = new DataTable();
        public AE_Employee()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-3HK6G3K\SQLEXPRESS;Initial Catalog=Repair_of_quipment;Integrated Security=True;User ID=Iam;Password=111");
            sqlConnection.Open();
            lb1.Text = data.arrEmployee[0].ToString();
            lb2.Text = data.arrEmployee[1].ToString();
            lb3.Text = data.arrEmployee[2].ToString();
            lb4.Text = data.arrEmployee[3].ToString();
            lb5.Text = data.arrEmployee[4].ToString();
            lb6.Text = data.arrEmployee[5].ToString();
            if (data.mode == "add")
            {
                custom_button1.Text = "Добавить";
                this.Text = "Добавить запись в таблицу Сотрудники";
            }
            else if (data.mode == "edit")
            {
                custom_button1.Text = "Изменить";
                this.Text = "Изменить запись таблицы Сотрудники";
                SqlDataAdapter sda = new SqlDataAdapter("Select * From Employee where Id_employee = '" + data.idIndex + "'", sqlConnection);
                sda.Fill(dt);
                tb1.Text = dt.Rows[0][0].ToString();
                tb2.Text = dt.Rows[0][1].ToString();
                tb3.Text = dt.Rows[0][2].ToString();
                tb4.Text = dt.Rows[0][3].ToString();
                tb5.Text = dt.Rows[0][4].ToString();
                tb6.Text = dt.Rows[0][5].ToString();
            }
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            data.exit = 1;
            this.Close();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void custom_button1_Click(object sender, EventArgs e)
        {
            if (custom_button1.Text == "Изменить")
            {
                this.Text = "Изменить запись";
                if (tb3.Text.ToString() == "" || tb4.Text.ToString() == "" || tb5.Text.ToString() == "" || tb2.Text.ToString() == "")

                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("UPDATE Employee SET Name='" + tb2.Text.ToString() + "', Surname ='" + tb3.Text.ToString() + "', Father_name ='" + tb4.Text.ToString() + "', Phone ='" + tb5.Text.ToString() + "', Post ='" + tb6.Text.ToString() + "' WHERE Id_employee = '" + data.idIndex + "'", sqlConnection);
                    cmd.ExecuteNonQuery();
                    Form1 form1 = new Form1();
                    MessageBox.Show("Запись изменена");
                    form1.Show();
                    this.Close();
                }
            }

            if (custom_button1.Text == "Добавить")
            {
                if (tb3.Text.ToString() == "" || tb4.Text.ToString() == "" || tb5.Text.ToString() == "" || tb2.Text.ToString() == "")
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into [Employee] ( Name, Surname, Father_name, Phone, Post)" +
                        "values (@Name, @Surname, @Father_name, @Phone, @Post)", sqlConnection);
                    cmd.Parameters.AddWithValue("Name", tb2.Text);
                    cmd.Parameters.AddWithValue("Surname", tb3.Text);
                    cmd.Parameters.AddWithValue("Father_name", tb4.Text);
                    cmd.Parameters.AddWithValue("Phone", tb5.Text);
                    cmd.Parameters.AddWithValue("Post", tb6.Text);

                    cmd.ExecuteNonQuery();

                    tb1.Text = "";
                    tb2.Text = "";
                    tb3.Text = "";
                    tb4.Text = "";
                    tb5.Text = "";
                    tb6.Text = "";

                    MessageBox.Show("Запись добавлена");
                }
            }
            }
    }
}
