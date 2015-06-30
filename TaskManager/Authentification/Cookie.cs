using System;
using System.Collections.Generic;

namespace TaskManager.Authentification
{
    public class Cookie
    {
        public Cookie()
        {
            Roles = new List<string>();
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool RememberMe { get; set; }
    }
}