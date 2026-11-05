using BusinessLogic;
using Model;

namespace lab1
{
    /// <summary>
    /// Главное окно: таблица записей, поиск и камеры.
    /// </summary>
    public partial class Form1 : Form
    {
        Logic logic = IoC.GetLogic();

        /// <summary>
        /// Открывает окно и загружает записи из базы.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Fill(logic.GetAll());
        }

        void Fill(List<Record> list)
        {
            dataGridView1.Rows.Clear();
            foreach (Record r in list)
            {
                dataGridView1.Rows.Add(r.ID, r.Name, r.Age, r.Date.ToShortDateString(), r.Cause, r.Cell);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecordForm f = new RecordForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (logic.Add(f.name, f.age, f.date, f.cause, f.cell))
                    Fill(logic.GetAll());
                else
                    MessageBox.Show("Камера занята или нет такой (1-" + logic.cells + ")");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выбери запись в таблице");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            Record r = null;
            foreach (Record x in logic.GetAll())
            {
                if (x.ID == id)
                    r = x;
            }
            if (r == null)
            {
                MessageBox.Show("Записи уже нет, обнови таблицу");
                Fill(logic.GetAll());
                return;
            }

            RecordForm f = new RecordForm(r);
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (logic.Edit(id, f.name, f.age, f.date, f.cause, f.cell))
                    Fill(logic.GetAll());
                else
                    MessageBox.Show("Не получилось, проверь камеру");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выбери запись в таблице");
                return;
            }

            if (MessageBox.Show("Точно удалить?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!logic.Delete(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)))
                    MessageBox.Show("Записи уже нет");
                Fill(logic.GetAll());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Record> res = logic.FindByCause(textBox6.Text);
            Fill(res);
            if (res.Count == 0)
                MessageBox.Show("Ничего не найдено");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
            Fill(logic.GetAll());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<int> busy = logic.BusyCells();
            List<int> free = logic.FreeCells();
            label8.Text = "Занято: " + busy.Count + " (" + string.Join(", ", busy) + ")\nСвободно: " + free.Count + " (" + string.Join(", ", free) + ")";
        }
    }
}
