//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResearchAppointment
    {
        public int IDResearchAppointment { get; set; }
        public int IDAppointment { get; set; }
        public int IDDocResearch { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual Appointment Appointment { get; set; }
        public virtual DocResearch DocResearch { get; set; }
    }
}
