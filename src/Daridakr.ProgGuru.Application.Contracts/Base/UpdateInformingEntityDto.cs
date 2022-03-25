﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Base
{
    public class UpdateInformingEntityDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string TextInformation { get; set; }
    }
}
