using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Client
{
    class IsConnect
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool isConnect { get; set; }
    }
}
