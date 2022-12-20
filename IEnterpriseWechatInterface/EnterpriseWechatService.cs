using HLoggers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEnterpriseWechatInterface
{
    /// <summary>
    /// 企业微信实现类
    /// </summary>
    public class EnterpriseWechatService : IEnterpriseWechatInterface
    {
        private readonly string _cropId;
        private readonly string _corpsecret;
        private readonly string _agent;
        public EnterpriseWechatService(IOptionsSnapshot<WechatSetting> options)
        {
            _cropId = options.Value.Wechat_Corp_Id;
            _corpsecret = options.Value.Wechat_Secret;
            _agent = options.Value.Wechat_AgentId;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string Get_Access_TokenAsync()
        {
            try
            {
                var url = $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={_cropId}&corpsecret={_corpsecret}";
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                    var obj = JObject.Parse(res);
                    var errorCode = obj["errcode"].ToString();
                    if (errorCode != "0") throw new Exception($"请求数据:{url},返回数据:{res}");
                    var access_token = obj["access_token"].ToString();
                    return access_token;
                }
            }
            catch (Exception e)
            {
                Logger.Default.Error("GetAccess_Token报错啦,错误原因:" + e.Message);
                throw;
            }
        }
        /// <summary>
        /// 发送消息给某人
        /// </summary>
        /// <param name="message"></param>
        /// <param name="touser"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<JObject> SendMessageAsync(string message, string touser, string access_token)
        {
            try
            {
                var model = new JObject();
                model["touser"] = touser;
                model["msgtype"] = "text";
                model["agentid"] =_agent;
                model["text"] = new JObject() { ["content"] = message };
                var url = $" https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={access_token}";
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model));
                    var response = await client.PostAsync(url, content);
                    var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                    var errorCode = res["errcode"].ToString();
                    if (errorCode != "0") throw new Exception($"发送消息:{url},返回数据:{res}");
                    return res;
                }
            }
            catch (Exception e)
            {
                Logger.Default.Error("SendMessage报错啦,错误原因:" + e.Message);
                throw;
            }
        }
        /// <summary>
        /// 发送消息给机器人
        /// </summary>
        /// <param name="robotName"></param>
        /// <param name="message"></param>
        /// <param name="userList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public async Task<JObject> SendRobotMessageAsync(string url, string message, string userList = "\"@all\"")
        {
            try
            {
                var requestBody = "{\"msgtype\":\"text\",\"text\":{\"content\":\"" + message + "\",\"mentioned_mobile_list\":[" + userList + "]}}";
                using (var client = new HttpClient())
                {
                    var content = new StringContent(requestBody);
                    var response = await client.PostAsync(url, content);
                    var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                    var errorCode = res["errcode"].ToString();
                    if (errorCode != "0") throw new Exception($"推送机器人报错:{url},返回数据:{res}");
                    return res;
                }
            }
            catch (Exception e)
            {
                Logger.Default.Error("SendRobotMessage报错了,错误原因:" + e.Message);
                throw;
            }
            
        }
    }
}
