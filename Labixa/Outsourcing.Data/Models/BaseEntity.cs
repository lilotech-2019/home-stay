﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool Deleted { get; set; }
        public bool Status { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? LastModify { get; set; }
    }
}