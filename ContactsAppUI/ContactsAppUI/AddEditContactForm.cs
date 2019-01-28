using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    public partial class AddEditContactForm : Form
    {
        //Создаем новый контакт.
        private Contact _сontact = new Contact();

        //Строка, в которую записывается текст ошибки.
        private string _textException;

        //Флаг, в котором хранится верно ли заполнено поле Surname.
        private bool buttonOKisAvailableToClick_Surname = false;
        private bool buttonOKisAvailableToClick_Name = false;
        private bool buttonOKisAvailableToClick_Birthday = false;
        private bool buttonOKisAvailableToClick_Phone = false;
        private bool buttonOKisAvailableToClick_Email = false;
        private bool buttonOKisAvailableToClick_IdVk = false;

        //Метод, устанавливающий и возвращающий данные о контакте.
        public Contact Contact
        {
            get { return _сontact; }
            set
            {
                _сontact.Surname = value.Surname;
                _сontact.Name = value.Name;
                _сontact.phoneNumber.Number = value.phoneNumber.Number;
                _сontact.DateOfBirth = value.DateOfBirth;
                _сontact.Email = value.Email;
                _сontact.IdVk = value.IdVk;
            }
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public AddEditContactForm()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// Запись в DialogResult "OK" при нажатии "ОК".
        /// </summary>
        private void OkButton_Click(object sender, EventArgs e)
        {
            //Если все поля заполнены верно, то разрешаем нажатие кнопки ОК.
            if (buttonOKisAvailableToClick_Surname && buttonOKisAvailableToClick_Name &&
                buttonOKisAvailableToClick_Birthday && buttonOKisAvailableToClick_Phone &&
                buttonOKisAvailableToClick_Email && buttonOKisAvailableToClick_IdVk == true) 
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }

            //Иначе выводим сообщение об ошибке и кнопка не активна.
            else
            {
                OkButton.Enabled = false;
                ToolTip okToolTip =  new ToolTip();

                okToolTip.Show("Заполните форму корректными значениями, чтобы добавить контакт.", OkButton,
                    (Point) (SurnameTextBox.Size + new Size(-500, 10)), 5000);


                //Разрешаем кнопке реагировать на нажатия.
                OkButton.Enabled = true;
            }
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
            int countException = 0;
            ToolTip surnameToolTip = new ToolTip();

            try
            {
                _сontact.Surname = SurnameTextBox.Text;

                //Разрешаем нажатие кнопки Ок из TextBox surname.
                buttonOKisAvailableToClick_Surname = true;
            }
            catch (Exception exception)
            {
                _textException = exception.Message;
                countException++;

                //Переключаем флаг в false и запрещаем нажимать кнопку ОК.
                buttonOKisAvailableToClick_Surname = false;

            }

            //Если произошла ошибка.
            if (countException != 0)
            {
                SurnameTextBox.BackColor = Color.LightSalmon;
                surnameToolTip.Show(_textException, SurnameTextBox,
                    (Point)(SurnameTextBox.Size + new Size(-400, 10)), 5000);
            }
            else
            {
                SurnameTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Считывает имя контакта с TextBox
        /// </summary>
        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            int countException = 0;
            ToolTip nameToolTip= new ToolTip();

            try
            {
                _сontact.Name = NameTextBox.Text;

                //Разрешаем нажатие кнопки Ок из TextBox name.
                buttonOKisAvailableToClick_Name = true;
            }
            catch (Exception exception)
            {
                _textException = exception.Message;
                countException++;
                buttonOKisAvailableToClick_Name = false;
            }

            //Если произошла ошибка.
            if (countException != 0)
            {
                NameTextBox.BackColor = Color.LightSalmon;
                nameToolTip.Show(_textException, NameTextBox, 
                    (Point)(NameTextBox.Size + new Size(-400, 10)), 5000);
            }
            else
            {
                NameTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Считывает дату рождения контакта с TextBox
        /// </summary>
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int countException = 0;
            ToolTip dateOfBirthToolTip = new ToolTip();

            try
            {
                _сontact.DateOfBirth = BirthdayTimePicker.Value;

                //Разрешаем нажатие кнопки Ок из TextBox BirthdaytimePicker.
                buttonOKisAvailableToClick_Birthday = true;
            }
            catch (Exception exception)
            {
                _textException = exception.Message;
                countException++;

                //Переключаем флаг в false и запрещаем нажимать кнопку ОК.
                buttonOKisAvailableToClick_Birthday = false;
            }

            //Если произошла ошибка.
            if (countException != 0)
            {
                BirthdayTimePicker.CalendarMonthBackground = Color.LightSalmon;
                dateOfBirthToolTip.Show(_textException, BirthdayTimePicker,
                    (Point)(BirthdayTimePicker.Size + new Size(-163, 10)), 6000);
            }
            else
            {
                BirthdayTimePicker.CalendarMonthBackground = Color.White;
            }
        }

        /// <summary>
        /// Считывает номер телефона контакта с TextBox
        /// </summary>
        private void PhoneTextBox_TextChanged(object sender, EventArgs e)
        {
            int countException = 0;
            ToolTip phoneToolTip = new ToolTip();

            try
            {
                //Заполняем данные из TextBox*а, если в строке есть символы.
                if (PhoneTextBox.Text.Length != 0)
                {
                    _сontact.phoneNumber.Number = Convert.ToInt64(PhoneTextBox.Text);

                    //Разрешаем нажатие кнопки Ок из TextBox phone.
                    buttonOKisAvailableToClick_Phone = true;
                }
                else
                {
                    countException++;
                    phoneToolTip.Show("Вы ввели пустую строку.Повторите ввод.", PhoneTextBox,
                        (Point) (PhoneTextBox.Size + new Size(-400, 10)), 5000);

                    //Переключаем флаг в false и запрещаем нажимать кнопку ОК.
                    buttonOKisAvailableToClick_Phone = false;
                }
            }

            catch (ArgumentException exception)
            {
                _textException = exception.Message;
                countException++;

                //Переключаем флаг в false и запрещаем нажимать кнопку ОК.
                buttonOKisAvailableToClick_Phone = false;
            }

            //Если произошла ошибка.
            if (countException != 0)
            {
                PhoneTextBox.BackColor = Color.LightSalmon;
                phoneToolTip.Show(_textException, PhoneTextBox,
                    (Point)(PhoneTextBox.Size + new Size(-400, 10)), 5000);
            }
            else
            {
                PhoneTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Считывает e-mail контакта с TextBox
        /// </summary>
        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            int countException = 0;
            ToolTip emailToolTip = new ToolTip();

            try
            {
                _сontact.Email = EmailTextBox.Text;

                //Разрешаем нажатие кнопки Ок из TextBox email.
                buttonOKisAvailableToClick_Email = true;
            }

            catch (Exception exception)
            {
                _textException = exception.Message;
                countException++;

                //Переключаем флаг в false и запрещаем нажимать кнопку ОК.
                buttonOKisAvailableToClick_Email = false;
            }

            //Если произошла ошибка.
            if (countException != 0)
            {
                EmailTextBox.BackColor = Color.LightSalmon;
                emailToolTip.Show(_textException, EmailTextBox,
                    (Point)(EmailTextBox.Size + new Size(-400, 10)), 5000);
            }
            else
            {
                EmailTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Считывает Id vk контакта с TextBox
        /// </summary>
        private void IdVkTextBox_TextChanged(object sender, EventArgs e)
        {
            int countException = 0;
            ToolTip idVkToolTip = new ToolTip();

            try
            {
                _сontact.IdVk = IdVkTextBox.Text;

                //Разрешаем нажатие кнопки Ок из TextBox Id Vk.
                buttonOKisAvailableToClick_IdVk = true;
            }

            catch (Exception exception)
            {
                _textException = exception.Message;
                countException++;

                //Переключаем флаг в false и запрещаем нажимать кнопку ОК.
                buttonOKisAvailableToClick_IdVk = false;
            }

            //Если произошла ошибка.
            if (countException != 0)
            {
                IdVkTextBox.BackColor = Color.LightSalmon;
                idVkToolTip.Show(_textException, IdVkTextBox,
                    (Point)(IdVkTextBox.Size + new Size(-400, 10)), 5000);
            }
            else
            {
                IdVkTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Заполняет форму для дальнейшего редактирования данных.
        /// </summary>
        private void AddEditContactForm_Load(object sender, EventArgs e)
        {
            //Если выбран какой-либо контакт.
            if (_сontact.Surname != null)
            {
                //Заполняем форму данными выбранного контакта.
                SurnameTextBox.Text = _сontact.Surname;
                NameTextBox.Text = _сontact.Name;
                BirthdayTimePicker.Value = _сontact.DateOfBirth;
                PhoneTextBox.Text = _сontact.phoneNumber.Number.ToString();
                EmailTextBox.Text = _сontact.Email;
                IdVkTextBox.Text = _сontact.IdVk;
            }
        }

        /// <summary>
        /// Защита ввода данных в Surname TextBox, формы AddEditContact.
        /// </summary>
        private void SurnameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Проверка на Backspace, позволяет очищать строку.
            if(e.KeyChar == (char)Keys.Back)
                return;

            //Проверка на ввод, пропускающая только символы и дефис.
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == '-')) 
                e.Handled = true;
        }

        /// <summary>
        /// Защита ввода данных в Name TextBox, формы AddEditContact.
        /// </summary>
        private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Проверка на Backspace, позволяет очищать строку.
            if (e.KeyChar == (char)Keys.Back)
                return;

            //Проверка на ввод, пропускающая только символы и дефис.
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == '-'))
                e.Handled = true;
        }

        /// <summary>
        /// Защита ввода данных в Phone TextBox, формы AddEditContact.
        /// </summary>
        private void PhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int i = 0;

            //Проверка на Backspace, позволяет очищать строку.
            if (e.KeyChar == (char) Keys.Back)
                return;

            //Проверка на ввод, пропускающая только цифры.
            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
                i++;
            }

            //Ограничение на ввод 11 символов.
            PhoneTextBox.MaxLength = 11;
        }
    }
}