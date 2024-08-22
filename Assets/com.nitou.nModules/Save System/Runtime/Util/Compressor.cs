using System.IO;
using System.IO.Compression;

namespace nitou.SaveSystem.Utils {

    /// <summary>
    /// �o�C�i���f�[�^��gzip�t�H�[�}�b�g�ɏ]���Ĉ��k�A�𓀂���ÓI�N���X
    /// </summary>
    public static class Compressor{

        /// <summary>
        /// �f�[�^�����k����
        /// </summary>
        public static byte[] Compress(byte[] rawData) {
            byte[] result = null;

            using (MemoryStream compressedStream = new MemoryStream()) {
                using (GZipStream gZipStream = new GZipStream(compressedStream, CompressionMode.Compress)) {
                    gZipStream.Write(rawData, 0, rawData.Length);
                }
                result = compressedStream.ToArray();
            }

            return result;
        }

        /// <summary>
        /// �f�[�^���𓀂���
        /// </summary>
        public static byte[] Decompress(byte[] compressedData) {
            byte[] result = null;

            using (MemoryStream compressedStream = new MemoryStream(compressedData)) {
                using (MemoryStream decompressedStream = new MemoryStream()) {
                    using (GZipStream gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress)) {
                        gZipStream.CopyTo(decompressedStream);
                    }
                    result = decompressedStream.ToArray();
                }
            }

            return result;
        }
    }
}
