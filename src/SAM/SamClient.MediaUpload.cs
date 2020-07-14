using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAM.DTO;
using Newtonsoft.Json;

namespace SAM
{
    public partial class SamClient
    {
        /// <summary>
        /// Provides a wrapper method for the following upload methods that automatically
        /// initiates an upload, splits a byte array into appropriately sized parts, and
        /// finishes the upload when all of the parts have been uploaded. NOTE: not optimized
        /// for very large files. It's recommended that you read large files in parts from
        /// disk and use the separate functions below.
        /// </summary>
        /// <param name="fileParams">An object with the file info.</param>
        /// <param name="auth">Your SAM auth object, if not already set up.</param>
        /// <returns>An upload ID string.</returns>
        public string UploadMedia(byte[] bytes, string mimetype, string name = null, SamAuth auth = null)
        {
            var fileParams = new SamStartUploadParams();
            fileParams.mimetype = mimetype;
            fileParams.size = bytes.Length;
            fileParams.name = name;

            double parts = (double)bytes.Length / (double)Constants.PART_SIZE;
            fileParams.parts = (int)Math.Ceiling(parts);

            var uploadResult = StartUpload(fileParams, auth);

            using (var stream = new MemoryStream(bytes))
            {
                var partNumber = 1;
                byte[] part = new byte[Constants.PART_SIZE];

                while (stream.Position < stream.Length)
                {
                    var bytesRead = stream.Read(part, 0, Constants.PART_SIZE);
                    if (bytesRead < Constants.PART_SIZE)
                    {
                        Array.Resize(ref part, bytesRead);
                    }

                    var uploadParameters = new SamAppendUploadParams();
                    uploadParameters.part = partNumber;
                    uploadParameters.body = part;

                    AppendUpload(uploadResult.media_id, uploadParameters, auth);

                    partNumber++;
                }
            }

            CompleteUpload(uploadResult.media_id, auth);

            return uploadResult.media_id;
        }

        /// <summary>
        /// Initializes the upload by creating a persistent upload tracking ID.
        /// </summary>
        /// <param name="fileParams">Describes the file to be uploaded.</param>
        /// <param name="auth">Your SAM auth object, if not already set up.</param>
        /// <returns>An object with the uploaded media_id in it.</returns>
        public SamUpload StartUpload(SamStartUploadParams fileParams, SamAuth auth = null)
        {
            var body = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(fileParams));
            var url = string.Format("{0}/upload.xml", UploadBaseUrl);
            var response = request(url, body, null, auth);
            return Utils.FromXml<SamUpload>(response);
        }

        /// <summary>
        /// Appends a part of a file to an in-progress file upload.
        /// </summary>
        /// <param name="part">The byte array representing a part of the file. Parts must be a 
        /// minimum of 5MB (except for the final part) and a maximum of 10MB.</param>
        /// <param name="partNumber">The index of the part (NOTE: the first part has an index of 1)</param>
        /// <param name="uploadId">The ID received from the InitMediaUpload function.</param>
        /// <param name="auth">Your SAM auth object, if not already set up.</param>
        public void AppendUpload(string mediaId, SamAppendUploadParams parameters, SamAuth auth = null)
        {
            var query = new Dictionary<string, string>() { { "part", parameters.part.ToString() } };
            var url = string.Format("{0}/upload/{1}/append.xml", UploadBaseUrl, mediaId);
            request(url, parameters.body, "POST", "application/octet-stream", query, auth);
        }

        /// <summary>
        /// Finalizes the file upload once all parts have been uploaded.
        /// </summary>
        /// <param name="uploadId">The upload ID string.</param>
        /// <param name="auth">Your SAM authorization object, if not already set up.</param>
        public void CompleteUpload(string mediaId, SamAuth auth = null)
        {
            var url = string.Format("{0}/upload/{1}/complete.xml", UploadBaseUrl, mediaId);
            request(url, null, "POST", "application/json", null, auth);
        }
    }
}
