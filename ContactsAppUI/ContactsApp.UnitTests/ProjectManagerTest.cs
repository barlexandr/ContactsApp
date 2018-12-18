using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;

namespace ContactsApp.UnitTests
{
    [TestFixture]
    class ProjectManagerTest
    {
        private Contact _contact;
        private Project _project;

        [SetUp]
        public void InitComponent()
        {
            _project = new Project();

            _contact = new Contact();
            _contact.Name = "Maxim";
            _contact.Surname = "Petrov";
            _contact.DateOfBirth = new DateTime(1975, 01, 05);
            _contact.Email = "maxim.petrov@gmail.com";
            _contact.IdVk = "petrov75";
            _contact.phoneNumber.Number = 76665554433;
        }

        [Test(Description = "Позитивный тест сериализации")]
        public void TestSerialization_CorrectValue()
        {
            _project.contactsList.Add(_contact);

            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                      "/ContactsApp" + "/ContactAppTest");

            Assert.DoesNotThrow(
                () => { ProjectManager.ProjectSerialization(_project,
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ContactApp" + "/ContactAppTest" +
                        "/ContactAppTestSerialize.notes"); }, "Тест сериализации не пройден." );
        }

        [Test(Description = "Позитивный тест десериализации")]
        public void TestDeserilization_CorrectValue()
        {
            Project project = new Project();
            _project.contactsList.Add(_contact);

            ProjectManager.ProjectSerialization(_project,
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ContactApp" + "/ContactAppTest" +
                "/ContactAppTestDeserialize.notes");

            project = ProjectManager.ProjectDeserialization(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ContactApp" + "/ContactAppTest" +
                "/ContactAppTestDeserialize.notes");

            Assert.AreEqual(_project, project, "Десериализация работает неправильно.");
        }
    }
}
