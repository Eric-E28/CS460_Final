using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HWK6.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("StoreID")]
    public int? StoreId { get; set; }

    [Column("DlvyID")]
    public int? DlvyId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string CustomerName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Time { get; set; }

    [ForeignKey("DlvyId")]
    [InverseProperty("Orders")]
    [JsonIgnore]

    public virtual Dlvy Dlvy { get; set; }

    [InverseProperty("Order")]
    [JsonIgnore]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("StoreId")]
    [InverseProperty("Orders")]
    [JsonIgnore]
    public virtual Store Store { get; set; }
}
