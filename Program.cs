﻿using FileHandler.Exceptions;

namespace FileHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.Write("Indtast fornavn: ");
                string firstName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(firstName)) {
                    throw new InvalidNameException("Fornavnet må ikke være tomt.");
                }
                Console.Write("Indtast efternavn: ");
                string lastName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(lastName)) {
                    throw new InvalidNameException("Efternavnet må ikke være tomt.");
                }
                Console.Write("Indtast alder: ");
                if (!int.TryParse(Console.ReadLine(), out int age) || age < 18 || age > 50)
                {
                    throw new InvalidAgeException("Alder skal være et gyldigt heltal mellem 18 og 50.");
                }
                Console.Write("Indtast email: ");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains(".")) {
                    throw new InvalidEmailException("Email må ikke være tom.", new ArgumentNullException());
                }
                string user = $"{firstName} {lastName}, {age}, {email}";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Users.txt");

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(user);
                }
                //list users
                if (!File.Exists(filePath))
                    throw new FileLoadException("Users.txt filen eksisterer ikke.");

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                        throw new FileLoadException("Users.txt filen er tom eller beskadiget.");

                    Console.WriteLine("Registrerede brugere:");
                    Console.WriteLine(content);
                }
            }
            catch (InvalidNameException ex)
            {
                Console.WriteLine($"Name Error: {ex.Message}");
            }
            catch (InvalidAgeException ex)
            {
                Console.WriteLine($"Age Error: {ex.Message}");
            }
            catch (InvalidEmailException ex)
            {
                Console.WriteLine($"Email Error: {ex.Message} - {ex.InnerException?.Message}");
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine($"File Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Program afsluttes korrekt.");
            }
        }
    }
}
