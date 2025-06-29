﻿
namespace Entity
{
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string? second_last_name { get; set; }
        public string Phone { get; set; }
        public string sex { get; set; }
        public int idSex { get; set; }
        public string userType { get; set; }
        public int iduserType { get; set; }
        public string photo_name { get; set; }
        public byte[] photo { get; set; }
        public string photoBase64 { get; set; }
        public List<int> likes { get; set; }

    }
}
