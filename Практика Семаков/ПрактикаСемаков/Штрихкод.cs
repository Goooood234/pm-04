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
    
    public partial class Штрихкод
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Штрихкод()
        {
            this.Заказ = new HashSet<Заказ>();
        }
    
        public int Id_Штрихкода { get; set; }
        public System.DateTime ДатаСоздания { get; set; }
        public int ВидМатериала { get; set; }
        public int Id_услуги { get; set; }
        public int Id_биоматериала { get; set; }
    
        public virtual ВидМатериала ВидМатериала1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказ> Заказ { get; set; }
        public virtual Услуга Услуга { get; set; }
        public virtual Биоматериал Биоматериал { get; set; }
    }
}
