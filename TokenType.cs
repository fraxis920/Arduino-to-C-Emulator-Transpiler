
namespace Tokeniser
{
    
    class Token
    {
        public TokenType Type;     //Type of Value
        public string Value;       //save Value as string
        public int Position;       //position in the file
        public int line;       // Line in the file
        public int column;     // column in the file
    }
}