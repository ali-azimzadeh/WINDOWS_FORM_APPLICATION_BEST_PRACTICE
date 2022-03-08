using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    public abstract class BaseEntity : object
    {
        public BaseEntity() : base()
        {
            Id = 
                System.Guid.NewGuid();
        }

        // **********
        //[System.ComponentModel.DataAnnotations.Key]

        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
            (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]

        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Id))]
        public System.Guid Id { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.InsertDateTime))]
        public System.DateTime InsertDateTime { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.InsertByUser))]
        public System.Guid InsertByUser { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.UpdateDateTime))]
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.UpdateByUser))]
        public Nullable<System.Guid> UpdateByUser { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.DeleteDateTime))]
        public Nullable<System.DateTime> DeleteDateTime { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.DeleteByUser))]
        public Nullable<System.Guid> DeleteByUser { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.IsDeleted))]
        public bool IsDeleted { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.IsActive))]
        public bool IsActive { get; set; }
        // **********
    }
}
