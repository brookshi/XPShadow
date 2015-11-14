#region License
//   Copyright 2015 Brook Shi
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections.Generic;

namespace XP
{
    public class ShadowParam
    {
        public byte Alpha { get; set; }

        public float Blur { get; set; }

        public int Offset_Y { get; set; }
    }

    public static class ShadowConfig
    {
        private static Dictionary<int, List<ShadowParam>> _shadowParamsWithZDepth = new Dictionary<int, List<ShadowParam>>()
        {
            [1] = new List<ShadowParam>() { new ShadowParam() { Alpha = 61, Blur = 1.5f, Offset_Y = 1 }, new ShadowParam() { Alpha = 31, Blur = 2f, Offset_Y = 1 } },
            [2] = new List<ShadowParam>() { new ShadowParam() { Alpha = 59, Blur = 3f, Offset_Y = 3 }, new ShadowParam() { Alpha = 41, Blur = 3f, Offset_Y = 3 } },
            [3] = new List<ShadowParam>() { new ShadowParam() { Alpha = 59, Blur = 4f, Offset_Y = 3 }, new ShadowParam() { Alpha = 48, Blur = 5f, Offset_Y = 5 } },
            [4] = new List<ShadowParam>() { new ShadowParam() { Alpha = 56, Blur = 5f, Offset_Y = 4 }, new ShadowParam() { Alpha = 64, Blur = 9f, Offset_Y = 7 } },
            [5] = new List<ShadowParam>() { new ShadowParam() { Alpha = 56, Blur = 7f, Offset_Y = 5 }, new ShadowParam() { Alpha = 77, Blur = 12f, Offset_Y = 10 } },
        };

        public static List<ShadowParam> GetShadowParamForZDepth(int z_depth)
        {
            if (z_depth < 1 || z_depth > 5)
                throw new ArgumentException("z_depth should between 1 to 5");

            return _shadowParamsWithZDepth[z_depth];
        }
    }
}
