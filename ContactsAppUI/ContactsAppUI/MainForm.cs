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
        private Project project;

        public MainForm()
        {
            InitializeComponent();

            project = ProjectManager.ProjectDeserialization();

            int countContacts = 0;
            while (countContacts != project.contactsList.Count)
            {
                ContactsListBox.Items.Add(project.contactsList[countContacts].Surname);
                countContacts++;
            }
        }

        /// <summary>
        /// Функция добавления контакта.
        /// </summary>
        private void AddContact()
        {
            //Создаем новую форму.
            var newForm = new AddEditContactForm();
            //Создаем переменную, в которую помещаем результат взаимодействия пользователя с формой.
            var resultOfDialog = newForm.ShowDialog();
            //Если пользователь нажал ОК, то вносим исправленные данные.
            if (resultOfDialog == DialogResult.OK)
            {
                //Создаем переменную, в которую выполняем запись новых данных.
                var contact = newForm.NewContact;
                //Добавляем новый контакт в список.
                project.contactsList.Add(contact);
                //Добавляем новый контакт в пользовательский интерфейс.
                ContactsListBox.Items.Add(contact.Surname);
                //Выполняем сериализацию данных.
                ProjectManager.ProjectSerialization(project);
            }
        }

        /// <summary>
        /// Функция удаления контакта.
        /// </summary>
        private void RemoveContact()
        {
            //Находим индекс контакта, выбранного для удаления.
            var index = ContactsListBox.SelectedIndex;
            //Удаляем контакт из списка.
            project.contactsList.RemoveAt(index);
            //Удаляем контакт из пользователького интерфейса.
            ContactsListBox.Items.RemoveAt(index);
            //Выполняем сериализацию данных.
            ProjectManager.ProjectSerialization(project);
        }

        /// <summary>
        /// Функция, выполняющая редактирование данных.
        /// </summary>
        private void EditContact()
        {
            //Находим индекс редактируемого контакта.
            var index = ContactsListBox.SelectedIndex;
            //Находим редактируемый контакт.
            var contactOfIndex = project.contactsList[index];

            //Создаем клон редактируемого контакта.
            Contact newCloneContact = (Contact) contactOfIndex.Clone();

            //Создаем форму редактирования контакта.
            var newForm = new AddEditContactForm();
            //Заполняем форму данными объекта - клона контакта.
            newForm.NewContact = newCloneContact;

            //Создаем переменную, в которую помещаем результат взаимодействия пользователя с формой.
            var resultOfDialog = newForm.ShowDialog();
            //Если пользователь нажал ОК, то вносим исправленные данные.
            if (resultOfDialog == DialogResult.OK)
            {
                //Сохраняем новые данные в переменную со старыми.
                contactOfIndex = newForm.NewContact;
                //Вставляем данные в список по указанному индексу.
                project.contactsList.Insert(index, contactOfIndex);
                //Вставляем данные в интерфейс, находящиеся по указаному индексу.
                ContactsListBox.Items.Insert(index, contactOfIndex.Surname);
                //Удаляем сдвинутый на предыдущем шаге элемент из списка.
                project.contactsList.RemoveAt(index + 1);
                //Удаляем элемент, который изменили, из интерфейса.
                ContactsListBox.Items.RemoveAt(index + 1);
                //Выполняем сериализацию данных.
                ProjectManager.ProjectSerialization(project);
            }
        }


        private void FindTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        /// <summary>
        /// Вызывает функцию для добавления контакта.
        /// </summary>
        private void AddContactButton_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Вызывает функцию для редактирования контакта.
        /// </summary>
        private void EditContactButton_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Вызывает функцию для удаления контакта.
        /// </summary>
        private void RemoveContactButton_Click(object sender, EventArgs e)
        {
            RemoveContact();
        }

        /// <summary>
        /// Добавление контакта по клику в выпадающем сверху меню из Edit.
        /// </summary>
        private void AddContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContact();
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
        /// Выпадение формы About, при клике в верхнем меню на About.
        /// </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newForm = new AboutForm();
            newForm.Show();
        }

        /// <summary>
        /// Переключение между контактами списка и вывод выбранного контакта.
        /// </summary>
        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Находим индекс выбранного элемента.
            var selectedIndex = ContactsListBox.SelectedIndex;

            //
            if (selectedIndex == -1)
            {
                selectedIndex = 0;
            }

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
    }
}
