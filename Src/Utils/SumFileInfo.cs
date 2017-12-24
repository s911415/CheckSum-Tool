﻿/*
The MIT License

Copyright (c) 2007-2008 Ixonos Plc
Copyright (c) 2007-2008 Kimmo Varis <kimmov@winmerge.org>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Reflection;

namespace CheckSumTool.Utils
{
    /// <summary>
    /// Class formatting info saved to sum files.
    /// </summary>
    public class SumFileInfo
    {
        /// <summary>
        /// Program's name written to sum file.
        /// </summary>
        string _programName;

        /// <summary>
        /// Program version written to sum file.
        /// </summary>
        string _programVersion;

        /// <summary>
        /// Program URL written to sum file.
        /// </summary>
        string _programUrl;

        /// <summary>
        /// Returns program's product name.
        /// </summary>
        public string ProgramName
        {
            get { return _programName; }
            set { _programName = value; }
        }

        /// <summary>
        /// Returns programs version number.
        /// </summary>
        public string ProgramVersion
        {
            get { return _programVersion; }
            set { _programVersion = value; }
        }

        /// <summary>
        /// Return program's homepage URL.
        /// </summary>
        public string ProgramUrl
        {
            get { return _programUrl; }
            set { _programUrl = value; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SumFileInfo()
        {
            ProgramInfo info = new ProgramInfo();
            _programName = info.ProgramName;
            _programUrl = info.URL;
            _programVersion = info.Version;
        }
    }
}
