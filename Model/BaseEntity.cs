using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public abstract class Entity : object
    {
        public Entity() : base()
        {
            Id = 
                System.Guid.NewGuid();
        }

        // **********
        //[System.ComponentModel.DataAnnotations.Key]

        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
            (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]

        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(DataDictionary),
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
            (ResourceType = typeof(Resourcesx.DataDictionary),
            Name = nameof(Resourcesx.DataDictionary.UpdateDateTime))]
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resourcesx.DataDictionary),
            Name = nameof(Resourcesx.DataDictionary.UpdateByUser))]
        public Nullable<System.Guid> UpdateByUser { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resourcesx.DataDictionary),
            Name = nameof(Resourcesx.DataDictionary.DeleteDateTime))]
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
