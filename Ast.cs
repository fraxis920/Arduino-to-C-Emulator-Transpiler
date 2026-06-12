using System.Collections;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace interpreter
{
    abstract class Preprocessor
    {
    }

    class IncludePreprocessor : Preprocessor
    {
        public Identifier LibraryName;
        public IncludePreprocessor(Identifier Library)
        {
            LibraryName = Library;
        }
    }

    class DefinePreprocessor : Preprocessor
    {
        public Identifier Name;
        public int Value;
        public DefinePreprocessor(Identifier name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    abstract class Statement
    {
    }

    class BlockNode
    {
        public List<Statement> Block;
        public BlockNode(List<Statement> block)
        {
            Block = block;
        }
    } 

    class VariableDeclaration : Statement
    {
        public enum Modifiers
        {
            Unsigned,
            Const,
            Static,
        }

        public enum DataType
        {
            String,
            Int, 
            Bool, 
            Float,
            Char,
            Long,
        }
        public List<Modifiers> Mod;
        public DataType Type;              // Type of the vareable
        public Identifier Identifier;           // Variable ID
        public List<Expression> Dimension;
        public Expression? Initializer;            // Value given to ID 
            protected VariableDeclaration(List<Modifiers>mod, DataType type, Identifier identifier, List<Expression>dimension, Expression? value)
            {
                Mod = mod;
                Type = type;
                Identifier = identifier;
                Dimension = dimension;
                Initializer = value;
            }
    
    }

    class Assignment : Statement
    {
        public Identifier ID;
        public Expression Value;
        public Assignment(Identifier id, Expression value)
        {
            ID = id;
            Value = value;
        }
    }

    class IfStatementNode : Statement
    {
       public Expression Condition; 
       public BlockNode ThenBlock;
       public BlockNode? ElseBlock;
       public IfStatementNode(Expression statement, BlockNode then, BlockNode? Else)
        {
            Condition = statement;
            ThenBlock = then;
            ElseBlock = Else;
        }
    }

    class WhileStatementNode : Statement
    {
       public Expression Condition;
       public BlockNode Body;
       public WhileStatementNode(Expression condition, BlockNode body)
        {
            Condition = condition;
            Body = body;
        }
    }

    class DoWhileStatementNode : Statement
    {
        public BlockNode Body;
        public Expression Condition;
        public DoWhileStatementNode(Expression condition, BlockNode body)
        {
            Body = body;
            Condition = condition;
        }
    }

    class ForStatementNode : Statement
    {
        public Statement Initialization;
        public Expression Condition;
        public Expression Increment;
        public BlockNode Body;
        public ForStatementNode(Statement initialization, Expression condition, Expression increment, BlockNode body)
        {
            Initialization = initialization;
            Condition = condition;
            Increment = increment;
            Body = body;
        }
    }

    class SwitchStatementNode : Statement
    {
        public Identifier Variable;
        public List <CaseLable> Case;
        public SwitchStatementNode(Identifier var, List <CaseLable> CASE)
        {
            Variable = var;
            Case = CASE;
        }
    }

    class CaseLable
    {
        public Expression? Case; 
        public BlockNode Body;
        public CaseLable(Expression? CASE, BlockNode body)
        {
            Case = CASE;
            Body = body;
        }

    }

    class BreakStatement : Statement
    {        
    }

    class ContinueStatement : Statement
    {       
    }

    class ReturnStatement : Statement
    {       
    }

    abstract class Declaration 
    {
    }

    class FunctionDeclaration : Declaration
    {
        public enum ReturnType
        {
            Void,
            String,
            Int, 
            Bool, 
            Float,
            Char,
            Long,
        }
        public enum Modifiers
        {
            Unsigned,
            Const,
            Static,
        } 
        public List<Modifiers> Mod;
        public ReturnType Type;
        public Identifier Name;
        public List<ParameterDeclaration> Parameters;
        public BlockNode Body;
        public FunctionDeclaration(List<Modifiers> modifiers, ReturnType type, Identifier name, List<ParameterDeclaration> parameters, BlockNode body)
        {
            Mod = modifiers;
            Type = type;
            Name = name;
            Parameters = parameters;
            Body = body;
        }
    }

        class ParameterDeclaration 
    {
        public enum TransferMethod
        {
            Standard,
            Reference,
            Address,
            Pointer
        }
        public enum Modifiers
        {
            Unsigned,
            Const,
            Static,
        }

        public enum DataType
        {
            String,
            Int, 
            Bool, 
            Float,
            Char,
            Long,
        }
        public List<Modifiers> Mod;
        public DataType Type;             
        public Identifier Identifier;
        public List<Expression> Dimension;
        public TransferMethod Method;
        
        public ParameterDeclaration(List<Modifiers>mod, DataType type, Identifier identifier, List<Expression> dimension, TransferMethod method)
        {
            Mod = mod;
            Type = type;
            Identifier = identifier;
            Dimension = dimension;
            Method = method;
        } 
    }

    class EnumDeclaration : Declaration
    {
        public Identifier Name;
        public Members Value;
        public EnumDeclaration(Identifier name, Members value)
        {
            Name = name;
            Value = value;
        }
    }

    class StructDeclaration : Declaration
    {
        public Identifier? Name;
        public BlockNode Body;
        public StructDeclaration(Identifier? name, BlockNode body)
        {
            Name = name;
            Body = body;
        }
    }

    class ExpressionStatementNode : Statement
    {
        public Expression Expression;
    }

    class ClassDeclaretion : Declaration        //REVISION NEEDED
    {
        public Identifier Name;
        public List<ClassBody> Body;

    }

    class ClassBody                          //REVISION NEEDED
    {
        public enum AccessSpecifier
        {
            Public,
            Private
        }
        public AccessSpecifier Access;
        public BlockNode Body;
        public ClassBody(AccessSpecifier access, BlockNode body)
        {
            Access = access;
            Body = body;
        }
    }

    abstract class Expression  
    {
    }

    class Identifier : Expression
    {
        public string? ID;
        public Identifier(string? name)
        {
            ID = name;
        }
    }
    class Binary : Expression
    {
        public enum BynaryOperator
        {
            Add,            // +
            Subtract,       // -
            Multiply,       // *
            Divide,         // /
            Modulo,         // %

            // Assegnazione
            Assign,         // =
            AddAssign,      // +=
            SubAssign,      // -=
            MulAssign,      // *=
            DivAssign,      // /=
            ModAssign,      // %=

            // Confronto
            Equal,          // ==
            NotEqual,       // !=
            Less,           // <
            LessEqual,      // <=
            Greater,        // >
            GreaterEqual,   // >=

            // Logici
            LogicalAnd,     // &&
            LogicalOr,      // ||

            // Bitwise
            BitwiseAnd,     // &
            BitwiseOr,      // |
            BitwiseXor,     // ^
            LeftShift,      // <<
            RightShift,     // >>

            // Assegnazioni bitwise
            AndAssign,      // &=
            OrAssign,       // |=
            XorAssign,      // ^=
            LeftShiftAssign,// <<=
            RightShiftAssign,// >>= 
        }
        public Expression Left;
        public BynaryOperator Operator;
        public Expression Right;

        public Binary(Expression left, BynaryOperator op, Expression right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }           
    }

    class Unary : Expression
    {
        public enum UnaryOperator
        {
            Plus,          
            Minus,        
            LogicalNot,     
            BitwiseNot,     
            Increment,   
            Decrement,   
        }
        public UnaryOperator Operator;
        public Expression Operand;  
        public Unary(UnaryOperator op, Expression operand)
        {
            Operator = op;
            Operand = operand;
        }
    }

    class Literal : Expression      
    {
        public object Value;        

        public Literal(object value)
        {
            Value = value;
        }
    }

    class FunctionCall : Expression
    {
        public Expression FunctionName; 
        public List<Expression> Arguments;
        public FunctionCall(Expression Function, List<Expression>arguments)
        {
            FunctionName = Function;
            Arguments = arguments;
        }
    }

    class MemberAccess : Expression
    {
        public Identifier ObjectName;
        public Identifier FunctionName;
        public MemberAccess(Identifier Object, Identifier function)
        {
           ObjectName = Object;
           FunctionName = function;
        }
    }
    class Members : Expression
    {
        public List<Expression> Value;
        public Members(List<Expression> value)
        {
            Value = value;
        }
    }

    class ArrayAccess : Expression
    {
        public Identifier Name;
        public Expression Position;
        public ArrayAccess(Identifier name, Expression position)
        {
            Name = name;
            Position = position;
        }
    }

    class Cast : Expression
    {
        public enum DataType
        {
            String,
            Int, 
            Bool, 
            Float,
            Char,
            Long,
        }
        public DataType TargetType;
        public Expression Name; 
        public Cast(DataType Target, Expression name)
        {
            Name = name;
            TargetType = Target;
        }
    }
    
}