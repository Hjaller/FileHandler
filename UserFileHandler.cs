using System;
using System.Collections.Generic;
using System.IO;

namespace FileHandler
{
    internal static class UserFileHandler
    {
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Files", "Users.txt");

        public static void AddUserToFile(RegisteredUser user)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(user.ToString());
            }
        }

        public static List<RegisteredUser> GetUsersFromFile()
        {
            var users = new List<RegisteredUser>();

            if (!File.Exists(filePath))
                throw new FileLoadException("Users.txt filen eksisterer ikke.");

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(", ");
                    if (parts.Length == 4)
                    {
                        int age;
                        bool isAgeInt = int.TryParse(parts[2], out age);
                        var user = new RegisteredUser() { FirstName = parts[0], LastName = parts[1], Age = age, Email = parts[3] };
                        users.Add(user);
                    }
                }
            }

            users.Sort();
            return users;
        }
    }
}