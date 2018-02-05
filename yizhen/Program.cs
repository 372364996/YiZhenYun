using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace yizhen
{
    class Program
    {
        static void Main(string[] args)
        {
            string tokenUrl = "http://beta.pacs.health-100.cn/amol-back/oauth/token?client_id=amol_client_tpp&client_secret=amol_secret_tpp&grant_type=password&username=tpp_health&password=yizhen_tpp@1130";
            string addInfoUrl = "http://beta.pacs.health-100.cn/amol-back/tppCheck/addInfo?access_token=";
            //string tokenUrl = "https://api.pacs.health-100.cn/amol-back/oauth/token?client_id=amol_client_tpp&client_secret=amol_secret_tpp&grant_type=password&username=tpp_health&password=yizhen_tpp@1130";
            //string addInfoUrl = "https://api.pacs.health-100.cn/amol-back/tppCheck/addInfo?access_token=";
            WebClient client = new WebClient();
            string result = client.DownloadString(tokenUrl);
            var token = JsonConvert.DeserializeObject<Token>(result);
            Console.WriteLine("获取到的token:");
            Console.WriteLine($"AccessToken:{token.access_token}");
            Console.WriteLine($"TokenType:{token.token_type}");
            Console.WriteLine($"ExpiresIn:{token.expires_in}");
            Console.WriteLine($"Scope:{token.scope}");
            addInfoUrl = addInfoUrl + token.access_token;
            Console.WriteLine($"++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.WriteLine($"请求addInfoUrl:{ addInfoUrl}");
            Info info = new Info
            {
                hospitalName = "回龙观医院",
                hospitalCode = "123",
                vid = $"TJ{Guid.NewGuid().ToString("N")}",
                itemId = $"XM{Guid.NewGuid().ToString("N")}",
                itemName = "回龙观医院",
                reportNum = "123"
            };
            string postData = JsonConvert.SerializeObject(info);
            Console.WriteLine($"++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"datas参数：{postData}");
            Console.WriteLine(UploadData(addInfoUrl, postData));
        }
        public static string UploadData(string url, string datas)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                return Encoding.UTF8.GetString(client.UploadData(url,"POST", Encoding.UTF8.GetBytes(datas)));
            }
        }
    }
    public class Token
    {

        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
    }
    public class Info
    {
        public string hospitalName { get; set; }
        public string hospitalCode { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string vid { get; set; }
        public string itemId { get; set; }
        public string itemName { get; set; }
        public string npMark { get; set; }
        public string checkType { get; set; }
        public string checkMethod { get; set; }
        public string checkFingding { get; set; }
        public string checkResult { get; set; }
        public string checkTime { get; set; }
        public string printTime { get; set; }
        public string reportNum { get; set; }
        public string reportSize { get; set; }

    }
    // {"access_token":"8e423b91-1151-42a0-8351-e8d239bae972","token_type":"bearer","expires_in":3490,"scope":"read trust write"}
}
