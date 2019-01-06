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
    public partial class MainForm : Form
    {
        //Создаем список контактов.
        private Project project;

        public MainForm()
        {
            InitializeComponent();

            //Выполняем десериализацию.
            project = ProjectManager.LoadFromFile(ProjectManager.stringMyDocumentsPath);
            int countContacts = 0;

            //Пока количество записей в файле не равно количеству записей в ListBox.
            while (countContacts != project.contactsList.Count)
            {
                //Добавляем запись в ListBox.
                ContactsListBox.Items.Add(project.contactsList[countContacts].Surname);

                //Счетчик записей +1.
                countContacts++;
            }

            //Подсказка для кнопок Add, Remove, Edit
            ToolTip addRemoveEdiToolTip = new ToolTip();
            addRemoveEdiToolTip.SetToolTip(AddContactButton, "Нажмите для добавления контакта в список.");
            addRemoveEdiToolTip.SetToolTip(RemoveContactButton, "Нажмите для удаления контакта из списка.");
            addRemoveEdiToolTip.SetToolTip(EditContactButton, "Нажмите для редактирования контакта.");
        }
        
        /// <summary>
        /// Функция добавления контакта.
        /// </summary>
        private void AddContact()
        {
            var newForm = new AddEditContactForm();

            //Создаем переменную, в которую помещаем результат взаимодействия пользователя с формой.
            var resultOfDialog = newForm.ShowDialog();

            //Если пользователь нажал ОК, то вносим исправленные данные.
            if (resultOfDialog == DialogResult.OK)
            {
                var contact = newForm.Contact;
                project.contactsList.Add(contact);
                ContactsListBox.Items.Add(contact.Surname);
                ProjectManager.SaveToFile(project, ProjectManager.stringMyDocumentsPath);
            }
        }

        /// <summary>
        /// Функция удаления контакта.
        /// </summary>
        private void RemoveContact()
        {
            var index = 0;

            if (ContactsListBox.SelectedIndex > 0)
            {
                index = ContactsListBox.SelectedIndex;
            }

            //Если список не пуст.
            if (project.contactsList.Count > 0)
            {
                string removeThisContact =
                    "Do you really want to remove this contact: " + SurnameTextBox.Text + "?";

                var result = MessageBox.Show(removeThisContact, "Remove", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    project.contactsList.RemoveAt(index);
                    ContactsListBox.Items.RemoveAt(index);
                    ProjectManager.SaveToFile(project, ProjectManager.stringMyDocumentsPath);
                }
            }
            else
            {
                MessageBox.Show("Список пуст.", "Remove");
            }
        }

        /// <summary>
        /// Функция, выполняющая редактирование данных.
        /// </summary>
        private void EditContact()
        {
            var index = 0;
            if (ContactsListBox.SelectedIndex > 0)
            {
                index = ContactsListBox.SelectedIndex;
            }

            //Если список не пуст.
            if (project.contactsList.Count > 0)
            {
                var contactOfIndex = project.contactsList[index];
                Contact newCloneContact = (Contact) contactOfIndex.Clone();
                var newForm = new AddEditContactForm();
                newForm.Contact = newCloneContact;

                //Создаем переменную, в которую помещаем результат взаимодействия пользователя с формой.
                var resultOfDialog = newForm.ShowDialog();

                //Если пользователь нажал ОК, то вносим исправленные данные.
                if (resultOfDialog == DialogResult.OK)
                {
                    contactOfIndex = newForm.Contact;
                    
                    project.contactsList.RemoveAt(index);
                    ContactsListBox.Items.RemoveAt(index);

                    project.contactsList.Insert(index, contactOfIndex);
                    ContactsListBox.Items.Insert(index, contactOfIndex.Surname);

                    //Выполняем сериализацию данных.
                    ProjectManager.SaveToFile(project, ProjectManager.stringMyDocumentsPath);
                }
            }
            else
            {
                MessageBox.Show("Список пуст", "Edit");
            }
        }

        /// <summary>
        /// Вызывает функцию для добавления контакта.
        /// </summary>
        private void AddContactButton_Click(object sender, EventArgs e)
        {
           //Вызываем функцию добавления контакта.
            AddContact();
        }

        /// <summary>
        /// Вызывает функцию для редактирования контакта.
        /// </summary>
        private void EditContactButton_Click(object sender, EventArgs e)
        {
            //Вызываем функцию редактирования контакта.
            EditContact();
        }

        /// <summary>
        /// Вызывает функцию для удаления контакта.
        /// </summary>
        private void RemoveContactButton_Click(object sender, EventArgs e)
        {
            //Вызываем функцию удаления контакта.
            RemoveContact();
        }

        /// <summary>
        /// Переключение между контактами списка и вывод выбранного контакта.
        /// </summary>
        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Изначально выбран 0 элемент списка.
            var selectedIndex = 0;
            if (ContactsListBox.SelectedIndex > 0)
            {
                selectedIndex = ContactsListBox.SelectedIndex;
            }

            if (project.contactsList.Count > 0)
            {
                //Находим контакт, по выбранному индексу.
                Contact contact = project.contactsList[selectedIndex];

                //Заполняем правую часть главной формы данными выбранного элемента.
                SurnameTextBox.Text = contact.Surname;
                NameTextBox.Text = contact.Name;
                BirthdayDateTimePicker.Value = contact.DateOfBirth;
                PhoneTextBox.Text = Convert.ToString(contact.phoneNumber.Number);
                EmailTextBox.Text = contact.Email;
                VkTextBox.Text = contact.IdVk;
            }
            else
            {
                SurnameTextBox.Text = null;
                NameTextBox.Text = null;
                BirthdayDateTimePicker.Value = new DateTime(1900,01,01);
                PhoneTextBox.Text = null;
                EmailTextBox.Text = null;
                VkTextBox.Text = null;
            }
        }

        /// <summary>
        /// Добавление контакта по клику в выпадающем сверху меню из Edit.
        /// </summary>
        private void AddContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Выпадение формы About, при клике в верхнем меню на About.
        /// </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newForm = new AboutForm();
            newForm.Show();
        }

        /// <summary>
        /// Редактирование контакта по клику в выпадающем сверху меню из Edit.
        /// </summary>
        private void EditContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditContact();
        }
        
        /// <summary>
        /// Удаление контакта по клику в выпадающем сверху меню из Edit.
        /// </summary>
        private void RemoveContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveContact();
        }

        /// <summary>
        /// Закрывает главное окно по клику в выпадающем сверху меню на Exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}