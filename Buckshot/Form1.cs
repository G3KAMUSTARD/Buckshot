using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.ComponentModel;

namespace Buckshot
{
    public partial class Form1 : Form
    {

        DB db = new DB();

        enum RowState
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //вывести таблицы на форму
            db.OpenConection();
            string shooters = "select * from Shooters";
            string guns = "select * from Guns";

            SqlDataAdapter adapter1 = new SqlDataAdapter(shooters, db.GetConection());
            DataSet sho = new DataSet();
            adapter1.Fill(sho, "shooters");
            dataGridView1.DataSource = sho.Tables[0];

            SqlDataAdapter adapter2 = new SqlDataAdapter(guns, db.GetConection());
            DataSet gun = new DataSet();
            adapter2.Fill(gun, "Guns");
            dataGridView2.DataSource = gun.Tables[0];
            db.CloseConection();


            //предустановки выбора времени
            gogo.Format = DateTimePickerFormat.Custom;
            gogo.CustomFormat = "yyyy-MM-dd HH:mm";
            endend.Format = DateTimePickerFormat.Custom;
            endend.CustomFormat = "yyyy-MM-dd HH:mm";
            gogo.MinDate = DateTime.Now;
            endend.MinDate = DateTime.Today.AddMinutes(30);

            //позволить выбрать только исправное оружие
            db.OpenConection();
            SqlDataReader dr;
            SqlCommand com = new SqlCommand($"SELECT * FROM Guns WHERE condition LIKE 'Исправен'", db.GetConection());
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                GunsNum.Items.Add(dr["serial_num"]);
            }
            db.CloseConection();

            //позволить выбрать только исправное оружие в окне изменить данные о посетителе
            db.OpenConection();
            SqlDataReader drd;
            SqlCommand coma = new SqlCommand($"SELECT * FROM Guns WHERE condition LIKE 'Исправен'", db.GetConection());
            drd = com.ExecuteReader();

            while (drd.Read())
            {
                GunsNum_Chan.Items.Add(drd["serial_num"]);
            }
            db.CloseConection();
        }

        //автодобавление начала к времени конца
        private void gogo_ValueChanged(object sender, EventArgs e)
        {

            Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += (s, ev) =>
            {
                DateTime selectedDate = gogo.Value;
                DateTime newDate = selectedDate.AddHours(1);
                endend.Value = newDate;
                timer.Stop();
            };
            timer.Start();
        }
        //найти клиента
        private void SearchSho(DataGridView dataGridView1)
        {
            string searchString = $"select * from Shooters where concat (id, name, passport, gogo, endend, serial_num) like '%" + FindShooter.Text + "%'";
            SqlCommand com = new SqlCommand(searchString, db.GetConection());
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "shooters");
            dataGridView1.DataSource = ds.Tables[0];
            db.CloseConection();
        }

        //метод найти клиента
        private void Search_Shooter_Click(object sender, EventArgs e)
        {
            SearchSho(dataGridView1);
        }

        //обновить таблицу клиенты
        private void Update_Shooter_Click(object sender, EventArgs e)
        {
            db.OpenConection();
            string sho = "select * from Shooters";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sho, db.GetConection());
            DataSet ds = new DataSet();
            adapter1.Fill(ds, "shooters");
            dataGridView1.DataSource = ds.Tables[0];
            db.CloseConection();
        }

        //удалить клиента
        private void Delete_Shooter_Click(object sender, EventArgs e)
        {
            {
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                SqlCommand com = new SqlCommand($"DELETE FROM Shooters WHERE id like'{id}'", db.GetConection());

                com.Parameters.AddWithValue("@id", id);
                db.OpenConection();

                DialogResult result = MessageBox.Show
                    ("Вы уверены что хотите удалить данные о клиенте?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Запись удалена. Обновите таблицу.", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    catch
                    {
                        MessageBox.Show("Вы ничего не выбрали!", "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                db.CloseConection();
            }
        }

        //печать клиентов
        private void Print_Shooters_Click(object sender, EventArgs e)
        {
           PrintDocument pd = new PrintDocument();
           pd.PrintPage += new PrintPageEventHandler(PrintPage);
           pd.Print();
        }
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
         Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
         this.dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
         e.Graphics.DrawImage(bm, 0, 0);
         bm.Dispose();
        }

        //найти пушку
        private void SearchGun(DataGridView dataGridView1)
        {
            string searchString = $"select * from Guns where concat (serial_num, model, condition) like '%" + FindGun.Text + "%'";
            SqlCommand com = new SqlCommand(searchString, db.GetConection());
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "guns");
            dataGridView1.DataSource = ds.Tables[0];
            db.CloseConection();
        }

        //метод найти пушку
        private void Search_Gun_Click(object sender, EventArgs e)
        {
            SearchGun(dataGridView2);
        }

        //обновить пушки
        private void Update_Gun_Click(object sender, EventArgs e)
        {
            db.OpenConection();
            string guns = "select * from Guns";
            SqlDataAdapter adapter1 = new SqlDataAdapter(guns, db.GetConection());
            DataSet ds = new DataSet();
            adapter1.Fill(ds, "Guns");
            dataGridView2.DataSource = ds.Tables[0];
            db.CloseConection();
        }

        //удалить пушку
        private void Delete_Gun_Click(object sender, EventArgs e)
        {
            {
                string serial_num = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                SqlCommand com = new SqlCommand($"DELETE FROM Guns WHERE serial_num like'{serial_num}'", db.GetConection());

                com.Parameters.AddWithValue("@serial_num", serial_num);
                db.OpenConection();

                DialogResult result = MessageBox.Show
                    ("Вы уверены что хотите удалить данные об оружии?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Запись удалена. Обновите таблицу.", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    catch
                    {
                        MessageBox.Show("Вы ничего не выбрали!", "Возникла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                db.CloseConection();
            }
        }

        //печать пушек
        private void Print_Guns_Click(object sender, EventArgs e)
        {
          PrintDocument pd = new PrintDocument();
          pd.PrintPage += new PrintPageEventHandler(PrintPageForGuns);
          pd.Print();
        }

        private void PrintPageForGuns(object sender, PrintPageEventArgs e)
        {
        Bitmap bm = new Bitmap(this.dataGridView2.Width, this.dataGridView2.Height);
        this.dataGridView2.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView2.Width, this.dataGridView2.Height));
        e.Graphics.DrawImage(bm, 0, 0);
        bm.Dispose();
        }

        //добавить клиента
        private void Add_Sho_Click(object sender, EventArgs e)
        {
            if (name.Text == "")
            {
                MessageBox.Show("Вы не ввели ФИО клиента!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (passport.Text.Length < 12)
            {
                MessageBox.Show("Вы не до конца ввели номер и серию паспорта", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (GunsNum.Text == "")
            {
                MessageBox.Show("Вы не выбрали оружие!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                db.OpenConection();
                SqlCommand cmd;
                SqlCommand iden;
                cmd = new SqlCommand("INSERT INTO Shooters ( name, passport, gogo, endend, serial_num)  " +
                    "VALUES ( @name, @passport, @gogo, @endend, @serial_num)", db.GetConection());
                iden = new SqlCommand("SET IDENTITY_INSERT dbo.Shooters ON", db.GetConection());
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@passport", passport.Text);
                cmd.Parameters.AddWithValue("@gogo", gogo.Value);
                cmd.Parameters.AddWithValue("@endend", endend.Value);
                cmd.Parameters.AddWithValue("@serial_num", GunsNum.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Успешно", "Клиент был(а) добавлен(a) в БД!");
                db.CloseConection();
            }
        }
        //очистить поля добавления клиента
        private void Сlear_Sho_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            name.Text = "";
            name.Clear();
            passport.Text = "";
            passport.Clear();
            gogo.Value = now;
            endend.Value = now;
            GunsNum.SelectedIndex = -1;
        }

        private void Add_Gun_Click(object sender, EventArgs e)
        {
            {
                if (serial_num.Text == "")
                {
                    MessageBox.Show("Вы не ввели серийный номер оружия!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else if (model.Text == "")
                {
                    MessageBox.Show("Вы не ввели модель оружия!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else if (condition.SelectedIndex == -1)
                {
                    MessageBox.Show("Вы не выбрали состояние оружия!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

                else if (condition.SelectedIndex == 1)
                {
                    MessageBox.Show("Раз в год и палка стреляет?", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    db.OpenConection();
                    SqlCommand cmd;
                    SqlCommand iden;
                    cmd = new SqlCommand("INSERT INTO Guns (serial_num, model, condition )  " +
                        "VALUES ( @serial_num, @model, @condition )", db.GetConection());
                    iden = new SqlCommand("SET IDENTITY_INSERT dbo.Shooters ON", db.GetConection());
                    cmd.Parameters.AddWithValue("@serial_num", serial_num.Text);
                    cmd.Parameters.AddWithValue("@model", model.Text);
                    cmd.Parameters.AddWithValue("@condition", condition.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Успешно", "Оружие было добавлено в БД!");
                    db.CloseConection();
                }
            }
        }

        //Очистить поля добавить пушку
        private void Clear_gun_Click(object sender, EventArgs e)
        {
            serial_num.Text = "";
            serial_num.Clear();
            model.Text = "";
            model.Clear();
            condition.SelectedIndex = -1;
        }

        //изменить клиента
        private void Sho_chan_Click(object sender, EventArgs e)
        {

            if (name_chan.Text == "")
            {
                MessageBox.Show("Чтобы изменить данные, кликните 2  раза на строку в таблице", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (passport_chan.Text.Length < 12)
            {
                MessageBox.Show("Вы не до конца ввели номер телефона", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (GunsNum_Chan.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали пол!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                db.OpenConection();
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE Shooters " +
                "SET  name = @name ,passport = @passport , gogo = @gogo, endend = @endend, serial_num = @serial_num " +
                "WHERE passport like '%" + passport_chan.Text + "%'", db.GetConection());
                cmd.Parameters.AddWithValue("@name", name_chan.Text);
                cmd.Parameters.AddWithValue("@passport", passport_chan.Text);
                cmd.Parameters.AddWithValue("@gogo", gogo_chan.Value);
                cmd.Parameters.AddWithValue("@endend", endend_chan.Value);
                cmd.Parameters.AddWithValue("@serial_num", GunsNum_Chan.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Успешно", "Данные о посетителе были обновлены!");
                db.CloseConection();
            }
        }

        //автоподставка из таблицы клиентов
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[a];
                name_chan.Text = row.Cells[1].Value.ToString();
                passport_chan.Text = row.Cells[2].Value.ToString();
                gogo_chan.Text = row.Cells[3].Value.ToString();
                endend_chan.Text = row.Cells[4].Value.ToString();
                GunsNum_Chan.Text = row.Cells[5].Value.ToString();
            }
        }

        //очистить поля в изменить клиента
        private void clean_Sho_chan_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            name_chan.Text = "";
            name_chan.Clear();
            passport_chan.Text = "";
            passport_chan.Clear();
            gogo_chan.Value = now;
            endend_chan.Value = now;
            GunsNum_Chan.SelectedIndex = -1;
        }

        //автоподставка данных из таблицы пушек
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[a];
                serial_num_chan.Text = row.Cells[0].Value.ToString();
                model_chan.Text = row.Cells[1].Value.ToString();
                condition_chan.Text = row.Cells[2].Value.ToString();
            }
        }

        //изменить данные о пушке
        private void chan_gun_Click(object sender, EventArgs e)
        {
            if (serial_num_chan.Text == "")
            {
                MessageBox.Show("Чтобы изменить данные, кликните 2  раза на строку в таблице", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (condition_chan.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали состояние оружия!", $"ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                db.OpenConection();
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE Guns " +
                "SET  serial_num = @serial_num  ,model = @model , condition = @condition WHERE serial_num like '%" + serial_num_chan.Text + "%'", db.GetConection());
                cmd.Parameters.AddWithValue("@serial_num", serial_num_chan.Text);
                cmd.Parameters.AddWithValue("@model", model_chan.Text);
                cmd.Parameters.AddWithValue("@condition", condition_chan.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Успешно", "Данные о посетителе были обновлены!");
                db.CloseConection();
            }
        }

        //очистить поля в изменить пушку
        private void clear_cun_Click(object sender, EventArgs e)
        {
            serial_num_chan.Text = "";
            serial_num_chan.Clear();
            model_chan.Text = "";
            model_chan.Clear();
            condition_chan.SelectedIndex = -1;
        }

        //исключения на некорректные символы
        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void name_chan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;

            }
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }
        private void serial_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;

            }
        }
        private void model_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;

            }
        }
    }
}
