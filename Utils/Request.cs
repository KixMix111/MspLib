using FluorineFx.IO;
using MspLib.Autorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MspLib.Class;
using Newtonsoft.Json.Linq;

namespace MspLib.Utils
{
    internal class Request
    {
        public static async Task<MspResponse> SendPacketToMspApiAsync(string Method, string Server, object[] Content, string Proxy = null)
        {
            Random random = new Random();
            string ip = random.Next(0, 213) + "." + random.Next(0, 146) + "." + random.Next(0, 50) + "." + random.Next(0, 30);
            MspResponse mspResponse = new MspResponse();
            AMFMessage aMFMessage = new AMFMessage(3);
            aMFMessage.AddHeader(new AMFHeader("sessionID", false, ChecksumCalculator.generateID()));
            aMFMessage.AddHeader(new AMFHeader("id", false, ChecksumCalculator.createChecksum(Content)));
            aMFMessage.AddHeader(new AMFHeader("needClassName", false, true));
            aMFMessage.AddBody(new AMFBody(Method, "/1", Content));
            MemoryStream memoryStream = new MemoryStream();
            AMFSerializer aMFSerializer = new AMFSerializer(memoryStream);
            aMFSerializer.WriteMessage(aMFMessage);
            aMFSerializer.Flush();
            ByteArrayContent byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
            HttpClient httpClient = new HttpClient();
            if (Proxy != null)
            {
                httpClient = new HttpClient(new HttpClientHandler()
                {
                    Proxy = new WebProxy($"http://{Proxy}"),
                    UseProxy = true
                });
            }
            httpClient.DefaultRequestHeaders.Add("Referer", "https://inx.msp/");
            httpClient.DefaultRequestHeaders.Add("X-Forwarded-For", ip);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-amf");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync($"https://ws-{Server.ToLower()}.mspapis.com/Gateway.aspx?method={Method}", byteArrayContent);
            mspResponse.Status = (int)httpResponseMessage.StatusCode;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                mspResponse.Status = (int)httpResponseMessage.StatusCode;
                mspResponse.Success = true;
                byte[] response = await httpResponseMessage.Content.ReadAsByteArrayAsync();
                mspResponse.Response = DecodeMspResponse(response);
            }
            else
            {
                mspResponse.Status = (int)httpResponseMessage.StatusCode;
                mspResponse.Success = false;
                mspResponse.Response = null;
            }
            httpClient.Dispose();
            return mspResponse;
        }
        private static dynamic DecodeMspResponse(byte[] body)
        {
            MemoryStream stream = new MemoryStream(body);
            AMFDeserializer aMFDeserializer = new AMFDeserializer(stream);
            AMFMessage aMFMessage = aMFDeserializer.ReadAMFMessage();
            return aMFMessage.Bodies[0].Content;
        }
    }
}
