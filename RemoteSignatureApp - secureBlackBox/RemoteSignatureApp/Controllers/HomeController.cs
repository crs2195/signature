

using iText.Signatures;
using log4net;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using RemoteSignatureApp.Classes;
using RemoteSignatureApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace RemoteSignatureApp.Controllers
{
    public class HomeController : Controller
    {
        CSC_API_Client clt = new CSC_API_Client();
        ILog logger = LogManager.GetLogger("DebugLogger");
        public async System.Threading.Tasks.Task<ActionResult> Index(string code = null)
        {
            await clt.OAuth2_Authorize();
            if (code != null)
            {
                string access_token = await clt.OAuth2_Token(code);
                Session["AccessToken"] = access_token;
                ViewBag.State = "ListCredentials";
                return View("Index");
            }
            else
            {
                if (TempData["Message"] != null || TempData["State"] != null)
                {
                    if (TempData["Message"] != null)
                    {
                        ViewBag.Message = TempData["Message"];
                    }

                    if (TempData["State"] != null)
                    {
                        ViewBag.State = TempData["State"];


                    }
                    return View("Index");
                }

                ViewBag.State = "SelectFile";
                return View("Index");
            }

        }
        public ActionResult Authorize(System.Web.HttpPostedFileBase file)
        {

            if (Path.GetExtension(file.FileName).ToUpper() == ".PDF")
            {

                string folderPath = Server.MapPath("~/uploads/");
                string targetPath = Path.Combine(folderPath, file.FileName);
                file.SaveAs(targetPath);
                Session["inPath"] = targetPath;
                Session["outPath"] = string.Format("{0}.pdf", targetPath.Substring(0, targetPath.LastIndexOf(".")) + "Signed");
                Session["Filename"] = targetPath;

                return Redirect("authorizatioServerHere");

            }
            else
            {
                TempData["Message"] = "Fisierul nu este in formatul corespunzator (pdf).";
                return RedirectToAction("Index");

            }


        }
        public async System.Threading.Tasks.Task<ActionResult> ListCredentials()
        {
            try
            {
                List<CredentialViewModel> credentialList = new List<CredentialViewModel>();
                string access_token = (string)Session["AccessToken"];
                List<string> credentialIds = await clt.Credentials_List(access_token);

                foreach (var cred in credentialIds)
                {
                    var credential = await clt.Credentials_Info(access_token, cred);

                    credentialList.Add(new CredentialViewModel(cred, credential.description.Substring(credential.description.LastIndexOf(":") + 1), credential.cert.status));

                }


                return PartialView("CredentialPartial", credentialList);
            }
            catch (Exception e)
            {
                TempData["Message"] = "A aparut o eroare la afisarea credentialelor";
                logger.Error(e.Message);
                return PartialView("CredentialPartial", null);
            }
        }
        public async System.Threading.Tasks.Task<ActionResult> SendOTP(string cred)
        {
            try
            {
                Session["Credential"] = cred;
                string access_token = (string)Session["AccessToken"];
                if (cred != null)
                {
                    var credential = await clt.Credentials_Info(access_token, cred);
                    clt.Credentials_SendOTP(access_token, cred);

                    TempData["State"] = "EnterCredentials";
                    ViewBag.State = "EnterCredentials";
                    return View("Index");
                }
                else
                {
                    TempData["Message"] = "Nu ati selectat niciun credential pentru semnare";
                    return View("Index");
                }

            }
            catch (Exception e)
            {
                TempData["Message"] = "A aparut o eroare la solicitarea OTP-ului.";
                logger.Error(e.Message);
                return View("Index");
            }
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> SignFile(string otp, string password)
        {
            string access_token = (string)Session["AccessToken"];
            string cred = (string)Session["Credential"];
            if (otp != null && password != null)
            {
                string certificate = await clt.Credentials_Info_Cert(access_token, cred);
                
                BlackBoxSignature signature = new BlackBoxSignature(certificate, (string)Session["inPath"],(string)Session["outPath"],access_token, cred, password, otp);
                signature.SignPDF();

            }
            return View("Index");
        }



        public ActionResult DocumentInfo()
        {
           
                return PartialView("FileDetailsPartial", null);

        }
        public ActionResult DownloadSignedFile()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes((string)Session["outPath"]);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "SignedFile.pdf");
        }

        public ActionResult ReloadTransactions()
        {
            TempData["State"] = "SelectFile";
            ViewBag.State = "SelectFile";
            return View("Index");
        }


    }
}