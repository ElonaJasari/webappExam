using System;
using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models{

public class UserCharacterSelection {
    public int Id { get; set; }

    public String UserId { get; set; } = string.Empty;
    public int CharacterId { get; set; }

    public string CustomName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 } 
}