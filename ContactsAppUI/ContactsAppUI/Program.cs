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


            //ContactsApp.Contact contact = new ContactsApp.Contact(new PhoneNumber(), "Иван", "Петров", "ivanpetrov@gmail.com", new DateTime(1985, 03, 10), "4526478");
            Project project = new Project();

            Contact contactOne = new Contact();

            contactOne.Name = "Anton";
            contactOne.Family = "Petrov";
            contactOne.DateOfBirth = new DateTime(1985, 11, 25);
            contactOne.PhoneNumber.Number = 78886663322;
            contactOne.Email = "antonpetrov@mail.ru";
            contactOne.IdVk = "15864785";

            Contact contactTwo = (Contact) contactOne.Clone();

            project.contacts.Add(contactOne);
            project.contacts.Add(contactTwo);

            ProjectManager.ProjectSerialization(project);
        }
    }
}
