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
            this.type = type;
            this.carOwner = carOwner;
        }
    }
}
