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
    
    public partial class History
    {
        public int IDHistory { get; set; }
        public int IDPatients { get; set; }
        public int OldDelete { get; set; }
        public int NewDelete { get; set; }
        public System.DateTime DateChange { get; set; }
    
        public virtual Patients Patients { get; set; }
    }
}
