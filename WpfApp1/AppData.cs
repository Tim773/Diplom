﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class AppData
    {
        public static clinicEntities1 entities = new clinicEntities1();

        public static List<Doctors> GetDoctors()
        {           
            return entities.Doctors.ToList();
        }
    }

}
