using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BmeTodo.Api.Models
{
    /// <summary>
    /// Teendő adatai
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Teendő azonosítója
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Teendő készültsági állapota
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool IsDone { get; set; }

        /// <summary>
        /// Teendő címe
        /// </summary>
        [Required(ErrorMessage = "Cím megadása kötelező")]
        [MaxLength(50, ErrorMessage = "Túl hosszú cím")]
        public string Title { get; set; }

        /// <summary>
        /// Teendő leírása
        /// </summary>
        [Required(ErrorMessage = "Leírás megadása kötelező")]
        [MaxLength(1500, ErrorMessage = "Túl hosszú leírás")]
        public string Description { get; set; }

        /// <summary>
        /// Teendő határideje
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset Deadline { get; set; }

        /// <summary>
        /// Teendő prioritása
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        [Range((int)Priority.High, (int)Priority.Low, ErrorMessage = "Nem megfelelő prioritás érték")]
        public Priority Priority { get; set; }
    }
}
