using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Events.Models
{
    [MetadataType(typeof(EventMetadata))]
    public partial class Event
    {
    }
    public class EventMetadata
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = "Please provide Event name")]
        public string EventName { get; set; }
        [Required (AllowEmptyStrings = false, ErrorMessage = "Please provide Location name")]
        public string EventLocation { get; set; }
    }
}