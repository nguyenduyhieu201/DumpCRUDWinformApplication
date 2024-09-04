using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpCRUDWinformApplication
{
    public class Student
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public int Age { set; get; }
        [ForeignKey("Class")]
        public Guid ClassId { set; get; }
        public virtual Class Class { set; get; }
    }
}
