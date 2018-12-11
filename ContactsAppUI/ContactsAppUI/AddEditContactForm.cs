using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    public partial class AddEditContactForm : Form
    {
        //Создаем новый контакт.
        private Contact _newContact = new Contact();

        //Метод, устанавливающий и возвращающий данные о контакте.
        public Contact NewContact
        {
            get { return _newContact; }
            set
            {
                _newContact.Surname = value.Surname;
                _newContact.Name = value.Name;
                _newContact.phoneNumber.Number = value.phoneNumber.Number;
                _newContact.DateOfBirth = value.DateOfBirth;
                _newContact.Email = value.Email;
                _newContact.IdVk = value.IdVk;
            }
        }
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public AddEditContactForm()
        {
            //Метод, вызывающий инициализацию компонентов.
            InitializeComponent();
        }


        /// <summary>
        /// Запись в DialogResult "OK" при нажатии "ОК".
        /// </summary>
        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Запись в DialogResult "Cancel" при нажатии "Cancel".
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Считывает фамилию контакта с TextBox
        /// </summary>
        private void SurnameTextBox_TextChanged(object sender, EventArgs e)
        {
            _newContact.Surname = SurnameTextBox.Text;
        }

        /// <summary>
        /// Считывает имя контакта с TextBox
        /// </summary>
        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            _newContact.Name = NameTextBox.Text;
        }

        /// <summary>
        /// Считывает дату рождения контакта с TextBox
        /// </summary>
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            _newContact.DateOfBirth = BirthdayTimePicker.Value;
        }

        /// <summary>
        /// Считывает номер телефона контакта с TextBox
        /// </summary>
        private void PhoneTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _newContact.phoneNumber.Number = Convert.ToInt64(PhoneTextBox.Text);
            }
            catch (Exception exception)
            {

            }

        }

        /// <summary>
        /// Считывает e-mail контакта с TextBox
        /// </summary>
        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            _newContact.Email = EmailTextBox.Text;
        }

        /// <summary>
        /// Считывает Id vk контакта с TextBox
        /// </summary>
        private void IdVkTextBox_TextChanged(object sender, EventArgs e)
        {
            _newContact.IdVk = IdVkTextBox.Text;
        }

        /// <summary>
        /// Заполняет форму для дальнейшего редактирования данных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditContactForm_Load(object sender, EventArgs e)
        {
            //Если выбран какой-либо контакт.
            if (_newContact.Surname != null)
            {
                //Заполняем форму данными выбранного контакта.
                SurnameTextBox.Text = _newContact.Surname;
                NameTextBox.Text = _newContact.Name;
                BirthdayTimePicker.Value = _newContact.DateOfBirth;
                PhoneTextBox.Text = _newContact.phoneNumber.Number.ToString();
                EmailTextBox.Text = _newContact.Email;
                IdVkTextBox.Text = _newContact.IdVk;
            }

        }
    }
}
