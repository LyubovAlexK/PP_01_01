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
    public partial class AE_Users : Form
    {
        private SqlConnection sqlConnection = null;
        private DataSet dataSet = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private bool newRowAdding = false;
        private SqlCommandBuilder sqlBuilder = null;
        private DataTable dt = new DataTable();
        private DataTable dt2 = new DataTable();
        public AE_Users()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-3HK6G3K\SQLEXPRESS;Initial Catalog=Repair_of_quipment;Integrated Security=True;User ID=Iam;Password=111");
            sqlConnection.Open();
            lb1.Text = data.arrUsers[0].ToString();
            lb2.Text = data.arrUsers[1].ToString();
            lb3.Text = data.arrUsers[2].ToString();
            lb4.Text = data.arrUsers[3].ToString();
            if (data.mode == "add")
            {
                custom_button1.Text = "Добавить";
                this.Text = "Добавить запись в таблицу Пользователи";
            }
            else if (data.mode == "edit")
            {
                custom_button1.Text = "Изменить";
                this.Text = "Изменить запись таблицы Пользователи";
                SqlDataAdapter sda = new SqlDataAdapter("Select * From Users where Id_user = '" + data.idIndex + "'", sqlConnection);
                sda.Fill(dt);
                tb1.Text = dt.Rows[0][0].ToString();
                tb2.Text = dt.Rows[0][1].ToString();
                tb3.Text = dt.Rows[0][2].ToString();
                tb4.Text = dt.Rows[0][3].ToString();
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
                if (tb3.Text.ToString() == "" || tb4.Text.ToString() == "" || tb2.Text.ToString() == "")

                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Login='" + tb2.Text.ToString() + "', Password ='" + tb3.Text.ToString() + "', Role ='" + tb4.Text.ToString() + "' WHERE Id_user = '" + data.idIndex + "'", sqlConnection);
                    cmd.ExecuteNonQuery();
                    Form1 form1 = new Form1();
                    MessageBox.Show("Запись изменена");
                    form1.Show();
                    this.Close();
                }
            }

            if (custom_button1.Text == "Добавить")
            {
                if (tb3.Text.ToString() == "" || tb4.Text.ToString() == "" || tb2.Text.ToString() == "")
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into [Users] ( Login, Password, Role)" +
                        "values (@Login, @Password, @Role)", sqlConnection);
                    cmd.Parameters.AddWithValue("Login", tb2.Text);
                    cmd.Parameters.AddWithValue("Password", tb3.Text);
                    cmd.Parameters.AddWithValue("Role", tb4.Text);
                    cmd.ExecuteNonQuery();

                    tb1.Text = "";
                    tb3.Text = "";
                    tb4.Text = "";
                    tb2.Text = "";

                    MessageBox.Show("Запись добавлена");
                }
            }
        }
    }
}
