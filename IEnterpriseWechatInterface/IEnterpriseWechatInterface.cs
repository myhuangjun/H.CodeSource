using Newtonsoft.Json.Linq;

namespace H.EnterpriseWechatInterface
{
    /// <summary>
    /// 企业微信接口
    /// </summary>
    public interface IEnterpriseWechatInterface
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public string Get_Access_TokenAsync();
        /// <summary>
        /// 推送消息给用户
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="touser">用户账号,多个账号用|隔开</param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public Task<JObject> SendMessageAsync(string message, string touser, string access_token);
        /// <summary>
        /// 推送机器人消息
        /// </summary>
        /// <param name="robotName">机器人名称</param>
        /// <param name="message">消息</param>
        /// <param name="userList">需要@的人</param>
        /// <returns></returns>
        public Task<JObject> SendRobotMessageAsync(string robotName, string message, string userList = "\"@all\"");
    }
}