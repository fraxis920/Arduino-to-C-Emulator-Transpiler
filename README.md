# Arduino C# Transpiler / Lexer Engine

A C# project that parses Arduino/C++ source code into an intermediate token-based representation using lexical analysis. This is the first stage of a custom compiler pipeline designed to eventually support full parsing, AST generation, and code translation/execution in .NET.

## Features

- Full lexical analysis of Arduino/C++-like syntax
- Support for:
  - Keywords (if, while, for, switch, etc.)
  - Data types (int, float, char, bool, etc.)
  - Operators and bitwise logic
  - Preprocessor directives (#include, #define)
  - String, char, and numeric literals
- Comment removal (single-line and multi-line)
- Token stream generation with type/value mapping
- Foundation for parser, AST, and code generator

## Project Structure

- `TokenType.cs` → Defines all supported token categories
- `Token.cs` → Token data structure (type, value, position)
- `Lexer.cs` → Core lexical analyzer that converts source code into tokens
- `Parser.cs` *(planned)* → Will transform tokens into an AST
- `Ast.cs` *(planned)* → Abstract Syntax Tree representation
- `Generator.cs` *(planned)* → Code generation or interpretation layer

## How It Works

The system follows a standard compiler pipeline:

The current implementation focuses on the **Lexer stage**, which reads raw Arduino `.ino` files and converts them into a structured token list.

## Current Limitations

- Parser not yet implemented
- AST generation not yet implemented
- Execution layer not available
- Limited error handling
- Input file path is hardcoded

## Future Improvements

- Full recursive descent parser
- AST-based execution or translation
- Support for functions and classes
- Better error reporting system
- Dynamic file input instead of static path
- Modular plugin-like architecture for language features

## Goal

To build a lightweight educational compiler framework in C# capable of understanding and translating Arduino-style C/C++ code into a structured intermediate representation suitable for .NET execution or further compilation.

## License

Free to use for educational purposes.