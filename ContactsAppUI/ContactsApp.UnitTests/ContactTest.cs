using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ContactsApp.UnitTests
{
    [TestFixture]
    public class ContactTest
    {
        private Contact _contact;

        [SetUp]
        public void InitContact()
        {
            _contact = new Contact();
        }

        //Тесты свойства Surname.
        [TestCase("", "Должно возникать исключение, если фамилия - пустая строка",
            TestName = "Присвоение пустой строки в качестве фамилии")]
        [TestCase("Смирнов-Смирнов-Смирнов-Смирнов-Смирнов-Смирнов-Смирнов",
            "Должно возникать исключение, если фамилия длиннее 50 символов",
            TestName = "Присвоение неправильной фамилии, больше 50 символов")]
        public void TestSurnameSet_ArgumentException(string wrongSurname, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.Surname = wrongSurname; }, message);
        }

        [TestCase("Смирнов", "Тест не пройден, фамилия присвоена неверно",
            TestName = "Присвоение правильной фамилии")]
        public void TestSurnameSet_CorrectArgument(string correctSurname, string message)
        {
            Assert.DoesNotThrow(
                () => { _contact.Surname = correctSurname; }, message);
        }

        [TestCase("Смирнов", "Геттер surname возвращает неверную фамилию",
            TestName = "Возвращение геттером фамилии")]
        public void TestSurnameGet_CorrectArgument(string correctSurname, string message)
        {
            _contact.Surname = correctSurname;
            //Если не равны, то выдаст сообщение об этом
            Assert.AreEqual(correctSurname, _contact.Surname, message);
        }

        //Тесты свойства Name.
        [TestCase("", "Должно возникать исключение, если поле имя - пустая строка",
            TestName = "Присвоение пустой строки в качестве имени")]
        [TestCase("Николай-Николай-Николай-Николай-Николай-Николай-Николай",
            "Должно возникать исключение, если имя длиннее 50 символов",
            TestName = "Присвоение неправильного имени, больше 50 символов")]
        public void TestNameSet_ArgumentException(string wrongName, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.Name = wrongName; },
                message);
        }

        [TestCase("Николай", "Тест не пройден, имя  присвоено неверно",
            TestName = "Присвоение правильного имени")]
        public void TestNameSet_CorrectArgument(string correctName, string message)
        {
            Assert.DoesNotThrow(
                () => { _contact.Name = correctName; }, message);
        }

        [TestCase("Николай", "Геттер name возвращает неверное имя",
            TestName = "Возвращение геттером имени")]
        public void TestNameGet_CorrectArgument(string correctName, string message)
        {
            _contact.Name = correctName;
            Assert.AreEqual(correctName, _contact.Name, message);
        }

        //Тесты свойства Email.
        [TestCase("", "Должно возникать исключение, если поле е-мейл - пустая строка",
            TestName = "Присвоение пустой строки в качестве е-мейла")]
        [TestCase("smirnov.nikolay@gmail.com-smirnov.nikolay@gmail.com-",
            "Должно возникать исключение, если е-мейл длиннее 50 символов",
            TestName = "Присвоение неправильного е-мейла, больше 50 символов")]
        public void TestEmailSet_ArgumentException(string wrongEmail, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.Email = wrongEmail; },
                message);
        }

        [TestCase("smirnov.nikolay@gmail.com", "Тест не пройден, е-мейл присвоен неверно",
            TestName = "Присвоение правильного е-мейла")]
        public void TestEmailSet_CorrectArgument(string correctEmail, string message)
        {
            Assert.DoesNotThrow(
                () => { _contact.Email = correctEmail; }, message);
        }

        [TestCase("Николай", "Геттер е-мейл возвращает неверное имя",
            TestName = "Возвращение геттером е-мейла")]
        public void TestEmailGet_CorrectArgument(string correctEmail, string message)
        {
            _contact.Email = correctEmail;
            Assert.AreEqual(correctEmail, _contact.Email, message);
        }

        //Тесты свойства IdVk.
        [TestCase("", "Должно возникать исключение, если поле id vk - пустая строка",
            TestName = "Присвоение пустой строки в качестве id vk")]
        [TestCase("nikolaysmirnov19803015", "Должно возникать исключение, если id vk длиннее 15 символов",
            TestName = "Присвоение неправильного id vk, больше 15 символов")]
        public void TestIdVkSet_ArgumentException(string wrongIdVk, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.IdVk = wrongIdVk; }, message);
        }

        [TestCase("nikolay38", "Тест не пройден, id vk присваивается неверное значение.",
            TestName = "Присвоение правильного id vk")]
        public void TestIdVkSet_CorrectArgument(string correctIdVk, string message)
        {
            Assert.DoesNotThrow(
                () => { _contact.IdVk = correctIdVk; }, message);
        }

        [TestCase("nikolay38", "Геттер id vk возвращает неверное имя",
            TestName = "Возвращение геттером id vk")]
        public void TestIdVkGet_CorrectArgument(string correctIdVk, string message)
        {
            _contact.IdVk = correctIdVk;
            Assert.AreEqual(correctIdVk, _contact.IdVk, message);
        }

        //Тесты свойства DateOfBirth.
        [TestCase("1899, 01, 01", "Должно возникать исключение, если дата dateOfBirth меньше, чем 1900,01,01",
            TestName = "Присвоение dateOfBirth меньшее, чем 1900,01,01")]
        [TestCase("3075, 01, 01", "Должно возникать исключение, если дата dateOfBirth больше нынешней",
            TestName = "Присвоение dateOfBirth большее, чем нынешняя даты")]
        public void TestDateOfBirthSet_ArgumentException(string wrongDateOfBirth, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.DateOfBirth = DateTime.Parse(wrongDateOfBirth); },
                message);
        }

        [TestCase("1975, 01, 01", "Тест не пройден, dateOfBirth присваивается неверное значение",
            TestName = "Присвоение правильного dateOfBirth")]
        public void TestDateOfBirthSet_CorrectArgument(string correctDateOfBirth, string message)
        {
            Assert.DoesNotThrow(
                () => { _contact.DateOfBirth = DateTime.Parse(correctDateOfBirth); },
                message);
        }

        [TestCase("1980, 01, 01", "Геттер dateOfBirth возвращает неверное имя",
            TestName = "Возвращение геттером dateOfBirth")]
        public void TestDateOfBirthGet_CorrectArgument(string correctDateOfBirth, string message)
        {
            _contact.DateOfBirth = DateTime.Parse(correctDateOfBirth);
            Assert.AreEqual(DateTime.Parse(correctDateOfBirth), _contact.DateOfBirth,
                message);
        }

        //Тесты вызова свойства Number класса PhoneNumber.
        [TestCase("11111111111", "Должно возникать исключение, если номер начинается не с 7",
            TestName = "Присвоение number номера телефона, начинающегося не с 7")]
        [TestCase("123456789123", "Должно возникать исключение, если введенный номер длиннее 11 символов",
            TestName = "Присвоение number номера телефона, который длиннее 11 символов")]
        [TestCase("1234567891", "Должно возникать исключение, если введенный номер короче 11 символов",
            TestName = "Присвоение number номера телефона, который короче 11 символов")]
        [TestCase("", "Должно возникать исключение, если поле phone - пустая строка",
            Ignore = "Видимо ошибка преобразования пустой строки в лонг",
            TestName = "Присвоение пустой строки в качестве поля phone")]
        public void TestPhoneNumberSet_ArgumentException(string wrongPhoneNumber, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.phoneNumber.Number = Convert.ToInt64(wrongPhoneNumber); },
                message);
        }

        [TestCase("79998887766", "Тест не пройден, фамилия присваивается неверно",
            TestName = "Присвоение правильного номера телефона.")]
        public void TestPhoneNumberSet_CorrectArgument(string correctNumber, string message)
        {
            _contact.phoneNumber.Number = Convert.ToInt64(correctNumber);
            Assert.AreEqual(Convert.ToInt64(correctNumber), _contact.phoneNumber.Number,
                message);
        }

        [TestCase("76665554433", "Геттер phone, возвращает неверный номер телефона",
            TestName = "Возвращение геттером номера телефона")]
        public void TestPhoneNumberGet_CorrectArgument(string correctNumber, string message)
        {
            _contact.phoneNumber.Number = Convert.ToInt64(correctNumber);
            Assert.AreEqual(Convert.ToInt64(correctNumber), _contact.phoneNumber.Number,
                message);
        }
    }
}