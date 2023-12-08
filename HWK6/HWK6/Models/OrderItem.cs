using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HWK6.Models;

[Table("OrderItem")]
public partial class OrderItem
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    [Column("ItemID")]
    public int? ItemId { get; set; }

    public int? Qty { get; set; }

    public bool? Completed { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("OrderItems")]
    [JsonIgnore]
    public virtual Item Item { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    [JsonIgnore]
    public virtual Order Order { get; set; }
}
