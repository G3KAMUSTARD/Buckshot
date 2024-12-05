using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Buckshot
{
    public partial class Auth : Form
    {
        DB db = new DB();
        public Auth()
        {
            InitializeComponent();
            pswrd.UseSystemPasswordChar = true;
            nick.MaxLength = 10;
            pswrd.MaxLength = 6;
        }
        //кнопка входа
        private void Log_in_Click(object sender, EventArgs e)
        {
            var name = nick.Text;
            var psword = pswrd.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string quertystring = $"select id, nick, pswrd from LogIn where nick like '{name}' and pswrd = '{psword}' ";
            SqlCommand command = new SqlCommand(quertystring, db.GetConection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (pswrd.Text.Length < 6)
            {
                MessageBox.Show("Ваш пароль не мог быть короче 6 символов!", $"Уведомляем, что...", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (TimeWork())
            {
                return;
            }
            else if (table.Rows.Count == 1)
            {
                MessageBox.Show("Успешно!", $"Уведомляем, что...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Hide();
                var form2 = new MainForm(name);
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }
            else
            {
                MessageBox.Show("Вы ввели данные неверно, или такого профиля не существует!", $"Уведомляем, что...", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        //кнопка показать или скрыть пароль
        private void Show_Password_CheckedChanged(object sender, EventArgs e)
        {
            if (Pasword_Show.Checked)
            {
                pswrd.UseSystemPasswordChar = true;
            }
            else
            {
                pswrd.UseSystemPasswordChar = false;
            }
        }
        //исключения на некорректные символы пунктуации
        private void nick_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!Char.IsPunctuation(e.KeyChar)) return;
            else
                e.Handled = true;
        }
        private void pswrd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!Char.IsPunctuation(e.KeyChar)) return;
            else
                e.Handled = true;
        }
        private Boolean TimeWork()
        {
            var nickn = nick.Text;
            var vrema = DateTime.Now.TimeOfDay;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            db.OpenConection();
            string queryString = "SELECT * FROM LogIn WHERE nick = @nickn AND (enter > @vrema OR exitt < @vrema)";
            using (SqlCommand command = new SqlCommand(queryString, db.GetConection()))
            {
                command.Parameters.AddWithValue("@nickn", nickn);
                command.Parameters.AddWithValue("@vrema", vrema);
                command.ExecuteNonQuery();
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Ваша смена закончилась или еще не началась!", "Уведомляем, что...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
            {
                return false;
            }
        }
        //кнопка выход из приложения
        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
