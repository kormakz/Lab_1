using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Things
{
    public class Mouse:Thing
    {
        protected string color;
        protected string brand;
        protected int sensitivity;
        public override string ToString()
        {
            return 
                  "Мышь: " 
                + name
                + '\n'
                + "Цвет: " + color  
                + '\n' 
                + "Производитель/бренд: " + brand 
                + '\n' 
                + "Чувствительность: " + sensitivity 
                + "\n"
                + base.ToString();
        }
        public Mouse(int id, string name,string color, string brand, int sensitivity):base(id,name) {
            this.color = color;
            this.brand = brand;
            this.sensitivity = sensitivity;
        }

    }
}
