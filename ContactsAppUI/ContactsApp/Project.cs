using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp
{
    /// <summary>
    /// Класс, содержащий лист всех контактов.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Лист, который хранит в себе список контактов.
        /// </summary>
        public List<Contact> contactsList = new List<Contact>();

        /// <summary>
        /// Функция, выполняющая сортировку по алфавиту.
        /// </summary>
        /// <param name="project">Список, который нужно отсортировать</param>
        /// <returns>Отсортированный список</returns>
        public static Project Sort(Project project)
        {
            Project sortedProject = new Project();

            //Добавляем 0 элемент несортированного списка в сортированный.
            sortedProject.contactsList.Add(project.contactsList[0]);


            for (int i = 1; i < project.contactsList.Count; i++)
            {
                //Если фамилия из сортированного списка меньше фамилии из несортированного списка, то вставляем несортированную фамилию после.
                if (sortedProject.contactsList[i - 1].Surname[0] < project.contactsList[i].Surname[0])
                {
                    sortedProject.contactsList.Insert(i, project.contactsList[i]);
                    continue;
                }

                //Если фамилии равны, то вставляем несортированную фамилию после сортированной.
                if (sortedProject.contactsList[i - 1] == project.contactsList[i])
                {
                    sortedProject.contactsList.Insert(i, project.contactsList[i]);
                    continue;
                }

                int j = i;
                //int index = 1;

                bool flag = true;

                //Пока 1 символ отсортированной фамилии больше или равен 1 символа не сортированной
                while (j > 0 && sortedProject.contactsList[j - 1].Surname[0] >= project.contactsList[i].Surname[0] &&
                       sortedProject.contactsList[j - 1] != project.contactsList[i] && flag) 
                {
                    if (project.contactsList.Count == sortedProject.contactsList.Count)
                    {
                        return sortedProject;
                    }

                    //if (sortedProject.contactsList[j - 1] == project.contactsList[i])
                    //{
                    //    j--;
                    //    continue;
                    //}

                    //Если первые символы фамилий равны
                    if (sortedProject.contactsList[j - 1].Surname[0] == project.contactsList[i].Surname[0])
                    {
                        //Находим длину самой короткой фамилии
                        int countSubmolSurname = project.contactsList[i].Surname.Length >=
                                                 sortedProject.contactsList[j - 1].Surname.Length
                            ? sortedProject.contactsList[j - 1].Surname.Length
                            : project.contactsList[i].Surname.Length;

                        //Выполняем сравнение остальных
                        //for (int k = 1; k < countSubmolSurname; k++)
                        //{
                        int k = 1;
                        //Пока символ сортированной фамилии меньше соответствующего символа несортированной.
                        while (sortedProject.contactsList[j - 1].Surname[k] == project.contactsList[i].Surname[k]) ////////////////////////////////////////////////////
                        {
                            //index = i;
                            k++;
                            if (k==countSubmolSurname)
                            {
                                continue;
                            }
                            //break;
                        }

                        if (sortedProject.contactsList[j - 1].Surname[k] < project.contactsList[i].Surname[k])
                        {
                            sortedProject.contactsList.Insert(j, project.contactsList[i]);
                            flag = false;
                            continue;
                        }

                        else
                        {
                            sortedProject.contactsList.Insert(j - 1, project.contactsList[i]);
                            flag = false;
                            continue;
                        }

                        //if (j == 1)
                        //{
                        //    sortedProject.contactsList.Insert(0, project.contactsList[index]);
                        //    continue;
                        //}
                        //else
                        //{
                        //    sortedProject.contactsList.Insert(j - 2, project.contactsList[index]);
                        //    continue;
                        //}
                    }

                    if (j == 1)
                    {
                        sortedProject.contactsList.Insert(0, project.contactsList[i]);
                        continue;
                    }
                    else
                    {
                        sortedProject.contactsList.Insert(j-1, project.contactsList[i]);
                        continue;
                    }

                    j--;
                }

                //sortedProject.contactsList.Insert(j, project.contactsList[i]);
            }

            return sortedProject;
        }

        /// <summary>
        /// Функция, которая выполняет поиск контактов по имени и фамилии по указанной подстроке и сортирует их в алфавитном порядке.
        /// </summary>
        /// <param name="project">Проект, хранящий список, в котором будет выполняться поиск.</param>
        /// <param name="substring">Подстрока, вхождение которой будет искаться.</param>
        /// <returns>Проект с отсортированным списком, в котором есть вхождение подстроки.</returns>
        public static Project Sort(Project project, string substring)
        {
            Project sortedProject = new Project();

            for (int i = 0; i < project.contactsList.Count; i++)
            {
                if (project.contactsList[i].Surname.Contains(substring) ||
                    project.contactsList[i].Name.Contains(substring))
                {
                    sortedProject.contactsList.Add(project.contactsList[i]);
                }
            }

            if (sortedProject.contactsList.Count == 0)
            {
                sortedProject = null;
                return sortedProject;
            }

            Project.Sort(sortedProject);

            return sortedProject;
        }

        /// <summary>
        /// Функция, выполняющая поиск людей, у который день рождения в указанную дату.
        /// </summary>
        /// <param name="project">Проект, содержащий список людей, среди который будем искать у кого день рождения.</param>
        /// <param name="today">Дата дня рождения.</param>
        /// <returns>Проект, хранящий список именинников.</returns>
        public static Project Birthday(Project project, DateTime today)
        {
            Project birthdayList = new Project();

            for (int i = 0; i < project.contactsList.Count; i++)
            {
                if (project.contactsList[i].DateOfBirth.Day == today.Day &&
                    project.contactsList[i].DateOfBirth.Month == today.Month)
                {
                    birthdayList.contactsList.Add(project.contactsList[i]);
                }
            }

            return birthdayList;
        }
    }
}
