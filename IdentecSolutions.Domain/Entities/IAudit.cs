﻿namespace IdentecSolutions.Domain.Entities
{
    public interface IAudit
    {
        public DateTime CreatedAt { get; set; }
        //public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
       // public string UpdatedBy { get; set; }
    }
}
