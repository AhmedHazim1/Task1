using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Numerics;

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
                Inp = string.IsNullOrEmpty(Inp) == true ? "" : Inp;
                try
                {
                    if (int.TryParse(Inp, out int Num))
                    {
                        if (Num <= MaxValue && Num >= MinValue)
                            return Num;
                        else
                            throw new Exception($"Number must be bettwen {MinValue} : {MaxValue}.");
                    }
                    else
                    {
                        throw new Exception("This isn't a valid number.");
                    }
                }
                catch
                {
                    return 0;
                }
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
            static int ValidUsername(string username)
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
            static int ValidPassword(string password)
            {
                // 1: password is emtpy or null.
                // 2: length less than 6.
                if (string.IsNullOrEmpty(password))
                    return 1;
                if (password.Length < 4)
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
            public bool Validate(string name, string Pass)
            {
                return Username.Equals(name) && Password.Equals(Pass);
            }
        }   
        class Otp
        {
            private int _otpNumber;
            public Otp()
            {
                _otpNumber = new Random().Next(100000, 999999);
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
            string Username = Input.GetStr("Username: ");
            string Password = Input.GetStr("Password: ");

            if (user1.Validate(Username, Password))
            {
                Otp otp = new Otp();
                Console.WriteLine(otp.OtpNumber);
                
                bool ValidOtp = false;
                while(!ValidOtp)
                {
                    int num = Input.GetInt("Enter the OTP Code: ");
                    ValidOtp = otp.Validate(num);

                    if (ValidOtp)
                    {
                        Console.WriteLine("Logged in!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorect OTP, Please try again.");
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Invalid Username Or Password.");
            }
        }
    }

}