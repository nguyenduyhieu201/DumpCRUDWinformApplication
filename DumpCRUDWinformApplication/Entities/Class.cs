using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpCRUDWinformApplication
{
    public class Class
    {
        public Guid Id { set; get; }
        public string ClassCode { set; get; }
        public virtual ICollection<Student> Students { set; get; }
    }
}
