﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.DbEntities
{
    public class Advertisement
    {
        [Key]
        public Guid Id { get; init; }

        public bool IsHighlighted { get; set; } = false;

        [Required]
        public ICollection<Jobtask>? Jobtasks { get; set; }

        [Required]
        public Location? Location { get; set; }

        [Required]
        public DateTime DeadlineStart { get; init; }

        [Required]
        public DateTime DeadlineEnd { get; init; }

        [Required]
        public Guid? ApplicationUserId { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}