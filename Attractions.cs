//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducPractVer2._00
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attractions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attractions()
        {
            this.AttractionType = new HashSet<AttractionType>();
            this.Receipt = new HashSet<Receipt>();
        }
    
        public int attraction_id { get; set; }
        public string attraction_name { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> fear_level_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttractionType> AttractionType { get; set; }
        public virtual FearLevels FearLevels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receipt> Receipt { get; set; }
    }
}
