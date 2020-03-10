using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace World_Yachts
{
    public partial class Auth : Form
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["World_Yachts.Properties.Settings._16is13ConnectionString"].ConnectionString;
        public Auth()
        {
            InitializeComponent();

        }

        private void BT_Sign_In_Click(object sender, EventArgs e)
        {
            string Login = TB_Login.Text.Trim();
            string Password = TB_Password.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string RoleID = null;
                int Auth_Count = 0;
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT [RoleID] FROM [dbo].[User] WHERE Login = @Login AND Password = @Password";
                command.Connection = connection;

                command.Parameters.Add("@Login", SqlDbType.VarChar);
                command.Parameters["@Login"].Value = Login;

                command.Parameters.Add("@Password", SqlDbType.VarChar);
                command.Parameters["@Password"].Value = Password;

                try
                {
                    connection.Open();
                    RoleID = (string)command.ExecuteScalar();

                    if (Auth_Count <= 2)
                    {

                        if (RoleID.ToString() == "1")
                        {
                            MessageBox.Show("Вы успешно авторизовались как Администратор.");
                            connection.Close();
                            Form Administrator = new Administrator();
                            Administrator.Show();
                            this.Hide();
                        }
                        else if (RoleID != null)
                        {
                            MessageBox.Show("Вы успешно авторизовались.");
                            connection.Close();
                            Form Manager = new Manager();
                            Manager.Show();
                            this.Hide();
                        }
                        else
                        {
                            Auth_Count += 1;
                            MessageBox.Show("Ошибка авторизации");
                            connection.Close();
                        }
                    }
                    else
                    {

                    }


                }
                catch (Exception ex)
                {
                    Auth_Count += 1;
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
        }


    }
}
