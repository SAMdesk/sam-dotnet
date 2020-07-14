using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAM.DTO
{
    public class SamAppendUploadParams
    {
        /// <summary>
        /// The byte stream representing this part of the file. Cannot be
        /// larger than 10MB or smaller than 5MB. The only exception is if this
        /// is the last part of the upload, which has no minimum size 
        /// requirement.
        /// </summary>
        public byte[] body { get; set; }

        /// <summary>
        /// The index of this part of the file. NOTE: part numbers start at an
        /// index of 1, not 0.
        /// </summary>
        public int part { get; set; }
    }
}
