using System;
using System.ComponentModel.DataAnnotations;

namespace FirstMVC.models{

public class UserCharacterSelection {
    public int Id { get; set; }

    public String UserID { get; set; } = default;
    public int CharacterId { get; set; }

    public string CustomName { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 } 
}