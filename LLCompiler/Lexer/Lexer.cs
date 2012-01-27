﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLCompiler.Lexer
{

    public class Lexer
    {
        public IEnumerable<IToken> ProcessString(string str)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if(char.IsWhiteSpace(str[i]))
                    continue;

                // identifier token
                if(char.IsLetter(str[i]))
                {
                    StringBuilder tk = new StringBuilder(str[i].ToString());
                    while (i + 1 < str.Length && char.IsLetter(str[i + 1]))
                        tk.Append(str[++i]);

                    yield return new IdentifierToken { name = tk.ToString() };
                    continue;
                }

                // digit token
                if(char.IsDigit(str[i]))
                {
                    StringBuilder integer = new StringBuilder(str[i].ToString());
                    while (i + 1 < str.Length && char.IsDigit(str[i + 1]))
                        integer.Append(str[++i]);

                    yield return new IntegerConstantToken { value = int.Parse(integer.ToString()) };
                    continue;
                }

                // parenthese token
                switch (str[i])
                {
                    case '(':
                        yield return new ParentheseToken { isOpening = true };
                        break;
                    case ')':
                        yield return new ParentheseToken { isOpening = false };
                        break;
                }
            }

            

            yield break;
        }
    }
}
