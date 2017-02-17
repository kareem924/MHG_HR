using System;
using System.ComponentModel.DataAnnotations;

namespace mhg_internalnet2.ViewModel
{
    public class UserDocumentsViewModel
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentHashName { get; set; }
        [Required]
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int UserId { get; set; }
    }
}