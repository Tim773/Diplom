﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class AppData
    {
        public static Entities entities = new Entities();

        public static List<Doctors> GetDoctors()
        {           
            return entities.Doctors.ToList();
        }
    }

}
