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
    
    public partial class docrors_timetable
    {
        public int Timetable_id { get; set; }
        public int id_doctor { get; set; }
        public System.TimeSpan Appointment_time { get; set; }
    
        public virtual doctor doctor { get; set; }
    }
}
