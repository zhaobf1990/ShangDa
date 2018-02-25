using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new RestClient("http://192.168.9.20:8083/article/split");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "82939a75-ca49-a175-c856-0e660665b4c2");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "application/json");

            //request.AddParameter("application/json", "{\"content\":\"Apache Commons Lang, a .\"}", ParameterType.RequestBody);
            ////IRestResponse response = client.Execute(request);
            ////var content = response.Content;
            //RestResponse<Word> response2 = (RestResponse<Word>)client.Execute<Word>(request);
            //Console.WriteLine(response2.Data.errCode);
            //Console.Read();






            //var client = new RestClient("http://localhost:8083/article/split");
            //// client.Authenticator = new HttpBasicAuthenticator(username, password);

            //var request = new RestRequest("}", Method.POST);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            //// easily add HTTP Headers
            //request.AddHeader("header", "value");

            //// add files to upload (works with compatible verbs)
            //request.AddFile(path);

            //// execute the request
            //IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;

            try
            {
                RestResponse<PassthroughResponse> word = passThrough("160612079", 0, "01 03 00 4A 00 01 A5 DC");
                Console.WriteLine(word.Data.code);
                Console.Read();
            }
            catch (MyException e)
            {
                Console.WriteLine(e.msg);
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            Console.Read();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceId">设备编号</param>
        /// <param name="dataChannel">数据通道 (0:PLC, 1:摄像头, 2:流量计)</param>
        /// <param name="hexCommand">透传命令(十六进制表示的字符串)</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static RestResponse<PassthroughResponse> passThrough(String deviceId, int dataChannel, String hexCommand)
        {
            //接口说明http://116.62.67.134:8080/seem/api/link/#/
            var client = new RestClient("http://116.62.67.134:8080/seem/api/link/device");
            var request = new RestRequest("/" + deviceId + "/passthrough", Method.POST);
            //request.AddHeader("content-type", "application/json");

            //string body = "{\"baudrate\":2,\"parity\":0,\"dataChannel\":" + dataChannel + ",\"hexCommand\":\"" + hexCommand + "\",\"key\":\"90787bcf-6467-461a-8ec7-ce3ced2579de\"}";
            string body = "baudrate=2&parity=0&dataChannel=" + dataChannel + "&hexCommand=" + hexCommand + "&key=90787bcf-6467-461a-8ec7-ce3ced2579de";
            //request.AddParameter("application/json", body, ParameterType.RequestBody);

            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //var content = response.Content;
            DateTime start = new DateTime();
            RestResponse<PassthroughResponse> response2 = (RestResponse<PassthroughResponse>)client.Execute<PassthroughResponse>(request);
            DateTime end = new DateTime();

            if (response2.StatusCode != HttpStatusCode.OK)
            {
                throw new MyException("method:POST  url: " + client.BaseUrl + request.Resource + "?" + body+   "   HttpStatusCode:"+response2.StatusCode.ToString()  );
            }
            if (response2.Data == null) {
                throw new MyException("method:POST   url: " + client.BaseUrl + request.Resource + "?" + body + " +    HttpStatusCode:" + response2.StatusCode.ToString() + "    return is null");
            }
            if (response2.Data.isError()) {
                throw new MyException("method:POST    url: " + client.BaseUrl + request.Resource + "?" + body + "   HttpStatusCode:" + response2.StatusCode.ToString() + "   return:" + response2.Content);
            }
            return response2;
        }
    }
}
