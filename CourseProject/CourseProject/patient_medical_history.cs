//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class patient_medical_history
    {
        public int Medical_history_id { get; set; }
        public int id_patient { get; set; }
        public int id_appointment { get; set; }
    
        public virtual appointment appointment { get; set; }
        public virtual patient patient { get; set; }
    }
}
