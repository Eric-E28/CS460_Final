using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HWK6.Models;

[Table("Dlvy")]
public partial class Dlvy
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string DlvyName { get; set; }

    [InverseProperty("Dlvy")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
