using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Cryptography;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using System.Xml;
using LogOutUser.Core;

namespace LogOutUser.Controllers
{
    //[Authorize]
    public class DataController : ApiController
    {
        private static RSACryptoServiceProvider _rsa;
        private static RSAParameters _publicKey;
        private static RSAParameters _privateKey;


        public DataController()
        {
            _rsa = new RSACryptoServiceProvider(1024)
            {

                PersistKeyInCsp = false
            };
            _privateKey = _rsa.ExportParameters(true);
        }
        [CustomActionFilter]
        [HttpGet]
        public HttpResponseMessage Data()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, new {id = 10});
        }
        [HttpPost]
        public HttpResponseMessage Decrypt([FromBody]string data)
        {
            var bytes = Convert.FromBase64String(data);
            _rsa.ImportParameters(_privateKey);
            var message = _rsa.Decrypt(bytes, false);

            return Request.CreateResponse(HttpStatusCode.OK, Encoding.UTF8.GetString(message));

        }
        [HttpGet]
        public PublicKey GetPublicKey()
        {
            _publicKey = _rsa.ExportParameters(false);
            var xml = _rsa.ToXmlString(false);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.FirstChild;


            var data = _rsa.Encrypt(Encoding.UTF8.GetBytes("Hello"),false);
            var bytes = data;
            _rsa.ImportParameters(_privateKey);
            var message = _rsa.Decrypt(bytes, false);
            return new PublicKey()
            {
                Exponent = ConvertBase64ToHex(Convert.ToBase64String(_publicKey.Exponent)),
                Modules = ConvertBase64ToHex(Convert.ToBase64String(_publicKey.Modulus)),
                PK = Encoding.UTF8.GetString(message),
                Size = _rsa.KeySize
            };
        }
        private string ConvertBase64ToHex(string base64EncodedString)
        {
            return ByteArrayToString(System.Convert.FromBase64String(base64EncodedString));
        }

        private string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        [HttpGet]
        public HttpResponseMessage Xml()
        {
            var path = Path.Combine(HostingEnvironment.MapPath("~/"),"data.xml");
            var data = File.ReadAllText(path);
            return Request.CreateResponse(HttpStatusCode.Accepted, string.Empty,new XmlMediaTypeFormatter());
        }
        [HttpGet]
        public HttpResponseMessage Json()
        {
            var path = Path.Combine(HostingEnvironment.MapPath("~/"), "data.xml");
            var data = File.ReadAllText(path);
            return Request.CreateResponse(HttpStatusCode.Accepted, new { data = string.Empty });
        }

    }
}
