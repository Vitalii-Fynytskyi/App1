using App1.Enums;
using Dapper.Contrib.Extensions;

namespace App1.Models
{
    public class MediaItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MediumId => MediumInfo.Id;
        public ItemType MediaType { get; set; }
        [Computed]
        public Medium MediumInfo { get; set; }
        public LocationType Location { get; set; }
    }
}
