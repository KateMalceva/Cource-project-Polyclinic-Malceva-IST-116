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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class doctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public doctor()
        {
            this.appointment = new HashSet<appointment>();
            this.docrors_timetable = new HashSet<docrors_timetable>();
            this.making_appointment = new HashSet<making_appointment>();
            this.prescription = new HashSet<prescription>();
        }
    
        public int Doctor_id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime Date_of_birth { get; set; }
        public int Phone_number { get; set; }
        public int id_specialty { get; set; }
        public string Education { get; set; }

        [NotMapped]
        public List<list_of_specialty> ListSpetialty { get; set; }
      

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointment> appointment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<docrors_timetable> docrors_timetable { get; set; }
        public virtual list_of_specialty list_of_specialty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<making_appointment> making_appointment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prescription> prescription { get; set; }
    }
}
