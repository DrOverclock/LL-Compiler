﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLCompiler.Lexer
{

    public class Lexer
    {
        public static IEnumerable<IToken> ProcessString(string str)
        {
            char[] allowedChars = { '+', '-', '/', '*', '>', '-' };
            for(int i = 0; i < str.Length; i++)
            {
                if(char.IsWhiteSpace(str[i]))
                    continue;

                // identifier token
                if(char.IsLetter(str[i]) || char.IsSymbol(str[i]) || Array.IndexOf(allowedChars, str[i]) != -1)
                {
                    StringBuilder tk = new StringBuilder(str[i].ToString());
                    while (i + 1 < str.Length && char.IsLetterOrDigit(str[i + 1]))
                        tk.Append(str[++i]);

                    yield return new IdentifierToken { Name = tk.ToString() };
                    continue;
                }

                // digit token
                if(char.IsDigit(str[i]))
                {
                    StringBuilder integer = new StringBuilder(str[i].ToString());
                    while (i + 1 < str.Length && char.IsDigit(str[i + 1]))
                        integer.Append(str[++i]);

                    yield return new IntegerConstantToken { Value = int.Parse(integer.ToString()) };
                    continue;
                }

                // parenthese token
                if (str[i] == '(' || str[i] == ')')
                {
                    switch (str[i])
                    {
                        case '(':
                            yield return new ParentheseToken { isOpening = true };
                            break;
                        case ')':
                            yield return new ParentheseToken { isOpening = false };
                            break;
                    }
                    continue;
                }

                // char token
                if (str[i] == '\'')
                {
                    yield return new CharConstantToken { Value = str[++i] }; // yield char
                    i++; // remove last '
                    continue;
                }

                // string token
                if (str[i] == '\"')
                {
                    StringBuilder strtk = new StringBuilder();
                    while (i + 1 < str.Length && str[i + 1] != '\"')
                        strtk.Append(str[++i]);
                    yield return new StringConstantToken { Value = strtk.ToString() };
                    i++; // remove last "
                    continue;
                }

                // if we reach this point, the token is unknown -> exception
                throw new Exception("Lexer: Unkown token!");
            }

            

            yield break;
        }
    }
}
