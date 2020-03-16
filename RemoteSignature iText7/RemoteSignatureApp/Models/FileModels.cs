using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RemoteSignatureApp.Models
{
    public class FileModel
    {
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Selectati document PDF")]
        public HttpPostedFileBase file { get; set; }
    }
    public class FileDetailsModel : IEnumerable<FileDetailsModel>
    {
        public FileDetailsModel() { }
        public FileDetailsModel(int name, string fileName, string signature, string integrity, string signerName, string signatureDate)
        {
            Name = name;
            FileName = fileName;
            Signature = signature;
            Integrity = integrity;
            SignerName = signerName;
            SignatureDate = signatureDate;
        }

        public int Name { get; set; }
        [Display(Name = "Document")]
        public String FileName { get; set; }
        [Display(Name = "Semnatura")]
        public String Signature { get; set; }
        [Display(Name = "Integritate")]
        public String Integrity { get; set; }
        [Display(Name = "Nume semnatar")]
        public String SignerName { get; set; }
        [Display(Name = "Data semnare")]
        public String SignatureDate { get; set; }
        public IEnumerator<FileDetailsModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}