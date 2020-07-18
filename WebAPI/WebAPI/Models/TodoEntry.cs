using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class TodoEntry
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string title { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string entry { get; set; }
    }
}
