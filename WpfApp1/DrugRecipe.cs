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
    
    public partial class DrugRecipe
    {
        public int IDDrugRecipe { get; set; }
        public int IDDrug { get; set; }
        public int IDRecipe { get; set; }
    
        public virtual Drug Drug { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}