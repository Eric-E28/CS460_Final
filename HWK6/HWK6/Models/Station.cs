using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HWK6.Models;

[Table("Station")]
public partial class Station
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string StationName { get; set; }

    [InverseProperty("Station")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
