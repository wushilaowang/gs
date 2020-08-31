using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GSwmgzCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GSwmgzCore
{

    public class APIController : Controller
    {


        private readonly wmcscpContext _wmcontext;//测评用
        private readonly IHostingEnvironment _hostingEnvironment;

        public APIController(wmcscpContext wmcontext, IHostingEnvironment hostingEnvironment)
        {
            _wmcontext = wmcontext;
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 开始测评,创建不同测评使用不同的AppraisalType
        /// 0   无
        /// 1   文明单位
        /// 2   文明乡镇
        /// 3   文明社区
        /// 4   文明村
        /// 5   文明校园
        /// 6   少年宫
        /// 7   未成年人
        /// 
        /// <param name="AppraisalName">测评名</param>
        /// <param name="CardScore">卡片分，不填为0，之后可通过SetSumScore进行修改</param>
        /// <param name="WJScore">问卷分，不填为0，之后可通过SetSumScore进行修改</param>
        /// </summary>
        /// <returns></returns>
        [Route("api/StartAppraisal")]
        [HttpGet]
        public Result<AppraisalHistory> StartAppraisal(string AppraisalName, double CardScore = 0, double WJScore = 0)
        {
            Result<AppraisalHistory> result = new Result<AppraisalHistory>();

            AppraisalHistory appraisalHistory = _wmcontext.AppraisalHistory.OrderByDescending(x => x.AppraisalCode).FirstOrDefault();
            int count = _wmcontext.AppraisalHistory.Where(x => x.AppraisalName.Contains(AppraisalName)).ToList().Count() + 1;
            string AppraisalType = "0";//初始化用,0无法登录

            //甘肃不让做文明单位和文明社区
            //2020-03-03,发了条消息说又让做了.
            if (AppraisalName.Contains("文明单位"))//不做//又让做了
            {
                AppraisalType = "1";
            }
            else if (AppraisalName.Contains("文明村镇"))
            {
                AppraisalType = "2";
            }
            else if (AppraisalName.Contains("文明社区"))//不做//又让做了
            {
                AppraisalType = "3";
            }
            //else if (AppraisalName.Contains("文明村"))
            //{
            //    AppraisalType = "4";
            //}
            else if (AppraisalName.Contains("文明校园"))
            {
                AppraisalType = "5";
            }
            else if (AppraisalName.Contains("少年宫"))
            {
                AppraisalType = "6";
            }
            else if (AppraisalName.Contains("未成年人"))
            {
                AppraisalType = "7";
            }
            int AppraisalCode = 0;
            if (appraisalHistory != null)
            {
                if (appraisalHistory.Endtime == null)
                {
                    appraisalHistory.Endtime = DateTime.Now;
                    _wmcontext.Update(appraisalHistory);
                }
                AppraisalCode = appraisalHistory.AppraisalCode;
            }


            AppraisalHistory NEWappraisalHistory = new AppraisalHistory { Starttime = DateTime.Now, AppraisalCode = ++AppraisalCode, AppraisalName = DateTime.Now.Year + "第" + count + "次" + AppraisalName, AppraisalType = AppraisalType, CardScore = CardScore, WJScore = WJScore };
            _wmcontext.Add(NEWappraisalHistory);
            _wmcontext.SaveChanges();
            result.Data = NEWappraisalHistory;
            result.MsgCode = "OK";
            result.ErrorMsg = "开始测评";
            return result;
        }


        /// <summary>
        /// 结束测评,增加endtime
        /// </summary>
        /// <returns></returns>
        [Route("api/EndAppraisal")]
        [HttpGet]
        public Result<AppraisalHistory> EndAppraisal()
        {
            Result<AppraisalHistory> result = new Result<AppraisalHistory>();

            AppraisalHistory appraisalHistory = _wmcontext.AppraisalHistory.OrderByDescending(x => x.AppraisalCode).FirstOrDefault();
            appraisalHistory.Endtime = DateTime.Now;
            _wmcontext.Update(appraisalHistory);
            _wmcontext.SaveChanges();
            result.Data = appraisalHistory;
            result.MsgCode = "OK";
            result.ErrorMsg = "测评结束";
            return result;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        ///  <param name="districtCode">行政区划代码</param>//为0时代表后台登陆权限
        ///  <param name="entrance">用户选择的入口</param>用来判断用户是否有访问该测评的权限
        /// <returns></returns>
        [Route("api/Oblogin")]
        [HttpGet]
        public Result<ObSysUser> ObLogin(string account, string password, string districtCode, string entrance)
        {
            Result<ObSysUser> result = new Result<ObSysUser>();
            int CurAppraisalCode = ShowCurrentAppraisalCode();
            if (account == "firstAdmin")//新建空白账户用来初次登陆
            {
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";
                return result;
            }

            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            if (entrance != "")
            {
                CurAppraisalCode = ShowCurrentAppraisalCode(entrance);
            }
            try
            {
                if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                {
                    throw new Exception("账号或密码为空");
                }

                var exist = _wmcontext.ObSysUser.Where(x => x.Account == account.Trim() && x.AppraisalCode == CurAppraisalCode).FirstOrDefault();
                if (account.ToUpper() == "ADMINISTRATOR")//Admin是唯一账号
                {
                    exist = _wmcontext.ObSysUser.Where(x => x.Account == account.Trim()).FirstOrDefault();
                }
                if (exist == null)
                {
                    //如果该次测评没有该账号也算不存在
                    throw new Exception("账户不存在,请查看账号是否输入正确或入口是否选择正确", new Exception("NotFound"));
                }
                var pwd = exist.Password.Trim();
                if (pwd != password.Trim())
                {
                    throw new Exception("密码错误", new Exception("NotFound"));
                }
                var district = exist.DistrictCode;
                string[] arr_districts = district.Split(",");
                if (districtCode == "0")
                {
                    if (exist.Type == 2)
                    {
                        throw new Exception("您没有登陆后台的权限", new Exception("permissionerror"));
                    }

                }
                //测评包含市辖区
                else if (district.Contains("00000000") && !district.Contains(districtCode.Trim().Substring(0, 4)))
                {
                    throw new Exception("定位您当前位置不在测评地区,请点击获取位置后重试", new Exception("areaerror"));
                }
                //普通用户
                else if (!district.Contains("00000000") && !district.Contains(districtCode.Trim().Substring(0, 6)))
                {
                    throw new Exception("定位您当前位置不在测评地区,请点击获取位置后重试", new Exception("areaerror"));
                }

                ObSysUser imanageuser = new ObSysUser { Id = exist.Id, Account = exist.Account, RealName = exist.RealName, DistrictCode = exist.DistrictCode, DistrictName = exist.DistrictName, Type = exist.Type, AppraisalCode = exist.AppraisalCode };
                result.Data = imanageuser;
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                string message = ex.Message;
                if (message.Contains("objec"))
                {
                    message = "当前网络状况较差，请重试。";
                }
                result.ErrorMsg = message;//"当前网络状况较差，请重试。";
            }
            return result;
        }


        /// <summary>
        /// 获取对应县测评状态
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        [Route("api/checkiffinish")]
        [HttpGet]
        public Result<List<CardAllot>> checkiffinish(string districtCode, string entrance, string cardCode = null)
        {

            Result<List<CardAllot>> result = new Result<List<CardAllot>>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            int CurAppraisalCode = ShowCurrentAppraisalCode(entrance);


            try
            {//甘肃要求。卡片无限制，所以cardmaxcount设置为卡片可填
                var data = _wmcontext.CardAllot.Where(x => x.CardMaxCount != 0 && x.AppraisalCode == CurAppraisalCode && x.DistrictCode.Trim().Substring(0, 6) == districtCode.Trim().Substring(0, 6))
                    .ToList();

                if (cardCode != null)
                {
                    data = _wmcontext.CardAllot.Where(x => x.CardMaxCount != 0 && x.AppraisalCode == CurAppraisalCode && x.DistrictCode.Trim().Substring(0, 6) == districtCode.Trim().Substring(0, 6) && x.CardCode.Trim() == cardCode.Trim())
                    .ToList();
                }

                if (data.Count == 0)
                {
                    //如果县区没有测评,则查看市区是否有测评,如果都没有返回测评已完成,否则返回市区尚未完成
                    if (cardCode != null)
                    {
                        data = _wmcontext.CardAllot.Where(x => x.CardMaxCount != 0 && x.AppraisalCode == CurAppraisalCode && x.DistrictCode.Contains("00000000") && x.DistrictCode.Trim().Substring(0, 4) == districtCode.Trim().Substring(0, 4) && x.CardCode.Trim() == cardCode.Trim())
                        .ToList();
                    }
                    else
                    {
                        data = _wmcontext.CardAllot.Where(x => x.CardMaxCount != 0 && x.AppraisalCode == CurAppraisalCode && x.DistrictCode.Contains("00000000") && x.DistrictCode.Trim().Substring(0, 4) == districtCode.Trim().Substring(0, 4))
                    .ToList();
                    }
                    if (data.Count == 0)
                    {
                        result.MsgCode = "OK";
                        result.ErrorMsg = "测评已完成";
                    }
                    else
                    {
                        result.MsgCode = "OK";
                        result.ErrorMsg = "市区尚未完成";
                    }

                }
                else
                {
                    //县区有测评直接返回测评尚未完成
                    result.MsgCode = "OK";
                    result.ErrorMsg = "测评尚未完成";
                }
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }
            return result;
        }


        /// <summary>
        ///上传图片，filename为文件名
        /// </summary>
        [Route("api/uploadpic")]
        [HttpPost]
        public Result<string> Uploadpic()
        {
            Result<string> result = new Result<string>();
            int CurAppraisalCode = ShowCurrentAppraisalCode();

            try
            {
                if (HttpContext.Request.Form.Files.Count == 0)
                {
                    throw new Exception("请上传图片", new Exception("NotFound"));
                }

                string entrance = HttpContext.Request.Form["entrance"].ToString();
                if (entrance == "" || entrance == null)
                {
                    result.MsgCode = "Bad";
                    result.ErrorMsg = "没有测评类型";
                    return result;
                }
                if (entrance != "")
                {
                    CurAppraisalCode = ShowCurrentAppraisalCode(entrance);
                }

                string district = HttpContext.Request.Form["district"];
                // string pOv = HttpContext.Request.Form["POV"];
                var file = HttpContext.Request.Form.Files[0];

                string fileName = "(" + CurAppraisalCode + ")" + HttpContext.Request.Form["name"] + ".jpg";
                
                string rootPath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/cepingresult", district);

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                string rootPath2 = Path.Combine(rootPath, fileName);
                using (FileStream fs = System.IO.File.Create(rootPath2))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                string url = string.Format("{0}://{1}/{2}/{3}", Request.Scheme, Request.Host, district, fileName);

                result.Data = url;
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }
            return result;
        }
        /// <summary>
        ///上传文件
        /// </summary>
        [Route("api/uploadfile")]
        [HttpPost]
        public Result<string> UploadFile()
        {
            Result<string> result = new Result<string>();
            int CurAppraisalCode = ShowCurrentAppraisalCode();
            try
            {
                if (HttpContext.Request.Form.Files.Count == 0)
                {
                    throw new Exception("请上传文件", new Exception("NotFound"));
                }
                string district = HttpContext.Request.Form["district"];
                var file = HttpContext.Request.Form.Files[0];

                string fileName = "(" + CurAppraisalCode + ")" + HttpContext.Request.Form["name"];
                string rootPath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/cepingresult", district);

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                string rootPath2 = Path.Combine(rootPath, fileName);
                using (FileStream fs = System.IO.File.Create(rootPath2))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                string url = string.Format("{0}://{1}/{2}/{3}", Request.Scheme, Request.Host, district, fileName);

                result.Data = url;
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }
            return result;
        }


        /// <summary>
        ///上传调查结果数据
        /// </summary>
        [Route("api/uploaddata")]
        [HttpPost]
        public Result<AppraisalResult> Uploaddata(string numChecks, string picPath, string cardName, string cardCode, string localName, string districtName, string districtCode, string entrance, string beizhu = "", string uploader = "", string inputName = "")
        {
            Result<AppraisalResult> result = new Result<AppraisalResult>();

            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }

            int AppraisalCode = ShowCurrentAppraisalCode(entrance);
            if (cardCode.Contains("K9") || cardName.Contains("问卷"))
            {
                inputName += "调查问卷";
            }
            try
            {
                var cardallot = _wmcontext.CardAllot.Where(x => x.CardCode == cardCode && x.DistrictCode.Trim().Substring(0, 6) == districtCode.Trim().Substring(0, 6) && x.AppraisalCode == AppraisalCode).FirstOrDefault();
                //针对无账号用户提交问卷无法判断是县还是市进行处理

                if (cardallot == null && cardName.Contains("问卷"))
                {
                    //如果用户所在区名找不到对应点,则查看其所在市是否有对应点位,并更改code
                    cardallot = _wmcontext.CardAllot.Where(x => x.CardName.Contains("问卷") && x.DistrictCode.Contains("00000000") && x.DistrictCode.Trim().Substring(0, 4) == districtCode.Trim().Substring(0, 4) && x.AppraisalCode == AppraisalCode).FirstOrDefault();
                    districtCode = districtCode.Substring(0, 4) + "00";
                }
                if (!entrance.Contains("未成年人") && entrance != "7")
                {
                    if (cardName.Contains("问卷"))
                    {
                        int curWJCount = _wmcontext.AppraisalResult.Where(x => x.DistrictCode == districtCode && x.AppraisalCode == AppraisalCode && x.InputName == inputName).Count();
                        if (curWJCount >= cardallot.CardMaxCount)
                        {
                            result.MsgCode = "OK";
                            result.ErrorMsg = "已满";
                            return result;
                        }
                    }
                }

                //未成年人测评卡片数量有要求，按照分配的数量填写
                if (entrance.Contains("未成年人") || entrance == "7")
                {
                    if (cardallot.CardCurCount >= cardallot.CardMaxCount)
                    {
                        result.MsgCode = "OK";
                        result.ErrorMsg = "已满";
                        return result;
                    }
                }

                //未成年人测评卡片数量有要求，按照分配的数量填写
                //避免出现类似于西安市和碑林区被同时测评导致数据上传混乱的问题//虽然一般不会有人这么闲但万一人搞事呢www
                //用户提交上来的districtcode都是已经处理过的，但是districtname需要在这里手动加上辖区两个字，
                //用户区域如果包含市辖区,那他提交的数据就属于市级范围,否则属于当地县/区
                if (!cardCode.Contains("K9"))
                {
                    if (entrance.Contains("村镇") || entrance == "2" || entrance.Contains("单位") || entrance == "1")
                    {
                        var ifexit=_wmcontext.AppraisalResult.Where(x => x.AppraisalCode == AppraisalCode && x.InputName == inputName && x.CardCode==cardCode).FirstOrDefault();
                        if (ifexit != null)
                        {
                            result.MsgCode = "OK";
                            result.ErrorMsg = "已满";
                            return result;
                        }
                    }

                    var uploadUser = _wmcontext.ObSysUser.Where(x => x.RealName == uploader && x.AppraisalCode == AppraisalCode).FirstOrDefault();
                    if (uploadUser.DistrictName.Contains("市辖区"))
                    {
                        //如果用户的区域中包含辖区,代表他测的是整个市而非县/区
                        var cityCodes = _wmcontext.DistrictTb.Where(x => x.DistrictCode.Substring(0, 6) == districtCode.Substring(0, 6)).FirstOrDefault();
                        var owenrname = cityCodes.OwenerCityName + "辖区";
                        cityCodes = _wmcontext.DistrictTb.Where(x => x.DistrictName == owenrname).FirstOrDefault();
                        districtCode = cityCodes.DistrictCode.Substring(0, 6);
                        districtName = cityCodes.DistrictName.Substring(0, cityCodes.DistrictName.Length - 2);
                    }
                }
                //调查问卷偶尔会有界面还没加载完用户就提交数据的情况,因此后台赋值,全部符合
                if (numChecks == "" || numChecks == null)
                {
                    CardOutlin cardOutlin = _wmcontext.CardOutlin.Where(x => x.CardCode == cardCode && x.AppraisalCode == AppraisalCode).FirstOrDefault();
                    int carditemcounts = cardOutlin.cardItemCount;
                    for (int i = 0; i < carditemcounts; i++)
                    {
                        numChecks += "A";
                    }
                }
                //几乎一致,则是重复的/不插入
                var exist = _wmcontext.AppraisalResult.Where(
                    x => x.CardName == cardName.ToString().Trim()
                    && x.DistrictCode.Trim().Substring(0, 6) == districtCode.ToString().Trim().Substring(0, 6)
                    && x.AppraisalCode == AppraisalCode
                    && x.LocalName == localName
                    && x.Checks == numChecks
                    && x.PicPath == picPath
                    && x.Beizhu == beizhu
                    ).FirstOrDefault();
                if (exist != null && !cardName.Contains("问卷"))
                {
                    result.MsgCode = "OK";
                    result.ErrorMsg = "请勿重复提交";
                    return result;
                }

                AppraisalResult adobserverresult = new AppraisalResult { Checks = numChecks, PicPath = picPath.Substring(0, picPath.Length - 1).Trim(), WriteTime = DateTime.Now, CardCode = cardCode, LocalName = localName, CardName = cardName, DistrictName = districtName, DistrictCode = districtCode, Beizhu = beizhu, Uploader = uploader, AppraisalCode = AppraisalCode, InputName = inputName };
                //记录不存在,直接添加
                _wmcontext.Add(adobserverresult);
                cardallot.CardCurCount++;
                _wmcontext.Update(cardallot);
                _wmcontext.SaveChanges();
                result.Data = adobserverresult;
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";
                return result;
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///向已有数据追加内容
        /// </summary>
        [Route("api/Additionaldata")]
        [HttpPost]
        public Result<AppraisalResult> Additionaldata(string picPath, string cardName, string localName, string districtCode, string beizhu, string uploader, string entrance)
        {
            Result<AppraisalResult> result = new Result<AppraisalResult>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            int CurAppraisalCode = ShowCurrentAppraisalCode();
            if (entrance != "")
            {
                CurAppraisalCode = ShowCurrentAppraisalCode(entrance);
            }
            try
            {
                var exist = _wmcontext.AppraisalResult.Where(x => x.CardName == cardName.ToString().Trim() && x.DistrictCode.Trim().Substring(0, 6) == districtCode.ToString().Trim().Substring(0, 6) && x.LocalName == localName && x.AppraisalCode == CurAppraisalCode).FirstOrDefault();

                if (exist != null)
                {
                    //记录已存在
                    if (picPath != "没有图片")
                    {
                        exist.PicPath += "," + picPath.Trim();
                    }
                    if (beizhu != "")
                    {
                        exist.Beizhu += "," + beizhu.Trim();
                    }
                    exist.Uploader += "," + uploader.Trim();
                    _wmcontext.SaveChanges();
                    result.Data = exist;
                    result.MsgCode = "OK";
                    result.ErrorMsg = "成功";
                    return result;
                }
                else
                {
                    result.MsgCode = "OK";
                    result.ErrorMsg = "失败";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
                return result;
            }
        }


        /// <summary>
        /// 获取测评内容
        /// 给小程序用的,默认获取当前测评卡片的内容
        /// </summary>
        /// entrance
        /// <returns></returns>
        [Route("api/getcpcontent")]
        [HttpGet]
        public Result<List<CardContent>> GetCpContent(string cardCode, string entrance = "")
        {
            Result<List<CardContent>> result = new Result<List<CardContent>>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            int CurAppraisalCode = ShowCurrentAppraisalCode(entrance);

            try
            {
                var data = _wmcontext.CardContent.Where(x => x.CardCode.Trim() == cardCode.Trim() && x.AppraisalCode == CurAppraisalCode).OrderBy(x => x.Cixu).ToList();
                result.Data = data;
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 向cardOutlin添加内容(卡片总览)
        /// </summary>
        /// <param name="cardName">卡片名</param>
        /// <param name="cardCode">卡片编号</param>
        /// <param name="cardItemCount">卡片选项数量</param>
        /// 
        /// <returns></returns>
        [Route("api/SetCardList")]
        [HttpGet]
        public Result<CardOutlin> SetCardList(string cardName, string cardCode, int cardItemCount, string entrance)
        {
            int AppraisalCode = ShowCurrentAppraisalCode();


            Result<CardOutlin> result = new Result<CardOutlin>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }

            AppraisalCode = ShowCurrentAppraisalCode(entrance);

            var exist = _wmcontext.CardOutlin.Where(x => x.CardCode == cardCode && x.AppraisalCode == AppraisalCode).FirstOrDefault();

            if (exist != null)
            {
                if (cardCode == "K9001")
                {
                    var cards = _wmcontext.CardOutlin.Where(x => x.CardCode.Substring(0, 2) == "K9").OrderByDescending(x => x.CardCode).FirstOrDefault();
                    cardCode = "K" + (int.Parse(cards.CardCode.Substring(1, cards.CardCode.Length - 1)) + 1).ToString();

                }
                else
                {
                    result.MsgCode = "OK";
                    result.ErrorMsg = "该卡片已存在";
                    result.Data = exist;
                    return result;
                }
            }

            CardOutlin cardOutlin = new CardOutlin { CardName = cardName, CardCode = cardCode, cardItemCount = cardItemCount, AppraisalCode = AppraisalCode };
            _wmcontext.Add(cardOutlin);
            _wmcontext.SaveChanges();
            result.MsgCode = "OK";
            result.ErrorMsg = "成功";
            result.Data = cardOutlin;

            return result;
        }


        /// <summary>
        /// 向卡片内容表里添加文本
        /// </summary>
        /// <param name="cardId">用户点击选取即可</param>
        /// <param name="item">用户输入卡片内容</param>
        /// <param name="beizhu">备注</param>
        /// <param name="cardName">用户点击选取即可</param>
        /// <param name="score">备用</param>
        /// <param name="k50">备用</param>
        /// <param name="k51">备用</param>
        /// <param name="k52">备用</param>
        /// <param name="k53">备用</param>
        /// <param name="k54">备用</param>
        /// <param name="k55">备用</param>
        /// <param name="multisign">是否多用户测评,</param>
        /// <returns></returns>
        [Route("api/SetCardContent")]
        [HttpGet]
        public Result<CardContent> SetCardContent(string cardCode, string item, string beizhu, string cardName, string entrance, double score = 0, double k50 = 0, double k51 = 0, double k52 = 0, double k53 = 0, double k54 = 0, double k55 = 0, int? multisign = 0)
        {
            int AppraisalCode = ShowCurrentAppraisalCode();
            if (entrance != "")
            {
                AppraisalCode = ShowCurrentAppraisalCode(entrance);
            }
            int? cixu = 1;
            Result<CardContent> result = new Result<CardContent>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            var exist = _wmcontext.CardContent.Where(x => x.CardCode == cardCode && x.AppraisalCode == AppraisalCode).OrderByDescending(x => x.Cixu).FirstOrDefault();
            if (exist == null)
            {
                cixu = 1;
            }
            else if (exist.Cixu >= cixu)
            {
                cixu = exist.Cixu + 1;
            }

            CardContent cardContent = new CardContent { Cixu = cixu, CardCode = cardCode, Item = item, Score = score, K50 = k50, K51 = k51, K52 = k52, K53 = k53, K54 = k54, K55 = k55, Beizhu = beizhu, Multisign = multisign, CardName = cardName, AppraisalCode = AppraisalCode };
            _wmcontext.Add(cardContent);
            _wmcontext.SaveChanges();
            result.MsgCode = "OK";
            result.ErrorMsg = "成功";
            result.Data = cardContent;
            return result;
        }

        /// <summary>
        /// 给每个测评点的每张卡片分配数量 
        /// </summary>
        /// <param name="districtCode"></param>
        /// <param name="districtName"></param>
        /// <param name="cardCode"></param>
        /// <param name="cardName"></param>
        /// <param name="cardMaxCount"></param>
        /// <param name="AddOrUpd">1是增加,0是修改</param>
        /// <returns></returns>
        [Route("api/SetCardCount")]
        [HttpGet]
        public Result<CardAllot> SetCardCount(string districtCode, string districtName, string cardCode, string cardName, double cardScore, string entrance, int AddOrUpd = 1, int cardMaxCount = 0)
        {

            Result<CardAllot> result = new Result<CardAllot>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            int AppraisalCode = ShowCurrentAppraisalCode(entrance);

            int cardCurCount = 0;
            var exist = _wmcontext.CardAllot.Where(x => x.CardCode == cardCode && x.AppraisalCode == AppraisalCode && x.DistrictCode.Trim().Substring(0, 6) == districtCode.Trim().Substring(0, 6)).FirstOrDefault();
            if (exist != null && AddOrUpd == 1)//存在且为添加模式
            {
                result.ErrorMsg = "失败，该记录已存在";
                result.MsgCode = "Bad";
                return result;
            }
            else if (exist != null && AddOrUpd == 0)//存在且为修改模式
            {
                exist.DistrictCode = districtCode;
                exist.DistrictName = districtName;
                exist.CardCode = cardCode;
                exist.CardName = cardName;
                exist.CardMaxCount = cardMaxCount;
                exist.CardItemScore = cardScore;

                _wmcontext.Update(exist);
                _wmcontext.SaveChanges();
                result.Data = exist;
                result.ErrorMsg = "成功";
                result.MsgCode = "OK";
                return result;
            }
            CardAllot cardAllot = new CardAllot { DistrictCode = districtCode, DistrictName = districtName, CardCode = cardCode, CardName = cardName, CardMaxCount = cardMaxCount, CardCurCount = cardCurCount, AppraisalCode = AppraisalCode, CardItemScore = cardScore };

            _wmcontext.Add(cardAllot);
            _wmcontext.SaveChanges();


            result.Data = cardAllot;
            result.ErrorMsg = "成功";
            result.MsgCode = "OK";
            return result;
        }


        /// <summary>
        /// 新增或修改用户
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <param name="RealName"></param>
        /// <param name="DistrictCode"></param>
        /// <param name="DistrictName"></param>
        /// <param name="Type"></param>
        /// <param name="AddOrUpd">1是增加,0是修改</param>
        /// <returns></returns>
        [Route("api/SetUser")]
        [HttpGet]
        public Result<ObSysUser> SetUser(string Account, string Password, string RealName, string DistrictCode, string DistrictName, int Type, string entrance, int AddOrUpd = 1)
        {

            Result<ObSysUser> result = new Result<ObSysUser>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            int AppraisalCode = ShowCurrentAppraisalCode(entrance);

            if (Account.ToUpper() == "ADMINISTRATOR" && AddOrUpd == 1)//不允许添加Admin
            {
                result.ErrorMsg = "账号不能为Administrator";
                result.MsgCode = "Bad";
                return result;
            }
            try
            {
                var exist = _wmcontext.ObSysUser.Where(x => x.Account == Account && x.AppraisalCode == AppraisalCode).FirstOrDefault();
                if (exist != null && AddOrUpd == 1)//存在且为添加模式
                {
                    result.ErrorMsg = "失败，用户已存在";
                    result.MsgCode = "Bad";
                    return result;
                }
                else if (exist != null && AddOrUpd == 0)//存在且为修改模式
                {
                    exist.Password = Password;
                    exist.RealName = RealName;
                    exist.DistrictCode = DistrictCode == "" || DistrictCode == null ? exist.DistrictCode : DistrictCode;
                    exist.DistrictName = DistrictName == "" || DistrictName == null ? exist.DistrictName : DistrictName;
                    if (Type == 0)
                    {
                        exist.DistrictCode = exist.DistrictCode == "" || exist.DistrictCode == null ? "0" : exist.DistrictCode + ",0";
                    }
                    exist.Type = Type;
                    _wmcontext.Update(exist);
                    _wmcontext.SaveChanges();
                    result.Data = exist;
                    result.ErrorMsg = "成功";
                    result.MsgCode = "OK";
                    return result;
                }

                if (Type == 0)
                {
                    DistrictCode = DistrictCode + ",0";
                }

                ObSysUser obSysUser = new ObSysUser { Account = Account, Password = Password, RealName = RealName, DistrictCode = DistrictCode, DistrictName = DistrictName, Type = Type, AppraisalCode = AppraisalCode };
                _wmcontext.ObSysUser.Add(obSysUser);
                _wmcontext.SaveChanges();

                result.Data = obSysUser;
                result.ErrorMsg = "成功";
                result.MsgCode = "OK";
                return result;

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                result.MsgCode = "Bad";
                return result;
            }
        }



        /// <summary>
        /// 设置总分
        /// </summary>
        /// <param name="score">总分</param>
        /// <param name="AddOrUpd">1是增加,0是修改</param>
        /// <returns></returns>
        [Route("api/SetSumScore")]
        [HttpGet]
        public Result<AppraisalHistory> SetSumScore(int score, string entrance, string scoreType)
        {

            Result<AppraisalHistory> result = new Result<AppraisalHistory>();

            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            int AppraisalCode = ShowCurrentAppraisalCode(entrance);

            var exist = _wmcontext.AppraisalHistory.Where(x => x.AppraisalCode == AppraisalCode).FirstOrDefault();
            if (exist == null)
            {
                result.ErrorMsg = "暂无任何测评";
                result.MsgCode = "OK";
                return result;
            }
            if (scoreType.Contains("卡片"))//存在且为修改模式
            {
                exist.CardScore = score;

                _wmcontext.Update(exist);
                _wmcontext.SaveChanges();

                result.Data = exist;
                result.ErrorMsg = "成功";
                result.MsgCode = "OK";
                return result;
            }
            else if (scoreType.Contains("问卷"))
            {
                exist.WJScore = score;

                _wmcontext.Update(exist);
                _wmcontext.SaveChanges();
                result.Data = exist;
                result.ErrorMsg = "成功";
                result.MsgCode = "OK";
                return result;
            }
            else
            {

                result.ErrorMsg = "分数类型输入有误，请在卡片和问卷中任选一项输入";
                result.MsgCode = "OK";
                return result;
            }
        }

        /// <summary>
        /// 列出所有县/区
        /// </summary>
        /// <returns></returns>
        [Route("api/ShowAlldistrict")]
        [HttpGet]
        public Result<List<DistrictTb>> ShowAlldistrict()
        {

            Result<List<DistrictTb>> result = new Result<List<DistrictTb>>();
            List<DistrictTb> districtTb = _wmcontext.DistrictTb.ToList();
            result.Data = districtTb;
            result.ErrorMsg = "成功";
            result.MsgCode = "OK";
            return result;
        }
        /// <summary>
        /// 列出所有用户权限
        /// </summary>
        /// <returns></returns>
        [Route("api/ShowAllUserType")]
        [HttpGet]
        public Result<List<UserType>> ShowAllUserType()
        {
            Result<List<UserType>> result = new Result<List<UserType>>();
            List<UserType> userTypes = _wmcontext.UserType.ToList();
            result.Data = userTypes;
            result.ErrorMsg = "成功";
            result.MsgCode = "OK";
            return result;
        }

        /// <summary>
        /// 列出当前测评的所有用户
        /// </summary>
        /// <returns></returns>
        [Route("api/ShowAllUser")]
        [HttpGet]
        public Result<List<ObSysUser>> ShowAllUser(string entrance)
        {
            int AppraisalCode = ShowCurrentAppraisalCode();
            if (entrance != "")
            {
                AppraisalCode = ShowCurrentAppraisalCode(entrance);
            }
            Result<List<ObSysUser>> result = new Result<List<ObSysUser>>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            List<ObSysUser> obSysUser = _wmcontext.ObSysUser.Where(x => x.AppraisalCode == AppraisalCode).ToList();
            result.Data = obSysUser;
            result.ErrorMsg = "成功";
            result.MsgCode = "OK";
            return result;
        }

        /// <summary>
        /// 显示选中测评的卡片列表
        /// </summary>
        /// <returns></returns>
        [Route("api/ShowCardList")]
        [HttpGet]
        public Result<List<CardOutlin>> ShowCardList(string entrance)
        {
            int AppraisalCode = ShowCurrentAppraisalCode();
            if (entrance != "")
            {
                AppraisalCode = ShowCurrentAppraisalCode(entrance);
            }
            Result<List<CardOutlin>> result = new Result<List<CardOutlin>>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            List<CardOutlin> cardOutlin = _wmcontext.CardOutlin.Where(x => x.AppraisalCode == AppraisalCode).ToList();
            result.Data = cardOutlin;
            result.ErrorMsg = "成功";
            result.MsgCode = "OK";
            return result;
        }
        /// <summary>
        /// 获取所有测评卡片的内容
        /// </summary>
        /// <returns></returns>
        [Route("api/ShowCardContent")]
        [HttpGet]
        public Result<List<CardContent>> ShowCardContent(int AppraisalCode = 0)
        {
            if (AppraisalCode == 0)
            {
                AppraisalCode = ShowCurrentAppraisalCode();
            }

            Result<List<CardContent>> result = new Result<List<CardContent>>();

            List<CardContent> cardContent = _wmcontext.CardContent.Where(x => x.AppraisalCode == AppraisalCode).ToList();

            result.Data = cardContent;
            result.ErrorMsg = "成功";
            result.MsgCode = "OK";
            return result;
        }

        /// <summary>
        /// 返回选择的测评的测评结果.默认是全部测评结果,否则返回该地区结果
        /// </summary>
        /// <param name="AppraisalCode"></param>
        [Route("api/GetCpResults")]
        [HttpGet]
        public Result<List<AppraisalResult>> GetCpResults(int AppraisalCode, string districtCode = "")
        {
            Result<List<AppraisalResult>> result = new Result<List<AppraisalResult>>();
            try
            {
                var data = _wmcontext.CardAllot.Where(x => x.CardMaxCount != x.CardCurCount && x.AppraisalCode == AppraisalCode)
                    .ToList();
                var resultList = _wmcontext.AppraisalResult.Where(x => x.AppraisalCode == AppraisalCode).OrderBy(x => x.CardCode).ToList();

                if (districtCode != "" && districtCode.Length > 5)
                {
                    districtCode = districtCode.Substring(0, 6);
                    data = _wmcontext.CardAllot.Where(x => x.CardMaxCount != x.CardCurCount && x.DistrictCode == districtCode && x.AppraisalCode == AppraisalCode)
                    .ToList();


                    resultList = _wmcontext.AppraisalResult.Where(x => x.AppraisalCode == AppraisalCode && x.DistrictCode == districtCode).OrderBy(x => x.CardCode).ToList();
                }

                if (resultList.Count == 0)
                {
                    result.MsgCode = "OK";
                    result.ErrorMsg = "没有数据";
                }
                else if (data.Count != 0)
                {
                    result.Data = resultList;
                    result.MsgCode = "OK";
                    result.ErrorMsg = "尚未完成";
                }
                else if (data.Count == 0)
                {
                    result.Data = resultList;
                    result.MsgCode = "OK";
                    result.ErrorMsg = "成功";
                }

            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }


            return result;
        }

        /// <summary>
        /// 返回所有测评编号
        /// 如果有限制类型,就只返回该类型的所有测评编号
        /// </summary>
        /// <param name="AppraisalCode"></param>
        [Route("api/GetAllAppraisalCode")]
        [HttpGet]
        public Result<List<AppraisalHistory>> GetAllAppraisalCode(string entrance = "")
        {
            if (entrance == null || entrance == "" || entrance == "0")
            {
                entrance = "";
            }
            else if (entrance.Contains("文明单位") || entrance == "1")
            {
                entrance = "文明单位";
            }
            else if (entrance.Contains("文明村镇") || entrance == "2")
            {
                entrance = "文明村镇";
            }
            else if (entrance.Contains("文明社区") || entrance == "3")
            {
                entrance = "文明社区";
            }
            //else if (entrance.Contains("文明村") || entrance == "4")
            //{
            //    entrance = "文明村";
            //}
            else if (entrance.Contains("文明校园") || entrance == "5")
            {
                entrance = "文明校园";
            }
            else if (entrance.Contains("少年宫") || entrance == "6")
            {
                entrance = "少年宫";
            }
            else if (entrance.Contains("未成年人") || entrance == "7")
            {
                entrance = "未成年人";
            }

            Result<List<AppraisalHistory>> result = new Result<List<AppraisalHistory>>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            try
            {
                var data = _wmcontext.AppraisalHistory.ToList();
                if (entrance != "")
                {
                    data = _wmcontext.AppraisalHistory.Where(x => x.AppraisalName.Contains(entrance)).ToList();
                }
                if (data.Count == 0)
                {
                    result.MsgCode = "OK";
                    result.ErrorMsg = "无历史测评记录";
                }
                else
                {
                    result.Data = data;
                    result.MsgCode = "OK";
                    result.ErrorMsg = "成功";
                }
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }
            return result;
        }


        /// <summary>
        /// 查找该测评中所有有数据的县
        /// </summary>
        /// <param name="AppraisalCode"></param>
        [Route("api/GetTheDistricts")]
        [HttpGet]
        public Result<List<districtInfo>> GetTheDistricts(int AppraisalCode)
        {
            Result<List<districtInfo>> result = new Result<List<districtInfo>>();
            try
            {
                var data = _wmcontext.AppraisalResult.Where(x => x.AppraisalCode == AppraisalCode).GroupBy(x => new { x.DistrictCode, x.DistrictName })
                            .Select(x => new districtInfo { districtCodet = x.Key.DistrictCode, districtNamet = x.Key.DistrictName }).ToList();

                if (data.Count != 0)
                {
                    result.Data = data;
                    result.MsgCode = "OK";
                    result.ErrorMsg = "成功";
                }
                else
                {
                    result.MsgCode = "OK";
                    result.ErrorMsg = "失败";
                }
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }
            return result;

        }


        /// <summary>
        /// 根据测评code获取cardAllot
        /// </summary>
        /// <param name="AppraisalCode"></param>
        /// <returns></returns>
        [Route("api/GetCardAllot")]
        [HttpGet]
        public Result<List<CardAllot>> GetCardAllot(int AppraisalCode)
        {
            Result<List<CardAllot>> result = new Result<List<CardAllot>>();
            try
            {
                var exit = _wmcontext.CardAllot.Where(x => x.AppraisalCode == AppraisalCode).ToList();
                result.Data = exit;
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";
            }
            catch (Exception e)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = e.Message;
            }

            return result;
        }
        /// <summary>
        /// 暂时不用，只是一个想法
        /// </summary>
        /// <param name="AppraisalName"></param>
        /// <param name="districtCode"></param>
        /// <param name="entrance"></param>
        /// <returns></returns>
        [Route("api/CalculateScore")]
        [HttpGet]
        public Result<List<CardContent>> CalculateScore(string AppraisalName, string districtCode, string entrance)
        {
            Result<List<CardContent>> result = new Result<List<CardContent>>();


            if (AppraisalName.Contains("未成年"))
            {
                //特殊
            }
            else if (AppraisalName.Contains("村镇"))
            {
                getVillageScore(AppraisalName, districtCode, entrance);
            }
            else if (AppraisalName.Contains("社区"))
            {
                result.Data = showDataWithoutArea(AppraisalName, districtCode, entrance);
            }
            else if (AppraisalName.Contains("单位"))
            {
                result.Data = showDataWithoutArea(AppraisalName, districtCode, entrance);
            }
            else if (AppraisalName.Contains("校"))
            {
                result.Data = showDataWithoutArea(AppraisalName, districtCode, entrance);
            }
            else
            {

            }
            return result;

        }

        private List<CardContent> showDataWithoutArea(string AppraisalName, string districtCode, string entrance)
        {
            //获取卡片列表
            List<CardOutlin> LCardOutlins = ShowCardList(entrance).Data;
            //卡片编号
            int AppraisalCode = _wmcontext.AppraisalHistory.Where(x => x.AppraisalName.Contains(AppraisalName)).Select(x => x.AppraisalCode).FirstOrDefault();
            string startTime = _wmcontext.AppraisalHistory.Where(x => x.AppraisalName.Contains(AppraisalName)).Select(x => x.Starttime).ToString();
            //获取该次测评所有测评结果
            var LCPresults = GetCpResults(AppraisalCode).Data;
            //获取所有卡片内容
            List<CardContent> LCPcontents = ShowCardContent(AppraisalCode).Data;
            List<CardContent> CPRES = new List<CardContent>();


            CardContent tempcontent = new CardContent();
            //计算卡片的分数
            for (int s = 0; s < LCPresults.Count; s++)
            {
                //获取当前记录的基础分值
                double CurCardScore = 0.0;
                double tempscore = 0.00;//记录总分
                double scores = 0;//用逗号隔开每个选项的分值
                string check = "符合";
                for (int i = 0; i < LCPresults[s].Checks.Length; i++)
                {
                    var cardcontent = LCPcontents.Where(x => x.AppraisalCode == LCPresults[s].AppraisalCode && x.CardCode == LCPresults[s].CardCode && x.Cixu == i + 1).First();
                    CurCardScore = LCPcontents.Where(x => x.AppraisalCode == LCPresults[s].AppraisalCode && x.CardCode == LCPresults[s].CardCode && x.Cixu == i + 1)
                    .Select(x => x.Score).FirstOrDefault();

                    if (LCPresults[s].CardName.Contains("问卷"))
                    {
                        CurCardScore = 1.00;
                    }
                    switch (LCPresults[s].Checks.Substring(i, 1))
                    {
                        case "A":
                            check = "A";
                            scores = Math.Round(CurCardScore, 2, MidpointRounding.AwayFromZero);
                            break;
                        case "B":
                            check = "B";
                            scores = Math.Round(CurCardScore * 0.66, 2, MidpointRounding.AwayFromZero);
                            break;
                        case "C":
                            check = "C";
                            scores = Math.Round(CurCardScore * 0.33, 2, MidpointRounding.AwayFromZero);
                            break;
                        case "D":
                            check = "D";
                            scores = 0;
                            break;
                    }
                    tempcontent.AppraisalCode = cardcontent.AppraisalCode;
                    tempcontent.CardCode = cardcontent.CardCode;
                    tempcontent.CardName = cardcontent.CardName;
                    tempcontent.Cixu = cardcontent.Cixu;
                    tempcontent.diORx = cardcontent.diORx;
                    tempcontent.Item = cardcontent.Item;
                    tempcontent.Beizhu = check;
                    tempcontent.Id = cardcontent.Id;
                    tempcontent.Score = scores;
                    tempcontent.K53 = LCPresults[s].Id;
                    CPRES.Add(tempcontent);
                }

            }
            /*
             *统一计算完毕,开始单独计算问卷
             *由于文明测评实地考察包含多种情况无法按地域直接划分,故所有问卷集中在一起,无法分辨,
             *对测评结果按照inputname进行分组,在新的list中保存每个单位的问卷分值
             * 
             */
            List<List<string>> allChecksByItem = new List<List<string>>();

            //记录问卷要分成多少类
            int siteNum = 0;
            for (int j = 0; j < LCardOutlins.Count; j++)
            {
                if (!LCardOutlins[j].CardName.Contains("问卷"))
                {
                    continue;
                }


                var cardSumScores = LCPresults.Where(x => x.CardCode == LCardOutlins[j].CardCode).OrderBy(x => x.InputName).ToList();
                //问卷分组数量
                int s = 0;
                siteNum = cardSumScores.OrderBy(x => x.InputName).GroupBy(x => x.InputName).Count();
                var siteWJNum = cardSumScores.OrderBy(x => x.InputName).GroupBy(x => x.InputName).Select(x => x.Count()).ToList();
                for (int l = 0; l < siteNum; l++)
                {
                    allChecksByItem.Clear();
                    for (int k = 0; k < siteWJNum[l]; k++)
                    {
                        allChecksByItem.Add(cardSumScores[s].Checks.Split(',').ToList());
                        s++;
                    }

                    List<CardContent> WJRES = createWJScoreByVillage(allChecksByItem, j, l, siteWJNum, LCPcontents, cardSumScores, AppraisalName, LCPresults, LCardOutlins);
                    for (int k = 0; k < WJRES.Count; k++)
                    {
                        WJRES[k].K53 = cardSumScores[s].Id;
                    }
                    CPRES.AddRange(WJRES);
                }

            }
            return CPRES;

        }

        private void getVillageScore(string AppraisalName, string districtCode, string entrance)
        {
            //获取卡片列表
            List<CardOutlin> LCardOutlins = ShowCardList(entrance).Data;
            //卡片编号
            int AppraisalCode = _wmcontext.AppraisalHistory.Where(x => x.AppraisalName.Contains(AppraisalName)).Select(x => x.AppraisalCode).FirstOrDefault();
            string startTime = _wmcontext.AppraisalHistory.Where(x => x.AppraisalName.Contains(AppraisalName)).Select(x => x.Starttime).ToString();
            //获取该次测评所有测评结果
            var LCPresults = GetCpResults(AppraisalCode).Data;
            //获取所有卡片内容
            List<CardContent> LCPcontents = ShowCardContent(AppraisalCode).Data;

            for (int s = 0; s < LCPresults.Count; s++)
            {
                //获取当前记录的基础分值
                double CurCardScore = 1;
                double tempscore = 0.00;//记录总分
                string scores = "";//用逗号隔开每个选项的分值
                for (int i = 0; i < LCPresults[s].Checks.Length; i++)
                {
                    CurCardScore = LCPcontents.Where(x => x.AppraisalCode == LCPresults[s].AppraisalCode && x.CardCode == LCPresults[s].CardCode && x.Cixu == i + 1)
                    .Select(x => x.Score).FirstOrDefault();
                    switch (LCPresults[s].Checks.Substring(i, 1))
                    {
                        case "A":
                            tempscore += Math.Round(CurCardScore, 2, MidpointRounding.AwayFromZero);
                            scores += Math.Round(CurCardScore, 2, MidpointRounding.AwayFromZero) + ",";
                            break;
                        case "B":
                            tempscore += Math.Round(CurCardScore * 0.66, 2, MidpointRounding.AwayFromZero);
                            scores += Math.Round(CurCardScore * 0.66, 2, MidpointRounding.AwayFromZero) + ",";
                            break;
                        case "C":
                            tempscore += Math.Round(CurCardScore * 0.33, 2, MidpointRounding.AwayFromZero);
                            scores += Math.Round(CurCardScore * 0.33, 2, MidpointRounding.AwayFromZero) + ",";
                            break;
                        case "D":
                            tempscore += 0;
                            scores += "0,";
                            break;
                    }
                }
                var q = from p in LCPresults
                        where p.Id == LCPresults[s].Id
                        select p;
                foreach (var p in q)
                {
                    p.Checks = scores.Substring(0, scores.Length - 1);
                }
            }

            /*
            *成绩已经变成1,0.66,0.33,和0了
            * 村镇包括问卷共40个选项,分为五个部分
            * 需要一个一个处理,先处理问卷吧
            */
            List<List<string>> allChecksByItem = new List<List<string>>();

            //记录问卷要分成多少类
            int siteNum = 0;
            for (int j = 0; j < LCardOutlins.Count; j++)
            {
                if (!LCardOutlins[j].CardName.Contains("问卷"))
                {
                    continue;
                }
                var cardSumScores = LCPresults.Where(x => x.CardCode == LCardOutlins[j].CardCode).OrderBy(x => x.InputName).ToList();
                //问卷分组数量
                int s = 0;
                siteNum = cardSumScores.OrderBy(x => x.InputName).GroupBy(x => x.InputName).Count();
                var siteWJNum = cardSumScores.OrderBy(x => x.InputName).GroupBy(x => x.InputName).Select(x => x.Count()).ToList();
                for (int l = 0; l < siteNum; l++)
                {
                    allChecksByItem.Clear();
                    for (int k = 0; k < siteWJNum[l]; k++)
                    {
                        allChecksByItem.Add(cardSumScores[s].Checks.Split(',').ToList());
                        s++;
                    }

                    createWJScoreByVillage(allChecksByItem, j, l, siteWJNum, LCPcontents, cardSumScores, AppraisalName, LCPresults, LCardOutlins);

                }
            }
            //这里已经把问卷每项的符合程度计算出来了,之后要把0.66以上的设为A,0.33以上设为B,以下为C
            //一条一条处理吧

            var score_Temp = LCPresults.Where(x => x.AppraisalCode == AppraisalCode && !x.CardName.Contains("问卷")).ToList();
            for (int i = 0; i < score_Temp.Count(); i++)
            {
                //获取该条数据对应的问卷结果
                var temp = LCPresults.Where(x => x.AppraisalCode == AppraisalCode && x.InputName.Contains(score_Temp[i].InputName)
                                                && x.CardName.Contains("问卷")).FirstOrDefault();
                string[] wjcheck;
                if (temp == null)
                {
                    wjcheck = new string[0];
                }
                wjcheck = temp.Checks.Split(',');

                for (int j = 0; j < wjcheck.Count(); j++)
                {
                    if (Convert.ToDouble(wjcheck[j]) >= 0.66)
                    {
                        wjcheck[j] = "1";
                    }
                    else if (Convert.ToDouble(wjcheck[j]) >= 0.66)
                    {
                        wjcheck[j] = "0.66";
                    }
                    else
                    {
                        wjcheck[j] = "0.33";
                    }

                }
                //开始计算该地区总分
                var cardcheck = score_Temp[i].Checks.Split(',');

                //计算第一项分值,共20分,四项符合为A,具体看"G:\欧普\文档\甘肃文明办\(定稿）甘肃省文明村镇测评体系（2020年版）.docx"
                double Ascore = 0;

                if (cardcheck[0] == "1")
                {
                    Ascore++;
                }
                if (cardcheck[1] == "1")
                {
                    Ascore++;
                }
                if (wjcheck[0] == "1")
                {
                    Ascore++;
                }
                if (wjcheck[1] == "1")
                {
                    Ascore++;
                }
                switch (Ascore)
                {
                    case 4:
                        Ascore = 20;
                        break;
                    case 3:
                        Ascore = 13.2;
                        break;
                    case 2:
                        Ascore = 6.6;
                        break;
                    default:
                        Ascore = 0;
                        break;
                }
                //Bscore更加麻烦,一项会分成几种测评方式,但分数却只按一个算,mdzz
                double Bscore = 0;
                if (cardcheck[2] == "1" && wjcheck[2] == "1")
                {
                    Bscore++;
                }
                if (cardcheck[3] == "1" && wjcheck[3] == "1")
                {
                    Bscore++;
                }
                if (cardcheck[4] == "1" && wjcheck[4] == "1")
                {
                    Bscore++;
                }
                if (cardcheck[5] == "1" && wjcheck[5] == "1")
                {
                    Bscore++;
                }
                if (cardcheck[6] == "1" && wjcheck[6] == "1")
                {
                    Bscore++;
                }
                if (cardcheck[7] == "1" && wjcheck[7] == "1")
                {
                    Bscore++;
                }
                if (cardcheck[8] == "1" && cardcheck[9] == "1")
                {
                    Bscore++;
                }
                switch (Bscore)
                {
                    case 7:
                        Bscore = 25;
                        break;
                    case 6:
                        Bscore = 16.5;
                        break;
                    case 5:
                        Bscore = 8.25;
                        break;
                    default:
                        Bscore = 0;
                        break;


                }
                // Cscore同样一项会分成几种测评方式,但分数却只按一个算,mdzz
                double Cscore = 0;
                if (cardcheck[10] == "1")
                {
                    Cscore++;
                }
                if (cardcheck[11] == "1")
                {
                    Cscore++;
                }
                if (cardcheck[12] == "1" && wjcheck[8] == "1")
                {
                    Cscore++;
                }
                if (cardcheck[13] == "1" && wjcheck[9] == "1")
                {
                    Cscore++;
                }

                switch (Cscore)
                {
                    case 4:
                        Cscore = 15;
                        break;
                    case 3:
                        Cscore = 9.9;
                        break;
                    case 2:
                        Cscore = 4.95;
                        break;
                    default:
                        Cscore = 0;
                        break;
                }
                // Dscore同样一项会分成几种测评方式,但分数却只按一个算,mdzz
                double Dscore = 0;
                if (cardcheck[14] == "1" && wjcheck[10] == "1")
                {
                    Dscore++;
                }
                if (cardcheck[15] == "1" && wjcheck[11] == "1")
                {
                    Dscore++;
                }
                if (cardcheck[16] == "1" && wjcheck[12] == "1")
                {
                    Dscore++;
                }
                if (wjcheck[13] == "1")
                {
                    Dscore++;
                }
                switch (Dscore)
                {
                    case 4:
                        Dscore = 20;
                        break;
                    case 3:
                        Dscore = 13.2;
                        break;
                    case 2:
                        Dscore = 6.6;
                        break;
                    default:
                        Dscore = 0;
                        break;
                }
                // Escore同样一项会分成几种测评方式,但分数却只按一个算,mdzz
                double Escore = 0;
                if (cardcheck[17] == "1" && wjcheck[14] == "1")
                {
                    Escore++;
                }
                if (cardcheck[18] == "1" && wjcheck[15] == "1")
                {
                    Escore++;
                }
                if (cardcheck[19] == "1" && wjcheck[16] == "1")
                {
                    Escore++;
                }
                if (cardcheck[20] == "1" && wjcheck[17] == "1")
                {
                    Escore++;
                }
                if (wjcheck[18] == "1")
                {
                    Escore++;
                }
                switch (Escore)
                {
                    case 5:
                        Escore = 20;
                        break;
                    case 4:
                        Escore = 13.2;
                        break;
                    case 3:
                        Escore = 6.6;
                        break;
                    default:
                        Escore = 0;
                        break;
                }
                double SUM = Ascore + Bscore + Cscore + Dscore + Escore;
                var q = from p in LCPresults
                        where p.AppraisalCode == AppraisalCode && p.InputName.Contains(score_Temp[i].InputName)
                        select p;
                foreach (var p in q)
                {
                    p.InputName = p.InputName;
                }
            }

        }
        private List<CardContent> createWJScoreByVillage(List<List<string>> allChecksByItem, int flag, int wjIndex, List<int> WJnums, List<CardContent> LCPcontents, List<AppraisalResult> cardSumScores, string AppraisalName, List<AppraisalResult> LCPresults, List<CardOutlin> LCardOutlins)
        {
            List<CardContent> WJRES = new List<CardContent>();
            //获取问卷的基础分值
            double CurCardScores = 0.0;
            string TempScore = "";
            double cardSumScore = 0.0;

            CardContent tempcontent = new CardContent();
            if (allChecksByItem.Count > 0)
            {
                for (int l = 0; l < allChecksByItem[0].Count; l++)
                {
                    var cardcontent = LCPcontents.Where(x => x.AppraisalCode == cardSumScores[0].AppraisalCode && x.CardCode == cardSumScores[0].CardCode && x.Cixu == l + 1).First();
                    CurCardScores = LCPcontents.Where(x => x.AppraisalCode == cardSumScores[0].AppraisalCode && x.CardCode == cardSumScores[0].CardCode && x.Cixu == l + 1)
                    .Select(x => x.Score).FirstOrDefault();
                    double temp = 0.0;
                    for (int m = 0; m < allChecksByItem.Count; m++)
                    {
                        if (AppraisalName.Contains("校园"))
                        {
                            switch (allChecksByItem[m][l].ToString())
                            {
                                case "0.66":
                                    temp += 0.5;
                                    break;
                                case "1":
                                    temp += 1;
                                    break;
                                default:
                                    temp += 0;
                                    break;
                            }
                        }
                        else
                        if (AppraisalName.Contains("村镇"))
                        {
                            switch (allChecksByItem[m][l].ToString())
                            {
                                case "1":
                                    temp += 1;
                                    break;
                                default:
                                    temp += 0;
                                    break;
                            }
                        }
                        else
                        {
                            temp += double.Parse(allChecksByItem[m][l].ToString());
                        }
                    }

                    cardSumScore = Math.Round(temp * CurCardScores / allChecksByItem.Count, 2);

                    tempcontent.AppraisalCode = cardcontent.AppraisalCode;
                    tempcontent.CardCode = cardcontent.CardCode;
                    tempcontent.CardName = cardcontent.CardName;
                    tempcontent.Cixu = cardcontent.Cixu;
                    tempcontent.diORx = cardcontent.diORx;
                    tempcontent.Item = cardcontent.Item;
                    tempcontent.Id = cardcontent.Id;
                    tempcontent.Score = cardSumScore;
                    WJRES.Add(tempcontent);
                }
            }

            return WJRES;
        }
        ///// <summary>
        ///// 根据测评code,动态生成该次测评所有地区卡片分数
        ///// </summary>
        ///// <param name="AppraisalCode"></param>
        ///// <returns></returns>
        //[Route("api/CreateCardScore")]
        //[HttpGet]
        //public Result<List<CardAllot>> CreateCardScore(int AppraisalCode)
        //{
        //    Result<List<CardAllot>> result = new Result<List<CardAllot>>();


        //    try
        //    {
        //        //分别获取地级和县级有多少种卡片
        //        double dijiCount = 0.0;
        //        double xianjiCount = 0.0;
        //        var dijiexit = _wmcontext.CardOutlin.Where(x => x.AppraisalCode == AppraisalCode && x.diORx == 0 && x.CardCode!="K9001").ToList();
        //        dijiCount = dijiexit.Count;//54
        //        var xianjiexit = _wmcontext.CardOutlin.Where(x => x.AppraisalCode == AppraisalCode && x.diORx == 1 && x.CardCode != "K9001").ToList();
        //        xianjiCount = xianjiexit.Count;//49
        //        //记录地级和县级每类卡片的分数
        //        double dijiCardScore = dijiCount == 0 ? 0 : Math.Round(40.0 / dijiCount,2);//0.74
        //        double xianjiCardScore = xianjiCount == 0 ? 0 : Math.Round(40.0 / xianjiCount,2);//0.82

        //        var ResultAndScores = _wmcontext.CardAllot.Where(x => x.AppraisalCode == AppraisalCode &&x.CardCode!="k9001").ToList();

        //        string cardCode = "";
        //        int diORx = 0;

        //        for (int i = 0; i < ResultAndScores.Count; i++)
        //        {
        //            cardCode = ResultAndScores[i].CardCode;
        //            diORx = ResultAndScores[i].diORx;



        //            switch (diORx)
        //            {
        //                case 0:
        //                    {
        //                        var temp = dijiexit.Where(x => x.CardCode == cardCode).FirstOrDefault();
        //                        ResultAndScores[i].CardItemScore = ResultAndScores[i].CardCurCount == 0 ? 0:Math.Round(dijiCardScore / (ResultAndScores[i].CardCurCount * temp.cardItemCount), 2);
        //                        //避免出现符合但分数过小保留两位为0的情况
        //                        if (ResultAndScores[i].CardItemScore == 0.00)
        //                        {
        //                            ResultAndScores[i].CardItemScore = 0.01;
        //                        }
        //                        break;
        //                    }
        //                case 1:
        //                    {
        //                        var temp = xianjiexit.Where(x => x.CardCode == cardCode).FirstOrDefault();
        //                        ResultAndScores[i].CardItemScore = ResultAndScores[i].CardCurCount == 0 ? 0:Math.Round(xianjiCardScore / (ResultAndScores[i].CardCurCount * temp.cardItemCount), 2);
        //                        if (ResultAndScores[i].CardItemScore == 0.00)
        //                        {
        //                            ResultAndScores[i].CardItemScore = 0.01;
        //                        }
        //                        break;
        //                    }
        //            }
        //            _wmcontext.Update(ResultAndScores[i]);
        //        }

        //        _wmcontext.SaveChanges();
        //        result.Data = ResultAndScores;
        //        result.MsgCode = "OK";
        //        result.ErrorMsg = "成功";
        //    }
        //    catch (Exception e)
        //    {
        //        result.MsgCode = "Bad";
        //        result.ErrorMsg = e.Message;
        //    }

        //    return result;
        //}
        ///// <summary>
        ///// 根据测评code,动态生成该次测评所有地区问卷分数
        ///// </summary>
        ///// <param name="AppraisalCode"></param>
        ///// <returns></returns>
        //[Route("api/GetDisScore")]
        //[HttpGet]
        //public Result<List<string>> GetDisScore(int AppraisalCode,string DistrictCode)
        //{
        //    Result<List<string>> result = new Result<List<string>>();


        //    try
        //    {
        //        //获取问卷有多少选项
        //        double dijiCount = 0.0;
        //        var dijiexit = _wmcontext.CardOutlin.Where(x => x.AppraisalCode == AppraisalCode  && x.CardCode == "K9001").FirstOrDefault();
        //        dijiCount = dijiexit.cardItemCount;
        //        //
        //        var ResultAndScores = _wmcontext.CardAllot.Where(x => x.AppraisalCode == AppraisalCode&&x.DistrictCode.Substring(0,6)== DistrictCode.Substring(0,6)&&x.CardCode=="K9001").FirstOrDefault();
        //        //问卷总数
        //        var wenjuancount = ResultAndScores.CardCurCount;
        //        //记录地级和县级每类卡片的分数
        //        double dijiCardScore = dijiCount == 0 ? 0 : Math.Round(20.0 / wenjuancount, 2);

        //        var results = _wmcontext.AppraisalResult.Where(x => x.AppraisalCode == AppraisalCode && x.CardCode.Substring(0,2)=="K9" && x.DistrictCode.Substring(0, 6) == DistrictCode.Substring(0, 6)).ToList();
        //        //问卷分和卡片数量无关,不需要除,直接算出比例,每个选项分就是20/一张卡片里的项目书.
        //        var cardcontents = _wmcontext.CardContent.Where(x => x.AppraisalCode == AppraisalCode && x.CardCode.Substring(0, 2) == "K9").ToList();
        //        //获取单个选项分
        //        double dijiItemScore = cardcontents.Count == 0 ? 0 : Math.Round(20.0 / cardcontents.Count, 2);
        //        List<string> vs = new List<string>();

        //        double sumscore = 0.0;

        //        for(int i = 0; i < cardcontents.Count; i++)
        //        {
        //            double thisCount = 0;
        //            for(int j = 0; j < results.Count; j++)
        //            {
        //                if (results[j].Checks.Substring(i, 1) != "0")
        //                {
        //                    thisCount++;
        //                }
        //            }
        //            double bili = Math.Round(thisCount / wenjuancount, 2);
        //            if (bili >= cardcontents[i].K50)
        //            {
        //                sumscore += dijiItemScore;
        //            }
        //            else if (bili >= cardcontents[i].K51)
        //            {
        //                sumscore += Math.Round(dijiItemScore / 2, 2);

        //            }
        //            vs.Add(Math.Round(thisCount/ wenjuancount,2).ToString());
        //        }
        //        vs.Add(sumscore.ToString());
        //        result.Data = vs;
        //        result.MsgCode = "OK";
        //        result.ErrorMsg = "成功";
        //    }
        //    catch (Exception e)
        //    {
        //        result.MsgCode = "Bad";
        //        result.ErrorMsg = e.Message;
        //    }

        //    return result;
        //}

        [Route("api/GetWeixinToken")]
        [HttpGet]
        public Result<string> GetWeixinToken()
        {
            Result<string> result = new Result<string>();
            try
            {
                //甘肃省文明城市实地测评
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx69fd5afaa04a4e56&secret=ea8bc7c50245cc690342786bffdcf7c1";


                Uri httpURL = new Uri(url);
                ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换 
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
                ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换 
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容 
                ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理 
                Stream respStream = httpResp.GetResponseStream();
                ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以 
                //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8） 
                StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
                string strBuff = respStreamReader.ReadToEnd();
                JObject jo = (JObject)JsonConvert.DeserializeObject(strBuff);
                string token = jo["access_token"].ToString();



                result.Data = token;
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";

            }
            catch (Exception e)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = e.Message;
            }

            return result;
        }


        [Route("api/CreateQRCode")]
        [HttpGet]
        public Result<byte[]> CreateQRCode(string DistrictCode, string CardCode, string PagePath, string entrance = "")
        {
            Result<byte[]> result = new Result<byte[]>();
            try
            {
                var accessToken = GetWeixinToken().Data;//获取接口AccessToken
                var url = string.Format("https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={0}", accessToken);

                //var postData = "scene=" + DistrictCode + "," + CardCode + "&page=" + PagePath;
                //必须使用json格式上传数据,否则维新会返回格式错误异常
                var postData = "{\"scene\": \"" + DistrictCode + "," + CardCode + "," + entrance + "\",\"page\": \"" + PagePath + "\"}";


                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                byte[] payload;
                payload = System.Text.Encoding.UTF8.GetBytes(postData);
                request.ContentLength = payload.Length;
                Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();


                //得到返回值
                System.Net.HttpWebResponse response;
                response = (System.Net.HttpWebResponse)request.GetResponse();


                System.IO.Stream stream;
                stream = response.GetResponseStream();

                ////异常时用这段查看post请求的原本结果,查看完毕需要注释掉否则会影响下面的代码
                //string data2 = "";

                //////获取响应内容
                ////using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                ////{
                ////    data2 = reader.ReadToEnd();
                ////}

                List<byte> bytes = new List<byte>();
                int temp = stream.ReadByte();
                while (temp != -1)
                {
                    bytes.Add((byte)temp);
                    temp = stream.ReadByte();
                }
                byte[] data = bytes.ToArray();


                result.Data = data;
                result.MsgCode = "OK";
                result.ErrorMsg = "成功";

            }
            catch (Exception e)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = e.Message;
            }

            return result;
        }

        //添加电脑许可
        [Route("api/AddHardwareAuthorization")]
        [HttpGet]
        public Result<HardwareAuthorization> AddHardwareAuthorization(string HardwareCode, string CustomName, string CustomPhone, int licenseState = 0)
        {
            Result<HardwareAuthorization> result = new Result<HardwareAuthorization>();
            try
            {
                var exist = _wmcontext.HardwareAuthorization.Where(x => x.HardwareCode == HardwareCode).FirstOrDefault();
                if (exist != null)
                {
                    result.Data = exist;
                    result.MsgCode = "OK";
                    result.ErrorMsg = "该操作站已授权!";
                }
                else
                {
                    HardwareAuthorization data = new HardwareAuthorization();
                    data.HardwareCode = HardwareCode;
                    data.CustomName = CustomName;
                    data.CustomPhone = CustomPhone;
                    data.LicenseState = licenseState;
                    _wmcontext.HardwareAuthorization.Add(data);
                    _wmcontext.SaveChanges();


                    result.Data = data;
                    result.MsgCode = "OK";
                    result.ErrorMsg = "成功";
                }


            }
            catch (Exception e)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = e.Message;
            }

            return result;
        }

        //查询电脑许可及其状态
        [Route("api/CheckHardwareAuthorization")]
        [HttpGet]
        public Result<HardwareAuthorization> CheckHardwareAuthorization(string HardwareCode)
        {
            Result<HardwareAuthorization> result = new Result<HardwareAuthorization>();
            try
            {
                var exist = _wmcontext.HardwareAuthorization.Where(x => x.HardwareCode == HardwareCode).FirstOrDefault();
                if (exist != null)
                {
                    result.Data = exist;
                    result.MsgCode = "OK";
                    result.ErrorMsg = "该硬件已授权!";
                    if (exist.LicenseState != 0)
                    {
                        result.ErrorMsg = "该硬件授权已失效!";
                    }
                }
                else
                {
                    result.MsgCode = "OK";
                    result.ErrorMsg = "失败";
                }

            }
            catch (Exception e)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = e.Message;
            }

            return result;
        }
        /// <summary>
        /// 删除某个结果数据
        /// 暂时未启用,测评结束再进行调试
        /// </summary>
        [Route("api/deletedata")]
        [HttpPost]
        public Result<AppraisalResult> deletedata(string cardName, string districtName, string updateTime, string AppraisalName)
        {
            Result<AppraisalResult> result = new Result<AppraisalResult>();
            try
            {
                int CurrentAppraisalCode = ShowCurrentAppraisalCode(AppraisalName);

                var resul = _wmcontext.AppraisalResult.Where(x => x.AppraisalCode == CurrentAppraisalCode && x.CardName.Contains(cardName) && x.DistrictName.Contains(districtName) && x.WriteTime.ToString().Contains(updateTime)).ToList();
                if (resul == null)
                {
                    result.MsgCode = "OK";
                    result.ErrorMsg = "无记录";
                    return result;
                }
                int deleteId = 0;
                if (resul.Count > 1)
                {
                    deleteId = resul[0].Id;
                }
                _wmcontext.AppraisalResult.Remove(resul[0]);
                var AllotResult = _wmcontext.CardAllot.Where(x => x.CardName.Contains(cardName) && x.DistrictName.Contains(districtName)).FirstOrDefault();
                if (AllotResult.CardCurCount > 0)
                {
                    AllotResult.CardCurCount--;
                }
                _wmcontext.CardAllot.Update(AllotResult);

                _wmcontext.SaveChanges();


                result.MsgCode = "OK";
                result.ErrorMsg = "成功";
                return result;
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
                return result;
            }
        }


        [Route("api/deleteUser")]
        [HttpGet]
        public Result<ObSysUser> deleteUser(string Account, string entrance)
        {
            Result<ObSysUser> result = new Result<ObSysUser>();
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            int CurrentAppraisalCode = ShowCurrentAppraisalCode();
            if (entrance != "")
            {
                CurrentAppraisalCode = ShowCurrentAppraisalCode(entrance);
            }

            try
            {
                var exit = _wmcontext.ObSysUser.Where(x => x.Account == Account && x.AppraisalCode == CurrentAppraisalCode).FirstOrDefault();
                if (exit != null)
                {
                    _wmcontext.ObSysUser.Remove(exit);
                    _wmcontext.SaveChanges();
                    result.Data = exit;
                    result.ErrorMsg = "成功";
                    result.MsgCode = "OK";

                }
                else
                {
                    result.ErrorMsg = "该账号不存在";
                    result.MsgCode = "OK";
                }

            }
            catch (Exception ex)
            {

                result.ErrorMsg = ex.Message;
                result.MsgCode = "Bad";
            }


            return result;
        }


        /// <summary>
        /// 删除测评，所有关于该次测评的信息都将被删除，包括测评记录
        /// </summary>
        /// <param name="AppraisalName">测评名（汉字）</param>
        /// <returns></returns>
        [Route("api/deleteAppraisal")]
        [HttpPost]
        public Result<List<AppraisalHistory>> deleteAppraisal(string AppraisalName)
        {
            Result<List<AppraisalHistory>> result = new Result<List<AppraisalHistory>>();

            var exist = _wmcontext.AppraisalHistory.Where(x => x.AppraisalName == AppraisalName).First();
            if (exist == null)
            {
                result.ErrorMsg = "无该测评";
                result.MsgCode = "OK";
            }
            //删除测评编号
            var appCode = exist.AppraisalCode;
            _wmcontext.AppraisalHistory.Remove(exist);
            //删除所有测评结果
            var appresult = _wmcontext.AppraisalResult.Where(x => x.AppraisalCode == appCode);
            _wmcontext.AppraisalResult.RemoveRange(appresult);
            //删除卡片分配
            var appallot = _wmcontext.CardAllot.Where(x => x.AppraisalCode == appCode);
            _wmcontext.CardAllot.RemoveRange(appallot);
            //删除卡片内容
            var appcontent = _wmcontext.CardContent.Where(x => x.AppraisalCode == appCode);
            _wmcontext.CardContent.RemoveRange(appcontent);
            //删除卡片列表
            var appoutline = _wmcontext.CardOutlin.Where(x => x.AppraisalCode == appCode);
            _wmcontext.CardOutlin.RemoveRange(appoutline);
            //删除用户
            var appuser = _wmcontext.ObSysUser.Where(x => x.AppraisalCode == appCode);
            _wmcontext.ObSysUser.RemoveRange(appuser);

            _wmcontext.SaveChanges();

            var list = _wmcontext.AppraisalHistory.ToList();
            result.Data = list;
            result.ErrorMsg = "成功";
            result.MsgCode = "OK";
            return result;
        }

        /// <summary>
        /// 获取测评输入名称列表
        /// </summary>
        /// entrance
        /// <returns></returns>
        [Route("api/GetInputName")]
        [HttpGet]
        public Result<List<string>> GetInputName(string entrance = "", string city = "", string district = "")
        {
            Result<List<string>> result = new Result<List<string>>();
            List<string> res = new List<string>();
            int CurAppraisalCode = ShowCurrentAppraisalCode(entrance);
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            try
            {
                if (city == "" || city == null)
                {
                    result.Data = _wmcontext.InputNames.Where(x => x.AppraisalCode == CurAppraisalCode).OrderBy(x=>x.Id).ToList().Select(x=>x.CityName).Distinct().ToList();
                    result.MsgCode = "OK";
                    result.ErrorMsg = "市";
                    return result;
                }
                else if (district == "" || district == null)
                {
                    result.Data = _wmcontext.InputNames.Where(x => x.AppraisalCode == CurAppraisalCode && x.CityName == city).OrderBy(x => x.Id).ToList().Select(m => m.DistrictName).Distinct().ToList();
                    result.MsgCode = "OK";
                    result.ErrorMsg = "县";
                    return result;
                }
                else
                {
                    result.Data = _wmcontext.InputNames.Where(x => x.AppraisalCode == CurAppraisalCode && x.CityName == city && x.DistrictName == district).OrderBy(x => x.Id).ToList().Select(m => m.VillageName).Distinct().ToList();
                    result.MsgCode = "OK";
                    result.ErrorMsg = "点位";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取测评输入名称列表
        /// </summary>
        /// entrance
        /// <returns></returns>
        [Route("api/GetAllInputName")]
        [HttpGet]
        public Result<List<InputNames>> GetAllInputName(string entrance = "")
        {
            Result<List<InputNames>> result = new Result<List<InputNames>>();

            int CurAppraisalCode = ShowCurrentAppraisalCode(entrance);
            if (entrance == "" || entrance == null)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = "没有测评类型";
                return result;
            }
            try
            {

                    result.Data = _wmcontext.InputNames.Where(x => x.AppraisalCode == CurAppraisalCode).ToList();
                    result.MsgCode = "OK";
                    result.ErrorMsg = "成功";
                    return result;

            }
            catch (Exception ex)
            {
                result.MsgCode = "Bad";
                result.ErrorMsg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 展示当前测评编号
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentAppraisalCode()
        {
            //需要增加一个
            AppraisalHistory appraisalHistory = _wmcontext.AppraisalHistory.OrderByDescending(x => x.AppraisalCode).FirstOrDefault();
            if (appraisalHistory == null)
            {
                return 0;
            }
            return appraisalHistory.AppraisalCode;
        }

        /// <summary>
        /// 展示当前测评编号
        /// entrance 有两种情况,一种直接是汉字,判断包含即可
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentAppraisalCode(string entrance)
        {
            if (entrance == null || entrance == "" || entrance == "0")
            {
                entrance = "";
            }
            else if (entrance.Contains("文明单位") || entrance == "1")
            {
                entrance = "文明单位";
            }
            else if (entrance.Contains("文明村镇") || entrance == "2")
            {
                entrance = "文明村镇";
            }
            else if (entrance.Contains("文明社区") || entrance == "3")
            {
                entrance = "文明社区";
            }
            //else if (entrance.Contains("文明村") || entrance == "4")
            //{
            //    entrance = "文明村";
            //}
            else if (entrance.Contains("文明校园") || entrance == "5")
            {
                entrance = "文明校园";
            }
            else if (entrance.Contains("少年宫") || entrance == "6")
            {
                entrance = "少年宫";
            }
            else if (entrance.Contains("未成年人") || entrance == "7")
            {
                entrance = "未成年人";
            }


            //需要增加一个
            AppraisalHistory appraisalHistory = _wmcontext.AppraisalHistory.Where(x => x.AppraisalName.Contains(entrance)).OrderByDescending(x => x.AppraisalCode).FirstOrDefault();
            if (appraisalHistory == null)
            {
                return 0;
            }
            return appraisalHistory.AppraisalCode;
        }

        /// <summary>
        /// 裁剪行政区划代码
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        public string SubDistrictCode(string districtCode)
        {
            string result = districtCode.Substring(0, 6);
            return result;
        }

    }

}
