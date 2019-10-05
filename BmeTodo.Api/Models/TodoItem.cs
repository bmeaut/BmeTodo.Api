using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BmeTodo.Api.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool IsDone { get; set; }

        [Required(ErrorMessage = "Cím megadása kötelező")]
        [MaxLength(50, ErrorMessage = "Túl hosszú cím")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Leírás megadása kötelező")]
        [MaxLength(1500, ErrorMessage = "Túl hosszú leírás")]
        public string Description { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset Deadline { get; set; }

        [JsonProperty(Required = Required.Always)]
        [Range((int)Priority.High, (int)Priority.Low, ErrorMessage = "Nem megfelelő prioritás érték")]
        public Priority Priority { get; set; }
    }
}
