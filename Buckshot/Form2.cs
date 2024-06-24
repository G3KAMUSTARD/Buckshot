using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Buckshot
{
    public partial class Form2 : Form
    {
        DB db = new DB();
        public Form2()
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

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Успех!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Hide();
                var form1 = new Form1();
                form1.Closed += (s, args) => this.Close();
                form1.Show();


            }
            else if (pswrd.Text.Length < 6)
            {
                MessageBox.Show("Ваш пароль не мог быть короче 6 символов!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Вы ввели данные неверно, или такого профиля не существует!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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

        //кнопка выход из приложения
        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
