namespace SQLEncoderLibrary
{
    public class Encoder
    {
        private readonly ICodec _codec;
        // Default codec for SqlServer
        public Encoder(ICodec codec=null)
        {
            if (codec == null)
                codec = new SqlServerCodec();
            _codec = codec;
        }
        /// <summary>
        /// character array for characters that do not need to be encoded.
        /// </summary>
        private char[] immuneSql = { ' ' };
        public string EncodeForSql(string input)
        {
            return _codec.Encode(immuneSql, input);
        }
    }
}
