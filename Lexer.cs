using System;
using System.Collections;
using System.Collections.Generic;

using System.Security.Principal;

namespace Tokeniser
{
    class Lexer
    {
        private int Position;
        string word = "";
        private char currentCaracter;
        private char peekCaracter;
        List<Token> Tokenlist = new List<Token>();
        Dictionary<string, TokenType> Type = new Dictionary<string, TokenType>()
        {
            {"if", TokenType.KeywordIf},
            {"else", TokenType.KeywordElse},
            {"while", TokenType.KeywordWhile},
            {"do", TokenType.KeywordDo},
            {"continue", TokenType.KeywordContinue},
            {"for", TokenType.KeywordFor},
            {"switch", TokenType.KeywordSwitch},
            {"case", TokenType.KeywordCase},
            {"default", TokenType.KeywordDefault},
            {"break", TokenType.KeywordBreak},
            {"return", TokenType.KeywordReturn},
            {"typedef", TokenType.KeywordTypedef},
            {"sizeof", TokenType.SizeofOperator},

            {"const", TokenType.ModifierConst},
            {"static", TokenType.ModifierStatic},

            {"int", TokenType.TypeInt},
            {"string", TokenType.TypeString},
            {"bool", TokenType.TypeBool},
            {"char", TokenType.TypeChar},
            {"float", TokenType.TypeFloat},
            {"void", TokenType.TypeVoid},
            {"long", TokenType.TypeLong},
            {"unsigned", TokenType.TypeUnsigned},
            {"struct", TokenType.TypeStruct},
            {"enum", TokenType.TypeEnum},

            {"(", TokenType.OpenParen},
            {")", TokenType.CloseParen},

            {"[", TokenType.OpenBracket},
            {"]", TokenType.CloseBracket},

            {"{", TokenType.OpenBrace},
            {"}", TokenType.CloseBrace},

            {".", TokenType.Dot},
            {",", TokenType.Comma},
            {";", TokenType.Semicolon},
            {":", TokenType.Colon},
            {"%", TokenType.Modulo},
            {"?", TokenType.QuestionMark},

            {"=", TokenType.Assign},
            {"+", TokenType.Plus},
            {"-", TokenType.Minus},
            {"*", TokenType.Multiply},
            {"/", TokenType.Divide},

            {"++", TokenType.Increment},
            {"--", TokenType.Decrement},

            {"+=", TokenType.PlusEqual},
            {"-=", TokenType.MinusEqual},
            {"*=", TokenType.MultiplyEqual},
            {"/=", TokenType.DivideEqual},
            {"%=", TokenType.ModuloEqual},

            {">", TokenType.CompareGreater},
            {"<", TokenType.CompareLess},

            {"==", TokenType.CompareEqual},
            {"!=", TokenType.CompareNotEqual},
            {">=", TokenType.CompareGreaterEqual},
            {"<=", TokenType.CompareLessEqual},
            {"&=", TokenType.BitwiseAnd},
            {"|=", TokenType.BitwiseOr},
            {"^=", TokenType.BitwiseXor},

            {"&&", TokenType.LogicAnd},
            {"&", TokenType.BitwiseAnd},
            {"||", TokenType.LogicOr},
            {"|", TokenType.BitwiseOr},
            {"!", TokenType.LogicNot},
            {"~", TokenType.BitwiseNot},
            {"^", TokenType.BitwiseXor},
            {">>", TokenType.RightShift},
            {"<<", TokenType.LeftShift},

        };
        string sourceCode = File.ReadAllText(@"Path file");
        

        private void Advance()      //main loop
        {
            while(Position < sourceCode.Length)
            {
                currentCaracter = sourceCode[Position];
                Peek();
                Tokengen();
            }
            Tokenlist.Add(new Token {Type = TokenType.EOF, Value = "<EOF>"});
            foreach(Token token in Tokenlist)
            {
                Console.WriteLine($"Type: {token.Type}  Value: {token.Value}");
            }
        }

        private void Peek()     // peek next char in string sourceCode
        {
            if(Position+1 < sourceCode.Length)
            {
                peekCaracter = sourceCode[Position+1];      
            } 
        }

        private void SkipWhitespace()   // skip comment\space
        {
            while(char.IsWhiteSpace(currentCaracter) && Position+1 < sourceCode.Length)   // Skip space
            {
               Position++;
               currentCaracter = sourceCode[Position]; 
               Peek();
            
            if(currentCaracter == '/' && peekCaracter == '/' && Position+1 < sourceCode.Length)       // Skip comments
            {
                while(currentCaracter != '\n' && Position+1 < sourceCode.Length)
                {
                    Position++;
                    currentCaracter = sourceCode[Position];
                    Peek();
                }
            }
            if(currentCaracter == '/' && peekCaracter == '*' && Position+1 < sourceCode.Length)
            {
                while(!(currentCaracter == '*' && peekCaracter == '/') && Position+1 < sourceCode.Length)  // Skip long comment
                {
                    Position++;
                    currentCaracter = sourceCode[Position];
                    Peek();
                }
                if(Position+2 <= sourceCode.Length) // Skip final */
                {
                    Position += 2;
                    currentCaracter = sourceCode[Position];
                    Peek();
                }
            }
            }
            
        }

        private TokenType GetWordType()
        {
            if (Type.TryGetValue(word, out TokenType type)) return type;    // If the word exists in the keyword dictionary, return the corresponding TokenType
            return TokenType.Identifier;    // Else return Identifier  
        }

        private void StoresToken()
        {
            Tokenlist.Add(new Token {Type = GetWordType(), Value = word});     // Save the current word in the list
            word = "";  //reset
        }

        private bool IsDelimiter(char c)
        {
            return  c == ':' ||
                    c == '.' ||
                    c == ',' ||
                    c == ';' ||
                    c == '(' ||
                    c == ')' ||
                    c == '[' ||
                    c == ']' ||
                    c == '{' ||
                    c == '}' ||
                    c == '=' ||
                    c == '+' ||
                    c == '-' ||
                    c == '*' ||
                    c == '/' ||
                    c == '!' ||
                    c == '>' ||
                    c == '<' ||
                    c == '&' ||
                    c == '|' ||
                    c == '"' ||
                    c == '\'';
        }

        private void IsDuble()      // Store current word + next one
        {
            word += peekCaracter;
            StoresToken();
        }

        private void GetLib()       // get define / include
        {
            Position++;
            Peek();
            SkipWhitespace();
            word="";
            while(currentCaracter != '>' && !(char.IsDigit(currentCaracter) && char.IsWhiteSpace(peekCaracter)))
            {
                                Peek();
                 currentCaracter = sourceCode[Position];     
                 word += currentCaracter;                    
                 Position++;
            }
            if(currentCaracter == '>') Tokenlist.Add(new Token {Type = TokenType.PreprocessorInclude, Value = word});
            else Tokenlist.Add(new Token {Type = TokenType.PreprocessorDefine, Value = word});
            word ="";
        }

        private void FindType()
        {
           if(char.IsWhiteSpace(peekCaracter) || IsDelimiter(peekCaracter) || char.IsDigit(currentCaracter) || currentCaracter == '"' || currentCaracter == '\'')       // Stores string/number/char/bool/Identifier
                    { 
                        if(currentCaracter == '"')      // Is string
                        {
                            word = "";
                            Position ++;
                            while(peekCaracter!='"')    //store string until the peekchar is "
                            {
                               Peek();                                     //peek next char
                               currentCaracter = sourceCode[Position];     //update currentchar
                               word += currentCaracter;                    //add the char to the string
                               Position++;                                 // move the cursore by 1
                            }
                            Tokenlist.Add(new Token {Type = TokenType.StringLiteral, Value = word});      //add the string to the list
                            word = "";
                        }
                        else if(char.IsDigit(currentCaracter))      // Is number   
                        {
                            while(char.IsLetterOrDigit(peekCaracter) || !IsDelimiter(peekCaracter))       // Store int
                            {   
                               Position++;                                  // move the cursore by 1                         
                               Peek();                                      //peek next char
                               currentCaracter = sourceCode[Position];      //update currentchar
                               word += currentCaracter;                     //add the char to the string                                                                 
                            }
                            Tokenlist.Add(new Token {Type = TokenType.NumberLiteral, Value = word});      //add the number to the list
                            word = "";
                        }
                        else if(currentCaracter == '\'') // Is char
                        {
                            word = "";
                            Position ++;
                            while(sourceCode[Position] != '\'')   
                            {                                    
                               currentCaracter = sourceCode[Position];    
                               word += currentCaracter;                    //add the char to the string
                               Position++;                                 // move the cursore by 1
                            }
                            Tokenlist.Add(new Token {Type = TokenType.CharLiteral, Value = word});      //add to list
                            word = "";
                        }
                        else if(word == "true" || word == "false")   // is bool
                        {
                            Tokenlist.Add(new Token {Type = TokenType.BoolLiteral, Value = word});    //add to list     
                            word = "";
                        }
                        else if(word == "#include" || word == "#define")
                                {
                                    GetLib();
                                }
                        else if(!string.IsNullOrWhiteSpace(word)) StoresToken();  //store it as a Identifier if it is not null(\t \n \r space)
                    }    
        }

        private void Tokengen()
        {
            SkipWhitespace();
            Peek();
            word +=currentCaracter;     // Build up the string
            switch(currentCaracter)
            {
                case '.': StoresToken(); Position++;   break;
                case ',': StoresToken(); Position++;   break;
                case ';': StoresToken(); Position++;   break;
                case ':': StoresToken(); Position++;   break;
                case '(': StoresToken(); Position++;   break;
                case ')': StoresToken(); Position++;   break;
                case '[': StoresToken(); Position++;   break;
                case ']': StoresToken(); Position++;   break;
                case '{': StoresToken(); Position++;  break;
                case '}': StoresToken(); Position++;   break;
                case '~': StoresToken(); Position++;   break;
                case '?': StoresToken(); Position++;   break;
                case '^':
                    if(peekCaracter == '=')     // XorEqual
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // BitwiseXor
                    {
                        StoresToken(); 
                        Position++;
                    }
                    break;
                case '=': 
                    if(peekCaracter == '=')     // Assignament
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // Minus
                    {
                        StoresToken(); 
                        Position++;
                    }
                    break;
                case '+':
                    if(peekCaracter == '+' || peekCaracter == '=')     // Increment / PlusEqual
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    //plus
                    {
                        StoresToken(); 
                        Position++;
                    }
                    break;
                case '*':
                    if(peekCaracter == '=')     // multiply equal
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // Moltiply
                    {
                        StoresToken(); 
                        Position++;
                    }
                    break;
                case '/': 
                    if(peekCaracter == '=')     // Divide equal
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // Divide
                    {
                        StoresToken(); 
                        Position++;
                    }
                    break;
                case '-': 
                    if(peekCaracter == '-' || peekCaracter == '=')     // Decrement / MinusEqual
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // Minus
                    {
                        StoresToken();
                        Position++; 
                    }
                    break;
                case '%': 
                    if(peekCaracter == '=')     // Modulo equal
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // Modulo
                    {
                        StoresToken(); 
                        Position++;
                    }
                    break;
                case '!': 
                    if(peekCaracter == '=')     // NotEqual
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // Not
                    {
                        StoresToken(); 
                        Position++;
                    }
                    break;
                case '>':
                    if(peekCaracter == '=' || peekCaracter == '>')     // GraterEqual / RightShift / 
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // Grater
                    {
                        StoresToken();
                        Position++; 
                    }
                    break;
                case '<':
                    if(peekCaracter == '=' || peekCaracter == '<')     // LessEqual / LeftShift
                    {
                        IsDuble();
                        Position+=2;
                    }
                    else    // Less
                    {
                        StoresToken();
                        Position++;

                    }
                    break;
                case '&':                       
                    if(peekCaracter == '&' || peekCaracter == '=')      // And / AndEqual
                    {
                        IsDuble();
                        Position+=2; 
                    }
                    else        // BitwiseAnd
                    {
                        StoresToken();
                        Position++;
                    }
                    break;
                case '|':                       
                    if(peekCaracter == '|' || peekCaracter == '=')      // Or / OrEqual
                    {
                        IsDuble();
                        Position+=2; 
                    }
                    else        //BiteiseOr
                    {
                        StoresToken();
                        Position++;
                    }
                    break;
                default: FindType(); Position++; break;
            }

        }  
            static void Main()      
            {
                Lexer lexer = new Lexer();
                lexer.Advance();
            }
    }
}        
    
