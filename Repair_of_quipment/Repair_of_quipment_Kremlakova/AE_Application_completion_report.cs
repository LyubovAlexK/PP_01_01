using System;
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

namespace Repair_of_quipment_Kremlakova
{
    public partial class AE_Application_completion_report : Form
    {
        public string query = "SELECT Number_application FROM Applications";
        public string query1 = "SELECT Id_employee FROM Employee";
        private SqlConnection sqlConnection = null;
        private DataSet dataSet = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private bool newRowAdding = false;
        private SqlCommandBuilder sqlBuilder = null;
        private DataTable dt = new DataTable();
        private DataTable dt2 = new DataTable();
        public AE_Application_completion_report()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-3HK6G3K\SQLEXPRESS;Initial Catalog=Repair_of_quipment;Integrated Security=True;User ID=Iam;Password=111");
            sqlConnection.Open();
            lb1.Text = data.arrApplication_completion_report[0].ToString();
            lb2.Text = data.arrApplication_completion_report[1].ToString();
            lb3.Text = data.arrApplication_completion_report[2].ToString();
            lb4.Text = data.arrApplication_completion_report[3].ToString();
            lb5.Text = data.arrApplication_completion_report[4].ToString();
            lb6.Text = data.arrApplication_completion_report[5].ToString();
            if (data.mode == "add")
            {
                custom_button1.Text = "Добавить";
                this.Text = "Добавить запись в таблицу Отчеты";

                FillComboBox(comboBox1, query);
                FillComboBox(comboBox2, query1);
            }
            else if (data.mode == "edit")
            {
                custom_button1.Text = "Изменить";
                this.Text = "Изменить запись таблицы Отчеты";

                SqlDataAdapter sda = new SqlDataAdapter(
                    "SELECT * FROM Application_completion_report WHERE Number_report = @id", sqlConnection);
                sda.SelectCommand.Parameters.AddWithValue("@id", data.idIndex);
                sda.Fill(dt);

                tb1.Text = dt.Rows[0][0].ToString();
                tb3.Text = dt.Rows[0][2].ToString();
                tb4.Text = dt.Rows[0][3].ToString();
                tb5.Text = dt.Rows[0][4].ToString();

                FillComboBox(comboBox1, query);
                FillComboBox(comboBox2, query1);

                
                string selectedApplication = dt.Rows[0][5].ToString(); 
                comboBox1.SelectedItem = comboBox1.Items.Cast<string>().FirstOrDefault(item => item == selectedApplication);

                string selectedEmployee = dt.Rows[0][1].ToString();
                comboBox2.SelectedItem = comboBox2.Items.Cast<string>().FirstOrDefault(item => item == selectedEmployee);
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
            string selectedNumberApplicationsId = comboBox1.SelectedItem.ToString();
            string selectedEmployeeId = comboBox2.SelectedItem.ToString();
            if (custom_button1.Text == "Изменить")
            {
                this.Text = "Изменить запись";
                if (tb3.Text.ToString() == "" || tb4.Text.ToString() == "" || tb5.Text.ToString() == "")

                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("UPDATE Application_completion_report SET Id_employee='" + Int32.Parse(selectedEmployeeId) + "', Lead_time ='" + tb3.Text.ToString() + "', Repair_materials ='" + tb4.Text.ToString() + "', Price ='" + tb5.Text.ToString() + "', Number_application ='" + Int32.Parse(selectedNumberApplicationsId) + "' WHERE Number_report = '" + data.idIndex + "'", sqlConnection);
                    cmd.ExecuteNonQuery();
                    Form1 form1 = new Form1();
                    MessageBox.Show("Запись изменена");
                    form1.Show();
                    this.Close();
                }
            }

            if (custom_button1.Text == "Добавить")
            {
                if (tb3.Text.ToString() == "" || tb4.Text.ToString() == "" || tb5.Text.ToString() == "")
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into [Application_completion_report] ( Id_employee, Lead_time, Repair_materials, Price, Number_application)" +
                        "values (@Id_employee, @Lead_time, @Repair_materials, @Price, @Number_application)", sqlConnection);
                    cmd.Parameters.AddWithValue("Id_employee", selectedEmployeeId);
                    cmd.Parameters.AddWithValue("Lead_time", tb3.Text);
                    cmd.Parameters.AddWithValue("Repair_materials", tb4.Text);
                    cmd.Parameters.AddWithValue("Price", tb5.Text);
                    cmd.Parameters.AddWithValue("Number_application", selectedNumberApplicationsId);

                    cmd.ExecuteNonQuery();

                    tb1.Text = "";
                    tb3.Text = "";
                    tb4.Text = "";
                    tb5.Text = "";
                    comboBox1.Text = "";
                    comboBox2.Text = "";

                    MessageBox.Show("Запись добавлена");
                }
            }
        }
        private void FillComboBox(ComboBox comboBox, string query)
        {
            comboBox.Items.Clear();
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    comboBox.Items.Add(reader[0].ToString());
                }
            }
        }
    }
}
