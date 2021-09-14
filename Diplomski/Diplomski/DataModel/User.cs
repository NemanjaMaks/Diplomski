﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class User
    {
        private int id;
        private string name;
        private string lastName;
        private string username;
        private string password;
        private bool isAdmin;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public string LastName { get => lastName; set => lastName = value; }

        public User()
        {
        }

        public User(int id, string name, string lastName, string username, string password, bool isAdmin)
        {
            this.id = id;
            this.name = name;
            this.LastName = lastName;
            this.username = username;
            this.password = password;
            this.isAdmin = isAdmin;
        }

    }
}