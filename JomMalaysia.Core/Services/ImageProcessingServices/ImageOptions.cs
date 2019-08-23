using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Services.ImageProcessingServices
{
    public class ImageOptions
    {
        /// <summary>
        /// Allowed FIletypes
        /// </summary>
        public string FileTypes { get; set; }
        /// <summary>
        /// Maximum image size
        /// </summary>
        public int MaxSize { get; set; }
        /// <summary>
        /// Thumbail width size 
        /// </summary>
        public int ThumsizeW { get; set; }
        /// <summary>
        /// Thumbail height size 
        /// </summary>
        public int ThumsizeH { get; set; }
        /// <summary>
        /// Generate Thumbnail
        /// </summary>
        public bool MakeThumbnail { get; set; }
        /// <summary>
        /// Base URL
        /// </summary>
        public string ImageBaseUrl { get; set; }
    }
}
