using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAM.DTO
{
    public class SamStartUploadParams
    {
        /// <summary>
        /// The name of the file uploaded. 
        /// 
        /// Optional
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The mimetype of the file. For a list of supported mimetypes, see
        /// the SAM API docs.
        /// </summary>
        public string mimetype { get; set; }

        /// <summary>
        /// The size of the file in bytes.
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// The number of chunks that the upload will be broken into.
        /// </summary>
        public int parts { get; set; }
    }
}
