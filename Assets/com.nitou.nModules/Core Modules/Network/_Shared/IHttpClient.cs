using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace nitou.Networking {

    /// <summary>
    /// 
    /// </summary>
    public interface IHttpClient {

        UniTask<(HttpRequest.Result result, T response)> SendAsync<T>(HttpRequest request, Func<string, T> parseMethod,
            CancellationToken token)
            where T : HttpResponse, new();
    }


    public class HttpClient : IHttpClient {

        public async UniTask<(HttpRequest.Result result, T response)> SendAsync<T>(
           HttpRequest request, Func<string, T> parseMethod,
           CancellationToken token)
            where T : HttpResponse, new() {

            using (var unityWebRequest = UnityWebRequest.Get(request.Path)) {

                // �ʐM��񓯊��Ŏ��s
                var operation = await unityWebRequest.SendWebRequest().ToUniTask(cancellationToken: token);

                // ���s���i�ʐM�G���[�j
                if (operation.result == UnityWebRequest.Result.ConnectionError || operation.result == UnityWebRequest.Result.ProtocolError) {
                    return (new HttpRequest.Failed(), new T());
                }
                // ������
                else if (operation.result == UnityWebRequest.Result.Success) {
                    // ���X�|���X�f�[�^���擾
                    var responseData = unityWebRequest.downloadHandler.text;
                    T response;

                    // �����Ƃ��ēn���ꂽ�p�[�X���\�b�h�𗘗p���ă��X�|���X�f�[�^������
                    try {
                        response = parseMethod(responseData);
                    } catch (Exception ex) {
                        // �p�[�X���s���̃G���[�n���h�����O
                        UnityEngine.Debug.LogError($"Error parsing response: {ex.Message}");
                        return (new HttpRequest.Failed(), new T());
                    }

                    return (new HttpRequest.Success(), response);
                }
                // �L�����Z����
                return (new HttpRequest.Canceld(), new T());
            }
        }
    }
}
