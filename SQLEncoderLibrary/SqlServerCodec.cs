using System;
using System.Text;

namespace SQLEncoderLibrary
{
    /// <summary>
    /// Sql server encoder class.
    /// NOTE - ONLY SUPORTS UTF16
    /// </summary>
    public class SqlServerCodec:ICodec
    {
        private string[] hex = new string[256];

        public SqlServerCodec()
        {
            for (char c =(char) 0; c < 0xFF; c++)
            {
                if (c >= 0x30 && c <= 0x39 || c >= 0x41 && c <= 0x5A || c >= 0x61 && c <= 0x7A)
                {
                    // do not do any encoding for regular characters
                    hex[c] = null;
                }
                else
                {
                    //Console.WriteLine(string.Format("0x{0}", Convert.ToByte(c).ToString("x2")));
                    hex[c] = Convert.ToByte(c).ToString("x2");
                }
            }
        }
        public String Encode(char[] immune,string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(encodeCharacter(immune, input[i]));
            }
            return sb.ToString();
        }

        private string encodeCharacter(char[] immune,char c)
        {
            //check for immune characters
            if (containsCharacter(c, immune))
            {
                return "" + c;
            }
            // check for hexadecimal 
            if (getHexForNonAlphanumeric(c)==null)
            {
                return "" + c;
            }
            return encodeASCICharacters(c);
        }

        private string encodeASCICharacters(char c)
        {
            if (c == '\'')
            {
                // add additional apostophe
                return "\'\'";
            }
            else if (c =='-')
            {
                // remove the commented code
                return "";
            }
            else if (c == ';')
            {
                // remove the semicolon 
                return "";
            }
            return "" + c;
        }

        private bool containsCharacter(char c,char[] array)
        {
            for (int i=0; i<array.Length;i++)
            {
                if (c == array[i]) return true;
            }
            return false;
        }

        public string getHexForNonAlphanumeric(char c)
        {
            if (c < 0xFF)
                return hex[c];
            //return toHex(c);
            return "";
        }
    }
}
