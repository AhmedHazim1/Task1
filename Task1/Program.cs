using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace Task1
{
    class Program
    {
        class Input
        {
            public static string GetStr(string Prompt = "Enter a text: ")
            {
                Console.Write(Prompt);
                return Console.ReadLine();
            }
            public static int GetInt(string Prompt = "Enter an integer: ", int MaxValue = int.MaxValue, int MinValue = int.MinValue)
            {
                Console.Write(Prompt);
                string Inp = Console.ReadLine();                
                if (ValidInt(Inp))
                {
                    int Num = int.Parse(Inp);
                    if (Num <= MaxValue && Num >= MinValue)
                        return Num;
                    else
                        throw new Exception($"Number must be bettwen {MinValue} : {MaxValue}.");
                }                
                else
                {
                    throw new Exception($"Not a valid integer.");
                }
            }
            static bool ValidInt(string X)
            {
                X = string.IsNullOrEmpty(X) ? "" : X;
                return int.TryParse(X, out int n);
            }
        }
        class User
        {
            private string _username;
            private string _password;
            public User(string Name, string Pass)
            {
                Username = Name;
                Password = Pass;
            }
            public User(){}
            public static int ValidUsername(string username)
            {
                // 1: username is emtpy or null.
                // 2: length less than 4.
                // 3: doesn't start with a letter.
                if (string.IsNullOrEmpty(username))
                    return 1;
                if (username.Length < 4)
                    return 2;
                if (!char.IsLetter(username.First()))
                    return 3;
                return 0;
            }
            public static int ValidPassword(string password)
            {
                // 1: password is emtpy or null.
                // 2: length less than 6.
                if (string.IsNullOrEmpty(password))
                    return 1;
                if (password.Length < 6)
                    return 2;
                return 0;
            }
            public string Username
            {
                get 
                { 
                    return _username; 
                }
                set 
                {
                    value = value.Trim();
                    switch (ValidUsername(value))
                    {
                        default:
                            _username = value; break;
                        case 1:
                            throw new Exception("Username is Empty or Null."); break;
                        case 2:
                            throw new Exception("Username must be at least 4 charecters."); break;
                        case 3:
                            throw new Exception("Username must start with a letter."); break;
                    }
                }
            }
            
            public string Password
            { 
                get 
                {
                    return _password; 
                } 
                set 
                {
                    value = value.Trim();
                    switch (ValidUsername(value))
                    {
                        default:
                            _password = value; break;
                        case 1:
                            throw new Exception("Password is Empty or Null."); break;
                        case 2:
                            throw new Exception("Password must be at least 6 charecters."); break;
                    }
                }
            }
            public bool Authenticate(string name, string Pass)
            {
                return Username.Equals(name) && Password.Equals(Pass);
            }
        }   
        class Otp
        {
            private int _otpNumber;
            public Otp()
            {
                _otpNumber = GenearateRandomInt(100000, 999999);
            }
            private int GenearateRandomInt(int from, int to)
            {
                return new Random().Next(from, to);
            }
            public bool Validate(int Num)
            {
                if (Num == OtpNumber)
                    return true;    
                return false;
            }
            public int OtpNumber
            {
                get 
                { return _otpNumber; }
            }
        }

        static void Main(string[] args)
        {
            User user1 = new User("Ahmed Hazim", "Ahmed123");
            Console.WriteLine("Welcome to the Simple Login System!");
            
            //Validating User input data
            bool ValidUser = false;
            while (!ValidUser)
            {
                string Username = Input.GetStr("Username: ");
                string Password = Input.GetStr("Password: ");
                int ValidUsername = User.ValidUsername(Username);
                int ValidPassword = User.ValidPassword(Password);
                bool Authantic = user1.Authenticate(Username, Password);

                if (ValidUsername == 0 && ValidPassword == 0 && Authantic)
                {
                    break;
                }
                if (ValidUsername == 1 || ValidPassword == 1)
                {
                    Console.WriteLine("Username or password cannot be empty.");
                    continue;
                }
                else if (ValidUsername == 2 || ValidPassword == 2)
                {
                    Console.WriteLine("Length for the Username must be 4 And 6 for the password.");
                    continue;
                }
                if (ValidUsername == 3)
                {
                    Console.WriteLine("Username Should start with a letter.");
                    continue;
                }
                if (!Authantic)
                {
                    Console.WriteLine("Invalid username or password. Access denied.");
                    System.Environment.Exit(1);
                }
            }

            //Validating User input for OTP
            Otp otp = new Otp();
            Console.WriteLine(otp.OtpNumber);
            try
            {
                int num = Input.GetInt("Enter the OTP Code: ");
                bool ValidOtp = otp.Validate(num);
                if (ValidOtp)
                    Console.WriteLine("Success! You are logged in.");
                else                    
                    Console.WriteLine("Invalid OTP. Access denied.");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}