using Dapper.Contrib.Extensions;
using App1.Enums;

namespace App1.Models
{
    public class Medium
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType MediaType { get; set; }
    }
}
