using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            //Создаем список контактов.
            Project project = new Project();
            //Вызываем десериализацию для списка контактов.
            //Project project = ProjectManager.ProjectDeserialization();

            //Создаем новый контакт.
            Contact contactOne = new Contact();

            //Заполняем поля контакта.
            contactOne.Name = "Anton";
            contactOne.Surname = "Petrov";
            contactOne.DateOfBirth = new DateTime(1985, 11, 25);
            contactOne.PhoneNumber.Number = 78886663322;
            contactOne.Email = "antonpetrov@mail.ru";
            contactOne.IdVk = "15864785";

            //Создаем клон контакта.
            Contact contactTwo = (Contact) contactOne.Clone();

            //Добавляем в список контактов контакт 1.
            project.contacts.Add(contactOne);
            //Добавляем в список контактов контакт 2.
            project.contacts.Add(contactTwo);

            //Вызываем сериализацию для списка контактов.
            ProjectManager.ProjectSerialization(project);
        }
    }
}
