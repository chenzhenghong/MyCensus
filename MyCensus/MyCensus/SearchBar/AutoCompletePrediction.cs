//
// AutoCompletePrediction.cs
//
// Author:
//       Alex Smith <alex@duriancode.com>
//
// Copyright (c) 2017 (c) Alexander Smith
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Newtonsoft.Json;

namespace MyCensus.PlacesSearchBar
{
	/// <summary>
	/// Auto complete prediction.
	/// </summary>
	public class AutoCompletePrediction
    {
        [JsonProperty("brand")]
        /// <summary>
        /// Ʒ��(�ն������۵�ơ��Ʒ��)�ൺơ�ơ�ѩ��ơ�ơ�Budweiser����������
        /// </summary>
        public string Brand { get; set; }


        [JsonProperty("productname")]

        /// <summary>
        ///  ��Ʒ����(ɨ��+����)
        /// </summary>
        public string ProductName { get; set; }


        [JsonProperty("annualsales")]
        /// <summary>
        ///  ����������д���� ��/�䣩4����Ʒ�ƣ��ൺơ�ơ�ѩ��ơ�ơ�Budweiser��������������Ϊ�ĵ����������ߵ������ϣ��е����е��ߣ��е��͡�����
        /// </summary>
        public string AnnualSales { get; set; }


        [JsonProperty("packingform")]
        /// <summary>
        ///  ��װ��ʽ(ƿ������Ͱ)(����ơ�Ʋ�Ʒ����С��װ��ʽ)
        /// </summary>
        public string PackingForm { get; set; }


        [JsonProperty("productprovider")]
        /// <summary>
        /// ��Ʒ������
        /// </summary>
        public string ProductProvider { get; set; }


        [JsonProperty("channelattributes")]
        /// <summary>
        /// ��������((һ��������������)������ơ�Ʋ�Ʒ�����ն˵���������������ơ�Ʋ�Ʒ�����еĲ㼶������һ����������������һ��������֮�⣩��
        /// </summary>
        public string ChannelAttributes { get; set; }



        [JsonProperty("specification")]
        /// <summary>
        /// ���
        /// </summary>
        public string Specification { get; set; }


        [JsonProperty("barcode")]
        /// <summary>
        /// ����
        /// </summary>
        public string BarCode { get; set; }


        [JsonProperty("grade")]
        /// <summary>
        /// ����
        /// </summary>
        public string Grade { get; set; }


        public string Detalls { get; set; }
    }

}
