﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HWK6.Models;

[Table("Item")]
public partial class Item
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; }

    public double Price { get; set; }

    [Column("StationID")]
    public int StationId { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("StationId")]
    [InverseProperty("Items")]
    public virtual Station Station { get; set; }
}
