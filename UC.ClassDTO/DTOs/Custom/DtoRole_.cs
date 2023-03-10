

namespace UC.ClassDTO.DTOs.Custom
{
    public class DtoRole_
    {
        public int Rol_ID { get; set; }
        public int Rol_TagID { get; set; }
        public int Rol_MemberID { get; set; }
        public string Rol_TagName { get; set; }
        public string TransTagText { get; set; }

        public bool Rol_IsSystemRole { get; set; }
        public bool Rol_ReadOnly { get; set; }

    }
}
