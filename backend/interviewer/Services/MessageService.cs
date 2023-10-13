using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using interviewer.Data;
using StackExchange.Redis;
using Tea;

namespace interviewer.Services;

class MessageService : MessageServiceBase
{
    public MessageService(IConfiguration configuration, MessageSettings messageSettings)
    {
        _configuration = configuration;
        _messageSettings = messageSettings;
        var redisAddress = _configuration["RedisAddress"] ?? throw new Exception("找不到 RedisAddress 的配置");
        _redis = ConnectionMultiplexer.Connect(redisAddress);
        _redisDb = _redis.GetDatabase();
        _random = new Random();
    }

    private readonly IConfiguration _configuration;
    private readonly MessageSettings _messageSettings;
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _redisDb;
    private readonly Random _random;

    public override async Task<Result> SendVerificationCode(string phoneNumber)
    {
        RedisValue redisValue = _redisDb.StringGet(phoneNumber + "_time");
        if (redisValue.HasValue)
        {
            int seconds = (int)DateTime.UtcNow.Subtract(new DateTime((long)redisValue)).TotalSeconds;
            return new Result()
            {
                Data = null,
                ErrorMessages = new[]
                    { "请勿频繁发送验证码，请在" + (int)(_messageSettings.SendCodeInterval.TotalSeconds - seconds) + "秒后重试" }
            };
        }

        string code = RandomNumber(4);
        if (!SendVerifyCode(phoneNumber, code))
            return new Result() { Data = null, ErrorMessages = new[] { "验证码发送失败" } };
        
        _redisDb.StringSet(phoneNumber, code, _messageSettings.CodeExpireIn);
        _redisDb.StringSet(phoneNumber + "_time", DateTime.UtcNow.Ticks, _messageSettings.SendCodeInterval);

        return new Result() { Data = "验证码已发送" };
    }

    public override async Task<bool> VerifyCode(string phoneNumber, string code)
    {
        RedisValue redisValue = await _redisDb.StringGetAsync(phoneNumber);
        if (!redisValue.HasValue || redisValue.ToString() != code)
            return false;

        _redisDb.KeyDelete(phoneNumber);
        _redisDb.KeyDelete(phoneNumber + "_time");

        return true;
    }

    private string RandomNumber(int length)
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }


    private AlibabaCloud.SDK.Dysmsapi20170525.Client CreateClient(string accessKeyId, string accessKeySecret)
    {
        AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
        {
            // 必填，您的 AccessKey ID
            AccessKeyId = accessKeyId,
            // 必填，您的 AccessKey Secret
            AccessKeySecret = accessKeySecret,
        };
        // Endpoint 请参考 https://api.aliyun.com/product/Dysmsapi
        config.Endpoint = "dysmsapi.aliyuncs.com";
        return new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
    }

    private bool SendVerifyCode(string phoneNumber, string code)
    {
        AlibabaCloud.SDK.Dysmsapi20170525.Client client =
            CreateClient(_configuration["AlibabaCloudAccessKeyID"] ?? throw new InvalidOperationException("AlibabaCloudAccessKeyID 未配置"),
                _configuration["AlibabaCloudAccessKeySecret"] ?? throw new InvalidOperationException("AlibabaCloudAccessKeySecret 未配置"));
        AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendSmsRequest =
            new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
            {
                PhoneNumbers = phoneNumber,
                SignName = "gdutelc",
                TemplateCode = "SMS_463662125",
                TemplateParam = "{\"code\":\"" + code + "\"}"
            };
        try
        {
            // 复制代码运行请自行打印 API 的返回值
            SendSmsResponse sendSmsResponse = client.SendSmsWithOptions(sendSmsRequest, new AlibabaCloud.TeaUtil.Models.RuntimeOptions());
            if(sendSmsResponse.Body.Code!= "OK") return false;
            
        }
        catch (TeaException error)
        {
            // 如有需要，请打印 error
            AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            return false;
        }
        catch (Exception _error)
        {
            TeaException error = new TeaException(new Dictionary<string, object>
            {
                { "message", _error.Message }
            });
            // 如有需要，请打印 error
            AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            return false;
        }

        return true;
    }
}