namespace GSwmgzCore
{
    public class Result<T>
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 其他消息
        /// </summary>
        public string MsgCode { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

    }
}