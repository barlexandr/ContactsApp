using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ContactsApp
{
    /// <summary>
    /// Класс, реализующий сохранение данных в файл и загрузки из него.
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private static string stringMyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                                      "/ContactsApp" + "/ContactsApp.notes";

        /// <summary>
        /// Метод, выполняющий запись в файл
        /// </summary>
        /// <param name="notes">Экземпляр проекта для сериализации</param>
        public static void ProjectSerialization(Project contact)
        {
            // Экземпляр сериалиатора
            JsonSerializer serializer = new JsonSerializer();

            //Проверка на папку. Если нет папки ContactsApp, то создаем ее.
            if(!System.IO.Directory.Exists(stringMyDocumentsPath))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                          "/ContactsApp");
            }

            using (StreamWriter sw = new StreamWriter(stringMyDocumentsPath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                // Вызов сериализатора и передача объекта сериализации
                serializer.Serialize(writer, contact);
            }
        }

        /// <summary>
        /// Метод, выполняющий чтение из файла
        /// </summary>
        /// <returns>Экземпляр проекта, считанный из файла</returns>
        public static Project ProjectDeserialization()
        {
            //Переменная, в которую будет помещен результат десериализации
            Project project = new Project();

            //Экземпляр сериализатора
            JsonSerializer serializer = new JsonSerializer();

            //Сделаь проверку десeриализации

            //Открываем поток для чтения из файла с указанием пути
            using (StreamReader sr = new StreamReader(stringMyDocumentsPath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                //Вызываем десериализацию и явно преобразуем результат в целевой тип данных
                project = (Project) serializer.Deserialize<Project>(reader);
            }

            return project;
        }
    }
}