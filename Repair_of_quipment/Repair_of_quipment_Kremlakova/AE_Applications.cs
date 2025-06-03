using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Repair_of_quipment_Kremlakova.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Repair_of_quipment_Kremlakova
{
    public partial class AE_Applications : Form
    {
        public string query = "SELECT id_client FROM Clients";
        public string query2 = "SELECT id_client FROM Clients WHERE id_client = '"+ data.idIndex+"'";
        private SqlConnection sqlConnection = null;
        private DataSet dataSet = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private bool newRowAdding = false;
        private SqlCommandBuilder sqlBuilder = null;
        private DataTable dt = new DataTable();
        private DataTable dt2 = new DataTable();
        public AE_Applications()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-3HK6G3K\SQLEXPRESS;Initial Catalog=Repair_of_quipment;Integrated Security=True;User ID=Iam;Password=111");
            sqlConnection.Open();
            lb1.Text = data.arrAplications[0].ToString();
            lb2.Text = data.arrAplications[1].ToString();
            lb3.Text = data.arrAplications[2].ToString();
            lb4.Text = data.arrAplications[3].ToString();
            lb5.Text = data.arrAplications[4].ToString();
            lb6.Text = data.arrAplications[5].ToString();
            lb7.Text = data.arrAplications[6].ToString();
            if (data.mode == "add")
            {
                custom_button1.Text = "Добавить";
                this.Text = "Добавить запись в таблицу Заявки";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString());
                }
                reader.Close();
            }
            else if (data.mode == "edit")
            {
                custom_button1.Text = "Изменить";
                this.Text = "Изменить запись таблицы Заявки";
                SqlDataAdapter sda = new SqlDataAdapter("Select * From Applications where Number_application = '" + data.idIndex + "'", sqlConnection);
                sda.Fill(dt);
                tb1.Text = dt.Rows[0][0].ToString();
                dateTimePicker1.Text = dt.Rows[0][1].ToString();
                tb3.Text = dt.Rows[0][2].ToString();
                tb4.Text = dt.Rows[0][3].ToString();
                tb5.Text = dt.Rows[0][4].ToString();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString());
                }
                reader.Close();
                tb7.Text = dt.Rows[0][6].ToString();
                string selectCombo = dt.Rows[0][5].ToString(); ;
                comboBox1.SelectedItem = selectCombo;
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
            string selectedClientId = comboBox1.SelectedItem.ToString();
            if (custom_button1.Text == "Изменить")
            {
                this.Text = "Изменить запись";
                if ( tb3.Text.ToString() == "" || tb4.Text.ToString() == "" || tb5.Text.ToString() == "" || tb7.Text.ToString() == "")

                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                   
                    SqlCommand cmd = new SqlCommand("UPDATE Applications SET Date_added='" + dateTimePicker1.Value.ToString() + "', Repair_equipment ='" + tb3.Text.ToString() + "', Type_of_malfunction ='" + tb4.Text.ToString() + "', Problem_description ='" + tb5.Text.ToString() + "', Id_client ='" + Int32.Parse(selectedClientId) + "', Status_application ='" + tb7.Text.ToString() + "' WHERE Number_application = '" + data.idIndex + "'", sqlConnection);
                    cmd.ExecuteNonQuery();
                    Form1 form1 = new Form1();
                    MessageBox.Show("Запись изменена");
                    form1.Show();
                    this.Close();
                }
            }

            if (custom_button1.Text == "Добавить")
            {
                if ( tb3.Text.ToString() == "" || tb4.Text.ToString() == "" || tb5.Text.ToString() == "" || tb7.Text.ToString() == "")
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into [Applications] ( Date_added, Repair_equipment, Type_of_malfunction, Problem_description, Id_client, Status_application)" +
                        "values (@Date_added, @Repair_equipment, @Type_of_malfunction, @Problem_description, @Id_client, @Status_application)", sqlConnection);
                    cmd.Parameters.AddWithValue("Date_added", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
                    cmd.Parameters.AddWithValue("Repair_equipment", tb3.Text);
                    cmd.Parameters.AddWithValue("Type_of_malfunction", tb4.Text);
                    cmd.Parameters.AddWithValue("Problem_description", tb5.Text);
                    cmd.Parameters.AddWithValue("@Id_client", selectedClientId);
                    cmd.Parameters.AddWithValue("Status_application", tb7.Text);

                    cmd.ExecuteNonQuery();

                    tb1.Text = "";
                    tb3.Text = "";
                    tb4.Text = "";
                    tb5.Text = "";
                    tb7.Text = "";
                    dateTimePicker1.Text = "";
                    comboBox1.Text = "";

                    MessageBox.Show("Запись добавлена");
                }
            }
        }
    }
}
