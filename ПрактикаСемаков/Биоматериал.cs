//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ПрактикаКатаргинНкикита
{
    using System;
    using System.Collections.Generic;
    
    public partial class Биоматериал
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Биоматериал()
        {
            this.Штрихкод1 = new HashSet<Штрихкод>();
        }
    
        public int Id_материала { get; set; }
        public int Штрихкод { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Штрихкод> Штрихкод1 { get; set; }
    }
}
