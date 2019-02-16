using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int  rownum= 1;
        int columnnum = 1;
        int countQ=0;
        string pust = null;
        bool flag = true; //флаг колличества команд
        bool flag2 = false;
        string yQ;
        string[] pravo = new string[10] ;
        int z = 0;
        bool ytechka = false;


        Dictionary<int,string> subjects = new Dictionary<int,string>(); //  ключ номер строки таблицы
        Dictionary<int, string> objects = new Dictionary<int, string>(); //  ключ номер столбца таблицы
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.TopLeftHeaderCell.Value = "Субъекты/Объекты";

            dataGridView1.Visible =false ;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox1.Text=="")
            {
                MessageBox.Show ("Введите субъект!");
                return;
            }
            if (flag2 == false)
            {
                MessageBox.Show("Необходимо создать команду!");
                return;
            }
            subjects.Add(rownum,textBox1.Text);
            dataGridView1.RowCount = rownum + 1;
            dataGridView1.Rows[rownum-1].HeaderCell.Value = textBox1.Text;
            rownum = rownum + 1;
            textBox9.AppendText("CS[" + textBox1.Text + "];");
            objects.Add(columnnum, textBox1.Text);
            dataGridView1.ColumnCount = columnnum + 1;
            dataGridView1.Columns[columnnum - 1].HeaderText = textBox1.Text;
            columnnum = columnnum + 1;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите объект!");
                return;
            }
            if (flag2 == false)
            {
                MessageBox.Show("Необходимо создать команду!");
                return;
            }
            objects.Add(columnnum,textBox2.Text);
            dataGridView1.ColumnCount = columnnum + 1;
            dataGridView1.Columns[columnnum - 1].HeaderText = textBox2.Text;
            columnnum = columnnum + 1;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            textBox9.AppendText("CO[" + textBox2.Text + "];");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (countQ == 0)
            {
                MessageBox.Show("Не задано ни одной команды!");
                return;
            }
            dataGridView1.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            flag2 = true;

            if (rownum==1) {
            textBox9.Text = textBox9.Text + 'Q' + countQ+':';
            countQ = countQ + 1;
        }
        else
            {
            textBox9.Text = textBox9.Text + Environment.NewLine;
            textBox9.Text = textBox9.Text + 'Q' + countQ + ':';
            countQ = countQ + 1;

          }
        flag = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                MessageBox.Show("Введите право!");
                return;
            }

            if (textBox6.Text == "")
            {
                MessageBox.Show("Введите субъект!");
                return;
            }

            if (textBox5.Text == "")
            {
                MessageBox.Show("Введите объект!");
                return;
            }
            int cl = 0, rw = 0;
            string rule = null;

            if (flag2 == false)
            {
                MessageBox.Show("Необходимо создать команду!");
                return;
            }

            if (flag == true)
            {
                textBox10.AppendText('Q' + (countQ-1).ToString() + ";");
                flag = false;
            }

            ICollection<int> okeys = objects.Keys;
            ICollection<int> skeys = subjects.Keys;
            foreach (int j in skeys)
            {

                if (subjects[j] == textBox6.Text)
                {

                    rw = j - 1;
                    break;
                }

            }

            foreach (int j in okeys)
            {

                if (objects[j] == textBox5.Text)
                {

                    cl = j - 1;
                    break;
                }
            }

            rule = ' ' + rule + dataGridView1[cl, rw].Value + textBox11.Text + ' ';
            dataGridView1[cl, rw].Value = rule;
            textBox9.AppendText("ER[" + textBox11.Text + ';' + textBox6.Text + ';' + textBox5.Text + "];");
            if (ytechka == false)
            {
            int colcount, rowcount;
            colcount = Int32.Parse(dataGridView1.ColumnCount.ToString());
            rowcount = Int32.Parse(dataGridView1.RowCount.ToString());
            for (int i = 0; i <= rowcount - 1; i++)
            {
                for (int j = 0; j <= colcount - 1; j++)
                {
                    if (dataGridView1[j, i].Value != null)
                    {
                        string prov = dataGridView1[j, i].Value.ToString();
                        if (prov == " w " || prov == " r " || prov == " w r " || prov == " r w ")
                        {


                            for (int m = 0; m < colcount - 1; m++)
                            {

                                if (dataGridView1[m, j].Value != null)
                                {
                                    if (dataGridView1[m, i].Value == null || dataGridView1[m, j].Value.ToString() != dataGridView1[m, i].Value.ToString())   //объекты не сравнивают
                                    {
                                        
                                            yQ = countQ.ToString();
                                            pravo[z]="ER[" + textBox11.Text + ';' + textBox6.Text + ';' + textBox5.Text + "];";
                                            ytechka = true;
                                            return;
                                        
                                    }
                                }
                            }
                            for (int m = 0; m < colcount - 1; m++)
                            {

                                if (dataGridView1[m, i].Value != null & m != j)
                                {
                                    if (dataGridView1[m, j].Value == null || dataGridView1[m, j].Value.ToString() != dataGridView1[m, i].Value.ToString())   //объекты не сравнивают
                                    {
                                        
                                            yQ = countQ.ToString();
                                            pravo[z]="ER[" + textBox11.Text + ';' + textBox6.Text + ';' + textBox5.Text + "];";
                                            ytechka = true;
                                            return;
                                        
                                    }
                                }
                            }
                        }

                    }
                }
            }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox12.Text == "")
            {
                MessageBox.Show("Введите право!");
                return;
            }
            if (textBox7.Text == "")
            {
                MessageBox.Show("Введите субъект!");
                return;
            }
            if (textBox8.Text == "")
            {
                MessageBox.Show("Введите объект!");
                return;
            }
            string value= null;
            if (flag2 == false)
            {
                MessageBox.Show("Необходимо создать команду!");
                return;
            }

          
            
            int cl = 0, rw = 0;
           
            ICollection<int> okeys = objects.Keys;
            ICollection<int> skeys = subjects.Keys;
            foreach (int j in skeys)
            {

                if (subjects[j] == textBox7.Text)
                {

                    rw = j-1;
                    break;
                }

            }

            foreach (int j in okeys)
            {

                if (objects[j] == textBox8.Text)
                {

                    cl = j-1;
                    break;
                }
            }

            if (dataGridView1[cl, rw].Value != null)
            {
                value = value + dataGridView1[cl, rw].Value;
                dataGridView1[cl, rw].Value = pust;
                value = value.Remove(value.IndexOf(textBox12.Text), 1);
                dataGridView1[cl, rw].Value = value;
                textBox9.AppendText("DR[" + textBox12.Text + ';' + textBox7.Text + ';' + textBox8.Text + "];");
            }
            else
            {
                MessageBox.Show("Заданная ячейка пуста!");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(yQ) == 0)
            {

                MessageBox.Show("Состояние системы безопасно!");
                return;
            }
            textBox10.Text = textBox10.Text.Substring(textBox10.Text.IndexOf(yQ)-1); 

                MessageBox.Show("Утечка права в команде Q" +(Convert.ToInt32(yQ)-1).ToString() + " при внесении права " + pravo[z]);
                return;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Введите объект!");
                return;
            }
            if (flag2 == false)
            {
                MessageBox.Show("Необходимо создать команду!");
                return;
            }
            int delcol = 0, colcount;
            bool nashel = false;
            if (flag2 == false)
            {
                MessageBox.Show("Необходимо создать команду!");
                return;
            }
            colcount = Int32.Parse(dataGridView1.ColumnCount.ToString());
            ICollection<int> okeys = objects.Keys;
            foreach (int j in okeys)
            {

                if (objects[j] == textBox4.Text)
                {

                    delcol = j - 1;
                    nashel = true;
                    break;
                }

            }

            if (nashel == false)
            {
                MessageBox.Show("Введенный объект " + textBox4.Text + " не существует !");
                return;
            }

            dataGridView1.Columns.RemoveAt(delcol);
            delcol = delcol + 1;
            objects.Remove(delcol);

            for (int m = 1; m < colcount; m++)
            {

                if (m == delcol + 1)
                {
                    string value = objects[m];
                    objects.Remove(m);
                    objects.Add(m - 1, value);
                    delcol = delcol + 1;

                }
            }

            columnnum = columnnum - 1;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Введите субъект!");
                return;
            }
            int delrow=0 ,rowcount;
            bool nashel = false;
            if (flag2 == false)
            {
                MessageBox.Show("Необходимо создать команду!");
                return;
            }
            rowcount = Int32.Parse(dataGridView1.RowCount.ToString());
            ICollection<int> skeys = subjects.Keys;
            foreach (int j in skeys)
            {

                if (subjects[j] == textBox3.Text)
                {

                    delrow = j-1;
                    nashel = true;
                    break;
                }

            }

            if (nashel == false)
            {
                MessageBox.Show("Введенный субъект " + textBox3.Text + " не существует !");
                return;
            }

                dataGridView1.Rows.RemoveAt(delrow);
                delrow=delrow+1;
                subjects.Remove(delrow);

                for (int m = 1; m < rowcount; m++)
                {

                    if (m == delrow + 1)
                    {
                        string value = subjects[m];
                        subjects.Remove(m);
                        subjects.Add(m - 1, value);
                        delrow = delrow + 1;
                        
                    }
                }

                rownum = rownum - 1;



              int delcol = 0, colcount;
          
            colcount = Int32.Parse(dataGridView1.ColumnCount.ToString());
            ICollection<int> okeys = objects.Keys;
            foreach (int j in okeys)
            {

                if (objects[j] == textBox3.Text)
                {

                    delcol = j - 1;
                    break;
                }

            }

            dataGridView1.Columns.RemoveAt(delcol);
            delcol = delcol + 1;
            objects.Remove(delcol);

            for (int m = 1; m < colcount; m++)
            {

                if (m == delcol + 1)
                {
                    string value = objects[m];
                    objects.Remove(m);
                    objects.Add(m - 1, value);
                    delcol = delcol + 1;

                }
            }

            columnnum = columnnum - 1;
        
        }
        }

       
    }

