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

namespace Repair_of_quipment_Kremlakova
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private DataSet dataSet = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private bool newRowAdding = false;
        private SqlCommandBuilder sqlBuilder = null;
        private DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public static class data
        {
            public static int buf=1;
            public static String idIndex = "";
            public static String idLastIndex;
            public static String reg = "Начало";
            public static String mode = "";
            public static String[] arrNameTable = new string[] { "Таблица Заявки", "Таблица Клиенты", "Таблица Отчеты" , "Таблица Сотрудники" , "Таблица Пользователи"};
            public static String[] arrAplications = new string[] { "Код заявки", "Дата добавления", "Оборудование для ремонта", "Тип неисправности", "Описание проблемы", "Код клиента", "Статус заявки" };
            public static String[] arrClients = new string[] { "Код клиента", "Имя", "Фамилия", "Отчество", "Телефон" };
            public static String[] arrApplication_completion_report = new string[] { "Код отчета", "Код мастера", "Время выполнения", "Материалы для ремонта", "Стоимость", "Код заявки" };
            public static String[] arrEmployee = new string[] { "Код сотрудника", "Имя", "Фамилия", "Отчество", "Телефон","Должность" };
            public static String[] arrUsers = new string[] { "Код пользователя", "Логин", "Пароль", "Роль" };
            public static int exit = 0;
        }
        public Form1()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-3HK6G3K\SQLEXPRESS;Initial Catalog=Repair_of_quipment;Integrated Security=True;User ID=Iam;Password=111");
            sqlConnection.Open();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (data.exit > 0)
            {
                title_table.Text = data.arrNameTable[data.buf-1];
                Load_Table(data.buf);
                table_panel.Visible = true;
                regist_panel.Visible = false;
            }
            if (data.exit==0)
            {
                this.applicationsTableAdapter.Fill(this.repair_of_quipmentDataSet.Applications);
                dataGridView1.DataSource = this.repair_of_quipmentDataSet.Applications;
                Load_Table(data.buf);
                title_table.Text = data.arrNameTable[0];
                table_panel.Visible = false;
                regist_panel.Visible = true;
                custom_button_menu1.Visible = false;
                custom_button_menu2.Visible = false;
                custom_button_menu3.Visible = false;
                custom_button_menu4.Visible = false;
                custom_button_menu5.Visible = false;
                custom_button_menu6.Visible = false;
                label2.Visible = false;
                label1.Visible = false;
                tb_login.Text = "";
                tb_password.Text = "";
            }
            if (regist_panel.Visible = true) data.exit++;
        }

        private void custom_button_menu1_Click(object sender, EventArgs e)
        {
            data.buf = 1;
            Load_Table(data.buf);
            title_table.Text = data.arrNameTable[0];
            table_panel.Visible = true;
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
                Application.Exit();
        }

        private void custom_button_menu2_Click(object sender, EventArgs e)
        {
            data.buf = 2;
            Load_Table(data.buf);
            title_table.Text = data.arrNameTable[1];
            table_panel.Visible = true;
        }

        private void custom_button_menu3_Click(object sender, EventArgs e)
        {
            data.buf = 3;
            Load_Table(data.buf);
            title_table.Text = data.arrNameTable[2];
            table_panel.Visible = true;
        }

        private void custom_button_menu4_Click(object sender, EventArgs e)
        {
            data.buf = 4;
            Load_Table(data.buf);
            title_table.Text = data.arrNameTable[3];
            table_panel.Visible = true; ;
        }

        private void custom_button_menu5_Click(object sender, EventArgs e)
        {
            data.buf = 5;
            Load_Table(data.buf);
            title_table.Text = data.arrNameTable[4];
            table_panel.Visible = true;
        }
        private void custom_button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3HK6G3K\SQLEXPRESS;Initial Catalog=Repair_of_quipment;Integrated Security=True;User ID=Iam;Password=111");
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select role From Users where login = '" + tb_login.Text + "' and password ='" + tb_password.Text + "'", con);
            sda.Fill(dt);
            try
            {

                if (dt.Rows[0][0].ToString() != "0")
                {
                    if (dt.Rows[0][0].ToString().StartsWith("Администратор"))
                    {
                        table_panel.Visible = true;
                        regist_panel.Visible = false;
                        custom_button_menu1.Visible = true;
                        custom_button_menu2.Visible = true;
                        custom_button_menu3.Visible = true;
                        custom_button_menu4.Visible = true;
                        custom_button_menu5.Visible = true;
                        custom_button_menu6.Visible = true;
                        label2.Visible = true;
                        label1.Visible = true;
                    }
                    else
                    {
                        data.reg = "Мастер";
                        table_panel.Visible = true;
                        regist_panel.Visible = false;
                        custom_button_menu1.Visible = true;
                        custom_button_menu2.Visible = true;
                        custom_button_menu3.Visible = true;
                        custom_button_menu4.Visible = true;
                        custom_button_menu5.Visible = false;
                        custom_button_menu6.Visible = false;
                        label2.Visible = true;
                        label1.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пожалуста введите логин и пароль! Или введите корректные данные", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Load_Table(int x)
        {
            switch (x)
            {
                case 0:
                    {
                        this.application_completion_reportTableAdapter.Fill(this.repair_of_quipmentDataSet.Application_completion_report);
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = repair_of_quipmentDataSet.Application_completion_report;
                    }
                    break;
                case 1:
                    {
                        this.applicationsTableAdapter.Fill(this.repair_of_quipmentDataSet.Applications);
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = repair_of_quipmentDataSet.Applications;
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[0].HeaderText = data.arrAplications[0];
                        dataGridView1.Columns[1].Width = 150;
                        dataGridView1.Columns[1].HeaderText = data.arrAplications[1];
                        dataGridView1.Columns[2].Width = 220;
                        dataGridView1.Columns[2].HeaderText = data.arrAplications[2];
                        dataGridView1.Columns[3].Width = 150;
                        dataGridView1.Columns[3].HeaderText = data.arrAplications[3];
                        dataGridView1.Columns[4].Width = 150;
                        dataGridView1.Columns[4].HeaderText = data.arrAplications[4];
                        dataGridView1.Columns[5].Width = 150;
                        dataGridView1.Columns[5].HeaderText = data.arrAplications[5];
                        dataGridView1.Columns[6].Width = 150;
                        dataGridView1.Columns[6].HeaderText = data.arrAplications[6];
                    }
                    break;
                case 2:
                    {
                        this.clientsTableAdapter.Fill(this.repair_of_quipmentDataSet.Clients);
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = repair_of_quipmentDataSet.Clients;
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[0].HeaderText = data.arrClients[0];
                        dataGridView1.Columns[1].Width = 150;
                        dataGridView1.Columns[1].HeaderText = data.arrClients[1];
                        dataGridView1.Columns[2].Width = 150;
                        dataGridView1.Columns[2].HeaderText = data.arrClients[2];
                        dataGridView1.Columns[3].Width = 150;
                        dataGridView1.Columns[3].HeaderText = data.arrClients[3];
                        dataGridView1.Columns[4].Width = 150;
                        dataGridView1.Columns[4].HeaderText = data.arrClients[4];
                    }
                    break;
                case 3:
                    {
                        this.application_completion_reportTableAdapter.Fill(this.repair_of_quipmentDataSet.Application_completion_report);
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = repair_of_quipmentDataSet.Application_completion_report;
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[0].HeaderText = data.arrApplication_completion_report[0];
                        dataGridView1.Columns[1].Width = 150;
                        dataGridView1.Columns[1].HeaderText = data.arrApplication_completion_report[1];
                        dataGridView1.Columns[2].Width = 150;
                        dataGridView1.Columns[2].HeaderText = data.arrApplication_completion_report[2];
                        dataGridView1.Columns[3].Width = 220;
                        dataGridView1.Columns[3].HeaderText = data.arrApplication_completion_report[3];
                        dataGridView1.Columns[4].Width = 150;
                        dataGridView1.Columns[4].HeaderText = data.arrApplication_completion_report[4];
                        dataGridView1.Columns[5].Width = 150;
                        dataGridView1.Columns[5].HeaderText = data.arrApplication_completion_report[5];
                    }
                    break;
                case 4:
                    {
                        this.employeeTableAdapter.Fill(this.repair_of_quipmentDataSet.Employee);
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = repair_of_quipmentDataSet.Employee;
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[0].HeaderText = data.arrEmployee[0];
                        dataGridView1.Columns[1].Width = 150;
                        dataGridView1.Columns[1].HeaderText = data.arrEmployee[1];
                        dataGridView1.Columns[2].Width = 150;
                        dataGridView1.Columns[2].HeaderText = data.arrEmployee[2];
                        dataGridView1.Columns[3].Width = 150;
                        dataGridView1.Columns[3].HeaderText = data.arrEmployee[3];
                        dataGridView1.Columns[4].Width = 150;
                        dataGridView1.Columns[4].HeaderText = data.arrEmployee[4];
                        dataGridView1.Columns[5].Width = 150;
                        dataGridView1.Columns[5].HeaderText = data.arrEmployee[5];
                    }
                    break;
                case 5:
                    {
                        this.usersTableAdapter.Fill(this.repair_of_quipmentDataSet.Users);
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = repair_of_quipmentDataSet.Users;
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[0].HeaderText = data.arrUsers[0];
                        dataGridView1.Columns[1].Width = 150;
                        dataGridView1.Columns[1].HeaderText = data.arrUsers[1];
                        dataGridView1.Columns[2].Width = 150;
                        dataGridView1.Columns[2].HeaderText = data.arrUsers[2];
                        dataGridView1.Columns[3].Width = 300;
                        dataGridView1.Columns[3].HeaderText = data.arrUsers[3];
                    }
                    break;
                default:
                    {
                        MessageBox.Show("Ошибка при подключении к базе данных!");
                    }
                    break;
            }
        }

        private void tb_login_TextChanged(object sender, EventArgs e)
        {
            int s = 0;
            if (Int32.TryParse(tb_login.Text, out s))
            {
                errorProvider1.SetError(tb_login, "Некорректные данные!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void tb_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            e.Handled = !Char.IsDigit(ch) && !Char.IsControl(ch);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String str = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            String idIndex = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            data.idIndex = idIndex;
        }

        private void del_btn_Click(object sender, EventArgs e)
        {
            switch (data.buf)
            {
                case 1:
                    {
                        if (data.idIndex != "")
                        {
                            DialogResult dialogResult = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                SqlCommand cmd = new SqlCommand("DELETE Applications WHERE Number_application = '" + data.idIndex + "'", sqlConnection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Запись удалена");

                                this.applicationsTableAdapter.Fill(this.repair_of_quipmentDataSet.Applications);
                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите запись!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case 2:
                    {
                        if (data.idIndex != "")
                        {
                            DialogResult dialogResult = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                SqlCommand cmd = new SqlCommand("DELETE Clients WHERE Id_client = '" + data.idIndex + "'", sqlConnection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Запись удалена");

                                this.clientsTableAdapter.Fill(this.repair_of_quipmentDataSet.Clients);
                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите запись!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case 3:
                    {
                        if (data.idIndex != "")
                        {
                            DialogResult dialogResult = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                SqlCommand cmd = new SqlCommand("DELETE Application_completion_report WHERE Number_report = '" + data.idIndex + "'", sqlConnection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Запись удалена");

                                this.application_completion_reportTableAdapter.Fill(this.repair_of_quipmentDataSet.Application_completion_report);
                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите запись!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case 4:
                    {
                        if (data.idIndex != "")
                        {
                            DialogResult dialogResult = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                SqlCommand cmd = new SqlCommand("DELETE Employee WHERE Id_employee = '" + data.idIndex + "'", sqlConnection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Запись удалена");

                                this.employeeTableAdapter.Fill(this.repair_of_quipmentDataSet.Employee);
                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите запись!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case 5:
                    {
                        if (data.idIndex != "")
                        {
                            DialogResult dialogResult = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                SqlCommand cmd = new SqlCommand("DELETE Users WHERE Id_user = '" + data.idIndex + "'", sqlConnection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Запись удалена");

                                this.usersTableAdapter.Fill(this.repair_of_quipmentDataSet.Users);
                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите запись!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                default:
                    {
                        MessageBox.Show("Ошибка при подключении к базе данных!");
                    }
                    break;
            }
            
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            data.mode = "add";
            switch (data.buf)
            {
                case 1:
                    {
                        AE_Applications form1 = new AE_Applications();
                        form1.Show();
                        this.Hide();
                    }break;
                case 2:
                    {
                        AE_Clients form1 = new AE_Clients();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                case 3:
                    {
                        AE_Application_completion_report form1 = new AE_Application_completion_report();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                case 4:
                    {
                        AE_Employee form1 = new AE_Employee();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                case 5:
                    {
                        AE_Users form1 = new AE_Users();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                default:
                    {
                        MessageBox.Show("Ошибка при подключении к базе данных!");
                    }
                    break;
            }
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            data.mode = "edit";
            switch (data.buf)
            {
                case 1:
                    {
                        AE_Applications form1 = new AE_Applications();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                case 2:
                    {
                        AE_Clients form1 = new AE_Clients();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                case 3:
                    {
                        AE_Application_completion_report form1 = new AE_Application_completion_report();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                case 4:
                    {
                        AE_Employee form1 = new AE_Employee();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                case 5:
                    {
                        AE_Users form1 = new AE_Users();
                        form1.Show();
                        this.Hide();
                    }
                    break;
                default:
                    {
                        MessageBox.Show("Ошибка при подключении к базе данных!");
                    }
                    break;
            }
        }

        private void custom_button_menu6_Click(object sender, EventArgs e)
        {
            string a, b, c;
            statistic_panel.Visible = true;
            table_panel.Visible = false;
            regist_panel.Visible = false;

            string connectionString = "Data Source=DESKTOP-3HK6G3K\\SQLEXPRESS;Initial Catalog=Repair_of_quipment;Integrated Security=True;User ID=Iam;Password=111";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Количество выполненных заданий
                string query1 = "SELECT COUNT(*) FROM [Application_completion_report]";
                SqlCommand cmd1 = new SqlCommand(query1, connection);
                a = cmd1.ExecuteScalar().ToString();

                // Среднее время выполненных заявок
                string query2 = @"SELECT AVG(TRY_CAST(Lead_time AS FLOAT)) FROM [Application_completion_report] WHERE ISNUMERIC(Lead_time) = 1";

                SqlCommand cmd2 = new SqlCommand(query2, connection);
                object avgTime = cmd2.ExecuteScalar();
                b = avgTime != DBNull.Value ? Math.Round(Convert.ToDouble(avgTime), 2).ToString() : "0";

                string query3 = "SELECT COUNT(*) FROM Applications WHERE RTRIM(LTRIM(Type_of_malfunction)) = 'Электрическая'";
                SqlCommand cmd3 = new SqlCommand(query3, connection);
                c = cmd3.ExecuteScalar().ToString();
                lb1.Text = "Количество заполненных заявок: "+a;
                lb2.Text = "Среднее время выполнения заявок: "+b;
                lb3.Text = "Количество электрических неисправностей: "+c+" в часах";

                connection.Close();
            }
        }
    }
}
