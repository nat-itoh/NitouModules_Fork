using System;

namespace nitou.Networking {

    [Serializable]
    public class HttpResponseStatus{
        public int ok;
        public int error_code;
    }

    /// <summary>
    /// ���X�|���X�̊��N���X
    /// </summary>
    public abstract class HttpResponse {
        public HttpResponseStatus status;
    }
}
