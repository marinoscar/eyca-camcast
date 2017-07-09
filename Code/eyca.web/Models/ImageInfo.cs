using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eyca.web.Models
{
    public class ImageInfo
    {
        public string ImageId { get; set; }

        public override string ToString()
        {
            return ImageId;
        }
    }
}