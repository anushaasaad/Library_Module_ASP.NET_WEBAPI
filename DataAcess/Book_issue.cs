//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book_issue
    {
        public string student_id { get; set; }
        public string book_id { get; set; }
        public System.DateTime date_issue { get; set; }
        public System.DateTime date_due { get; set; }
    
        public virtual Book_inventory Book_inventory { get; set; }
        public virtual student student { get; set; }
    }
}
