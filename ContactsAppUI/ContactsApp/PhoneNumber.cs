using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp
{
    /// <summary>
    /// Класс, принимающий и возвращающий номер телефона учащегося.
    /// </summary>
    public class PhoneNumber
    {
        private long _number;

        public long Number
        {
            get { return _number; }
            set
            {
                //Телефон может начинаться только с цифры 7.
                if (value.ToString()[0]!='7')
                {
                    throw new ArgumentException("Введите номер телефона, начинающийся с 7.");
                }

                //Проверка на количество цифр. Если больше 11, то исключение.
                if (value > 99999999999)
                {
                    throw new ArgumentException("Вы ввели больше 11 цифр, введите номер из 11 цифр.");
                }

                //Проверка на количество цифр. Если меньше 11, то исключение.
                if (value < 10000000000)
                {
                    throw new ArgumentException("Вы ввели меньше 11 цифр, введите номер, состоящий из 11 цифр.");
                }

                //Проверка на пустую строку
                if (String.IsNullOrWhiteSpace(value.ToString()))
                {
                    throw new ArgumentException("Вы ввели пустую строку. Повторите ввод.");
                }

                //Иначе присваиваем переменной номер.
                else
                {
                    _number = value;
                }
            }
        }
    }
}
