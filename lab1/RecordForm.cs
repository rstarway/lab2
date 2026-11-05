using BusinessLogic;
using Model;

namespace lab1
{
    /// <summary>
    /// Окно карточки записи: ввод и проверка данных.
    /// </summary>
    public partial class RecordForm : Form
    {
        RecordValidator validator = IoC.GetValidator();

        /// <summary>
        /// ФИО после нажатия ОК.
        /// </summary>
        public string name;

        /// <summary>
        /// Возраст после нажатия ОК.
        /// </summary>
        public int age;

        /// <summary>
        /// Дата поступления после нажатия ОК.
        /// </summary>
        public DateTime date;

        /// <summary>
        /// Причина смерти после нажатия ОК.
        /// </summary>
        public string cause;

        /// <summary>
        /// Номер камеры после нажатия ОК.
        /// </summary>
        public int cell;

        /// <summary>
        /// Открывает пустую карточку для новой записи.
        /// </summary>
        public RecordForm()
        {
            InitializeComponent();
            Text = "Новая запись";
        }

        /// <summary>
        /// Открывает карточку с данными записи для изменения.
        /// </summary>
        /// <param name="r">Изменяемая запись</param>
        public RecordForm(Record r)
        {
            InitializeComponent();
            Text = "Изменение записи";
            textBox1.Text = r.Name;
            textBox2.Text = r.Age.ToString();
            dateTimePicker1.Value = r.Date;
            textBox3.Text = r.Cause;
            textBox4.Text = r.Cell.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out age))
            {
                MessageBox.Show("Возраст должен быть числом");
                return;
            }
            if (!int.TryParse(textBox4.Text, out cell))
            {
                MessageBox.Show("Камера должна быть числом");
                return;
            }

            string err = validator.Check(textBox1.Text, age, textBox3.Text);
            if (err != "")
            {
                MessageBox.Show(err);
                return;
            }

            name = textBox1.Text.Trim();
            date = dateTimePicker1.Value.Date;
            cause = textBox3.Text.Trim();
            DialogResult = DialogResult.OK;
        }
    }
}
