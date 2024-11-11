using System;
using System.IO;
using FileHandler.Exceptions;

namespace FileHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string firstName = "";
            string lastName = "";
            bool ageIsInt = false;
            try
            {
                Console.Write("Indtast fornavn: ");
                firstName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(firstName))
                {
                    throw new InvalidNameException("Fornavnet må ikke være tomt.");
                }

                Console.Write("Indtast efternavn: ");
                lastName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    throw new InvalidNameException("Efternavnet må ikke være tomt.");
                }

                Console.Write("Indtast alder: ");
                string ageInput = Console.ReadLine();
                ageIsInt = int.TryParse(ageInput, out int age);
                if (!ageIsInt)
                {
                    throw new InvalidAgeException("Alder skal være et gyldigt heltal.");
                }

                if (!(firstName + " " + lastName).Equals("niels olesen", StringComparison.OrdinalIgnoreCase) && (age < 18 || age > 50))
                {
                    throw new InvalidAgeException("Alder skal være mellem 18 og 50.");
                }

                Console.Write("Indtast email: ");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                {
                    throw new InvalidEmailException("Email må ikke være tom, og skal indeholde både '@' og '.' .", new ArgumentNullException());
                }

                string user = $"{firstName} {lastName}, {age}, {email}";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Files", "Users.txt");

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(user);
                }

                // List users
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
            catch (InvalidAgeException ex) when (!((firstName + " " + lastName).Equals("niels olesen", StringComparison.OrdinalIgnoreCase)))
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
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Directory Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled Exception: {ex.ToString()}");
            }
            finally
            {
                Console.WriteLine("Program afsluttes korrekt.");
            }
        }
    }
}