using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UC.ClassDTO.DTOs
{
    public class DtoMenuLink
    {
        public int MenuLnk_ID { get; set; }
        public int MenuLnk_TagID { get; set; }
        public int MenuLnk_FrmTagID { get; set; }
        public int MenuLnk_ActnTagID { get; set; }
        public int MenuLnk_SysTagID { get; set; }
        public int MenuLnk_TypRoutID { get; set; }
        public int MenuLnk_ParntID { get; set; }
        public string MenuLnk_Icon { get; set; }
        public string MenuLnk_NavigaPath { get; set; } 
    }
}
