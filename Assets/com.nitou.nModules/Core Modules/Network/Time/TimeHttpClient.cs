using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace nitou.Networking{

    //public class TimeHttpClient {
    //    private readonly IHttpClient _httpClient;

    //    // World Time API ��URL
    //    internal const string WorldTimeApiUrl = "http://worldtimeapi.org/api/timezone/Etc/UTC";

    //    public TimeHttpClient(IHttpClient httpClient) {
    //        _httpClient = httpClient;
    //    }

    //    // ���ݎ������擾���郁�\�b�h�i�p�[�X���\�b�h�������œn���j
    //    public async UniTask<(HttpRequest.Result result, TimeResponse response)> GetCurrentTimeAsync(CancellationToken token, Func<string, TimeResponse> parseMethod) {
    //        var request = new TimeHttpRequest();  // �������N�G�X�g���쐬
    //                                              // �p�[�X���\�b�h��n���Č��ݎ������擾
    //        var (result, response) = await timeClient.GetCurrentTimeAsync(
    //            cancellationToken,
    //            responseData => JsonUtility.FromJson<TimeResponse>(responseData)  // JsonUtility ���g�p���ăp�[�X
    //        );

    //        var (result, response) = await _httpClient.SendAsync(request, token, parseMethod);  // �p�[�X���\�b�h���n��
    //        return (result, response);
    //    }
    //}

    //// �����擾�p�̃��N�G�X�g�N���X
    //public class TimeHttpRequest : HttpRequest {
    //    public override string Path => TimeHttpClient.WorldTimeApiUrl;
    //}
}

