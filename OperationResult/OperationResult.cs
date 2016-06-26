/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀ 
      █
      █    ▄██████▄                                                                              ▄████████
      █   ███    ███                                                                            ███    ███
      █   ███    ███    ██████▄    ▄██████    ██████   ▄█████      ██     █   ██████  ██▄▄▄▄    ███    ███    ▄██████   ▄██████ ██    █   █           ██     
      █   ███    ███   ██    ██   ██    █    ██   ██   ██   ██ ▀███████▄ ██  ██    ██ ██▀▀▀█▄  ▄███▄▄▄▄██▀   ██    █    ██   ▀  ██    ██ ██       ▀███████▄  
      █   ███    ███   ██    ██  ▄██▄▄▄     ▄██▄▄▄█▀   ██   ██     ██  ▀ ██▌ ██    ██ ██   ██ ▀▀███▀▀▀▀▀    ▄██▄▄▄      ██      ██    ██ ██           ██  ▀  
      █   ███    ███ ▀███████▀  ▀▀██▀▀▀    ▀████████ ▀████████     ██    ██  ██    ██ ██   ██ ▀███████████ ▀▀██▀▀▀    ▀████████ ██    ██ ██           ██     
      █   ███    ███   ██         ██    █    ██   ██   ██   ██     ██    ██  ██    ██ ██   ██   ███    ███   ██    █     ▄   ██ ██    ██ ██▌    ▄     ██     
      █    ▀██████▀   ▄███▀       ████████   ██   ██   ██   █▀    ▄██▀   █    ██████   █   █    ███    ███   ████████  ▄█████▀  ███████  ████▄▄██    ▄██▀     
      █
 ▄ ▄▄ █ ▄▄▄▄▄▄▄▄▄  ▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄ 
 █ ██ █ █████████  ████ ██████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █ 
      █ 
      █  Encapsulates the result of any operation. Includes a result code and a list of messages generated during the operation.
      █
      █  Additional methods provide logging functionality for convenience, and a generic extension class is provided to allow for 
      █  Result instances which contain an object as a return value.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀   
      █  The MIT License (MIT)
      █  
      █  Copyright (c) 2016 JP Dillingham (jp@dillingham.ws)
      █  
      █  Permission is hereby granted, free of charge, to any person obtaining a copy
      █  of this software and associated documentation files (the "Software"), to deal
      █  in the Software without restriction, including without limitation the rights
      █  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
      █  copies of the Software, and to permit persons to whom the Software is
      █  furnished to do so, subject to the following conditions:
      █  
      █  The above copyright notice and this permission notice shall be included in all
      █  copies or substantial portions of the Software.
      █  
      █  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
      █  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
      █  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
      █  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
      █  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
      █  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
      █  SOFTWARE. 
      █ 
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀     ▀▀▀   
      █  Dependencies:
      █     └─ NLog (https://www.nuget.org/packages/NLog/)
      █     
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██ 
                                                                                                   ██   
                                                                                               ▀█▄ ██ ▄█▀                       
                                                                                                 ▀████▀   
                                                                                                   ▀▀                               */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OperationResult
{
    /// <summary>
    /// <para>
    /// The OperationResult namespace contains two types; the <see cref="Result"/> type and the generic extension type <see cref="Result{T}"/>.
    /// </para>
    /// <para>
    /// Both types are designed to be used as method return types.  Both types contain a <see cref="ResultCode"/> enumeration used to describe 
    /// the result of the operation in terms of success, success with warnings, or failure, and a <see cref="List{T}"/> of type <see cref="Message"/> 
    /// containing a list of messages generated during the operation.
    /// </para>
    /// <para>
    /// These types allow the programmer to defer logging to other parts of the application code, as well as allowing more detailed information about
    /// the operation to be conveyed to calling members without throwing expensive exceptions or passing special values in return types.
    /// </para>
    /// </summary>
    /// <remarks>
    /// This library depends on NLog (https://www.nuget.org/packages/NLog/) for logging functions.
    /// </remarks>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc { }
}
