using System;
using System.Collections.Generic;
using System.Text;

namespace DBFrameWork.Models
{
    public class Mark
    {
        private int id;
        private string type;
        private string carOwner;

        public Mark(string type, string carOwner)
        {
            this.Type = type;
            this.CarOwner = carOwner;
        }

        public int Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public string CarOwner { get => carOwner; set => carOwner = value; }
    }
}
