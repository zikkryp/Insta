using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Instagram.DataStructure
{
    public class AuthData
    {
        public AuthData()
        {

        }

        public AuthData(int id, string token, bool isActive)
        {
            this.Id = id;
            this.Token = token;
            this.IsActive = isActive;
        }

        [PrimaryKey]
        public int Id { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return this.Token;
        }
    }
}
