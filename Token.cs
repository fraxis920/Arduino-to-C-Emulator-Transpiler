

namespace Tokeniser
{
    public enum TokenType
    {
        // identifiers
        Identifier,          // Variable, function, class or object name

        // literals
        NumberLiteral,       // Numeric value: 123, 3.14
        StringLiteral,       // String value: "hello"
        CharLiteral,         // Character value: 'a'
        BoolLiteral,         // Boolean value: true / false

        // keywords
        KeywordIf,           // if statement
        KeywordElse,         // else statement
        KeywordWhile,        // while statement
        KeywordContinue,     // Continue statement
        KeywordDo,           // Do statement
        KeywordFor,          // for statement
        KeywordSwitch,       // switch statement
        KeywordCase,         // case statement
        KeywordDefault,      // default statement
        KeywordBreak,        // break keyword
        KeywordReturn,       // return keyword
        KeywordTypedef,      // Typedef

        SizeofOperator,        // Sizeof()

        ModifierConst,       // Const
        ModifierStatic,      // Static

        // types
        TypeInt,             // int type
        TypeString,          // string typr
        TypeBool,            // bool type
        TypeChar,            // char type
        TypeFloat,          // float type
        TypeVoid,            // void type
        TypeLong,            // long type
        TypeUnsigned,        // unsigned type
        TypeStruct,          // struct declaration
        TypeEnum,            // enum declaration

        // grammar
        OpenParen,           // (
        CloseParen,          // )

        OpenBracket,         // [
        CloseBracket,        // ]

        OpenBrace,           // {
        CloseBrace,          // }

        Dot,                 // .
        Comma,               // ,
        Semicolon,           // ;
        Colon,               // :
        QuestionMark,        //?

        // operators
        Assign,              // =
        Plus,                // +
        Minus,               // -
        Multiply,            // *
        Divide,              // /
        Modulo,              // %   

        Increment,           // ++
        Decrement,           // --
        PlusEqual,           // +=
        MinusEqual,          // -=
        MultiplyEqual,       // *=
        DivideEqual,         // /=
        ModuloEqual,         // %=

        CompareEqual,        // ==
        CompareNotEqual,     // !=
        CompareGreater,      // >
        CompareLess,         // <
        CompareGreaterEqual, // >=
        CompareLessEqual,    // <=

        BitwiseAndEqual,    // &=
        BitwiseOrEqual,    // |=
        BitwiseXorEqual,   // ^=

        
        LogicAnd,            // &&
        LogicOr,             // ||
        LogicNot,            // !

        //Bitwise
        BitwiseAnd,     // &
        BitwiseOr,      // |
        BitwiseXor,     // ^
        BitwiseNot,     // ~

        LeftShift,      // <<
        RightShift,     // >>

        // special
        PreprocessorInclude,       
        PreprocessorDefine,
        EOF             // End of file


    }
}

