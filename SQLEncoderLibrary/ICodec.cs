using System;
using System.Collections.Generic;
using System.Text;

namespace SQLEncoderLibrary
{
    public interface ICodec
    {
        string Encode(char[] immune,string input);
    }
}
