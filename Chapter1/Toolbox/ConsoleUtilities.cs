using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolbox
{
    /// <summary>
    /// Contient des méthodes spécifiques aux applications console
    /// </summary>
    public static class ConsoleUtilities
    {
        /// <summary>
        /// Permet de récupérer un mot de passe dans une application console en masquant la chaine de caractères
        /// </summary>
        /// <returns>Un string contenant le mot de passe</returns>
        public static string GetPassword()
        {
            string pwd = string.Empty;
            bool typingPwd = true;

            while (typingPwd)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                if (keyPressed.Key == ConsoleKey.Backspace && pwd != string.Empty)
                {
                    pwd = pwd.Substring(0, pwd.Length - 1);
                }
                else
                {
                    char c = keyPressed.KeyChar;
                    if (c == '\r')
                    {
                        typingPwd = false;
                        Console.WriteLine();
                    }
                    else
                    {
                        pwd += c.ToString();
                    }
                }
            }

            return pwd;
        }
    }
}
